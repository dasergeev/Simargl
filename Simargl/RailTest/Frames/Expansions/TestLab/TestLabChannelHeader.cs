using System;

namespace RailTest.Frames;

/// <summary>
/// Представляет заголовок канала в формате <see cref="StorageFormat.TestLab"/>.
/// </summary>
public class TestLabChannelHeader : ChannelHeader
{
    /// <summary>
    /// Поле для хранения описания канала.
    /// </summary>
    private string _Description;

    /// <summary>
    /// Поле для хранения смещения значений канала.
    /// </summary>
    private double _Offset;

    /// <summary>
    /// Поле для хранения масштаба значений канала.
    /// </summary>
    private double _Scale;

    /// <summary>
    /// Поле для хранения типа канала.
    /// </summary>
    private TestLabChannelType _Type;

    /// <summary>
    /// Поле для хранения формата данных канала.
    /// </summary>
    private TestLabDataFormat _DataFormat;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public TestLabChannelHeader() : base(StorageFormat.TestLab)
    {
        _Description = "";
        _Offset = 0;
        _Scale = 1;
        _Type = TestLabChannelType.Normal;
        _DataFormat = TestLabDataFormat.Float64;
    }

    /// <summary>
    /// Возвращает или задаёт описание канала.
    /// </summary>
    public string Description
    {
        get
        {
            return _Description;
        }
        set
        {
            _Description = value ?? "";
        }
    }

    /// <summary>
    /// Возвращает или задаёт смещение значений канала.
    /// </summary>
    public double Offset
    {
        get
        {
            return _Offset;
        }
        set
        {
            _Offset = value;
        }
    }

    /// <summary>
    /// Возвращает или задаёт масштаб значений канала.
    /// </summary>
    public double Scale
    {
        get
        {
            return _Scale;
        }
        set
        {
            _Scale = value;
        }
    }

    /// <summary>
    /// Возвращает или задаёт тип канала.
    /// </summary>
    public TestLabChannelType Type
    {
        get
        {
            return _Type;
        }
        set
        {
            _Type = value;
        }
    }

    /// <summary>
    /// Возвращает или задаёт формат данных канала.
    /// </summary>
    public TestLabDataFormat DataFormat
    {
        get
        {
            return _DataFormat;
        }
        set
        {
            _DataFormat = value;
        }
    }

    /// <summary>
    /// Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>
    /// Новый объект, являющийся копией этого экземпляра.
    /// </returns>
    public override ChannelHeader Clone()
    {
        TestLabChannelHeader duplicate = new()
        {
            Name = Name,
            Sampling = Sampling,
            Unit = Unit,
            Cutoff = Cutoff,

            _Description = _Description,
            _Offset = _Offset,
            _Scale = _Scale,
            _Type = _Type,
            _DataFormat = _DataFormat
        };
        return duplicate;
    }

    /// <summary>
    /// Возвращает заголовок канала в другом формате.
    /// </summary>
    /// <param name="format">
    /// Формат заголовка канала, в который необходимо преобразовать текущий объект.
    /// </param>
    /// <returns>
    /// Преобразованный объект.
    /// </returns>
    internal override ChannelHeader Convert(StorageFormat format)
    {
        switch (format)
        {
            case StorageFormat.TestLab:
                return Clone();
            case StorageFormat.Catman:
                {
                    CatmanChannelHeader header = new()
                    {
                        Sampling = Sampling,
                        Cutoff = Cutoff
                    };
                    return header;
                }
            default:
                throw new Exception();
        }
    }
    
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