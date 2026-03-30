using Simargl.Concurrent;
using Simargl.Engine;
using Simargl.Web.Browsing.Core.Controls;

namespace Simargl.Web.Browsing;

/// <summary>
/// Представляет управляющего веб-содержимым.
/// </summary>
public sealed class WebManager :
    Something
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="container">
    /// Контейнер веб-элементов.
    /// </param>
    internal WebManager(WebContainer container)
    {
        //  Установка контейнера веб-элементов.
        Container = container;

        //  Создание критического объекта для блокировки размещения.
        LayoutLock = new();

        //  Создание коллекции веб-страниц.
        Pages = new(this);
    }

    /// <summary>
    /// Возвращает контейнер веб-элементов.
    /// </summary>
    internal WebContainer Container { get; }

    /// <summary>
    /// Возвращает критический объект для блокировки размещения.
    /// </summary>
    internal AsyncLock LayoutLock { get; }

    /// <summary>
    /// Возвращает коллекцию веб-страниц.
    /// </summary>
    public WebPageCollection Pages { get; }
}
