namespace Apeiron.Platform.Databases.Ape90Database.Entities;

/// <summary>
/// Представляет файл с исходными данными.
/// </summary>
public abstract class RawFile :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт путь к файлу.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт идентификатор каталога исходных данных.
    /// </summary>
    public long RawDirectoryId { get; set; }

    /// <summary>
    /// Возвращает или задаёт каталог исходных данных.
    /// </summary>
    public RawDirectory RawDirectory { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт значение, определяющее определены ли метрики файла.
    /// </summary>
    public bool IsMetrics { get; set; }

    /// <summary>
    /// Возвращает или задаёт время, определённое по имени файла.
    /// </summary>
    public DateTime NameTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт время создания файла.
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт время последнего доступа к файлу.
    /// </summary>
    public DateTime LastAccessTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт время псоледней записи в файл.
    /// </summary>
    public DateTime LastWriteTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт размер файла.
    /// </summary>
    public long Size { get; set; }
}
