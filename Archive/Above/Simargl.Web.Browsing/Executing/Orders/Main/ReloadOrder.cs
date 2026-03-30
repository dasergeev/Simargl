using Simargl.Web.Browsing.Core;

namespace Simargl.Web.Browsing.Executing.Orders.Main;

/// <summary>
/// Представляет предписание перезагрузки страницы.
/// </summary>
public sealed class ReloadOrder() :
    WebPageRequest<bool>
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
    internal override async Task<bool> InvokeAsync(WebPageController controller, CancellationToken cancellationToken)
    {
        //  Выполнение предписания.
        return await controller.WebView.ReloadAsync(cancellationToken).ConfigureAwait(false);
    }
}
