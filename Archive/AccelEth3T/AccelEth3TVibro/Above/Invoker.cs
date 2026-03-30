using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет средство вызова методов в основном потоке.
/// </summary>
public sealed class Invoker
{
    /// <summary>
    /// Поле для хранения таймера.
    /// </summary>
    private readonly Timer _Timer;

    /// <summary>
    /// Поле для хранения очереди действий.
    /// </summary>
    private readonly ConcurrentQueue<Action> _Actions;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="timer">
    /// Таймер.
    /// </param>
    public Invoker(Timer timer)
    {
        //  Установка таймера.
        _Timer = timer;

        //  Настройка таймера.
        _Timer.Interval = 100;

        //  Установка обработчика события таймера.
        _Timer.Tick += Timer_Tick;

        //  Создание очереди действий.
        _Actions = [];

        //  Запуск таймера.
        _Timer.Start();
    }

    /// <summary>
    /// Помещает действие в очередь для выполнения в основном потоке.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в основном потоке.
    /// </param>
    public void Enqueue(Action action)
    {
        //  Добавления действия в очередь.
        _Actions.Enqueue(action);
    }

    /// <summary>
    /// Асинхронно выполняет действие в основном потоке.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в основном потоке.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая действие в основном потоке.
    /// </returns>
    public async Task InvokeAsync(Action action, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание источника токена отмены.
        using CancellationTokenSource tokenSource = new();

        //  Создание связанного источника токена отмены.
        using CancellationTokenSource linkedSource = CancellationTokenSource.CreateLinkedTokenSource(
            tokenSource.Token, cancellationToken);

        //  Создание источника завершения задачи.
        TaskCompletionSource completionSource = new();

        //  Запуск задачи отслеживания токена отмены.
        _ = Task.Run(async delegate
        {
            //  Ожидание отмены.
            await Task.Delay(Timeout.Infinite, linkedSource.Token).ConfigureAwait(false);

            //  Завершение задачи.
            completionSource.SetException(new OperationCanceledException());
        }, cancellationToken).ConfigureAwait(false);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Добавление задачи на очередь выполнения в основном потоке.
        Enqueue(delegate
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Выполнение действия.
                    action.Invoke();

                    //  Завершение задачи.
                    completionSource.SetResult();
                }
                else
                {
                    //  Завершение задачи.
                    completionSource.SetException(new OperationCanceledException());
                }
            }
            catch (Exception ex)
            {
                //  Завершение задачи.
                completionSource.SetException(ex);
            }
        });

        //  Ожидание завершения задачи.
        await completionSource.Task.ConfigureAwait(false);
    }

    /// <summary>
    /// Обрабатывает событие таймера.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Timer_Tick(object? sender, EventArgs e)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Извлечение действий из очереди.
            while (_Actions.TryDequeue(out Action? action))
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Выполнение действия.
                    action?.Invoke();
                }
                catch { }
            }
        } catch { }
    }
}
