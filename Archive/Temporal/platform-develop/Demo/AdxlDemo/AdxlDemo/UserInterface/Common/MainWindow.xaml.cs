namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface;

/// <summary>
/// Представляет основное окно приложения.
/// </summary>
public partial class MainWindow :
    Window
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public MainWindow()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Настройка события выбора нового узла.
        _Explorer.SelectedNodeChanged += (sender, e) =>
        {
            //  Передача объекта в элемент управления, отображающий свойства.
            _PropertiesView.SelectedObject = _Explorer.SelectedNode;

            //  Установка выбранного объекта в основном элементе управления.
            _MainControl.SetNode(_Explorer.SelectedNode);
        };
    }
}
