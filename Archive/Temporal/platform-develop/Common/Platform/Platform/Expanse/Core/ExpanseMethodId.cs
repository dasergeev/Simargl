namespace Apeiron.Platform.Expanse.Core;

/// <summary>
/// Предоставляет идентификатор метода, выполняемого в серверном пространстве.
/// </summary>
internal enum ExpanseMethodId
{
    /// <summary>
    /// Закрытие соединения.
    /// </summary>
    Shutdown = 0,

    /// <summary>
    /// Метод, возвращающий все файловые хранилища.
    /// </summary>
    GetAllFileStorages = 1,

    /// <summary>
    /// Метод, возвращающий файловое хранилище по идентификатору.
    /// </summary>
    GetFileStorageFromId = 2,

    /// <summary>
    /// Метод, возвращающий файловое хранилище по имени.
    /// </summary>
    GetFileStorageFromName = 3,

    /// <summary>
    /// Метод, обновляющий свойства файлового хранилища.
    /// </summary>
    UpdateFileStorage = 4,

    /// <summary>
    /// Метод, изменяющий имя файлового хранилища.
    /// </summary>
    RenameFileStorage = 5,

    /// <summary>
    /// Метод, создающий файловое хранилище.
    /// </summary>
    CreateFileStorage = 6,

    /// <summary>
    /// Метод, удаляющий файловое хранилище.
    /// </summary>
    RemoveFileStorage = 7,

    /// <summary>
    /// Метод, возвращающий информацию о соединениях с файловым хранилищем.
    /// </summary>
    GetFileStorageConnectors = 8,

    /// <summary>
    /// Метод, добавляющий информацию о соединении с файловым хранилищем.
    /// </summary>
    AddFileStorageConnector = 9,

    /// <summary>
    /// Метод, удаляющий информацию о соединении с файловым хранилищем.
    /// </summary>
    RemoveFileStorageConnector = 10,

    /// <summary>
    /// Метод, возвращающий информацию о соединении с файловым хранилищем по идентификатору.
    /// </summary>
    GetFileStorageConnectorFromId = 11,

    /// <summary>
    /// Метод, обновляющий свойства информации о соединении с файловым хранилищем.
    /// </summary>
    UpdateFileStorageConnector = 12,

    /// <summary>
    /// Метод, изменяющий приоритет информации о соединении с файловым хранилищем.
    /// </summary>
    ChangeFileStorageConnectorPriority = 13,

    /// <summary>
    /// Метод, изменяющий путь к файловому хранилищу в информации о соединении с файловым хранилищем.
    /// </summary>
    ChangeFileStorageConnectorPath = 14,
}
