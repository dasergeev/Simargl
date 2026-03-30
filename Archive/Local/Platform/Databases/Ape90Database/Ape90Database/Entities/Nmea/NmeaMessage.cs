namespace Apeiron.Platform.Databases.Ape90Database.Entities;

/// <summary>
/// Представляет сообщение в формате NMEA.
/// </summary>
public abstract class NmeaMessage :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт идентификатор файла геолокационных данных.
    /// </summary>
    public long RawGeolocationId { get; set; }

    /// <summary>
    /// Возвращает или задаёт время начала записи в файл.
    /// </summary>
    public DateTime FileTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт индекс сообщения в файле.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее проанализировано ли сообщение.
    /// </summary>
    public bool IsAnalyzed { get; set; }

    /// <summary>
    /// Возвращвет или задаёт файл геолокационных данных.
    /// </summary>
    public RawGeolocation RawGeolocation { get; set; } = null!;
}
