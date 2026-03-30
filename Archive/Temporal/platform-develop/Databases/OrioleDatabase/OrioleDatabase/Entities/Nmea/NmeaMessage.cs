namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет сообщение в формате NMEA.
/// </summary>
public abstract class NmeaMessage
{
    /// <summary>
    /// Возвращает или задаёт идентификатор каталога необработанных данных.
    /// </summary>
    public int RawDirectoryId { get; set; }

    /// <summary>
    /// Возвращает или задаёт время начала записи в файл.
    /// </summary>
    public DateTime FileTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт индекс сообщения в файле.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Возвращает или задаёт идентификатор регистратора.
    /// </summary>
    public int RegistrarId { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее проанализировано ли сообщение.
    /// </summary>
    public bool IsAnalyzed { get; set; }

    /// <summary>
    /// Возвращвет или задаёт каталог необработанных данных.
    /// </summary>
    public RawDirectory RawDirectory { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт файл, содержащий NMEA сообщения.
    /// </summary>
    public NmeaFile NmeaFile { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт регистратор.
    /// </summary>
    public Registrar Registrar { get; set; } = null!;
}
