using Apeiron.Events;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Apeiron.Collections;

/// <summary>
/// Представляет отслеживаемую коллекцию.
/// </summary>
/// <typeparam name="T">
/// Тип элемента коллекции.
/// </typeparam>
public class NotableCollection<T> :
    Collection<T>,
    INotifyPropertyChanged,
    INotifyCollectionChanged
{
    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged
    {
        add => _PropertyChangedProvider.AddHandler(value);
        remove => _PropertyChangedProvider.RemoveHandler(value);
    }

    /// <summary>
    /// Происходит при изменении коллекции.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged
    {
        add => _CollectionChangedProvider.AddHandler(value);
        remove => _CollectionChangedProvider.RemoveHandler(value);
    }

    /// <summary>
    /// Поле для хранения поставщика события <see cref="PropertyChanged"/>.
    /// </summary>
    private readonly EventProvider
        <PropertyChangedEventArgs, PropertyChangedEventHandler>
        _PropertyChangedProvider;

    /// <summary>
    /// Поле для хранения поставщика события <see cref="CollectionChanged"/>.
    /// </summary>
    private readonly EventProvider
        <NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler>
        _CollectionChangedProvider;

    /// <summary>
    /// Инициалазирует новый экземпляр класса.
    /// </summary>
    /// <remarks>
    /// Используются настройки по умолчанию <see cref="CollectionOptions.Default"/>.
    /// </remarks>
    public NotableCollection() :
        this(NotableCollectionOptions.Default)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="options">
    /// Настройки коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="options"/> передана пустая ссылка.
    /// </exception>
    public NotableCollection(NotableCollectionOptions options) :
        this(new NotableCollectionProvider<T>(options))
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="provider">
    /// Поставщик коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="provider"/> передана пустая ссылка.
    /// </exception>
    internal NotableCollection(NotableCollectionProvider<T> provider) :
        base(provider)
    {
        //  Установка поставщика коллекции.
        Provider = Check.IsNotNull(provider, nameof(provider));

        //  Создание поставщиков событий.
        _PropertyChangedProvider = new(this);
        _CollectionChangedProvider = new(this);

        //  Добавление обработчика события изменения коллекции.
        ((INotifyPropertyChanged)provider).PropertyChanged += (object? _, PropertyChangedEventArgs e) =>
        {
            //  Проверка имени свойства.
            if (e.PropertyName == nameof(provider.Count))
            {
                //  Вызов события об изменении свойства.
                OnPropertyChanged(e);
            }
        };

        //  Добавление обработчика события изменения количества элементов.
        ((INotifyCollectionChanged)provider).CollectionChanged += (object? _, NotifyCollectionChangedEventArgs e) =>
        {
            //  Вызов события об изменении коллекции.
            OnCollectionChanged(e);
        };
    }

    /// <summary>
    /// Возвращает поставщика коллекции.
    /// </summary>
    protected new NotableCollectionProvider<T> Provider { get; }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        _PropertyChangedProvider.RaiseEvent(e);
    }

    /// <summary>
    /// Вызывает событие <see cref="CollectionChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        //  Вызов события.
        _CollectionChangedProvider.RaiseEvent(e);
    }
}
