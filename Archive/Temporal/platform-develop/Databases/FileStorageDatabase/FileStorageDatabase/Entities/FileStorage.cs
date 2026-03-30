namespace Apeiron.Platform.Databases.FileStorageDatabase.Entities;

/// <summary>
/// Представляет файловое хранилище.
/// </summary>
[CLSCompliant(false)]
public sealed class FileStorage :
    NamedEntity
{
    /// <summary>
    /// Возвращает коллекцию информации о соединениях с файловым хранилищем.
    /// </summary>
    public HashSet<FileStorageConnector> FileStorageConnectors { get; } = new();
}
