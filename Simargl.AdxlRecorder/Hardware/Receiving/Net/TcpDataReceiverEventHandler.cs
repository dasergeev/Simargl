namespace Simargl.AdxlRecorder.Hardware.Receiving.Net;

/// <summary>
/// Представляет обработчик события класса <see cref="TcpDataReceiver"/>.
/// </summary>
/// <param name="sender">
/// Объект, создавший событие.
/// </param>
/// <param name="e">
/// Аргументы, связанные с событием.
/// </param>
public delegate void TcpDataReceiverEventHandler(object? sender, TcpDataReceiverEventArgs e);
