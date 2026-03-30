using Simargl.Designing.Utilities;
using Simargl.Engine;
using Simargl.Journaling;
using Simargl.Performing;
using Simargl.Web.Browsing.Core.Controls;
using Simargl.Web.Browsing.Core.Drivers;
using Simargl.Web.Proxies;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.Text;

namespace Simargl.Web.Browsing.Core.Controllers;

/// <summary>
/// Представляет контроллер веб-содержимого.
/// </summary>
internal sealed class WebViewController :
    Performer
{
    /// <summary>
    /// Поле для хранения контроллера веб-страницы.
    /// </summary>
    public readonly WebPageController _WebPageController;

    /// <summary>
    /// Поле для хранения оболочки веб-элемента.
    /// </summary>
    private readonly WebShell _Shell;

    /// <summary>
    /// Поле для хранения драйвера элемента управления для внедрения.
    /// </summary>
    private volatile WebViewDriver? _WebViewDriver;

    /// <summary>
    /// Поле для хранения драйвера элемента, отображающего веб-контент.
    /// </summary>
    private volatile CoreWebViewDriver? _CoreWebViewDriver;

    /// <summary>
    /// Инициализирует новый экземпляр.
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
    internal WebViewController(WebPageController webPageController, WebShell shell, CancellationToken cancellationToken) :
        base(cancellationToken)
    {
        //  Установка значений полей.
        _WebPageController = webPageController;
        _Shell = shell;
        _WebViewDriver = null;
        _CoreWebViewDriver = null;
    }

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
            //  Асинхронное выполнение.
            _ = Task.Run(StopAsync);
        }

        //  Вызов метода базового класса.
        base.Dispose(disposing);
    }

    /// <summary>
    /// Асинхронно возвращает драйвер элемента, отображающего веб-контент.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая драйвер элемента, отображающего веб-контент.
    /// </returns>
    private async Task<WebViewDriver> GetWebViewDriverAsync(CancellationToken cancellationToken)
    {
        //  Проверка ссылки на драйвер.
        if (_WebViewDriver is null)
        {
            //  Запуск работы.
            await StartAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка ссылки на драйвер.
            if (_WebViewDriver is null)
            {
                throw new InvalidOperationException("Не удалось запустить работу веб-страницы.");
            }
        }

        //  Возврат драйвера.
        return _WebViewDriver;
    }

    /// <summary>
    /// Асинхронно возвращает драйвер элемента, отображающего веб-контент.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая драйвер элемента, отображающего веб-контент.
    /// </returns>
    private async Task<CoreWebViewDriver> GetCoreWebViewDriverAsync(CancellationToken cancellationToken)
    {
        //  Проверка ссылки на драйвер.
        if (_CoreWebViewDriver is null)
        {
            //  Запуск работы.
            await StartAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка ссылки на драйвер.
            if (_CoreWebViewDriver is null)
            {
                throw new InvalidOperationException("Не удалось запустить работу веб-страницы.");
            }
        }

        //  Возврат драйвера.
        return _CoreWebViewDriver;
    }

    /// <summary>
    /// Асинхронно запускает работу элемента управления.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, запускающая работу элемента управления.
    /// </returns>
    private async Task<bool> StartAsync(CancellationToken cancellationToken)
    {
        //  Создание связанного источника токена отмены.
        using CancellationTokenSource linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            GetCancellationToken(), cancellationToken);

        //  Замена источника токена отмены.
        cancellationToken = linkedTokenSource.Token;

        //  Блок перехвата всех исключений.
        try
        {
            //  Результат выполнения.
            bool result = true;

            //  Получение информации.
            WebPageInfo info = _WebPageController.WebPage.Info;

            //  Выполнение в основном потоке.
            await Entry.Unique.Invoker.InvokeAsync(async delegate (CancellationToken cancellationToken)
            {
                //  Создание настроек.
                CoreWebView2EnvironmentOptions coreOptions = new();

                //  Получение информации о прокси-сервере.
                WebProxyInfo? proxy = info.WebProxyInfo;

                //  Проверка необходимости использовать прокси сервер.
                if (proxy is not null)
                {
                    //  Добавление указания использовать прокси сервер.
                    coreOptions.AdditionalBrowserArguments = $"--proxy-server={proxy.Type.ToString().ToLower()}://{proxy.Address}:{proxy.Port}";

                    //  Проверка указания использовать логин и пароль.
                    if (!string.IsNullOrEmpty(proxy.Username) &&
                        !string.IsNullOrEmpty(proxy.Password))
                    {
                        //  Добавление указания использовать логин и пароль.
                        coreOptions.AdditionalBrowserArguments += $" --proxy-user={proxy.Username}:{proxy.Password}";
                    }
                }

                //  Создание среды.
                CoreWebView2Environment environment = await CoreWebView2Environment.CreateAsync(
                    null, info.DataPath, coreOptions).ConfigureAwait(true);

                //  Создание элемента.
                WebView2 webView = new();

                //  Создание драйвера.
                _WebViewDriver = new(webView);

                //  Добавление элемента.
                _Shell.Attach(webView);

                //  Явный запуск инициализации элемента управления.
                await webView.EnsureCoreWebView2Async(environment).ConfigureAwait(true);

                //  Получение элемента, отображающего веб-контент.
                CoreWebView2 coreWebView = webView.CoreWebView2;

                //  Создание драйвера элемента, отображающего веб-контент.
                _CoreWebViewDriver = new(coreWebView);

                //  Прикрепление событий.
                AttachEvents();
            }, cancellationToken).ConfigureAwait(false);

            //  Установка масштабного множителя.
            await SetZoomAsync(info.ZoomFactor, cancellationToken).ConfigureAwait(false);

            //  Проверка начальной страницы.
            if (info.Source is string source)
            {
                //  Переход на начальную страницу.
                result = await NavigateAsync(source, cancellationToken).ConfigureAwait(false);
            }

            //  Установка значения, определяющего находится ли страница в действительном состоянии.
            await info.SetIsValidAsync(true).ConfigureAwait(false);

            //  Возврат результата.
            return result;
        }
        catch
        {
            //  Остановка элемента.
            await StopAsync().ConfigureAwait(false);

            //  Повторный выброс исключения.
            throw;
        }
    }

    /// <summary>
    /// Асинхронно останавливает работу элемента управления.
    /// </summary>
    /// <returns>
    /// Задача, останавливающая работу элемента управления.
    /// </returns>
    private async Task StopAsync()
    {
        //  Открепление событий.
        DefyCritical(DetachEvents);

        //  Подавление всех некритических исключений.
        await DefyCriticalAsync(async delegate (CancellationToken cancellationToken)
        {
            //  Установка значения, определяющего находится ли страница в действительном состоянии.
            await _WebPageController.WebPage.Info.SetIsValidAsync(true).ConfigureAwait(false);
        }, CancellationToken.None).ConfigureAwait(false);

        //  Подавление всех некритических исключений.
        await DefyCriticalAsync(async delegate (CancellationToken cancellationToken)
        {
            //  Получение драйвера элемента, отображающего веб-контент.
            CoreWebViewDriver? coreWebViewDriver = Interlocked.Exchange(ref _CoreWebViewDriver, null);

            //  Проверка драйвера.
            if (coreWebViewDriver is not null)
            {
                //  Остановка навигации, если она активна.
                await safe(coreWebViewDriver.Target.Stop).ConfigureAwait(false);
            }
        }, CancellationToken.None).ConfigureAwait(false);

        //  Подавление всех некритических исключений.
        await DefyCriticalAsync(async delegate (CancellationToken cancellationToken)
        {
            //  Получение драйвера.
            WebViewDriver? webViewDriver = Interlocked.Exchange(ref _WebViewDriver, null);

            //  Проверка драйвера.
            if (webViewDriver is not null)
            {
                //  Получение элемента.
                WebView2 webView = webViewDriver.Target;

                //  Удаление элемента.
                await safe(() => _Shell.Detach());

                //  Разрушение элемента.
                await safe(webView.Dispose);
            }
        }, CancellationToken.None).ConfigureAwait(false);

        //  Безопасно выполняет действие в основном потоке.
        async Task safe(Action action)
        {
            //  Подавление всех некритических исключений.
            await DefyCriticalAsync(async delegate (CancellationToken cancellationToken)
            {
                //  Выполнение в основном потоке.
                await Entry.Unique.Invoker.InvokeAsync(delegate
                {
                    //  Выполнение дейсвтия.
                    DefyCritical(action);
                }, CancellationToken.None).ConfigureAwait(false);
            }, CancellationToken.None);
        }
    }

    /// <summary>
    /// Асинхронно выполняет замену прокси-сервера.
    /// </summary>
    /// <param name="proxy">
    /// Информация о прокси-сервере.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая замену прокси-сервера.
    /// </returns>
    public async Task<bool> SetProxyAsync(WebProxyInfo? proxy, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка выполнения.
        await IsNotCancelledAsync(GetCancellationToken()).ConfigureAwait(false);

        //  Получение информации.
        WebPageInfo info = _WebPageController.WebPage.Info;

        //  Проверка изменения прокси-сервера.
        if (info.WebProxyInfo != proxy)
        {
            //  Изменение прокси-сервера.
            await info.SetWebProxyInfoAsync(proxy).ConfigureAwait(false);

            //  Остановка работы.
            await StopAsync().ConfigureAwait(false);

            //  Проверка источника страницы.
            if (info.Source is not null)
            {
                //  Ожидание перед запуском.
                await Task.Delay(100, cancellationToken).ConfigureAwait(false);

                //  Запуск работы.
                return await StartAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        //  Успешное выполнение.
        return true;
    }

    /// <summary>
    /// Асинхронно изменяет путь к пользователсьским данным.
    /// </summary>
    /// <param name="path">
    /// Путь к пользовательским данным.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, изменяющая путь.
    /// </returns>
    public async Task SetDataPathAsync(string path, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка выполнения.
        await IsNotCancelledAsync(GetCancellationToken()).ConfigureAwait(false);

        //  Получение информации.
        WebPageInfo info = _WebPageController.WebPage.Info;

        //  Проверка изменения пути.
        if (info.DataPath != path)
        {
            //  Изменение пути.
            await info.SetDataPathAsync(path).ConfigureAwait(false);

            //  Остановка работы.
            await StopAsync().ConfigureAwait(false);

            //  Проверка источника страницы.
            if (info.Source is not null)
            {
                //  Ожидание перед запуском.
                await Task.Delay(100, cancellationToken).ConfigureAwait(false);

                //  Запуск работы.
                await StartAsync(cancellationToken).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Асинхронно выполняет переход с ожиданием.
    /// </summary>
    /// <param name="source">
    /// Новый источник страницы.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая переход с ожиданием.
    /// </returns>
    public async Task<bool> NavigateAsync(string source, CancellationToken cancellationToken)
    {
        //  Получение драйвера веб-элемента.
        CoreWebViewDriver coreWebViewDriver = await GetCoreWebViewDriverAsync(cancellationToken).ConfigureAwait(false);

        //  Создание источника завершения задачи.
        TaskCompletionSource<bool> completion = new();

        //  Создание связанного источника токена отмены.
        using CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            GetCancellationToken(), cancellationToken);

        //  Запуск задачи отслеживания токена отмены.
        _ = Task.Run(async delegate
        {
            //  Подавление некритических исключений.
            await DefyCriticalAsync(async delegate (CancellationToken cancellationToken)
            {
                //  Ожидание токена отмены.
                await Task.Delay(-1, tokenSource.Token).ConfigureAwait(false);
            }, cancellationToken).ConfigureAwait(false);

            //  Проверка токена отмены.
            if (cancellationToken.IsCancellationRequested)
            {
                //  Завершение основной задачи.
                completion.SetException(new OperationCanceledException());
            }
        }, cancellationToken).ConfigureAwait(false);

        //  Блок с гарантированным завершением.
        try
        {
            //  Выполнение в основном потоке.
            await Entry.Unique.Invoker.InvokeAsync(delegate
            {
                //  Добавление обработчика события.
                coreWebViewDriver.Target.NavigationCompleted += navigationCompleted;

                //  Запуск навигации.
                coreWebViewDriver.Target.Navigate(source);
            }, cancellationToken).ConfigureAwait(false);

            //  Установка нового источника.
            await _WebPageController.WebPage.Info.SetSourceAsync(source).ConfigureAwait(false);

            //  Ожидание завершения задачи.
            return await completion.Task.ConfigureAwait(false);
        }
        finally
        {
            //  Подавление некритических исключений.
            await DefyCriticalAsync(async delegate (CancellationToken cancellationToken)
            {
                //  Выполнение в основном потоке.
                await Entry.Unique.Invoker.InvokeAsync(delegate
                {
                    //  Удаление обработчика события.
                    coreWebViewDriver.Target.NavigationCompleted -= navigationCompleted;
                }, cancellationToken).ConfigureAwait(false);
            }, cancellationToken).ConfigureAwait(false);
        }

        //  Обрабатывает событие завершения навигации.
        void navigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            //  Завершение навигации.
            DefyCritical(() => completion.SetResult(e.IsSuccess));
        }
    }

    /// <summary>
    /// Асинхронно устанавливает масштабный множитель.
    /// </summary>
    /// <param name="zoomFactor">
    /// Масштабный множитель.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая масштабный множитель.
    /// </returns>
    public async Task SetZoomAsync(double zoomFactor, CancellationToken cancellationToken)
    {
        //  Проверка масштабного множителя.
        IsFinite(zoomFactor);
        IsPositive(zoomFactor);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка выполнения.
        await IsNotCancelledAsync(GetCancellationToken()).ConfigureAwait(false);

        //  Получение информации.
        WebPageInfo info = _WebPageController.WebPage.Info;

        //  Проверка изменения масштабного множителя.
        if (info.ZoomFactor != zoomFactor)
        {
            //  Изменение масштабного множителя.
            await info.SetZoomFactorAsync(zoomFactor).ConfigureAwait(false);

            //  Проверка источника страницы.
            if (info.Source is not null)
            {
                //  Получение драйвера веб-элемента.
                WebViewDriver webViewDriver = await GetWebViewDriverAsync(cancellationToken).ConfigureAwait(false);

                //  Установка масштабного множителя.
                webViewDriver.Target.ZoomFactor = zoomFactor;
            }
        }
    }

    /// <summary>
    /// Асинхронно выполняет перезагрузку с ожиданием.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая перезагрузку.
    /// </returns>
    public async Task<bool> ReloadAsync(CancellationToken cancellationToken)
    {
        //  Получение драйвера веб-элемента.
        CoreWebViewDriver coreWebViewDriver = await GetCoreWebViewDriverAsync(cancellationToken).ConfigureAwait(false);

        //  Создание источника завершения задачи.
        TaskCompletionSource<bool> completion = new();

        //  Создание связанного источника токена отмены.
        using CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        //  Запуск задачи отслеживания токена отмены.
        _ = Task.Run(async delegate
        {
            //  Подавление всех некритических исключений.
            await DefyCriticalAsync(async delegate (CancellationToken cancellationToken)
            {
                //  Ожидание токена отмены.
                await Task.Delay(-1, tokenSource.Token).ConfigureAwait(false);
            }, cancellationToken).ConfigureAwait(false);

            //  Проверка токена отмены.
            if (cancellationToken.IsCancellationRequested)
            {
                //  Завершение основной задачи.
                completion.SetException(new OperationCanceledException());
            }
        }, cancellationToken).ConfigureAwait(false);

        //  Блок с гарантированным завершением.
        try
        {
            //  Выполнение в основном потоке.
            await Entry.Unique.Invoker.InvokeAsync(delegate
            {
                //  Добавление обработчика события.
                coreWebViewDriver.Target.NavigationCompleted += navigationCompleted;

                //  Запуск навигации.
                coreWebViewDriver.Target.Reload();
            }, cancellationToken).ConfigureAwait(false);

            //  Ожидание завершения задачи.
            return await completion.Task.ConfigureAwait(false);
        }
        finally
        {
            //  Подавление всех некритических исключений.
            await DefyCriticalAsync(async delegate (CancellationToken cancellationToken)
            {
                //  Выполнение в основном потоке.
                await Entry.Unique.Invoker.InvokeAsync(delegate
                {
                    //  Удаление обработчика события.
                    coreWebViewDriver.Target.NavigationCompleted -= navigationCompleted;
                }, cancellationToken).ConfigureAwait(false);
            }, cancellationToken).ConfigureAwait(false);
        }

        //  Обрабатывает событие завершения навигации.
        void navigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            //  Завершение навигации.
            DefyCritical(() => completion.SetResult(e.IsSuccess));
        }
    }

    /// <summary>
    /// Прикрекляет события.
    /// </summary>
    private void AttachEvents()
    {
        //  Получение драйвера элемента управления для внедрения.
        if (_WebViewDriver is WebViewDriver webViewDriver)
        {
            //  Добавление обработчиков событий.
            webViewDriver.Target.ContentLoading += WebView_ContentLoading;
            webViewDriver.Target.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
            webViewDriver.Target.NavigationCompleted += WebView_NavigationCompleted;
            webViewDriver.Target.NavigationStarting += WebView_NavigationStarting;
            webViewDriver.Target.SourceChanged += WebView_SourceChanged;
            webViewDriver.Target.WebMessageReceived += WebView_WebMessageReceived;
            webViewDriver.Target.ZoomFactorChanged += WebView_ZoomFactorChanged;
        }

        //  Получение драйвера элемента, отображающего веб-контент.
        if (_CoreWebViewDriver is CoreWebViewDriver coreWebViewDriver)
        {
            //  Добавление обработчиков событий.
            coreWebViewDriver.Target.BasicAuthenticationRequested += CoreWebView_BasicAuthenticationRequested;
            coreWebViewDriver.Target.ClientCertificateRequested += CoreWebView_ClientCertificateRequested;
            coreWebViewDriver.Target.ContainsFullScreenElementChanged += CoreWebView_ContainsFullScreenElementChanged;
            coreWebViewDriver.Target.ContentLoading += CoreWebView_ContentLoading;
            coreWebViewDriver.Target.ContextMenuRequested += CoreWebView_ContextMenuRequested;
            coreWebViewDriver.Target.DocumentTitleChanged += CoreWebView_DocumentTitleChanged;
            coreWebViewDriver.Target.DOMContentLoaded += CoreWebView_DOMContentLoaded;
            coreWebViewDriver.Target.DownloadStarting += CoreWebView_DownloadStarting;
            coreWebViewDriver.Target.FaviconChanged += CoreWebView_FaviconChanged;
            coreWebViewDriver.Target.FrameCreated += CoreWebView_FrameCreated;
            coreWebViewDriver.Target.FrameNavigationCompleted += CoreWebView_FrameNavigationCompleted;
            coreWebViewDriver.Target.FrameNavigationStarting += CoreWebView_FrameNavigationStarting;
            coreWebViewDriver.Target.HistoryChanged += CoreWebView_HistoryChanged;
            coreWebViewDriver.Target.IsDefaultDownloadDialogOpenChanged += CoreWebView_IsDefaultDownloadDialogOpenChanged;
            coreWebViewDriver.Target.IsDocumentPlayingAudioChanged += CoreWebView_IsDocumentPlayingAudioChanged;
            coreWebViewDriver.Target.IsMutedChanged += CoreWebView_IsMutedChanged;
            coreWebViewDriver.Target.LaunchingExternalUriScheme += CoreWebView_LaunchingExternalUriScheme;
            coreWebViewDriver.Target.NavigationCompleted += CoreWebView_NavigationCompleted;
            coreWebViewDriver.Target.NavigationStarting += CoreWebView_NavigationStarting;
            coreWebViewDriver.Target.NewWindowRequested += CoreWebView_NewWindowRequested;
            coreWebViewDriver.Target.NotificationReceived += CoreWebView_NotificationReceived;
            coreWebViewDriver.Target.PermissionRequested += CoreWebView_PermissionRequested;
            coreWebViewDriver.Target.ProcessFailed += CoreWebView_ProcessFailed;
            coreWebViewDriver.Target.SaveAsUIShowing += CoreWebView_SaveAsUIShowing;
            coreWebViewDriver.Target.SaveFileSecurityCheckStarting += CoreWebView_SaveFileSecurityCheckStarting;
            coreWebViewDriver.Target.ScreenCaptureStarting += CoreWebView_ScreenCaptureStarting;
            coreWebViewDriver.Target.ScriptDialogOpening += CoreWebView_ScriptDialogOpening;
            coreWebViewDriver.Target.ServerCertificateErrorDetected += CoreWebView_ServerCertificateErrorDetected;
            coreWebViewDriver.Target.SourceChanged += CoreWebView_SourceChanged;
            coreWebViewDriver.Target.StatusBarTextChanged += CoreWebView_StatusBarTextChanged;
            coreWebViewDriver.Target.WebMessageReceived += CoreWebView_WebMessageReceived;
            coreWebViewDriver.Target.WebResourceRequested += CoreWebView_WebResourceRequested;
            coreWebViewDriver.Target.WebResourceResponseReceived += CoreWebView_WebResourceResponseReceived;
            coreWebViewDriver.Target.WindowCloseRequested += CoreWebView_WindowCloseRequested;
        }
    }

    /// <summary>
    /// Открепляет события.
    /// </summary>
    private void DetachEvents()
    {
        //  Подавление всех некритических исключений.
        DefyCritical(delegate
        {
            //  Получение драйвера элемента управления для внедрения.
            if (_WebViewDriver is WebViewDriver webViewDriver)
            {
                //  Удаление обработчиков событий.
                DefyCritical(() => webViewDriver.Target.ContentLoading -= WebView_ContentLoading);
                DefyCritical(() => webViewDriver.Target.CoreWebView2InitializationCompleted -= WebView_CoreWebView2InitializationCompleted);
                DefyCritical(() => webViewDriver.Target.NavigationCompleted -= WebView_NavigationCompleted);
                DefyCritical(() => webViewDriver.Target.NavigationStarting -= WebView_NavigationStarting);
                DefyCritical(() => webViewDriver.Target.SourceChanged -= WebView_SourceChanged);
                DefyCritical(() => webViewDriver.Target.WebMessageReceived -= WebView_WebMessageReceived);
                DefyCritical(() => webViewDriver.Target.ZoomFactorChanged -= WebView_ZoomFactorChanged);
            }
        });

        //  Подавление всех некритических исключений.
        DefyCritical(delegate
        {
            //  Получение драйвера элемента, отображающего веб-контент.
            if (_CoreWebViewDriver is CoreWebViewDriver coreWebViewDriver)
            {
                //  Удаление обработчиков событий.
                DefyCritical(() => coreWebViewDriver.Target.BasicAuthenticationRequested -= CoreWebView_BasicAuthenticationRequested);
                DefyCritical(() => coreWebViewDriver.Target.ClientCertificateRequested -= CoreWebView_ClientCertificateRequested);
                DefyCritical(() => coreWebViewDriver.Target.ContainsFullScreenElementChanged -= CoreWebView_ContainsFullScreenElementChanged);
                DefyCritical(() => coreWebViewDriver.Target.ContentLoading -= CoreWebView_ContentLoading);
                DefyCritical(() => coreWebViewDriver.Target.ContextMenuRequested -= CoreWebView_ContextMenuRequested);
                DefyCritical(() => coreWebViewDriver.Target.DocumentTitleChanged -= CoreWebView_DocumentTitleChanged);
                DefyCritical(() => coreWebViewDriver.Target.DOMContentLoaded -= CoreWebView_DOMContentLoaded);
                DefyCritical(() => coreWebViewDriver.Target.DownloadStarting -= CoreWebView_DownloadStarting);
                DefyCritical(() => coreWebViewDriver.Target.FaviconChanged -= CoreWebView_FaviconChanged);
                DefyCritical(() => coreWebViewDriver.Target.FrameCreated -= CoreWebView_FrameCreated);
                DefyCritical(() => coreWebViewDriver.Target.FrameNavigationCompleted -= CoreWebView_FrameNavigationCompleted);
                DefyCritical(() => coreWebViewDriver.Target.FrameNavigationStarting -= CoreWebView_FrameNavigationStarting);
                DefyCritical(() => coreWebViewDriver.Target.HistoryChanged -= CoreWebView_HistoryChanged);
                DefyCritical(() => coreWebViewDriver.Target.IsDefaultDownloadDialogOpenChanged -= CoreWebView_IsDefaultDownloadDialogOpenChanged);
                DefyCritical(() => coreWebViewDriver.Target.IsDocumentPlayingAudioChanged -= CoreWebView_IsDocumentPlayingAudioChanged);
                DefyCritical(() => coreWebViewDriver.Target.IsMutedChanged -= CoreWebView_IsMutedChanged);
                DefyCritical(() => coreWebViewDriver.Target.LaunchingExternalUriScheme -= CoreWebView_LaunchingExternalUriScheme);
                DefyCritical(() => coreWebViewDriver.Target.NavigationCompleted -= CoreWebView_NavigationCompleted);
                DefyCritical(() => coreWebViewDriver.Target.NavigationStarting -= CoreWebView_NavigationStarting);
                DefyCritical(() => coreWebViewDriver.Target.NewWindowRequested -= CoreWebView_NewWindowRequested);
                DefyCritical(() => coreWebViewDriver.Target.NotificationReceived -= CoreWebView_NotificationReceived);
                DefyCritical(() => coreWebViewDriver.Target.PermissionRequested -= CoreWebView_PermissionRequested);
                DefyCritical(() => coreWebViewDriver.Target.ProcessFailed -= CoreWebView_ProcessFailed);
                DefyCritical(() => coreWebViewDriver.Target.SaveAsUIShowing -= CoreWebView_SaveAsUIShowing);
                DefyCritical(() => coreWebViewDriver.Target.SaveFileSecurityCheckStarting -= CoreWebView_SaveFileSecurityCheckStarting);
                DefyCritical(() => coreWebViewDriver.Target.ScreenCaptureStarting -= CoreWebView_ScreenCaptureStarting);
                DefyCritical(() => coreWebViewDriver.Target.ScriptDialogOpening -= CoreWebView_ScriptDialogOpening);
                DefyCritical(() => coreWebViewDriver.Target.ServerCertificateErrorDetected -= CoreWebView_ServerCertificateErrorDetected);
                DefyCritical(() => coreWebViewDriver.Target.SourceChanged -= CoreWebView_SourceChanged);
                DefyCritical(() => coreWebViewDriver.Target.StatusBarTextChanged -= CoreWebView_StatusBarTextChanged);
                DefyCritical(() => coreWebViewDriver.Target.WebMessageReceived -= CoreWebView_WebMessageReceived);
                DefyCritical(() => coreWebViewDriver.Target.WebResourceRequested -= CoreWebView_WebResourceRequested);
                DefyCritical(() => coreWebViewDriver.Target.WebResourceResponseReceived -= CoreWebView_WebResourceResponseReceived);
                DefyCritical(() => coreWebViewDriver.Target.WindowCloseRequested -= CoreWebView_WindowCloseRequested);
            }
        });
    }

    private void WebView_ContentLoading(object? sender, CoreWebView2ContentLoadingEventArgs e)
    {
        //Journal.Default.Add("WebView_ContentLoading", JournalRecordLevel.Debug);
    }

    private void WebView_CoreWebView2InitializationCompleted(object? sender, CoreWebView2InitializationCompletedEventArgs e)
    {
        //Journal.Default.Add("WebView_CoreWebView2InitializationCompleted", JournalRecordLevel.Debug);
    }

    private void WebView_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        //  Проверка объекта.
        if (sender is WebView2 webView)
        {
            //  Установка масштабного множителя.
            webView.ZoomFactor = _WebPageController.WebPage.Info.ZoomFactor;
        }

        //Journal.Default.Add("WebView_NavigationCompleted", JournalRecordLevel.Debug);
    }

    private void WebView_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
    {
        //  Проверка объекта.
        if (sender is WebView2 webView)
        {
            //  Установка масштабного множителя.
            webView.ZoomFactor = _WebPageController.WebPage.Info.ZoomFactor;
        }

        //Journal.Default.Add("WebView_NavigationStarting", JournalRecordLevel.Debug);
    }

    private void WebView_SourceChanged(object? sender, CoreWebView2SourceChangedEventArgs e)
    {
        //  Проверка объекта.
        if (sender is WebView2 webView)
        {
            //  Получение текущего источника.
            string source = webView.Source.ToString();

            //  Асинхронное выполнение.
            _ = Task.Run(async delegate
            {
                //  Изменение источника.
                await _WebPageController.WebPage.Info.SetSourceAsync(source).ConfigureAwait(false);
            });
        }

        //Journal.Default.Add("WebView_SourceChanged", JournalRecordLevel.Debug);
    }

    private void WebView_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        //Journal.Default.Add("WebView_WebMessageReceived", JournalRecordLevel.Debug);
    }

    private void WebView_ZoomFactorChanged(object? sender, EventArgs e)
    {
        //Journal.Default.Add("WebView_ZoomFactorChanged", JournalRecordLevel.Debug);
    }

    private void CoreWebView_BasicAuthenticationRequested(object? sender, CoreWebView2BasicAuthenticationRequestedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_BasicAuthenticationRequested", JournalRecordLevel.Debug);
    }

    private void CoreWebView_ClientCertificateRequested(object? sender, CoreWebView2ClientCertificateRequestedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_ClientCertificateRequested", JournalRecordLevel.Debug);
    }

    private void CoreWebView_ContainsFullScreenElementChanged(object? sender, object e)
    {
        //Journal.Default.Add("CoreWebView_ContainsFullScreenElementChanged", JournalRecordLevel.Debug);
    }

    private void CoreWebView_ContentLoading(object? sender, CoreWebView2ContentLoadingEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_ContentLoading", JournalRecordLevel.Debug);
    }

    private void CoreWebView_ContextMenuRequested(object? sender, CoreWebView2ContextMenuRequestedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_ContextMenuRequested", JournalRecordLevel.Debug);
    }

    private void CoreWebView_DocumentTitleChanged(object? sender, object e)
    {
        //Journal.Default.Add("CoreWebView_DocumentTitleChanged", JournalRecordLevel.Debug);
    }

    private void CoreWebView_DOMContentLoaded(object? sender, CoreWebView2DOMContentLoadedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_DOMContentLoaded", JournalRecordLevel.Debug);
    }

    private void CoreWebView_DownloadStarting(object? sender, CoreWebView2DownloadStartingEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_DownloadStarting", JournalRecordLevel.Debug);
    }

    private void CoreWebView_FaviconChanged(object? sender, object e)
    {
        //Journal.Default.Add("CoreWebView_FaviconChanged", JournalRecordLevel.Debug);
    }

    private void CoreWebView_FrameCreated(object? sender, CoreWebView2FrameCreatedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_FrameCreated", JournalRecordLevel.Debug);
    }

    private void CoreWebView_FrameNavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_FrameNavigationCompleted", JournalRecordLevel.Debug);
    }

    private void CoreWebView_FrameNavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_FrameNavigationStarting", JournalRecordLevel.Debug);
    }

    private void CoreWebView_HistoryChanged(object? sender, object e)
    {
        //Journal.Default.Add("CoreWebView_HistoryChanged", JournalRecordLevel.Debug);
    }

    private void CoreWebView_IsDefaultDownloadDialogOpenChanged(object? sender, object e)
    {
        //Journal.Default.Add("CoreWebView_IsDefaultDownloadDialogOpenChanged", JournalRecordLevel.Debug);
    }

    private void CoreWebView_IsDocumentPlayingAudioChanged(object? sender, object e)
    {
        //Journal.Default.Add("CoreWebView_IsDocumentPlayingAudioChanged", JournalRecordLevel.Debug);
    }

    private void CoreWebView_IsMutedChanged(object? sender, object e)
    {
        //Journal.Default.Add("CoreWebView_IsMutedChanged", JournalRecordLevel.Debug);
    }

    private void CoreWebView_LaunchingExternalUriScheme(object? sender, CoreWebView2LaunchingExternalUriSchemeEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_LaunchingExternalUriScheme", JournalRecordLevel.Debug);
    }

    private void CoreWebView_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        //StringBuilder builder = new();
        //builder.AppendLine("CoreWebView_NavigationCompleted:");
        //builder.AppendLine($"  IsSuccess = {e.IsSuccess}");
        //builder.AppendLine($"  WebErrorStatus = {e.WebErrorStatus}");
        //builder.AppendLine($"  HttpStatusCode = {e.HttpStatusCode}");
        //Journal.Default.Add(builder.ToString(), JournalRecordLevel.Debug);

        //  Проверка сбоя навигации.
        if (!e.IsSuccess)
        {
            //  Вызов события о сбое навигации.
            _WebPageController.WebPage.Info.OnNavigationFailed(EventArgs.Empty);
        }
    }

    private void CoreWebView_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_NavigationStarting", JournalRecordLevel.Debug);
    }

    private void CoreWebView_NewWindowRequested(object? sender, CoreWebView2NewWindowRequestedEventArgs e)
    {
        //  Подавление всех некритических исключений.
        DefyCritical(delegate
        {
            //  Получение текущего окна.
            if (sender is CoreWebView2 current)
            {
                //  Перенаправление в текущее окно.
                e.NewWindow = current;
            }
        });

        //Journal.Default.Add("CoreWebView_NewWindowRequested", JournalRecordLevel.Debug);
    }

    private void CoreWebView_NotificationReceived(object? sender, CoreWebView2NotificationReceivedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_NotificationReceived", JournalRecordLevel.Debug);
    }

    private void CoreWebView_PermissionRequested(object? sender, CoreWebView2PermissionRequestedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_PermissionRequested", JournalRecordLevel.Debug);
    }

    private void CoreWebView_ProcessFailed(object? sender, CoreWebView2ProcessFailedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_ProcessFailed", JournalRecordLevel.Debug);
    }

    private void CoreWebView_SaveAsUIShowing(object? sender, CoreWebView2SaveAsUIShowingEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_SaveAsUIShowing", JournalRecordLevel.Debug);
    }

    private void CoreWebView_SaveFileSecurityCheckStarting(object? sender, CoreWebView2SaveFileSecurityCheckStartingEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_SaveFileSecurityCheckStarting", JournalRecordLevel.Debug);
    }

    private void CoreWebView_ScreenCaptureStarting(object? sender, CoreWebView2ScreenCaptureStartingEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_ScreenCaptureStarting", JournalRecordLevel.Debug);
    }

    private void CoreWebView_ScriptDialogOpening(object? sender, CoreWebView2ScriptDialogOpeningEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_ScriptDialogOpening", JournalRecordLevel.Debug);
    }

    private void CoreWebView_ServerCertificateErrorDetected(object? sender, CoreWebView2ServerCertificateErrorDetectedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_ServerCertificateErrorDetected", JournalRecordLevel.Debug);
    }

    private void CoreWebView_SourceChanged(object? sender, CoreWebView2SourceChangedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_SourceChanged", JournalRecordLevel.Debug);
    }

    private void CoreWebView_StatusBarTextChanged(object? sender, object e)
    {
        //Journal.Default.Add("CoreWebView_StatusBarTextChanged", JournalRecordLevel.Debug);
    }

    private void CoreWebView_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_WebMessageReceived", JournalRecordLevel.Debug);
    }

    private void CoreWebView_WebResourceRequested(object? sender, CoreWebView2WebResourceRequestedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_WebResourceRequested", JournalRecordLevel.Debug);
    }

    private void CoreWebView_WebResourceResponseReceived(object? sender, CoreWebView2WebResourceResponseReceivedEventArgs e)
    {
        //Journal.Default.Add("CoreWebView_WebResourceResponseReceived", JournalRecordLevel.Debug);
    }

    private void CoreWebView_WindowCloseRequested(object? sender, object e)
    {
        //Journal.Default.Add("CoreWebView_WindowCloseRequested", JournalRecordLevel.Debug);
    }
}


