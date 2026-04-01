using System.Collections;
using System.Collections.Generic;

namespace Simargl.Analysis.Calibrations;

/// <summary>
/// Представляет коллекцию модификаторов канала.
/// </summary>
public sealed class ChannelModifierCollection :
    IEnumerable<ChannelModifier>
{
    /// <summary>
    /// Поле для хранения списка элементов коллекции.
    /// </summary>
    private readonly List<ChannelModifier> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="collection">
    /// Коллекция информации о калибровочных кадрах.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="collection"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="collection"/> передана коллекция, содержащая пустую ссылку.
    /// </exception>
    public ChannelModifierCollection(IEnumerable<ChannelModifier> collection)
    {
        //  Создание списка элементов коллекции.
        _Items = new();

        //  Перебор элементов коллекции.
        foreach (ChannelModifier item in IsNotNull(collection, nameof(collection)))
        {
            //  Проверка ссылки на элемент коллекции.
            IsNotNull(item, nameof(item));

            //  Добавление элемента в коллекцию.
            _Items.Add(item);
        }
    }

    /// <summary>
    /// Возвращает перечислитель элементов коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель элементов коллекции.
    /// </returns>
    public IEnumerator<ChannelModifier> GetEnumerator()
    {
        //  Возврат перечислителя списка элементов коллекции.
        return ((IEnumerable<ChannelModifier>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель элементов коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель элементов коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя списка элементов коллекции.
        return ((IEnumerable)_Items).GetEnumerator();
    }
}
