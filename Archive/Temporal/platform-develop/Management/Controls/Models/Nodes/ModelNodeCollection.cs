using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Apeiron.Platform.Management.Models.Nodes;

/// <summary>
/// Представляет коллекцию узлов модели.
/// </summary>
public sealed class ModelNodeCollection :
    IEnumerable<ModelNode>,
    INotifyCollectionChanged
{
    /// <summary>
    /// Происходит при изменении коллекции.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => ((INotifyCollectionChanged)_Items).CollectionChanged += value;
        remove => ((INotifyCollectionChanged)_Items).CollectionChanged -= value;
    }

    /// <summary>
    /// Поле для хранения списка элементов коллекции.
    /// </summary>
    private readonly ObservableCollection<ModelNode> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal ModelNodeCollection()
    {
        //  Создание списка элементов коллекции.
        _Items = new();
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => _Items.Count;

    /// <summary>
    /// Возвращает или задаёт элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns>
    /// Элемент с указанным индексом.
    /// </returns>
    internal ModelNode this[int index]
    {
        get => _Items[index];
        set => _Items[index] = value;
    }

    /// <summary>
    /// Добавляет узел в коллекцию.
    /// </summary>
    /// <param name="node">
    /// Узел, который необходимо добавить в коллекцию.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="node"/> передана пустая ссылка.
    /// </exception>
    public void Add(ModelNode node)
    {
        //  Проверка ссылки на узел.
        node = Check.IsNotNull(node, nameof(node));

        //  Добавление узла в коллекцию.
        _Items.Add(node);
    }

    /// <summary>
    /// Удаляет элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс удаляемого элемента.
    /// </param>
    internal void RemoveAt(int index) => _Items.RemoveAt(index);

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<ModelNode> GetEnumerator()
    {
        //  Возврат перечислителя списка элементов коллекции.
        return ((IEnumerable<ModelNode>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя списка элементов коллекции.
        return ((IEnumerable)_Items).GetEnumerator();
    }
}
