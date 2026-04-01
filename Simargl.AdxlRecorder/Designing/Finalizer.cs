namespace Simargl.AdxlRecorder.Designing;

/// <summary>
/// Представляет финализатор.
/// </summary>
public sealed class Finalizer :
    IAsyncDisposable
{
    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private CancellationTokenSource? _CancellationTokenSource;

    /// <summary>
    /// Поле для хранения очереди методов разрушения.
    /// </summary>
    private ConcurrentQueue<Destroyer>? _Destroyers;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="cancellationTokenSource">
    /// Источник токена отмены.
    /// </param>
    private Finalizer(CancellationTokenSource cancellationTokenSource)
    {
        //  Установка источника токена отмены.
        _CancellationTokenSource = cancellationTokenSource;

        //  Создание очереди методов разрушения.
        _Destroyers = [];

        //  Установка токена отмены.
        CancellationToken = _CancellationTokenSource.Token;
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Finalizer() :
        this(new CancellationTokenSource())
    {
        
    }

    /// <summary>
    /// Возвращает токен отмены.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// Асинхронно регистриует метод завершения.
    /// </summary>
    /// <param name="destroyer">
    /// Метод завершения.
    /// </param>
    /// <returns>
    /// Задача, регистрирующая метод завершения.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="destroyer"/> передана пустая ссылка.
    /// </exception>
    public async ValueTask RegisterAsync(Destroyer destroyer)
    {
        //  Проверка метода завершения.
        IsNotNull(destroyer);

        //  Получение очереди методов разрушения.
        if (Volatile.Read(ref _Destroyers) is not ConcurrentQueue<Destroyer> destroyers)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Выполнение метода разрушения.
                await destroyer().ConfigureAwait(false);
            }
            catch { }

            //  Завершение регистрации.
            return;
        }

        //  Добавление метода разрушения.
        destroyers.Enqueue(destroyer);

        //  Проверка очереди методов разрушения.
        if (Volatile.Read(ref _Destroyers) is null)
        {
            //  Разрушение объекта.
            await DisposeAsync(destroyers).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно разрушает объект.
    /// </summary>
    /// <returns>
    /// Задача, выполняющая разрушение объекта.
    /// </returns>
    public async ValueTask DisposeAsync()
    {
        //  Получение очереди методов разрушения.
        if (Interlocked.Exchange(ref _Destroyers, null) is ConcurrentQueue<Destroyer> destroyers)
        {
            //  Разрушение объекта.
            await DisposeAsync(destroyers).ConfigureAwait(false);
        }

        //  Получение источника токена отмены.
        if (Interlocked.Exchange(ref _CancellationTokenSource, null) is CancellationTokenSource cancellationTokenSource)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Отправка запроса на завершение задач.
                await cancellationTokenSource.CancelAsync().ConfigureAwait(false);
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

    /// <summary>
    /// Асинхронно разрушает объект.
    /// </summary>
    /// <param name="destroyers">
    /// Очередь методов разрушения.
    /// </param>
    /// <returns>
    /// Задача, разрушающая объект.
    /// </returns>
    private static async ValueTask DisposeAsync(ConcurrentQueue<Destroyer> destroyers)
    {
        //  Извлечение методов разрушения из очереди.
        while (destroyers.TryDequeue(out Destroyer? destroyer))
        {
            //  Проверка метода разрушения.
            if (destroyer is not null)
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Выполнение метода разрушения.
                    await destroyer().ConfigureAwait(false);
                }
                catch { }
            }
        }
    }
}
