using Simargl.UI.Nodes.Core;

namespace Simargl.UI.Nodes;

/// <summary>
/// Представляет узел группы.
/// </summary>
public class GroupNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="parent">
    /// Родительский узел.
    /// </param>
    /// <param name="name">
    /// Имя узла.
    /// </param>
    internal GroupNode(Node? parent, string name) :
        base(parent, name)
    {

    }

    /// <summary>
    /// Добавляет узел.
    /// </summary>
    /// <typeparam name="TNode">
    /// Тип добавляемого узла.
    /// </typeparam>
    /// <returns>
    /// Добавленный узел.
    /// </returns>
    public TNode AddNode<TNode>()
        where TNode : Node
    {
        //  Создание узла.
        TNode node = Activator.CreateInstance(typeof(TNode), this) as TNode ??
            throw new InvalidOperationException("Не удалось создать узел.");

        //  Добавление узла.
        Provider.Add(node);

        //  Возврат нового узла.
        return node;
    }

    /// <summary>
    /// Добавляет новую группу.
    /// </summary>
    /// <param name="name">
    /// Имя группы.
    /// </param>
    /// <returns>
    /// Новая группа.
    /// </returns>
    public GroupNode AddGroup(string name)
    {
        //  Создание группы.
        GroupNode group = new(this, name);

        //  Добавление группы.
        Provider.Add(group);

        //  Возврат группы.
        return group;
    }

    /// <summary>
    /// Возвращает источник элемента управления, отображающего узел в дереве.
    /// </summary>
    public override sealed ControlSource ItemSource { get; } = NodeItemSimple.CreateControlSource(Images.Group);
}
