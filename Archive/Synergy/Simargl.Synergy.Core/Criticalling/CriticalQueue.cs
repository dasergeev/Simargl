using System.Collections.Concurrent;

namespace Simargl.Synergy.Core.Criticalling;

/// <summary>
/// Представляет критическую очередь.
/// </summary>
/// <typeparam name="T">
/// Тип элементов.
/// </typeparam>
internal sealed class CriticalQueue<T> :
    Critical
{
    /// <summary>
    /// Поле для хранения метода разрушения элемента.
    /// </summary>
    private AsyncDestroyer<T> _Destroyer = null!;

    /// <summary>
    /// Поле для хранения потокобезопасной очереди.
    /// </summary>
    private ConcurrentQueue<T>? _Queue;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    private CriticalQueue()
    {

    }

    /// <summary>
    /// Асинхронно создаёт очередь.
    /// </summary>
    /// <param name="destroyer">
    /// Метод разрушения элемента.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая очередь.
    /// </returns>
    public async Task<CriticalQueue<T>> CreateAsync(AsyncDestroyer<T> destroyer, CancellationToken cancellationToken)
    {
        //  Создание очереди.
        CriticalQueue<T> queue = new();

        //  Блок перехвата всех исключений.
        try
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Установка метода разрушения элемента.
            queue._Destroyer = IsNotNull(destroyer);

            //  Создание потокобезопасной очереди.
            queue._Queue = [];

            //  Добавление метода разрушения потокобезопасной очереди.
            await queue.AddDestroyerAsync(async delegate
            {
                //  Извлечение потокобезопасной очереди.
                ConcurrentQueue<T>? coreQueue =
                    Interlocked.Exchange(ref queue._Queue, null);

                //  Проверка очереди.
                if (coreQueue is not null)
                {
                    //  Разрушение.
                    await DestroyAsync(coreQueue).ConfigureAwait(false);
                }
            });

            //  Возврат очереди.
            return queue;
        }
        catch
        {
            //  Разрушение очереди.
            await queue.DisposeAsync().ConfigureAwait(false);

            //  Повторный выброс исключения.
            throw;
        }
    }

    /// <summary>
    /// Асинхронно разрушает очередь.
    /// </summary>
    /// <param name="queue">
    /// Разрушаемая очередь.
    /// </param>
    /// <returns>
    /// Задача, выполняющая разрушение очереди.
    /// </returns>
    private async ValueTask DestroyAsync(ConcurrentQueue<T> queue)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Извлечение элементов из очереди.
            while (queue.TryDequeue(out T? obj))
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Разрушение элемента.
                    await _Destroyer(obj).ConfigureAwait(false);
                }
                catch { }
            }
        }
        catch { }
    }
}
