using Simargl.Web.Browsing.Core;

namespace Simargl.Web.Browsing.Executing.Orders.Main;

/// <summary>
/// Представляет предписание направления на новую страницу.
/// </summary>
/// <param name="source">
/// Источник страницы.
/// </param>
public sealed class NavigateOrder(string source) :
    WebPageRequest<bool>
{
    /// <summary>
    /// Возвращает источник страницы.
    /// </summary>
    public string Source { get; } = IsNotEmpty(source);

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
        return await controller.WebView.NavigateAsync(Source, cancellationToken).ConfigureAwait(false);
    }
}
