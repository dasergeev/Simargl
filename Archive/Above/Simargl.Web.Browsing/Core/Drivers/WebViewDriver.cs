using Simargl.Engine;
using Microsoft.Web.WebView2.WinForms;

namespace Simargl.Web.Browsing.Core.Drivers;

/// <summary>
/// Представляет драйвер объекта <see cref="WebView2"/>.
/// </summary>
/// <param name="target">
/// Целевой объект.
/// </param>
internal sealed class WebViewDriver(WebView2 target) :
    Something
{
    /// <summary>
    /// Возвращает целевой объект.
    /// </summary>
    public WebView2 Target { get; } = target;




    /*



    //
    // Сводка:
    //     Gets or sets a bag of options which are used during initialization of the control's
    //     Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2. This property cannot be
    //     modified (an exception will be thrown) after initialization of the control's
    //     CoreWebView2 has started.
    //
    // Исключения:
    //   T:System.InvalidOperationException:
    //     Thrown if initialization of the control's CoreWebView2 has already started.
    [Browsable(false)]
    public CoreWebView2CreationProperties CreationProperties
    {
        get
        {
            return _creationProperties;
        }
        set
        {
            if (_initTask != null)
            {
                throw new InvalidOperationException("CreationProperties cannot be modified after the initialization of CoreWebView2 has begun.");
            }

            _creationProperties = value;
        }
    }



    //
    // Сводка:
    //     The underlying CoreWebView2. Use this property to perform more operations on
    //     the WebView2 content than is exposed on the WebView2. This value is null until
    //     it is initialized and the object itself has undefined behaviour once the control
    //     is disposed. You can force the underlying CoreWebView2 to initialize via the
    //     Microsoft.Web.WebView2.WinForms.WebView2.EnsureCoreWebView2Async(Microsoft.Web.WebView2.Core.CoreWebView2Environment,Microsoft.Web.WebView2.Core.CoreWebView2ControllerOptions)
    //     method.
    //
    // Исключения:
    //   T:System.InvalidOperationException:
    //     Thrown if the calling thread isn't the thread which created this object (usually
    //     the UI thread). See System.Windows.Forms.Control.InvokeRequired for more info.
    public CoreWebView2 CoreWebView2
    {
        get
        {
            try
            {
                return _coreWebView2Controller?.CoreWebView2;
            }
            catch (Exception innerException)
            {
                if (base.InvokeRequired)
                {
                    throw new InvalidOperationException("CoreWebView2 can only be accessed from the UI thread.", innerException);
                }

                throw;
            }
        }
    }

    //
    // Сводка:
    //     The zoom factor for the WebView.
    public double ZoomFactor
    {
        get
        {
            if (_coreWebView2Controller != null)
            {
                return _coreWebView2Controller.ZoomFactor;
            }

            return _zoomFactor;
        }
        set
        {
            _zoomFactor = value;
            if (_coreWebView2Controller != null)
            {
                _coreWebView2Controller.ZoomFactor = value;
            }
        }
    }

    //
    // Сводка:
    //     Enable/disable external drop.
    public bool AllowExternalDrop
    {
        get
        {
            if (_coreWebView2Controller != null)
            {
                return _coreWebView2Controller.AllowExternalDrop;
            }

            return _allowExternalDrop;
        }
        set
        {
            _allowExternalDrop = value;
            if (_coreWebView2Controller != null)
            {
                _coreWebView2Controller.AllowExternalDrop = value;
            }
        }
    }

    //
    // Сводка:
    //     The Source property is the URI of the top level document of the WebView2. Setting
    //     the Source is equivalent to calling Microsoft.Web.WebView2.Core.CoreWebView2.Navigate(System.String).
    //     Setting the Source will trigger initialization of the Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2,
    //     if not already initialized. The default value of Source is null, indicating that
    //     the Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 is not yet initialized.
    //
    //
    // Исключения:
    //   T:System.ArgumentException:
    //     Specified value is not an absolute System.Uri.
    //
    //   T:System.NotImplementedException:
    //     Specified value is null and the control is initialized.
    [Browsable(true)]
    public Uri Source
    {
        get
        {
            return _source;
        }
        set
        {
            if (value == null)
            {
                if (!(_source == null))
                {
                    throw new NotImplementedException("Setting Source to null is not implemented yet.");
                }

                return;
            }

            if (!value.IsAbsoluteUri)
            {
                throw new ArgumentException("Only absolute URI is allowed", "Source");
            }

            if (IsInitialized && _source != null && _source.AbsoluteUri == value.AbsoluteUri)
            {
                return;
            }

            _source = value;
            if (!_inInit)
            {
                if (!IsInitialized)
                {
                    EnsureCoreWebView2Async();
                }
                else
                {
                    CoreWebView2.Navigate(value.AbsoluteUri);
                }
            }
        }
    }

    //
    // Сводка:
    //     Returns true if the webview can navigate to a next page in the navigation history
    //     via the Microsoft.Web.WebView2.WinForms.WebView2.GoForward method. This is equivalent
    //     to the Microsoft.Web.WebView2.Core.CoreWebView2.CanGoForward. If the underlying
    //     Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 is not yet initialized,
    //     this property is false.
    [Browsable(false)]
    public bool CanGoForward => CoreWebView2?.CanGoForward ?? false;

    //
    // Сводка:
    //     Returns true if the webview can navigate to a previous page in the navigation
    //     history via the Microsoft.Web.WebView2.WinForms.WebView2.GoBack method. This
    //     is equivalent to the Microsoft.Web.WebView2.Core.CoreWebView2.CanGoBack. If the
    //     underlying Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 is not yet initialized,
    //     this property is false.
    [Browsable(false)]
    public bool CanGoBack => CoreWebView2?.CanGoBack ?? false;

    //
    // Сводка:
    //     The default background color for the WebView.
    public Color DefaultBackgroundColor
    {
        get
        {
            if (_coreWebView2Controller != null)
            {
                return _coreWebView2Controller.DefaultBackgroundColor;
            }

            return _defaultBackgroundColor;
        }
        set
        {
            if (_coreWebView2Controller != null)
            {
                _coreWebView2Controller.DefaultBackgroundColor = value;
            }
            else
            {
                _defaultBackgroundColor = value;
            }
        }
    }


    //
    // Сводка:
    //     This event is triggered either 1) when the control's Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2
    //     has finished being initialized (regardless of how it was triggered or whether
    //     it succeeded) but before it is used for anything OR 2) the initialization failed.
    //     You should handle this event if you need to perform one time setup operations
    //     on the CoreWebView2 which you want to affect all of its usages (e.g. adding event
    //     handlers, configuring settings, installing document creation scripts, adding
    //     host objects).
    //
    // Примечания:
    //     This sender will be the WebView2 control, whose CoreWebView2 property will now
    //     be valid (i.e. non-null) for the first time if Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs.IsSuccess
    //     is true. Unlikely this event can fire second time (after reporting initialization
    //     success first) if the initialization is followed by navigation which fails.
    public event EventHandler<CoreWebView2InitializationCompletedEventArgs> CoreWebView2InitializationCompleted;

    //
    // Сводка:
    //     NavigationStarting dispatches before a new navigate starts for the top level
    //     document of the Microsoft.Web.WebView2.WinForms.WebView2. This is equivalent
    //     to the Microsoft.Web.WebView2.Core.CoreWebView2.NavigationStarting event.
    public event EventHandler<CoreWebView2NavigationStartingEventArgs> NavigationStarting;

    //
    // Сводка:
    //     NavigationCompleted dispatches after a navigate of the top level document completes
    //     rendering either successfully or not. This is equivalent to the Microsoft.Web.WebView2.Core.CoreWebView2.NavigationCompleted
    //     event.
    public event EventHandler<CoreWebView2NavigationCompletedEventArgs> NavigationCompleted;

    //
    // Сводка:
    //     WebMessageReceived dispatches after web content sends a message to the app host
    //     via chrome.webview.postMessage. This is equivalent to the Microsoft.Web.WebView2.Core.CoreWebView2.WebMessageReceived
    //     event.
    public event EventHandler<CoreWebView2WebMessageReceivedEventArgs> WebMessageReceived;

    //
    // Сводка:
    //     SourceChanged dispatches after the Microsoft.Web.WebView2.WinForms.WebView2.Source
    //     property changes. This may happen during a navigation or if otherwise the script
    //     in the page changes the URI of the document. This is equivalent to the Microsoft.Web.WebView2.Core.CoreWebView2.SourceChanged
    //     event.
    public event EventHandler<CoreWebView2SourceChangedEventArgs> SourceChanged;

    //
    // Сводка:
    //     ContentLoading dispatches after a navigation begins to a new URI and the content
    //     of that URI begins to render. This is equivalent to the Microsoft.Web.WebView2.Core.CoreWebView2.ContentLoading
    //     event.
    public event EventHandler<CoreWebView2ContentLoadingEventArgs> ContentLoading;

    //
    // Сводка:
    //     ZoomFactorChanged dispatches when the Microsoft.Web.WebView2.WinForms.WebView2.ZoomFactor
    //     property changes. This is equivalent to the Microsoft.Web.WebView2.Core.CoreWebView2Controller.ZoomFactorChanged
    //     event.
    public event EventHandler<EventArgs> ZoomFactorChanged;


    //
    // Сводка:
    //     Explicitly trigger initialization of the control's Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2.
    //
    //
    // Параметры:
    //   environment:
    //     A pre-created Microsoft.Web.WebView2.Core.CoreWebView2Environment that should
    //     be used to create the Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2.
    //     Creating your own environment gives you control over several options that affect
    //     how the Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 is initialized.
    //     If you pass null (the default value) then a default environment will be created
    //     and used automatically.
    //
    //   controllerOptions:
    //     A pre-created Microsoft.Web.WebView2.Core.CoreWebView2ControllerOptions that
    //     should be used to create the Microsoft.Web.WebView2.Core.CoreWebView2. Creating
    //     your own controller options gives you control over several options that affect
    //     how the Microsoft.Web.WebView2.Core.CoreWebView2 is initialized. If you pass
    //     a controllerOptions to this method then it will override any settings specified
    //     on the Microsoft.Web.WebView2.WinForms.WebView2.CreationProperties property.
    //     If you pass null (the default value) and no value has been set to Microsoft.Web.WebView2.WinForms.WebView2.CreationProperties
    //     then a default controllerOptions will be created and used automatically.
    //
    // Возврат:
    //     A Task that represents the background initialization process. When the task completes
    //     then the Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 property will
    //     be available for use (i.e. non-null). Note that the control's Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2InitializationCompleted
    //     event will be invoked before the task completes or on exceptions.
    //
    // Исключения:
    //   T:System.ArgumentException:
    //     Thrown if this method is called with a different environment than when it was
    //     initialized. See Remarks for more info.
    //
    //   T:System.InvalidOperationException:
    //     Thrown if this instance of Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2
    //     is already disposed, or if the calling thread isn't the thread which created
    //     this object (usually the UI thread). See System.Windows.Forms.Control.InvokeRequired
    //     for more info. May also be thrown if the browser process has crashed unexpectedly
    //     and left the control in an invalid state. We are considering throwing a different
    //     type of exception for this case in the future.
    //
    // Примечания:
    //     Unless previous initialization has already failed, calling this method additional
    //     times with the same parameter will have no effect (any specified environment
    //     is ignored) and return the same Task as the first call. Unless previous initialization
    //     has already failed, calling this method after initialization has been implicitly
    //     triggered by setting the Microsoft.Web.WebView2.WinForms.WebView2.Source property
    //     will have no effect if no environment is given and simply return a Task representing
    //     that initialization already in progress. Unless previous initialization has already
    //     failed, calling this method with a different environment after initialization
    //     has begun will result in an System.ArgumentException. For example, this can happen
    //     if you begin initialization by setting the Microsoft.Web.WebView2.WinForms.WebView2.Source
    //     property and then call this method with a new environment, if you begin initialization
    //     with Microsoft.Web.WebView2.WinForms.WebView2.CreationProperties and then call
    //     this method with a new environment, or if you begin initialization with one environment
    //     and then call this method with no environment specified. When this method is
    //     called after previous initialization has failed, it will trigger initialization
    //     of the control's Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 again.
    //     Note that even though this method is asynchronous and returns a Task, it still
    //     must be called on the UI thread like most public functionality of most UI controls.
    //
    //
    //     The following summarizes the possible error values and a description of why these
    //     errors occur.
    //
    //     Error Value Description
    //     HRESULT_FROM_WIN32(ERROR_NOT_SUPPORTED) *\\Edge\\Application* path used in browserExecutableFolder.
    //
    //     HRESULT_FROM_WIN32(ERROR_INVALID_STATE) Specified options do not match the options
    //     of the WebViews that are currently running in the shared browser process.
    //     HRESULT_FROM_WIN32(ERROR_INVALID_WINDOW_HANDLE) WebView2 Initialization failed
    //     due to an invalid host HWND parentWindow.
    //     HRESULT_FROM_WIN32(ERROR_DISK_FULL) WebView2 Initialization failed due to reaching
    //     the maximum number of installed runtime versions.
    //     HRESULT_FROM_WIN32(ERROR_PRODUCT_UNINSTALLED If the Webview depends upon an installed
    //     WebView2 Runtime version and it is uninstalled.
    //     HRESULT_FROM_WIN32(ERROR_FILE_NOT_FOUND) Could not find Edge installation.
    //     HRESULT_FROM_WIN32(ERROR_FILE_EXISTS) User data folder cannot be created because
    //     a file with the same name already exists.
    //     E_ACCESSDENIED Unable to create user data folder, Access Denied.
    //     E_FAIL Edge runtime unable to start.
    public Task EnsureCoreWebView2Async(CoreWebView2Environment environment = null, CoreWebView2ControllerOptions controllerOptions = null)
    {
        if (IsInDesignMode)
        {
            return Task.FromResult(0);
        }

        VerifyNotClosedGuard();
        VerifyBrowserNotCrashedGuard();
        if (base.InvokeRequired)
        {
            throw new InvalidOperationException("The method EnsureCoreWebView2Async can be invoked only from the UI thread.");
        }

        if (_initTask == null || _initTask.IsFaulted)
        {
            _initTask = InitCoreWebView2Async(environment, controllerOptions);
        }
        else
        {
            if ((!_isExplicitEnvironment && environment != null) || (_isExplicitEnvironment && environment != null && Environment != environment))
            {
                throw new ArgumentException("WebView2 was already initialized with a different CoreWebView2Environment. Check to see if the Source property was already set or EnsureCoreWebView2Async was previously called with different values.");
            }

            if ((!_isExplicitControllerOptions && controllerOptions != null) || (_isExplicitControllerOptions && controllerOptions != null && ControllerOptions != controllerOptions))
            {
                throw new ArgumentException("WebView2 was already initialized with a different CoreWebView2ControllerOptions. Check to see if the Source property was already set or EnsureCoreWebView2Async was previously called with different values.");
            }
        }

        return _initTask;
    }

    //
    // Сводка:
    //     Explicitly trigger initialization of the control's Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2.
    //
    //
    // Параметры:
    //   environment:
    //     A pre-created Microsoft.Web.WebView2.Core.CoreWebView2Environment that should
    //     be used to create the Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2.
    //     Creating your own environment gives you control over several options that affect
    //     how the Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 is initialized.
    //     If you pass null then a default environment will be created and used automatically.
    //
    //
    // Возврат:
    //     A Task that represents the background initialization process. When the task completes
    //     then the Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 property will
    //     be available for use (i.e. non-null). Note that the control's Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2InitializationCompleted
    //     event will be invoked before the task completes or on exceptions.
    //
    // Исключения:
    //   T:System.ArgumentException:
    //     Thrown if this method is called with a different environment than when it was
    //     initialized. See Remarks for more info.
    //
    //   T:System.InvalidOperationException:
    //     Thrown if this instance of Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2
    //     is already disposed, or if the calling thread isn't the thread which created
    //     this object (usually the UI thread). See System.Windows.Forms.Control.InvokeRequired
    //     for more info. May also be thrown if the browser process has crashed unexpectedly
    //     and left the control in an invalid state. We are considering throwing a different
    //     type of exception for this case in the future.
    //
    // Примечания:
    //     Unless previous initialization has already failed, calling this method additional
    //     times with the same parameter will have no effect (any specified environment
    //     is ignored) and return the same Task as the first call. Unless previous initialization
    //     has already failed, calling this method after initialization has been implicitly
    //     triggered by setting the Microsoft.Web.WebView2.WinForms.WebView2.Source property
    //     will have no effect if no environment is given and simply return a Task representing
    //     that initialization already in progress. Unless previous initialization has already
    //     failed, calling this method with a different environment after initialization
    //     has begun will result in an System.ArgumentException. For example, this can happen
    //     if you begin initialization by setting the Microsoft.Web.WebView2.WinForms.WebView2.Source
    //     property and then call this method with a new environment, if you begin initialization
    //     with Microsoft.Web.WebView2.WinForms.WebView2.CreationProperties and then call
    //     this method with a new environment, or if you begin initialization with one environment
    //     and then call this method with no environment specified. When this method is
    //     called after previous initialization has failed, it will trigger initialization
    //     of the control's Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 again.
    //     Note that even though this method is asynchronous and returns a Task, it still
    //     must be called on the UI thread like most public functionality of most UI controls.
    public Task EnsureCoreWebView2Async(CoreWebView2Environment environment)
    {
        return EnsureCoreWebView2Async(environment, null);
    }



    //
    // Сводка:
    //     Executes the provided script in the top level document of the Microsoft.Web.WebView2.WinForms.WebView2.
    //     This is equivalent to Microsoft.Web.WebView2.Core.CoreWebView2.ExecuteScriptAsync(System.String).
    //
    //
    // Исключения:
    //   T:System.InvalidOperationException:
    //     The underlying Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 is not yet
    //     initialized.
    //
    //   T:System.InvalidOperationException:
    //     Thrown when browser process has unexpectedly and left this control in an invalid
    //     state. We are considering throwing a different type of exception for this case
    //     in the future.
    public async Task<string> ExecuteScriptAsync(string script)
    {
        VerifyInitializedGuard();
        VerifyBrowserNotCrashedGuard();
        return await CoreWebView2.ExecuteScriptAsync(script);
    }

    //
    // Сводка:
    //     Reloads the top level document of the Microsoft.Web.WebView2.WinForms.WebView2.
    //     This is equivalent to Microsoft.Web.WebView2.Core.CoreWebView2.Reload.
    //
    // Исключения:
    //   T:System.InvalidOperationException:
    //     The underlying Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 is not yet
    //     initialized.
    //
    //   T:System.InvalidOperationException:
    //     Thrown when browser process has unexpectedly and left this control in an invalid
    //     state. We are considering throwing a different type of exception for this case
    //     in the future.
    public void Reload()
    {
        VerifyInitializedGuard();
        VerifyBrowserNotCrashedGuard();
        CoreWebView2.Reload();
    }

    //
    // Сводка:
    //     Navigates to the next page in navigation history. This is equivalent to Microsoft.Web.WebView2.Core.CoreWebView2.GoForward.
    //     If the underlying Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 is not
    //     yet initialized, this method does nothing.
    public void GoForward()
    {
        CoreWebView2?.GoForward();
    }

    //
    // Сводка:
    //     Navigates to the previous page in navigation history. This is equivalent to Microsoft.Web.WebView2.Core.CoreWebView2.GoBack.
    //     If the underlying Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 is not
    //     yet initialized, this method does nothing.
    public void GoBack()
    {
        CoreWebView2?.GoBack();
    }

    //
    // Сводка:
    //     Renders the provided HTML as the top level document of the Microsoft.Web.WebView2.WinForms.WebView2.
    //     This is equivalent to Microsoft.Web.WebView2.Core.CoreWebView2.NavigateToString(System.String).
    //
    //
    // Исключения:
    //   T:System.InvalidOperationException:
    //     The underlying Microsoft.Web.WebView2.WinForms.WebView2.CoreWebView2 is not yet
    //     initialized.
    //
    //   T:System.InvalidOperationException:
    //     Thrown when browser process has unexpectedly and left this control in an invalid
    //     state. We are considering throwing a different type of exception for this case
    //     in the future.
    //
    // Примечания:
    //     The htmlContent parameter may not be larger than 2 MB (2 * 1024 * 1024 bytes)
    //     in total size. The origin of the new page is about:blank.
    public void NavigateToString(string htmlContent)
    {
        VerifyInitializedGuard();
        VerifyBrowserNotCrashedGuard();
        CoreWebView2.NavigateToString(htmlContent);
    }



    */

}
