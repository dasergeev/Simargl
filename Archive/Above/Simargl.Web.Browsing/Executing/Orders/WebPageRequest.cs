using Simargl.Designing.Utilities;
using Simargl.Web.Browsing.Core;

namespace Simargl.Web.Browsing.Executing.Orders;

/// <summary>
/// Представляет предписание для веб-страницы возвращающее результат.
/// </summary>
/// <typeparam name="TResult">
/// Тип результата.
/// </typeparam>
public abstract class WebPageRequest<TResult> :
    WebPageOrder
{
    /// <summary>
    /// Поле для хранения источника завершения задачи.
    /// </summary>
    private readonly TaskCompletionSource<TResult> _TaskCompletionSource = new();

    /// <summary>
    /// Возвращает задачу, ожидающую результат.
    /// </summary>
    public Task<TResult> Task => _TaskCompletionSource.Task;

    /// <summary>
    /// Асинхронно выполняет предписание.
    /// </summary>
    /// <param name="controller">
    /// Контроллер веб-страницы.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая предписание.
    /// </returns>
    internal abstract Task<TResult> InvokeAsync(WebPageController controller, CancellationToken cancellationToken);

    /// <summary>
    /// Выполняет попытку установить исключение.
    /// </summary>
    /// <param name="exception">
    /// Исключение, которое необходимо установить.
    /// </param>
    internal override sealed void TrySetException(Exception exception)
    {
        //  Попытка установить исключение.
        _TaskCompletionSource.TrySetException(exception);
    }

    /// <summary>
    /// Асинхронно выполняет предписание.
    /// </summary>
    /// <param name="controller">
    /// Контроллер веб-страницы.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая предписание.
    /// </returns>
    internal override sealed async Task ExecutionCoreAsync(WebPageController controller, CancellationToken cancellationToken)
    {
        //  Выполнение предписания.
        TResult result = await InvokeAsync(controller, cancellationToken).ConfigureAwait(false);

        //  Установка результата.
        _TaskCompletionSource.TrySetResult(result);
    }
}
