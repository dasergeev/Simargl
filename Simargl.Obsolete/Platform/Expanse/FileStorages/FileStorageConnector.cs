using Simargl.Designing;
using System.Runtime.CompilerServices;

namespace Simargl.Platform.Expanse.FileStorages;

/// <summary>
/// Представляет информацию о соединении с файловым хранилищем.
/// </summary>
public sealed class FileStorageConnector
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="id">
    /// Идентификатор соединения с файловым хранилищем.
    /// </param>
    /// <param name="priority">
    /// Приоритет соединения.
    /// </param>
    /// <param name="path">
    /// Путь к файловому хранилищу.
    /// </param>
    /// <param name="fileStorage">
    /// Описание файлового хранилища.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal FileStorageConnector(
        [NoVerify] long id,
        [NoVerify] int priority,
        [NoVerify] string path,
        [NoVerify] FileStorage fileStorage)
    {
        //  Установка идентификатора соединения с файловым хранилищем.
        Id = id;

        //  Установка приоритета соединения.
        Priority = priority;

        //  Установка пути к файловому хранилищу.
        Path = path;

        //  Установка описания файлового хранилища.
        FileStorage = fileStorage;
    }

    /// <summary>
    /// Возвращает идентификатор соединения с файловым хранилищем.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Возвращает приоритет соединения.
    /// </summary>
    public int Priority { get; }

    /// <summary>
    /// Возвращает путь к файловому хранилищу.
    /// </summary>
    public string Path { get; }

    /// <summary>
    /// Возвращает описание файлового хранилища.
    /// </summary>
    public FileStorage FileStorage { get; }

    //  Добавить получение вообще всех соединений???????

    //public static async Task<FileStorageConnector> FromIdAsync(long id, CancellationToken cancellationToken)
    //{

    //}

    //public async Task UpdateAsync(CancellationToken cancellationToken)
    //{

    //}

    //public async Task ChangePriorityAsync(int priority, CancellationToken cancellationToken)
    //{

    //}

    //public async Task ChangePathAsync(string path, CancellationToken cancellationToken)
    //{

    //}

    //public async Task RemoveAsync(CancellationToken cancellationToken)
    //{

    //}
}
