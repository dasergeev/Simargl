using Apeiron.Gps;

namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет геолокационные данные.
/// </summary>
public class Geolocation
{
    /// <summary>
    /// Возвращает или задаёт идентификатор регистратора.
    /// </summary>
    public int RegistrarId { get; set; }

    /// <summary>
    /// Возвращает или задаёт метку времени получения данных.
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее проанализированы ли данные.
    /// </summary>
    public bool IsAnalyzed { get; set; }

    /// <summary>
    /// Возвращает или задаёт время определения координат.
    /// </summary>
    public DateTime? Time { get; set; }

    /// <summary>
    /// Возвращает или задаёт широту в градусах.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт долготу в градусах.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт высоту над средним уровнем моря в метрах.
    /// </summary>
    public double? Altitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт скорость в километрах в час.
    /// </summary>
    public double? Speed { get; set; }

    /// <summary>
    /// Возвращает или задаёт курс на истинный полюс в градусах.
    /// </summary>
    public double? PoleCourse { get; set; }

    /// <summary>
    /// Возвращает или задаёт курс на магнитный полюс в градусах.
    /// </summary>
    public double? MagneticCourse { get; set; }

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

    /// <summary>
    /// Возвращает или задаёт значение, определяющее достоверность координат.
    /// </summary>
    public bool? Valid { get; set; }

    /// <summary>
    /// Возвращает или задаёт скорость в узлах.
    /// </summary>
    public double? Knots { get; set; }

    /// <summary>
    /// Возвращает или задаёт отклонение курса на магнитный полюс.
    /// </summary>
    public double? MagneticVariation { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее режим системы позиционирования.
    /// </summary>
    public GpsMode? Mode { get; set; }

    /// <summary>
    /// Возвращает или задаёт регистратор.
    /// </summary>
    public Registrar Registrar { get; set; } = null!;

    /// <summary>
    /// Возвращает метку времени для времени.
    /// </summary>
    /// <param name="time">
    /// Время.
    /// </param>
    /// <returns>
    /// Метка времени.
    /// </returns>
    public static long ToTimestamp(DateTime time)
    {
        //  Расчёт метки времени.
        return time.Ticks / TimeSpan.TicksPerSecond;
    }

    /// <summary>
    /// Возвращает время для заданной метки времени.
    /// </summary>
    /// <param name="timestamp">
    /// Метка времени.
    /// </param>
    /// <returns>
    /// Время.
    /// </returns>
    public static DateTime FromTimestamp(long timestamp)
    {
        //  Расчёт времени.
        return new DateTime(timestamp * TimeSpan.TicksPerSecond);
    }
}
