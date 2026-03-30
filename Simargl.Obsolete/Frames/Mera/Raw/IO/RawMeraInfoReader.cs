using Simargl.Designing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Simargl.Frames.Mera.Raw;

/// <summary>
/// Представляет средство чтения сырого информационного файла в формате <see cref="StorageFormat.Mera"/>.
/// </summary>
/// <param name="stream">
/// Поток для чтения исходных данных.
/// </param>
/// <param name="encoding">
/// Кодировка символов, которую нужно использовать.
/// </param>
/// <param name="leaveOpen">
/// Значение, определяющее, нужно ли оставить поток открытым после удаления объекта.
/// </param>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="stream"/> передана пустая ссылка.
/// </exception>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="encoding"/> передана пустая ссылка.
/// </exception>
public sealed class RawMeraInfoReader(Stream stream, Encoding encoding, bool leaveOpen) :
    IDisposable
{
    /// <summary>
    /// Создание средства чтения потока.
    /// </summary>
    private StreamReader _Reader = new(
        IsNotNull(stream, nameof(stream)),
        encoding: IsNotNull(encoding, nameof(encoding)),
        leaveOpen: leaveOpen);

    /// <summary>
    /// Поле для хранения последнего раздела сырого информационного файла в формате <see cref="StorageFormat.Mera"/>.
    /// </summary>
    private RawMeraInfoSection? _LastSection = null;

    /// <summary>
    /// Асинхронно выполняет чтение заголовка кадра.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение заголовка кадра.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Обнаружен неизвестный элемент информационного файла в формате <see cref="StorageFormat.Mera"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Некорректная последовательность чтения данных.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Не найден заголовок кадра.
    /// </exception>
    public async Task<MeraFrameHeader> ReadFrameHeader(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Заголовок кадра.
        MeraFrameHeader? header = null;

        //  Время проведения испытания.
        string? time = null;

        //  Дата проведения испытания.
        string? date = null;

        //  Основной цикл чтения.
        while (true)
        {
            //  Чтение элемента.
            RawMeraInfoElement? element = await ReadElementAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка прочитанного элемента.
            if (element is null)
            {
                //  Завершение чтения.
                break;
            }

            //  Проверка раздела.
            if (element is RawMeraInfoSection section)
            {
                //  Проверка заголовка кадра.
                if (header is null)
                {
                    //  Проверка имени раздела.
                    if (section.Name != "MERA")
                    {
                        //  Завершение чтения.
                        break;
                    }

                    //  Создание заголовка кадра.
                    header = new();
                }
                else
                {
                    //  Установка последнего раздела.
                    _LastSection = section;

                    //  Завершение чтения.
                    break;
                }
            }

            //  Проверка поля.
            if (element is RawMeraInfoField field)
            {
                //  Проверка заголовка кадра.
                if (header is null)
                {
                    //  Завершение чтения.
                    break;
                }

                //  Разбор имени поля.
                switch (field.Name)
                {
                    case "Time":
                        //  Установка времени проведения испытания.
                        time = field.Value;

                        //  Завершение разбора.
                        break;
                    case "Date":
                        //  Установка даты проведения испытания.
                        date = field.Value;

                        //  Завершение разбора.
                        break;
                    case "Test":
                        //  Установка названия испытаний.
                        header.Title = field.Value;

                        //  Завершение разбора.
                        break;
                    case "Prod":
                        //  Установка названия изделия.
                        header.Product = field.Value;

                        //  Завершение разбора.
                        break;
                    case "DataSourceApp":
                        //  Установка приложения.
                        header.DataSourceApplication = field.Value;

                        //  Завершение разбора.
                        break;
                    case "DataSourceVer":
                        //  Установка версии.
                        header.DataSourceVersion = field.Value;

                        //  Завершение разбора.
                        break;
                    default:
                        //  Завершение разбора.
                        break;
                }
            }
        }

        //  Проверка заголовка кадра.
        if (header is null)
        {
            throw new InvalidDataException("Не найден заголовок кадра.");
        }

        //  Попытка преобразовать значения полей в дату и время.
        if (RawMeraInfoFieldConverter.TryParse(date, time, out DateTime result))
        {
            //  Установка времени.
            header.Time = result;
        }

        //  Возврат заголовка кадра.
        return header;
    }

    /// <summary>
    /// Асинхронно выполняет чтение заголовка канала.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение заголовка канала.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Обнаружен неизвестный элемент информационного файла в формате <see cref="StorageFormat.Mera"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Некорректная последовательность чтения данных.
    /// </exception>
    public async Task<RawMeraChannelHeader?> ReadChannelHeaderAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка последнего раздела.
        if (_LastSection is null)
        {
            //  Достигнут конец потока.
            return null;
        }

        //  Определение имени канала.
        string name = _LastSection.Name;

        //  Сброс последнего раздела.
        _LastSection = null;

        //  Значения допустимых полей.
        (
            string? Start,
            string? Address,
            string? Comment,
            string? Freq,
            string? k0,
            string? k1,
            string? ModName,
            string? ModSN,
            string? tare0,
            string? XUnits,
            string? YFormat,
            string? YUnits
        ) fields = new();

        //  Основной цикл чтения.
        while (true)
        {
            //  Чтение элемента.
            RawMeraInfoElement? element = await ReadElementAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка прочитанного элемента.
            if (element is null)
            {
                //  Завершение чтения.
                break;
            }

            //  Проверка раздела.
            if (element is RawMeraInfoSection section)
            {
                //  Установка последнего раздела.
                _LastSection = section;

                //  Завершение чтения.
                break;
            }

            //  Проверка поля.
            if (element is RawMeraInfoField field)
            {
                //  Разбор имени поля.
                switch (field.Name)
                {
                    case "Start":
                        //  Установка значения.
                        fields.Start = field.Value;

                        //  Завершение разбора.
                        break;
                    case "Address":
                        //  Установка значения.
                        fields.Address = field.Value;

                        //  Завершение разбора.
                        break;
                    case "Comment":
                        //  Установка значения.
                        fields.Comment = field.Value;

                        //  Завершение разбора.
                        break;
                    case "Freq":
                        //  Установка значения.
                        fields.Freq = field.Value;

                        //  Завершение разбора.
                        break;
                    case "k0":
                        //  Установка значения.
                        fields.k0 = field.Value;

                        //  Завершение разбора.
                        break;
                    case "k1":
                        //  Установка значения.
                        fields.k1 = field.Value;

                        //  Завершение разбора.
                        break;
                    case "ModName":
                        //  Установка значения.
                        fields.ModName = field.Value;

                        //  Завершение разбора.
                        break;
                    case "ModSN":
                        //  Установка значения.
                        fields.ModSN = field.Value;

                        //  Завершение разбора.
                        break;
                    case "tare0":
                        //  Установка значения.
                        fields.tare0 = field.Value;

                        //  Завершение разбора.
                        break;
                    case "XUnits":
                        //  Установка значения.
                        fields.XUnits = field.Value;

                        //  Завершение разбора.
                        break;
                    case "YFormat":
                        //  Установка значения.
                        fields.YFormat = field.Value;

                        //  Завершение разбора.
                        break;
                    case "YUnits":
                        //  Установка значения.
                        fields.YUnits = field.Value;

                        //  Завершение разбора.
                        break;
                    case "XUnitsId":
                    case "maxY":
                    case "minY":
                        //  Завершение разбора.
                        break;
                    default:
                        //  Неизвестное поле.
                        throw new InvalidDataException($"Обнаружено неизвестное поле с именем \"{field.Name}\".");
                }
            }
        }

        //  Проверка размерности данных по оси Ox.
        if (fields.XUnits is not null && fields.XUnits != "сек")
        {
            throw new NotSupportedException($"Значение \"{fields.XUnits}\" поля с именем \"XUnits\" не поддерживается.");
        }

        //  Проверка поля "tare0".
        if (fields.tare0 is not null && fields.tare0 != "\"\"")
        {
            throw new NotSupportedException($"Значение \"{fields.tare0}\" поля с именем \"Xtare0\" не поддерживается.");
        }

        //  Частота дискретизации.
        double sampling = 1;

        //  Определение частоты дискретизации.
        if (fields.Freq is not null && !RawMeraInfoFieldConverter.TryParse(fields.Freq, out sampling))
        {
            throw new InvalidDataException($"Некорректное значение поля с именем \"Freq\": \"{fields.Freq}\".");
        }

        //  Формат данных.
        MeraDataFormat dataFormat = MeraDataFormat.Int16;

        //  Определение формата данных.
        if (fields.YFormat is not null)
        {
            dataFormat = fields.YFormat switch
            {
                "I1" or "byte" => MeraDataFormat.Int8,
                "UI1" => MeraDataFormat.UInt8,
                "I2" or "int" => MeraDataFormat.Int16,
                "UI2" => MeraDataFormat.UInt16,
                "I4" or "int32" => MeraDataFormat.Int32,
                "I8" => MeraDataFormat.Int64,
                "R4" or "single" => MeraDataFormat.Float32,
                "R8" or "double" => MeraDataFormat.Float64,
                _ => throw new InvalidDataException($"Некорректное значение поля с именем \"YFormat\": \"{fields.YFormat}\"."),
            };
        }

        //  Масштаб.
        double scale = 1;

        //  Определение масштаба.
        if (fields.k1 is not null && !RawMeraInfoFieldConverter.TryParse(fields.k1, out scale))
        {
            throw new InvalidDataException($"Некорректное значение поля с именем \"k1\": \"{fields.k1}\".");
        }

        //  Проверка масштаба.
        if (scale == 0)
        {
            //  Корректировка масштаба.
            scale = 1;
        }

        //  Смещение значений.
        double offset = 0;

        //  Определение смещения значений.
        if (fields.k0 is not null && !RawMeraInfoFieldConverter.TryParse(fields.k0, out offset))
        {
            throw new InvalidDataException($"Некорректное значение поля с именем \"k0\": \"{fields.k0}\".");
        }

        //  Смещение начала записи.
        double startValue = 0;

        //  Определение смещения начала записи.
        if (fields.Freq is not null && !RawMeraInfoFieldConverter.TryParse(fields.Start, out startValue))
        {
            throw new InvalidDataException($"Некорректное значение поля с именем \"Start\": \"{fields.Start}\".");
        }

        //  Создание заголовка канала.
        MeraChannelHeader header = new(name, fields.YUnits ?? string.Empty, 0)
        {
            Description = fields.Comment ?? string.Empty,
            Start = TimeSpan.FromSeconds(startValue),
            DataFormat = dataFormat,
            Scale = scale,
            Offset = offset,
            ModuleName = fields.ModName ?? string.Empty,
            ModuleSerialNumber = fields.ModSN ?? string.Empty,
            Address = fields.Address ?? string.Empty,
        };

        //  Возврат прочитанного сырого заголовка.
        return new(header, sampling);
    }

    /// <summary>
    /// Асинхронно выполняет чтение следующего элемента.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая чтение следующего элемента.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Обнаружен неизвестный элемент информационного файла в формате <see cref="StorageFormat.Mera"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Некорректная последовательность чтения данных.
    /// </exception>
    public async Task<RawMeraInfoElement?> ReadElementAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка последнего раздела.
        if (_LastSection is not null)
        {
            throw new InvalidOperationException("Некорректная последовательность чтения данных.");
        }

        //  Следующая строка.
        string? line = string.Empty;

        //  Цикл чтения строки.
        while (line.Length == 0)
        {
            //  Чтение строки.
            line = await _Reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка ссылки на строку.
            if (line is null)
            {
                //  Достигрут конец потока.
                return null;
            }

            //  Нормализация строки.
            line = Normalize(line);
        }

        //  Разбор найденной строки.
        return Parse(line);
    }

    /// <summary>
    /// Выполняет нормализацию строки.
    /// </summary>
    /// <param name="line">
    /// Строка для нормализации.
    /// </param>
    /// <returns>
    /// Нормализованная строка.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string Normalize([NoVerify] string line)
    {
        //  Поиск комментария.
        int index = line.IndexOf(';');

        //  Проверка комментария.
        if (index >= 0)
        {
            //  Отбрасывание комментария.
            line = line[..index];
        }

        //  Обрезка строки.
        return line.Trim();
    }

    /// <summary>
    /// Выполняет синтаксический разбор строки.
    /// </summary>
    /// <param name="line">
    /// Строка для синтаксического разбора.
    /// </param>
    /// <returns>
    /// Элемент сырого информационного файла в формате <see cref="StorageFormat.Mera"/>.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Обнаружен неизвестный элемент информационного файла в формате <see cref="StorageFormat.Mera"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static RawMeraInfoElement? Parse([NoVerify] string line)
    {
        //  Проверка раздела.
        if (line[0] == '[' && line[^1] == ']')
        {
            //  Возврат секции.
            return new RawMeraInfoSection(line[1..^1].Trim());
        }

        //  Поиск разделителя.
        int index = line.IndexOf('=');

        //  Проверка разделителя.
        if (index >= 0)
        {
            //  Возврат поля.
            return new RawMeraInfoField(
                line[..index].Trim(),
                line[(index + 1)..].Trim());
        }

        //  Элемент не найден.
        throw new InvalidDataException("Обнаружен неизвестный элемент информационного файла МЕРА.");
    }

    /// <summary>
    /// Разрушает объект.
    /// </summary>
    public void Dispose()
    {
        //  Разрушение средства чтения потока.
        _Reader?.Dispose();

        //  Очистка полей.
        _Reader = null!;

        //  Сообщение сборщику мусора о разрушении объекта.
        GC.SuppressFinalize(this);
    }
}
