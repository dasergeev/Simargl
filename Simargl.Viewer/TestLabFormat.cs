using System.Globalization;
using System.Text;

namespace Simargl.Viewer;

/// <summary>
/// Исключение неверного формата файла TestLab.
/// </summary>
internal sealed class TestLabFormatException : Exception
{
    public TestLabFormatException(string message)
        : base(message)
    {
    }

    public TestLabFormatException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Представляет полностью прочитанный файл регистрации TestLab.
/// </summary>
internal sealed record TestLabDocument(
    string FilePath,
    TestLabHeader Header,
    IReadOnlyList<TestLabChannel> Channels);

/// <summary>
/// Заголовок файла регистрации.
/// </summary>
internal sealed record TestLabHeader(
    string Signature,
    string ObjectName,
    string TestType,
    string Place,
    DateOnly Date,
    TimeOnly Time,
    ushort ChannelCount);

/// <summary>
/// Описание канала.
/// </summary>
internal sealed record TestLabChannelDescription(
    string Name,
    string Description,
    string Unit,
    float Offset,
    float Scale,
    float Cutoff,
    ushort SampleRate,
    byte ChannelType,
    TestLabDataFormat Format,
    uint Length);

/// <summary>
/// Канал с исходными и фактическими значениями.
/// </summary>
internal sealed record TestLabChannel(
    TestLabChannelDescription Description,
    double[] RawValues,
    double[] ActualValues,
    ushort EndMarker);

/// <summary>
/// Поддерживаемые форматы данных в файле TestLab.
/// </summary>
internal enum TestLabDataFormat : byte
{
    UInt8 = 1,
    UInt16 = 2,
    UInt32 = 3,
    Float32 = 4,
    Float64 = 8,
    Int8 = 17,
    Int16 = 18,
    Int32 = 19,
    Float32Alternate = 20,
    Float64Alternate = 24,
}

/// <summary>
/// Читатель файлов формата TestLab.
/// </summary>
internal static class TestLabReader
{
    private const ushort EndOfArrayMarker = ushort.MaxValue;
    private const string Signature = "TESTLAB";
    private static readonly Encoding StringEncoding = CreateStringEncoding();

    public static TestLabDocument Read(string filePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        try
        {
            using var stream = File.OpenRead(filePath);
            using var reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: false);

            var header = ReadHeader(reader);
            var descriptions = ReadChannelDescriptions(reader, header.ChannelCount);
            var channels = ReadChannels(reader, descriptions);

            return new TestLabDocument(filePath, header, channels);
        }
        catch (TestLabFormatException)
        {
            throw;
        }
        catch (EndOfStreamException ex)
        {
            throw new TestLabFormatException("Файл завершился раньше, чем ожидалось по спецификации формата.", ex);
        }
        catch (IOException ex)
        {
            throw new TestLabFormatException("Не удалось прочитать файл регистрации.", ex);
        }
    }

