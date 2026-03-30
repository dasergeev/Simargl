using Simargl.Concurrent;
using Simargl.Performing;

namespace Simargl.Entities;

/// <summary>
/// Представляет хранилище данных.
/// </summary>
/// <typeparam name="TContext">
/// Тип контекста базы данных.
/// </typeparam>
[CLSCompliant(false)]
public sealed class Storage<TContext> :
    Performer
    where TContext : StorageContext, new()
{
    /// <summary>
    /// Поле для хранения критического объекта.
    /// </summary>
    private readonly AsyncLock _Lock;

    /// <summary>
    /// Поле для хранения контекста.
    /// </summary>
    private readonly TContext _Context;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public Storage(CancellationToken cancellationToken) :
        base(cancellationToken)
    {
        //  Замена токена отмены.
        cancellationToken = GetCancellationToken();

        //  Создание контекста.
        _Context = new();

        //  Создание критического объекта.
        _Lock = new();

        //  Регистрация метода завершения.
        cancellationToken.Register(delegate
        {
            //  Асинхронное выполнение.
            Task.Run(async delegate
            {
                //  Блокировка критического объекта.
                using (await _Lock.LockAsync().ConfigureAwait(false))
                {
                    //  Блок с гарантированным завершением.
                    try
                    {
                        //  Разрушение контекста базы данных.
                        await _Context.DisposeAsync().ConfigureAwait(false);
                    }
                    finally
                    {
                        //  Разрушение критического объекта.
                        _Lock.Dispose();
                    }
                }
            });
        });
    }

    /// <summary>
    /// Асинхронно выполняет действие с контекстом.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить с контекстом.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая действие с контекстом.
    /// </returns>
    public async Task InvokeAsync(AsyncAction<TContext> action, CancellationToken cancellationToken)
    {
        //  Проверка действия.
        IsNotNull(action);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание источника связанного токена отмены.
        using CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            GetCancellationToken(), cancellationToken);

        //  Замена токена отмены.
        cancellationToken = linkedTokenSource.Token;

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Блокировка критического объекта.
        using (await _Lock.LockAsync(cancellationToken).ConfigureAwait(false))
        {
            //  Выполнение действия.
            await action(_Context, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет действие с контекстом.
    /// </summary>
    /// <typeparam name="TResult">
    /// Тип результата.
    /// </typeparam>
    /// <param name="func">
    /// Действие, которое необходимо выполнить с контекстом.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая действие с контекстом.
    /// </returns>
    public async Task<TResult> InvokeAsync<TResult>(AsyncFunc<TContext, TResult> func, CancellationToken cancellationToken)
    {
        //  Результат действия.
        TResult result = default!;

        //  Выполнение действия.
        await InvokeAsync(async delegate(TContext context, CancellationToken cancellationToken)
        {
            //  Получение результата.
            result = await func(context, cancellationToken).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);

        //  Возврат результата.
        return result;
    }
}
