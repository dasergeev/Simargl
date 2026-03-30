using Simargl.Web.Browsing.Core;

namespace Simargl.Web.Browsing.Executing.Orders.Main;

/// <summary>
/// Представляет предписание смены масштабного коэффициента.
/// </summary>
/// <param name="factor">
/// Масштабный коэффициент.
/// </param>
public sealed class ZoomFactorOrder(double factor) :
    WebPageAction
{
    /// <summary>
    /// Возвращает масштабный коэффициент.
    /// </summary>
    public double Factor { get; } = IsFinite(IsPositive(factor), nameof(factor));

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
        await controller.WebView.SetZoomAsync(Factor, cancellationToken).ConfigureAwait(false);
    }
}
