using Simargl.Web.Browsing.Core;

namespace Simargl.Web.Browsing.Executing.Orders;

/// <summary>
/// Представляет предписание для веб-страницы не возвращающее результат.
/// </summary>
public abstract class WebPageAction :
    WebPageOrder
{
    /// <summary>
    /// Поле для хранения источника завершения задачи.
    /// </summary>
    private readonly TaskCompletionSource _TaskCompletionSource = new();

    /// <summary>
    /// Возвращает задачу, ожидающую результат.
    /// </summary>
    public Task Task => _TaskCompletionSource.Task;

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
    internal abstract Task InvokeAsync(WebPageController controller, CancellationToken cancellationToken);

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
        await InvokeAsync(controller, cancellationToken).ConfigureAwait(false);

        //  Установка результата.
        _TaskCompletionSource.TrySetResult();
    }
}
