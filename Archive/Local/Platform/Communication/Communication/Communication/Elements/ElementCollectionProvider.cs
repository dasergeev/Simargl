using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Apeiron.Platform.Communication.Elements;

/// <summary>
/// Представляет поставщика элементов коллекции.
/// </summary>
/// <typeparam name="TElement">
/// Тип элемента коллекции.
/// </typeparam>
public sealed class ElementCollectionProvider<TElement> :
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
    /// Поле для хранения элементов.
    /// </summary>
    private readonly ObservableCollection<TElement> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="communicator">
    /// Коммуникатор с серверным узлом.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="communicator"/> передана пустая ссылка.
    /// </exception>
    internal ElementCollectionProvider(Communicator communicator) :
        base(communicator)
    {
        //  Создание хранилища элементов.
        _Items = new();

        //  Добавление обработчиков событий хранилища.
        ((INotifyPropertyChanged)_Items).PropertyChanged += (_, e) =>
        {
            //  Проверка имени свойства.
            if (e.PropertyName == nameof(Count))
            {
                //  Вызов события.
                OnPropertyChanged(e);
            }
        };
        _Items.CollectionChanged += (_, e) => OnCollectionChanged(e);
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => _Items.Count;

    /// <summary>
    /// Выполняет действие над хранилищем элементов в основном потоке.
    /// </summary>
    /// <param name="action">
    /// Действие, которое необходимо выполнить в основном потоке.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="action"/> передана пустая ссылка.
    /// </exception>
    public void PrimaryInvoke(Action<ObservableCollection<TElement>> action)
    {
        //  Проверка ссылки на действие.
        IsNotNull(action, nameof(action));

        //  Выполнение в основном потоке.
        PrimaryInvoker.Invoke(delegate
        {
            //  Выполнение действия.
            action(_Items);
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
        //  Возврат перечислителя хранилища.
        return _Items.GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Вызывает событие <see cref="CollectionChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с коллекцией.
    /// </param>
    private void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
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
}
