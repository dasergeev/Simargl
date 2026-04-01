using System;

namespace RailTest.Logging;

/// <summary>
/// Представляет средство для ведения журнала.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Регистрирует сообщение.
    /// </summary>
    /// <param name="message">
    /// Сообщение, которое необходимо зарегистрировать.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="message"/> передана пустая ссылка.
    /// </exception>
    void LogMessage(string message);
}
