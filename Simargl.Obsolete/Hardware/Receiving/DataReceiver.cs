namespace Simargl.Hardware.Receiving;

/// <summary>
/// Представляет приёмник данных.
/// </summary>
public abstract class DataReceiver :
    IAsyncDisposable
{
    /// <summary>
    /// Происходит при запуске средства приёма данных.
    /// </summary>
    public event EventHandler? Started;

    /// <summary>
    /// Происходит при остановке средства приёма данных.
    /// </summary>
    public event EventHandler? Stopped;

    /// <summary>
    /// Происходит при получении данных.
    /// </summary>
    public event DataReceiverReceivedEventHandler? Received;

    /// <summary>
    /// Происходит при внутренней ошибке.
    /// </summary>
    public event FailedEventHandler? Failed;

    /// <summary>
    /// Поле для хранения основного токена отмены.
    /// </summary>
    private readonly CancellationToken _CancellationToken;

    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private CancellationTokenSource? _CancellationTokenSource;

    /// <summary>
    /// Поле для хранения источника завершения задачи ожидания начала работы.
    /// </summary>
    private readonly TaskCompletionSource _StartSource;

    /// <summary>
    /// Поле для хранения задачи.
    /// </summary>
    private readonly Task _Task;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public DataReceiver(CancellationToken cancellationToken)
    {
        //  Установка основного токена отмены.
        _CancellationToken = cancellationToken;

        //  Создание источника токена отмены.
        _CancellationTokenSource = new();

        //  Создание источника завершения задачи ожидания начала работы.
        _StartSource = new();

        //  Создание задачи.
        _Task = Task.Run(ExecuteAsync, CancellationToken.None);
    }

    /// <summary>
    /// Запускает выполнение работы.
    /// </summary>
    public void Start()
    {
        //  Запуск работы.
        _StartSource.TrySetResult();
    }

    /// <summary>
    /// Асинхронно разрушает объект.
    /// </summary>
    /// <returns>
    /// Задача, разрушающая объект.
    /// </returns>
    public async ValueTask DisposeAsync()
    {
        //  Разрушение объекта.
        DisposeCore();

        //  Сообщение сборщику мусора.
        GC.SuppressFinalize(this);

        //  Ожидание завершения основной задачи.
        await _Task.ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токена отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected abstract Task InvokeAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Вызывает событие <see cref="Started"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnStarted(EventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref Started)?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="Stopped"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnStopped(EventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref Stopped)?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="Received"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnReceived(DataReceiverReceivedEventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref Received)?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="Failed"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnFailed(FailedEventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref Failed)?.Invoke(this, e);
    }

    /// <summary>
    /// Асинхронно выполняет работу.
    /// </summary>
    /// <returns>
    /// Задача, выполняющая работу.
    /// </returns>
    private async Task ExecuteAsync()
    {
        //  Ожидание начала работы.
        await _StartSource.Task.ConfigureAwait(false);

        //  Блок перехвата всех исключений.
        try
        {
            //  Вызов события начала приёма данных.
            OnStarted(EventArgs.Empty);

            //  Проверка основного источника токена отмены.
            if (Volatile.Read(ref _CancellationTokenSource) is CancellationTokenSource cancellationTokenSource)
            {
                //  Создание связанного источника токена отмены.
                CancellationTokenSource linkedTokenSource =
                    CancellationTokenSource.CreateLinkedTokenSource(
                        _CancellationToken,
                        cancellationTokenSource.Token);

                //  Получение токена отмены.
                CancellationToken cancellationToken = linkedTokenSource.Token;

                //  Регистрация метода разрушения объекта.
                cancellationToken.Register(DisposeCore);

                //  Основной цикл поддержки.
                while (!cancellationToken.IsCancellationRequested)
                {
                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Выполнение основной работы.
                        await InvokeAsync(cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        //  Проверка токена отмены.
                        if (!cancellationToken.IsCancellationRequested)
                        {
                            //  Создание аргументов события.
                            FailedEventArgs e = new(ex);

                            //  Вызов события.
                            OnFailed(e);
                        }
                    }

                    //  Ожидание перед следующим проходом.
                    await Task.Delay(100, cancellationToken).ConfigureAwait(false);
                }
            }
        }
        catch { }

        //  Запуск асинхронной задачи.
        _ = Task.Run(delegate
        {
            //  Вызов события завершения приёма данных.
            OnStopped(EventArgs.Empty);
        }, CancellationToken.None);

        //  Разрушение объекта.
        DisposeCore();
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    private void DisposeCore()
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Установка состояния запуска.
            _StartSource.TrySetResult();
        }
        catch { }

        //  Проверка источника токена отмены.
        if (Interlocked.Exchange(ref _CancellationTokenSource, null) is CancellationTokenSource cancellationTokenSource)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Отправка запроса на отмену задачи.
                cancellationTokenSource.Cancel();
            }
            catch { }

            //  Блок перехвата всех исключений.
            try
            {
                //  Разрушение источника токена отмены.
                cancellationTokenSource.Dispose();
            }
            catch { }
        }
    }
}
