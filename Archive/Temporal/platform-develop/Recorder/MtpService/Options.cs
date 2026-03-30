using Apeiron.Platform.Transmitters;

namespace Apeiron.Mtp;

/// <summary>
/// Предоставляет настройки.
/// </summary>
public class Options
{
    /// <summary>
    /// Устанавливает и возвращает номер порта для подключения датчиков MTP.
    /// </summary>
    public int MtpStreamPort { get; set; } = 49002;

    /// <summary>
    /// Устанавливает и возвращает время ожидания данных от датчика MTP в милисекундах.
    /// </summary>
    public int MtpWaitingTimeout { get; set; } = 10000;

    /// <summary>
    /// Устанавливает и возвращает тайм-аут ожидания подключения нового клиента.
    /// </summary>
    public int ListeningTimeout { get; set; } = 1000;

    /// <summary>
    /// Устанавливает и возвращает конфигурацию передатчика.
    /// </summary>
    public TransmittersOptions? TransmitterConfig { get; set; }
}
