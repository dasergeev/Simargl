using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;
using System.Windows.Input;

namespace Simargl.Embedded.Tunings.TuningsEditor.UI;

/// <summary>
/// Представляет элемент управления, отображающий узел в дереве обозревателя.
/// </summary>
partial class NodeItem
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public NodeItem()
    {
        //  инициализация основных компонентов.
        InitializeComponent();
    }

    /// <summary>
    /// Обрабатывает событие перемещения мыши.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Item_MouseMove(object sender, MouseEventArgs e)
    {
        //  Проверка необходимых условий для перетаскивания.
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            //  Получение узла раздела спецификации.
            if (DataContext is Node node &&
                node.IsMovable)
            {
                //  Создание данных.
                DataObject data = new(typeof(Node), node);

                //  Запуск операции перетаскивания.
                DragDrop.DoDragDrop(this, data, DragDropEffects.Move);
            }
        }

        //  Установка значения, определяющего, что событие обработано.
        e.Handled = true;
    }

    /// <summary>
    /// Обрабатывает событие перемещения операции перетаскивания.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Item_DragOver(object sender, DragEventArgs e)
    {
        //  Установка эффекта запрета.
        e.Effects = DragDropEffects.None;

        //  Установка значения, определяющего, что событие обработано.
        e.Handled = true;

        //  Получение принимающего узла.
        if (DataContext is not Node host ||
            host.Parent is not Node parentHost)
        {
            //  Завершение обработки.
            return;
        }

        //  Получение данных.
        if (e.Data.GetDataPresent(typeof(Node)) &&
            e.Data.GetData(typeof(Node)) is Node movable)
        {
            //  Проверка возможности перемещения.
            if (parentHost.CanContain(movable) ||
                host.CanContain(movable))
            {
                //  Установка эффекта перемещения.
                e.Effects = DragDropEffects.Move;
            }
        }
    }

    /// <summary>
    /// Обрабатывает событие передачи данных операции перетаскивания.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Item_Drop(object sender, DragEventArgs e)
    {
        //  Установка значения, определяющего, что событие обработано.
        e.Handled = true;

        //  Получение принимающего узла.
        if (DataContext is not Node host ||
            host.Parent is not Node parentHost)
        {
            //  Завершение обработки.
            return;
        }

        //  Получение данных.
        if (e.Data.GetDataPresent(typeof(Node)) &&
            e.Data.GetData(typeof(Node)) is Node movable)
        {
            //  Проверка возможности перемещения.
            if (host.CanContain(movable))
            {
                //  Удаление из текущей позиции.
                movable.Remove();

                //  Добавление в новую позицию.
                host.Nodes.Add(movable);
            }

            //  Проверка возможности перемещения в родительский узел.
            if (parentHost.CanContain(movable))
            {
                //  Проверка необходимости перемещения.
                if (!ReferenceEquals(host, movable))
                {
                    //  Получение текущего индекса.
                    int index = parentHost.Nodes.IndexOf(host);

                    //  Проверка текущего индекса.
                    if (index >= 0)
                    {
                        //  Удаление из текущей позиции.
                        movable.Remove();

                        //  Обновление текущего индекса.
                        index = parentHost.Nodes.IndexOf(host);

                        //  Вставка в новую позицию.
                        parentHost.Nodes.Insert(index, movable);
                    }
                }
            }
        }
    }
}
