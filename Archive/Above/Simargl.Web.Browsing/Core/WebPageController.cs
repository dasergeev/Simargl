using Simargl.Performing;
using Simargl.Web.Browsing.Core.Controllers;
using Simargl.Web.Browsing.Core.Controls;

namespace Simargl.Web.Browsing.Core;

/// <summary>
/// Представляет контроллер веб-страницы.
/// </summary>
internal sealed class WebPageController :
    Performer
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="manager">
    /// Управляющий веб-содержимым.
    /// </param>
    /// <param name="shell">
    /// Оболочка веб-элемента.
    /// </param>
    /// <param name="page">
    /// Веб-страница.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public WebPageController(WebManager manager, WebShell shell, WebPage page, CancellationToken cancellationToken) :
        base(cancellationToken)
    {
        //  Установка значений полей.
        Manager = manager;
        WebShell = new(this, shell, cancellationToken);
        WebView = new(this, shell, cancellationToken);
        WebPage = page;
    }

    /// <summary>
    /// Поле для хранения управляющего веб-содержимым.
    /// </summary>
    public WebManager Manager { get; }

    /// <summary>
    /// Возвращает контроллер оболочки веб-элемента.
    /// </summary>
    public WebShellController WebShell { get; }

    /// <summary>
    /// Возвращает контроллер веб-содержимого.
    /// </summary>
    public WebViewController WebView { get; }

    /// <summary>
    /// Возвращает веб-страницу.
    /// </summary>
    public WebPage WebPage { get; }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    /// <param name="disposing">
    /// Значение, определяющее требуется ли освободить управляемое состояние.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        //  Проверка необходимости разрушения управляемого состояния.
        if (disposing)
        {
            //  Разрушение контроллеров.
            DefyCritical(WebShell.Dispose);
            DefyCritical(WebView.Dispose);
        }

        //  Вызов метода базового класса.
        base.Dispose(disposing);
    }
}
