using Apeiron.Gps;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет сообщение GPS, содержащее минимальный рекомендованный набор данных.
/// </summary>
public class RmcMessage :
    NmeaMessage
{
    /// <summary>
    /// Возвращает или задаёт время определения координат.
    /// </summary>
    public TimeSpan? Time { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее достоверность координат.
    /// </summary>
    public bool? Valid { get; set; }

    /// <summary>
    /// Возвращает или задаёт широту в градусах.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт долготу в градусах.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт скорость в узлах.
    /// </summary>
    public double? Knots { get; set; }

    /// <summary>
    /// Возвращает или задаёт скорость в километрах в час.
    /// </summary>
    public double? Speed { get; set; }

    /// <summary>
    /// Возвращает или задаёт курс на истинный полюс в градусах.
    /// </summary>
    public double? PoleCourse { get; set; }

    /// <summary>
    /// Возвращает или задаёт дату.
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Возвращает или задаёт отклонение курса на магнитный полюс.
    /// </summary>
    public double? MagneticVariation { get; set; }

    /// <summary>
    /// Возвращает или задаёт курс на магнитный полюс в градусах.
    /// </summary>
    public double? MagneticCourse { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее режим системы позиционирования.
    /// </summary>
    public GpsMode? Mode { get; set; }
}
