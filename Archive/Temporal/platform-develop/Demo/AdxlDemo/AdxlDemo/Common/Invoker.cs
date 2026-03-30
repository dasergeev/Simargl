using Apeiron.Threading;
using System.Collections.Concurrent;
using System.Windows.Threading;

namespace Apeiron.Platform.Demo.AdxlDemo;

/// <summary>
/// Представляет средство вызова методов.
/// </summary>
public sealed class Invoker :
    Element
{
    /// <summary>
    /// Поле для хранения очереди действий.
    /// </summary>
    private readonly ConcurrentQueue<Action> _Actions;

    /// <summary>
    /// Поле для хранения таймера.
    /// </summary>
    private readonly DispatcherTimer _Timer;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    public Invoker(Engine engine) :
        base(engine)
    {
        //  Создание очереди действий.
        _Actions = new();

        //  Установка диспетчера основого потока.
        Dispatcher = Application.Dispatcher;

        //  Создание таймера.
        _Timer = new(DispatcherPriority.Normal, Dispatcher)
        {
            Interval = Settings.InvokerInterval,
        };

        //  Добавление обработчика события таймера.
        _Timer.Tick += Timer_Tick;

        //  Добавление обработчика запуска приложения.
        Application.Startup += (sender, e) =>
        {
            //  Запуск таймера.
            _Timer.Start();
        };

        //  Добавление обработчика события завершения приложения.
        Application.Exit += (sender, e) =>
        {
            //  Остановка таймера.
            _Timer.Stop();
        };
    }

    /// <summary>
    /// Возвращает диспетчера основного потока.
    /// </summary>
    public Dispatcher Dispatcher { get; }

    /// <summary>
    /// Добавляет действие в очередь выполнения без ожидания.
    /// </summary>
    /// <param name="action">
    /// Дейсвтие, которое необходимо добавить в очередь.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public void AddAction(Action action)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Добавление действия в очередь.
        _Actions.Enqueue(action);
    }

    /// <summary>
    /// Асинхронно добавляет действие в очередь выполнения без ожидания.
    /// </summary>
    /// <param name="action">
    /// Дейсвтие, которое необходимо добавить в очередь.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, добавляющее действие в очередь выполнения без ожидания.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task AddActionAsync(Action action, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Асинхронное выполнение.
        await Task.Run(delegate
        {
            //  Добавление действия в очередь.
            _Actions.Enqueue(action);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Безопасно выполняет действие с перехватом всех несистемных исключений.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public void System(Action action)
    {
        System(action, out Exception _);
    }

    /// <summary>
    /// Безопасно выполняет действие с перехватом всех несистемных исключений.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <param name="exception">
    /// Исключение, которое возникло при выполнении действия.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public void System(Action action, out Exception? exception)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Установка пустой ссылки на исключение.
        exception = null;

        //  Блок перехвата всех некритических исключений.
        try
        {
            //  Выполнение действия.
            action();
        }
        catch (Exception ex)
        {
            //  Проверка критического исключения.
            if (ex.IsSystem())
            {
                //  Повторный выброс исключения.
                throw;
            }

            //  Регистрация сообщение в журнале.
            Logger.Log(ex);

            //  Установка исключения.
            exception = ex;
        }
    }

    /// <summary>
    /// Асинхронно безопасно выполняет действие с перехватом всех несистемных исключений.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая дейсвтие.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task SystemAsync(AsyncAction action, CancellationToken cancellationToken)
    {
        await SystemAsync(action, null, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно безопасно выполняет действие с перехватом всех несистемных исключений.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <param name="exceptionAction">
    /// Действие, которое необходимо выполнить в случае возникновения исключения.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая дейсвтие.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task SystemAsync(AsyncAction action, AsyncAction<Exception>? exceptionAction, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блок перехвата всех несистемных исключений.
        try
        {
            //  Выполнение действия.
            await action(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            //  Проверка системного исключения.
            if (ex.IsSystem())
            {
                //  Повторный выброс исключения.
                throw;
            }

            //  Регистрация сообщение в журнале.
            await Logger.LogAsync(ex, cancellationToken).ConfigureAwait(false);

            //  Проверка ссылки на действие, которое необходимо выполнить при исключении.
            if (exceptionAction is not null)
            {
                //  Выполнение действия.
                await exceptionAction(ex, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Безопасно выполняет действие с перехватом всех некритических исключений.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public void Critical(Action action)
    {
        Critical(action, out Exception _);
    }

    /// <summary>
    /// Безопасно выполняет действие с перехватом всех некритических исключений.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <param name="exception">
    /// Исключение, которое возникло при выполнении действия.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public void Critical(Action action, out Exception? exception)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Установка пустой ссылки на исключение.
        exception = null;

        //  Блок перехвата всех некритических исключений.
        try
        {
            //  Выполнение действия.
            action();
        }
        catch (Exception ex)
        {
            //  Проверка критического исключения.
            if (ex.IsCritical())
            {
                //  Повторный выброс исключения.
                throw;
            }

            //  Регистрация сообщение в журнале.
            Logger.Log(ex);

            //  Установка исключения.
            exception = ex;
        }
    }

    /// <summary>
    /// Асинхронно безопасно выполняет действие с перехватом всех некритических исключений.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая дейсвтие.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task CriticalAsync(AsyncAction action, CancellationToken cancellationToken)
    {
        await CriticalAsync(action, null, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно безопасно выполняет действие с перехватом всех некритических исключений.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <param name="exceptionAction">
    /// Действие, которое необходимо выполнить в случае возникновения исключения.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая дейсвтие.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task CriticalAsync(AsyncAction action, AsyncAction<Exception>? exceptionAction, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блок перехвата всех некритических исключений.
        try
        {
            //  Выполнение действия.
            await action(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            //  Проверка критического исключения.
            if (ex.IsCritical())
            {
                //  Повторный выброс исключения.
                throw;
            }

            //  Регистрация сообщение в журнале.
            await Logger.LogAsync(ex, cancellationToken).ConfigureAwait(false);

            //  Проверка ссылки на действие, которое необходимо выполнить при исключении.
            if (exceptionAction is not null)
            {
                //  Выполнение действия.
                await exceptionAction(ex, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Выполняет действие в основном потоке.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в основном потоке.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public void Primary(Action action)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка необходимости добавления действия в очередь.
        if (Dispatcher.Thread.ManagedThreadId == Environment.CurrentManagedThreadId)
        {
            //  Выполнение в текущем потоке.
            action();
        }
        else
        {
            //  Исключение, которое произошло во время выполнения действия.
            Exception? exception = null;

            //  Выполнение в основном потоке.
            Dispatcher.Invoke(delegate
            {
                //  Безопасное выполнение действия.
                Critical(action, out exception);
            });

            //  Проверка исключения.
            if (exception is not null)
            {
                //  Повторный выброс исключения.
                throw exception;
            }
        }
    }

    /// <summary>
    /// Выполняет действие в основном потоке.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата.
    /// </typeparam>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в основном потоке.
    /// </param>
    /// <returns>
    /// Результат действия.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public TResult Primary<TResult>(Func<TResult> action)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Результат действия.
        TResult result = default!;

        //  Выполнение в основном потоке.
        Primary(delegate
        {
            //  Выполнение действия.
            result = action();
        });

        //  Возврат результата действия.
        return result;
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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task PrimaryAsync(Action action, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Асинхронное выполнение.
        await Task.Run(delegate
        {
            //  Выполнение действия в основном потоке.
            Primary(action);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет действие в основном потоке.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата действия.
    /// </typeparam>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в основном потоке.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая действие в основном потоке.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task<TResult> PrimaryAsync<TResult>(Func<TResult> action, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Асинхронное выполение.
        return await Task.Run(delegate
        {
            //  Выполнение действия в основном потоке.
            return Primary(action);
        }, cancellationToken).ConfigureAwait(false);
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
        //  Безопасное выполнение действия.
        Critical(delegate
        {
            //  Извлечение всех действий из очереди.
            while (_Actions.TryDequeue(out Action? action))
            {
                //  Безопасное выполнение действия.
                Critical(action);
            }
        });
    }
}
