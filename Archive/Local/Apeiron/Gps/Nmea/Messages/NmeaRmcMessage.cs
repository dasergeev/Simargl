namespace Apeiron.Gps.Nmea;

/// <summary>
/// Представляет сообщение NMEA, содержащее минимальный рекомендованный набор данных.
/// </summary>
public class NmeaRmcMessage :
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
    public NmeaRmcMessage(string preamble, NmeaFieldCollection fields) :
        base(preamble, NmeaMnemonics.Rmc)
    {
        //  Значение одного узла в километрах в час.
        const double knot = 1.852;

        //  Проверка ссылки на коллекцию полей сообщения.
        fields = IsNotNull(fields, nameof(fields));

        //  Установка времени определения координат.
        Time = fields[0].ToTime();

        //  Установка значения, определяющего достоверность координат.
        Valid = fields[1].ToStatus();

        //  Установка широты.
        Latitude = fields[2..4].ToLatitude();

        //  Установка долготы.
        Longitude = fields[4..6].ToLongitude();

        //  Установка скорости в узлах.
        Knots = fields[6].ToFloating();

        //  Установка скорости в километрах в час.
        Speed = Knots * knot;

        //  Установка курса на истинный полюс в градусах.
        PoleCourse = fields[7].ToFloating();

        //  Установка даты.
        Date = fields[8].ToDate();

        //  Установка отклонения курса на магнитный полюс.
        MagneticVariation = fields[9..11].ToMagneticVariation();

        //  Установка курса на магнитный полюс в градусах.
        MagneticCourse = PoleCourse + MagneticVariation;

        //  Установка значения, определяющего режим системы позиционирования.
        Mode = fields[11].ToGpsMode();
    }

    /// <summary>
    /// Возвращает время определения координат.
    /// </summary>
    public TimeOnly? Time { get; }

    /// <summary>
    /// Возвращает значение, определяющее достоверность координат.
    /// </summary>
    public bool? Valid { get; }

    /// <summary>
    /// Возвращает широту в градусах.
    /// </summary>
    public double? Latitude { get; }

    /// <summary>
    /// Возвращает долготу в градусах.
    /// </summary>
    public double? Longitude { get; }

    /// <summary>
    /// Возвращает скорость в узлах.
    /// </summary>
    public double? Knots { get; }

    /// <summary>
    /// Возвращает скорость в километрах в час.
    /// </summary>
    public double? Speed { get; }

    /// <summary>
    /// Возвращает курс на истинный полюс в градусах.
    /// </summary>
    public double? PoleCourse { get; }

    /// <summary>
    /// Возвращает дату.
    /// </summary>
    public DateOnly? Date { get; }

    /// <summary>
    /// Возвращает отклонение курса на магнитный полюс.
    /// </summary>
    public double? MagneticVariation { get; }

    /// <summary>
    /// Возвращает курс на магнитный полюс в градусах.
    /// </summary>
    public double? MagneticCourse { get; }

    /// <summary>
    /// Возвращает значение, определяющее режим системы позиционирования.
    /// </summary>
    public GpsMode? Mode { get; }
}
