namespace Simargl.Frames.TestLab;

/// <summary>
/// Представляет заголовок канала в формате <see cref="StorageFormat.TestLab"/>.
/// </summary>
/// <param name="name">
/// Имя канала.
/// </param>
/// <param name="unit">
/// Единица измерения.
/// </param>
/// <param name="cutoff">
/// Частота среза фильтра.
/// </param>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="name"/> передана пустая ссылка.
/// </exception>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="unit"/> передана пустая ссылка.
/// </exception>
/// <exception cref="ArgumentOutOfRangeException">
/// В параметре <paramref name="cutoff"/> передано нечисловое значение.
/// </exception>
/// <exception cref="ArgumentOutOfRangeException">
/// В параметре <paramref name="cutoff"/> передано бесконечное значение.
/// </exception>
public class TestLabChannelHeader(string name, string unit, double cutoff) :
    ChannelHeader(StorageFormat.TestLab, name, unit, cutoff)
{
    /// <summary>
    /// Поле для хранения описания канала.
    /// </summary>
    private string _Description = string.Empty;

    /// <summary>
    /// Поле для хранения смещения значений канала.
    /// </summary>
    private double _Offset = 0;

    /// <summary>
    /// Поле для хранения масштаба значений канала.
    /// </summary>
    private double _Scale = 1;

    /// <summary>
    /// Поле для хранения типа канала.
    /// </summary>
    private TestLabChannelType _Type = TestLabChannelType.Normal;

    /// <summary>
    /// Поле для хранения формата данных канала.
    /// </summary>
    private TestLabDataFormat _DataFormat = TestLabDataFormat.Float64;

    /// <summary>
    /// Возвращает или задаёт описание канала.
    /// </summary>
    public string Description
    {
        get => _Description;
        set => _Description = IsNotNull(value, nameof(Description));
    }

    /// <summary>
    /// Возвращает или задаёт смещение значений канала.
    /// </summary>
    public double Offset
    {
        get => _Offset;
        set => _Offset = value;
    }

    /// <summary>
    /// Возвращает или задаёт масштаб значений канала.
    /// </summary>
    public double Scale
    {
        get => _Scale;
        set => _Scale = value;
    }

    /// <summary>
    /// Возвращает или задаёт тип канала.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое не содержится в перечислении <see cref="TestLabChannelType"/>.
    /// </exception>
    public TestLabChannelType Type
    {
        get => _Type;
        set => _Type = IsDefined(value, nameof(Type));
    }

    /// <summary>
    /// Возвращает или задаёт формат данных канала.
    /// </summary>
    public TestLabDataFormat DataFormat
    {
        get => _DataFormat;
        set => _DataFormat = IsDefined(value, nameof(DataFormat));
    }

    /// <summary>
    /// Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>
    /// Новый объект, являющийся копией этого экземпляра.
    /// </returns>
    public override ChannelHeader Clone() => new TestLabChannelHeader(Name, Unit, Cutoff)
    {
        _Description = _Description,
        _Offset = _Offset,
        _Scale = _Scale,
        _Type = _Type,
        _DataFormat = _DataFormat
    };

    /// <summary>
    /// Выполняет проверку и нормализацию значения типа <see cref="TestLabChannelType"/>.
    /// </summary>
    /// <param name="value">
    /// Исходное значение.
    /// </param>
    /// <returns>
    /// Нормализованное значение.
    /// </returns>
    internal static TestLabChannelType Validation(TestLabChannelType value)
    {
        return value switch
        {
            TestLabChannelType.Normal => TestLabChannelType.Normal,
            TestLabChannelType.Service => TestLabChannelType.Service,
            _ => throw new InvalidOperationException("Тип канала не поддерживается."),
        };
    }

    /// <summary>
    /// Выполняет проверку и нормализацию значения типа <see cref="TestLabDataFormat"/>.
    /// </summary>
    /// <param name="value">
    /// Исходное значение.
    /// </param>
    /// <returns>
    /// Нормализованное значение.
    /// </returns>
    internal static TestLabDataFormat Validation(TestLabDataFormat value)
    {
        return value switch
        {
            TestLabDataFormat.UInt8 => TestLabDataFormat.UInt8,
            TestLabDataFormat.UInt16 => TestLabDataFormat.UInt16,
            TestLabDataFormat.UInt32 => TestLabDataFormat.UInt32,
            TestLabDataFormat.Int8 => TestLabDataFormat.Int8,
            TestLabDataFormat.Int16 => TestLabDataFormat.Int16,
            TestLabDataFormat.Int32 => TestLabDataFormat.Int32,
            TestLabDataFormat.Float32 => TestLabDataFormat.Float32,
            (TestLabDataFormat)((int)TestLabDataFormat.Float32 | 0x10) => TestLabDataFormat.Float32,
            TestLabDataFormat.Float64 => TestLabDataFormat.Float64,
            (TestLabDataFormat)((int)TestLabDataFormat.Float64 | 0x10) => TestLabDataFormat.Float64,
            _ => throw new InvalidOperationException("Формат данных не поддерживается."),
        };
    }

    /// <summary>
    /// Возвращает размер элемента массива данных канала в байтах.
    /// </summary>
    /// <param name="format">
    /// Формат данных.
    /// </param>
    /// <returns>
    /// Размер элемента массива данных канала в байтах.
    /// </returns>
    internal static int GetItemSize(TestLabDataFormat format)
    {
        return format switch
        {
            TestLabDataFormat.UInt8 => 1,
            TestLabDataFormat.UInt16 => 2,
            TestLabDataFormat.UInt32 => 4,
            TestLabDataFormat.Int8 => 1,
            TestLabDataFormat.Int16 => 2,
            TestLabDataFormat.Int32 => 4,
            TestLabDataFormat.Float32 => 4,
            TestLabDataFormat.Float64 => 8,
            _ => throw new InvalidOperationException("Формат данных не поддерживается."),
        };
    }
}
