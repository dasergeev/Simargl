using Microsoft.Web.WebView2.Core;

namespace Simargl.Web.Browsing.Core.Drivers;

/// <summary>
/// Представляет драйвер объекта <see cref="CoreWebView2"/>.
/// </summary>
/// <param name="target">
/// Целевой объект.
/// </param>
internal sealed class CoreWebViewDriver(CoreWebView2 target)
{
    /// <summary>
    /// Возвращает целевой объект.
    /// </summary>
    public CoreWebView2 Target { get; } = target;
}
