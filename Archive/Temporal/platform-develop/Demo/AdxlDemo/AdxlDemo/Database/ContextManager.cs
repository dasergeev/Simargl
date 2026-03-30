using Apeiron.Threading;

namespace Apeiron.Platform.Demo.AdxlDemo.Database;

/// <summary>
/// Представляет управляющего контекстом базы данных.
/// </summary>
public sealed class ContextManager :
    IDisposable
{
    /// <summary>
    /// Поле для хранения контекста базы данных.
    /// </summary>
    private readonly Context _Context;

    /// <summary>
    /// Поле для хранения примитива асинхронной блокировки.
    /// </summary>
    private readonly AsyncLock _Lock;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public ContextManager()
    {
        //  Создание контекста базы данных.
        _Context = new();

        //  Создание примитива асинхронной блокировки.
        _Lock = new();
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    public void Dispose()
    {
        //  Разрушение контекста базы данных.
        _Context.Dispose();
    }

    /// <summary>
    /// Выполняет запрос к базе данных.
    /// </summary>
    /// <typeparam name="T">
    /// Тип результата.
    /// </typeparam>
    /// <param name="action">
    /// Действие, выполняющее запрос.
    /// </param>
    /// <returns>
    /// Результат запроса.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    [CLSCompliant(false)]
    public T Request<T>(Func<Context, T> action)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Результат запроса.
        T result = default!;

        //  Флаг получения результата.
        bool isResult = false;

        //  Асинхронное выполнение.
        _ = Task.Run(async delegate
        {
            //  Блокировка критического объекта.
            using AsyncLocking locking = await _Lock.LockAsync().ConfigureAwait(false);

            //  Выполнение запроса.
            result = action(_Context);

            //  Установка флага результата.
            isResult = true;
        });

        //  Создание объекта ожидания.
        SpinWait spinWait = new();

        //  Ожидание завершения запроса.
        while (!isResult)
        {
            //  Выполнение одной прокрутки.
            spinWait.SpinOnce();
        }

        //  Возврат результата.
        return result;
    }

    /// <summary>
    /// Выполняет транзакцию в базу данных.
    /// </summary>
    /// <param name="action">
    /// Действие, выполняющее транзакцию.
    /// </param>
    /// <returns>
    /// Задача, выполняющая транзакцию.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    [CLSCompliant(false)]
    public void Transaction(Action<Context> action)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Флаг получения результата.
        bool isResult = false;

        //  Асинхронное выполнение.
        _ = Task.Run(async delegate
        {
            //  Блокировка критического объекта.
            using AsyncLocking locking = await _Lock.LockAsync().ConfigureAwait(false);

            //  Начало транзакции.
            using var transaction = _Context.Database.BeginTransaction();

            //  Внесение изменений.
            action(_Context);

            //  Сохранение изменений в базу данных.
            await _Context.SaveChangesAsync().ConfigureAwait(false);

            //  Фиксирование изменений.
            transaction.Commit();

            //  Установка флага результата.
            isResult = true;
        });

        //  Создание объекта ожидания.
        SpinWait spinWait = new();

        //  Ожидание завершения запроса.
        while (!isResult)
        {
            //  Выполнение одной прокрутки.
            spinWait.SpinOnce();
        }
    }

    /// <summary>
    /// Асинхронно выполняет запрос к базе данных.
    /// </summary>
    /// <typeparam name="T">
    /// Тип результата.
    /// </typeparam>
    /// <param name="action">
    /// Действие, выполняющее запрос.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запрос к базе данных.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [CLSCompliant(false)]
    public async Task<T> RequestAsync<T>(
        Func<Context, CancellationToken, Task<T>> action,
        CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блокировка критического объекта.
        using AsyncLocking locking = await _Lock.LockAsync(cancellationToken).ConfigureAwait(false);

        //  Выполнение запроса.
        return await action(_Context, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет транзакцию в базу данных.
    /// </summary>
    /// <param name="action">
    /// Действие, выполняющее транзакцию.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая транзакцию.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    [CLSCompliant(false)]
    public async Task TransactionAsync(
        Func<Context, CancellationToken, Task> action,
        CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блокировка критического объекта.
        using AsyncLocking locking = await _Lock.LockAsync(cancellationToken).ConfigureAwait(false);

        //  Начало транзакции.
        using var transaction = _Context.Database.BeginTransaction();

        //  Внесение изменений.
        await action(_Context, cancellationToken).ConfigureAwait(false);

        //  Сохранение изменений в базу данных.
        await _Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        //  Фиксирование изменений.
        transaction.Commit();
    }
}
