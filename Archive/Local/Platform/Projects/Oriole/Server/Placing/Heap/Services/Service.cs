namespace Apeiron.Oriole.Server.Services;

/// <summary>
/// Представляет службу.
/// </summary>
/// <typeparam name="TService">
/// Тип службы.
/// </typeparam>
public abstract class Service<TService> :
    BackgroundService
    where TService : Service<TService>
{
    /// <summary>
    /// Постоянная, определяющая задержку в милисекундах запуска службы
    /// для инициализации консоли и выдачи служебных сообщений.
    /// </summary>
    private const int _InitializationDelay = 50;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал службы.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public Service(ILogger<TService> logger)
    {
        //  Установка средства записи в журнал службы.
        Logger = Check.IsNotNull(logger, nameof(logger));
    }

    /// <summary>
    /// Возвращает средство записи в журнал службы.
    /// </summary>
    protected ILogger<TService> Logger { get; }

    /// <summary>
    /// Возвращает таймаут работы службы.
    /// </summary>
    protected TimeSpan Timeout { get; } = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Ассинхронно выполняет фоновую работу с поддержкой.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая фоновую работу с поддержкой.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override sealed async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Задержка для инициализации консоли и выдачи служебных сообщений.
        await Task.Delay(_InitializationDelay, cancellationToken).ConfigureAwait(false);

        //  Основной цикл службы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех некритических исключений.
            try
            {
                //  Выполнение фоновой работы.
                await InvokeAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Вывод сообщения об ошибке в журнал.
                    Logger.LogError("{exception}", ex);
                }

                //  Проверка критического исключения.
                if (ex.IsCritical())
                {
                    //  Повторный выброс исключения.
                    throw;
                }
            }

            //  Ожидание перед повторным запуском.
            await Task.Delay(Timeout, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет фоновую работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая фоновую работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected abstract Task InvokeAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Асинхронно безопасно выполняет действие.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить безопасно.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая безопасный вызов действия.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected async ValueTask SafeCallAsync(
        Func<CancellationToken, ValueTask> action,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блок перехвата всех некритических исключений.
        try
        {
            //  Выполнение действия.
            await action(cancellationToken);
        }
        catch (Exception ex)
        {
            //  Проверка токена отмены.
            if (!cancellationToken.IsCancellationRequested)
            {
                //  Вывод сообщения об ошибке в журнал.
                Logger.LogError("{exception}", ex);
            }

            //  Проверка критического исключения.
            if (ex.IsCritical())
            {
                //  Повторный выброс исключения.
                throw;
            }
        }
    }
}
