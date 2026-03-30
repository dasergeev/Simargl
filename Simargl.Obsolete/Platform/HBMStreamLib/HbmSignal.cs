using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Nodes;

namespace Simargl.QuantumX;

/// <summary>
/// Представляет класс сигнала.
/// </summary>
public class HbmSignal
{
    /// <summary>
    /// Представляет номер сигнала.
    /// </summary>
    public int SignalNumber { get; private set; }

    /// <summary>
    /// Представляет описание сигнала
    /// </summary>		
    public string SignalReference { get; private set; }

    /// <summary>
    /// Представляе массив значений.
    /// </summary>
    public List<double> SignalValue { get; private set; } = new();

    /// <summary>
    /// Представляе массив меток времени.
    /// </summary>
    [CLSCompliant(false)]
    public List<ulong> SignalNtp { get; private set; } = new();

    /// <summary>
    /// Представляет временный буфер разбора данных.
    /// </summary>
    private double[] ParsedData = new double[128];

    /// <summary>
    /// Представляет тип данных значения.
    /// </summary>
    private DataType_t DataValueType { get; set; }

    /// <summary>
    /// Представляет размер данных значения.
    /// </summary>
    private long DataValueSize { get; set; }

    /// <summary>
    ///  Представляет перечисление типов временой метки.
    /// </summary>
    private enum TimeType_t
    {
        TIMETYPE_NTP
    };

    /// <summary>
    /// Представляет тип временой метки.
    /// </summary>
    private TimeType_t DataTimeType { get; set; }

    /// <summary>
    /// Представляет размер временой метки.
    /// </summary>
    private long DataTimeSize { get; set; }




    /// <summary>
    /// Перечисление типа данных.
    /// </summary>
    public enum Pattern_t
    {
        /// "V"; No timestamps, values only. Signal rate is received first.
        /// Time stamp of first value is received as meat information before value.
        /// Increment with delta time from signal rate for each value.
        PATTERN_V,
        /// "TV"; One timestamp per value, first comes the timestamp, then the value.
        /// This pattern is used for asynchonous values.
        PATTERN_TV,
        /// "TB"; One timestamp per signal block. The timestamp corresponds to the first sample in the signal block.
        PATTERN_TB,
    };

    /// <summary>
    /// Представляет шаблон данных.
    /// </summary>
    private Pattern_t DataFormatPattern { get; set; }

    /// <summary>
    /// Признак представления данных big endian (сетевой порядок байт) или little endian
    /// </summary>
    private bool IsDataBigEndian { get; set; }

    /// <summary>
    /// Допустимые типы данных, которые могут поступать от устройства
    /// </summary>
    public enum DataType_t
    {
        /// <summary>
        /// Тип Uint32
        /// </summary>
        DATATYPE_U32,
        /// <summary>
        /// Тип Int32
        /// </summary>
        DATATYPE_S32,
        /// <summary>
        /// Тип float
        /// </summary>
        DATATYPE_REAL32,
        /// <summary>
        /// Тип Uint64
        /// </summary>
        DATATYPE_U64,
        /// <summary>
        /// Тип Int64
        /// </summary>
        DATATYPE_S64,
        /// <summary>
        /// Тип double
        /// </summary>
        DATATYPE_REAL64,
        /// <summary>
        /// Тип CAN интерфейса
        /// </summary>
        DATATYPE_CANRAW,
    };
    /// <summary>
    /// Инициализирут объект класса.
    /// </summary>
    /// <param name="signalNumber">Номер сигнала.</param>
    public HbmSignal(int signalNumber)
    {
        //  Установка номера канала
        SignalNumber = signalNumber;

        //  Установка заглушик описания
        SignalReference = "";
    }

