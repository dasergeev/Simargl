namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет источник данных.
/// </summary>
public class Source
{
    /// <summary>
    /// Возвращает или инициализирует идентификатор источника данных.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Возвращает или задаёт идентификатор датика.
    /// </summary>
    public int SensorId { get; set; }

    /// <summary>
    /// Возвращает или задаёт идентификатор канала.
    /// </summary>
    public int ChannelId { get; set; }

    /// <summary>
    /// Возвращает или задаёт время начала действия источника.
    /// </summary>
    public DateTime BeginTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт время окончания действия источника.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт формат данных.
    /// </summary>
    public int Format { get; set; }

    /// <summary>
    /// Возвращает или задаёт номер сигнала.
    /// </summary>
    public int Signal { get; set; }

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    public double Sampling { get; set; }

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    public double Cutoff { get; set; }

    /// <summary>
    /// Возвращает или задаёт датчик.
    /// </summary>
    public Sensor Sensor { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт канал.
    /// </summary>
    public Channel Channel { get; set; } = null!;
}
