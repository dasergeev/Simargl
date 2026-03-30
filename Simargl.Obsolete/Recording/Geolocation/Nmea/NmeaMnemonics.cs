namespace Simargl.Recording.Geolocation.Nmea;

/// <summary>
/// Представляет значение, определяющее мнемонику сообщения NMEA.
/// </summary>
public enum NmeaMnemonics
{
    /// <summary>
    /// Неизвестная мнемоника.
    /// </summary>
    Unknown,

    /// <summary>
    /// Данные местоположения.
    /// </summary>
    Gga,

    /// <summary>
    /// Данные о наземном курсе и скорости.
    /// </summary>
    Vtg,

    /// <summary>
    /// Минимальный рекомендованный набор данных.
    /// </summary>
    Rmc,
}
