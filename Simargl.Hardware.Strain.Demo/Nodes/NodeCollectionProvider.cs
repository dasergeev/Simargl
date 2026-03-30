using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Simargl.Hardware.Strain.Demo.Nodes;

/// <summary>
/// Представляет поставщика коллекции узлов.
/// </summary>
public sealed class NodeCollectionProvider :
    Anything,
    IEnumerable<Node>,
    INotifyCollectionChanged,
    INotifyPropertyChanged
{
    /// <summary>
    /// Поле для хранения аргументов события изменения значения свойства <see cref="Count"/>.
    /// </summary>
    private static readonly PropertyChangedEventArgs _CountChangedArgs = new(nameof(Count));

    /// <summary>
    /// Происходит при изменении коллекции.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения списка элементов.
    /// </summary>
    private readonly ObservableCollection<Node> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="parent">
    /// Возвращает родительский узел.
    /// </param>
    internal NodeCollectionProvider([NoVerify] Node parent)
    {
        //  Обращение к объекту.
        Lay();

        //  Создание списка элементов.
        _Items = [];

        //  Установка родительского узла.
        Parent = parent;

        //  Настройка события изменения коллекции.
        _Items.CollectionChanged += delegate (object? sender, NotifyCollectionChangedEventArgs e)
        {
            //  Вызов события.
            Volatile.Read(ref CollectionChanged)?.Invoke(this, e);

            //  Обновление количества элементов в коллекции.
            updateCount();
        };

        //  Настройка события изменения значения свойства.
        ((INotifyPropertyChanged)_Items).PropertyChanged += delegate (object? sender, PropertyChangedEventArgs e)
        {
            //  Обновление количества элементов в коллекции.
            updateCount();
        };

        //  Обновляет количество элементов в коллекции.
        void updateCount()
        {
            //  Проверка изменения количества элементов.
            if (Count != _Items.Count)
            {
                //  Установка нового значения.
                Count = _Items.Count;

                //  Вызов события.
                Volatile.Read(ref PropertyChanged)?.Invoke(this, _CountChangedArgs);
            }
        }
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Возвращает узел с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс узла.
    /// </param>
    /// <returns>
    /// Узел с указанным индексом.
    /// </returns>
    public Node this[int index] => _Items[index];

    /// <summary>
    /// Возвращает родительский узел.
    /// </summary>
    internal Node Parent { get; }

    /// <summary>
    /// Проверяет содержится ли данный узел в коллекции.
    /// </summary>
    /// <param name="node">
    /// Проверяемый узел.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="node"/> передана пустая ссылка.
    /// </exception>
    public bool Contains(Node node)
    {
        //  Проверка ссылки.
        IsNotNull(node);
        
        //  Проверка.
        return _Items.Contains(node);
    }

    ///// <summary>
    ///// Определяет индекс узла в коллекции.
    ///// </summary>
    ///// <param name="node"></param>
    ///// <returns></returns>
    //public int IndexOf(Node node)
    //{
    //    Array.FindIndex
    //}

    /// <summary>
    /// Добавляет узел.
    /// </summary>
    /// <param name="node">
    /// Узел, который необходимо добавить.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="node"/> передана пустая ссылка.
    /// </exception>
    public void Add(Node node)
    {
        //  Проверка ссылки.
        IsNotNull(node);

        //  Добавление в список.
        _Items.Add(node);
    }

    /// <summary>
    /// Вставляет узел.
    /// </summary>
    /// <param name="index">
    /// Индекс места вставки.
    /// </param>
    /// <param name="node">
    /// Вставляемый узел.
    /// </param>
    public void Insert(int index, Node node)
    {
        //  Вставка узла в список.
        _Items.Insert(index, node);
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Node> GetEnumerator()
    {
        //  Возврат перечислителя списка.
        return ((IEnumerable<Node>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя списка.
        return ((IEnumerable)_Items).GetEnumerator();
    }
}
