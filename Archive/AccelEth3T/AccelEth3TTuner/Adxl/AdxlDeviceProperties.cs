using Simargl.Net;
using System;

namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет свойства датчика ADXL357.
/// </summary>
public sealed class AdxlDeviceProperties
{
    /// <summary>
    /// Поле для хранения типа датчика.
    /// </summary>
    private readonly string _SensorType = string.Empty;

    /// <summary>
    /// Поле для хранения версии прошивки.
    /// </summary>
    private readonly string _FirmwareVersion = string.Empty;

    /// <summary>
    /// Поле для хранения даты изготовления прошивки.
    /// </summary>
    private readonly string _FirmwareDate = string.Empty;

    /// <summary>
    /// Поле для хранения серийного номер.
    /// </summary>
    private readonly uint _SerialNumber = default;

    /// <summary>
    /// Поле для хранения значения, определяющего использование DHCP.
    /// </summary>
    private readonly bool _UseDhcp = default;

    /// <summary>
    /// Поле для хранения UDP порта для подключения к датчику.
    /// </summary>
    private readonly ushort _UdpPort = default;

    /// <summary>
    /// Поле для хранения TCP порта для подключения к серверу.
    /// </summary>
    private readonly ushort _TcpPort = default;

    /// <summary>
    /// Поле для хранения диапазона измерения датчика.
    /// </summary>
    private readonly ushort _MeasuringRange = default;

    /// <summary>
    /// Поле для хранения значения HighPass фильтра.
    /// </summary>
    private readonly ushort _HighPassFilter = default;

    /// <summary>
    /// Поле для хранения частоты дискретизации датчика.
    /// </summary>
    private readonly ushort _Sampling = default;

    /// <summary>
    /// Поле для хранения смещения сигнала по оси Ox.
    /// </summary>
    private readonly float _XOffset = default;

    /// <summary>
    /// Поле для хранения смещения сигнала по оси Oy.
    /// </summary>
    private readonly float _YOffset = default;

    /// <summary>
    /// Поле для хранения смещения сигнала по оси Oz.
    /// </summary>
    private readonly float _ZOffset = default;

    /// <summary>
    /// Поле для хранения IP адреса датчика.
    /// </summary>
    private readonly IPv4Address _Address = default;

    /// <summary>
    /// Поле для хранения маски подсети.
    /// </summary>
    private readonly IPv4Address _Mask = default;

    /// <summary>
    /// Поле для хранения шлюза.
    /// </summary>
    private readonly IPv4Address _Gateway = default;

    /// <summary>
    /// Поле для хранения адреса сервера.
    /// </summary>
    private readonly IPv4Address _Server = default;

    /// <summary>
    /// Поле для хранения максимального значения температуры.
    /// </summary>
    private readonly float _MaxTemperature = default;

    /// <summary>
    /// Поле для хранения минимального значения температуры.
    /// </summary>
    private readonly float _MinTemperature = default;

    /// <summary>
    /// Поле для хранения максимального значения напряжения питания.
    /// </summary>
    private readonly float _MaxVoltage = default;

    /// <summary>
    /// Поле для хранения минимального значения напряжения питания.
    /// </summary>
    private readonly float _MinVoltage = default;

    /// <summary>
    /// Поле для хранения значения самодиагностики акселерометра.
    /// </summary>
    private readonly float _DiagnosticValue = default;

    /// <summary>
    /// Поле для хранения информации о сохранённых ошибках.
    /// </summary>
    private readonly uint _ErrorCodes = default;

