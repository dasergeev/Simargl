using System.Collections;
using System.Collections.Generic;

namespace Simargl.Analysis.Calibrations;

/// <summary>
/// Представляет коллекцию информации об исходных сигналах.
/// </summary>
public sealed class SignalInfoCollection :
    IEnumerable<SignalInfo>
{
    /// <summary>
    /// Поле для хранения карты элементов коллекции.
    /// </summary>
    private readonly List<SignalInfo> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="collection">
    /// Коллекция информации об исходных сигналах.
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
    public SignalInfoCollection(IEnumerable<SignalInfo> collection)
    {
        //  Создание списка элементов коллекции.
        _Items = new();

        //  Перебор коллекции.
        foreach (SignalInfo item in IsNotNull(collection, nameof(collection)))
        {
            //  Проверка ссылки на элемент коллекции.
            IsNotNull(item, nameof(item));

            //  Проверка дублирования имени.
            if (_Items.Exists(x => x.Name == item.Name))
            {
                //  Элемент с указанным именем уже находится в коллекции.
                throw new ArgumentOutOfRangeException(nameof(item), $"Элемент с указанным именем уже находится в коллекции."); ;
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
    public IEnumerator<SignalInfo> GetEnumerator()
    {
        //  Возврат перечислителя списка элементов коллекции.
        return ((IEnumerable<SignalInfo>)_Items).GetEnumerator();
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
