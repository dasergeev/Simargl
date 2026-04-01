using Simargl.Concurrent;

namespace Simargl.Performing;

/// <summary>
/// Представляет средство вызова методов.
/// </summary>
/// <param name="sender">
/// Метод, отправляющий действие для выполнения.
/// </param>
/// <param name="cancellationToken">
/// Токен отмены.
/// </param>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="sender"/> передана пустая ссылка.
/// </exception>
public sealed class Invoker(Sender sender, CancellationToken cancellationToken) :
    Performer(cancellationToken)
{
    /// <summary>
    /// Поле для хранения метода, отправляющего действие для выполнения.
    /// </summary>
    private readonly Sender _Sender = IsNotNull(sender);

    /// <summary>
    /// Отправляет действие для выполнения.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public void Send(Action action)
    {
        //  Проверка действия.
        IsNotNull(action);

        //  Отправка действия.
        _Sender(action);
    }

    /// <summary>
    /// Асинхронно выполняет действие.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая действие.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    public async Task InvokeAsync(Action action, CancellationToken cancellationToken)
    {
        //  Проверка действия.
        IsNotNull(action);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Асинхронное выполнение.
        await InvokeAsync(async delegate (CancellationToken cancellation)
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(true);

            //  Выполнение действия.
            action();
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет функцию.
    /// </summary>
    /// <param name="func">
    /// Фнкция, которую необходимо выполнить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая функцию.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="func"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    public async Task<TResult> InvokeAsync<TResult>(Func<TResult> func, CancellationToken cancellationToken)
    {
        //  Проверка функции.
        IsNotNull(func);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Результат выполения.
        TResult result = default!;

        //  Выполнение действия.
        await InvokeAsync(delegate
        {
            //  Выполнение функции.
            result = func();
        }, cancellationToken).ConfigureAwait(false);

        //  Возврат результата.
        return result;
    }

    /// <summary>
    /// Асинхронно выполняет действие.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая действие.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task InvokeAsync(AsyncAction action, CancellationToken cancellationToken)
    {
        //  Проверка действия.
        IsNotNull(action);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание связанного токена отмены.
        using CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            GetCancellationToken(), cancellationToken);

        //  Замена токена отмены.
        cancellationToken = linkedTokenSource.Token;

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание источника завершения задачи.
        TaskCompletionSource taskCompletion = new();

        //  Запуск задачи отслеживания токена отмены.
        _ = Task.Run(async delegate
        {
            //  Блок с гарантированным завершением.
            try
            {
                //  Бесконечное ожидание.
                await Task.Delay(
                    Timeout.InfiniteTimeSpan,
                    cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                //  Попытка отмены задачи.
                taskCompletion.TrySetCanceled();
            }
        }, cancellationToken);

        //  Отправка действия для выполнения в основном потоке.
        Send(delegate
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Получение планировщика задач.
                TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();

                //  Добавление продолжения к завершённой задаче.
                _ = Task.CompletedTask.ContinueWith(async delegate (Task task)
                {
                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Выполнение действия.
                        await action(cancellationToken).ConfigureAwait(false);

                        //  Завершение задачи.
                        taskCompletion.SetResult();
                    }
                    catch (Exception ex)
                    {
                        //  Установка исключения.
                        taskCompletion.SetException(ex);
                    }
                }, scheduler);
            }
            catch (Exception ex)
            {
                //  Установка исключения.
                taskCompletion.SetException(ex);
            }
        });

        //  Ожидание завершения задачи.
        await taskCompletion.Task.ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет функцию.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата функции.
    /// </typeparam>
    /// <param name="func">
    /// Функция, которую необходимо выполнить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая функцию
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="func"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    /// В результате операции произошло обращение к разрушенному объекту.
    /// </exception>
    public async Task<TResult> InvokeAsync<TResult>(AsyncFunc<TResult> func, CancellationToken cancellationToken)
    {
        //  Проверка функции.
        IsNotNull(func);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Результат выполения.
        TResult result = default!;

        //  Выполнение действия в основном потоке.
        await InvokeAsync(async delegate (CancellationToken cancellationToken)
        {
            //  Выполнение функции.
            result = await func(cancellationToken).ConfigureAwait(true);
        }, cancellationToken).ConfigureAwait(false);

        //  Возврат результата.
        return result;
    }
}
