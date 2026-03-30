using Simargl.Embedded.Tunings.TuningsEditor.Core;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;

namespace Simargl.Embedded.Tunings.TuningsEditor.UI;

/// <summary>
/// Представляет обозреватель проекта.
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
    }

    /// <summary>
    /// Обрабатывает событие изменения выбранного элемента.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        //  Проверка сердца приложения.
        if (DataContext is Heart heart)
        {
            //  Установка узла.
            heart.SelectedNode = e.NewValue as Node;
        }
    }
}
