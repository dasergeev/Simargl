using Apeiron.Platform.Databases.FileStorageDatabase;
using Apeiron.Platform.Databases.FileStorageDatabase.Entities;

namespace Apeiron.Platform.Expanse.Coordinators;

/// <summary>
/// Представляет координатора файловых хранилищ.
/// </summary>
internal sealed class FileStoragesCoordinator
{
    /// <summary>
    /// Поле для хранения кэша файловых хранилищ.
    /// </summary>
    private readonly SortedDictionary<long, FileStorage> _FileStorageCache;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public FileStoragesCoordinator()
    {
        //  Создание кэша файловых хранилищ.
        _FileStorageCache = new();
    }

    /// <summary>
    /// Асинхронно выполняет метод, возвращающий все файловые хранилища.
    /// </summary>
    /// <param name="methodContext">
    /// Контекст выполнения метода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task GetAllFileStoragesAsync(
        [ParameterNoChecks] MethodContext methodContext,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание подключения к базе данных.
        await using FileStorageDatabaseContext context = new();

        //  Блокировка кэша файловых хранилищ.
        lock (_FileStorageCache)
        {
            //  Запрос количества файловых хранилищ.
            long count = context.FileStorages.LongCount();

            //  Проверка необходимости загрузки данных из базы данных.
            if (_FileStorageCache.Count != count)
            {
                //  Запрос всех хранилищ.
                FileStorage[] fileStorages = context.FileStorages.ToArray();

                //  Перебор всех хранилищ.
                foreach (FileStorage fileStorage in fileStorages)
                {
                    //  Проверка кэша.
                    if (!_FileStorageCache.ContainsKey(fileStorage.Id))
                    {
                        //  Добавление в кэш.
                        _FileStorageCache.Add(fileStorage.Id, fileStorage);
                    }
                }
            }

            //  Запись количества хранилищ.
            methodContext.Writer.Write(_FileStorageCache.Count);

            //  Перебор всех хранилищ.
            foreach (FileStorage fileStorage in _FileStorageCache.Values)
            {
                //  Запись идентификатора хранилища.
                methodContext.Writer.Write(fileStorage.Id);

                //  Запись имени хранилища.
                methodContext.Writer.Write(fileStorage.Name);
            }
        }
    }

    /// <summary>
    /// Асинхронно выполняет метод, возвращающий файловое хранилище по идентификатору.
    /// </summary>
    /// <param name="methodContext">
    /// Контекст выполнения метода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task GetFileStorageFromIdAsync(
        [ParameterNoChecks] MethodContext methodContext,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение идентификатора.
        long id = methodContext.Reader.ReadInt64();

        //  Блокировка кэша файловых хранилищ.
        lock (_FileStorageCache)
        {
            //  Поиск в кэше.
            if (!_FileStorageCache.ContainsKey(id))
            {
                //  Создание подключения к базе данных.
                using FileStorageDatabaseContext context = new();

                //  Поиск в базе данных.
                FileStorage fileStorage = context.FileStorages
                    .Where(fileStorage => fileStorage.Id == id)
                    .First();

                //  Добавление в кэш.
                _FileStorageCache.Add(fileStorage.Id, fileStorage);
            }

            //  Получение имени хранилища.
            string name = _FileStorageCache[id].Name;

            //  Запись имени.
            methodContext.Writer.Write(name);
        }
    }

    /// <summary>
    /// Асинхронно выполняет метод, возвращающий файловое хранилище по имени.
    /// </summary>
    /// <param name="methodContext">
    /// Контекст выполнения метода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task GetFileStorageFromNameAsync(
        [ParameterNoChecks] MethodContext methodContext,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение имени.
        string name = methodContext.Reader.ReadString();

        //  Блокировка кэша файловых хранилищ.
        lock (_FileStorageCache)
        {
            //  Поиск в кэше.
            FileStorage? fileStorage = _FileStorageCache.Values
                .FirstOrDefault(fileStorage => fileStorage.Name == name);

            //  Проверка результатов поиска.
            if (fileStorage is null)
            {
                //  Создание подключения к базе данных.
                using FileStorageDatabaseContext context = new();

                //  Поиск в базе данных.
                fileStorage = context.FileStorages
                    .Where(fileStorage => fileStorage.Name == name)
                    .First();

                //  Добавление в кэш.
                _FileStorageCache.Add(fileStorage.Id, fileStorage);
            }

            //  Запись идентификатора.
            methodContext.Writer.Write(fileStorage.Id);
        }
    }

    /// <summary>
    /// Асинхронно выполняет метод, обновляющий свойства файлового хранилища.
    /// </summary>
    /// <param name="methodContext">
    /// Контекст выполнения метода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task UpdateFileStorageAsync(
        [ParameterNoChecks] MethodContext methodContext,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение идентификатора.
        long id = methodContext.Reader.ReadInt64();

        //  Блокировка кэша файловых хранилищ.
        lock (_FileStorageCache)
        {
            //  Поиск в кэше.
            if (!_FileStorageCache.ContainsKey(id))
            {
                //  Создание подключения к базе данных.
                using FileStorageDatabaseContext context = new();

                //  Поиск в базе данных.
                FileStorage fileStorage = context.FileStorages
                    .Where(fileStorage => fileStorage.Id == id)
                    .First();

                //  Добавление в кэш.
                _FileStorageCache.Add(fileStorage.Id, fileStorage);
            }

            //  Получение имени хранилища.
            string name = _FileStorageCache[id].Name;

            //  Запись имени.
            methodContext.Writer.Write(name);
        }
    }

    /// <summary>
    /// Асинхронно выполняет метод, изменяющий имя файлового хранилища.
    /// </summary>
    /// <param name="methodContext">
    /// Контекст выполнения метода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task RenameFileStorageAsync(
        [ParameterNoChecks] MethodContext methodContext,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение идентификатора.
        long id = methodContext.Reader.ReadInt64();

        //  Чтение имени.
        string name = methodContext.Reader.ReadString();

        //  Блокировка кэша файловых хранилищ.
        lock (_FileStorageCache)
        {
            //  Поиск в кэше.
            if (_FileStorageCache.TryGetValue(id, out FileStorage? actualFileStorage))
            {
                //  Получение записи.
                FileStorage fileStorage = actualFileStorage;

                //  Проверка необходимости изменить имя.
                if (fileStorage.Name != name)
                {
                    //  Создание подключения к базе данных.
                    FileStorageDatabaseContext context = new();

                    //  Присоединение сущности к базе данных.
                    context.Attach(fileStorage);

                    //  Старое имя.
                    string oldName = fileStorage.Name;

                    try
                    {
                        //  Изменение имени.
                        fileStorage.Name = name;

                        //  Сохранение изменений.
                        context.SaveChanges();
                    }
                    catch
                    {
                        //  Установка старого имени.
                        fileStorage.Name = oldName;
                    }
                }
            }
            else
            {
                //  Создание подключения к базе данных.
                FileStorageDatabaseContext context = new();

                //  Поиск в базе данных.
                FileStorage fileStorage = context.FileStorages
                    .Where(fileStorage => fileStorage.Id == id)
                    .First();

                //  Проверка необходимости изменения имени.
                if (fileStorage.Name != name)
                {
                    //  Изменение имени.
                    fileStorage.Name = name;

                    //  Сохранение изменений.
                    context.SaveChanges();
                }

                //  Добавление в кэш.
                _FileStorageCache.Add(fileStorage.Id, fileStorage);
            }
        }
    }

    /// <summary>
    /// Асинхронно выполняет метод, создающий файловое хранилище.
    /// </summary>
    /// <param name="methodContext">
    /// Контекст выполнения метода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task CreateFileStorageAsync(
        [ParameterNoChecks] MethodContext methodContext,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение имени.
        string name = methodContext.Reader.ReadString();

        //  Блокировка кэша файловых хранилищ.
        lock (_FileStorageCache)
        {
            //  Создание подключения к базе данных.
            using FileStorageDatabaseContext context = new();

            //  Добавление нового хранилища.
            context.FileStorages.Add(new() { Name = name });

            //  Сохранение изменений.
            context.SaveChanges();

            //  Запрос записи.
            FileStorage fileStorage = context.FileStorages
                .Where(fileStorage => fileStorage.Name == name)
                .First();

            //  Добавление в кэш.
            _FileStorageCache.Add(fileStorage.Id, fileStorage);

            //  Запись идентификатора.
            methodContext.Writer.Write(fileStorage.Id);
        }
    }

    /// <summary>
    /// Асинхронно выполняет метод, удаляющий файловое хранилище.
    /// </summary>
    /// <param name="methodContext">
    /// Контекст выполнения метода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая метод.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task RemoveFileStorageAsync(
        [ParameterNoChecks] MethodContext methodContext,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение идентификатора.
        long id = methodContext.Reader.ReadInt64();

        //  Блокировка кэша файловых хранилищ.
        lock (_FileStorageCache)
        {
            //  Создание подключения к базе данных.
            using FileStorageDatabaseContext context = new();

            //  Запрос хранилища.
            FileStorage fileStorage = context.FileStorages
                    .Where(fileStorage => fileStorage.Id == id)
                    .First();

            //  Удаление хранилища.
            context.FileStorages.Remove(fileStorage);

            //  Сохранение изменений.
            context.SaveChanges();

            //  Поиск в кэше.
            if (_FileStorageCache.ContainsKey(id))
            {
                //  Удаление из кэша.
                _FileStorageCache.Remove(fileStorage.Id);
            }
        }
    }
}
