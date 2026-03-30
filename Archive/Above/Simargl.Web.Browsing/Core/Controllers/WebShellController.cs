using Simargl.Engine;
using Simargl.Performing;
using Simargl.Web.Browsing.Core.Controls;

namespace Simargl.Web.Browsing.Core.Controllers;

/// <summary>
/// Представляет контроллер оболочки веб-содержимого.
/// </summary>
/// <param name="webPageController">
/// Контроллер веб-страницы.
/// </param>
/// <param name="shell">
/// Оболочка вокруг веб-элемента.
/// </param>
/// <param name="cancellationToken">
/// Токен отмены.
/// </param>
internal sealed class WebShellController(WebPageController webPageController, WebShell shell, CancellationToken cancellationToken) :
    Performer(cancellationToken)
{
    /// <summary>
    /// Возвращает контроллер веб-страницы.
    /// </summary>
    public WebPageController WebPageController { get; } = webPageController;

    /// <summary>
    /// Возвращает оболочку веб-элемента.
    /// </summary>
    public WebShell Shell { get; } = shell;

    /// <summary>
    /// Асинхронно перемещает оболочку в начало z-порядка.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, перемещающая оболочку в начало z-порядка.
    /// </returns>
    public async Task BringToFrontAsync(CancellationToken cancellationToken)
    {
        //  Создание связанного токена отмены.
        using CancellationTokenSource linkedTokenSource =
            CancellationTokenSource.CreateLinkedTokenSource(
                GetCancellationToken(), cancellationToken);

        //  Выполнение в основном потоке.
        await Entry.Unique.Invoker.InvokeAsync(Shell.BringToFront, linkedTokenSource.Token).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно устанавливает размер веб элемента.
    /// </summary>
    /// <param name="width">
    /// Ширина веб элемента.
    /// </param>
    /// <param name="height">
    /// Высота веб элемента.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая размер веб элемента.
    /// </returns>
    public async Task SetSizeAsync(int width, int height, CancellationToken cancellationToken)
    {
        //  Создание связанного токена отмены.
        using CancellationTokenSource linkedTokenSource =
            CancellationTokenSource.CreateLinkedTokenSource(
                GetCancellationToken(), cancellationToken);

        //  Выполнение в основном потоке.
        await Entry.Unique.Invoker.InvokeAsync(delegate
        {
            //  Установка размера.
            Shell.SetWebSize(width, height);
        }, linkedTokenSource.Token).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно сбрасывает размер веб элемента.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая сброс размера веб элемента.
    /// </returns>
    /// <remarks>
    /// Веб элемент будет занимать всё клиентское пространство.
    /// </remarks>
    public async Task ResetSizeAsync(CancellationToken cancellationToken)
    {
        //  Создание связанного токена отмены.
        using CancellationTokenSource linkedTokenSource =
            CancellationTokenSource.CreateLinkedTokenSource(
                GetCancellationToken(), cancellationToken);

        //  Сброс размера.
        await Entry.Unique.Invoker.InvokeAsync(Shell.ResetWebSize, linkedTokenSource.Token).ConfigureAwait(false);
    }
}
