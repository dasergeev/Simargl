using Simargl.Engine;
using Simargl.Web.Browsing.Core;
using Simargl.Web.Browsing.Core.Controls;
using Simargl.Web.Browsing.Executing;

namespace Simargl.Web.Browsing;

/// <summary>
/// Представляет веб-страницу.
/// </summary>
public sealed class WebPage :
    Something,
    IDisposable
{
    /// <summary>
    /// Поле для хранения управляющего веб-содержимым.
    /// </summary>
    private readonly WebManager _WebManager;

    /// <summary>
    /// Поле для хранения оболочки веб-элемента.
    /// </summary>
    private readonly WebShell _WebShell;

    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private volatile CancellationTokenSource? _CancellationTokenSource;

    /// <summary>
    /// Поле для хранения токена отмены.
    /// </summary>
    private readonly CancellationToken _CancellationToken;

    /// <summary>
    /// Поле для хранения контроллера.
    /// </summary>
    private volatile WebPageController? _Controller;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="manager">
    /// Управляющий веб-содержимым.
    /// </param>
    /// <param name="shell">
    /// Оболочка веб-элемента.
    /// </param>
    internal WebPage(WebManager manager, WebShell shell)
    {
        //  Установка значений полей.
        _WebManager = manager;
        _WebShell = shell;

        //  Создание свойств.
        Info = new();
        Executor = new();

        //  Создание источника токена отмены.
        _CancellationTokenSource = new();

        //  Получение токена отмены.
        _CancellationToken = _CancellationTokenSource.Token;

        //  Добавление основной задачи в механизм поддержки.
        Entry.Keeper.Add(InvokeAsync);
    }

    /// <summary>
    /// Возвращает информацию о веб странице.
    /// </summary>
    public WebPageInfo Info { get; }

    /// <summary>
    /// Возвращает исполнителя веб-страницы.
    /// </summary>
    public WebPageExecutor Executor { get; }

    /// <summary>
    /// Асинхронно выполняет основную работу веб-страницы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу веб-страницы.
    /// </returns>
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Источник связанного токена отмены.
        CancellationTokenSource? linkedTokenSource = null;

        //  Связанный токен отмены.
        CancellationToken linkedToken = new(true);

        //  Блок с гарантированным завершением.
        try
        {
            //  Создание источника связанного токена отмены.
            linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                _CancellationToken, cancellationToken);

            //  Получение связанного токена отмены.
            linkedToken = linkedTokenSource.Token;

            //  Замена токена отмены.
            cancellationToken = linkedToken;

            //  Создание контроллера.
            using WebPageController controller = new(_WebManager, _WebShell, this, cancellationToken);

            //  Установка контроллера.
            Interlocked.Exchange(ref _Controller, controller);

            //  Выполнение работы исполнителя.
            await Executor.InvokeAsync(controller, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            //  Подавление всех некритических исключений.
            await DefyCriticalAsync(async delegate (CancellationToken cancellationToken)
            {
                //  Проверка связанного токена отмены.
                if (linkedToken.IsCancellationRequested)
                {
                    //  Удаление страницы.
                    await _WebManager.Pages.RemoveAsync(this, _WebShell, CancellationToken.None).ConfigureAwait(false);
                }
            }, CancellationToken.None).ConfigureAwait(false);

            //  Проверка источника связанного токена отмены.
            if (linkedTokenSource is not null)
            {
                //  Разрушение источника связанного токена отмены.
                DefyCritical(linkedTokenSource.Dispose);
            }
        }
    }

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
        //  Получение контроллера.
        if (_Controller is WebPageController controller)
        {
            //  Выполнение задачи.
            await controller.WebShell.BringToFrontAsync(cancellationToken).ConfigureAwait(false);
        }
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
        //  Получение контроллера.
        if (_Controller is WebPageController controller)
        {
            //  Выполнение задачи.
            await controller.WebShell.SetSizeAsync(width, height, cancellationToken).ConfigureAwait(false);
        }
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
        //  Получение контроллера.
        if (_Controller is WebPageController controller)
        {
            //  Выполнение задачи.
            await controller.WebShell.ResetSizeAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    public void Dispose()
    {
        //  Получение токена отмены.
        CancellationTokenSource? tokenSource = Interlocked.Exchange(ref _CancellationTokenSource, null);

        //  Проверка источника токена отмены.
        if (tokenSource is not null)
        {
            //  Отправка запроса на отмену.
            DefyCritical(tokenSource.Cancel);

            //  Разрушение источника токена отмены.
            DefyCritical(tokenSource.Dispose);
        }

        //  Уведомление среды выполнения о том, чтобы она не вызывала средство завершения для объекта.
        GC.SuppressFinalize(this);
    }
}
