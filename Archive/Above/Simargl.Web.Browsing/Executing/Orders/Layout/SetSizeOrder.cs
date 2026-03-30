using Simargl.Web.Browsing.Core;

namespace Simargl.Web.Browsing.Executing.Orders.Layout;

/// <summary>
/// Представляет предписание устанавливающее размер.
/// </summary>
/// <param name="width">
/// Ширина элемент.
/// </param>
/// <param name="height">
/// Высота элемента.
/// </param>
public sealed class SetSizeOrder(int width, int height) :
    WebPageAction
{
    /// <summary>
    /// Возвращает ширину элемента.
    /// </summary>
    public int Width { get; } = IsPositive(width);

    /// <summary>
    /// Возвращает высоту элемента.
    /// </summary>
    public int Height { get; } = IsPositive(height);

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
        await controller.WebShell.SetSizeAsync(Width, Height, cancellationToken).ConfigureAwait(false);
    }
}
