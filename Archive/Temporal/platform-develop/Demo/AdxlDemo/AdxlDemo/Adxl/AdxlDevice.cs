using Apeiron.Platform.Demo.AdxlDemo.Net;
using Apeiron.Platform.Demo.AdxlDemo.Nodes;
using System.ComponentModel;

namespace Apeiron.Platform.Demo.AdxlDemo.Adxl;

/// <summary>
/// Представляет датчик ADXL357.
/// </summary>
public sealed class AdxlDevice :
    Node<INode>
{
    /// <summary>
    /// Поле для хранения серийный номер.
    /// </summary>
    private readonly uint _SerialNumber;

    /// <summary>
    /// Поле для хранения соединения с датчиком.
    /// </summary>
    private AdxlConnect _Connect;

    /// <summary>
    /// Поле для хранения текущих свойств датчика.
    /// </summary>
    private AdxlDeviceProperties _DeviceProperties = new();

    /// <summary>
    /// Поле для хранения типа датчика.
    /// </summary>
    private string _SensorType = string.Empty;

    /// <summary>
    /// Поле для хранения версии прошивки.
    /// </summary>
    private string _FirmwareVersion = string.Empty;

    /// <summary>
    /// Поле для хранения даты изготовления прошивки.
    /// </summary>
    private string _FirmwareDate = string.Empty;

    /// <summary>
    /// Поле для хранения значения, определяющего использование DHCP.
    /// </summary>
    private bool _UseDhcp = default;

    /// <summary>
    /// Поле для хранения UDP порта для подключения к датчику.
    /// </summary>
    private ushort _UdpPort = default;

    /// <summary>
    /// Поле для хранения TCP порта для подключения к серверу.
    /// </summary>
    private ushort _TcpPort = default;

    /// <summary>
    /// Поле для хранения диапазона измерения датчика.
    /// </summary>
    private ushort _MeasuringRange = default;

    /// <summary>
    /// Поле для хранения значения HighPass фильтра.
    /// </summary>
    private ushort _HighPassFilter = default;

    /// <summary>
    /// Поле для хранения частоты дискретизации датчика.
    /// </summary>
    private ushort _Sampling = default;

    /// <summary>
    /// Поле для хранения смещения сигнала по оси Ox.
    /// </summary>
    private float _XOffset = default;

    /// <summary>
    /// Поле для хранения смещения сигнала по оси Oy.
    /// </summary>
    private float _YOffset = default;

    /// <summary>
    /// Поле для хранения смещения сигнала по оси Oz.
    /// </summary>
    private float _ZOffset = default;

    /// <summary>
    /// Поле для хранения IP адреса датчика.
    /// </summary>
    private IPv4Address _Address = default;

    /// <summary>
    /// Поле для хранения маски подсети.
    /// </summary>
    private IPv4Address _Mask = default;

    /// <summary>
    /// Поле для хранения шлюза.
    /// </summary>
    private IPv4Address _Gateway = default;

    /// <summary>
    /// Поле для хранения адреса сервера.
    /// </summary>
    private IPv4Address _Server = default;

    /// <summary>
    /// Поле для хранения максимального значения температуры.
    /// </summary>
    private float _MaxTemperature = default;

    /// <summary>
    /// Поле для хранения минимального значения температуры.
    /// </summary>
    private float _MinTemperature = default;

    /// <summary>
    /// Поле для хранения максимального значения напряжения питания.
    /// </summary>
    private float _MaxVoltage = default;

    /// <summary>
    /// Поле для хранения минимального значения напряжения питания.
    /// </summary>
    private float _MinVoltage = default;

    /// <summary>
    /// Поле для хранения значения самодиагностики акселерометра.
    /// </summary>
    private float _DiagnosticValue = default;

    /// <summary>
    /// Поле для хранения информации о сохранённых ошибках.
    /// </summary>
    private uint _ErrorCodes = default;

    /// <summary>
    /// Поле для хранения значения, определяющего включены ли измерения.
    /// </summary>
    private bool _Enable = default;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <param name="serialNumber">
    /// Серийный номер датчика.
    /// </param>
    /// <param name="connect">
    /// Соединения с датчиком.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="serialNumber"/> передано значение,
    /// которое меньше значения <see cref="uint.MinValue"/> или больше значения <see cref="uint.MaxValue"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="connect"/> передана пустая ссылка.
    /// </exception>
    public AdxlDevice(Engine engine, long serialNumber, AdxlConnect connect) :
        base(engine, $"{serialNumber:X8}", NodeFormat.AdxlDevice)
    {
        //  Установка серийного номера.
        _SerialNumber = (uint)IsInRange(serialNumber, uint.MinValue, uint.MaxValue, nameof(serialNumber));

        //  Установка соединения с датчиком.
        _Connect = IsNotNull(connect, nameof(connect));

        //  Создание коллекции параметров датчика.
        Parameters = new(this);
    }

    /// <summary>
    /// Возвращает коллекцию параметров датчика
    /// </summary>
    [Browsable(false)]
    public AdxlDeviceParameterCollection Parameters { get; }

    /// <summary>
    /// Возвращает серийный номер датчика.
    /// </summary>
    [Browsable(false)]
    public long SerialNumber => _SerialNumber;

    /// <summary>
    /// Возвращает серийный номер датчика.
    /// </summary>
    [Category("Информация")]
    [DisplayName("Номер")]
    [Description("Серийный номер датчика.")]
    public string SerialNumberName => $"{_SerialNumber:X8}";

    /// <summary>
    /// Возвращает или задаёт соединения с датчиком.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Произошла попытка выполнить недопустимую операцию.
    /// </exception>
    [Browsable(false)]
    public AdxlConnect Connect
    {
        get => _Connect;
        set
        {
            //  Проверка параметра.
            IsNotNull(value, nameof(Connect));

            //  Проверка серийного номера.
            if (SerialNumber != value.ReadSerialNumber())
            {
                //  Произошла попытка выполнить недопустимую операцию.
                throw Exceptions.OperationInvalid();
            }

            //  Выполнение в основном потоке.
            Invoker.Primary(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_Connect != value)
                {
                    //  Установка нового значения.
                    _Connect = value;

                    //  Вызов события об изменении значения.
                    OnPropertyChanged(new(nameof(Connect)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает тип датчика.
    /// </summary>
    [Category("Информация")]
    [DisplayName("Тип")]
    [Description("Тип датчика.")]
    public string SensorType => _SensorType;

    /// <summary>
    /// Возвращает версию прошивки.
    /// </summary>
    [Category("Информация")]
    [DisplayName("Прошивка")]
    [Description("Версия прошивки.")]
    public string FirmwareVersion => _FirmwareVersion;

    /// <summary>
    /// Возвращает дату изготовления прошивки.
    /// </summary>
    [Category("Информация")]
    [DisplayName("Изготовлен")]
    [Description("Дата изготовления прошивки.")]
    public string FirmwareDate => _FirmwareDate;

    /// <summary>
    /// Возвращает значение, определяющее использование DHCP.
    /// </summary>
    [Category("Информация")]
    [DisplayName("DHCP")]
    [Description("Использование DHCP.")]
    public bool UseDhcp => _UseDhcp;

    /// <summary>
    /// Возвращает UDP порт для подключения к датчику.
    /// </summary>
    [Category("Подключение")]
    [DisplayName("UDP порт")]
    [Description("UDP порт для подключения к датчику.")]
    public int UdpPort => _UdpPort;

    /// <summary>
    /// Возвращает TCP порт для подключения к серверу.
    /// </summary>
    [Category("Подключение")]
    [DisplayName("TCP порт")]
    [Description("TCP порт для подключения к серверу.")]
    public int TcpPort => _TcpPort;

    /// <summary>
    /// Возвращает диапазон измерения датчика.
    /// </summary>
    [Category("Настройки")]
    [DisplayName("Диапазон")]
    [Description("Диапазон измерения датчика.")]
    public double MeasuringRange => _MeasuringRange;

    /// <summary>
    /// Возвращает частоту HighPass фильтра.
    /// </summary>
    [Category("Настройки")]
    [DisplayName("Фильтр")]
    [Description("Частота фильтра низких частот.")]
    public double HighPassFilter => _HighPassFilter switch
    {
        1 => 0.00247 * Sampling,
        2 => 0.00062084 * Sampling,
        3 => 0.00015545 * Sampling,
        4 => 0.00003862 * Sampling,
        5 => 0.00000954 * Sampling,
        6 => 0.00000238 * Sampling,
        _ => 0,
    };

    /// <summary>
    /// Возвращает частоту дискретизации.
    /// </summary>
    [Category("Настройки")]
    [DisplayName("Частота")]
    [Description("Частота дискретизации.")]
    public double Sampling => _Sampling;

    /// <summary>
    /// Возвращает смещение сигнала по оси Ox.
    /// </summary>
    [Category("Настройки")]
    [DisplayName("Смещение Ox")]
    [Description("Смещение сигнала по оси Ox.")]
    public double XOffset => _XOffset;

    /// <summary>
    /// Возвращает смещение сигнала по оси Oy.
    /// </summary>
    [Category("Настройки")]
    [DisplayName("Смещение Oy")]
    [Description("Смещение сигнала по оси Oy.")]
    public double YOffset => _YOffset;

    /// <summary>
    /// Возвращает смещение сигнала по оси Oz.
    /// </summary>
    [Category("Настройки")]
    [DisplayName("Смещение Oz")]
    [Description("Смещение сигнала по оси Oz.")]
    public double ZOffset => _ZOffset;

    /// <summary>
    /// Возвращает IP адрес датчика.
    /// </summary>
    [Category("Подключение")]
    [DisplayName("Адрес")]
    [Description("IP адрес датчика.")]
    public IPv4Address Address => _Address;

    /// <summary>
    /// Возвращает маску подсети.
    /// </summary>
    [Category("Подключение")]
    [DisplayName("Маска")]
    [Description("IP маска подсети.")]
    public IPv4Address Mask => _Mask;

    /// <summary>
    /// Возвращает шлюз.
    /// </summary>
    [Category("Подключение")]
    [DisplayName("Шлюз")]
    [Description("IP адрес шлюза.")]
    public IPv4Address Gateway => _Gateway;

    /// <summary>
    /// Возвращает адреса сервера.
    /// </summary>
    [Category("Подключение")]
    [DisplayName("Сервер")]
    [Description("IP адрес сервера.")]
    public IPv4Address Server => _Server;

    /// <summary>
    /// Возвращает максимальное значение температуры.
    /// </summary>
    [Category("Состояние")]
    [DisplayName("Макс температура")]
    [Description("Максимальное значение температуры.")]
    public double MaxTemperature => _MaxTemperature;

    /// <summary>
    /// Возвращает минимальное значение температуры.
    /// </summary>
    [Category("Состояние")]
    [DisplayName("Мин температура")]
    [Description("Минимальное значение температуры.")]
    public double MinTemperature => _MinTemperature;

    /// <summary>
    /// Возвращает максимальное значение напряжения питания.
    /// </summary>
    [Category("Состояние")]
    [DisplayName("Макс напряжение")]
    [Description("Максимальное значение напряжения питания.")]
    public double MaxVoltage => _MaxVoltage / 1000;

    /// <summary>
    /// Возвращает минимальное значение напряжения питания.
    /// </summary>
    [Category("Состояние")]
    [DisplayName("Мин напряжение")]
    [Description("Минимальное значение напряжения питания.")]
    public double MinVoltage => _MinVoltage / 1000;

    /// <summary>
    /// Возвращает значение самодиагностики акселерометра.
    /// </summary>
    [Category("Состояние")]
    [DisplayName("Диагностика")]
    [Description("Значение самодиагностики акселерометра.")]
    public double DiagnosticValue => _DiagnosticValue;

    /// <summary>
    /// Возвращает информацию о сохранённых ошибках.
    /// </summary>
    [Category("Состояние")]
    [DisplayName("Ошибки")]
    [Description("Информация о сохранённых ошибках.")]
    public AdxlErrorCodes ErrorCodes => (AdxlErrorCodes)_ErrorCodes;

    /// <summary>
    /// Возвращает значение, определяющее включены ли измерения.
    /// </summary>
    [Category("Состояние")]
    [DisplayName("Измерения")]
    [Description("Значение, определяющее включены ли измерения.")]
    public bool Enable => _Enable;

    /// <summary>
    /// Асинхронно перезагружает датчик.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая перезагрузку датчика.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task RebootAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Безопасное выполнение действия.
        Invoker.Critical(delegate
        {
            //  Запись значения, сообщающего датчику о необходимости перезагрузки.
            Connect.WriteState(0xA0A0);
        });
    }

    /// <summary>
    /// Асинхронно выполняет обновление информации о датчике.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая обновление информации о датчике.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Чтение типа датчика.
        string sensorType = Connect.ReadSensorType();

        //  Чтение версии прошивки.
        string firmwareVersion = Connect.ReadFirmwareVersion();

        //  Чтение даты изготовления прошивки.
        string firmwareDate = Connect.ReadFirmwareDate();

        //  Чтение значения, определяющего использование DHCP.
        bool useDhcp = Connect.ReadUseDhcp();

        //  Чтение UDP порта для подключения к датчику.
        ushort udpPort = Connect.ReadUdpPort();

        //  Чтение TCP порта для подключения к серверу.
        ushort tcpPort = Connect.ReadTcpPort();

        //  Чтение диапазона измерения датчика.
        ushort measuringRange = Connect.ReadMeasuringRange();

        //  Чтение значения HighPass фильтра.
        ushort highPassFilter = Connect.ReadHighPassFilter();

        //  Чтение частоты дискретизации датчика.
        ushort sampling = Connect.ReadSampling();

        //  Чтение смещения сигнала по оси Ox.
        float xOffset = Connect.ReadXOffset();

        //  Чтение смещения сигнала по оси Oy.
        float yOffset = Connect.ReadYOffset();

        //  Чтение смещения сигнала по оси Oz.
        float zOffset = Connect.ReadZOffset();

        //  Чтение IP адреса датчика.
        IPv4Address address = Connect.ReadAddress();

        //  Чтение маски подсети.
        IPv4Address mask = Connect.ReadMask();

        //  Чтение шлюза.
        IPv4Address gateway = Connect.ReadGateway();

        //  Чтение адреса сервера.
        IPv4Address server = Connect.ReadServer();

        //  Чтение максимального значения температуры.
        float maxTemperature = Connect.ReadMaxTemperature();

        //  Чтение минимального значения температуры.
        float minTemperature = Connect.ReadMinTemperature();

        //  Чтение максимального значения напряжения питания.
        float maxVoltage = Connect.ReadMaxVoltage();

        //  Чтение минимального значения напряжения питания.
        float minVoltage = Connect.ReadMinVoltage();

        //  Чтение значения самодиагностики акселерометра.
        float diagnosticValue = Connect.ReadDiagnosticValue();

        //  Чтение информации о сохранённых ошибках.
        uint errorCodes = Connect.ReadErrorCodes();

        //  Чтение состояния датчика.
        bool enable = Connect.ReadState() != 0;

        //  Выполнение в основном потоке.
        await PrimaryInvokeAsync(delegate
        {
            //  Сохранение старых значений.
            string oldSensorType = SensorType;
            string oldFirmwareVersion = FirmwareVersion;
            string oldFirmwareDate = FirmwareDate;
            bool oldUseDhcp = UseDhcp;
            int oldUdpPort = UdpPort;
            int oldTcpPort = TcpPort;
            double oldMeasuringRange = MeasuringRange;
            double oldHighPassFilter = HighPassFilter;
            double oldSampling = Sampling;
            double oldXOffset = XOffset;
            double oldYOffset = YOffset;
            double oldZOffset = ZOffset;
            IPv4Address oldAddress = Address;
            IPv4Address oldMask = Mask;
            IPv4Address oldGateway = Gateway;
            IPv4Address oldServer = Server;
            double oldMaxTemperature = MaxTemperature;
            double oldMinTemperature = MinTemperature;
            double oldMaxVoltage = MaxVoltage;
            double oldMinVoltage = MinVoltage;
            double oldDiagnosticValue = DiagnosticValue;
            AdxlErrorCodes oldErrorCodes = ErrorCodes;
            bool oldEnable = Enable;

            //  Установка новых значений.
            _SensorType = sensorType;
            _FirmwareVersion = firmwareVersion;
            _FirmwareDate = firmwareDate;
            _UseDhcp = useDhcp;
            _UdpPort = udpPort;
            _TcpPort = tcpPort;
            _MeasuringRange = measuringRange;
            _HighPassFilter = highPassFilter;
            _Sampling = sampling;
            _XOffset = xOffset;
            _YOffset = yOffset;
            _ZOffset = zOffset;
            _Address = address;
            _Mask = mask;
            _Gateway = gateway;
            _Server = server;
            _MaxTemperature = maxTemperature;
            _MinTemperature = minTemperature;
            _MaxVoltage = maxVoltage;
            _MinVoltage = minVoltage;
            _DiagnosticValue = diagnosticValue;
            _ErrorCodes = errorCodes;
            _Enable = enable;

            //  Создание свойств.
            AdxlDeviceProperties deviceProperties = new(
                _SensorType, _FirmwareVersion, _FirmwareDate, _SerialNumber,
                _UseDhcp, _UdpPort, _TcpPort, _MeasuringRange, _HighPassFilter,
                _Sampling, _XOffset, _YOffset, _ZOffset,
                _Address, _Mask, _Gateway, _Server,
                _MaxTemperature, _MinTemperature, _MaxVoltage, _MinVoltage,
                _DiagnosticValue, _ErrorCodes, _Enable);

            //  Установка свойств.
            Interlocked.Exchange(ref _DeviceProperties, deviceProperties);

            //  Вызов событий.
            if (oldSensorType != SensorType) OnPropertyChanged(new(nameof(SensorType)));
            if (oldFirmwareVersion != FirmwareVersion) OnPropertyChanged(new(nameof(FirmwareVersion)));
            if (oldFirmwareDate != FirmwareDate) OnPropertyChanged(new(nameof(FirmwareDate)));
            if (oldUseDhcp != UseDhcp) OnPropertyChanged(new(nameof(UseDhcp)));
            if (oldUdpPort != UdpPort) OnPropertyChanged(new(nameof(UdpPort)));
            if (oldTcpPort != TcpPort) OnPropertyChanged(new(nameof(TcpPort)));
            if (oldMeasuringRange != MeasuringRange) OnPropertyChanged(new(nameof(MeasuringRange)));
            if (oldHighPassFilter != HighPassFilter) OnPropertyChanged(new(nameof(HighPassFilter)));
            if (oldSampling != Sampling) OnPropertyChanged(new(nameof(Sampling)));
            if (oldXOffset != XOffset) OnPropertyChanged(new(nameof(XOffset)));
            if (oldYOffset != YOffset) OnPropertyChanged(new(nameof(YOffset)));
            if (oldZOffset != ZOffset) OnPropertyChanged(new(nameof(ZOffset)));
            if (oldAddress != Address) OnPropertyChanged(new(nameof(Address)));
            if (oldMask != Mask) OnPropertyChanged(new(nameof(Mask)));
            if (oldGateway != Gateway) OnPropertyChanged(new(nameof(Gateway)));
            if (oldServer != Server) OnPropertyChanged(new(nameof(Server)));
            if (oldMaxTemperature != MaxTemperature) OnPropertyChanged(new(nameof(MaxTemperature)));
            if (oldMinTemperature != MinTemperature) OnPropertyChanged(new(nameof(MinTemperature)));
            if (oldMaxVoltage != MaxVoltage) OnPropertyChanged(new(nameof(MaxVoltage)));
            if (oldMinVoltage != MinVoltage) OnPropertyChanged(new(nameof(MinVoltage)));
            if (oldDiagnosticValue != DiagnosticValue) OnPropertyChanged(new(nameof(DiagnosticValue)));
            if (oldErrorCodes != ErrorCodes) OnPropertyChanged(new(nameof(ErrorCodes)));
            if (oldEnable != Enable) OnPropertyChanged(new(nameof(Enable)));
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно возвращает текущие настройки датчика.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая текущие настройки датчика.
    /// </returns>
    public async Task<AdxlDeviceProperties> GetPropertiesAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Асинхронное выполнение.
        return await Task.Run(delegate
        {
            //  Создание свойств.
            AdxlDeviceProperties deviceProperties = new();

            //  Установка свойств.
            Interlocked.Exchange(ref deviceProperties, _DeviceProperties);

            //  Возврат полученных свойств.
            return deviceProperties;
        }, cancellationToken).ConfigureAwait(false);
    }
}