    /// <summary>
    /// Представляет функцию получения формата канала.
    /// </summary>
    /// <param name="prms">Парметры.</param>
    /// <exception cref="ArgumentException">
    /// Получено не извесное значение параметра.
    /// </exception>
    private void SetDataFormat(JsonNode prms)
    {
        //   Получение параметра.
        var value = prms["pattern"];

        // Получение строки для сравнения.
        string dataFormatPattern = value != null ? value.ToString() : "";
        
        //  Проверка параметра
        if (dataFormatPattern == "V")
        {
            //  Установка значения
            DataFormatPattern = Pattern_t.PATTERN_V;
        }
        else if (dataFormatPattern == "TV")
        {
            //  Установка значения
            DataFormatPattern = Pattern_t.PATTERN_TV;
        }
        else if (dataFormatPattern == "TB")
        {
            //  Установка значения
            DataFormatPattern = Pattern_t.PATTERN_TB;
        }
        else
        {
            //  Получено неизвестное значение параметра
            throw new ArgumentException("Invalid data pattern: {dataFormatPattern}");
        }

        //   Получение параметра.
        value = prms["endian"];

        // Получение строки для сравнения.
        string endianness = value != null ? value.ToString() : "";


        //  Проверка параметра
        if (endianness == "big")
        {
            //  Установка значения
            IsDataBigEndian = true;
        }
        else if (endianness == "little")
        {
            //  Установка значения
            IsDataBigEndian = false;
        }
        else
        {
            //  Получено неизвестное значение параметра
            throw new ArgumentException("Invalid endianness: {endianness}");
        }

        //   Получение параметра.
        value = prms["valueType"];

        // Получение строки для сравнения.
        string dataValueType = value != null ? value.ToString() : "";

        //  Проверка параметра
        if (dataValueType == "real32")
        {
            //  Установка значений
            DataValueType = DataType_t.DATATYPE_REAL32;
            DataValueSize = 4;
        }
        else if (dataValueType == "u32")
        {
            //  Установка значений
            DataValueType = DataType_t.DATATYPE_U32;
            DataValueSize = 4;
        }
        else if (dataValueType == "s32")
        {
            //  Установка значений
            DataValueType = DataType_t.DATATYPE_S32;
            DataValueSize = 4;
        }
        else if (dataValueType == "real64")
        {
            //  Установка значений
            DataValueType = DataType_t.DATATYPE_REAL64;
            DataValueSize = 8;
        }
        else if (dataValueType == "u64")
        {
            //  Установка значений
            DataValueType = DataType_t.DATATYPE_U64;
            DataValueSize = 8;
        }
        else if (dataValueType == "s64")
        {
            //  Установка значений
            DataValueType = DataType_t.DATATYPE_S64;
            DataValueSize = 8;
        }
        else if (dataValueType == "CanRaw")
        {
            //  Установка значений
            DataValueType = DataType_t.DATATYPE_CANRAW;
            DataValueSize = 1;
        }
        else
        {
            //  Получено неизвестное значение параметра
            throw new ArgumentException("Invalid value type: {dataValueType}");
        }

        //   Получение параметра.
        value = prms["time"]?["type"];

        // Получение строки для сравнения.
        string dataTimeType = value != null ? value.ToString() : "";

        //  Проверка параметра
        if (dataTimeType == "ntp")
        {
            //  Установка значения
            DataTimeType = TimeType_t.TIMETYPE_NTP;
        }

        //   Получение параметра.
        value = prms["time"]?["size"];

        //  Установка значения
        DataTimeSize = value != null ? (long)value : 0;
    }
    /// <summary>
    /// Функция меняет порядок байт
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static byte[] SwapBytes(byte[] value)
    {
        Array.Reverse(value);
        return value;
    }

    /// <summary>
    /// Представляет функцию получения Float
    /// </summary>
    /// <param name="reader">Читатель</param>
    /// <returns>Значение</returns>
    private float ExtractFloat(BinaryReader reader)
    {
        //  Чтение
        var bytes = reader.ReadBytes(sizeof(float));

        //  Проверка порядка байт
        if (IsDataBigEndian)
        {
            //  Перетасовка байт
            bytes = SwapBytes(bytes);
        }

        //  Возврат значения
        return BitConverter.ToSingle(bytes);
    }

    /// <summary>
    /// Представляет функцию получения Double
    /// </summary>
    /// <param name="reader">Читатель</param>
    /// <returns>Значение</returns>
    private double ExtractDouble(BinaryReader reader)
    {
        //  Чтение
        var bytes = reader.ReadBytes(sizeof(double));

        //  Проверка порядка байт
        if (IsDataBigEndian)
        {
            //  Перетасовка байт
            bytes = SwapBytes(bytes);
        }

        //  Возврат значения
        return BitConverter.ToDouble(bytes);
    }

    /// <summary>
    /// Представляет функцию получения Uint32
    /// </summary>
    /// <param name="reader">Читатель</param>
    /// <returns>Значение</returns>
    private uint ExtractUInt32(BinaryReader reader)
    {
        //  Чтение
        var bytes = reader.ReadBytes(sizeof(uint));

        //  Проверка порядка байт
        if (IsDataBigEndian)
        {
            //  Перетасовка байт
            bytes = SwapBytes(bytes);
        }

        //  Возврат значения
        return BitConverter.ToUInt32(bytes);
    }

