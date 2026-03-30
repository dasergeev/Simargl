namespace Simargl.Frames.Catman;

/// <summary>
/// Представляет заголовок канала в формате <see cref="StorageFormat.Catman"/>.
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
public class CatmanChannelHeader(string name, string unit, double cutoff) :
    ChannelHeader(StorageFormat.Catman, name, unit, cutoff)
{
    /// <summary>
    /// Поле для хранения положения канала в базе данных Catman.
    /// </summary>
    private int _LocationInDatabase = 0;

    /// <summary>
    /// Поле для хранения коментария к каналу.
    /// </summary>
    private string _Comment = string.Empty;

    /// <summary>
    /// Поле для хранения формата данных канала.
    /// </summary>
    private CatmanDataFormat _DataFormat = CatmanDataFormat.DoubleNumeric;

    /// <summary>
    /// Поле для хранения ширины данных в байтах.
    /// </summary>
    private int _DataWidth = 8;

    /// <summary>
    /// Поле для хранения даты и времени измерения.
    /// </summary>
    private DateTime _Time = DateTime.Now;

    /// <summary>
    /// Поле для хранения размера дополнительного заголовка канала в байтах.
    /// </summary>
    private int _SizeOfExtendedHeader = 148;

    /// <summary>
    /// Поле для хранения времени начала измерений.
    /// </summary>
    private DateTime _StartTime = DateTime.Now;

    /// <summary>
    /// Поле для хранения кода типа датчика.
    /// </summary>
    private CatmanCodeOfSensorType _CodeOfSensorType = CatmanCodeOfSensorType.StrainGageFullBridge120Ohms;

    /// <summary>
    /// Поле для хранения кода напряжения питания.
    /// </summary>
    private CatmanCodeOfSupplyVoltage _CodeOfSupplyVoltage = CatmanCodeOfSupplyVoltage.Voltage2_5;

    /// <summary>
    /// Поле для хранения кода характеристик фильтра.
    /// </summary>
    private CatmanCodeOfFilterCharacteristic _CodeOfFilterCharacteristics = CatmanCodeOfFilterCharacteristic.NoFilter;

    /// <summary>
    /// Поле для хранения кода частоты фильтра.
    /// </summary>
    private CatmanCodeOfFilterFrequency _CodeOfFilterFrequency = CatmanCodeOfFilterFrequency.FrequencyNone;

    /// <summary>
    /// Поле для хранения значения тары.
    /// </summary>
    private double _TareValue = 0;

    /// <summary>
    /// Поле для хранения нулевого значения.
    /// </summary>
    private double _ZeroValue = 0;

    /// <summary>
    /// Поле для хранения кода измерительного диапазона.
    /// </summary>
    private CatmanCodeOfMeasuringRange _CodeOfMeasuringRange = CatmanCodeOfMeasuringRange.Range1000mV;

    /// <summary>
    /// Поле для хранения входных характеристик.
    /// </summary>
    private double[] _InputCharacteristics = [0, 0, 1, 1];

    /// <summary>
    /// Поле для хранения серийного номера усилителя.
    /// </summary>
    private string _AmplifierSerialNumber = string.Empty;

    /// <summary>
    /// Поле для хранения физической единицы измерения.
    /// </summary>
    private string _PhysicalUnit = string.Empty;

    /// <summary>
    /// Поле для хранения исходной единицы измерения.
    /// </summary>
    private string _NativeUnit = string.Empty;

    /// <summary>
    /// Поле для хранения номера слота оборудования.
    /// </summary>
    private int _HardwareSlotNumber = 0;

    /// <summary>
    /// Поле для хранения номера подканала или 0, если одноканальный слот.
    /// </summary>
    private int _HardwareSubSlotNumber = 0;

    /// <summary>
    /// Поле для хранения кода усилителя.
    /// </summary>
    private int _CodeOfAmplifierType = 0;

    /// <summary>
    /// Поле для хранения кода типа соединителя AP.
    /// </summary>
    private int _CodeOfAPConnectorType = 0;

    /// <summary>
    /// Поле для хранения коэффициента заполнения, используемого в измерениях тензодатчиков.
    /// </summary>
    private double _GageFactor = 0;

    /// <summary>
    /// Поле для хранения мостового коэффициента, используемого в измерениях тензодатчиков.
    /// </summary>
    private double _BridgeFactor = 0;

    /// <summary>
    /// Поле для хранения кода измерительного сигнала.
    /// </summary>
    private int _CodeOfMeasurementSignal = 0;

    /// <summary>
    /// Поле для хранения кода входного усилителя.
    /// </summary>
    private int _CodeOfAmplifierInput = 0;

    /// <summary>
    /// Поле для хранения кода фильтра высоких частот.
    /// </summary>
    private int _CodeOfHighpassFilter = 0;

    /// <summary>
    /// Поле для хранения специальной информации, используемой в заголовках файлов онлайн-экспорта.
    /// </summary>
    private int _OnlineImportInfo = 0;

    /// <summary>
    /// Поле для хранения кода типа шкалы.
    /// </summary>
    private int _CodeOfScaleType = 0;

    /// <summary>
    /// Поле для хранения программного нуля (тары) для каналов, имеющих пользовательскую шкалу.
    /// </summary>
    private double _SoftwareZeroValue = 0;

    /// <summary>
    /// Поле для хранения защиты от записи. Если не ноль, то доступ на запись запрещен.
    /// </summary>
    private int _WriteProtected = 0;

    /// <summary>
    /// Поле для хранения данных для выравнивания структуры данных.
    /// </summary>
    private byte[] _Alignment = [0, 0, 0];

    /// <summary>
    /// Поле для хранения номинального диапазона.
    /// </summary>
    private double _NominalRange = 0;

    /// <summary>
    /// Поле для хранения коэффициента компенсации длины кабеля.
    /// </summary>
    private double _CableLengthCompensation = 0;

    /// <summary>
    /// Поле для хранения формата экспортируемых данных.
    /// </summary>
    private int _ExportFormat = 0;

    /// <summary>
    /// Поле для хранения типа канала.
    /// </summary>
    private int _ChannelType = 0;

    /// <summary>
    /// Поле для хранения соединителя на слое системы сбора данных.
    /// </summary>
    private int _EDaqConnectorOnLayer = 0;

    /// <summary>
    /// Поле для хранения слоя системы сбора данных.
    /// </summary>
    private int _EDaqLayer = 0;

    /// <summary>
    /// Поле для хранения типа содержимого.
    /// </summary>
    private int _ContentType = 0;

    /// <summary>
    /// Поле для хранения зарезервированных данных.
    /// </summary>
    private byte[] _Reserved = [0, 0, 0];

    /// <summary>
    /// Поле для хранения режима линеаризации.
    /// </summary>
    private int _LinearisationMode = 0;

    /// <summary>
    /// Поле для хранения типа пользовательского масштаба.
    /// </summary>
    private int _UserScaleType = 0;

    /// <summary>
    /// Поле для хранения количества точек для таблицы пользовательского масштаба.
    /// </summary>
    private int _NumberOfPointsScaleTable = 4;

    /// <summary>
    /// Поле для хранения серии значений таблицы пользовательского масштаба.
    /// Значения зависят от типа масштабирования.
    /// </summary>
    private double[] _PointsScaleTable = [0, 0, 1, 1];

    /// <summary>
    /// Поле для хранения термо-типа.
    /// </summary>
    private int _ThermoType = 0;

    /// <summary>
    /// Поле для хранения формулы.
    /// </summary>
    private string _Formula = string.Empty;

    /// <summary>
    /// Поле для хранения информации о сенсоре.
    /// </summary>
    private CatmanSensorInfo _SensorInfo = new();

    /// <summary>
    /// Поле для хранения формата строки.
    /// </summary>
    private string _FormatString = "0.0000";

    /// <summary>
    /// Возвращает или задаёт положение канала в базе данных Catman.
    /// </summary>
    public int LocationInDatabase
    {
        get => _LocationInDatabase;
        set => _LocationInDatabase = value;
    }

    /// <summary>
    /// Возвращает или задаёт коментарий к каналу.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Comment
    {
        get => _Comment;
        set => _Comment = IsNotNull(value, nameof(Comment));
    }

    /// <summary>
    /// Возвращает или задаёт формат данных канала.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое не содержится в перечислении <see cref="CatmanDataFormat"/>.
    /// </exception>
    internal CatmanDataFormat DataFormat
    {
        get => _DataFormat;
        set => _DataFormat = IsDefined(value, nameof(DataFormat));
    }

    /// <summary>
    /// Возвращает или задаёт ширину данных в байтах.
    /// </summary>
    public int DataWidth
    {
        get => _DataWidth;
        set => _DataWidth = value;
    }

    /// <summary>
    /// Возвращает или задаёт дату и время измерения.
    /// </summary>
    public DateTime Time
    {
        get => _Time;
        set => _Time = value;
    }

    /// <summary>
    /// Возвращает или задаёт размер дополнительного заголовка канала в байтах.
    /// </summary>
    public int SizeOfExtendedHeader
    {
        get => _SizeOfExtendedHeader;
        set => _SizeOfExtendedHeader = value;
    }

    /// <summary>
    /// Возвращает или задаёт время начала измерений.
    /// </summary>
    public DateTime StartTime
    {
        get => _StartTime;
        set => _StartTime = value;
    }

    /// <summary>
    /// Возвращает или задаёт код типа датчика.
    /// </summary>
    public CatmanCodeOfSensorType CodeOfSensorType
    {
        get => _CodeOfSensorType;
        set => _CodeOfSensorType = value;
    }

    /// <summary>
    /// Возвращает или задаёт код напряжения питания.
    /// </summary>
    public CatmanCodeOfSupplyVoltage CodeOfSupplyVoltage
    {
        get => _CodeOfSupplyVoltage;
        set => _CodeOfSupplyVoltage = value;
    }

    /// <summary>
    /// Возвращает или задаёт код характеристик фильтра.
    /// </summary>
    public CatmanCodeOfFilterCharacteristic CodeOfFilterCharacteristics
    {
        get => _CodeOfFilterCharacteristics;
        set => _CodeOfFilterCharacteristics = value;
    }

    /// <summary>
    /// Возвращает или задаёт код частоты фильтра.
    /// </summary>
    internal CatmanCodeOfFilterFrequency CodeOfFilterFrequency
    {
        get => _CodeOfFilterFrequency;
        set => _CodeOfFilterFrequency = value;
    }

    /// <summary>
    /// Возвращает или задаёт значение тары.
    /// </summary>
    public double TareValue
    {
        get => _TareValue;
        set => _TareValue = value;
    }

    /// <summary>
    /// Возвращает или задаёт нулевое значение.
    /// </summary>
    public double ZeroValue
    {
        get => _ZeroValue;
        set => _ZeroValue = value;
    }

    /// <summary>
    /// Возвращает или задаёт код измерительного диапазона.
    /// </summary>
    public CatmanCodeOfMeasuringRange CodeOfMeasuringRange
    {
        get => _CodeOfMeasuringRange;
        set => _CodeOfMeasuringRange = value;
    }

    /// <summary>
    /// Возвращает или задаёт входные характеристики.
    /// </summary>
    internal double[] InputCharacteristics
    {
        get => _InputCharacteristics;
        set => _InputCharacteristics = value;
    }

    /// <summary>
    /// Возвращает или задаёт серийный номер усилителя.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string AmplifierSerialNumber
    {
        get => _AmplifierSerialNumber;
        set => _AmplifierSerialNumber = IsNotNull(value, nameof(AmplifierSerialNumber));
    }

    /// <summary>
    /// Возвращает или задаёт физическую единицу измерения.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string PhysicalUnit
    {
        get => _PhysicalUnit;
        set => _PhysicalUnit = IsNotNull(value, nameof(PhysicalUnit));
    }

    /// <summary>
    /// Возвращает или задаёт исходную единицу измерения.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string NativeUnit
    {
        get => _NativeUnit;
        set => _NativeUnit = IsNotNull(value, nameof(NativeUnit));
    }

    /// <summary>
    /// Возвращает или задаёт номер слота оборудования.
    /// </summary>
    public int HardwareSlotNumber
    {
        get => _HardwareSlotNumber;
        set => _HardwareSlotNumber = value;
    }

    /// <summary>
    /// Возвращает или задаёт номер подканала.
    /// </summary>
    public int HardwareSubSlotNumber
    {
        get => _HardwareSubSlotNumber;
        set => _HardwareSubSlotNumber = value;
    }

    /// <summary>
    /// Возвращает или задаёт код усилителя.
    /// </summary>
    public int CodeOfAmplifierType
    {
        get => _CodeOfAmplifierType;
        set => _CodeOfAmplifierType = value;
    }

    /// <summary>
    /// Возвращает или задаёт код типа соединителя AP.
    /// </summary>
    public int CodeOfAPConnectorType
    {
        get => _CodeOfAPConnectorType;
        set => _CodeOfAPConnectorType = value;
    }

    /// <summary>
    /// Возвращает или задаёт коэффициент заполнения, используемый в измерениях тензодатчиков.
    /// </summary>
    public double GageFactor
    {
        get => _GageFactor;
        set => _GageFactor = value;
    }

    /// <summary>
    /// Возвращает или задаёт мостовой коэффициент, используемый в измерениях тензодатчиков.
    /// </summary>
    public double BridgeFactor
    {
        get => _BridgeFactor;
        set => _BridgeFactor = value;
    }

    /// <summary>
    /// Возвращает или задаёт код измерительного сигнала.
    /// </summary>
    public int CodeOfMeasurementSignal
    {
        get => _CodeOfMeasurementSignal;
        set => _CodeOfMeasurementSignal = value;
    }

    /// <summary>
    /// Возвращает или задаёт код входного усилителя.
    /// </summary>
    public int CodeOfAmplifierInput
    {
        get => _CodeOfAmplifierInput;
        set => _CodeOfAmplifierInput = value;
    }

    /// <summary>
    /// Возвращает или задаёт код фильтра высоких частот.
    /// </summary>
    public int CodeOfHighpassFilter
    {
        get => _CodeOfHighpassFilter;
        set => _CodeOfHighpassFilter = value;
    }

    /// <summary>
    /// Возвращает или задаёт специальную информацию, используемую в заголовках файлов онлайн-экспорта.
    /// </summary>
    public int OnlineImportInfo
    {
        get => _OnlineImportInfo;
        set => _OnlineImportInfo = value;
    }

    /// <summary>
    /// Возвращает или задаёт код типа шкалы.
    /// </summary>
    public int CodeOfScaleType
    {
        get => _CodeOfScaleType;
        set => _CodeOfScaleType = value;
    }

    /// <summary>
    /// Возвращает или задаёт программный нуль (тару) для каналов, имеющих пользовательскую шкалу.
    /// </summary>
    public double SoftwareZeroValue
    {
        get => _SoftwareZeroValue;
        set => _SoftwareZeroValue = value;
    }

    /// <summary>
    /// Возвращает или задаёт защиту от записи. Если не ноль, то доступ на запись запрещен.
    /// </summary>
    public int WriteProtected
    {
        get => _WriteProtected;
        set => _WriteProtected = value;
    }

    /// <summary>
    /// Возвращает или задаёт данные для выравнивания структуры данных.
    /// </summary>
    internal byte[] Alignment
    {
        get => _Alignment;
        set => _Alignment = value;
    }

    /// <summary>
    /// Возвращает или задаёт номинальный диапазон.
    /// </summary>
    public double NominalRange
    {
        get => _NominalRange;
        set => _NominalRange = value;
    }

    /// <summary>
    /// Возвращает или задаёт коэффициент компенсации длины кабеля.
    /// </summary>
    public double CableLengthCompensation
    {
        get => _CableLengthCompensation;
        set => _CableLengthCompensation = value;
    }

    /// <summary>
    /// Возвращает или задаёт формат экспортируемых данных.
    /// </summary>
    public int ExportFormat
    {
        get => _ExportFormat;
        set => _ExportFormat = value;
    }

    /// <summary>
    /// Возвращает или задаёт тип канала.
    /// </summary>
    public int ChannelType
    {
        get => _ChannelType;
        set => _ChannelType = value;
    }

    /// <summary>
    /// Возвращает или задаёт соединитель на слое системы сбора данных.
    /// </summary>
    public int EDaqConnectorOnLayer
    {
        get => _EDaqConnectorOnLayer;
        set => _EDaqConnectorOnLayer = value;
    }

    /// <summary>
    /// Возвращает или задаёт слой системы сбора данных.
    /// </summary>
    public int EDaqLayer
    {
        get => _EDaqLayer;
        set => _EDaqLayer = value;
    }

    /// <summary>
    /// Возвращает или задаёт тип содержимого.
    /// </summary>
    public int ContentType
    {
        get => _ContentType;
        set => _ContentType = value;
    }

    /// <summary>
    /// Возвращает или задаёт зарезервированные данные.
    /// </summary>
    internal byte[] Reserved
    {
        get => _Reserved;
        set => _Reserved = value;
    }

    /// <summary>
    /// Возвращает или задаёт режим линеаризации.
    /// </summary>
    public int LinearisationMode
    {
        get => _LinearisationMode;
        set => _LinearisationMode = value;
    }

    /// <summary>
    /// Возвращает или задаёт тип пользовательского масштаба.
    /// </summary>
    public int UserScaleType
    {
        get => _UserScaleType;
        set => _UserScaleType = value;
    }

    /// <summary>
    /// Возвращает или задаёт количество точек для таблицы пользовательского масштаба.
    /// </summary>
    public int NumberOfPointsScaleTable
    {
        get => _NumberOfPointsScaleTable;
        set => _NumberOfPointsScaleTable = value;
    }

    /// <summary>
    /// Возвращает или задаёт серию значений таблицы пользовательского масштаба.
    /// Значения зависят от типа масштабирования.
    /// </summary>
    internal double[] PointsScaleTable
    {
        get => _PointsScaleTable;
        set => _PointsScaleTable = value;
    }

    /// <summary>
    /// Возвращает или задаёт термо-тип.
    /// </summary>
    public int ThermoType
    {
        get => _ThermoType;
        set => _ThermoType = value;
    }

    /// <summary>
    /// Возвращает или задаёт формулу.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Formula
    {
        get => _Formula;
        set => _Formula = IsNotNull(value, nameof(Formula));
    }

    /// <summary>
    /// Возвращает или задаёт информацию о сенсоре.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public CatmanSensorInfo SensorInfo
    {
        get => _SensorInfo;
        set => _SensorInfo = IsNotNull(value, nameof(SensorInfo));
    }

    /// <summary>
    /// Возвращает формат строки.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string FormatString
    {
        get => _FormatString;
        set => _FormatString = IsNotNull(value, nameof(FormatString));
    }

    /// <summary>
    /// 
    /// </summary>
    public double MinValueFactor { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double MaxValueFactor { get; set; }

    /// <summary>
    /// Создаёт копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    public override ChannelHeader Clone()
    {
        CatmanChannelHeader header = new(Name, Unit, Cutoff)
        {
            LocationInDatabase = LocationInDatabase,
            Comment = Comment,
            DataFormat = DataFormat,
            DataWidth = DataWidth,
            Time = Time,
            SizeOfExtendedHeader = SizeOfExtendedHeader,
            StartTime = StartTime,
            CodeOfSensorType = CodeOfSensorType,
            CodeOfSupplyVoltage = CodeOfSupplyVoltage,
            CodeOfFilterCharacteristics = CodeOfFilterCharacteristics,
            CodeOfFilterFrequency = CodeOfFilterFrequency,
            TareValue = TareValue,
            ZeroValue = ZeroValue,
            CodeOfMeasuringRange = CodeOfMeasuringRange,
            AmplifierSerialNumber = AmplifierSerialNumber,
            PhysicalUnit = PhysicalUnit,
            NativeUnit = NativeUnit,
            HardwareSlotNumber = HardwareSlotNumber,
            HardwareSubSlotNumber = HardwareSubSlotNumber,
            CodeOfAmplifierType = CodeOfAmplifierType,
            CodeOfAPConnectorType = CodeOfAPConnectorType,
            GageFactor = GageFactor,
            BridgeFactor = BridgeFactor,
            CodeOfMeasurementSignal = CodeOfMeasurementSignal,
            CodeOfAmplifierInput = CodeOfAmplifierInput,
            CodeOfHighpassFilter = CodeOfHighpassFilter,
            OnlineImportInfo = OnlineImportInfo,
            CodeOfScaleType = CodeOfScaleType,
            SoftwareZeroValue = SoftwareZeroValue,
            WriteProtected = WriteProtected,
            NominalRange = NominalRange,
            CableLengthCompensation = CableLengthCompensation,
            ExportFormat = ExportFormat,
            ChannelType = ChannelType,
            EDaqConnectorOnLayer = EDaqConnectorOnLayer,
            EDaqLayer = EDaqLayer,
            ContentType = ContentType,
            LinearisationMode = LinearisationMode,
            UserScaleType = UserScaleType,
            NumberOfPointsScaleTable = NumberOfPointsScaleTable,
            ThermoType = ThermoType,
            Formula = Formula,
            FormatString = FormatString,
            MinValueFactor = MinValueFactor,
            MaxValueFactor = MaxValueFactor,
            SensorInfo = new CatmanSensorInfo()
            {
                InUse = SensorInfo.InUse,
                Description = SensorInfo.Description,
                Tid = SensorInfo.Tid,
            },

        };

        if (InputCharacteristics != null)
        {
            header.InputCharacteristics = new double[InputCharacteristics.Length];
            for (int i = 0; i != InputCharacteristics.Length; ++i)
            {
                header.InputCharacteristics[i] = InputCharacteristics[i];
            }
        }

        if (Alignment != null)
        {
            header.Alignment = new byte[Alignment.Length];
            for (int i = 0; i != Alignment.Length; ++i)
            {
                header.Alignment[i] = Alignment[i];
            }
        }

        if (Reserved != null)
        {
            header.Reserved = new byte[Reserved.Length];
            for (int i = 0; i != Reserved.Length; ++i)
            {
                header.Reserved[i] = Reserved[i];
            }
        }

        if (PointsScaleTable != null)
        {
            header.PointsScaleTable = new double[PointsScaleTable.Length];
            for (int i = 0; i != PointsScaleTable.Length; ++i)
            {
                header.PointsScaleTable[i] = PointsScaleTable[i];
            }
        }

        return header;
    }
}
