using Apeiron.IO;
using System.Text.RegularExpressions;

namespace Apeiron.Platform.Performers.OrioleKmtPerformers;

/// <summary>
/// Представляет исполнителя, выполняющего поиск файлов Oriole Ktm.
/// </summary>
public sealed class OrioleKmtFileVisor : Performer
{
    /// <summary>
    /// Представляет путь к корневому каталогу размещения файлов.
    /// </summary>
    private const string _RootPathToFiles = @"\\railtest.ru\Data\06-НТО\RawData\Oriole\KMT";

    /// <summary>
    /// Поле для хранения списка файлов в базе данных.
    /// </summary>
    private readonly List<FileProp> _DatabaseFiles;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="journal">
    /// Журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="journal"/> передана пустая ссылка.
    /// </exception>
    public OrioleKmtFileVisor(Journal journal) :
        base(journal)
    {
        //  Создание списка для хранений файлов, загруженных в базу данных.
        _DatabaseFiles = new();
    }

    /// <summary>
    /// Асинхронно выполняет работу исполнителя.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу исполнителя.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public override sealed async Task PerformAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Доп. задержка, для вывода изначальных сообщений в консоль.
            await Task.Delay(50, cancellationToken).ConfigureAwait(false);

            await Journal.LogInformationAsync(
                $"Поиск файлов", cancellationToken).ConfigureAwait(false);

            //  Запуск загрузки файлов в БД.
            await LoadFilePathsAsync(_RootPathToFiles, cancellationToken).ConfigureAwait(false);

            await Journal.LogWarningAsync(
                "Итерация загрузки данных в БД завершена.",
                cancellationToken).ConfigureAwait(false);

            //  Доп. задержка.
            await Task.Delay(15000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Класс для хранения информации о файле на файловой системе.
    /// </summary>
    private sealed class FileProp
    {
        public string FilePath { get; init; } = null!;
        public DateTime Time { get; init; }
    }

    /// <summary>
    /// Асинхронно выполняет поиск информации по каталогу необработанных данных.
    /// </summary>
    /// <param name="filePath">Идентификатор каталога необработанных данных.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, выполняющая поиск.</returns>
    /// <exception cref="OperationCanceledException">Операция отменена.</exception>
    private async Task LoadFilePathsAsync(string filePath, CancellationToken cancellationToken)
    {
        Check.IsNotNull(filePath, nameof(filePath));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Паттерн выборки видео файлов.
        var searchPattern = new Regex(@"$(?<=\.(mp4))", RegexOptions.IgnoreCase);

        // Получение списка файлов.
        var filesInFileSystem = Directory.EnumerateFiles(_RootPathToFiles, "*", SearchOption.AllDirectories)
            .Where(f => !searchPattern.IsMatch(f))
            .Select((path) => 
                new FileProp
                {
                    FilePath = PathBuilder.Normalize(path),
                    Time = File.GetLastWriteTime(path)
                })
            .ToList();

        await Journal.LogInformationAsync(
            $"Найдено файлов с данными на файловой системе {filesInFileSystem.Count}",
            cancellationToken).ConfigureAwait(false);

        // Проверка наличия новых файлов, которые отсутствуют в выгруженном из базы списке.
        lock (_DatabaseFiles)
        {
            //  Получение списка файлов, которые необходимо добавить в базу данных.
            filesInFileSystem = filesInFileSystem.Except(_DatabaseFiles).ToList();
        }

        await Journal.LogInformationAsync(
            $"Файлов для записи информации в БД {filesInFileSystem.Count}",
            cancellationToken).ConfigureAwait(false);

        //  Определение количества файлов.
        int fileCount = filesInFileSystem.Count;

        //  Проверка количества файлов.
        if (fileCount > 0)
        {
            //  Вывод информации в журнал.
            await Journal.LogInformationAsync(
                $"Найдено {fileCount} новых файлов",
                cancellationToken).ConfigureAwait(false);

            //  Асинхронное добавление информации о новых файлах в базу данных.
            await Parallel.ForEachAsync(filesInFileSystem, new ParallelOptions() { CancellationToken = cancellationToken },
                async (fileInFileSystem, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Безопасное выполнение операции.
                    await SafeCallAsync(async cancellationToken =>
                    {
                        //  Блок перехвата исключений.
                        try
                        {
                            //  Создание контекста сеанса работы с базой данных.
                            using OrioleKmtDatabaseContext database = new();

                            //  Начало транзакции.
                            using var transaction = await database.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

                            await database.RawFrames.AddAsync(
                                    new RawFrame()
                                    {
                                        FilePath = fileInFileSystem.FilePath,
                                        Time = fileInFileSystem.Time,
                                    }, cancellationToken)
                                    .ConfigureAwait(false);


                            //  Сохранение изменений в базу данных.
                            int count = await database.SaveChangesAsync(cancellationToken);

                            //  Фиксирование изменений.
                            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

                            //  Обновляем список хранящийся в памяти.
                            lock (_DatabaseFiles)
                            {
                                _DatabaseFiles.Add(fileInFileSystem);
                            }
                        }
                        catch (DbUpdateException)
                        {
                            //Проверка файла в базе данных.

                            //  Создание контекста сеанса работы с базой данных.
                            using OrioleKmtDatabaseContext database = new();
                            // Запрос получения информации о файлах(полных путей)
                            if (await database.RawFrames
                                    .AnyAsync(x => x.FilePath == fileInFileSystem.FilePath, cancellationToken)
                                    .ConfigureAwait(false))
                            {
                                //  Обновляем список хранящийся в памяти.
                                lock (_DatabaseFiles)
                                {
                                    _DatabaseFiles.Add(fileInFileSystem);
                                }
                            }
                            else
                            {
                                throw;
                            }
                        }

                    }, cancellationToken).ConfigureAwait(false);

                }).ConfigureAwait(false);
        }
    }
}