    /// <summary>
    /// Представляет функцию получения Int32
    /// </summary>
    /// <param name="reader">Читатель</param>
    /// <returns>Значение</returns>
    private int ExtractInt32(BinaryReader reader)
    {
        //  Чтение
        var bytes = reader.ReadBytes(sizeof(int));

        //  Проверка порядка байт
        if (IsDataBigEndian)
        {
            //  Перетасовка байт
            bytes = SwapBytes(bytes);
        }

        //  Возврат значения
        return BitConverter.ToInt32(bytes);
    }

    /// <summary>
    /// Представляет функцию получения Int64
    /// </summary>
    /// <param name="reader">Читатель</param>
    /// <returns>Значение</returns>
    private long ExtractInt64(BinaryReader reader)
    {
        //  Чтение
        var bytes = reader.ReadBytes(sizeof(long));

        //  Проверка порядка байт
        if (IsDataBigEndian)
        {
            //  Перетасовка байт
            bytes = SwapBytes(bytes);
        }

        //  Возврат значения
        return BitConverter.ToInt64(bytes);
    }

    /// <summary>
    /// Представляет функцию получения Uint64
    /// </summary>
    /// <param name="reader">Читатель</param>
    /// <returns>Значение</returns>
    private ulong ExtractUInt64(BinaryReader reader)
    {
        //  Чтение
        var bytes = reader.ReadBytes(sizeof(ulong));

        //  Проверка порядка байт
        if (IsDataBigEndian)
        {
            //  Перетасовка байт
            bytes = SwapBytes(bytes);
        }

        //  Возврат значения
        return BitConverter.ToUInt64(bytes);
    }

    /// <summary>
    /// Представляет функцию получения byte
    /// </summary>
    /// <param name="reader">Читатель</param>
    /// <returns>Значение</returns>
    private static byte ExtractByte(BinaryReader reader)
    {
        //  Возврат значения
        return reader.ReadByte();
    }

    /// <summary>
    /// Преобразование даных.
    /// </summary>
    /// <param name="stream">Поток</param>
    /// <param name="count">Количество значений.</param>
    /// <exception cref="ArgumentException">
    /// Исключение - неизвестный тип данных.
    /// </exception>
    private void InterpretValuesAsDouble(MemoryStream stream, long count)
    {
        //  Получение читателя
        using BinaryReader reader = new(stream, Encoding.UTF8, true);

        //   Цикл по всем значениям
        for (long i = 0; i < count; i++)
        {
            //  Свитч по типу данных
            switch (DataValueType)
            {
                case DataType_t.DATATYPE_REAL32:
                    {
                        //  Получение данных
                        ParsedData[i] = ExtractFloat(reader);
                    }
                    break;
                case DataType_t.DATATYPE_REAL64:
                    {
                        //  Получение данных
                        ParsedData[i] = ExtractDouble(reader);
                    }
                    break;
                case DataType_t.DATATYPE_U32:
                    {
                        //  Получение данных
                        ParsedData[i] = ExtractUInt32(reader);
                    }
                    break;
                case DataType_t.DATATYPE_S32:
                    {
                        //  Получение данных
                        ParsedData[i] = ExtractInt32(reader);
                    }
                    break;
                case DataType_t.DATATYPE_S64:
                    {
                        //  Получение данных
                        ParsedData[i] = ExtractInt64(reader);
                    }
                    break;
                case DataType_t.DATATYPE_U64:
                    {
                        //  Получение данных
                        ParsedData[i] = ExtractUInt64(reader);
                    }
                    break;
                case DataType_t.DATATYPE_CANRAW:
                    {
                        //  Получение данных
                        ParsedData[i] = ExtractByte(reader);
                    }
                    break;
                default:
                    //  Выброс исключения
                    throw new ArgumentException(nameof(DataValueType));

            }
        }
    }

    /// <summary>
    /// Преобразует временную метку.
    /// </summary>
    /// <param name="stream">Поток</param>
    /// <returns>NTP Timestamp</returns>
    private ulong InterpreteNtpTimestamp(MemoryStream stream)
    {
        //  Инициализация значения
        ulong value = 0;

        //  Проверка типа
        if (DataTimeType == TimeType_t.TIMETYPE_NTP)
        {
            //  Создание читателя
            using BinaryReader reader = new(stream, Encoding.UTF8, true);
            
            //  Получение значения
            value = ExtractUInt64(reader);
        }

        //  Возврат значения
        return value;
    }

