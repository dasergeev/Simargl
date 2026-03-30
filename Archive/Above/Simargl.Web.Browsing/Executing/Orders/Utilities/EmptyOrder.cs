using Simargl.Web.Browsing.Core;

namespace Simargl.Web.Browsing.Executing.Orders.Utilities;

/// <summary>
/// Представляет пустое предписание.
/// </summary>
public sealed class EmptyOrder :
    WebPageAction
{
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
    internal override async Task InvokeAsync(WebPageController controller, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);
    }
}
