using System.Windows;

namespace Simargl.UI.Nodes.Core;

/// <summary>
/// Представляет элемент дерева проводника.
/// </summary>
internal sealed class ExplorerItem :
    Control
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public ExplorerItem()
    {
        //  Проверка режима разработки.
        if (IsInDesignMode)
        {
            //  Завершение инициализации.
            return;
        }

        //  Добавление обработчика события.
        DataContextChanged += ExplorerItem_DataContextChanged;
    }

    /// <summary>
    /// Обрабатывает событие изменения значения свойства <see cref="FrameworkElement.DataContext"/>.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void ExplorerItem_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        //  Проверка данных.
        if (DataContext is Node node &&
            node.ItemSource.CreateInstance() is NodeItem nodeItem)
        {
            //  Настройка контекста данных.
            nodeItem.DataContext = node;

            //  Установка содержимого.
            Content = nodeItem;
        }
        else
        {
            //  Сброс содержимого.
            Content = null;
        }
    }
}
