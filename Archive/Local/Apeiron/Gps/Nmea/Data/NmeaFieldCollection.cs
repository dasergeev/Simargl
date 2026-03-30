using System.Collections;

namespace Apeiron.Gps.Nmea;

/// <summary>
/// Представляет коллекцию полей сообщения NMEA.
/// </summary>
public sealed class NmeaFieldCollection :
    IEnumerable<NmeaField>
{
    /// <summary>
    /// Поле для хранения массива элементов коллекции.
    /// </summary>
    private readonly NmeaField[] _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="fields">
    /// Массив значений полей.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="fields"/> передана пустая ссылка.
    /// </exception>
    internal NmeaFieldCollection(string?[] fields)
    {
        //  Проверка ссылки на массив.
        fields = IsNotNull(fields, nameof(fields));

        //  Определение количества элементов.
        Count = fields.Length;

        //  Создание массива для элементов коллекции.
        _Items = new NmeaField[Count];

        //  Создание элеменов коллекции.
        for (int i = 0; i < Count; i++)
        {
            //  Создание элемента.
            _Items[i] = new NmeaField(fields[i]);
        }
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Возвращает элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns>
    /// Элемент с указанным индексом.
    /// </returns>
    /// <remarks>
    /// Если в параметре <paramref name="index"/> передано значение
    /// большее <see cref="Count"/>, то индексатор вернёт пустое поле.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    public NmeaField this[int index]
    {
        get
        {
            //  Проверка индекса.
            index = IsNotNegative(index, nameof(index));

            //  Проверка индекса.
            if (index < Count)
            {
                //  Возврат значения.
                return _Items[index];
            }
            else
            {
                //  Возврат пустого значения.
                return NmeaField.Empty;
            }
        }
    }

    /// <summary>
    /// Возвращает диапазон полей сообщения NMEA.
    /// </summary>
    /// <param name="range">
    /// Границы дианазона.
    /// </param>
    /// <returns>
    /// Диапазон полей сообщения NMEA.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="range"/> передан диапазон, в котором начальный индекс отрицательный.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="range"/> передан диапазон, в котором начальный индекс превышает допустимое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="range"/> передан диапазон, в котором отрицательная длина.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="range"/> передан диапазон, который выходит за пределы коллекции.
    /// </exception>
    public NmeaFieldRange this[Range range]
    {
        get
        {
            //  Проверка диапазона.
            (int index, int length)  = IsRange(range, Count, nameof(range));

            //  Создание массива полей.
            NmeaField[] fields = new NmeaField[length];

            //  Заполнение массива полей.
            Array.Copy(_Items, index, fields, 0, length);

            //  Возврат диапазона.
            return new(fields);
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<NmeaField> GetEnumerator()
    {
        //  Возврат перечислителя массива элементов.
        return ((IEnumerable<NmeaField>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя массива элементов.
        return _Items.GetEnumerator();
    }
}
