using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Simargl.Designing.Utilities;

/// <summary>
/// Предоставляет методы создания исключений.
/// </summary>
public static class ExceptionsCreator
{
    ///// <summary>
    ///// Генерирует исключение, сообщающее о том, что
    ///// передана пустая ссылка.
    ///// </summary>
    ///// <typeparam name="T">
    ///// Тип значения.
    ///// </typeparam>
    ///// <param name="paramName">
    ///// Имя параметра.
    ///// </param>
    ///// <returns>
    ///// Метод не возвращает значений.
    ///// </returns>
    ///// <exception cref="ArgumentNullException">
    ///// Передана пустая ссылка.
    ///// </exception>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static T Null<T>(string? paramName = null)
    //{
    //    //  Проверка имени параметра.
    //    if (paramName is not null)
    //    {
    //        //  Выброс исключения.
    //        throw new ArgumentNullException(
    //            paramName,
    //            $"В параметре \"{paramName}\" передана пустая ссылка.");
    //    }
    //    else
    //    {
    //        //  Выброс исключения.
    //        throw new ArgumentNullException(null, "Передана пустая ссылка.");
    //    }
    //}

    /// <summary>
    /// Генерирует исключение, сообщающее о том, что
    /// операция отменена.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static OperationCanceledException Cancelled()
    {
        //  Выброс исключения.
        return new OperationCanceledException("Операция отменена.");
    }

    /// <summary>
    /// Создаёт исключение, сообщающее о том, что
    /// в параметре передана пустая ссылка.
    /// </summary>
    /// <param name="paramName">
    /// Имя параметра, вызвавшего исключение.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentNullException ArgumentNull(string? paramName)
    {
        //  Возврат исключения.
        return  new(paramName, $"В параметре {paramName} передана пустая ссылка.");
    }

