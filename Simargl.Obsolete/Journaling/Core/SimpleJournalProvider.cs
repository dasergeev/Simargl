using Simargl.Concurrent;

namespace Simargl.Journaling.Core;

/// <summary>
/// Представляет простого поставщика журнала.
/// </summary>
internal sealed class SimpleJournalProvider :
    JournalProvider
{
    /// <summary>
    /// Поле для хранения метода, отправляющего запись.
    /// </summary>
    private readonly AsyncAction<JournalRecord> _Sender;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="sender">
    /// Метод, отправляющий запись.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sender"/> передана пустая ссылка.
    /// </exception>
    public SimpleJournalProvider(AsyncAction<JournalRecord> sender)
    {
        //  Проверка метода.
        IsNotNull(sender);

        //  Установка метода.
        _Sender = sender;
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="sender">
    /// Метод, отправляющий запись.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sender"/> передана пустая ссылка.
    /// </exception>
    public SimpleJournalProvider(Action<JournalRecord> sender)
    {
        //  Проверка метода.
        IsNotNull(sender);

        //  Установка метода.
        _Sender = async delegate (JournalRecord record, CancellationToken cancellationToken)
        {
            //  Проверка записи.
            IsNotNull(record);

            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Отправка записи.
            sender(record);
        };
    }

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
    public override sealed async Task SendAsync(JournalRecord record, CancellationToken cancellationToken)
    {
        //  Проверка записи.
        IsNotNull(record);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Отправка записи.
        await _Sender(record, cancellationToken).ConfigureAwait(false);
    }
}
