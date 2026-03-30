namespace Apeiron.Platform.Databases.OrioleDatabase;

/// <summary>
/// Представляет управляющего базой данных.
/// </summary>
public static class OrioleDatabaseManager
{
    /// <summary>
    /// Выполняет запрос к базе данных.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата запроса.
    /// </typeparam>
    /// <param name="request">
    /// Запрос.
    /// </param>
    /// <returns>
    /// Результат запроса.
    /// </returns>
    public static TResult Request<TResult>(Func<OrioleDatabaseContext, TResult> request)
    {
        //  Создание контекста сеанса работы с базой данных.
        using OrioleDatabaseContext database = new();

        //  Выполнение запроса.
        return request(database);
    }

    /// <summary>
    /// Асинхронно выполняет запрос к базе данных.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата запроса.
    /// </typeparam>
    /// <param name="request">
    /// Запрос.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запрос к базе данных.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task<TResult> RequestAsync<TResult>(
        Func<OrioleDatabaseContext, CancellationToken, Task<TResult>> request,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание контекста сеанса работы с базой данных.
        await using OrioleDatabaseContext database = new();

        //  Выполнение запроса.
        return await request(database, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет транзакцию с перехватом всех несистемных сиключений.
    /// </summary>
    /// <param name="changer">
    /// Метод, вносящий изменения в базу данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая транзакцию.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task TryTransactionAsync(
        Func<OrioleDatabaseContext, CancellationToken, Task> changer,
        CancellationToken cancellationToken)
    {
        //  Безопасный вызов транзакции.
        await Invoker.SafeNotSystemAsync(async cancellationToken =>
        {
            //  Выполнение транзакции.
            await TransactionAsync(changer, cancellationToken).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет транзакцию.
    /// </summary>
    /// <param name="changer">
    /// Метод, вносящий изменения в базу данных.
    /// </param>
    public static void Transaction(Action<OrioleDatabaseContext> changer)
    {
        //  Создание контекста сеанса работы с базой данных.
        using OrioleDatabaseContext database = new();

        //  Начало транзакции.
        using var transaction = database.Database.BeginTransaction();

        //  Внесение изменений.
        changer(database);

        //  Сохранение изменений в базу данных.
        database.SaveChanges();

        //  Фиксирование изменений.
        transaction.Commit();
    }

    /// <summary>
    /// Асинхронно выполняет транзакцию.
    /// </summary>
    /// <param name="changer">
    /// Метод, вносящий изменения в базу данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая транзакцию.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task TransactionAsync(
        Func<OrioleDatabaseContext, CancellationToken, Task> changer,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание контекста сеанса работы с базой данных.
        await using OrioleDatabaseContext database = new();

        //  Начало транзакции.
        await using var transaction = await database.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

        //  Внесение изменений.
        await changer(database, cancellationToken).ConfigureAwait(false);

        //  Сохранение изменений в базу данных.
        int count = await database.SaveChangesAsync(cancellationToken);

        //  Фиксирование изменений.
        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
    }
}
