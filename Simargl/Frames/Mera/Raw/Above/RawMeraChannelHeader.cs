using System.IO;
using System.Runtime.CompilerServices;

namespace Simargl.Frames.Mera.Raw;

/// <summary>
/// Представляет сырой заголовок канала в формате <see cref="StorageFormat.Mera"/>.
/// </summary>
/// <param name="header">
/// Заголовок канала в формате <see cref="StorageFormat.Mera"/>.
/// </param>
/// <param name="sampling">
/// Частота дискретизации в Гц.
/// </param>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="header"/> передана пустая ссылка.
/// </exception>
/// <exception cref="ArgumentOutOfRangeException">
/// В параметре <paramref name="sampling"/> передано бесконечное значение.
/// </exception>
/// <exception cref="ArgumentOutOfRangeException">
/// В параметре <paramref name="sampling"/> передано нечисловое значение.
/// </exception>
/// <exception cref="ArgumentOutOfRangeException">
/// В параметре <paramref name="sampling"/> передано отрицательное значение.
/// </exception>
/// <exception cref="ArgumentOutOfRangeException">
/// В параметре <paramref name="sampling"/> передано нулевое значение.
/// </exception>
public class RawMeraChannelHeader(MeraChannelHeader header, double sampling)
{
    /// <summary>
    /// Возвращает заголовок канала в формате <see cref="StorageFormat.Mera"/>.
    /// </summary>
    public MeraChannelHeader Header { get; } = IsNotNull(header, nameof(header));

    /// <summary>
    /// Возвращает частоту дискретизации в Гц.
    /// </summary>
    public double Sampling { get; } = IsSampling(sampling, nameof(sampling));

