using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Simargl.UI.Nodes.Core;

/// <summary>
/// Представляет обозревателя.
/// </summary>
internal partial class Explorer : Simargl.UI.Nodes.Control
{
    private readonly TreeView _TreeView;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Explorer()
    {
        // Создание TreeView
        _TreeView = new TreeView
        {
            Name = "_TreeView",
            BorderThickness = new Thickness(0)
        };

        // Обработчики событий
        _TreeView.SelectedItemChanged += TreeView_SelectedItemChanged;
        EventManager.RegisterClassHandler(typeof(TreeViewItem), TreeViewItem.SelectedEvent, new RoutedEventHandler(TreeViewItem_Selected));
        EventManager.RegisterClassHandler(typeof(TreeViewItem), TreeViewItem.UnselectedEvent, new RoutedEventHandler(TreeViewItem_Unselected));
        EventManager.RegisterClassHandler(typeof(TreeViewItem), TreeViewItem.ExpandedEvent, new RoutedEventHandler(TreeViewItem_Expanded));
        EventManager.RegisterClassHandler(typeof(TreeViewItem), TreeViewItem.CollapsedEvent, new RoutedEventHandler(TreeViewItem_Collapsed));

        // Настройка ItemContainerStyle
        var itemContainerStyle = new Style(typeof(TreeViewItem));
        itemContainerStyle.Setters.Add(new Setter(TreeViewItem.IsSelectedProperty, new Binding("IsSelected")));
        itemContainerStyle.Setters.Add(new Setter(TreeViewItem.IsExpandedProperty, new Binding("IsExpanded")));
        _TreeView.ItemContainerStyle = itemContainerStyle;

        // Настройка ItemTemplate
        var hierarchicalDataTemplate = new HierarchicalDataTemplate
        {
            ItemsSource = new Binding("Nodes")
        };
        hierarchicalDataTemplate.VisualTree = new FrameworkElementFactory(typeof(ExplorerItem));
        _TreeView.ItemTemplate = hierarchicalDataTemplate;

        // Установка TreeView как содержимого окна
        Content = _TreeView;

        //  Проверка режима разработки.
        if (IsInDesignMode)
        {
            //  Завершение инициализации.
            return;
        }

        //  Установка источника данных.
        _TreeView.ItemsSource = Root.Nodes;
    }

    /// <summary>
    /// Обрабатывает событие изменения выбранного узла.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        //  Изменение выбранного элемента.
        Root.Selected = e.NewValue as Node;
    }

    /// <summary>
    /// Обрабатывает событие установки выбора узла.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
    {
        //  Получение узла.
        if (e.OriginalSource is TreeViewItem item &&
            item.DataContext is Node node)
        {
            //  Установка значения, определяющего выбран ли узел.
            node.IsSelected = true;
        }
    }

    /// <summary>
    /// Обрабатывает событие снятия выбора узла.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TreeViewItem_Unselected(object sender, RoutedEventArgs e)
    {
        //  Получение узла.
        if (e.OriginalSource is TreeViewItem item &&
            item.DataContext is Node node)
        {
            //  Установка значения, определяющего выбран ли узел.
            node.IsSelected = false;
        }
    }

    /// <summary>
    /// Обрабатывает событие разворачивания узла.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
    {
        //  Получение узла.
        if (e.OriginalSource is TreeViewItem item &&
            item.DataContext is Node node)
        {
            //  Установка значения, определяющего развёрнут ли узел.
            node.IsExpanded = true;
        }
    }

    /// <summary>
    /// Обрабатывает событие сворачивания узла.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TreeViewItem_Collapsed(object sender, RoutedEventArgs e)
    {
        //  Получение узла.
        if (e.OriginalSource is TreeViewItem item &&
            item.DataContext is Node node)
        {
            //  Установка значения, определяющего развёрнут ли узел.
            node.IsExpanded = false;
        }
    }
}
