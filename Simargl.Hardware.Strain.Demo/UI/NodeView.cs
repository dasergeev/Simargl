using Simargl.Hardware.Strain.Demo.Nodes;

namespace Simargl.Hardware.Strain.Demo.UI;

/// <summary>
/// Представляет элемент управления, отображающий узел.
/// </summary>
public abstract class NodeView :
    Control
{
    /// <summary>
    /// Происходит при изменении значения свойства <see cref="TargetNode"/>.
    /// </summary>
    public event EventHandler? TargetNodeChanged;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public NodeView()
    {
        //  Внутреннее обращение к объекту.
        Lay();

        //  Добавление обработчика события.
        SelectedNodeChanged += delegate (object? sender, EventArgs e)
        {
            //  Обновление целевого узла.
            UpdateTargetNode();
        };

        //  Обновление целевого узла.
        UpdateTargetNode();
    }

    /// <summary>
    /// Возвращает целевой узел.
    /// </summary>
    protected Node? TargetNode { get; private set; }

    /// <summary>
    /// Возвращает целевой тип узла.
    /// </summary>
    public abstract Type TargetNodeType { get; }

    /// <summary>
    /// Обновляет целевой узел.
    /// </summary>
    private void UpdateTargetNode()
    {
        //  Получение целевого узла.
        Node? selectedNode = SelectedNode;

        //  Проверка выбранного узла.
        if (selectedNode is not null)
        {
            //  Проверка типа выбранного узла.
            if (selectedNode.GetType() != TargetNodeType)
            {
                //  Сброс выбранного узла.
                selectedNode = null;
            }
        }

        //  Проверка изменения выбранного узла.
        if (!ReferenceEquals(TargetNode, selectedNode))
        {
            //  Установка выбранного узла.
            TargetNode = selectedNode;

            //  Установка контекста данных.
            DataContext = TargetNode;

            //  Вызов события об изменении значения свойства.
            Volatile.Read(ref TargetNodeChanged)?.Invoke(this, EventArgs.Empty);
        }
    }
}
