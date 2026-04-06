using System.Collections.Specialized;
using System.ComponentModel;

namespace Simargl.Hardware.Strain.Demo.Nodes;

/// <summary>
/// Представляет коллекию узлов.
/// </summary>
public sealed class NodeCollection :
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
    /// Поле для хранения поставщика коллекции.
    /// </summary>
    private readonly NodeCollectionProvider _Provider;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="provider">
    /// Поставщик коллекции.
    /// </param>
    internal NodeCollection([NoVerify] NodeCollectionProvider provider)
    {
        //  Установка поставщика коллекции.
        _Provider = provider;

        //  Настройка события изменения коллекции.
        _Provider.CollectionChanged += delegate (object? sender, NotifyCollectionChangedEventArgs e)
        {
            //  Вызов события.
            Volatile.Read(ref CollectionChanged)?.Invoke(this, e);

            //  Обновление количества элементов в коллекции.
            updateCount();
        };

        //  Настройка события изменения значения свойства.
        _Provider.PropertyChanged += delegate (object? sender, PropertyChangedEventArgs e)
        {
            //  Обновление количества элементов в коллекции.
            updateCount();
        };

        //  Обновляет количество элементов в коллекции.
        void updateCount()
        {
            //  Проверка изменения количества элементов.
            if (Count != _Provider.Count)
            {
                //  Установка нового значения.
                Count = _Provider.Count;

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
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Node> GetEnumerator()
    {
        //  Возврат перечислителя поставщика.
        return _Provider.GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя поставщика.
        return ((IEnumerable)_Provider).GetEnumerator();
    }
}
