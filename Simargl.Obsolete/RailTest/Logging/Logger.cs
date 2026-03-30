namespace RailTest.Logging;

/// <summary>
/// Представляет средство для ведения журнала.
/// </summary>
public abstract class Logger :
    ILogger
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sender">
    /// Имя отправителя.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sender"/> передана пустая ссылка.
    /// </exception>
    protected Logger(string sender)
    {
        //  Проверка ссылки на имя отправителя.
        IsNotNull(sender, nameof(sender));

        //  Установка имени отправителя.
        Sender = sender;
    }

    /// <summary>
    /// Возвращает имя отправителя.
    /// </summary>
    public string Sender { get; }

    /// <summary>
    /// Регистрирует сообщение.
    /// </summary>
    /// <param name="message">
    /// Сообщение, которое необходимо зарегистрировать.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="message"/> передана пустая ссылка.
    /// </exception>
    public void LogMessage(string message)
    {
        //  Проверка ссылки на сообщение.
        IsNotNull(message, nameof(message));

        //  Регистрация строки журнала.
        Log(new LoggerRow(Sender, DateTime.Now, message));
    }

    /// <summary>
    /// Регистрирует строку журнала.
    /// </summary>
    /// <param name="row">
    /// Строка журнала.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="row"/> передана пустая ссылка.
    /// </exception>
    protected abstract void Log(LoggerRow row);
}
