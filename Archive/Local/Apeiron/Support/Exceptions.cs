using System.Globalization;

namespace Apeiron.Support;

/// <summary>
/// Предоставляет вспомогательные методы для создания исключений.
/// </summary>
public static partial class Exceptions
{
    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// неверная контрольная сумма пакета данных Adxl.
    /// </summary>
    /// <param name="innerException">
    /// Исключение, ставшее причиной текущего исключения,
    /// или пустая ссылка, если внутреннее исключение не задано.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidDataException AdxlInvalidChecksum(Exception? innerException = null)
    {
        //  Неверная контрольная сумма пакета данных Adxl.
        return new InvalidDataException(Messages.AdxlInvalidChecksum, innerException);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// неверный формат пакета данных Adxl.
    /// </summary>
    /// <param name="innerException">
    /// Исключение, ставшее причиной текущего исключения,
    /// или пустая ссылка, если внутреннее исключение не задано.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidDataException AdxlInvalidFormat(Exception? innerException = null)
    {
        //  Неверный формат пакета данных Adxl.
        return new InvalidDataException(Messages.AdxlInvalidFormat, innerException);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// неверная сигнатура пакета данных Adxl.
    /// </summary>
    /// <param name="innerException">
    /// Исключение, ставшее причиной текущего исключения,
    /// или пустая ссылка, если внутреннее исключение не задано.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidDataException AdxlInvalidSignature(Exception? innerException = null)
    {
        //  Неверная сигнатура пакета данных Adxl.
        return new InvalidDataException(Messages.AdxlInvalidSignature, innerException);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передано значение, которое уже содержится коллекции.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано значение.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static ArgumentOutOfRangeException ArgumentAlreadyContainedInCollection(
        string? paramName)
    {
        //  В параметре передано значение, которое уже содержится коллекции.
        return new(paramName,
            string.Format(
                CultureInfo.CurrentCulture,
                Messages.ArgumentAlreadyContainedInCollection,
                paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передано бесконечное значение.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано значение.
    /// </param>
    /// <param name="innerException">
    /// Исключение, ставшее причиной текущего исключения,
    /// или пустая ссылка, если внутреннее исключение не задано.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static ArgumentOutOfRangeException ArgumentInfinity(
        string? paramName,
        Exception? innerException = null)
    {
        //  В параметре передано бесконечное значение.
        return new(
            string.Format(
                CultureInfo.CurrentCulture,
                Messages.ArgumentInfinity,
                paramName),
            innerException);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// в параметре передан недопустимый таймаут.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передано значение.
    /// </param>
    /// <param name="innerException">
    /// Исключение, ставшее причиной текущего исключения,
    /// или пустая ссылка, если внутреннее исключение не задано.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static ArgumentOutOfRangeException ArgumentInvalidTimeout(
        string? paramName, Exception? innerException = null)
    {
        //  В параметре передан недопустимый таймаут.
        return new(
            string.Format(
                CultureInfo.CurrentCulture,
                Messages.ArgumentInvalidTimeout,
                paramName),
            innerException);
    }

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
    public static ArgumentOutOfRangeException ArgumentNaN(string? paramName)
    {
        //  В параметре передано нечисловое значение.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentNaN, paramName));
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
    public static ArgumentOutOfRangeException ArgumentNotPowerOfTwo(string? paramName)
    {
        //  В параметре передано значение, которое не является степенью двух.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentNotPowerOfTwo, paramName));
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
    public static ArgumentOutOfRangeException ArgumentArrayLargerMax(string? paramName)
    {
        //  В параметре передан массив, размер которого больше максимально допустимого.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentArrayLargerMax, paramName));
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
    public static ArgumentOutOfRangeException ArgumentArrayLessMin(string? paramName)
    {
        //  В параметре передан массив, размер которого меньше минимально допустимого.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentArrayLessMin, paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о передаче пустой ссылки в параметре.
    /// </summary>
    /// <param name="paramName">
    /// Параметр, в котором передана пустая ссылка.
    /// </param>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static ArgumentNullException ArgumentNullReference(string? paramName)
    {
        //  В параметре передана пустая ссылка.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentNullReference, paramName));
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
    public static ArgumentOutOfRangeException ArgumentOutOfRange(string? paramName)
    {
        //  В параметре передано значение, которое не соответствует допустимому диапазону значений.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentOutOfRange, paramName));
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
    public static ArgumentOutOfRangeException ArgumentRangeLargeIndex(string? paramName)
    {
        //  В параметре передан диапазон, в котором начальный индекс превышает допустимое значение.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentRangeLargeIndex, paramName));
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
    public static ArgumentOutOfRangeException ArgumentRangeNegativeIndex(string? paramName)
    {
        //  В параметре передан диапазон, в котором начальный индекс отрицательный.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentRangeNegativeIndex, paramName));
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
    public static ArgumentOutOfRangeException ArgumentRangeNegativeLength(string? paramName)
    {
        //  В параметре передан диапазон, в котором отрицательная длина.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentRangeNegativeLength, paramName));
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
    public static ArgumentOutOfRangeException ArgumentRangeNotBelong(string? paramName)
    {
        //  В параметре передан диапазон, который выходит за пределы коллекции.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentRangeNotBelong, paramName));
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
    public static ArgumentOutOfRangeException ArgumentStringContainInvalidChar(string? paramName)
    {
        //  В параметре передана строка, которая содержит недопустимый символ.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentStringContainInvalidChar, paramName));
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
    public static ArgumentOutOfRangeException ArgumentNotContainedInEnumeration<T>(string? paramName)
        where T : Enum
    {
        //  В параметре передано значение, которое не содержится в перечислении.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentNotContainedInEnumeration,
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
    public static ArgumentOutOfRangeException ArgumentEmptyArray(string? paramName)
    {
        //  В параметре передан пустой массив.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentEmptyArray, paramName));
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
    public static ArgumentOutOfRangeException ArgumentEmptyCollection(string? paramName)
    {
        //  В параметре передана пустая коллекция.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentEmptyCollection, paramName));
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
    public static ArgumentOutOfRangeException ArgumentEmptyString(string? paramName)
    {
        //  В параметре передана пустая строка.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentEmptyString, paramName));
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
    public static ArgumentOutOfRangeException ArgumentEmptyVector(string? paramName)
    {
        //  В параметре передан пустой вектор.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentEmptyVector, paramName));
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
    public static ArgumentException ArgumentInvalidType(string? paramName)
    {
        //  В параметре передан объект недопустимого типа.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentInvalidType, paramName));
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
    public static RankException ArgumentMultidimensionalArray(string? paramName)
    {
        //  В параметре передан многомерный массив.
        return new(
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentMultidimensionalArray, paramName));
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
    public static ArgumentOutOfRangeException ArgumentNegativeValue(string? paramName)
    {
        //  В параметре передано отрицательное значение.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentNegativeValue, paramName));
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
    public static ArgumentOutOfRangeException ArgumentNotContainedInCollection(string? paramName)
    {
        //  В параметре передано значение, которое не содержится в коллекции.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentNotContainedInCollection, paramName));
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
    public static ArgumentOutOfRangeException ArgumentZeroValue(string? paramName)
    {
        //  В параметре передано нулевое значение.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentZeroValue, paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// кадр имеет неверную сигнатуру.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidDataException FrameInvalidSignature()
    {
        //  Неверная сигнатура кадра.
        return new(Messages.FrameInvalidSignature);
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
    public static InvalidDataException FrameInvalidSignature(Exception? innerException)
    {
        //  Неверная сигнатура кадра.
        return new(Messages.FrameInvalidSignature, innerException);
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
    public static ContextMarshalException MarshalMemberNotFound(string? memberName)
    {
        //  Член не найден.
        return new(string.Format(CultureInfo.CurrentCulture, Messages.MarshalMemberNotFound, memberName));
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
    public static ContextMarshalException MarshalMemberNotFound(string? memberName, Exception? innerException)
    {
        //  Член не найден.
        return new(string.Format(CultureInfo.CurrentCulture, Messages.MarshalMemberNotFound, memberName),
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
    public static ContextMarshalException MarshalObjectNotFound(string? objectName)
    {
        //  Объект не найден.
        return new(string.Format(CultureInfo.CurrentCulture, Messages.MarshalObjectNotFound, objectName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// неверная контрольная сумма строки в формате NMEA.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidDataException NmeaInvalidChecksum()
    {
        //  Неверная контрольная сумма строки в формате NMEA.
        return new InvalidDataException(Messages.NmeaInvalidChecksum);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// неверный формат строки в формате NMEA.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidDataException NmeaInvalidFormat()
    {
        //  Неверный формат строки в формате NMEA.
        return new InvalidDataException(Messages.NmeaInvalidFormat);
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
    public static InvalidDataException NmeaInvalidFormat(Exception? innerException)
    {
        //  Неверный формат строки в формате NMEA.
        return new InvalidDataException(Messages.NmeaInvalidFormat, innerException);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// неверная сигнатура строки в формате NMEA.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidDataException NmeaInvalidSignature()
    {
        //  Неверная сигнатура строки в формате NMEA.
        return new InvalidDataException(Messages.NmeaInvalidSignature);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// операция отменена.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static OperationCanceledException OperationCanceled()
    {
        //  Операция отменена.
        return new(Messages.OperationCanceled);
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
    public static ArgumentOutOfRangeException ArgumentStringLargerMax(string? paramName)
    {
        //  В параметре передана строка, длина которой меньше минимально допустимого.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentStringLargerMax, paramName));
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
    public static ArgumentOutOfRangeException ArgumentStringLessMin(string? paramName)
    {
        //  В параметре передана строка, длина которой меньше минимально допустимого.
        return new(paramName,
            string.Format(CultureInfo.CurrentCulture, Messages.ArgumentStringLessMin, paramName));
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// после создания перечислителя семейство было изменено.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidOperationException OperationEnumerationChanged()
    {
        //  После создания перечислителя семейство было изменено.
        return new(Messages.OperationEnumerationChanged);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// произошла попытка выполнить недопустимую операцию.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidOperationException OperationInvalid()
    {
        //  Недопустимая операция.
        return new(Messages.OperationInvalid);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// произошла попытка выполнить недопустимое преобразование.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidCastException OpertionInvalidCast()
    {
        //  Недопустимое преобразование.
        return new(Messages.OperationInvalidConversion);
    }

    /// <summary>
    /// Возвращает исключение, сообщающее о том, что
    /// операция не поддерживается.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static NotSupportedException OperationNotSupported()
    {
        //  Операция не поддерживается.
        return new(Messages.OperationNotSupported);
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
    public static ObjectDisposedException OperationObjectDisposed(string? objectName)
    {
        //  В результате операции произошло обращение к разрушенному объекту.
        return new(objectName, Messages.OperationObjectDisposed);
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// операция привела к переполнению.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static OverflowException OperationOverflow()
    {
        //  Операция привела к переполнению.
        return new(Messages.OperationOverflow);
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// время ожидания операции истекло.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static TimeoutException OperationTimeout()
    {
        //  Время ожидания операции истекло.
        return new(Messages.OperationTimeout);
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// достигнут конец потока.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static EndOfStreamException StreamEnd()
    {
        //  Достигнут конец потока.
        return new(Messages.StreamEnd);
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// поток имеет неверный формат.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static IOException StreamInvalidFormat()
    {
        //  Неверный формат потока.
        return new(Messages.StreamInvalidFormat);
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// поток не поддерживает чтение.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidOperationException StreamNotReadable()
    {
        //  Поток не поддерживает чтение.
        return new(Messages.StreamNotReadable);
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// поток не поддерживает поиск.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidOperationException StreamNotSearchable()
    {
        //  Поток не поддерживает поиск.
        return new(Messages.StreamNotSearchable);
    }

    /// <summary>
    /// Возвращает исключение, сообщающе о том, что
    /// поток не поддерживает запись.
    /// </summary>
    /// <returns>
    /// Новое исключение.
    /// </returns>
    public static InvalidOperationException StreamNotWritable()
    {
        //  Поток не поддерживает запись.
        return new(Messages.StreamNotWritable);
    }
}
