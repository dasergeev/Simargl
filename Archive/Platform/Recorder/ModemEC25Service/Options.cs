using Apeiron.Platform.Transmitters;

namespace Apeiron.EC25;

/// <summary>
/// Представляет класс настроек программы.
/// </summary>
public class Options
{
    /// <summary>
    /// Представляет путь к файлу Nmea.
    /// </summary>
    public string? NmeaPipeFile;

    /// <summary>
    /// Представляет конфигурацию передачи Nmea.
    /// </summary>
    public TransmittersOptions? NmeaTransmitteOptions { get; set; }

}

