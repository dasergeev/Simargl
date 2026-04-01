namespace Simargl.Designing;

/// <summary>
/// Представляет данные для события регистрации исключения.
/// </summary>
/// <param name="exception">
/// Исключение.
/// </param>
public sealed class FailedEventArgs(Exception exception) :
    EventArgs
{
    /// <summary>
    /// Возвращает исключение, вызвавшее ошибку.
    /// </summary>
    public Exception Exception { get; } = exception;
}
