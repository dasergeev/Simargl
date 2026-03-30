using Simargl.AccelEth3T.AccelEth3TViewer.Nodes;

namespace Simargl.AccelEth3T.AccelEth3TViewer.UI;

/// <summary>
/// Представляет обозревателя.
/// </summary>
partial class Explorer
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public Explorer()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

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
