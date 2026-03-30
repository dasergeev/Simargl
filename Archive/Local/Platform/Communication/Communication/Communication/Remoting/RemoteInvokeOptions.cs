namespace Apeiron.Platform.Communication.Remoting;

/// <summary>
/// Представляет параметры удалённого вызова.
/// </summary>
public readonly struct RemoteInvokeOptions
{
    /// <summary>
    /// Возвращает пустые параметры удалённого вызова.
    /// </summary>
    public static RemoteInvokeOptions Empty { get; } = new();

    /// <summary>
    /// Инициализирует новый экземпляр структуры.
    /// </summary>
    /// <param name="timeout">
    /// Время ожидания ответа.
    /// </param>
    /// <param name="attempts">
    /// Количество попыток.
    /// </param>
    /// <param name="delay">
    /// Время задержки перед повторной попыткой.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="timeout"/> передано отрицательное или нулевое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="attempts"/> передано отрицательное или нулевое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="delay"/> передано отрицательное или нулевое значение.
    /// </exception>
    public RemoteInvokeOptions(TimeSpan timeout, int attempts, TimeSpan delay)
    {
        //  Установка времени ожидания ответа.
        Timeout = IsPositive(timeout, nameof(timeout));

        //  Установка поличества попыток.
        Attempts = IsPositive(attempts, nameof(attempts));

        //  Установка времени задержки перед повторной попыткой.
        Delay = IsPositive(delay, nameof(delay));
    }

    /// <summary>
    /// Возвращает время ожидания ответа.
    /// </summary>
    public TimeSpan Timeout { get; }

    /// <summary>
    /// Возвращает количество попыток.
    /// </summary>
    public int Attempts { get; }

    /// <summary>
    /// Возвращает время задержки перед повторной попыткой.
    /// </summary>
    public TimeSpan Delay { get; }

    /// <summary>
    /// Возвращает значение, определяющее, являются ли параметры пустыми.
    /// </summary>
    public bool IsEmpty => Attempts == 0;

    /// <summary>
    /// Асинхронно создаёт токен отмены, отслеживающий таймаут.
    /// </summary>
    /// <param name="cancellationToken">
    /// Исходный токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая токен отмены, отслеживающий таймаут.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    internal async Task<CancellationToken> CreateTokenAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание токена отмены для таймаута.
        CancellationTokenSource timeoutTokenSource = new();

        //  Установка таймаута.
        timeoutTokenSource.CancelAfter(Timeout);

        //  Создание источника общего токена отмены.
        CancellationTokenSource tokenSource = CancellationTokenSource
            .CreateLinkedTokenSource(cancellationToken, timeoutTokenSource.Token);

        //  Возврат общего токена отмены.
        return tokenSource.Token;
    }
}
