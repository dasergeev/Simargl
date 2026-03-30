using Simargl.Concurrent;
using Simargl.Engine;
using Simargl.Web.Browsing.Core;
using Simargl.Web.Browsing.Executing.Orders;
using Simargl.Web.Browsing.Executing.Orders.Utilities;
using System.Collections.Concurrent;

namespace Simargl.Web.Browsing.Executing;

/// <summary>
/// Представляет исполнителя веб-страницы.
/// </summary>
public sealed class WebPageExecutor :
    Something
{
    /// <summary>
    /// Поле для хранения очереди предписаний.
    /// </summary>
    private readonly ConcurrentQueue<WebPageOrder> _Orders;

    /// <summary>
    /// Поле для хранения критического объекта для текущего состояния.
    /// </summary>
    private readonly AsyncLock _CurrentLock;

    /// <summary>
    /// Поле для хранения источника текущего токена отмены.
    /// </summary>
    private CancellationTokenSource _CurrentTokenSource;

    /// <summary>
    /// Поле для хранения текущего источника завершения задачи.
    /// </summary>
    private TaskCompletionSource? _CurrentCompletionSource;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    internal WebPageExecutor()
    {
        //  Создание очереди предписаний.
        _Orders = [];

        //  Создание критического объекта.
        _CurrentLock = new();

        //  Создание источника текущего токена отмены.
        _CurrentTokenSource = new();

        //  Установка текущего источника завершения задачи.
        _CurrentCompletionSource = null;
    }

    /// <summary>
    /// Добавляет предписание.
    /// </summary>
    /// <param name="order">
    /// Новое предписание.
    /// </param>
    public void Add(WebPageOrder order)
    {
        //  Проверка ссылки.
        IsNotNull(order);

        //  Добавление в очередь.
        _Orders.Enqueue(order);
    }

    /// <summary>
    /// Добавляет предписания.
    /// </summary>
    /// <param name="action">
    /// Действие, добавляющее предписание.
    /// </param>
    /// <returns>
    /// Последнее предписание.
    /// </returns>
    public WebPageAction Add(Action<WebPageOrderBuilder> action)
    {
        //  Проверка действия.
        IsNotNull(action);

        //  Создание построителя предписаний.
        WebPageOrderBuilder builder = new(this);

        //  Добавление предписаний.
        action(builder);

        //  Создание пустого предписания.
        EmptyOrder empty = new();

        //  Добавление пустого предписания.
        Add(empty);

        //  Возврат пустого предписания.
        return empty;
    }

    /// <summary>
    /// Асинхронно отменяет текущие предписания.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, отменяющая текущие предписания.
    /// </returns>
    public async Task CancelAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Блокировка критического объекта.
        using (await _CurrentLock.LockAsync(cancellationToken).ConfigureAwait(false))
        {
            //  Блок с гарантированным завершением.
            try
            {
                //  Отправка запроса на отмену.
                Volatile.Read(ref _CurrentTokenSource).Cancel();

                //  Создание источника завершения задачи.
                TaskCompletionSource completion = new();

                //  Установка текущего источника завершения задачи.
                Volatile.Write(ref _CurrentCompletionSource, completion);

                //  Ожидание завершения.
                await completion.Task.ConfigureAwait(false);
            }
            finally
            {
                //  Подавление некритических исключений.
                DefyCritical(delegate
                {
                    //  Замена источника завершения задачи.
                    if (Interlocked.Exchange(ref _CurrentTokenSource, new()) is CancellationTokenSource source)
                    {
                        //  Разрушение источника токена отмены.
                        DefyCritical(source.Dispose);
                    }
                });

                //  Подавление некритических исключений.
                DefyCritical(delegate
                {
                    //  Сброс текущего источника завершения задачи.
                    Volatile.Write(ref _CurrentCompletionSource, null);
                });
            }
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу исполнителя.
    /// </summary>
    /// <param name="controller">
    /// Контроллер веб-страницы.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу исполнителя.
    /// </returns>
    internal async Task InvokeAsync(WebPageController controller, CancellationToken cancellationToken)
    {
        //  Основной цикл выполнения.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Извлечение предписаний из очереди.
            while (_Orders.TryDequeue(out WebPageOrder? order))
            {
                //  Проверка ссылки на предписание.
                if (order is not null)
                {
                    //  Установка значения, определяющего выполняются ли предписания.
                    await controller.WebPage.Info.SetIsExecutedAsync(true).ConfigureAwait(false);

                    //  Подавление всех некритических исключений.
                    await DefyCriticalAsync(async delegate (CancellationToken _)
                    {
                        //  Связанный токен отмены.
                        CancellationToken linkedToken = new(true);

                        //  Подавление всех некритических исключений.
                        DefyCritical(delegate
                        {
                            //  Создание связанного источника токена отмены.
                            CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                                Volatile.Read(ref _CurrentTokenSource).Token, cancellationToken);

                            //  Замена связанного токена отмены.
                            linkedToken = linkedTokenSource.Token;
                        });

                        //  Выполнение предписания.
                        await order.ExecutionAsync(controller, linkedToken).ConfigureAwait(false);
                    }, CancellationToken.None).ConfigureAwait(false);
                }
            }

            //  Получение текущего источника задачи.
            if (Volatile.Read(ref _CurrentCompletionSource) is TaskCompletionSource completion)
            {
                //  Попытка завершить задачу.
                completion.TrySetResult();
            }

            //  Установка значения, определяющего выполняются ли предписания.
            await controller.WebPage.Info.SetIsExecutedAsync(false).ConfigureAwait(false);

            //  Ожидание перед следующим проходом.
            await Task.Delay(10, cancellationToken).ConfigureAwait(false);
        }
    }
}
