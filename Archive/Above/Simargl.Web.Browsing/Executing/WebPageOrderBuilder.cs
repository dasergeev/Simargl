using Simargl.Web.Browsing.Executing.Orders.Layout;
using Simargl.Web.Browsing.Executing.Orders.Main;
using Simargl.Web.Proxies;

namespace Simargl.Web.Browsing.Executing;

/// <summary>
/// Представляет построителя предписаний.
/// </summary>
public sealed class WebPageOrderBuilder
{
    /// <summary>
    /// Поле для хранения исполнителя.
    /// </summary>
    private readonly WebPageExecutor _Executor;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="executor">
    /// Исполнитель.
    /// </param>
    internal WebPageOrderBuilder(WebPageExecutor executor)
    {
        //  Установка исполнителя.
        _Executor = executor;
    }

    ///// <summary>
    ///// Добавляет предписание перемещения в начало z-порядка.
    ///// </summary>
    //public void BringToFront() => _Executor.Add(new BringToFrontOrder());

    /// <summary>
    /// Добавляет предписание сброса размера.
    /// </summary>
    public void ResetSize() => _Executor.Add(new ResetSizeOrder());

    /// <summary>
    /// Добавляет предписание установки размера.
    /// </summary>
    /// <param name="width">
    /// Ширина элемента управления.
    /// </param>
    /// <param name="height">
    /// Высота элемента управления.
    /// </param>
    public void SetSize(int width, int height) => _Executor.Add(new SetSizeOrder(width, height));

    /// <summary>
    /// Добавляет предписание смены пути к пользовательским данным.
    /// </summary>
    /// <param name="path">
    /// Путь к пользовательским данным.
    /// </param>
    public void DataPath(string? path) => _Executor.Add(new DataPathOrder(path));

    /// <summary>
    /// Добавляет предписание смены масштабного коэффициента.
    /// </summary>
    /// <param name="factor">
    /// Масштабный коэффициент.
    /// </param>
    public void ZoomFactor(double factor) => _Executor.Add(new ZoomFactorOrder(factor));

    /// <summary>
    /// Добавляет предписание направления на новую страницу.
    /// </summary>
    /// <param name="source">
    /// Источник страницы.
    /// </param>
    public void Navigate(string source) => _Executor.Add(new NavigateOrder(source));

    /// <summary>
    /// Добавляет предписание перезагрузки страницы.
    /// </summary>
    public void Reload() => _Executor.Add(new ReloadOrder());

    /// <summary>
    /// Добвыляет предписание установки прокси-сервера.
    /// </summary>
    /// <param name="proxy">
    /// Информация о подключении к прокси-серверу.
    /// </param>
    public void SetProxy(WebProxyInfo? proxy) => _Executor.Add(new SetProxyOrder(proxy));
}