/*



using LRESULT = nint;
using HWND = nint;
using UINT = System.Int32;
using WPARAM = nuint;
using LPARAM = nint;
using POINT = System.Drawing.Point;
using RECT = System.Drawing.Rectangle;
using BOOL = System.Int32;
using System.Drawing;
using System.IO;
using LONG = long;
using WORD = ushort;
using DWORD = uint;
using DWORD_PTR = ulong;



    /// <summary>
    /// Асинхронно возвращает текущий источник.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая текущий источник.
    /// </returns>
    public async Task<string> GetSourceAsync(CancellationToken cancellationToken)
    {
        //  Источник.
        string source = string.Empty;

        //  Выполнение в основном потоке.
        await Entry.Invoker.InvokeAsync(delegate
        {
            //  Установка размера.
            source = _WebView!.Source.ToString();
        }, cancellationToken).ConfigureAwait(false);

        //  Возврат источника.
        return source;
    }




    //PasteFromClipboard


    //  PasteImage
    //  ScriptAsync
    /// <summary>
    /// Асинхронно вставляет данные из буфера обмена.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, вставляющая данные из буфера обмена.
    /// </returns>
    public async Task PasteAsync(CancellationToken cancellationToken)
    {
        //  Асинхронное выполнение.
        await SafeAsync(async delegate (CancellationToken cancellationToken)
        {
            //  Выполнение в основном потоке.
            await Entry.Unique.Invoker.InvokeAsync(delegate
            {
                //  Обновление.
                nint handle = GetTopHandle();

                //  Проверка дескриптора окна.
                if (handle != 0)
                {
                    PasteFromClipboard(handle);
                }
            }, cancellationToken).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);

    }

    ///// <summary>
    ///// Асинхронно вставляет данные из буфера обмена.
    ///// </summary>
    ///// <param name="cancellationToken">
    ///// Токен отмены.
    ///// </param>
    ///// <returns>
    ///// Задача, вставляющая данные из буфера обмена.
    ///// </returns>
    //public async Task PasteAsync(CancellationToken cancellationToken)
    //{
    //    // Вставляем изображение в WebView2 с помощью JavaScript
    //    string script = @"
    //                    var event = new KeyboardEvent('keydown', {
    //                        key: 'v',
    //                        ctrlKey: true,
    //                        bubbles: true
    //                    });
    //                    document.body.dispatchEvent(event);
    //                ";

    //    //  Выполнение скрипта.
    //    await ScriptAsync(script, cancellationToken).ConfigureAwait(false);
    //}

    ///// <summary>
    ///// Асинхронно вставляет изображение в текущее положение.
    ///// </summary>
    ///// <param name="image">
    ///// Изображение, которое необходимо вставить.
    ///// </param>
    ///// <param name="cancellationToken">
    ///// Токен отмены.
    ///// </param>
    ///// <returns>
    ///// Задача, выполняющая вставку картинки.
    ///// </returns>
    //public async Task PasteAsync(Image image, CancellationToken cancellationToken)
    //{
    //    // Преобразуем изображение в формат Base64
    //    string base64Image = ConvertImageToBase64(image);

    //    // Вставляем изображение в WebView2 с помощью JavaScript
    //    string script = $@"
    //                    var img = document.createElement('img');
    //                    img.src = 'data:image/png;base64,{base64Image}';
    //                    var selection = window.getSelection();
    //                    var range = selection.getRangeAt(0);
    //                    range.deleteContents();
    //                    range.insertNode(img);
    //                ";

    //    //  Выполнение скрипта.
    //    await ScriptAsync(script, cancellationToken).ConfigureAwait(false);

    //    static string ConvertImageToBase64(Image image)
    //    {
    //        using MemoryStream ms = new();
    //        // Сохраняем изображение в поток в формате PNG
    //        image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
    //        byte[] imageBytes = ms.ToArray();

    //        // Преобразуем байты в строку Base64
    //        return Convert.ToBase64String(imageBytes);
    //    }
    //}

    /// <summary>
    /// Асинхронно вставляет текст в текущее положение.
    /// </summary>
    /// <param name="text">
    /// Текст, который необходимо вставить.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, вставляющая текст.
    /// </returns>
    public async Task PasteAsync(string text, CancellationToken cancellationToken)
    {
        //  Асинхронное выполнение.
        await SafeAsync(async delegate (CancellationToken cancellationToken)
        {
            //  Выполнение в основном потоке.
            await Entry.Unique.Invoker.InvokeAsync(async delegate (CancellationToken cancellationToken)
            {
                string escapedText = text.Replace("'", "\\'").Replace(Environment.NewLine, "\\n");

                string script = $@"
                (function() {{
                    const selection = window.getSelection();
                    if (!selection.rangeCount) return;
                    const range = selection.getRangeAt(0);
                    range.deleteContents();
                    const textNode = document.createTextNode('{escapedText}');
                    range.insertNode(textNode);
                    range.collapse(false);
                }})();";



                await _WebView!.ExecuteScriptAsync(script);
            }, cancellationToken).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно имитирует нажатие левой клавиши мыши.
    /// </summary>
    /// <param name="x">
    /// Координата места нажатия по оси Ox.
    /// </param>
    /// <param name="y">
    /// Координата места нажатия по оси Oy.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, имитирующая нажатие левой клавиши мыши.
    /// </returns>
    public async Task SendClickAsync(int x, int y, CancellationToken cancellationToken)
    {
        //  Выполнение одиночного нажатия.
        await SendClickAsync(x, y, 1, 0, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно имитирует нажатие левой клавиши мыши.
    /// </summary>
    /// <param name="x">
    /// Координата места нажатия по оси Ox.
    /// </param>
    /// <param name="y">
    /// Координата места нажатия по оси Oy.
    /// </param>
    /// <param name="count">
    /// Количество нажатий.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, имитирующая нажатие левой клавиши мыши.
    /// </returns>
    public async Task SendClickAsync(int x, int y, int count, CancellationToken cancellationToken)
    {
        //  Выполнение нажатий.
        await SendClickAsync(x, y, count, 0, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно имитирует нажатие левой клавиши мыши.
    /// </summary>
    /// <param name="x">
    /// Координата места нажатия по оси Ox.
    /// </param>
    /// <param name="y">
    /// Координата места нажатия по оси Oy.
    /// </param>
    /// <param name="count">
    /// Количество нажатий.
    /// </param>
    /// <param name="delay">
    /// Задержка между нажатиями.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, имитирующая нажатие левой клавиши мыши.
    /// </returns>
    public async Task SendClickAsync(int x, int y, int count, int delay, CancellationToken cancellationToken)
    {
        //  Проверка количества нажатий.
        if (count <= 0)
        {
            //  Нет нажатий.
            return;
        }

        //  Проверка задержки.
        if (delay > 0)
        {
            //  Цикл по нажатиям.
            for (int i = 0; i < count; i++)
            {
                //  Проверка необходимоти ожидания.
                if (i != 0)
                {
                    //  Ожидание.
                    await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                }

                //  Асинхронное выполнение.
                await SafeAsync(async delegate (CancellationToken cancellationToken)
                {
                    //  Выполнение в основном потоке.
                    await Entry.Unique.Invoker.InvokeAsync(delegate
                    {
                        SendClick(x - 1, y - 1, x + 1, y + 1);
                    }, cancellationToken).ConfigureAwait(false);
                }, cancellationToken).ConfigureAwait(false);
            }
        }
        else
        {
            //  Асинхронное выполнение.
            await SafeAsync(async delegate (CancellationToken cancellationToken)
            {
                //  Выполнение в основном потоке.
                await Entry.Unique.Invoker.InvokeAsync(delegate
                {
                    //  Цикл по нажатиям.
                    for (int i = 0; i < count; i++)
                    {
                        SendClick(x - 1, y - 1, x + 1, y + 1);
                    }
                }, cancellationToken).ConfigureAwait(false);
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно отправляет сообщение о прокрутке мышью.
    /// </summary>
    /// <param name="x">
    /// Координата места нажатия по оси Ox.
    /// </param>
    /// <param name="y">
    /// Координата места нажатия по оси Oy.
    /// </param>
    /// <param name="delta">
    /// Значение прокрутки.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, отправляющая сообщение.
    /// </returns>
    public async Task SendMouseScrollAsync(int x, int y, int delta, CancellationToken cancellationToken)
    {
        await SendMouseScrollAsync(x, y, delta, 1, 0, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно отправляет сообщение о прокрутке мышью.
    /// </summary>
    /// <param name="x">
    /// Координата места нажатия по оси Ox.
    /// </param>
    /// <param name="y">
    /// Координата места нажатия по оси Oy.
    /// </param>
    /// <param name="delta">
    /// Значение прокрутки.
    /// </param>
    /// <param name="count">
    /// Количество нажатий.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, отправляющая сообщение.
    /// </returns>
    public async Task SendMouseScrollAsync(int x, int y, int delta, int count, CancellationToken cancellationToken)
    {
        await SendMouseScrollAsync(x, y, delta, count, 0, cancellationToken).ConfigureAwait(false);
    }


    /// <summary>
    /// Асинхронно отправляет сообщение о прокрутке мышью.
    /// </summary>
    /// <param name="x">
    /// Координата места нажатия по оси Ox.
    /// </param>
    /// <param name="y">
    /// Координата места нажатия по оси Oy.
    /// </param>
    /// <param name="delta">
    /// Значение прокрутки.
    /// </param>
    /// <param name="count">
    /// Количество нажатий.
    /// </param>
    /// <param name="delay">
    /// Задержка между нажатиями.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, отправляющая сообщение.
    /// </returns>
    public async Task SendMouseScrollAsync(int x, int y, int delta, int count, int delay, CancellationToken cancellationToken)
    {
        //  Проверка количества нажатий.
        if (count <= 0)
        {
            //  Нет нажатий.
            return;
        }

        //  Проверка задержки.
        if (delay > 0)
        {
            //  Цикл по нажатиям.
            for (int i = 0; i < count; i++)
            {
                //  Проверка необходимоти ожидания.
                if (i != 0)
                {
                    //  Ожидание.
                    await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                }

                //  Асинхронное выполнение.
                await SafeAsync(async delegate (CancellationToken cancellationToken)
                {
                    //  Выполнение в основном потоке.
                    await Entry.Unique.Invoker.InvokeAsync(delegate
                    {
                        SendMouseScroll(x, y, delta);
                    }, cancellationToken).ConfigureAwait(false);
                }, cancellationToken).ConfigureAwait(false);
            }
        }
        else
        {
            //  Асинхронное выполнение.
            await SafeAsync(async delegate (CancellationToken cancellationToken)
            {
                //  Выполнение в основном потоке.
                await Entry.Unique.Invoker.InvokeAsync(delegate
                {
                    //  Цикл по нажатиям.
                    for (int i = 0; i < count; i++)
                    {
                        SendMouseScroll(x, y, delta);
                    }
                }, cancellationToken).ConfigureAwait(false);
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет скрипт.
    /// </summary>
    /// <param name="script">
    /// Текст скрипта.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, вставляющая текст.
    /// </returns>
    public async Task ScriptAsync(string script, CancellationToken cancellationToken)
    {
        //  Асинхронное выполнение.
        await SafeAsync(async delegate (CancellationToken cancellationToken)
        {
            //  Выполнение в основном потоке.
            await Entry.Unique.Invoker.InvokeAsync(async delegate (CancellationToken cancellationToken)
            {
                //  Запуск скрипта.
                await _WebView!.ExecuteScriptAsync(script);
            }, cancellationToken).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }


    


    /// <summary>
    /// Поле для хранения генератора случайных чисел.
    /// </summary>
    private readonly Random _Random = new(unchecked((int)DateTime.Now.Ticks));

    private void SendMouseScroll(int x, int y, int delta)
    {
        //  Обновление.
        nint handle = GetTopHandle();

        //  Проверка дескриптора окна.
        if (handle != 0)
        {

            //  Определения параметра для отправки сообщений.
            nint lParam = MakeParam(x, y);

            //  Отправка собщений окну.
            SendMessage(handle, WM_MOUSEMOVE, 0, lParam);
            SendMessage(handle, WM_MOUSEWHEEL, MAKEWPARAM(0, delta), lParam);
        }
    }

    /// <summary>
    /// Отправляет команду нажатия мыши.
    /// </summary>
    private void SendClick(int x1, int y1, int x2, int y2)
    {
        //  Обновление.
        nint handle = GetTopHandle();

        //  Проверка дескриптора окна.
        if (handle != 0)
        {
            //  Получение координат на экране.
            System.Drawing.Point screenPoint = _WebView!.PointToScreen(new System.Drawing.Point(x1, y1));

            //  Получение ограничиающего прямоугольника окна.
            if (GetWindowRect(
                handle, out System.Drawing.Rectangle rectangle))
            {
                //  Корректировка координат.
                x1 = screenPoint.X - rectangle.Left + (int)((x2 - x1) * _Random.NextDouble());
                y1 = screenPoint.Y - rectangle.Top + (int)((y2 - y1) * _Random.NextDouble());

                //  Определения параметра для отправки сообщений.
                nint param = MakeParam(x1, y1);

                //  Отправка собщений окну.
                SendMessage(handle, WM_MOUSEMOVE, 0, param);
                SendMessage(handle, WM_LBUTTONDOWN, 0, param);
                SendMessage(handle, WM_LBUTTONUP, 0, param);
            }
        }
    }

    /// <summary>
    /// Формирует параметр события.
    /// </summary>
    private static IntPtr MakeParam(int low, int hight)
    {
        return (low & 0xFFFF) | (hight << 16);
    }

    private static LONG MAKELONG(int a, int b)
    {
        return ((LONG)(((WORD)(unchecked((short)a) & 0xffff)) | ((DWORD)((WORD)(unchecked((short)b) & 0xffff))) << 16));

        //return ((LONG)(((WORD)(((DWORD_PTR)(a)) & 0xffff)) | ((DWORD)((WORD)(((DWORD_PTR)(b)) & 0xffff))) << 16));
    }

    private static WPARAM MAKEWPARAM(int l, int h)
    {
        return ((WPARAM)(DWORD)MAKELONG(l, h));
    }


    private const int WM_MOUSEWHEEL = 0x020A;
    /// <summary>
    /// 
    /// </summary>
    private const int WM_MOUSEMOVE = 0x0200;

    /// <summary>
    /// 
    /// </summary>
    private const int WM_LBUTTONDOWN = 0x0201;

    /// <summary>
    /// 
    /// </summary>
    private const int WM_LBUTTONUP = 0x0202;

    /// <summary>
    /// Отправляет указанное сообщение в окно или окна.
    /// Метод вызывает процедуру окна для указанного окна и не возвращается,
    /// пока процедура окна не обработала сообщение.
    /// </summary>
    /// <param name="hWnd">
    /// Дескриптор окна, процедура окна которого получит сообщение.
    /// </param>
    /// <param name="msg">
    /// Отправляемое сообщение.
    /// </param>
    /// <param name="wParam">
    /// Дополнительные сведения, относящиеся к сообщению.
    /// </param>
    /// <param name="lParam">
    /// Дополнительные сведения, относящиеся к сообщению.
    /// </param>
    /// <returns>
    /// Возвращаемое значение указывает результат обработки сообщения; это зависит от отправленного сообщения.
    /// </returns>
    private static LRESULT SendMessage(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
    {
        //  Вызов функции.
        return SendMessageW(hWnd, msg, wParam, lParam);

        //  Метод прямого вызова.
        [DllImport("user32.dll")]
        static extern LRESULT SendMessageW(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);
    }


    /// <summary>
    /// Возвращает дескриптор верхнего элемента.
    /// </summary>
    /// <returns>
    /// Дескриптор верхнего элемента.
    /// </returns>
    private nint GetTopHandle()
    {
        //  Текущий дескриптор.
        nint topHandle = _Shell.Handle;

        //  Цикл перебора дескрипторов.
        while (true)
        {
            //  Получение следующего дескриптора.
            nint handle = GetTopWindow(topHandle);

            //  Проверка следующего дескриптора.
            if (handle == 0)
            {
                //  Завершение перебора.
                break;
            }

            //  Установка нового дескриптора.
            topHandle = handle;
        }

        //  Возврат дескриптор верхнего элемента.
        return topHandle;
    }

    /// <summary>
    /// Проверяет Z-порядок дочерних окон, связанных с указанным родительским окном,
    /// и извлекает маркер дочернего окна в верхней части Z-порядка.
    /// </summary>
    /// <param name="hWnd">
    /// Дескриптор родительского окна, дочерние окна которого должны быть проверены.
    /// </param>
    /// <returns>
    /// Если функция выполнена успешно, возвращаемым значением является дескриптор дочернего окна в верхней части порядка Z.
    /// Если указанное окно не имеет дочерних окон, возвращаемое значение равно NULL.
    /// </returns>
    private static HWND GetTopWindow(HWND hWnd)
    {
        //  Прямой вызов.
        return GetTopWindow(hWnd);

        //  Выполняет прямой вызов.
        [DllImport("user32.dll")]
        static extern HWND GetTopWindow(HWND hWnd);
    }

    /// <summary>
    /// Извлекает размеры ограничивающего прямоугольника указанного окна.
    /// Размеры задаются в координатах экрана относительно левого верхнего угла экрана.
    /// </summary>
    /// <param name="handle">
    /// Дескриптор окна.
    /// </param>
    /// <param name="rectangle">
    /// Ограничивающий прямоугольник.
    /// </param>
    /// <returns>
    /// Результат вызова.
    /// </returns>
    private static bool GetWindowRect(HWND handle, out RECT rectangle)
    {
        //  Вызов функции.
        return GetWindowRect(handle, out rectangle) != 0;

        //  Метод прямого вызова.
        [DllImport("user32.dll")]
        static extern BOOL GetWindowRect(HWND hWnd, out RECT lpRect);
    }


    //Marshal.StringToHGlobalUni(clipboardText)

    private static void PasteFromClipboard(IntPtr hWnd)
    {
        // Импортируем необходимые Win32 API функции
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetFocus(IntPtr hWnd);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        //[DllImport("user32.dll")]
        //static extern bool OpenClipboard(IntPtr hWndNewOwner);

        //[DllImport("user32.dll")]
        //static extern bool CloseClipboard();

        //// Константы для сообщений
        //const uint WM_PASTE = 0x0302;

        //// Открываем буфер обмена
        //if (OpenClipboard(IntPtr.Zero))
        //{
        //// Если в буфере обмена есть текст, вставляем его в окно
        //if (System.Windows.Forms.Clipboard.ContainsText())
        //{
        //    SetFocus(hWnd); // Фокусируемся на целевом окне
        //    SendMessage(hWnd, WM_PASTE, IntPtr.Zero, IntPtr.Zero); // Эмулируем вставку
        //}
        //// Если в буфере обмена есть изображение, можно вставить его аналогично
        //else if (System.Windows.Forms.Clipboard.ContainsImage())
        //{
        // Логика для вставки изображения (если нужно)
        SetFocus(hWnd); // Фокусируемся на окне
                        //SendMessage(hWnd, WM_PASTE, IntPtr.Zero, IntPtr.Zero); // Эмулируем вставку
                        //}
                        //    CloseClipboard(); // Закрываем буфер обмена
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Не удалось открыть буфер обмена.");
                        //}

        // Сообщения для работы с клавишами
        const uint VK_CONTROL = 0x11;
        const uint VK_V = 0x56;
        const uint KEYEVENTF_KEYDOWN = 0x0000;
        const uint KEYEVENTF_KEYUP = 0x0002;

        keybd_event((byte)VK_CONTROL, 0, KEYEVENTF_KEYDOWN, 0);
        keybd_event((byte)VK_V, 0, KEYEVENTF_KEYDOWN, 0);
        keybd_event((byte)VK_V, 0, KEYEVENTF_KEYUP, 0);
        keybd_event((byte)VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);


    }
}


*/