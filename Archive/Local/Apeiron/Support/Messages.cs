using Apeiron.Properties;

namespace Apeiron.Support;

/// <summary>
/// Предоставляет вспомогательные сообщения.
/// </summary>
public static class Messages
{
    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Неверная контрольная сумма пакета данных Adxl.".
    /// </summary>
    public static string AdxlInvalidChecksum => Resources.AdxlInvalidChecksum;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Неверный формат пакета данных Adxl.".
    /// </summary>
    public static string AdxlInvalidFormat => Resources.AdxlInvalidFormat;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Неверная сигнатура пакета данных Adxl.".
    /// </summary>
    public static string AdxlInvalidSignature => Resources.AdxlInvalidSignature;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передано значение, которое уже содержится коллекции.".
    /// </summary>
    public static string ArgumentAlreadyContainedInCollection => Resources.ArgumentAlreadyContainedInCollection;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передан массив, размер которого больше максимально допустимого.".
    /// </summary>
    public static string ArgumentArrayLargerMax => Resources.ArgumentArrayLargerMax;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передан массив, размер которого меньше минимально допустимого.".
    /// </summary>
    public static string ArgumentArrayLessMin => Resources.ArgumentArrayLessMin;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передан пустой массив.".
    /// </summary>
    public static string ArgumentEmptyArray => Resources.ArgumentEmptyArray;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передана пустая коллекция.".
    /// </summary>
    public static string ArgumentEmptyCollection => Resources.ArgumentEmptyCollection;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передана пустая строка.".
    /// </summary>
    public static string ArgumentEmptyString => Resources.ArgumentEmptyString;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передан пустой вектор.".
    /// </summary>
    public static string ArgumentEmptyVector => Resources.ArgumentEmptyVector;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передан объект недопустимого типа.".
    /// </summary>
    public static string ArgumentInvalidType => Resources.ArgumentInvalidType;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передан многомерный массив.".
    /// </summary>
    public static string ArgumentMultidimensionalArray => Resources.ArgumentMultidimensionalArray;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передано отрицательное значение.".
    /// </summary>
    public static string ArgumentNegativeValue => Resources.ArgumentNegativeValue;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передано значение, которое не содержится в коллекции.".
    /// </summary>
    public static string ArgumentNotContainedInCollection => Resources.ArgumentNotContainedInCollection;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передано значение, которое не содержится в перечислении {1}.".
    /// </summary>
    public static string ArgumentNotContainedInEnumeration => Resources.ArgumentNotContainedInEnumeration;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передано значение, которое не является степенью двух.".
    /// </summary>
    public static string ArgumentNotPowerOfTwo => Resources.ArgumentNotPowerOfTwo;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передана пустая ссылка.".
    /// </summary>
    public static string ArgumentNullReference => Resources.ArgumentNullReference;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передано значение, которое не соответствует допустимому диапазону значений.".
    /// </summary>
    public static string ArgumentOutOfRange => Resources.ArgumentOutOfRange;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передан диапазон, в котором начальный индекс превышает допустимое значение.".
    /// </summary>
    public static string ArgumentRangeLargeIndex => Resources.ArgumentRangeLargeIndex;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передан диапазон, в котором начальный индекс отрицательный.".
    /// </summary>
    public static string ArgumentRangeNegativeIndex => Resources.ArgumentRangeNegativeIndex;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передан диапазон, в котором отрицательная длина.".
    /// </summary>
    public static string ArgumentRangeNegativeLength => Resources.ArgumentRangeNegativeLength;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передан диапазон, который выходит за пределы коллекции.".
    /// </summary>
    public static string ArgumentRangeNotBelong => Resources.ArgumentRangeNotBelong;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передана строка, которая содержит недопустимый символ.".
    /// </summary>
    public static string ArgumentStringContainInvalidChar => Resources.ArgumentStringContainInvalidChar;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передана строка, длина которой больше максимально допустимого.".
    /// </summary>
    public static string ArgumentStringLargerMax => Resources.ArgumentStringLargerMax;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передана строка, длина которой меньше минимально допустимого.".
    /// </summary>
    public static string ArgumentStringLessMin => Resources.ArgumentStringLessMin;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передано нулевое значение.".
    /// </summary>
    public static string ArgumentZeroValue => Resources.ArgumentZeroValue;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Данные базы данных не загружены.".
    /// </summary>
    public static string DatabaseDataNotLoaded => Resources.DatabaseDataNotLoaded;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Таблица базы данных не найдена.".
    /// </summary>
    public static string DatabaseTableNotFound => Resources.DatabaseTableNotFound;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Размеры данных не совпадают.".
    /// </summary>
    public static string DataSizesDoNotMatch => Resources.DataSizesDoNotMatch;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Неверная сигнатура кадра.".
    /// </summary>
    public static string FrameInvalidSignature => Resources.FrameInvalidSignature;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Член {0} не найден.".
    /// </summary>
    public static string MarshalMemberNotFound => Resources.MarshalMemberNotFound;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Объект {0} не найден.".
    /// </summary>
    public static string MarshalObjectNotFound => Resources.MarshalObjectNotFound;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Неверная контрольная сумма строки в формате NMEA.".
    /// </summary>
    public static string NmeaInvalidChecksum => Resources.NmeaInvalidChecksum;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Неверный формат строки в формате NMEA.".
    /// </summary>
    public static string NmeaInvalidFormat => Resources.NmeaInvalidFormat;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Неверная сигнатура строки в формате NMEA.".
    /// </summary>
    public static string NmeaInvalidSignature => Resources.NmeaInvalidSignature;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Операция отменена.".
    /// </summary>
    public static string OperationCanceled => Resources.OperationCanceled;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "После создания перечислителя семейство было изменено.".
    /// </summary>
    public static string OperationEnumerationChanged => Resources.OperationEnumerationChanged;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Недопустимая операция.".
    /// </summary>
    public static string OperationInvalid => Resources.OperationInvalid;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Недопустимое преобразование.".
    /// </summary>
    public static string OperationInvalidConversion => Resources.OperationInvalidConversion;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Операция не поддерживается.".
    /// </summary>
    public static string OperationNotSupported => Resources.OperationNotSupported;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В результате операции произошло обращение к разрушенному объекту.".
    /// </summary>
    public static string OperationObjectDisposed => Resources.OperationObjectDisposed;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Операция привела к переполнению.".
    /// </summary>
    public static string OperationOverflow => Resources.OperationOverflow;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Время ожидания операции истекло.".
    /// </summary>
    public static string OperationTimeout => Resources.OperationTimeout;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Достигнут конец потока.".
    /// </summary>
    public static string StreamEnd => Resources.StreamEnd;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Неверный формат кадра.".
    /// </summary>
    public static string FrameInvalidFormat => Resources.FrameInvalidFormat;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Неверный формат потока.".
    /// </summary>
    public static string StreamInvalidFormat => Resources.StreamInvalidFormat;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Поток не поддерживает чтение.".
    /// </summary>
    public static string StreamNotReadable => Resources.StreamNotReadable;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Поток не поддерживает поиск.".
    /// </summary>
    public static string StreamNotSearchable => Resources.StreamNotSearchable;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "Поток не поддерживает запись.".
    /// </summary>
    public static string StreamNotWritable => Resources.StreamNotWritable;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передано бесконечное значение.".
    /// </summary>
    public static string ArgumentInfinity => Resources.ArgumentInfinity;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передан недопустимый таймаут.".
    /// </summary>
    public static string ArgumentInvalidTimeout => Resources.ArgumentInvalidTimeout;

    /// <summary>
    /// Возвращает локализованную строку, похожую на
    /// "В параметре {0} передано нечисловое значение.".
    /// </summary>
    public static string ArgumentNaN => Resources.ArgumentNaN;
}
