using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;

/// <summary>
/// Представляет коллекцию узлов.
/// </summary>
/// <param name="parent">
/// Родительский узел.
/// </param>
public sealed class NodeCollection(Node parent) :
    IEnumerable<Node>,
    INotifyCollectionChanged
{
    private readonly ObservableCollection<Node> provider = [];

    /// <summary>
    /// Происходит при изменении коллекции.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => ((INotifyCollectionChanged)provider).CollectionChanged += value;
        remove => ((INotifyCollectionChanged)provider).CollectionChanged -= value;
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => provider.Count;

    /// <summary>
    /// Возвращает индекс элемента.
    /// </summary>
    /// <param name="item">
    /// Элемент, для которого необходимо определить индекс.
    /// </param>
    /// <returns>
    /// Индекс элемента или -1.
    /// </returns>
    public int IndexOf(Node item)
    {
        //  Обращение к поставщику.
        return provider.IndexOf(item);
    }

    /// <summary>
    /// Удаляет элемент.
    /// </summary>
    /// <param name="item">
    /// Удаляемый элемент.
    /// </param>
    public void Remove(Node item)
    {
        //  Проверка возможности удаления.
        if (!item.IsMovable)
        {
            //  Выброс исклчения.
            throw new InvalidOperationException("Произошла ошибка при удалении узла.");
        }

        //  Удаление из поставщика коллекции.
        provider.Remove(item);

        //  Сброс родителя.
        item.Parent = null;
    }

    /// <summary>
    /// Вставляет элемент.
    /// </summary>
    /// <param name="item">
    /// Элемент для вставки.
    /// </param>
    public void Add(Node item)
    {
        //  Вставка в поставщика.
        Insert(Count, item);
    }

    /// <summary>
    /// Вставляет элемент.
    /// </summary>
    /// <param name="index">
    /// Индекс места вставки.
    /// </param>
    /// <param name="item">
    /// Элемент для вставки.
    /// </param>
    public void Insert(int index, Node item)
    {
        //  Проверка возможности вставки.
        if (!parent.CanContain(item))
        {
            //  Выброс исклчения.
            throw new InvalidOperationException("Произошла ошибка при добавлении узла.");
        }

        //  Вставка в поставщика.
        provider.Insert(index, item);

        //  Установка родителя.
        item.Parent = parent;
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Node> GetEnumerator()
    {
        //  Возврат перечислителя списка элементов.
        return ((IEnumerable<Node>)provider).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя списка элементов.
        return ((IEnumerable)provider).GetEnumerator();
    }
}