    /// <summary>
    /// Асинхронно загружает канал.
    /// </summary>
    /// <param name="directory">
    /// Каталог, содержащий данные кадра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая канал.
    /// </returns>
    public async Task<Channel> LoadChannelAsync(DirectoryInfo directory, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Загрузка данных.
        byte[] buffer = await File.ReadAllBytesAsync(
            Path.Combine(directory.FullName, Header.Name + ".dat"),
            cancellationToken).ConfigureAwait(false);

        //  Длина канала.
        int length;

        //  Данные канала.
        double[] data;

        //  Получение масштаба.
        double scale = Header.Scale;

        //  Получение смещения данных.
        double offset = Header.Offset;

        //  Разбор формата данных канала.
        switch (header.DataFormat)
        {
            case MeraDataFormat.Int8:
                {
                    //  Определение длины канала.
                    length = buffer.Length;

                    //  Получение исходных значений.
                    byte[] source = buffer;

                    //  Создание массива данных канала.
                    data = new double[length];

                    //  Перебор значений.
                    for (int i = 0; i < length; i++)
                    {
                        //  Расчёт значения.
                        data[i] = scale * (offset + source[i]);
                    }

                    //  Завершение разбора.
                    break;
                }
            case MeraDataFormat.UInt8:
                {
                    //  Определение длины канала.
                    length = buffer.Length;

                    //  Создание массива исходных значений.
                    sbyte[] source = new sbyte[length];

                    //  Копирование значений.
                    Buffer.BlockCopy(buffer, 0, source, 0, length);

                    //  Создание массива данных канала.
                    data = new double[length];

                    //  Перебор значений.
                    for (int i = 0; i < length; i++)
                    {
                        //  Расчёт значения.
                        data[i] = scale * (offset + source[i]);
                    }

                    //  Завершение разбора.
                    break;
                }
            case MeraDataFormat.Int16:
                {
                    //  Определение длины канала.
                    length = buffer.Length >> 1;

                    //  Создание массива исходных значений.
                    short[] source = new short[length];

                    //  Копирование значений.
                    Buffer.BlockCopy(buffer, 0, source, 0, length << 1);

                    //  Создание массива данных канала.
                    data = new double[length];

                    //  Перебор значений.
                    for (int i = 0; i < length; i++)
                    {
                        //  Расчёт значения.
                        data[i] = scale * (offset + source[i]);
                    }

                    //  Завершение разбора.
                    break;
                }
            case MeraDataFormat.UInt16:
                {
                    //  Определение длины канала.
                    length = buffer.Length >> 1;

                    //  Создание массива исходных значений.
                    ushort[] source = new ushort[length];

                    //  Копирование значений.
                    Buffer.BlockCopy(buffer, 0, source, 0, length << 1);

                    //  Создание массива данных канала.
                    data = new double[length];

                    //  Перебор значений.
                    for (int i = 0; i < length; i++)
                    {
                        //  Расчёт значения.
                        data[i] = scale * (offset + source[i]);
                    }

                    //  Завершение разбора.
                    break;
                }
            case MeraDataFormat.Int32:
                {
                    //  Определение длины канала.
                    length = buffer.Length >> 2;

                    //  Создание массива исходных значений.
                    int[] source = new int[length];

                    //  Копирование значений.
                    Buffer.BlockCopy(buffer, 0, source, 0, length << 2);

                    //  Создание массива данных канала.
                    data = new double[length];

                    //  Перебор значений.
                    for (int i = 0; i < length; i++)
                    {
                        //  Расчёт значения.
                        data[i] = scale * (offset + source[i]);
                    }

                    //  Завершение разбора.
                    break;
                }
            case MeraDataFormat.Int64:
                {
                    //  Определение длины канала.
                    length = buffer.Length >> 3;

                    //  Создание массива исходных значений.
                    long[] source = new long[length];

                    //  Копирование значений.
                    Buffer.BlockCopy(buffer, 0, source, 0, length << 3);

                    //  Создание массива данных канала.
                    data = new double[length];

                    //  Перебор значений.
                    for (int i = 0; i < length; i++)
                    {
                        //  Расчёт значения.
                        data[i] = scale * (offset + source[i]);
                    }

                    //  Завершение разбора.
                    break;
                }
            case MeraDataFormat.Float32:
                {
                    //  Определение длины канала.
                    length = buffer.Length >> 2;

                    //  Создание массива исходных значений.
                    float[] source = new float[length];

                    //  Копирование значений.
                    Buffer.BlockCopy(buffer, 0, source, 0, length << 2);

                    //  Создание массива данных канала.
                    data = new double[length];

                    //  Перебор значений.
                    for (int i = 0; i < length; i++)
                    {
                        //  Расчёт значения.
                        data[i] = scale * (offset + source[i]);
                    }

                    //  Завершение разбора.
                    break;
                }
            case MeraDataFormat.Float64:
                {
                    //  Определение длины канала.
                    length = buffer.Length >> 3;

                    //  Создание массива исходных значений.
                    double[] source = new double[length];

                    //  Копирование значений.
                    Buffer.BlockCopy(buffer, 0, source, 0, length << 3);

                    //  Установка массива данных канала.
                    data = source;

                    //  Перебор значений.
                    for (int i = 0; i < length; i++)
                    {
                        //  Расчёт значения.
                        data[i] = scale * (offset + source[i]);
                    }

                    //  Завершение разбора.
                    break;
                }
            default:
                throw new InvalidDataException($"Обнаружен неизвестный формат данных канала: {header.DataFormat}");
        }

        //  Получение пути к файлу с информацией о порциях записи.
        string path = Path.Combine(directory.FullName, Header.Name + ".prt");

        //  Проверка существования файла.
        if (File.Exists(path))
        {
            //  Чтение всех строк из файла.
            string[] lines = File.ReadAllLines(path);

            //  Перебор строк.
            foreach (string item in lines)
            {
                //  Получение нормализованной строки.
                string line = item.Trim();

                //  Проверка строки.
                if (line.Length > 1)
                {
                    //  Разбивка строки на части.
                    string[] parts = line.Split(' ');

                    //  Проверка частей.
                    if (parts.Length != 2 ||
                        !RawMeraInfoFieldConverter.TryParse(parts[0], out double time) ||
                        !int.TryParse(parts[1], out int dataOffset))
                    {
                        throw new InvalidDataException(
                            "Файл с информацией о смещении и времени начала порции при прерывистой записи содержит некорректные данные.");
                    }

                    //  Добавление новой части.
                    header.PortionInfos.Add(new()
                    {
                        Begin = TimeSpan.FromSeconds(time),
                        Offset = dataOffset,
                    });

                }
            }
        }

        //  Создание и возврат нового канала.
        return new(Header, new(Sampling, data));
    }

    /// <summary>
    /// Выполняет проверку значения частоты дискретизации.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нулевое значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double IsSampling(double value, string? paramName)
    {
        //  Проверка на бесконечное значение.
        IsNotInfinity(value, paramName);

        //  Проверка на нечисловое значение.
        IsNotNaN(value, paramName);

        //  Проверка на отрицательное и нулевое значения.
        IsPositive(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }
}
