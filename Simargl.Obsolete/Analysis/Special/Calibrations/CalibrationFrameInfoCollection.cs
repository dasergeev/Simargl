using System.Collections;
using System.Collections.Generic;

namespace Simargl.Analysis.Calibrations;

/// <summary>
/// Представляет коллекцию информации о калибровочных кадрах.
/// </summary>
public sealed class CalibrationFrameInfoCollection :
    IEnumerable<CalibrationFrameInfo>
{
    /// <summary>
    /// Поле для хранения списка элементов коллекции.
    /// </summary>
    private readonly List<CalibrationFrameInfo> _Items;

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
    public CalibrationFrameInfoCollection(IEnumerable<CalibrationFrameInfo> collection)
    {
        //  Создание списка элементов коллекции.
        _Items = new();

        //  Перебор элементов коллекции.
        foreach (CalibrationFrameInfo item in IsNotNull(collection, nameof(collection)))
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
    public IEnumerator<CalibrationFrameInfo> GetEnumerator()
    {
        //  Возврат перечислителя списка элементов коллекции.
        return ((IEnumerable<CalibrationFrameInfo>)_Items).GetEnumerator();
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
