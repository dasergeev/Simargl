namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет файл с исходными данными.
/// </summary>
public abstract class SourceFile
{
    /// <summary>
    /// Возвращает или задаёт идентификатор каталога необработанных данных.
    /// </summary>
    public int RawDirectoryId { get; set; }

    /// <summary>
    /// Возвращает или задаёт время начала записи в файл.
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Возвращвет или задаёт каталог необработанных данных.
    /// </summary>
    public RawDirectory RawDirectory { get; set; } = null!;
}
