using Apeiron.Platform.Expanse.Core;

namespace Apeiron.Platform.Expanse.FileStorages;

/// <summary>
/// Представляет описание файлового хранилища.
/// </summary>
public sealed class FileStorage
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="id">
    /// Идентификатор файлового хранилища.
    /// </param>
    /// <param name="name">
    /// Имя файлового хранилища.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FileStorage([ParameterNoChecks] long id, [ParameterNoChecks] string name)
    {
        //  Установка идентификатора файлового хранилища.
        Id = id;

        //  Установка имени файлового хранилища.
        Name = name;
    }

    /// <summary>
    /// Возвращает идентификатор файлового хранилища.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Возвращает имя файлового хранилища.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Асинхронно возвращает коллекцию всех описаний файловых хранилищ.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая коллекцию всех описаний файловых хранилищ.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task<IEnumerable<FileStorage>> GetAllAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос информации о всех описаниях файловых хранилищах.
        List<FileStorage> fileStorages = await ExpanseConnection.SingleInvokeAsync(
            ExpanseMethodId.GetAllFileStorages,
            writer => { },
            reader =>
            {
                //  Чтение количества описаний хранилищ.
                int count = reader.ReadInt32();

                //  Создание списка описаний файловых хранилищ.
                List<FileStorage> fileStorages = new();

                //  Перебор всех описаний файловых хранилищ.
                for (int i = 0; i < count; i++)
                {
                    //  Чтение идентификатора файлового хранилища.
                    long id = reader.ReadInt64();

                    //  Чтение имени файлового хранилища.
                    string name = reader.ReadString();

                    //  Создание нового описания файлового хранилища.
                    FileStorage fileStorage = new(id, name);

                    //  Добавление описания файлового хранилища в список.
                    fileStorages.Add(fileStorage);
                }

                //  Возврат списка описаний файловых хранилищ.
                return fileStorages;
            },
            cancellationToken)
            .ConfigureAwait(false);

        //  Возврат оболочки списка описаний файловых хранилищ, доступной только на чтение.
        return fileStorages.AsReadOnly();
    }

    /// <summary>
    /// Асинхронно возвращает описание файлового хранилища с заданным идентификатором.
    /// </summary>
    /// <param name="id">
    /// Идентификатор файлового хранилища.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая описание файлового хранилища.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="id"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="id"/> передано нулевое значение.
    /// </exception>
    public static async Task<FileStorage> FromIdAsync(long id, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка идентификатора файлового хранилища.
        id = Check.IsPositive(id, nameof(id));

        //  Запрос имени файлового хранилища.
        string name = await ExpanseConnection.SingleInvokeAsync(
            ExpanseMethodId.GetFileStorageFromId,
            writer => writer.Write(id),
            reader => reader.ReadString(),
            cancellationToken)
            .ConfigureAwait(false);

        //  Возврат описания файлового хранилища.
        return new(id, name);
    }

    /// <summary>
    /// Асинхронно возвращает описание файлового хранилища с заданным именем.
    /// </summary>
    /// <param name="name">
    /// Имя файлового хранилища.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая описание файлового хранилища.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    public static async Task<FileStorage> FromNameAsync(string name, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка имени файлового хранилища.
        name = Check.IsNotNull(name, nameof(name));

        //  Запрос идентификатора файлового хранилища.
        long id = await ExpanseConnection.SingleInvokeAsync(
            ExpanseMethodId.GetFileStorageFromName,
            writer => writer.Write(name),
            reader => reader.ReadInt64(),
            cancellationToken)
            .ConfigureAwait(false);

        //  Возврат описания файлового хранилища.
        return new(id, name);
    }

    /// <summary>
    /// Асинхронно выполняет обновление свойств описания файлового хранилища.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая обновление свойств описания файлового хранилища.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Обновление свойств описания файлового хранилища.
        Name = await ExpanseConnection.SingleInvokeAsync(
            ExpanseMethodId.UpdateFileStorage,
            writer => writer.Write(Id),
            reader =>
            {
                //  Чтение имени.
                return reader.ReadString();
            },
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно изменяет имя в описании файлового хранилища.
    /// </summary>
    /// <param name="name">
    /// Новое имя файлового хранилища.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, изменяющая имя в описании файлового хранилища.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    public async Task RenameAsync(string name, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка нового имени в описании файлового хранилища.
        name = Check.IsNotNull(name, nameof(name));

        //  Изменение имени в описании файлового хранилища.
        await ExpanseConnection.SingleInvokeAsync(
            ExpanseMethodId.RenameFileStorage,
            writer =>
            {
                    //  Запись идентификатора.
                    writer.Write(Id);

                    //  Запись нового имени.
                    writer.Write(name);
            },
            cancellationToken).ConfigureAwait(false);

        //  Установка нового имени.
        Name = name;
    }

    /// <summary>
    /// Асинхронно создаёт описание файлового хранилища.
    /// </summary>
    /// <param name="name">
    /// Имя файлового хранилища.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая описание файлового хранилище.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    public static async Task<FileStorage> CreateAsync(string name, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка имени файлового хранилища.
        name = Check.IsNotNull(name, nameof(name));

        //  Создание описания файлового хранилища.
        return await ExpanseConnection.SingleInvokeAsync(
            ExpanseMethodId.CreateFileStorage,
            writer => writer.Write(name),
            reader =>
            {
                //  Чтение идентификатора.
                long id = reader.ReadInt64();

                //  Возврат описания файлового хранилища.
                return new FileStorage(id, name);
            },
            cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно удаляет описание файлового хранилища.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая удаление описания файлового хранилища.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task RemoveAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Удаление описания файлового хранилища.
        await ExpanseConnection.SingleInvokeAsync(
            ExpanseMethodId.RemoveFileStorage,
            writer =>
            {
                //  Запись идентификатора.
                writer.Write(Id);
            },
            cancellationToken).ConfigureAwait(false);
    }

    //public async Task<IEnumerable<FileStorageConnector>> GetConnectorsAsync(CancellationToken cancellationToken)
    //{

    //}

    //public async Task<FileStorageConnector> AddConnectorAsync(int priority, string path, CancellationToken cancellationToken)
    //{

    //}
}