    /// <summary>
    /// Поле для хранения значения, определяющего включены ли измерения.
    /// </summary>
    private readonly bool _Enable = default;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public AdxlDeviceProperties()
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="sensorType">
    /// Поле для хранения типа датчика.
    /// </param>
    /// <param name="firmwareVersion">
    /// Поле для хранения версии прошивки.
    /// </param>
    /// <param name="firmwareDate">
    /// Поле для хранения даты изготовления прошивки.
    /// </param>
    /// <param name="serialNumber">
    /// Поле для хранения серийный номер.
    /// </param>
    /// <param name="useDhcp">
    /// Поле для хранения значения, определяющего использование DHCP.
    /// </param>
    /// <param name="udpPort">
    /// Поле для хранения UDP порта для подключения к датчику.
    /// </param>
    /// <param name="tcpPort">
    /// Поле для хранения TCP порта для подключения к серверу.
    /// </param>
    /// <param name="measuringRange">
    /// Поле для хранения диапазона измерения датчика.
    /// </param>
    /// <param name="highPassFilter">
    /// Поле для хранения значения HighPass фильтра.
    /// </param>
    /// <param name="sampling">
    /// Поле для хранения частоты дискретизации датчика.
    /// </param>
    /// <param name="xOffset">
    /// Поле для хранения смещения сигнала по оси Ox.
    /// </param>
    /// <param name="yOffset">
    /// Поле для хранения смещения сигнала по оси Oy.
    /// </param>
    /// <param name="zOffset">
    /// Поле для хранения смещения сигнала по оси Oz.
    /// </param>
    /// <param name="address">
    /// Поле для хранения IP адреса датчика.
    /// </param>
    /// <param name="mask">
    /// Поле для хранения маски подсети.
    /// </param>
    /// <param name="gateway">
    /// Поле для хранения шлюза.
    /// </param>
    /// <param name="server">
    /// Поле для хранения адреса сервера.
    /// </param>
    /// <param name="maxTemperature">
    /// Поле для хранения максимального значения температуры.
    /// </param>
    /// <param name="minTemperature">
    /// Поле для хранения минимального значения температуры.
    /// </param>
    /// <param name="maxVoltage">
    /// Поле для хранения максимального значения напряжения питания.
    /// </param>
    /// <param name="minVoltage">
    /// Поле для хранения минимального значения напряжения питания.
    /// </param>
    /// <param name="diagnosticValue">
    /// Поле для хранения значения самодиагностики акселерометра.
    /// </param>
    /// <param name="errorCodes">
    /// Поле для хранения информации о сохранённых ошибках.
    /// </param>
    /// <param name="enable">
    /// Поле для хранения значения, определяющего включены ли измерения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sensorType"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="firmwareVersion"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="firmwareDate"/> передана пустая ссылка.
    /// </exception>
    internal AdxlDeviceProperties(string sensorType, string firmwareVersion, string firmwareDate,
        uint serialNumber, bool useDhcp, ushort udpPort, ushort tcpPort, ushort measuringRange,
        ushort highPassFilter, ushort sampling, float xOffset, float yOffset, float zOffset,
        IPv4Address address, IPv4Address mask, IPv4Address gateway, IPv4Address server,
        float maxTemperature, float minTemperature, float maxVoltage, float minVoltage,
        float diagnosticValue, uint errorCodes, bool enable)
    {
        //  Установка значений полей.
        _SensorType = IsNotNull(sensorType, nameof(sensorType));
        _FirmwareVersion = IsNotNull(firmwareVersion, nameof(firmwareVersion));
        _FirmwareDate = IsNotNull(firmwareDate, nameof(firmwareDate));
        _SerialNumber = serialNumber;
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
    }

    /// <summary>
    /// Возвращает тип датчика.
    /// </summary>
    public string SensorType => _SensorType;

    /// <summary>
    /// Возвращает версию прошивки.
    /// </summary>
    public string FirmwareVersion => _FirmwareVersion;

    /// <summary>
    /// Возвращает дату изготовления прошивки.
    /// </summary>
    public string FirmwareDate => _FirmwareDate;

    /// <summary>
    /// Возвращает серийный номер датчика.
    /// </summary>
    public long SerialNumber => _SerialNumber;

    /// <summary>
    /// Возвращает значение, определяющее использование DHCP.
    /// </summary>
    public bool UseDhcp => _UseDhcp;

    /// <summary>
    /// Возвращает UDP порт для подключения к датчику.
    /// </summary>
    public int UdpPort => _UdpPort;

    /// <summary>
    /// Возвращает TCP порт для подключения к серверу.
    /// </summary>
    public int TcpPort => _TcpPort;

    /// <summary>
    /// Возвращает диапазон измерения датчика.
    /// </summary>
    public double MeasuringRange => _MeasuringRange;

    /// <summary>
    /// Возвращает частоту HighPass фильтра.
    /// </summary>
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
    public double Sampling => _Sampling;

    /// <summary>
    /// Возвращает смещение сигнала по оси Ox.
    /// </summary>
    public double XOffset => _XOffset;

    /// <summary>
    /// Возвращает смещение сигнала по оси Oy.
    /// </summary>
    public double YOffset => _YOffset;

    /// <summary>
    /// Возвращает смещение сигнала по оси Oz.
    /// </summary>
    public double ZOffset => _ZOffset;

    /// <summary>
    /// Возвращает IP адрес датчика.
    /// </summary>
    public IPv4Address Address => _Address;

    /// <summary>
    /// Возвращает маску подсети.
    /// </summary>
    public IPv4Address Mask => _Mask;

    /// <summary>
    /// Возвращает шлюз.
    /// </summary>
    public IPv4Address Gateway => _Gateway;

    /// <summary>
    /// Возвращает адреса сервера.
    /// </summary>
    public IPv4Address Server => _Server;

    /// <summary>
    /// Возвращает максимальное значение температуры.
    /// </summary>
    public double MaxTemperature => _MaxTemperature;

    /// <summary>
    /// Возвращает минимальное значение температуры.
    /// </summary>
    public double MinTemperature => _MinTemperature;

    /// <summary>
    /// Возвращает максимальное значение напряжения питания.
    /// </summary>
    public double MaxVoltage => _MaxVoltage;

    /// <summary>
    /// Возвращает минимальное значение напряжения питания.
    /// </summary>
    public double MinVoltage => _MinVoltage;

    /// <summary>
    /// Возвращает значение самодиагностики акселерометра.
    /// </summary>
    public double DiagnosticValue => _DiagnosticValue;

    /// <summary>
    /// Возвращает информацию о сохранённых ошибках.
    /// </summary>
    public AdxlErrorCodes ErrorCodes => (AdxlErrorCodes)_ErrorCodes;

    /// <summary>
    /// Возвращает значение, определяющее включены ли измерения.
    /// </summary>
    public bool Enable => _Enable;
}
