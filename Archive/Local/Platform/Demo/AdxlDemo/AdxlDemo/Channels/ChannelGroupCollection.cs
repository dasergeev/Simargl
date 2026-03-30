using System.Collections;

namespace Apeiron.Platform.Demo.AdxlDemo.Channels;

/// <summary>
/// Представляет коллекцию групп каналов.
/// </summary>
public sealed class ChannelGroupCollection :
    IEnumerable<ChannelGroup>
{
    /// <summary>
    /// Поле для хранения элементов коллекции.
    /// </summary>
    private readonly ChannelGroup[] _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="items">
    /// Коллекция групп.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="items"/> передана пустая ссылка.
    /// </exception>
    public ChannelGroupCollection(ChannelGroup[] items)
    {
        //  Установка коллекции групп каналов.
        _Items = IsNotNull(items, nameof(items));
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => _Items.Length;

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<ChannelGroup> GetEnumerator()
    {
        //  Возврат перечислителя хранилища элементов коллекции.
        return ((IEnumerable<ChannelGroup>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя хранилища элементов коллекции.
        return _Items.GetEnumerator();
    }
}
