using System.Text;

namespace Apeiron.Platform.Databases.CentralDatabase;

public partial class CentralDatabaseAgent
{
    /// <summary>
    /// Предоставляет доступ к элементам файловой системы, содержащимися в центральной базе данных.
    /// </summary>
    public static class FileSystem
    {
        /// <summary>
        /// Асинхронно выполняет регистрацию внутреннего файла в базе данных.
        /// </summary>
        /// <param name="path">
        /// Текущий абсолютный путь к внутреннему файлу.
        /// </param>
        /// <param name="registrationTime">
        /// Время регистрации файла.
        /// </param>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, выполняющая регистрацию внутреннего файла в базе данных.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="path"/> передана пустая ссылка.
        /// </exception>
        public static async Task<InternalFile> FileRegistrationAsync(
            string path, DateTime registrationTime, CancellationToken cancellationToken)
        {
            //  Проверка ссылки на путь.
            path = Check.IsNotNull(path, nameof(path));

            //  Нормализация пути.
            path = PathBuilder.Normalize(path);

            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Получение информации о файле.
            FileInfo fileInfo = new(path);

            //  Проверка родительского каталога.
            if (fileInfo.Directory is not DirectoryInfo directoryInfo)
            {
                //  Генерация исключения.
                throw new InvalidOperationException();
            }

            //  Получение имения файла.
            string name = fileInfo.Name[..^fileInfo.Extension.Length];

            //  Получение информации о каталоге.
            InternalDirectory directory = await GetDirectoryAsync(
                directoryInfo.FullName, cancellationToken).ConfigureAwait(false);

            //  Получение относительного пути.
            string relativePath = PathBuilder.Combine(directory.Path, fileInfo.Name);

            //  Добавление информации о файле.
            InternalFile internalFile = await RetrievalAsync(
                internalFile =>
                    internalFile.FileStorageId == directory.FileStorageId &&
                    internalFile.GeneralDirectoryId == directory.GeneralDirectoryId &&
                    internalFile.Path == relativePath,
                () => new InternalFile(
                    name: name,
                    path: relativePath,
                    extension: fileInfo.Extension,
                    fileStorageId: directory.FileStorageId,
                    generalDirectoryId: directory.GeneralDirectoryId,
                    parentDirectoryId: directory.Id)
                {
                    RegistrationTime = registrationTime,
                },
                cancellationToken).ConfigureAwait(false);

            //  Возврат информации о файле.
            return internalFile;
        }

        /// <summary>
        /// Асинхронно возвращает внутренний каталог. При необходимости добавляет запись в базу данных.
        /// </summary>
        /// <param name="path">
        /// Текущий абсолютный путь к внутреннему каталогу.
        /// </param>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, выполняющая поиск внутреннего каталога.
        /// </returns>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="path"/> передана пустая ссылка.
        /// </exception>
        public static async Task<InternalDirectory> GetDirectoryAsync(
            string path, CancellationToken cancellationToken)
        {
            //  Проверка ссылки на путь.
            path = Check.IsNotNull(path, nameof(path));

            //  Нормализация пути.
            path = PathBuilder.Normalize(path);

            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Запрос файловых хранилищ.
            FileStorage[] fileStorages = await RequestAsync(
                async (session, cancellationToken) => await session.FileStorages
                    .ToArrayAsync(cancellationToken),
                cancellationToken).ConfigureAwait(false);

            //  Перебор файловых хранилищ.
            foreach (FileStorage fileStorage in fileStorages)
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Перебор общих каталогов.
                foreach (GeneralDirectory generalDirectory in fileStorage.GeneralDirectories)
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Получение коллекции абсолютных путей к общему каталогу.
                    IEnumerable<string> generalDirectoryPaths = await generalDirectory
                        .GetAbsolutePathsAsync(cancellationToken).ConfigureAwait(false);

                    //  Перебор абсолютных путей к общему каталогу.
                    foreach (string generalDirectoryPath in generalDirectoryPaths)
                    {
                        //  Проверка токена отмены.
                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Проверка принадлежности пути к файловому хранилищу.
                        if (path.Length >= generalDirectoryPath.Length &&
                            path[..generalDirectoryPath.Length] == generalDirectoryPath)
                        {
                            //  Получение относительного пути.
                            string relativePath = PathBuilder.RelativeNormalize(path[generalDirectoryPath.Length..]);

                            //  Получение имён каталогов.
                            string[] names = relativePath.Split(Path.DirectorySeparatorChar);

                            //  Проверка количества имён.
                            if (names.Length != 0)
                            {
                                //  Текущий каталог.
                                InternalDirectory? currentDirectory = null;

                                //  Текущий относительный путь.
                                relativePath = string.Empty;

                                //  Перебор имён.
                                foreach (string name in names)
                                {
                                    //  Проверка токена отмены.
                                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                    //  Корректировка текущего относительного пути.
                                    relativePath = string.IsNullOrEmpty(relativePath) ?
                                        name :
                                        PathBuilder.Combine(relativePath, name);

                                    //  Запрос каталога.
                                    InternalDirectory internalDirectory = await RetrievalAsync(
                                        internalDirectory => internalDirectory.FileStorageId == fileStorage.Id &&
                                            internalDirectory.GeneralDirectoryId == generalDirectory.Id &&
                                            internalDirectory.Path == relativePath,
                                        () => new InternalDirectory(name, relativePath, fileStorage.Id, generalDirectory.Id)
                                        {
                                            ParentDirectoryId = currentDirectory?.Id,
                                        },
                                        cancellationToken).ConfigureAwait(false);

                                    //  Установка текущего каталога.
                                    currentDirectory = internalDirectory;
                                }

                                //  Проверка ссылки на полученный каталог.
                                if (currentDirectory is not null)
                                {
                                    //  Возврат полученного каталога.
                                    return currentDirectory;
                                }
                            }
                        }
                    }
                }
            }