    /// <summary>
    /// Функция обработки измеренных данных
    /// </summary>
    /// <param name="pData">Массив данных</param>
    /// <param name="size">Размер массива данных</param>
    /// <returns>Количество обработанных байт</returns>
    public void ProcessMeasuredData(byte[] pData, long size)
    {
        //  Создание потока памяти.
        using MemoryStream mstream = new(pData);

        //  Выполнить для указанного формата.
        switch (DataFormatPattern)
        {
            case Pattern_t.PATTERN_V:
                {
                    //  Подсчет количество значений
                    long valueCount = size / DataValueSize;
                    
                    //  Если некоректное значения количества
                    if (valueCount <= 0)
                    {
                        //  Выход из свича.
                        break;
                    }

                    //  Если в буфере не достаточно емкости
                    if (ParsedData.Length < valueCount)
                    {
                        //  Перевыделение памяти.
                        ParsedData = new double[valueCount];
                    }

                    //  Разбор данных
                    InterpretValuesAsDouble(mstream, valueCount);

                    //  Если данных больше 1
                    if (valueCount > 1)
                    {
                        //  Выделение временного буфера
                        double[] temp = new double[valueCount];

                        //  Копирование.
                        Array.Copy(ParsedData, temp, valueCount);

                        //  Добавление в список.
                        SignalValue.AddRange(temp);
                    }
                    else
                    {
                        //  Добавление 1 значения.
                        SignalValue.Add(ParsedData[0]);
                    }
                }
                break;
                //  Устройство не дает такой формат
            case Pattern_t.PATTERN_TV:
                {
                    //   Подсчет длины.
                    long tupleSize = DataTimeSize + DataValueSize;

                    while (size >= tupleSize)
                    {
                        //  Получение метки времени
                        ulong ntpTimeStamp = InterpreteNtpTimestamp(mstream);

                        //  Сохранение метки времени.
                        SignalNtp.Add(ntpTimeStamp);

                        //  Преобразование значения
                        InterpretValuesAsDouble(mstream, 1);

                        //  Добавление в список.
                        SignalValue.Add(ParsedData[0]);

                        //  Пересчет длины.
                        size -= tupleSize;
                    }
                }
                break;
            //  Устройство не дает такой формат
            case Pattern_t.PATTERN_TB:
                {
                    //  Подсчет длины
                    long tupleSize = DataTimeSize + DataValueSize;

                    //  Проверка целостности пакета.
                    if (size >= tupleSize)
                    {
                        //  Подсчет количества значений.
                        long valueCount = (size - DataTimeSize) / DataValueSize;

                        //  Если в буфере не достаточно емкости
                        if (ParsedData.Length < valueCount)
                        {
                            //  Перевыделение памяти.
                            ParsedData = new double[valueCount];
                        }

                        //  Получение метки времени
                        ulong ntpTimeStamp = InterpreteNtpTimestamp(mstream);

                        //  Сохранение метки времени.
                        SignalNtp.Add(ntpTimeStamp);

                        //  Разбор данных
                        InterpretValuesAsDouble(mstream, valueCount);

                        //  Если данных больше 1
                        if (valueCount > 1)
                        {
                            //  Выделение временного буфера
                            double[] temp = new double[valueCount];

                            //  Копирование.
                            Array.Copy(ParsedData, temp, valueCount);

                            //  Добавление в список.
                            SignalValue.AddRange(temp);
                        }
                        else
                        {
                            //  Добавление 1 значения.
                            SignalValue.Add(ParsedData[0]);
                        }
                    }
                }
                break;
        }
    }
    /// <summary>
    /// Функция обработки метаинформации сигнала
    /// </summary>
    /// <param name="metaInfo"></param>
    /// <exception cref="NotImplementedException">
    /// Получена не обработанная мета информация.
    /// </exception>
    public void ProcessSignalMetaInformation(HbmMetaInfo metaInfo)
    {
        //  Получение значения метода
        string? method = metaInfo.Method?.GetValue<string>();

        //  Получение параметров.
        var parameters = metaInfo.Params;

        //  Проверка ссылок.
        if (method is null || parameters is null)
        {
            // Возврат из функции
            return;
        }

        //  Проверка метода
        if (method.Equals("subscribe"))
        {
            //  Установк опорного параметра сигнала
            SignalReference = parameters[0]!.ToString();
            return;
        }


        //  Проверка метода
        if (method.Equals("time"))
        {
            // Возврат из функции
            return;
        }

        //  Проверка метода
        if (method.Equals("data"))
        {
            SetDataFormat(parameters);

            // Возврат из функции
            return;
        }

        //  Проверка метода
        if (method.Equals("signalRate"))
        {
            // Возврат из функции
            return;
        }

        //  Создание сообщение об ошибке.
        string errorMsg = "Unhandled signal related meta information " +
        "'" + metaInfo.Method + 
        "' for signal " + 
        SignalReference + 
        " with parameters: " + 
        parameters.ToString();


        //  Выброс исключения.
        throw new NotImplementedException(errorMsg);
    }

}
