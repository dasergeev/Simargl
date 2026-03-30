namespace Apeiron.Gps.Nmea;

/// <summary>
/// Представляет сообщение NMEA, содержащее данные о наземном курсе и скорости.
/// </summary>
public class NmeaVtgMessage :
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
    public NmeaVtgMessage(string preamble, NmeaFieldCollection fields) :
        base(preamble, NmeaMnemonics.Vtg)
    {
        //  Проверка ссылки на коллекцию полей сообщения.
        fields = IsNotNull(fields, nameof(fields));

        //  Установка курса на истинный полюс в градусах.
        PoleCourse = fields[0..2].ToFloating('T');

        //  Установка курса на магнитный полюс в градусах.
        MagneticCourse = fields[2..4].ToFloating('M');

        //  Установка скорости в узлах.
        Knots = fields[4..6].ToFloating('N');

        //  Установка скорости в километрах в час.
        Speed = fields[6..8].ToFloating('K');

        //  Установка значения, определяющего режим системы позиционирования.
        Mode = fields[8].ToGpsMode();
    }

    /// <summary>
    /// Возвращает курс на истинный полюс в градусах.
    /// </summary>
    public double? PoleCourse { get; }

    /// <summary>
    /// Возвращает курс на магнитный полюс в градусах.
    /// </summary>
    public double? MagneticCourse { get; }

    /// <summary>
    /// Возвращает скорость в узлах.
    /// </summary>
    public double? Knots { get; }

    /// <summary>
    /// Возвращает скорость в километрах в час.
    /// </summary>
    public double? Speed { get; }

    /// <summary>
    /// Возвращает значение, определяющее режим системы позиционирования.
    /// </summary>
    public GpsMode? Mode { get; }
}