    //  ArgumentException
    //  ArgumentNullException
    //  ArgumentOutOfRangeException
    //  ArithmeticException
    //  IndexOutOfRangeException
    //  InvalidCastException
    //  InvalidOperationException
    //  NotFiniteNumberException
    //  NotSupportedException
    //  NullReferenceException
    //  ObjectDisposedException
    //  OperationCanceledException
    //  OutOfMemoryException
    //  OverflowException
    //  PlatformNotSupportedException
    //  TimeoutException


    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передано нечисловое значение.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано значение.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentNaN(string? paramName)
    {
        //  В параметре передано нечисловое значение.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передано нечисловое значение.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передано значение, которое не является степенью двух.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано значение.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentNotPowerOfTwo(string? paramName)
    {
        //  В параметре передано значение, которое не является степенью двух.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передано значение, которое не является степенью двух.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передан массив, размер которого больше максимально допустимого.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передан массив.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentArrayLargerMax(string? paramName)
    {
        //  В параметре передан массив, размер которого больше максимально допустимого.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передан массив, размер которого больше максимально допустимого.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передан массив, размер которого меньше минимально допустимого.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передан массив.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentArrayLessMin(string? paramName)
    {
        //  В параметре передан массив, размер которого меньше минимально допустимого.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передан массив, размер которого меньше минимально допустимого.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что в параметре передано значение,
    /// которое не соответствует допустимому диапазону значений.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано значение,
    /// которое не соответствует допустимому диапазону значений.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentOutOfRange(string? paramName)
    {
        //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передано значение, которое не соответствует допустимому диапазону значений.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передан диапазон, в котором начальный индекс превышает допустимое значение.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано значение,
    /// которое не соответствует допустимому диапазону значений.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentRangeLargeIndex(string? paramName)
    {
        //  В параметре передан диапазон, в котором начальный индекс превышает допустимое значение.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передан диапазон, в котором начальный индекс превышает допустимое значение.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передан диапазон, в котором начальный индекс отрицательный.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано значение,
    /// которое не соответствует допустимому диапазону значений.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentRangeNegativeIndex(string? paramName)
    {
        //  В параметре передан диапазон, в котором начальный индекс отрицательный.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передан диапазон, в котором начальный индекс отрицательный.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передан диапазон, в котором отрицательная длина.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано значение,
    /// которое не соответствует допустимому диапазону значений.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentRangeNegativeLength(string? paramName)
    {
        //  В параметре передан диапазон, в котором отрицательная длина.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передан диапазон, в котором отрицательная длина.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передан диапазон, который выходит за пределы коллекции.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано значение,
    /// которое не соответствует допустимому диапазону значений.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentRangeNotBelong(string? paramName)
    {
        //  В параметре передан диапазон, который выходит за пределы коллекции.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передан диапазон, который выходит за пределы коллекции.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что в параметре
    /// передана строка, которая содержит недопустимый символ.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано значение,
    /// которое не соответствует допустимому диапазону значений.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentStringContainInvalidChar(string? paramName)
    {
        //  В параметре передана строка, которая содержит недопустимый символ.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передана строка, которая содержит недопустимый символ.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что значение не содержится в заданном перечислении.
    /// </summary>
    /// <typeparam name="T">
    /// Тип перечисления.
    /// </typeparam>
    /// <param name="paramName">
    /// Параметр, в котором передано значение, которое не содержится в перечислении.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentNotContainedInEnumeration<T>(string? paramName)
        where T : Enum
    {
        //  В параметре передано значение, которое не содержится в перечислении.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передано значение, которое не содержится в перечислении {1}.",
            paramName, typeof(T).Name));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что в параметре передан пустой массив.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передан пустой массив.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentEmptyArray(string? paramName)
    {
        //  В параметре передан пустой массив.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передан пустой массив.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что в параметре передана пустая коллекция.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передана пустая коллекция.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentEmptyCollection(string? paramName)
    {
        //  В параметре передана пустая коллекция.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передана пустая коллекция.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что в параметре передана пустая строка.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передана пустая строка.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentEmptyString(string? paramName)
    {
        //  В параметре передана пустая строка.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передана пустая строка.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передан пустой вектор.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передан пустой вектор.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentEmptyVector(string? paramName)
    {
        //  В параметре передан пустой вектор.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передан пустой вектор.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре объект недопустимого типа.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передан объект недопустимого типа.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentException ArgumentInvalidType(string? paramName)
    {
        //  В параметре передан объект недопустимого типа.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передан объект недопустимого типа.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передан многомерный массив.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передан объект многомерный массив.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RankException ArgumentMultidimensionalArray(string? paramName)
    {
        //  В параметре передан многомерный массив.
        return new(
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передан многомерный массив.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что в параметре передано отрицательное значение.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано отрицательное значение.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentNegativeValue(string? paramName)
    {
        //  В параметре передано отрицательное значение.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передано отрицательное значение.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передано значение, которое не содержится в коллекции.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано отрицательное значение.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentNotContainedInCollection(string? paramName)
    {
        //  В параметре передано значение, которое не содержится в коллекции.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передано значение, которое не содержится в коллекции.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что в параметре передано нулевое значение.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано нулевое значение.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentZeroValue(string? paramName)
    {
        //  В параметре передано нулевое значение.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передано нулевое значение.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// кадр имеет неверную сигнатуру.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static InvalidDataException FrameInvalidSignature()
    {
        //  Неверная сигнатура кадра.
        return new("Неверная сигнатура кадра.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// кадр имеет неверную сигнатуру.
    /// </summary>
    /// <param name="innerException">
    /// Исключение, которое является причиной текущего исключения.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static InvalidDataException FrameInvalidSignature(Exception? innerException)
    {
        //  Неверная сигнатура кадра.
        return new("Неверная сигнатура кадра.", innerException);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// член не найден.
    /// </summary>
    /// <param name="memberName">
    /// Имя члена.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ContextMarshalException MarshalMemberNotFound(string? memberName)
    {
        //  Член не найден.
        return new(string.Format(CultureInfo.CurrentCulture, "Член {0} не найден.", memberName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// член не найден.
    /// </summary>
    /// <param name="innerException">
    /// Исключение, которое является причиной текущего исключения.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ContextMarshalException MarshalMemberNotFound(Exception? innerException)
    {
        //  Член не найден.
        return MarshalMemberNotFound(null, innerException);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// член не найден.
    /// </summary>
    /// <param name="memberName">
    /// Имя члена.
    /// </param>
    /// <param name="innerException">
    /// Исключение, которое является причиной текущего исключения.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ContextMarshalException MarshalMemberNotFound(string? memberName, Exception? innerException)
    {
        //  Член не найден.
        return new(string.Format(CultureInfo.CurrentCulture, "Член {0} не найден.", memberName),
            innerException);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// объект не найден.
    /// </summary>
    /// <param name="objectName">
    /// Имя объекта.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ContextMarshalException MarshalObjectNotFound(string? objectName)
    {
        //  Объект не найден.
        return new(string.Format(CultureInfo.CurrentCulture, "Объект {0} не найден.", objectName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// неверная контрольная сумма строки в формате NMEA.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static InvalidDataException NmeaInvalidChecksum()
    {
        //  Неверная контрольная сумма строки в формате NMEA.
        return new InvalidDataException("Неверная контрольная сумма строки в формате NMEA.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// неверный формат строки в формате NMEA.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static InvalidDataException NmeaInvalidFormat()
    {
        //  Неверный формат строки в формате NMEA.
        return new InvalidDataException("Неверный формат строки в формате NMEA.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// неверный формат строки в формате NMEA.
    /// </summary>
    /// <param name="innerException">
    /// Исключение, которое является причиной текущего исключения.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static InvalidDataException NmeaInvalidFormat(Exception? innerException)
    {
        //  Неверный формат строки в формате NMEA.
        return new InvalidDataException("Неверный формат строки в формате NMEA.", innerException);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// неверная сигнатура строки в формате NMEA.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static InvalidDataException NmeaInvalidSignature()
    {
        //  Неверная сигнатура строки в формате NMEA.
        return new InvalidDataException("Неверная сигнатура строки в формате NMEA.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передана строка, длина которой меньше минимально допустимого.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передан массив.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentStringLargerMax(string? paramName)
    {
        //  В параметре передана строка, длина которой меньше минимально допустимого.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передана строка, длина которой больше максимально допустимого значения.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передана строка, длина которой меньше минимально допустимого.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передан массив.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ArgumentOutOfRangeException ArgumentStringLessMin(string? paramName)
    {
        //  В параметре передана строка, длина которой меньше минимально допустимого.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, "В параметре {0} передана строка, длина которой меньше минимально допустимого значения.", paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// после создания перечислителя семейство было изменено.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static InvalidOperationException OperationEnumerationChanged()
    {
        //  После создания перечислителя семейство было изменено.
        return new("После создания перечислителя семейство было изменено.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// произошла попытка выполнить недопустимую операцию.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static InvalidOperationException OperationInvalid()
    {
        //  Недопустимая операция.
        return new("Произошла попытка выполнить недопустимую операцию.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// произошла попытка выполнить недопустимое преобразование.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static InvalidCastException OpertionInvalidCast()
    {
        //  Недопустимое преобразование.
        return new("Недопустимое преобразование.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// операция не поддерживается.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static NotSupportedException OperationNotSupported()
    {
        //  Операция не поддерживается.
        return new("Операция не поддерживается.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// в результате операции произошло обращение к разрушенному объекту.
    /// </summary>
    /// <param name="objectName">
    /// Имя разрушенного объекта.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ObjectDisposedException OperationObjectDisposed(string? objectName)
    {
        //  В результате операции произошло обращение к разрушенному объекту.
        return new(objectName, "В результате операции произошло обращение к разрушенному объекту.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// операция привела к переполнению.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static OverflowException OperationOverflow()
    {
        //  Операция привела к переполнению.
        return new("Операция привела к переполнению.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// время ожидания операции истекло.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TimeoutException OperationTimeout()
    {
        //  Время ожидания операции истекло.
        return new("Время ожидания операции истекло.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// достигнут конец потока.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static EndOfStreamException StreamEnd()
    {
        //  Достигнут конец потока.
        return new("Достигнут конец потока.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// поток имеет неверный формат.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IOException StreamInvalidFormat()
    {
        //  Неверный формат потока.
        return new("Неверный формат потока.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// поток не поддерживает чтение.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static InvalidOperationException StreamNotReadable()
    {
        //  Поток не поддерживает чтение.
        return new("Поток не поддерживает чтение.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// поток не поддерживает поиск.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static InvalidOperationException StreamNotSearchable()
    {
        //  Поток не поддерживает поиск.
        return new("Поток не поддерживает поиск.");
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// поток не поддерживает запись.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static InvalidOperationException StreamNotWritable()
    {
        //  Поток не поддерживает запись.
        return new("Поток не поддерживает запись.");
    }
}
