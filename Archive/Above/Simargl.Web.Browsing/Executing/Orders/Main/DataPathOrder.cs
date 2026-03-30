using Simargl.Web.Browsing.Core;

namespace Simargl.Web.Browsing.Executing.Orders.Main;

/// <summary>
/// Представляет предписание смены пути к пользовательским данным.
/// </summary>
/// <param name="path">
/// Путь к пользовательским данным.
/// </param>
public sealed class DataPathOrder(string? path) :
    WebPageAction
{
    /// <summary>
    /// Возвращает путь к пользовательским данным.
    /// </summary>
    public string Path { get; } = IsNotEmpty(path);

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
        await controller.WebView.SetDataPathAsync(Path, cancellationToken).ConfigureAwait(false);
    }
}
