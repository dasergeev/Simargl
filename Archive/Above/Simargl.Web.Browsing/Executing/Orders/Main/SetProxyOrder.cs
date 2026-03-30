using Simargl.Web.Browsing.Core;
using Simargl.Web.Proxies;

namespace Simargl.Web.Browsing.Executing.Orders.Main;

/// <summary>
/// Представляет предписание установки прокси-сервера.
/// </summary>
/// <param name="proxy">
/// Информация о подключении к прокси-серверу.
/// </param>
public sealed class SetProxyOrder(WebProxyInfo? proxy) :
    WebPageRequest<bool>
{
    /// <summary>
    /// Возвращает информацию о подключении к прокси-серверу.
    /// </summary>
    public WebProxyInfo? WebProxyInfo = proxy;

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
        return await controller.WebView.SetProxyAsync(WebProxyInfo, cancellationToken).ConfigureAwait(false);
    }
}
