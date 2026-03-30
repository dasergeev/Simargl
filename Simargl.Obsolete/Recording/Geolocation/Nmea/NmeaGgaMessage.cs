using System.IO;

namespace Simargl.Recording.Geolocation.Nmea;

/// <summary>
/// Представляет сообщение NMEA, содержащее данные местоположения.
/// </summary>
public class NmeaGgaMessage :
    NmeaMessage
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="preamble">
    /// Преамбула сообщения.
    /// </param>
    /// <param name="fields">
    /// Коллекция полей сообщения NMEA.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="preamble"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="fields"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public NmeaGgaMessage(string preamble, NmeaFieldCollection fields) :
        base(preamble, NmeaMnemonics.Gga)
    {
        //  Проверка ссылки на коллекцию полей сообщения.
        fields = IsNotNull(fields, nameof(fields));

        //  Установка времени определения координат.
        Time = fields[0].ToTime();

        //  Установка широты.
        Latitude = fields[1..3].ToLatitude();

        //  Установка долготы.
        Longitude = fields[3..5].ToLongitude();

        //  Установка значения, определяющего способ вычисления координат.
        Solution = fields[5].ToGpsSolution();

        //  Установка количества активных спутников.
        Satellites = fields[6].ToInteger(2, 0, int.MaxValue);

        //  Установка горизонтального снижения точности.
        Hdop = fields[7].ToFloating(0, double.MaxValue);

        //  Установка высоты над средним уровнем моря.
        Altitude = fields[8..10].ToFloating('M');

        //  Установка отклонения геоида.
        Geoidal = fields[10..12].ToFloating('M');

        //  Установка возраста дифференциальных поправок.
        Age = fields[12].ToFloating();

        //  Установка идентификатора дифференциальной станции.
        Station = fields[13].ToInteger(4, 0, int.MaxValue);
    }

    /// <summary>
    /// Возвращает время определения координат.
    /// </summary>
    public TimeOnly? Time { get; }

    /// <summary>
    /// Возвращает широту в градусах.
    /// </summary>
    public double? Latitude { get; }

    /// <summary>
    /// Возвращает долготу в градусах.
    /// </summary>
    public double? Longitude { get; }

    /// <summary>
    /// Возвращает значение, определяющее способ вычисления координат.
    /// </summary>
    public GpsSolution? Solution { get; }

    /// <summary>
    /// Возвращает количество активных спутников.
    /// </summary>
    public int? Satellites { get; }

    /// <summary>
    /// Возвращает горизонтальное снижение точности.
    /// </summary>
    public double? Hdop { get; }

    /// <summary>
    /// Возвращает высоту над средним уровнем моря в метрах.
    /// </summary>
    public double? Altitude { get; }

    /// <summary>
    /// Возвращает отклонение геоида.
    /// </summary>
    /// <remarks>
    /// Отклонение геоида - это различие между поверхностью земного эллипсоида WGS-84
    /// и средним уровнем моря (поверхностью геоида).
    /// Если значение отрицательное, то средний уровень моря
    /// находится ниже уровня поверхности эллипсоида WGS-84.
    /// </remarks>
    public double? Geoidal { get; }

    /// <summary>
    /// Возвращает возраст дифференциальных поправок.
    /// </summary>
    /// <remarks>
    /// Возраст дифференциальных поправок - это количество секунд,
    /// прошедшее с момента прихода последнего сообщения с дифференциальными коррекциями.
    /// </remarks>
    public double? Age { get; }

    /// <summary>
    /// Возвращает идентификатор дифференциальной станции.
    /// </summary>
    public int? Station { get; }
}
