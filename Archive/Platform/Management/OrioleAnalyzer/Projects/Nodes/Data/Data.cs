using System.Windows.Threading;

namespace Apeiron.Oriole.Analysis.Projects;

/// <summary>
/// Представляет узел дерева проекта, представляющий данные.
/// </summary>
/// <typeparam name="TParentNode">
/// Тип родительского узла.
/// </typeparam>
public abstract class Data<TParentNode> :
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
    /// <param name="position">
    /// Положение в файле.
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
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="position"/> передано отрицательное значение.
    /// </exception>
    public Data(Dispatcher dispatcher, string name, TParentNode parent, int position) :
        base(dispatcher, name, parent)
    {
        //  Установка времени начала записи в файл.
        Position = Check.IsNotNegative(position, nameof(position));
    }

    /// <summary>
    /// Возвращает положение в файле.
    /// </summary>
    public int Position { get; }
}