            //  Генерация исключения.
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Асинхронно выполняет обновление метрики файла.
        /// </summary>
        /// <param name="internalFile">
        /// Файл, для которого необходимо обновить метрику.
        /// </param>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, выполняющая обновление метрики файла.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="internalFile"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        public static async Task UpdateFileMetricAsync(InternalFile internalFile, CancellationToken cancellationToken)
        {
            //  Проверка ссылки на файл.
            internalFile = Check.IsNotNull(internalFile, nameof(internalFile));

            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Метрика файла.
            InternalFileMetric? metric = null;

            //  Перебор подключений к файловому хранилищу, содержащему файл.
            foreach (FileStorageConnector connector in internalFile.FileStorage.Connectors
                .OrderBy(storage => storage.Priority))
            {
                //  Проверка доступа к файловому хранилищу.
                if (System.IO.Directory.Exists(connector.Path))
                {
                    //  Получение пути к файлу.
                    string path = PathBuilder.Combine(
                        connector.Path, internalFile.GetFullRelativePath());

                    //  Проверка существования файла.
                    if (File.Exists(path))
                    {
                        //  Загрузка данных файла.
                        byte[] bytes = await File.ReadAllBytesAsync(path, cancellationToken).ConfigureAwait(false);

                        //  Хэш-код.
                        long hashCode = 0;

                        //  Сумма байтов.
                        long bytesSum = 0;

                        //  Перебор всех байтов.
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            //  Корректировка хэш-кода.
                            hashCode = unchecked(hashCode + bytes[i]);
                            hashCode = unchecked(hashCode + (hashCode << 10));
                            hashCode = unchecked(hashCode ^ (hashCode >> 6));

                            //  Корректировка суммы байтов.
                            bytesSum = unchecked(bytesSum + bytes[i]);
                        }

                        //  Создание метрики.
                        metric = new(internalFile.Id)
                        {
                            IsExists = true,
                            DeterminationTime = DateTime.Now,
                            RegistrationTime = internalFile.RegistrationTime,
                            CreationTime = File.GetCreationTime(path),
                            LastAccessTime = File.GetLastAccessTime(path),
                            LastWriteTime = File.GetLastWriteTime(path),
                            Size = bytes.Length,
                            Crc32 = Cryptography.Crc32.Compute(bytes),
                            HashCode = hashCode,
                            BytesSum = bytesSum,
                        };
                    }
                    else
                    {
                        //  Создание метрики, с отсутствующим файлом.
                        metric = new(internalFile.Id)
                        {
                            IsExists = false,
                            DeterminationTime = DateTime.Now,
                            RegistrationTime = internalFile.RegistrationTime,
                            CreationTime = default,
                            LastAccessTime = default,
                            LastWriteTime = default,
                            Size = 0,
                            Crc32 = 0,
                            HashCode = 0,
                            BytesSum = 0,
                        };
                    }

                    //  Завершение цикла по соединениям с файловым хранилищем.
                    break;
                }
            }

