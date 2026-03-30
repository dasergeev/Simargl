using Microsoft.EntityFrameworkCore.Storage;
using Simargl.Concurrent;
using Simargl.Trials.Aurora.Aurora01.Storage;

namespace Simargl.Trials.Aurora.Aurora01.Processor;

/// <summary>
/// Представляет контекст обработки.
/// </summary>
[CLSCompliant(false)]
public readonly struct ProcessorContext //(Aurora01StorageContext storageContext, AsyncLock storageLock)
{
    ///// <summary>
    ///// Возвращает критический объект.
    ///// </summary>
    //public AsyncLock StorageLock { get; } = storageLock;

    /// <summary>
    /// Асинхронно выполняет транзакцию.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая транзакцию.
    /// </returns>
    public async Task TransactionAsync(AsyncAction<Aurora01StorageContext> action, CancellationToken cancellationToken)
    {
        //////  Блокировка критического объекта.
        ////using (await StorageLock.LockAsync(cancellationToken).ConfigureAwait(false))
        ////{

        using Aurora01StorageContext storageContext = new();

            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Начало транзакции.
            using IDbContextTransaction transaction = await storageContext.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

            //  Выполнение действия.
            await action(storageContext, cancellationToken).ConfigureAwait(false);

            //  Сохранение изменений.
            await storageContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            //  Завершение транзакции.
            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        //}
    }

    /// <summary>
    /// Асинхронно выполняет транзакцию.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая транзакцию.
    /// </returns>
    public async Task TransactionAsync(Action<Aurora01StorageContext> action, CancellationToken cancellationToken)
    {
        //  Выполнение транзакции.
        await TransactionAsync(
            async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
            {
                //  Проверка токена отмены.
                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                //  Выполнение действия.
                action(context);
            }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет запрос.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата запроса.
    /// </typeparam>
    /// <param name="action">
    /// Действие, выполняющее запрос.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запрос.
    /// </returns>
    public async Task<TResult> TransactionAsync<TResult>(
        AsyncFunc<Aurora01StorageContext, TResult> action, CancellationToken cancellationToken)
    {
        //  Результат запроса.
        TResult result = default!;

        //  Выполнение транзакции.
        await TransactionAsync(
            async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
            {
                //  Проверка токена отмены.
                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                //  Выполнение действия.
                result = await action(context, cancellationToken).ConfigureAwait(false);
            }, cancellationToken).ConfigureAwait(false);

        //  Возврат результата.
        return result;
    }
}
