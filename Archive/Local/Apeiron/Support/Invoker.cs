using Apeiron.Threading;

namespace Apeiron.Support;

/// <summary>
/// Предоставляет вспомогательные методы для вызова методов.
/// </summary>
public static class Invoker
{
    /// <summary>
    /// Асинхронно поддерживает выполнение действия.
    /// </summary>
    /// <param name="action">
    /// Асинхронное действие.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поддержку действия.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task KeepAsync(AsyncAction action, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        action = IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Цикл для отслеживания задачи.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Безопасный вызов действия.
            await SafeNotCriticalAsync(action, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет действие, перехватывая несистемные исключения.
    /// </summary>
    /// <param name="action">
    /// Асинхронное действие, которое необходимо выполнить безопасно.
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
    public static async Task SafeNotSystemAsync(AsyncAction action, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        action = IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блок перехвата несистемных исключений.
        try
        {
            //  Вызов действия.
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
        }
    }

    /// <summary>
    /// Асинхронно выполняет действие, перехватывая некритические исключения.
    /// </summary>
    /// <param name="action">
    /// Асинхронное действие, которое необходимо выполнить безопасно.
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
    public static async Task SafeNotCriticalAsync(AsyncAction action, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на действие.
        action = IsNotNull(action, nameof(action));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блок перехвата некритических исключений.
        try
        {
            //  Вызов действия.
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
        }
    }

    /// <summary>
    /// Безопасно выполняет действие, перехватывая несистемные исключения.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить безопасно.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public static void SafeNotSystem(Action action)
    {
        //  Проверка ссылки на действие.
        action = IsNotNull(action, nameof(action));

        //  Блок перехвата несистемных исключений.
        try
        {
            //  Выполнение действия.
            action();
        }
        catch (Exception ex)
        {
            //  Проверка системного исключения.
            if (ex.IsSystem())
            {
                //  Повторный выброс исключения.
                throw;
            }
        }
    }

    /// <summary>
    /// Безопасно выполняет действие, перехватывая некритические исключения.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить безопасно.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public static void SafeNotCritical(Action action)
    {
        //  Проверка ссылки на действие.
        action = IsNotNull(action, nameof(action));

        //  Блок перехвата некритических исключений.
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
        }
    }
}