            //  Проверка ссылки на метрику.
            if (metric is null)
            {
                //  Генерация исключения.
                throw new InvalidOperationException();
            }

            //  Запрос предыдущей метрики.
            InternalFileMetric? oldMetric = await RequestAsync(
                async (session, cancellationToken) => await session.InternalFileMetrics
                    .Where(internalFileMetric => internalFileMetric.FileId == internalFile.Id)
                    .OrderBy(internalFileMetric => internalFileMetric.DeterminationTime)
                    .LastOrDefaultAsync(cancellationToken).ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);

            //  Проверка необходимости обноления метрики.
            if (oldMetric is null ||
                oldMetric.IsExists != metric.IsExists ||
                oldMetric.RegistrationTime != metric.RegistrationTime ||
                oldMetric.Size != metric.Size ||
                oldMetric.Crc32 != metric.Crc32 ||
                oldMetric.HashCode != metric.HashCode ||
                oldMetric.BytesSum != metric.BytesSum)
            {
                //  Добавление новой метрики.
                await TransactionAsync(
                    async (session, cancellationToken) => await session.InternalFileMetrics
                        .AddAsync(metric, cancellationToken).ConfigureAwait(false),
                    cancellationToken).ConfigureAwait(false);
            }

            //  Выполнение транзакции для удаления лишних метрик.
            await TransactionAsync(
                async (session, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Запрос всех метрик файла.
                    InternalFileMetric[] metrics = await session.InternalFileMetrics
                        .Where(metric => metric.FileId == internalFile.Id)
                        .OrderBy(metric => metric.DeterminationTime)
                        .ToArrayAsync(cancellationToken)
                        .ConfigureAwait(false);

                    //  Проверка количества метрик.
                    if (metrics.Length > 1)
                    {
                        //  Перебор всех старых метрик.
                        for (int i = 0; i < metrics.Length - 1; i++)
                        {
                            //  Удаление метрики.
                            session.InternalFileMetrics.Remove(metrics[i]);
                        }
                    }
                },
                cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Асинхронно определяет формат файла.
        /// </summary>
        /// <param name="internalFile">
        /// Файл, формат которого необходимо определить.
        /// </param>
        /// <param name="cancellationToken">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача, выполняющая определение формата файла.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="internalFile"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="OperationCanceledException">
        /// Операция отменена.
        /// </exception>
        public static async Task<InternalFileFormat> DefineFileFormatAsync(
            InternalFile internalFile, CancellationToken cancellationToken)
        {
            //  Проверка ссылки на файл.
            internalFile = Check.IsNotNull(internalFile, nameof(internalFile));

            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Перебор путей к файлу.
            foreach (string path in internalFile.GetAbsolutePaths())
            {
                //  Проверка существования файла.
                if (File.Exists(path))
                {
                    //  Открытие потока для чтения файла.
                    using FileStream stream = new(path, FileMode.Open, FileAccess.Read, FileShare.Read);

                    //  Создание средства чтения файла.
                    using BinaryReader reader = new(stream, Encoding.UTF8, true);

                    //  Определение размера файла.
                    long fileSize = stream.Length;

                    //  Проверка кадра в формате TestLab.
                    if (fileSize >= 350 && reader.ReadUInt64() == 0x42414C54534554UL)
                    {
                        //  Файл, содержит кадр регистрации.
                        return InternalFileFormat.Frame;
                    }

                    //  Сброс положения в потоке.
                    stream.Position = 0;

                    //  Проверка кадра в формате Catman.
                    if (fileSize >= 2 && reader.ReadInt16() == 0x1394)
                    {
                        //  Файл, содержит кадр регистрации.
                        return InternalFileFormat.Frame;
                    }

                    //  Неизвестный формат файла.
                    return InternalFileFormat.Unknown;
                }
            }

            //  Генерация исключения.
            throw new InvalidOperationException($"Не найден файл: {internalFile.Path}");
        }
    }
}
