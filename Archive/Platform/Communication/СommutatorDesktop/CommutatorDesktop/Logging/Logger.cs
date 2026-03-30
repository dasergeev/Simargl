using System.Collections.Concurrent;

namespace Apeiron.Platform.Communication.СommutatorDesktop.Logging;

/// <summary>
/// Представляет средство ведения журнала.
/// </summary>
public sealed class Logger :
    Active
{
    /// <summary>
    /// Поле для хранения очереди сообщений журнала.
    /// </summary>
    private readonly ConcurrentQueue<LogMessage> _LogMessages;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logMessages">
    /// Очередь сообщений журнала.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logMessages"/> передана пустая ссылка.
    /// </exception>
    public Logger(ConcurrentQueue<LogMessage> logMessages)
    {
        //  Установка очереди сообщений журнала.
        _LogMessages = IsNotNull(logMessages, nameof(logMessages));
    }

    /// <summary>
    /// Добавляет сообщение в журнал.
    /// </summary>
    /// <param name="text">
    /// Текст сообщения.
    /// </param>
    /// <returns>
    /// Сообщение, добавляемое в журнал.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    public LogMessage Log(string text)
    {
        //  Создание сообщения.
        LogMessage message = new(text);

        //  Добавление сообщения в очередь.
        _LogMessages.Enqueue(message);

        //  Возврат добавленного сообщения.
        return message;
    }

    /// <summary>
    /// Добавляет сообщение об исключении в журнал.
    /// </summary>
    /// <param name="text">
    /// Текст сообщения.
    /// </param>
    /// <param name="exception">
    /// Исключение, информацию о котором необходимо добавить в журнал.
    /// </param>
    /// <returns>
    /// Сообщение, добавляемое в журнал.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="exception"/> передана пустая ссылка.
    /// </exception>
    public LogMessage Log(string text, Exception exception)
    {
        //  Проверка ссылок.
        IsNotNull(text, nameof(text));
        IsNotNull(exception, nameof(exception));

        //  Добавление сообщения в журнал.
        return Log($"{text}\r\n{exception}");
    }
}
