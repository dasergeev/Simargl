using System.Collections.Concurrent;
using System.Runtime.Versioning;

namespace Simargl.Com;

/// <summary>
/// Представляет планировщик COM-объектов.
/// </summary>
public sealed class ComScheduler :
    IAsyncDisposable
{
    /// <summary>
    /// Поле для хранения очереди действий.
    /// </summary>
    private BlockingCollection<ComAction>? _Actions;

    /// <summary>
    /// Поле для хранения источника завершения задачи.
    /// </summary>
    private readonly TaskCompletionSource _TaskCompletionSource;

    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private CancellationTokenSource? _CancellationTokenSource;

    /// <summary>
    /// Поле для хранения токена отмены.
    /// </summary>
    private readonly CancellationToken _CancellationToken;

    /// <summary>
    /// Поле для хранения потока.
    /// </summary>
    private readonly Thread _Thread;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public ComScheduler()
    {
        //  Создание очереди действий.
        _Actions = [];

        //  Создание источника завершения задачи.
        _TaskCompletionSource = new();

        //  Создание источника токена отмены.
        _CancellationTokenSource = new();

        //  Установка токена отмены.
        _CancellationToken = _CancellationTokenSource.Token;

        //  Создание потока.
        _Thread = new(Entry)
        {
            //  Установка основного потока.
            IsBackground = false,
        };

        //  Настройка STA-потока.
        _Thread.SetApartmentState(ApartmentState.STA);

        //  Запуск потока.
        _Thread.Start();
    }

    /// <summary>
    /// Точка входа потока.
    /// </summary>
    private void Entry()
    {
        //  Блок с гарантированных завершением.
        try
        {
            //  Получение очереди действий.
            if (Volatile.Read(ref _Actions) is BlockingCollection<ComAction> actions)
            {
                //  Подавление всех некритических исключений.
                DefyCritical(delegate
                {
                    //  Извлечение действий.
                    foreach (ComAction action in actions.GetConsumingEnumerable(_CancellationToken))
                    {
                        //  Выполнение действия.
                        DefyCritical(action.Invoke);
                    }
                });
            }

            //  Сброс коллекции действий.
            Dispose(Interlocked.Exchange(ref _Actions, null));
        }
        finally
        {
            //  Завершение задачи ожидания.
            _TaskCompletionSource.TrySetResult();
        }
    }

    /// <summary>
    /// Разрушает коллекцию действий.
    /// </summary>
    /// <param name="actions">
    /// Коллекция действий.
    /// </param>
    private void Dispose(BlockingCollection<ComAction>? actions)
    {
        //  Проверка коллекции.
        if (actions is null) return;

        //  Перебор элементов в коллеции.
        foreach (ComAction action in actions)
        {

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
        //  Получение источника токена отмены.
        if (Interlocked.Exchange(ref _CancellationTokenSource, null) is CancellationTokenSource cancellationTokenSource)
        {
            //  Отправка запроса на отмену.
            DefyCritical(cancellationTokenSource.Cancel);

            //  Разрушение источника токена отмены.
            DefyCritical(cancellationTokenSource.Dispose);
        }

        //  Ожидание завершения потока.
        await _TaskCompletionSource.Task.ConfigureAwait(false);
    }
}
