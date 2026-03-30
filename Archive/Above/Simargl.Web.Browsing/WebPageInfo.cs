using Simargl.Engine;
using Simargl.Web.Proxies;
using System.ComponentModel;

namespace Simargl.Web.Browsing;

/// <summary>
/// Представляет информацию о веб-странице.
/// </summary>
public sealed class WebPageInfo :
    Something,
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Происходит при сбое навигации.
    /// </summary>
    public event EventHandler? NavigationFailed;

    /// <summary>
    /// Поле для хранения значения, определяющего находится ли страница в действительном состоянии.
    /// </summary>
    private volatile bool _IsValid = false;

    /// <summary>
    /// Поле для хранения значения, определяющего выполняет ли страница предписания.
    /// </summary>
    private volatile bool _IsExecuted = false;

    /// <summary>
    /// Поле для хранения источника текущей страницы.
    /// </summary>
    private volatile string? _Source = null;

    /// <summary>
    /// Поле для хранения информации о прокси-сервере.
    /// </summary>
    private volatile WebProxyInfo? _WebProxyInfo = null;

    /// <summary>
    /// Поле для хранения пути к каталогу с пользовательскими данными.
    /// </summary>
    private volatile string? _DataPath = null;

    /// <summary>
    /// Поле для хранения коэффициента масштабирования.
    /// </summary>
    private double _ZoomFactor = 1;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    internal WebPageInfo()
    {

    }

    /// <summary>
    /// Возвращает значение, определяющее находится ли страница в действительном состоянии.
    /// </summary>
    public bool IsValid => _IsValid;

    /// <summary>
    /// Возвращает значение, определяющее выполняет ли страница предписания.
    /// </summary>
    public bool IsExecuted => _IsExecuted;

    /// <summary>
    /// Возвращает значение, определяющее выполняет ли страница предписания.
    /// </summary>
    public bool IsNotExecuted => !_IsExecuted;

    /// <summary>
    /// Возвращает источник текущей страницы.
    /// </summary>
    public string? Source => _Source;

    /// <summary>
    /// Возвращает информацию о прокси-сервере.
    /// </summary>
    public WebProxyInfo? WebProxyInfo => _WebProxyInfo;

    /// <summary>
    /// Возвращает путь к каталогу с пользовательскими данными.
    /// </summary>
    public string? DataPath => _DataPath;

    /// <summary>
    /// Возвращает коэффициент масштабирования.
    /// </summary>
    public double ZoomFactor => Volatile.Read(ref _ZoomFactor);

    /// <summary>
    /// Вызывает событие <see cref="NavigationFailed"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    internal void OnNavigationFailed(EventArgs e)
    {
        //  Отправка действия для выполнения в основном потоке.
        Entry.Invoker.Send(delegate
        {
            //  Вызов события.
            Volatile.Read(ref NavigationFailed)?.Invoke(this, e);
        });
    }

    /// <summary>
    /// Асинхронно устанавливает значение свойтва <see cref="IsValid"/>.
    /// </summary>
    /// <param name="value">
    /// Новое значение свойства.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая значение свойства.
    /// </returns>
    internal async Task SetIsValidAsync(bool value)
    {
        //  Выполнение в основном потоке.
        await Entry.Invoker.InvokeAsync(delegate
        {
            //  Проверка изменения занчения.
            if (_IsValid != value)
            {
                //  Установка нового значения.
                _IsValid = value;

                //  Вызов события об изменении значения свойства.
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(IsValid)));
            }
        }, Entry.CancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно устанавливает значение свойтва <see cref="IsExecuted"/>.
    /// </summary>
    /// <param name="value">
    /// Новое значение свойства.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая значение свойства.
    /// </returns>
    internal async Task SetIsExecutedAsync(bool value)
    {
        //  Выполнение в основном потоке.
        await Entry.Invoker.InvokeAsync(delegate
        {
            //  Проверка изменения занчения.
            if (_IsExecuted != value)
            {
                //  Установка нового значения.
                _IsExecuted = value;

                //  Вызов события об изменении значения свойства.
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(IsExecuted)));
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(IsNotExecuted)));
            }
        }, Entry.CancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно устанавливает значение свойтва <see cref="Source"/>.
    /// </summary>
    /// <param name="value">
    /// Новое значение свойства.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая значение свойства.
    /// </returns>
    internal async Task SetSourceAsync(string? value)
    {
        //  Выполнение в основном потоке.
        await Entry.Invoker.InvokeAsync(delegate
        {
            //  Проверка изменения занчения.
            if (_Source != value)
            {
                //  Установка нового значения.
                _Source = value;

                //  Вызов события об изменении значения свойства.
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(Source)));
            }
        }, Entry.CancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно устанавливает значение свойтва <see cref="WebProxyInfo"/>.
    /// </summary>
    /// <param name="value">
    /// Новое значение свойства.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая значение свойства.
    /// </returns>
    internal async Task SetWebProxyInfoAsync(WebProxyInfo? value)
    {
        //  Выполнение в основном потоке.
        await Entry.Invoker.InvokeAsync(delegate
        {
            //  Проверка изменения занчения.
            if (_WebProxyInfo != value)
            {
                //  Установка нового значения.
                _WebProxyInfo = value;

                //  Вызов события об изменении значения свойства.
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(WebProxyInfo)));
            }
        }, Entry.CancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно устанавливает значение свойтва <see cref="DataPath"/>.
    /// </summary>
    /// <param name="value">
    /// Новое значение свойства.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая значение свойства.
    /// </returns>
    internal async Task SetDataPathAsync(string? value)
    {
        //  Выполнение в основном потоке.
        await Entry.Invoker.InvokeAsync(delegate
        {
            //  Проверка изменения занчения.
            if (_DataPath != value)
            {
                //  Установка нового значения.
                _DataPath = value;

                //  Вызов события об изменении значения свойства.
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(DataPath)));
            }
        }, Entry.CancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно устанавливает значение свойтва <see cref="ZoomFactor"/>.
    /// </summary>
    /// <param name="value">
    /// Новое значение свойства.
    /// </param>
    /// <returns>
    /// Задача, устанавливающая значение свойства.
    /// </returns>
    internal async Task SetZoomFactorAsync(double value)
    {
        //  Выполнение в основном потоке.
        await Entry.Invoker.InvokeAsync(delegate
        {
            //  Проверка изменения занчения.
            if (Volatile.Read(ref _ZoomFactor) != value)
            {
                //  Установка нового значения.
                Volatile.Write(ref _ZoomFactor, value);

                //  Вызов события об изменении значения свойства.
                Volatile.Read(ref PropertyChanged)?.Invoke(this, new(nameof(ZoomFactor)));
            }
        }, Entry.CancellationToken).ConfigureAwait(false);
    }
}
