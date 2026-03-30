namespace Apeiron.Platform.Demo.AdxlDemo.Logging;

/// <summary>
/// Представляет сообщение журнала.
/// </summary>
public sealed class LogMessage
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="text">
    /// Текст сообщения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    public LogMessage(string text)
    {
        //  Установка времени сообщения.
        Time = DateTime.Now;

        //  Установка текста сообщения.
        Text = IsNotNull(text, nameof(text));

        //  Установка индентификатора потока.
        ThreadId = Environment.CurrentManagedThreadId;
    }

    /// <summary>
    /// Возвращает время сообщения.
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    /// Возвращает идентификатор потока, который создал сообщение.
    /// </summary>
    public int ThreadId { get; }

    /// <summary>
    /// Возвращает текст сообщения.
    /// </summary>
    public string Text { get; }
}
