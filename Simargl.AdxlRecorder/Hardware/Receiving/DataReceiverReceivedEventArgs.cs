namespace Simargl.AdxlRecorder.Hardware.Receiving;

/// <summary>
/// Представляет данные для события <see cref="DataReceiver.Received"/>.
/// </summary>
/// <param name="data">
/// Результат получения данных.
/// </param>
public sealed class DataReceiverReceivedEventArgs(DataReceiveResult data) :
    EventArgs
{
    /// <summary>
    /// Возвращает результат получения данных.
    /// </summary>
    public DataReceiveResult Data { get; } = data;
}
