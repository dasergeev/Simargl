using Simargl.Concurrent;
using Simargl.Journaling.Core;

namespace Simargl.Journaling;

/// <summary>
/// Представляет поставщика журнала.
/// </summary>
public abstract class JournalProvider
{
    /// <summary>
    /// Асинхронно отправляет запись.
    /// </summary>
    /// <param name="record">
    /// Отправляемая запись.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запись в журнал.
    /// </returns>
    public abstract Task SendAsync(JournalRecord record, CancellationToken cancellationToken);

    /// <summary>
    /// Создаёт простого поставщика журнала.
    /// </summary>
    /// <param name="sender">
    /// Метод, отправляющий запись.
    /// </param>
    /// <returns>
    /// Простой поставщик журнала.
    /// </returns>
    public static JournalProvider Create(AsyncAction<JournalRecord> sender)
    {
        //  Возврат простого поставщика журнала.
        return new SimpleJournalProvider(sender);
    }

    /// <summary>
    /// Создаёт простого поставщика журнала.
    /// </summary>
    /// <param name="sender">
    /// Метод, отправляющий запись.
    /// </param>
    /// <returns>
    /// Простой поставщик журнала.
    /// </returns>
    public static JournalProvider Create(Action<JournalRecord> sender)
    {
        //  Возврат простого поставщика журнала.
        return new SimpleJournalProvider(sender);
    }
}