    private static Encoding CreateStringEncoding()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        return Encoding.GetEncoding(1251);
    }

    private static TestLabHeader ReadHeader(BinaryReader reader)
    {
        var signature = ReadNullTerminatedString(reader, 8, "сигнатура файла");
        var objectName = ReadNullTerminatedString(reader, 61, "объект испытаний");
        var testType = ReadNullTerminatedString(reader, 121, "тип испытаний");
        var place = ReadNullTerminatedString(reader, 121, "место проведения испытаний");
        var dateText = ReadNullTerminatedString(reader, 11, "дата");
        var timeText = ReadNullTerminatedString(reader, 9, "время");
        var channelCount = reader.ReadUInt16();
        EnsureReservedBytes(reader, 17, "заголовка файла");

        if (!string.Equals(signature, Signature, StringComparison.Ordinal))
        {
            throw new TestLabFormatException($"Ожидалась сигнатура TESTLAB, но получено значение '{signature}'.");
        }

        var date = ParseDate(dateText);
        var time = ParseTime(timeText);

        return new TestLabHeader(
            Signature: signature,
            ObjectName: objectName,
            TestType: testType,
            Place: place,
            Date: date,
            Time: time,
            ChannelCount: channelCount);
    }

    private static IReadOnlyList<TestLabChannelDescription> ReadChannelDescriptions(BinaryReader reader, ushort count)
    {
        var descriptions = new List<TestLabChannelDescription>(count);

        for (var index = 0; index < count; index++)
        {
            var name = ReadNullTerminatedString(reader, 13, $"название канала {index + 1}");
            var description = ReadNullTerminatedString(reader, 121, $"описание канала {index + 1}");
            var unit = ReadNullTerminatedString(reader, 13, $"единица измерения канала {index + 1}");
            var offset = reader.ReadSingle();
            var scale = reader.ReadSingle();
            var cutoff = reader.ReadSingle();
            var sampleRate = reader.ReadUInt16();
            var channelType = reader.ReadByte();
            var formatCode = reader.ReadByte();
            var length = reader.ReadUInt32();
            EnsureReservedBytes(reader, 25, $"описания канала {index + 1}");

            if (channelType != 0)
            {
                throw new TestLabFormatException(
                    $"Канал с индексом {index} содержит неподдерживаемый тип {channelType}. Ожидалось значение 0.");
            }

            descriptions.Add(new TestLabChannelDescription(
                Name: name,
                Description: description,
                Unit: unit,
                Offset: offset,
                Scale: scale,
                Cutoff: cutoff,
                SampleRate: sampleRate,
                ChannelType: channelType,
                Format: ParseDataFormat(formatCode, index),
                Length: length));
        }

        return descriptions;
    }

    private static IReadOnlyList<TestLabChannel> ReadChannels(
        BinaryReader reader,
        IReadOnlyList<TestLabChannelDescription> descriptions)
    {
        var channels = new List<TestLabChannel>(descriptions.Count);

        for (var channelIndex = 0; channelIndex < descriptions.Count; channelIndex++)
        {
            var description = descriptions[channelIndex];
            var rawValues = ReadRawValues(reader, description, channelIndex);
            var actualValues = CalculateActualValues(rawValues, description.Offset, description.Scale);
            var endMarker = reader.ReadUInt16();

            if (endMarker != EndOfArrayMarker)
            {
                throw new TestLabFormatException(
                    $"Канал с индексом {channelIndex} завершён неверным признаком конца массива {endMarker}. Ожидалось значение 65535.");
            }

            channels.Add(new TestLabChannel(
                Description: description,
                RawValues: rawValues,
                ActualValues: actualValues,
                EndMarker: endMarker));
        }

        return channels;
    }

    private static double[] ReadRawValues(
        BinaryReader reader,
        TestLabChannelDescription description,
        int channelIndex)
    {
        if (description.Length > int.MaxValue)
        {
            throw new TestLabFormatException(
                $"Канал с индексом {channelIndex} содержит слишком большой массив данных длиной {description.Length.ToString(CultureInfo.InvariantCulture)}.");
        }

        var count = checked((int)description.Length);
        var values = new double[count];

        for (var index = 0; index < count; index++)
        {
            values[index] = description.Format switch
            {
                TestLabDataFormat.UInt8 => reader.ReadByte(),
                TestLabDataFormat.UInt16 => reader.ReadUInt16(),
                TestLabDataFormat.UInt32 => reader.ReadUInt32(),
                TestLabDataFormat.Int8 => reader.ReadSByte(),
                TestLabDataFormat.Int16 => reader.ReadInt16(),
                TestLabDataFormat.Int32 => reader.ReadInt32(),
                TestLabDataFormat.Float32 or TestLabDataFormat.Float32Alternate => reader.ReadSingle(),
                TestLabDataFormat.Float64 or TestLabDataFormat.Float64Alternate => reader.ReadDouble(),
                _ => throw new TestLabFormatException($"Формат данных {description.Format} не поддерживается."),
            };
        }

        return values;
    }

    private static double[] CalculateActualValues(IReadOnlyList<double> rawValues, float offset, float scale)
    {
        var actualValues = new double[rawValues.Count];

        for (var index = 0; index < rawValues.Count; index++)
        {
            actualValues[index] = scale * (rawValues[index] - offset);
        }

        return actualValues;
    }

    private static TestLabDataFormat ParseDataFormat(byte formatCode, int channelIndex)
    {
        return formatCode switch
        {
            1 => TestLabDataFormat.UInt8,
            2 => TestLabDataFormat.UInt16,
            3 => TestLabDataFormat.UInt32,
            4 => TestLabDataFormat.Float32,
            8 => TestLabDataFormat.Float64,
            17 => TestLabDataFormat.Int8,
            18 => TestLabDataFormat.Int16,
            19 => TestLabDataFormat.Int32,
            20 => TestLabDataFormat.Float32Alternate,
            24 => TestLabDataFormat.Float64Alternate,
            _ => throw new TestLabFormatException(
                $"Канал с индексом {channelIndex} содержит неподдерживаемый код формата данных {formatCode}."),
        };
    }

    private static string ReadNullTerminatedString(BinaryReader reader, int length, string fieldName)
    {
        var bytes = reader.ReadBytes(length);
        if (bytes.Length != length)
        {
            throw new TestLabFormatException(
                $"Не удалось полностью прочитать поле '{fieldName}' длиной {length.ToString(CultureInfo.InvariantCulture)} байт.");
        }

        var zeroIndex = Array.IndexOf(bytes, (byte)0);
        if (zeroIndex < 0)
        {
            throw new TestLabFormatException(
                $"Поле '{fieldName}' должно завершаться нулевым символом.");
        }

        ValidateStringBytes(bytes, zeroIndex, fieldName);
        var effectiveLength = zeroIndex;
        return StringEncoding.GetString(bytes, 0, effectiveLength).Trim();
    }

    private static void ValidateStringBytes(byte[] bytes, int zeroIndex, string fieldName)
    {
        for (var index = 0; index < zeroIndex; index++)
        {
            var value = bytes[index];
            if (value is <= 31 or 127 or 152 or 160 or 173)
            {
                throw new TestLabFormatException(
                    $"Поле '{fieldName}' содержит недопустимый код символа {value}.");
            }
        }

        for (var index = zeroIndex + 1; index < bytes.Length; index++)
        {
            if (bytes[index] != 0)
            {
                throw new TestLabFormatException(
                    $"Все байты после завершающего нулевого символа в поле '{fieldName}' должны быть равны нулю.");
            }
        }
    }

    private static void EnsureReservedBytes(BinaryReader reader, int length, string sectionName)
    {
        var bytes = reader.ReadBytes(length);
        if (bytes.Length != length)
        {
            throw new TestLabFormatException(
                $"Не удалось полностью прочитать зарезервированную область секции '{sectionName}'.");
        }

        if (bytes.Any(static value => value != 0))
        {
            throw new TestLabFormatException(
                $"Зарезервированная область секции '{sectionName}' должна быть заполнена нулями.");
        }
    }

    private static DateOnly ParseDate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return DateOnly.MinValue;
        }

        if (!DateOnly.TryParseExact(
                value,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var result))
        {
            throw new TestLabFormatException($"Дата '{value}' не соответствует формату dd.MM.yyyy.");
        }

        return result == DateOnly.MinValue ? DateOnly.MinValue : result;
    }

    private static TimeOnly ParseTime(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return TimeOnly.MinValue;
        }

        if (!TimeOnly.TryParseExact(
                value,
                "HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var result))
        {
            throw new TestLabFormatException($"Время '{value}' не соответствует формату HH:mm:ss.");
        }

        return result == TimeOnly.MinValue ? TimeOnly.MinValue : result;
    }
}
