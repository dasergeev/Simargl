using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Apeiron.Platform.Software.ChannelAdditor.Projects;

/// <summary>
/// Представляет коллекцию правил.
/// </summary>
public sealed class RuleCollection :
    IEnumerable<Rule>,
    INotifyCollectionChanged,
    INotifyPropertyChanged
{
    /// <summary>
    /// Происходит при изменении коллекции.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Поле для хранения списка правил.
    /// </summary>
    private readonly ObservableCollection<Rule> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal RuleCollection()
    {
        //  Создание списка правил.
        _Items = new();

        //  Подписка на события списка.
        ((INotifyCollectionChanged)_Items).CollectionChanged += (obj, e) => CollectionChanged?.Invoke(this, e);
        ((INotifyPropertyChanged)_Items).PropertyChanged += (obj, e) => PropertyChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => _Items.Count;

    /// <summary>
    /// Добавляет новое правило в коллекцию.
    /// </summary>
    /// <param name="rule">
    /// Добавляемое правило.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="rule"/> передана пустая ссылка.
    /// </exception>
    public void Add(Rule rule)
    {
        //  Проверка ссылки на правило.
        Check.IsNotNull(rule, nameof(rule));

        //  Добавления правила в список.
        _Items.Add(rule);
    }

    /// <summary>
    /// Удаляет правило из коллекции.
    /// </summary>
    /// <param name="rule">
    /// Удаляемое правило.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    public bool Remove(Rule rule)
    {
        //  Удаление из списка.
        return _Items.Remove(rule);
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Rule> GetEnumerator()
    {
        //  Возврат перечислителя списка.
        return ((IEnumerable<Rule>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
