using System.Windows.Threading;

namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет узел дерева проекта, представляющий каталог файловой системы.
/// </summary>
/// <typeparam name="TParentNode">
/// Тип родительского узла.
/// </typeparam>
public abstract class Directory<TParentNode> :
    ChildNode<TParentNode>
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
    /// <param name="path">
    /// Путь к каталогу.
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
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="path"/> передана пустая ссылка.
    /// </exception>
    public Directory(Dispatcher dispatcher, string name, TParentNode parent, string path) :
        base(dispatcher, name, parent)
    {
        //  Установка пути к каталогу.
        Path = Check.IsNotNull(path, nameof(path));
    }

    /// <summary>
    /// Возвращает путь к каталогу.
    /// </summary>
    public string Path { get; }
}
