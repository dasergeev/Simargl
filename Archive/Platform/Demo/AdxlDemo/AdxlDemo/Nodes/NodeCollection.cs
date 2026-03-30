using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Apeiron.Platform.Demo.AdxlDemo.Nodes;

/// <summary>
/// Представляет коллекцию узлов.
/// </summary>
/// <typeparam name="TNode">
/// Тип узла коллекции.
/// </typeparam>
public sealed class NodeCollection<TNode> :
    Primary,
    INodeCollection,
    IEnumerable<TNode>
    where TNode : INode
{
    /// <summary>
    /// Происходит при изменении коллекции.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <summary>
    /// Поле для хранения элементов коллекции.
    /// </summary>
    private readonly ObservableCollection<TNode> _Nodes;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <param name="nodes">
    /// Хранилище элементов коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="nodes"/> передана пустая ссылка.
    /// </exception>
    public NodeCollection(Engine engine, ObservableCollection<TNode> nodes) :
        base(engine)
    {
        //  Установка хранилища элементов коллекции.
        _Nodes = IsNotNull(nodes, nameof(nodes));

        //  Подписка на события хранилища.
        _Nodes.CollectionChanged += (sender, e) => OnCollectionChanged(e);
        ((INotifyPropertyChanged)_Nodes).PropertyChanged += (sender, e) => OnPropertyChanged(e);
    }

    /// <summary>
    /// Возвращает количество узлов.
    /// </summary>
    public int Count => _Nodes.Count;

    /// <summary>
    /// Возвращает элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns>
    /// Элемент с указанным индексом.
    /// </returns>
    public TNode this[int index] => _Nodes[index];

    /// <summary>
    /// Вызывает событие <see cref="CollectionChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        //  Захват текущего делегата.
        NotifyCollectionChangedEventHandler? handler = Volatile.Read(ref CollectionChanged);

        //  Проверка ссылки на делегат.
        if (handler is not null)
        {
            //  Выполнение в основном потоке.
            Invoker.Primary(delegate
            {
                //  Вызов события.
                handler(this, e);
            });
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator<TNode> IEnumerable<TNode>.GetEnumerator() => _Nodes.GetEnumerator();

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => _Nodes.GetEnumerator();
}
