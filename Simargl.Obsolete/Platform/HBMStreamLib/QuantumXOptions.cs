using Simargl.Platform.Transmitters;


namespace Simargl.QuantumX;

/// <summary>
/// Конфигурация QuantumX 
/// </summary>
public class QuantumXOptions
{
    /// <summary>
    /// Устанавливает и возвращает опции передатчика.
    /// </summary>
    public TransmitterSensorOptions? TransmitterSensorOptions { get; set; }

    /// <summary>
    /// Устанавливает и возвращает количество каналов в устройстве.
    /// </summary>
    public int MaximumChannel { get; set; } = 16;

    /// <summary>
    /// Устанавливает и возвращает размер буфера получателя.
    /// </summary>
    public int TcpReceiverBufferSize { get; set; } = 10 * 1024 * 1024;

    /// <summary>
    /// Устанавливает и возвращает размер порции получения.
    /// </summary>
    public int TcpReceiverPortionSize { get; set; } = 10 * 1024;

    /// <summary>
    /// Устанавливает и возвращает время ожидания буфера данных.
    /// </summary>
    public int TcpReceiverTimeSpan { get; set; } = 2;
}
