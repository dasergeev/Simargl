using Microsoft.Web.WebView2.WinForms;

namespace Simargl.Web.Browsing.Core.Controls;

/// <summary>
/// Представляет оболочку вокруг веб-элемента.
/// </summary>
internal sealed class WebShell :
    System.Windows.Forms.UserControl
{
    /// <summary>
    /// Поле для хранения родительского элемента управления.
    /// </summary>
    private readonly System.Windows.Forms.UserControl _Parent;

    /// <summary>
    /// Поле для хранения дочернего элемента управления.
    /// </summary>
    private System.Windows.Forms.UserControl? _Child;

    /// <summary>
    /// Поле для хранения веб-элемента.
    /// </summary>
    private volatile WebView2? _WebView;

    /// <summary>
    /// Поле для хранения размера веб-элемента.
    /// </summary>
    private System.Drawing.Size? _WebSize;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public WebShell()
    {
        //  Установка веб-элемента.
        _WebView = null;

        //  Утановка размера веб-элемента.
        _WebSize = null;

        //  Установка цвета фона.
        BackColor = System.Drawing.Color.Black;

        //  Создание родительского элемента управления.
        _Parent = new()
        {
            BackColor = System.Drawing.Color.Green,
        };

        //  Добавление родительского элемента управления.
        Controls.Add(_Parent);
    }

    /// <summary>
    /// Прикрепляет веб-элемент.
    /// </summary>
    /// <param name="webView">
    /// Прикрепляемый веб-элемент.
    /// </param>
    public void Attach(WebView2 webView)
    {
        //  Открепление предыдущего веб-элемента.
        Detach();

        //  Создание дочернего элемента.
        _Child = new()
        {
            Dock = System.Windows.Forms.DockStyle.Fill,
        };

        //  Настройка веб-элемента.
        webView.Dock = System.Windows.Forms.DockStyle.Fill;

        //  Добавление веб-элемента.
        _Child.Controls.Add(webView);

        //  Добавление дочернего элемента.
        _Parent.Controls.Add(_Child);

        //  Установка ссылки на новый веб-элемент.
        _WebView = webView;
    }

    /// <summary>
    /// Открепляет веб-элемент.
    /// </summary>
    public void Detach()
    {
        //  Проверка дочернего элемента.
        if (_Child is not null)
        {
            //  Проверка текущего значения.
            if (_WebView is not null)
            {
                //  Удаление текущего веб-элемента.
                _Child.Controls.Remove(_WebView);

                //  Сброс ссылки на текущий веб-элемент.
                _WebView = null;
            }

            //  Удаление дочернего элемента.
            _Parent.Controls.Remove(_Child);

            //  Сброс ссылки на дочерный элемент.
            _Child = null;
        }
    }

    /// <summary>
    /// Возвращает текущее положение мыши.
    /// </summary>
    /// <returns>
    /// Текущее положение мыши.
    /// </returns>
    public System.Drawing.Point GetMouseLocation()
    {
        //  Возврат текущего положения мыши.
        return _Parent.PointToClient(MousePosition);
    }

    /// <summary>
    /// Возвращает границы веб-содержимого на экране.
    /// </summary>
    /// <returns>
    /// Границы веб-содержимого на экране.
    /// </returns>
    public System.Drawing.Rectangle GetBoundsScreen()
    {
        //  Возврат границ веб-содержимого на экране.
        return new(_Parent.PointToScreen(new(0, 0)), _Parent.Size);
    }

    /// <summary>
    /// Устанавливает размер веб-элемента.
    /// </summary>
    /// <param name="width">
    /// Ширина веб-элемента.
    /// </param>
    /// <param name="height">
    /// Высота веб-элемента.
    /// </param>
    public void SetWebSize(int width, int height)
    {
        //  Установка размера.
        _WebSize = new(width, height);

        //  Обновление размера веб-элемента.
        UpdateSize();
    }

    /// <summary>
    /// Сбрасывает размер веб-элемента.
    /// </summary>
    /// <remarks>
    /// Веб-элемент будет занимать всё клиентское пространство.
    /// </remarks>
    public void ResetWebSize()
    {
        //  Установка размера.
        _WebSize = null;

        //  Обновление размера веб-элемента.
        UpdateSize();
    }

    /// <summary>
    /// Вызывает событие <see cref="System.Windows.Forms.Control.Resize"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override void OnResize(EventArgs e)
    {
        //  Вызов метода базового класса.
        base.OnResize(e);

        //  Обновление размера веб-элемента.
        UpdateSize();
    }

    /// <summary>
    /// Обновляет размер веб-элемента.
    /// </summary>
    private void UpdateSize()
    {
        //  Определение размера.
        int width = _WebSize.HasValue ? _WebSize.Value.Width : -1;
        int height = _WebSize.HasValue ? _WebSize.Value.Height : -1;

        //  Нормализация размера.
        if (width <= 0) width = ClientSize.Width;
        if (height <= 0) height = ClientSize.Height;

        //  Установка границ основного компонента.
        _Parent.SetBounds(
            ClientSize.Width - width >> 1,
            ClientSize.Height - height >> 1,
            width, height);
    }
}
