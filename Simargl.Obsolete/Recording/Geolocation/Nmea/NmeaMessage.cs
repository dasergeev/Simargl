using System.IO;

namespace Simargl.Recording.Geolocation.Nmea;

/// <summary>
/// Представляет сообщение NMEA.
/// </summary>
public abstract class NmeaMessage
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="preamble">
    /// Преамбула сообщения.
    /// </param>
    /// <param name="mnemonics">
    /// Мнемоника сообщения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="preamble"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="mnemonics"/> передано значение,
    /// которое не содержится в перечислении <see cref="NmeaMnemonics"/>.
    /// </exception>
    internal NmeaMessage(string preamble, NmeaMnemonics mnemonics)
    {
        //  Установка преамбулы сообщения.
        Preamble = IsNotNull(preamble, nameof(preamble));

        //  Установка мнемоники сообщения.
        Mnemonics = IsDefined(mnemonics, nameof(mnemonics));
    }

    /// <summary>
    /// Возвращает преамбулу сообщения.
    /// </summary>
    public string Preamble { get; }

    /// <summary>
    /// Возвращает мнемонику сообщения.
    /// </summary>
    public NmeaMnemonics Mnemonics { get; }

    /// <summary>
    /// Выполняет синтаксический анализ сообщения.
    /// </summary>
    /// <param name="message">
    /// Сообщение для анализа.
    /// </param>
    /// <returns>
    /// Сообщение NMEA.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="message"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверная сигнатура строки в формате NMEA.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверная контрольная сумма строки в формате NMEA.
    /// </exception>
    public static NmeaMessage Parse(string message)
    {
        //  Получение данных сообщения.
        NmeaData data = new(message);

        //  Анализ мнемоники.
        return data.Mnemonics.ToUpper() switch
        {
            "GGA" => new NmeaGgaMessage(data.Preamble, data.Fields),
            "VTG" => new NmeaVtgMessage(data.Preamble, data.Fields),
            "RMC" => new NmeaRmcMessage(data.Preamble, data.Fields),
            _ => new NmeaUnknownMessage(data),
        };
    }
}
