using Apeiron.Gps;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет сообщение GPS, содержащее данные местоположения.
/// </summary>
public class GgaMessage :
    NmeaMessage
{
    /// <summary>
    /// Возвращает или задаёт время определения координат.
    /// </summary>
    public TimeSpan? Time { get; set; }

    /// <summary>
    /// Возвращает или задаёт широту в градусах.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт долготу в градусах.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее способ вычисления координат.
    /// </summary>
    public GpsSolution? Solution { get; set; }

    /// <summary>
    /// Возвращает или задаёт количество активных спутников.
    /// </summary>
    public int? Satellites { get; set; }

    /// <summary>
    /// Возвращает или задаёт горизонтальное снижение точности.
    /// </summary>
    public double? Hdop { get; set; }

    /// <summary>
    /// Возвращает или задаёт высоту над средним уровнем моря в метрах.
    /// </summary>
    public double? Altitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт отклонение геоида.
    /// </summary>
    /// <remarks>
    /// Отклонение геоида - это различие между поверхностью земного эллипсоида WGS-84
    /// и средним уровнем моря (поверхностью геоида).
    /// Если значение отрицательное, то средний уровень моря
    /// находится ниже уровня поверхности эллипсоида WGS-84.
    /// </remarks>
    public double? Geoidal { get; set; }

    /// <summary>
    /// Возвращает или задаёт возраст дифференциальных поправок.
    /// </summary>
    /// <remarks>
    /// Возраст дифференциальных поправок - это количество секунд,
    /// прошедшее с момента прихода последнего сообщения с дифференциальными коррекциями.
    /// </remarks>
    public double? Age { get; set; }

    /// <summary>
    /// Возвращает или задаёт идентификатор дифференциальной станции.
    /// </summary>
    public int? Station { get; set; }
}
