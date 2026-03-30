using System.Windows.Threading;

namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет дочерний узел проекта.
/// </summary>
/// <typeparam name="TParentNode">
/// Тип родительского узла.
/// </typeparam>
public abstract class ChildNode<TParentNode> :
    Node
    where TParentNode : Node
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="dispatcher">
    /// Диспетчер.
    /// </param>
    /// <param name="name">
    /// Имя узла.
    /// </param>
    /// <param name="parent">
    /// Родительский узел.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="dispatcher"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="name"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="parent"/> передана пустая ссылка.
    /// </exception>
    public ChildNode(Dispatcher dispatcher, string name, TParentNode parent) :
        base(dispatcher, name)
    {
        //  Установка родительского узла.
        Parent = Check.IsNotNull(parent, nameof(parent));
    }

    /// <summary>
    /// Возвращает родительский узел.
    /// </summary>
    public TParentNode Parent { get; }
}
