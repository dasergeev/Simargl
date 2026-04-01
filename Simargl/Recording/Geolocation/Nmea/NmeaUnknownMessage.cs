namespace Simargl.Recording.Geolocation.Nmea;

/// <summary>
/// Представляет неизвестное сообщение NMEA.
/// </summary>
public sealed class NmeaUnknownMessage :
    NmeaMessage
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="data">
    /// Данные сообщения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="data"/> передана пустая ссылка.
    /// </exception>
    internal NmeaUnknownMessage(NmeaData data) :
        base(IsNotNull(data, nameof(data)).Preamble,
            NmeaMnemonics.Unknown)
    {
        //  Установка данных сообщения.
        Data = data;
    }

    /// <summary>
    /// Возвращает данные сообщения.
    /// </summary>
    public NmeaData Data { get; }
}
