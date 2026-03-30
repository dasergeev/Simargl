using Simargl.UI.Nodes.Core;

namespace Simargl.UI.Nodes;

/// <summary>
/// Представляет главное окно узлов.
/// </summary>
public class MainWindow :
    UI.Window
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public MainWindow()
    {
        //  Создание основного элемента управления.
        MainControl mainControl = new();

        //  Добавление основного элемента управления.
        Content = mainControl;
    }
}
