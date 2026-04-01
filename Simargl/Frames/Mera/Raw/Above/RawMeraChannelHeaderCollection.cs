using System.Collections;
using System.Collections.Generic;

namespace Simargl.Frames.Mera.Raw;

/// <summary>
/// Представляет коллекцию сырых заголовков канала в формате <see cref="StorageFormat.Mera"/>.
/// </summary>
public sealed class RawMeraChannelHeaderCollection :
    IEnumerable<RawMeraChannelHeader>
{
    /// <summary>
    /// Поле для хранения списка элементов.
    /// </summary>
    private readonly List<RawMeraChannelHeader> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    internal RawMeraChannelHeaderCollection()
    {
        //  Создание списка элементов.
        _Items = [];
    }

    /// <summary>
    /// Добавляет новый элемент в коллекцию.
    /// </summary>
    /// <param name="item">
    /// Элемент, который необходимо добавить в коллекцию.
    /// </param>
    public void Add(RawMeraChannelHeader item)
    {
        //  Добавление элемента в список.
        _Items.Add(item);
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<RawMeraChannelHeader> GetEnumerator()
    {
        //  Возврат перечислителя списка.
        return ((IEnumerable<RawMeraChannelHeader>)_Items).GetEnumerator();
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
