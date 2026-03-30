using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Simargl.UI.Nodes;

/// <summary>
/// Представляет коллекцию узлов.
/// </summary>
public sealed class NodeCollection :
    Anything,
    IEnumerable<Node>,
    INotifyCollectionChanged
{
    /// <summary>
    /// Происходит при изменении коллекции.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <summary>
    /// Поле для хранения поставщика элементов коллекции.
    /// </summary>
    private readonly ObservableCollection<Node> _Provider;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="provider">
    /// Поставщик элементов коллекции.
    /// </param>
    internal NodeCollection(ObservableCollection<Node> provider)
    {
        //  Установка поставщика элементов коллекции.
        _Provider = IsNotNull(provider);

        //  Добавление обработчика события изменения коллекции.
        _Provider.CollectionChanged += delegate (object? sender, NotifyCollectionChangedEventArgs e)
        {
            //  Вызов события об изменении коллекции.
            Volatile.Read(ref CollectionChanged)?.Invoke(this, e);
        };
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Node> GetEnumerator()
    {
        //  Возврат перечислителя поставщика коллекции.
        return ((IEnumerable<Node>)_Provider).GetEnumerator();
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
        return ((IEnumerable)_Provider).GetEnumerator();
    }
}
