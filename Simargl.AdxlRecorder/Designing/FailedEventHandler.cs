namespace Simargl.AdxlRecorder.Designing;

/// <summary>
/// Представляет обработчик события регистрации исключения.
/// </summary>
/// <param name="sender">
/// Объект, создавший событие.
/// </param>
/// <param name="e">
/// Аргументы, связанные с событием.
/// </param>
public delegate void FailedEventHandler(object? sender, FailedEventArgs e);
