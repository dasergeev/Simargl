using Simargl.Web.Browsing.Core.Controls;
using System.Windows.Forms.Integration;

namespace Simargl.Web.Browsing;

/// <summary>
/// Представляет веб-браузер.
/// </summary>
public partial class WebBrowser : Simargl.UI.Control
{
    private readonly WindowsFormsHost _Host;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public WebBrowser()
    {

        _Host = new();
        Content = _Host;

        //  Проверка режима разработки.
        if (IsInDesignMode)
        {
            //  Установка заглушек.
            Manager = null!;

            //  Завершение инициализации.
            return;
        }

        //  Создание контейнера веб-элементов.
        WebContainer container = new()
        {
            //  Установка способа прикрепления элемента управления.
            Dock = System.Windows.Forms.DockStyle.Fill,

            //  Настройка фона.
            BackColor = System.Drawing.Color.Black,
        };

        //  Добавление контейнера.
        _Host.Child = container;

        //  Создание управляющего веб-содержимым.
        Manager = new(container);
    }

    /// <summary>
    /// Возвращает управляющего веб-содержимым.
    /// </summary>
    public WebManager Manager { get; }
}
