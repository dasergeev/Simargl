using Apeiron.Platform.Transmitters;

namespace Apeiron.Adxl;

/// <summary>
/// Предоставляет настройки.
/// </summary>
public class Options

{
    /// <summary>
    /// Возвращает и устанавливает конфигурацию передатчиков датчиков.
    /// </summary>
    public List<TransmitterSensorOptions>?  SensorTransmitterConfiguration { get; set; }
}
