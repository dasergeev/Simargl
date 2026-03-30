using Apeiron.Events;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Apeiron.Collections;

/// <summary>
/// Представляет постащика отслеживаемой коллекции.
/// </summary>
/// <typeparam name="T">
/// Тип элементов коллекции.
/// </typeparam>
public class NotableCollectionProvider<T> :
    CollectionProvider<T>,
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
    /// Поле для хранения списка элементов.
    /// </summary>
    private readonly ObservableCollection<T> _Items;

    /// <summary>
    /// Поле для хранения метода, который выполняет делегат в базовом потоке.
    /// </summary>
    private readonly Action<Action> _Invoker;

    /// <summary>
    /// Поле для хранения поставщика события <see cref="PropertyChanged"/>.
    /// </summary>
    readonly EventProvider<PropertyChangedEventArgs, PropertyChangedEventHandler> _PropertyChangedProvider;

    /// <summary>
    /// Поле для хранения поставщика события <see cref="CollectionChanged"/>.
    /// </summary>
    readonly EventProvider<NotifyCollectionChangedEventArgs, NotifyCollectionChangedEventHandler> _CollectionChangedProvider;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="options">
    /// Настройки коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="options"/> передана пустая ссылка.
    /// </exception>
    internal NotableCollectionProvider(NotableCollectionOptions options) :
        this(options, new(), new())
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="options">
    /// Настройки коллекции.
    /// </param>
    /// <param name="items">
    /// Список для хранения элементов.
    /// </param>
    /// <param name="sync">
    /// Объект, с помощью которого можно синхронизировать доступ к коллекции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="options"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="items"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sync"/> передана пустая ссылка.
    /// </exception>
    private NotableCollectionProvider(NotableCollectionOptions options, ObservableCollection<T> items, object sync) :
        base(options, items, sync)
    {
        //  Установка настроек коллекции.
        Options = Check.IsNotNull(options, nameof(options));

        //  Установка списка элементов.
        _Items = Check.IsNotNull(items, nameof(items));

        //  Установка метода, который выполняет делегат в базовом потоке.
        _Invoker = options.Invoker ?? (action => action());

        //  Создание поставщиков событий.
        _PropertyChangedProvider = new(this);
        _CollectionChangedProvider = new(this);

        //  Добавление обработчика события изменения коллекции.
        ((INotifyPropertyChanged)items).PropertyChanged += (object? _, PropertyChangedEventArgs e) =>
        {
            //  Проверка имени свойства.
            if (e.PropertyName == nameof(_Items.Count))
            {
                //  Вызов события об изменении свойства.
                OnPropertyChanged(e);
            }
        };

        //  Добавление обработчика события изменения количества элементов.
        ((INotifyCollectionChanged)items).CollectionChanged += (object? _, NotifyCollectionChangedEventArgs e) =>
        {
            //  Вызов события об изменении коллекции.
            OnCollectionChanged(e);
        };
    }

    /// <summary>
    /// Возвращает настройки коллекции.
    /// </summary>
    public new NotableCollectionOptions Options { get; }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов события.
        _Invoker(() => _PropertyChangedProvider.RaiseEvent(e));
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
        _Invoker(() => _CollectionChangedProvider.RaiseEvent(e));
    }
}
