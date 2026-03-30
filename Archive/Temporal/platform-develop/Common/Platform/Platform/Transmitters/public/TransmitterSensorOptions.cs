namespace Apeiron.Platform.Transmitters;

/// <summary>
/// Представляет конфигурацию датчика.
/// </summary>
public class TransmitterSensorOptions
{
    /// <summary>
    /// Возвращает и устанавливает IP адрес датчика.
    /// </summary>
    public string? SensorIP { get; set; }

    /// <summary>
    /// Возвращает и устанавливает настройки передачи по умолчанию.
    /// </summary>
    public TransmittersOptions? TranmitterConfig { get; set; }
}
