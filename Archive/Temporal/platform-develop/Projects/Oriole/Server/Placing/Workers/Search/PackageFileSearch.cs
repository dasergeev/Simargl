using Apeiron.IO;
using Apeiron.Oriole.Server.Workers.Common;
using Apeiron.Platform.Databases.CentralDatabase;
using Apeiron.Platform.Databases.CentralDatabase.Entities;
using Apeiron.Platform.Databases.OrioleDatabase;
using Apeiron.Platform.Databases.OrioleDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apeiron.Oriole.Server.Workers.Search;

/// <summary>
/// Представляет фоновый процесс, выполняющий поиск файлов с исходными данными.
/// </summary>
public class PackageFileSearch :
    Worker<PackageFileSearch>
{
    /// <summary>
    /// Поле для хранения списка файлов в базе данных.
    /// </summary>
    private readonly List<string> _DatabaseFiles;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал службы.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public PackageFileSearch(ILogger<PackageFileSearch> logger) :
        base(logger)
    {
        //  Создание списка для хранений файлов, загруженных в базу данных.
        _DatabaseFiles = new();
    }

    /// <summary>
    /// Асинхронно выполняет фоновую работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая фоновую работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Проверка количества файлов в списке.
            if (_DatabaseFiles.Count == 0)
            {
                //  Загрузка данных из базы данных.
                await LoadDatabaseFilesAsync(cancellationToken);
            }

            //  Поиск в базе данных.
            await SearchInDatabaseAsync(cancellationToken);

            //  Ожидание перед следующим поиском.
            await Task.Delay(300000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно загружает данные из базы данных.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task LoadDatabaseFilesAsync(CancellationToken cancellationToken)
    {
        //  Безопасный вызов.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных.
            await using OrioleDatabaseContext database = new();

            //  Запрос каталогов необработанных данных.
            RawDirectory[] rawDirectories = await database.RawDirectories
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Перебор каталогов необработанных данных.
            foreach (RawDirectory rawDirectory in rawDirectories)
            {
                //  Получение массива идентификаторов.
                var packageFileIds = await OrioleDatabaseManager.RequestAsync(
                    async (database, cancellationToken) => await database.PackageFiles
                    .Where(packageFile => packageFile.RawDirectoryId == rawDirectory.Id)
                    .Select(packageFile => new
                    {
                        packageFile.Format,
                        packageFile.Time,
                    })
                    .ToArrayAsync(cancellationToken),
                    cancellationToken).ConfigureAwait(false);

                //  Формирование коллекции путей.
                IEnumerable<string> paths = packageFileIds
                    .Select(packageFileId => PathBuilder.Combine(
                        rawDirectory.Path,
                        $"{packageFileId.Time:yyyy-MM-dd-HH}",
                        $"Adxl{packageFileId.Format:000}",
                        $"{packageFileId.Time:yyyy-MM-dd-HH-mm-ss-fff}.adxl"));

                //  Блокировка списка.
                lock (_DatabaseFiles)
                {
                    //  Добавление путей в список.
                    _DatabaseFiles.AddRange(paths);

                    //  Вывод в журнал.
                    Logger.LogInformation("Загружено файлов из БД: {count}", _DatabaseFiles.Count);
                }
            }
        },
        cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет поиск информации в базе данных.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task SearchInDatabaseAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос идентификаторов каталогов необработанных данных.
        int[] rawDirectoryIds = await OrioleDatabaseManager.RequestAsync(
            async (database, cancellationToken) => await database.RawDirectories
                .Select(rawDirectory => rawDirectory.Id)
                .ToArrayAsync(cancellationToken),
            cancellationToken).ConfigureAwait(false);

        //  Асинхронная работа с каталогами необработанных данных.
        await Parallel.ForEachAsync(
            rawDirectoryIds,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken,
            },
            SearchInRawDirectoryAsync).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет поиск информации по каталогу необработанных данных.
    /// </summary>
    /// <param name="rawDirectoryId">
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
    private async ValueTask SearchInRawDirectoryAsync(int rawDirectoryId, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение пути к каталогу.
        RawDirectory? rawDirectory = await OrioleDatabaseManager.RequestAsync(
            async (database, cancellationToken) => await database.RawDirectories
                .Where(rawDirectory => rawDirectory.Id == rawDirectoryId)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Проверка каталога.
        if (rawDirectory is null)
        {
            //  Каталог не найден.
            return;
        }

        //  Получение контекста центральной базы данных.
        await using CentralDatabaseContext centralContext = new();

        //  Запрос каталога.
        InternalDirectory? internalDirectory = await centralContext.Set<InternalDirectory>()
            .Where(internalDirectory => internalDirectory.Id == rawDirectory.InternalDirectoryId)
            .Include(internalDirectory => internalDirectory.GeneralDirectory)
            .Include(internalDirectory => internalDirectory.InternalDirectories)
            //.ThenInclude(internalDirectory => internalDirectory.InternalDirectories)
            //.ThenInclude(internalDirectory => internalDirectory.InternalFiles)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
        
        //  Проверка каталога.
        if (internalDirectory is null)
        {
            //  Каталог не найден.
            return;
        }

        //  Корневой путь.
        string? rootPath = null;

        //  Поиск корневого каталога.
        {
            //  Инициализация каталога.
            DirectoryInfo? rootDirectory = new(rawDirectory.Path);

            //  Цикл поиска.
            while (rootDirectory is not null && rootDirectory.Name != internalDirectory.GeneralDirectory.Path)
            {
                //  Переход на каталог выше.
                rootDirectory = rootDirectory.Parent;
            }

            //  Проверка найденного каталога.
            if (rootDirectory is not null)
            {
                //  Установка корневого пути.
                rootPath = rootDirectory.FullName;
            }
        }

        //  Проверка пути к корневому каталогу.
        if (rootPath is null)
        {
            //  Каталог не найден.
            return;
        }

        //  Перебор каталогов по времени.
        await Parallel.ForEachAsync(
            internalDirectory.InternalDirectories,
            new ParallelOptions
            {
                CancellationToken = cancellationToken,
            },
            async (timeDirectory, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Вывод информации.
                Logger.LogInformation("Поиск в каталоге: {path}", timeDirectory.Path);

                //  Перебор каталогов по формату.
                foreach (InternalDirectory formatDirectory in timeDirectory.InternalDirectories)
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Перебор файлов.
                    foreach (InternalFile internalFile in formatDirectory.InternalFiles)
                    {
                        //  Проверка токена отмены.
                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Получение пути к файлу.
                        string path = PathBuilder.Combine(rootPath, internalFile.Path);

                        //  Проверка токена отмены.
                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Получение информации о файле.
                        FileInfo fileInfo = new(path);

                        //  Проверка расширения файла.
                        if (fileInfo.Extension == ".adxl")
                        {
                            //  Блокировка списка файлов.
                            lock (_DatabaseFiles)
                            {
                                //  Проверка файла в базе данных.
                                if (_DatabaseFiles.Contains(path))
                                {
                                    //  Завершение работы с файлом.
                                    return;
                                }
                            }

                            //  Безопасная работа с файлом.
                            await SafeCallAsync(async cancellationToken =>
                            {
                                //  Проверка токена отмены.
                                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                //  Метод, извлекающий формат и дату файла.
                                static bool tryParce(FileInfo fileInfo, out int format, out DateTime time)
                                {
                                    //  Формат файла.
                                    format = default;

                                    //  Дата файла.
                                    time = default;

                                    //  Извлечение данных.
                                    return fileInfo.Directory is not null &&
                                        RawDirectory.TryParseDirectoryFormat(fileInfo.Directory.Name, out format) &&
                                        RawDirectory.TryParseFileTime(fileInfo.Name, out time);
                                }

                                //  Определение формата файла.
                                if (tryParce(fileInfo, out int format, out DateTime time))
                                {
                                    //  Получение файла, который необходимо добавить в базу данных.
                                    PackageFile packageFile = new()
                                    {
                                        RawDirectoryId = rawDirectoryId,
                                        Format = format,
                                        Time = time,
                                    };

                                    //  Проверка токена отмены.
                                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                    //  Безопасное выполнение операции.
                                    await SafeCallAsync(async cancellationToken =>
                                    {
                                        //  Блок перехвата исключений.
                                        try
                                        {
                                            //  Выполнение транзакции.
                                            await OrioleDatabaseManager.TransactionAsync(async (database, cancellationToken) =>
                                            {
                                                //  Добавление новых файлов.
                                                await database.PackageFiles.AddAsync(packageFile, cancellationToken);
                                            }, cancellationToken).ConfigureAwait(false);

                                            //  Вывод информации.
                                            Logger.LogInformation("Найден новый файл: {path}", path);
                                        }
                                        catch (DbUpdateException)
                                        {
                                            //  Проверка файла в базе данных.
                                            if (await OrioleDatabaseManager.RequestAsync(
                                                    async (database, cancellationToken) => await database.PackageFiles
                                                        .AllAsync(file => file.RawDirectoryId != packageFile.RawDirectoryId ||
                                                            file.Format != packageFile.Format ||
                                                            file.Time != packageFile.Time,
                                                            cancellationToken)
                                                        .ConfigureAwait(false),
                                                    cancellationToken).ConfigureAwait(false))
                                            {
                                                //  Повторный выброс исключения.
                                                throw;
                                            }
                                        }

                                        //  Блокировка списка файлов.
                                        lock (_DatabaseFiles)
                                        {
                                            //  Проверка списка.
                                            if (!_DatabaseFiles.Contains(path))
                                            {
                                                //  Добвление пути в список.
                                                _DatabaseFiles.Add(path);
                                            }
                                        }
                                    },
                                    cancellationToken).ConfigureAwait(false);
                                }
                            },
                            cancellationToken).ConfigureAwait(false);
                        }
                    }
                }
            }).ConfigureAwait(false);
    }

    ///// <summary>
    ///// Асинхронно выполняет поиск информации по каталогу необработанных данных.
    ///// </summary>
    ///// <param name="rawDirectoryId">
    ///// Идентификатор каталога необработанных данных.
    ///// </param>
    ///// <param name="cancellationToken">
    ///// Токен отмены.
    ///// </param>
    ///// <returns>
    ///// Задача, выполняющая поиск.
    ///// </returns>
    ///// <exception cref="OperationCanceledException">
    ///// Операция отменена.
    ///// </exception>
    //private async ValueTask SearchInRawDirectoryAsync(int rawDirectoryId, CancellationToken cancellationToken)
    //{
    //    //  Проверка токена отмены.
    //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //    //  Получение пути к каталогу.
    //    string? rootPath = await OrioleDatabaseManager.RequestAsync(
    //        async (database, cancellationToken) => await database.RawDirectories
    //            .Where(rawDirectory => rawDirectory.Id == rawDirectoryId)
    //            .Select(rawDirectory => rawDirectory.Path)
    //            .FirstOrDefaultAsync(cancellationToken)
    //            .ConfigureAwait(false),
    //        cancellationToken).ConfigureAwait(false);

    //    //  Проверка пути к каталогу.
    //    if (rootPath is null)
    //    {
    //        //  Каталог не найден.
    //        return;
    //    }

    //    //  Загрузка всех фалов в файловой системе.
    //    await WorkInRawDirectoryAsync(rawDirectoryId, new(rootPath), cancellationToken);
    //}

    ///// <summary>
    ///// Асинхронно выполняет работу с каталогом.
    ///// </summary>
    ///// <param name="rawDirectoryId">
    ///// Идентификатор каталога необработанных данных.
    ///// </param>
    ///// <param name="directory">
    ///// Каталог.
    ///// </param>
    ///// <param name="cancellationToken">
    ///// Токен отмены.
    ///// </param>
    ///// <returns>
    ///// Задача, выполняющая работу с каталогом.
    ///// </returns>
    ///// <exception cref="OperationCanceledException">
    ///// Операция отменена.
    ///// </exception>
    //private async ValueTask WorkInRawDirectoryAsync(int rawDirectoryId, DirectoryInfo directory, CancellationToken cancellationToken)
    //{
    //    //  Проверка токена отмены.
    //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //    //  Выполнение работы с подкаталогами.
    //    await Parallel.ForEachAsync(
    //        directory.GetDirectories(),
    //        new ParallelOptions()
    //        {
    //            CancellationToken = cancellationToken,
    //        },
    //        async (directory, cancellationToken) =>
    //            await WorkInDirectoryAsync(rawDirectoryId, directory, cancellationToken))
    //    .ConfigureAwait(false);
    //}

    ///// <summary>
    ///// Асинхронно выполняет работу с каталогом.
    ///// </summary>
    ///// <param name="rawDirectoryId">
    ///// Идентификатор каталога необработанных данных.
    ///// </param>
    ///// <param name="directory">
    ///// Каталог.
    ///// </param>
    ///// <param name="cancellationToken">
    ///// Токен отмены.
    ///// </param>
    ///// <returns>
    ///// Задача, выполняющая работу с каталогом.
    ///// </returns>
    ///// <exception cref="OperationCanceledException">
    ///// Операция отменена.
    ///// </exception>
    //private async ValueTask WorkInDirectoryAsync(int rawDirectoryId, DirectoryInfo directory, CancellationToken cancellationToken)
    //{
    //    //  Проверка токена отмены.
    //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //    //  Вывод информации.
    //    Logger.LogInformation("Поиск в каталоге: {path}", directory.FullName);

    //    //  Безопасный вызов.
    //    await SafeCallAsync(async cancellationToken =>
    //    {
    //        //  Проверка токена отмены.
    //        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //        ////  Выполнение работы с подкаталогами.
    //        //await Parallel.ForEachAsync(
    //        //    directory.GetDirectories(),
    //        //    new ParallelOptions()
    //        //    {
    //        //        CancellationToken = cancellationToken,
    //        //    },
    //        //    async (directory, cancellationToken) =>
    //        //        await WorkInDirectoryAsync(rawDirectoryId, directory, cancellationToken))
    //        //.ConfigureAwait(false);

    //        //  Выполнение работы с файлами.
    //        await Parallel.ForEachAsync(
    //            directory.GetFiles("*.adxl", SearchOption.AllDirectories),
    //            new ParallelOptions()
    //            {
    //                CancellationToken = cancellationToken,
    //            },
    //            async (file, cancellationToken) =>
    //            {
    //                await SafeCallAsync(async cancellationToken =>
    //                {
    //                    //  Проверка токена отмены.
    //                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //                    //  Проверка расширения файла.
    //                    if (file.Extension == ".adxl")
    //                    {
    //                        //  Получение пути к файлу.
    //                        string path = PathBuilder.Normalize(file.FullName);

    //                        ////  Вывод информации.
    //                        //Logger.LogInformation("Анализ файла: {path}", path);

    //                        //  Блокировка списка файлов.
    //                        lock (_DatabaseFiles)
    //                        {
    //                            //  Проверка файла в базе данных.
    //                            if (_DatabaseFiles.Contains(path))
    //                            {
    //                                //  Завершение работы с файлом.
    //                                return;
    //                            }
    //                        }

    //                        //  Безопасная работа с файлом.
    //                        await SafeCallAsync(async cancellationToken =>
    //                        {
    //                            //  Проверка токена отмены.
    //                            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //                            //  Метод, извлекающий формат и дату файла.
    //                            static bool tryParce(FileInfo fileInfo, out int format, out DateTime time)
    //                            {
    //                                //  Формат файла.
    //                                format = default;

    //                                //  Дата файла.
    //                                time = default;

    //                                //  Извлечение данных.
    //                                return fileInfo.Directory is not null &&
    //                                    RawDirectory.TryParseDirectoryFormat(fileInfo.Directory.Name, out format) &&
    //                                    RawDirectory.TryParseFileTime(fileInfo.Name, out time);
    //                            }

    //                            //  Определение формата файла.
    //                            if (tryParce(file, out int format, out DateTime time))
    //                            {
    //                                //  Получение файла, который необходимо добавить в базу данных.
    //                                PackageFile packageFile = new()
    //                                {
    //                                    RawDirectoryId = rawDirectoryId,
    //                                    Format = format,
    //                                    Time = time,
    //                                };

    //                                //  Проверка токена отмены.
    //                                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //                                //  Безопасное выполнение операции.
    //                                await SafeCallAsync(async cancellationToken =>
    //                                {
    //                                    //  Блок перехвата исключений.
    //                                    try
    //                                    {
    //                                        //  Выполнение транзакции.
    //                                        await OrioleDatabaseManager.TransactionAsync(async (database, cancellationToken) =>
    //                                        {
    //                                            //  Добавление новых файлов.
    //                                            await database.PackageFiles.AddAsync(packageFile, cancellationToken);
    //                                        }, cancellationToken).ConfigureAwait(false);

    //                                        //  Вывод информации.
    //                                        Logger.LogInformation("Найден новый файл: {path}", path);
    //                                    }
    //                                    catch (DbUpdateException)
    //                                    {
    //                                        //  Проверка файла в базе данных.
    //                                        if (await OrioleDatabaseManager.RequestAsync(
    //                                                async (database, cancellationToken) => await database.PackageFiles
    //                                                    .AllAsync(file => file.RawDirectoryId != packageFile.RawDirectoryId ||
    //                                                        file.Format != packageFile.Format ||
    //                                                        file.Time != packageFile.Time,
    //                                                        cancellationToken)
    //                                                    .ConfigureAwait(false),
    //                                                cancellationToken).ConfigureAwait(false))
    //                                        {
    //                                            //  Повторный выброс исключения.
    //                                            throw;
    //                                        }
    //                                    }

    //                                    //  Блокировка списка файлов.
    //                                    lock (_DatabaseFiles)
    //                                    {
    //                                        //  Проверка списка.
    //                                        if (!_DatabaseFiles.Contains(path))
    //                                        {
    //                                            //  Добвление пути в список.
    //                                            _DatabaseFiles.Add(path);
    //                                        }
    //                                    }
    //                                },
    //                                cancellationToken).ConfigureAwait(false);
    //                            }
    //                        },
    //                        cancellationToken).ConfigureAwait(false);
    //                    }
    //                },
    //                cancellationToken).ConfigureAwait(false);
    //            }).ConfigureAwait(false);
    //    },
    //    cancellationToken).ConfigureAwait(false);
    //}
}
