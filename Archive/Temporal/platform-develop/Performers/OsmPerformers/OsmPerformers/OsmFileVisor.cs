namespace Apeiron.Platform.Performers.OsmPerformers;

/// <summary>
/// Представляет исполнителя, выполняющего поиск файлов OSM-данных.
/// </summary>
public sealed class OsmFileVisor :
    Performer
{
    /// <summary>
    /// Представляет путь к корневому каталогу размещения файлов.
    /// </summary>
    private const string _RootPathToFiles = @"\\10.47.49.45\osm\Parts";

    /// <summary>
    /// Поле для хранения списка файлов в базе данных.
    /// </summary>
    private readonly List<string> _DatabaseFiles;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="journal">
    /// Журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="journal"/> передана пустая ссылка.
    /// </exception>
    public OsmFileVisor(Journal journal) :
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

        //  Загрузка всех фалов в файловой системе.
        List<string> filesInFileSystem = Directory.GetFiles(_RootPathToFiles, "*", SearchOption.AllDirectories)
            .Select(path => PathBuilder.Normalize(path))
            .ToList();

        await Journal.LogInformationAsync(
            $"Найдено файлов с данным карт на файловой системе {filesInFileSystem.Count}",
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
        if (filesInFileSystem.Count > 0)
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
                            using OsmDatabaseContext database = new();

                            //  Начало транзакции.
                            using var transaction = await database.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

                            //  Добавление в базу записи.
                            await database.OsmFiles.AddAsync(
                                new OsmFile()
                                {
                                    FilePath = fileInFileSystem,
                                    IsAnalyzed = false,
                                    IsNodesEmpty = false,
                                    IsWaysEmpty = false,
                                    IsNodesLoad = false,
                                    IsNodeTagsLoad = false,
                                    IsWaysLoad = false,
                                    IsWayTagsLoad = false
                                },
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
                            using OsmDatabaseContext database = new();
                            // Запрос получения информации о файлах(полных путей)
                            if (await database.OsmFiles
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
