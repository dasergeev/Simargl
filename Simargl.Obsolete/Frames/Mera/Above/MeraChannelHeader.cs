namespace Simargl.Frames.Mera;

/// <summary>
/// Представляет заголовок канала в формате <see cref="StorageFormat.Mera"/>.
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
public sealed class MeraChannelHeader(string name, string unit, double cutoff) :
    ChannelHeader(StorageFormat.Mera, name, unit, cutoff)
{
    /// <summary>
    /// Поле для хранения описания канала.
    /// </summary>
    private string _Description = string.Empty;

    /// <summary>
    /// Поле для хранения смещения значений канала.
    /// </summary>
    private double _Offset = 1;

    /// <summary>
    /// Поле для хранения масштаба значений канала.
    /// </summary>
    private double _Scale = 0;

    /// <summary>
    /// Поле для хранения значения, определяющего формат данных канала.
    /// </summary>
    private MeraDataFormat _DataFormat = MeraDataFormat.Int16;

    /// <summary>
    /// Поле для хранения имени измерительного модуля.
    /// </summary>
    private string _ModuleName = string.Empty;

    /// <summary>
    /// Поле для хранения серийного номера измерительного модуля.
    /// </summary>
    private string _ModuleSerialNumber = string.Empty;

    /// <summary>
    /// Поле для хранения адреса источника данных.
    /// </summary>
    private string _Address = string.Empty;

    /// <summary>
    /// Возвращает коллекцию информации о порциях записи при прерывистой записи.
    /// </summary>
    public MeraPortionInfoCollection PortionInfos { get; } = [];

    /// <summary>
    /// Возвращает или задаёт описание канала.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Description
    {
        get => _Description;
        set => _Description = IsNotNull(value, nameof(Description));
    }

    /// <summary>
    /// Возвращает или задаёт смещение значений канала.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    public double Offset
    {
        get => _Offset;
        set
        {
            //  Проверка числового значения.
            IsNotNaN(value, nameof(Offset));

            //  Проверка конечного значения.
            IsNotInfinity(value, nameof(Offset));

            //  Установка значения.
            _Offset = value;
        }
    }

    /// <summary>
    /// Возвращает или задаёт масштаб значений канала.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    public double Scale
    {
        get => _Scale;
        set
        {
            //  Проверка числового значения.
            IsNotNaN(value, nameof(Scale));

            //  Проверка конечного значения.
            IsNotInfinity(value, nameof(Scale));

            //  Установка значения.
            _Scale = value;
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее формат данных канала.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое не содержится в перечислении <see cref="MeraDataFormat"/>.
    /// </exception>
    public MeraDataFormat DataFormat
    {
        get => _DataFormat;
        set => _DataFormat = IsDefined(value, nameof(DataFormat));
    }

    /// <summary>
    /// Возвращает или задаёт смещение начала записи.
    /// </summary>
    public TimeSpan Start { get; set; }

    /// <summary>
    /// Возвращает или задаёт имя измерительного модуля.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string ModuleName
    {
        get => _ModuleName;
        set => _ModuleName = IsNotNull(value, nameof(ModuleName));
    }

    /// <summary>
    /// Возвращает или задаёт серийный номер измерительного модуля.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string ModuleSerialNumber
    {
        get => _ModuleSerialNumber;
        set => _ModuleSerialNumber = IsNotNull(value, nameof(ModuleSerialNumber));
    }

    /// <summary>
    /// Возвращает или задаёт адрес источника данных.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    public string Address
    {
        get => _Address;
        set => _Address = IsNotNull(value, nameof(Address));
    }

    /// <summary>
    /// Создаёт копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    public override ChannelHeader Clone()
    {
        //  Создание и возврат копии кадра.
        return new MeraChannelHeader(Name, Unit, Cutoff)
        {
            _Description = _Description,
            _Offset = _Offset,
            _Scale = _Scale,
            _DataFormat = _DataFormat,
            _ModuleName = _ModuleName,
            _ModuleSerialNumber = _ModuleSerialNumber,
            _Address = _Address,
        };
    }
}
