using System.Collections;

namespace Apeiron.Gps.Nmea;

/// <summary>
/// Представляет диапазон полей сообщения NMEA.
/// </summary>
public class NmeaFieldRange :
    IEnumerable<NmeaField>
{
    /// <summary>
    /// Поле для хранения массива полей сообщения NMEA.
    /// </summary>
    private readonly NmeaField[] _Fields;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="fields">
    /// Массив полей сообщения NMEA.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="fields"/> передана пустая ссылка.
    /// </exception>
    internal NmeaFieldRange(NmeaField[] fields)
    {
        //  Проверка ссылки на массив.
        _Fields = IsNotNull(fields, nameof(fields));

        //  Определение количества полей.
        Count = fields.Length;
    }

    /// <summary>
    /// Возвращает количество элементов в диапазоне.
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
                return _Fields[index];
            }
            else
            {
                //  Возврат пустого значения.
                return NmeaField.Empty;
            }
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к числу с плавающей точкой с заданной размерностью.
    /// </summary>
    /// <param name="unit">
    /// Размерность значения.
    /// </param>
    /// <returns>
    /// Число с плавающей точкой, которое содержится в диапазоне полей.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public double? ToFloating(char unit)
    {
        //  Получение значения числа с плавающей точкой.
        double? value = this[0].ToFloating();

        //  Получение фактической размерности значения.
        char? actualUnit = this[1].ToCharacter();

        //  Проверка значения.
        if (value.HasValue)
        {
            //  Проверка фактической размерности значения.
            if (!actualUnit.HasValue || actualUnit != unit)
            {
                //  Неверный формат строки в формате NMEA.
                throw Exceptions.NmeaInvalidFormat();
            }
        }
        else
        {
            if (actualUnit.HasValue)
            {
                //  Неверный формат строки в формате NMEA.
                throw Exceptions.NmeaInvalidFormat();
            }
        }

        //  Возврат числа с плавающей точкой.
        return value;
    }

    /// <summary>
    /// Выполняет преобразование диапазона полей к широте.
    /// </summary>
    /// <returns>
    /// Широта в градусах, которая содержится в диапазоне полей.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public double? ToLatitude()
    {
        //  Определение широты.
        double? latitude = this[0].ToLatitude();

        //  Определение направления широты.
        LatitudeDirection? direction = this[1].ToLatitudeDirection();

        //  Проверка установки широты.
        if (latitude.HasValue)
        {
            //  Проверка направления широты.
            if (direction.HasValue)
            {
                //  Корректировка широты.
                latitude = direction.Value switch
                {
                    LatitudeDirection.North => latitude.Value,
                    LatitudeDirection.South => -latitude.Value,
                    _ => throw Exceptions.NmeaInvalidFormat(),
                };
            }
            else
            {
                //  Неверный формат строки в формате NMEA.
                throw Exceptions.NmeaInvalidFormat();
            }
        }
        else
        {
            //  Проверка направления широты.
            if (direction.HasValue)
            {
                //  Неверный формат строки в формате NMEA.
                throw Exceptions.NmeaInvalidFormat();
            }
        }

        //  Возврат широты.
        return latitude;
    }

    /// <summary>
    /// Выполняет преобразование диапазона полей к долготе.
    /// </summary>
    /// <returns>
    /// Долгота в градусах, которая содержится в диапазоне полей.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public double? ToLongitude()
    {
        //  Определение долготы.
        double? longitude = this[0].ToLongitude();

        //  Определение направления долготы.
        LongitudeDirection? direction = this[1].ToLongitudeDirection();

        //  Проверка установки долготы.
        if (longitude.HasValue)
        {
            //  Проверка направления долготы.
            if (direction.HasValue)
            {
                //  Корректировка долготы.
                longitude = direction.Value switch
                {
                    LongitudeDirection.Eastern => longitude.Value,
                    LongitudeDirection.Western => -longitude.Value,
                    _ => throw Exceptions.NmeaInvalidFormat(),
                };
            }
            else
            {
                //  Неверный формат строки в формате NMEA.
                throw Exceptions.NmeaInvalidFormat();
            }
        }
        else
        {
            //  Проверка направления долготы.
            if (direction.HasValue)
            {
                //  Неверный формат строки в формате NMEA.
                throw Exceptions.NmeaInvalidFormat();
            }
        }

        //  Возврат долготы.
        return longitude;
    }

    /// <summary>
    /// Выполняет преобразование диапазона полей к отклонению курса на магнитный полюс в градусах.
    /// </summary>
    /// <returns>
    /// Отклонение курса на магнитный полюс в градусах, которая содержится в диапазоне полей.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public double? ToMagneticVariation()
    {
        //  Определение отклонения курса на магнитный полюс.
        double? variation = this[0].ToFloating();

        //  Определение значения, определяющего направление отклонения курса на магнитный полюс.
        MagneticVariationDirection? direction = this[1].ToMagneticVariationDirection();

        //  Проверка установки отклонения курса.
        if (variation.HasValue)
        {
            //  Проверка направления долготы.
            if (direction.HasValue)
            {
                //  Корректировка долготы.
                variation = direction.Value switch
                {
                    MagneticVariationDirection.Easterly => -variation.Value,
                    MagneticVariationDirection.Westerly => variation.Value,
                    _ => throw Exceptions.NmeaInvalidFormat(),
                };
            }
            else
            {
                //  Неверный формат строки в формате NMEA.
                throw Exceptions.NmeaInvalidFormat();
            }
        }
        else
        {
            //  Проверка направления долготы.
            if (direction.HasValue)
            {
                //  Неверный формат строки в формате NMEA.
                throw Exceptions.NmeaInvalidFormat();
            }
        }

        //  Возврат долготы.
        return variation;
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
        return ((IEnumerable<NmeaField>)_Fields).GetEnumerator();
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
        return _Fields.GetEnumerator();
    }
}
