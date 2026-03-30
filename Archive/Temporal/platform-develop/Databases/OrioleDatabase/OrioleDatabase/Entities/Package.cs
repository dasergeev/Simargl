namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет пакет данных.
/// </summary>
public class Package
{
    /// <summary>
    /// Возвращает или задаёт каталога необработанных данных.
    /// </summary>
    public int RawDirectoryId { get; set; }

    /// <summary>
    /// Возвращает или задаёт формат данных.
    /// </summary>
    public int Format { get; set; }

    /// <summary>
    /// Возвращает или задаёт время начала записи файла.
    /// </summary>
    public DateTime FileTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт смещение в файле.
    /// </summary>
    public int FileOffset { get; set; }

    /// <summary>
    /// Возвращает или задаёт синхромаркер в тактах.
    /// </summary>
    public long Synchromarker { get; set; }

    /// <summary>
    /// Возвращает или задаёт длину синхронных сигналов.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее проанализирован ли пакет.
    /// </summary>
    public bool IsAnalyzed { get; set; }

    /// <summary>
    /// Возвращает или задаёт время начала записи пакета.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации данных.
    /// </summary>
    public double Sampling { get; set; }

    /// <summary>
    /// Возвращвет или задаёт каталог необработанных данных.
    /// </summary>
    public RawDirectory RawDirectory { get; set; } = null!;

    /// <summary>
    /// Возвращвет или задаёт файл с пакетами данных.
    /// </summary>
    public PackageFile PackageFile { get; set; } = null!;
}
