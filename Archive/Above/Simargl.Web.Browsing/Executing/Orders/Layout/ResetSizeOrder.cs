using Simargl.Web.Browsing.Core;

namespace Simargl.Web.Browsing.Executing.Orders.Layout;

/// <summary>
/// Представляет предписание сбрасывающее размер.
/// </summary>
public sealed class ResetSizeOrder :
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
        //  Выполнение предписания.
        await controller.WebShell.ResetSizeAsync(cancellationToken).ConfigureAwait(false);
    }
}
