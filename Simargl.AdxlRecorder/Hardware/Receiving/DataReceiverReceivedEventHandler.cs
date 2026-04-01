namespace Simargl.AdxlRecorder.Hardware.Receiving;

/// <summary>
/// Представляет обработчик события <see cref="DataReceiver.Received"/>.
/// </summary>
/// <param name="sender">
/// Объект, создавший событие.
/// </param>
/// <param name="e">
/// Аргументы, связанные с событием.
/// </param>
public delegate void DataReceiverReceivedEventHandler(object? sender, DataReceiverReceivedEventArgs e);
