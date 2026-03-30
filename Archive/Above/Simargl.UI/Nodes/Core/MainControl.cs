using System.Windows;
using System.Windows.Controls;

namespace Simargl.UI.Nodes.Core;

/// <summary>
/// Представляет главный элемент управления.
/// </summary>
internal class MainControl : Simargl.UI.Nodes.Control
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public MainControl()
    {
        ////  Инициализация основных компонентов.
        //InitializeComponent();


        // Создание Grid
        var grid = new Grid();

        // Добавление столбцов
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4.5, GridUnitType.Star) });

        // Добавление строк
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        // GridSplitter
        var gridSplitter = new GridSplitter
        {
            Width = 4,
            //HorizontalAlignment = HorizontalAlignment.Stretch,
            //VerticalAlignment = VerticalAlignment.Stretch
        };
        Grid.SetRowSpan(gridSplitter, 2);
        Grid.SetRow(gridSplitter, 1);

        // ControlPanel
        var controlPanel = new Simargl.UI.Nodes.Core.ControlPanel();
        Grid.SetColumnSpan(controlPanel, 2);

        // Explorer
        var explorer = new Simargl.UI.Nodes.Core.Explorer
        {
            Margin = new Thickness(0, 0, 4, 0)
        };
        Grid.SetRowSpan(explorer, 2);
        Grid.SetRow(explorer, 1);

        // Workspace
        var workspace = new Simargl.UI.Nodes.Core.Workspace();
        Grid.SetColumn(workspace, 1);
        Grid.SetRow(workspace, 1);

        // Добавление элементов в Grid
        grid.Children.Add(gridSplitter);
        grid.Children.Add(controlPanel);
        grid.Children.Add(explorer);
        grid.Children.Add(workspace);

        // Установка содержимого контрола
        this.Content = grid;

        //  Проверка режима разработки.
        if (IsInDesignMode)
        {
            //  Завершение инициализации.
            return;
        }
    }
}
