namespace Apeiron.Platform.Databases.FileStorageDatabase.Entities;

/// <summary>
/// Представляет информацию о соединении с файловым хранилищем.
/// </summary>
[CLSCompliant(false)]
[Index(nameof(Path))]
public sealed class FileStorageConnector :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт приоритет соединения.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Возвращает или задаёт путь к файловому хранилищу.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт файловое хранилище.
    /// </summary>
    public FileStorage FileStorage { get; set; } = null!;
}
