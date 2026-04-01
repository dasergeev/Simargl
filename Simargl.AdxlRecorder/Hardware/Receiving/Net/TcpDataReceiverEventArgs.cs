namespace Simargl.AdxlRecorder.Hardware.Receiving.Net;

/// <summary>
/// Представляет аргументы события класса <see cref="TcpDataReceiver"/>.
/// </summary>
/// <param name="ipEndPoint">
/// Удалённая точка подключения.
/// </param>
/// <param name="time">
/// Время события.
/// </param>
public sealed class TcpDataReceiverEventArgs(IPEndPoint ipEndPoint, DateTime time) :
    EventArgs
{
    /// <summary>
    /// Возвращает удалённую точку подключения.
    /// </summary>
    public IPEndPoint IPEndPoint { get; } = IsNotNull(ipEndPoint);

    /// <summary>
    /// Возвращает время события.
    /// </summary>
    public DateTime Time { get; } = time;
}
