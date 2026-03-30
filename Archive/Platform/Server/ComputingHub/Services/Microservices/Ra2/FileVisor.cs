using Apeiron.IO;
using Apeiron.Platform.Databases.Ra2Database;
using ApeironApeiron.Platform.Databases.Ra2Database.Entities;

namespace Apeiron.Platform.Server.Services.Microservices;

/// <summary>
/// Представляет микрослужбу для поиска файлов данных. (Изначально для проекта Ra2)
/// </summary>
public sealed class FileVisor : ServerMicroservice<FileVisor>
{
    /// <summary>
    /// Представляет путь к корневому каталогу размещения файлов.
    /// </summary>
    private const string _RootPathToFiles = @"\\railtest.ru\Data\06-НТО\RawData\Railbus\2021\0001\Frames";

    /// <summary>
    /// Поле для хранения списка файлов в базе данных.
    /// </summary>
    private readonly List<string> _DatabaseFiles;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public FileVisor(ILogger<FileVisor> logger) : base(logger)
    {
        //  Создание списка для хранений файлов, загруженных в базу данных.
        _DatabaseFiles = new();
    }


    /// <summary>
    /// Асинхронно выполняет шаг работы микрослужбы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая шаг работы микрослужбы.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async ValueTask MakeStepAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        ////  Запуск основной задачи
        //await SearchInDatabaseAsync(cancellationToken);

        //  Запуск загрузки файлов в БД.
        await LoadFilePathsAsync(_RootPathToFiles, cancellationToken);

        // Доп. задержка.
        await Task.Delay(10000, cancellationToken).ConfigureAwait(false);
    }

    
    /// <summary>
    /// Асинхронно выполняет поиск информации по каталогу необработанных данных.
    /// </summary>
    /// <param name="filePath">
    /// Идентификатор каталога необработанных данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask LoadFilePathsAsync(string filePath, CancellationToken cancellationToken)
    {
        Check.IsNotNull(filePath, nameof(filePath));

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Загрузка всех фалов в файловой системе.
        List<string> filesInFileSystem = System.IO.Directory.GetFiles(_RootPathToFiles, "*", SearchOption.AllDirectories)
            .Select(path => PathBuilder.Normalize(path))
            .ToList();

        Logger.LogInformation("Найдено файлов на файловой системе {count}", filesInFileSystem.Count);

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Проверка наличия новых файлов, которые отсутствуют в выгруженном из базы списке.
        lock (_DatabaseFiles)
        {
            //  Получение списка файлов, которые необходимо добавить в базу данных.
            filesInFileSystem = filesInFileSystem.Except(_DatabaseFiles).ToList();
        }

        Logger.LogInformation("Файлов для записи информации в БД {count}", filesInFileSystem.Count);

        //  Определение количества файлов.
        int fileCount = filesInFileSystem.Count;

        //  Проверка количества файлов.
        if (filesInFileSystem.Count > 0)
        {
            //  Вывод информации в журнал.
            Logger.LogInformation("Найдено {count} новых файлов", fileCount);

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
                            using Ra2DatabaseContext database = new();
                            //  Начало транзакции.
                            using var transaction = await database.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

                            //  Добавление в базу записи.
                            await database.RawFiles.AddAsync( 
                                new RawFile() { FilePath = fileInFileSystem, IsAnalyzed = false }, 
                                cancellationToken).ConfigureAwait(false);

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
                            //  Проверка файла в базе данных.

                            //  Создание контекста сеанса работы с базой данных.
                            using Ra2DatabaseContext database = new();
                            // Запрос получения информации о файлах(полных путей)
                            if (await database.RawFiles
                                    .AnyAsync(x => x.FilePath == fileInFileSystem, cancellationToken)
                                    .ConfigureAwait(false))
                            {
                                //  Обновляем список хранящийся в памяти.
                                lock (_DatabaseFiles)
                                {
                                    _DatabaseFiles.Add(fileInFileSystem);
                                   // Logger.LogInformation("Размер списка после добавления существующего файла - {count}", _DatabaseFiles.Count);
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
