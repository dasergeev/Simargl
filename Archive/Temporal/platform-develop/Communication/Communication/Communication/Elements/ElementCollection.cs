using System.Collections;
using System.Collections.Specialized;

namespace Apeiron.Platform.Communication.Elements;

/// <summary>
/// Представляет коллекцию элементов.
/// </summary>
/// <typeparam name="TElement">
/// Тип элемента.
/// </typeparam>
public abstract class ElementCollection<TElement> :
    Element,
    INotifyCollectionChanged,
    IEnumerable<TElement>
    where TElement : Element
{
    /// <summary>
    /// Происходит при изменении коллекции.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="communicator">
    /// Коммуникатор с серверным узлом.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="communicator"/> передана пустая ссылка.
    /// </exception>
    internal ElementCollection(Communicator communicator) :
        base(communicator)
    {
        //  Создание поставщика коллекции.
        Provider = new(communicator);

        //  Добавление обработчиков событий поставщика коллекции.
        Provider.PropertyChanged += (_, e) => OnPropertyChanged(e);
        Provider.CollectionChanged += (_, e) => OnCollectionChanged(e);
    }

    /// <summary>
    /// Возвращает поставщика коллекции.
    /// </summary>
    protected ElementCollectionProvider<TElement> Provider { get; }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => Provider.Count;

    /// <summary>
    /// Вызывает событие <see cref="CollectionChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с коллекцией.
    /// </param>
    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        //  Выполнение в основном потоке.
        PrimaryInvoker.Invoke(delegate
        {
            //  Захват текущего делегата.
            NotifyCollectionChangedEventHandler? handler = Volatile.Read(ref CollectionChanged);

            //  Вызов события.
            handler?.Invoke(this, e);
        });
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<TElement> GetEnumerator()
    {
        //  Возврат перечислителя поставщика коллекции.
        return Provider.GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя поставщика коллекции.
        return ((IEnumerable)Provider).GetEnumerator();
    }
}
