using System.Collections;
using System.Collections.Generic;

namespace Simargl.Analysis.Calibrations;

/// <summary>
/// Представляет коллекцию информации о силах.
/// </summary>
public sealed class ForceInfoCollection :
    IEnumerable<ForceInfo>
{
    /// <summary>
    /// Поле для хранения списка элементов коллекции.
    /// </summary>
    private readonly List<ForceInfo> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="collection">
    /// Коллекция информации о силах.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="collection"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="collection"/> передана коллекция, содержащая пустую ссылку.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="collection"/> передана коллекция,
    /// содержащая несколько элементов с одинаковым именем.
    /// </exception>
    public ForceInfoCollection(IEnumerable<ForceInfo> collection)
    {
        //  Создание списка элементов коллекции.
        _Items = new();

        //  Перебор коллекции.
        foreach (ForceInfo item in IsNotNull(collection, nameof(collection)))
        {
            //  Проверка ссылки на элемент коллекции.
            IsNotNull(item, nameof(item));

            //  Проверка дублирования имени.
            if (_Items.Exists(x => x.ShortName == item.ShortName) ||
                _Items.Exists(x => x.FullName == item.FullName))
            {
                //  Элемент с указанным именем уже находится в коллекции.
                throw new ArgumentOutOfRangeException(nameof(item), $"Элемент с указанным именем уже находится в коллекции.");
            }

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
    public IEnumerator<ForceInfo> GetEnumerator()
    {
        //  Возврат перечислителя списка элементов коллекции.
        return ((IEnumerable<ForceInfo>)_Items).GetEnumerator();
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
