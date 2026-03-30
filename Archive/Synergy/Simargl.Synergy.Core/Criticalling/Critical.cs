using System.Collections.Concurrent;

namespace Simargl.Synergy.Core.Criticalling;

/// <summary>
/// Представляет критический объект.
/// </summary>
internal class Critical :
    Anything,
    IAsyncDisposable
{
    /// <summary>
    /// Поле для хранения очереди методов разрушения.
    /// </summary>
    private ConcurrentQueue<AsyncDestroyer>? _Destroyers;

    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private CancellationTokenSource? _CancellationTokenSource;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Critical()
    {
        //  Создание очереди методов разрушения.
        _Destroyers = [];

        //  Создание источника токена отмены.
        _CancellationTokenSource = new();

        //  Установка токена отмены.
        CancellationToken = _CancellationTokenSource.Token;
    }

    /// <summary>
    /// Возвращает токен отмены.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// Асинхронно добавляет метод разрушения.
    /// </summary>
    /// <param name="destroyer">
    /// Метод разрушения.
    /// </param>
    /// <returns>
    /// Задача, добавляющая метод разрушения.
    /// </returns>
    public async ValueTask AddDestroyerAsync(AsyncDestroyer destroyer)
    {
        //  Чтение очереди методов разрушения.
        ConcurrentQueue<AsyncDestroyer>? destroyers = Volatile.Read(ref _Destroyers);

        //  Проверка очереди методов разрушения.
        if (destroyers is not null)
        {
            //  Добавление метода в очередь.
            destroyers.Enqueue(destroyer);

            //  Проверка очереди методов разрушения.
            if (Volatile.Read(ref _Destroyers) is null)
            {
                //  Разрушение очереди методов разрушения.
                await DestroyAsync(destroyers).ConfigureAwait(false);
            }
        }
        else
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Вызов метода разрушения.
                await destroyer().ConfigureAwait(false);
            }
            catch { }
        }
    }

    /// <summary>
    /// Асинхронно разрушает объект.
    /// </summary>
    /// <returns>
    /// Задача, разрушающая объект.
    /// </returns>
    public async ValueTask DisposeAsync()
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Извлечение источника токена отмены.
            CancellationTokenSource? cancellationTokenSource = Interlocked.Exchange(ref _CancellationTokenSource, null);

            //  Проверка источника токена отмены.
            if (cancellationTokenSource is not null)
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Отправка запроса на отмены.
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
        catch { }

        //  Блок перехвата всех исключений.
        try
        {
            //  Извлечение очереди методов разрушения.
            ConcurrentQueue<AsyncDestroyer>? destroyers = 
                Interlocked.Exchange(ref _Destroyers, null);

            //  Проверка очереди методов разрушения.
            if (destroyers is not null)
            {
                //  Разрушение.
                await DestroyAsync(destroyers).ConfigureAwait(false);
            }
        }
        catch { }
    }

    /// <summary>
    /// Асинхронно выполняет разрушение.
    /// </summary>
    /// <param name="destroyers">
    /// Очередь методов разрушения.
    /// </param>
    /// <returns>
    /// Задача, выполняющая разрушение.
    /// </returns>
    private static async ValueTask DestroyAsync(ConcurrentQueue<AsyncDestroyer> destroyers)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Извлечение методов разрушения из очереди.
            while (destroyers.TryDequeue(out AsyncDestroyer? destroyer))
            {
                //  Проверка метода разрушения.
                if (destroyer is not null)
                {
                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Вызов метода разрушения.
                        await destroyer().ConfigureAwait(false);
                    }
                    catch { }
                }
            }
        }
        catch { }
    }
}


/*






    /// <summary>
    /// Асинхронно добавляет метод разрушения.
    /// </summary>
    /// <param name="destroyer">
    /// Метод разрушения.
    /// </param>
    /// <returns>
    /// Задача, добавляющая метод разрушения.
    /// </returns>
    public async ValueTask AddDestroyerAsync(Func<Task> destroyer)
    {
        //  Добавление асинхронного метода.
        await AddDestroyerAsync(new Func<ValueTask>(
            async delegate
            {
                //  Вызов метода разрушения.
                await destroyer().ConfigureAwait(false);
            }));
    }

    /// <summary>
    /// Асинхронно добавляет метод разрушения.
    /// </summary>
    /// <param name="destroyer">
    /// Метод разрушения.
    /// </param>
    /// <returns>
    /// Задача, добавляющая метод разрушения.
    /// </returns>
    public async ValueTask AddDestroyerAsync(Action destroyer)
    {
        //  Добавление асинхронного метода.
        await AddDestroyerAsync(new Func<ValueTask>(
            async delegate
            {
                //  Ожидание завершённой задачи.
                await ValueTask.CompletedTask;

                //  Вызов метода разрушения.
                destroyer();
            }));
    }



*/