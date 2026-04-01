using Simargl.Designing;
using Simargl.Embedded.Modbus;
using Simargl.Net;
using System.Collections.Concurrent;
using System.Net;
using System.Text;

namespace Simargl.Embedded.AccelEth3T;

/// <summary>
/// Представляет соединение с акселерометром AccelEth-3T
/// </summary>
public sealed class AccelEth3TConnect :
    Anything
{
    /// <summary>
    /// Постоянная, определяющая идентификатор устройства.
    /// </summary>
    private const byte _SlaveIdentifier = 1;

    /// <summary>
    /// Поле для хранения соединение по Modbus TCP.
    /// </summary>
    private readonly ModbusConnect _ModbusConnect;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="connect">
    /// Соединение по Modbus TCP.
    /// </param>
    private AccelEth3TConnect([NoVerify] ModbusConnect connect)
    {
        //  Установка соединения по Modbus TCP.
        _ModbusConnect = connect;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="address">
    /// Адрес датчика.
    /// </param>
    public AccelEth3TConnect(IPAddress address) :
        this(new ModbusConnect(address, _SlaveIdentifier))
    {

    }

    /// <summary>
    /// Возвращает адрес датчика.
    /// </summary>
    public IPAddress Address => _ModbusConnect.Address;

    /// <summary>
    /// Возвращает или задаёт таймаут ожидания.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передан недопустимый таймаут.
    /// </exception>
    public TimeSpan Timeout
    {
        get => _ModbusConnect.Timeout;
        set => _ModbusConnect.Timeout = value;
    }

    /// <summary>
    /// Асинхронно выполняет сканирование сети.
    /// </summary>
    /// <param name="first">
    /// Первый IP-адрес.
    /// </param>
    /// <param name="last">
    /// Последний IP-адрес.
    /// </param>
    /// <param name="timeout">
    /// Время ожидания ответа.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая сканирование сети.
    /// </returns>
    public static async Task<AccelEth3TConnect[]> ScanAsync(
        IPAddress first, IPAddress last,
        TimeSpan timeout, CancellationToken cancellationToken)
    {
        //  Поиск соединений по Modbus TCP.
        ModbusConnect[] modbusConnects = await ModbusConnect.ScanAsync(
            first, last, _SlaveIdentifier, timeout,
            cancellationToken).ConfigureAwait(false);

        //  Коллекция для хранения результата.
        ConcurrentBag<AccelEth3TConnect> connects = [];

        //  Перебор соединений.
        await Parallel.ForEachAsync(
            modbusConnects,
            cancellationToken,
            async (modbusConnect, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                //  Блок перехвата всех исключений.
                try
                {
                    //  Создание соединения.
                    AccelEth3TConnect connect = new(modbusConnect);

                    //  Чтение типа датчика.
                    await connect.ReadSensorTypeAsync(cancellationToken).ConfigureAwait(false);

                    //  Добавление соединения к результату.
                    connects.Add(connect);
                }
                catch { }
            });

        //  Возврат массива соединений
        return [.. connects];
    }

    /// <summary>
    /// Выполняет чтение типа датчика.
    /// </summary>
    /// <returns>
    /// Тип датчика.
    /// </returns>
    public async Task<string> ReadSensorTypeAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadStringAsync(
            1000, 5, Encoding.ASCII, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполнеят чтение версии прошивки.
    /// </summary>
    /// <returns>
    /// Версия прошивки.
    /// </returns>
    public async Task<string> ReadFirmwareVersionAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadStringAsync(
            1005, 6, Encoding.ASCII, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение даты изготовление прошивки.
    /// </summary>
    /// <returns>
    /// Дата изготовления прошивки.
    /// </returns>
    public async Task<string> ReadFirmwareDateAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadStringAsync(
            1011, 5, Encoding.ASCII, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение серийного номера.
    /// </summary>
    /// <returns>
    /// Серийный номер.
    /// </returns>
    [CLSCompliant(false)]
    public async Task<uint> ReadSerialNumberAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadUInt32Async(
            1016, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение значения, определяющего включен ли DHCP.
    /// </summary>
    /// <returns>
    /// Значение, определяющек включен ли DHCP.
    /// </returns>
    public async Task<bool> ReadUseDhcpAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadBooleanAsync(
            1018, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись значения, определяющего включен ли DHCP.
    /// </summary>
    public async Task WriteUseDhcpAsync(bool value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteBooleanAsync(
            1018, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение UDP-порта.
    /// </summary>
    /// <returns>
    /// UDP-порт.
    /// </returns>
    [CLSCompliant(false)]
    public async Task<ushort> ReadUdpPortAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadUInt16Async(
            1019, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись UDP-порта.
    /// </summary>
    [CLSCompliant(false)]
    public async Task WriteUdpPortAsync(ushort value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteUInt16Async(
            1019, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение TCP-порта.
    /// </summary>
    /// <returns>
    /// TCP-порт.
    /// </returns>
    [CLSCompliant(false)]
    public async Task<ushort> ReadTcpPortAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadUInt16Async(
            1020, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись TCP-порта.
    /// </summary>
    [CLSCompliant(false)]
    public async Task WriteTcpPortAsync(ushort value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteUInt16Async(
            1020, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение диапазона измерения.
    /// </summary>
    /// <returns>
    /// Диапазон измерения.
    /// </returns>
    [CLSCompliant(false)]
    public async Task<ushort> ReadMeasuringRangeAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadUInt16Async(
            1021, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись диапазона измерения.
    /// </summary>
    [CLSCompliant(false)]
    public async Task WriteMeasuringRangeAsync(ushort value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteUInt16Async(
            1021, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение значения HighPass фильтра.
    /// </summary>
    /// <returns>
    /// Значение HighPass фильтра.
    /// </returns>
    [CLSCompliant(false)]
    public async Task<ushort> ReadHighPassFilterAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadUInt16Async(
            1022, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись значения HighPass фильтра.
    /// </summary>
    [CLSCompliant(false)]
    public async Task WriteHighPassFilterAsync(ushort value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteUInt16Async(
            1022, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение частоты дискретизации.
    /// </summary>
    /// <returns>
    /// Частота дискретизации.
    /// </returns>
    [CLSCompliant(false)]
    public async Task<ushort> ReadSamplingAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadUInt16Async(
            1023, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись частоты дискретизации.
    /// </summary>
    [CLSCompliant(false)]
    public async Task WriteSamplingAsync(ushort value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteUInt16Async(
            1023, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение смещения по оси Ox.
    /// </summary>
    /// <returns>
    /// Смещение по оси Ox.
    /// </returns>
    public async Task<float> ReadXOffsetAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadFloat32Async(
            1024, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись смещения по оси Ox.
    /// </summary>
    public async Task WriteXOffsetAsync(float value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteFloat32Async(
            1024, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение смещения по оси Oy.
    /// </summary>
    /// <returns>
    /// Смещение по оси Oy.
    /// </returns>
    public async Task<float> ReadYOffsetAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadFloat32Async(
            1026, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись смещения по оси Oy.
    /// </summary>
    public async Task WriteYOffsetAsync(float value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteFloat32Async(
            1026, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение смещения по оси Oz.
    /// </summary>
    /// <returns>
    /// Смещение по оси Oz.
    /// </returns>
    public async Task<float> ReadZOffsetAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadFloat32Async(
            1028, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись смещения по оси Oz.
    /// </summary>
    public async Task WriteZOffsetAsync(float value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteFloat32Async(
            1028, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение адреса датчика.
    /// </summary>
    /// <returns>
    /// Адрес датчика.
    /// </returns>
    public async Task<IPv4Address> ReadAddressAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadIPv4AddressAsync(
            1030, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись адреса датчика.
    /// </summary>
    public async Task WriteAddressAsync(IPv4Address value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteIPv4AddressAsync(
            1030, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение маски сети.
    /// </summary>
    /// <returns>
    /// Маска сети.
    /// </returns>
    public async Task<IPv4Address> ReadMaskAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadIPv4AddressAsync(
            1032, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись маски сети.
    /// </summary>
    public async Task WriteMaskAsync(IPv4Address value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteIPv4AddressAsync(
            1032, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение шлюза по умолчанию.
    /// </summary>
    /// <returns>
    /// Шлюз по умолчанию.
    /// </returns>
    public async Task<IPv4Address> ReadGatewayAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadIPv4AddressAsync(
            1034, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись шлюза по умолчанию.
    /// </summary>
    public async Task WriteGatewayAsync(IPv4Address value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteIPv4AddressAsync(
            1034, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение адреса сервера.
    /// </summary>
    /// <returns>
    /// Адрес сервера.
    /// </returns>
    public async Task<IPv4Address> ReadServerAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadIPv4AddressAsync(
            1036, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись адреса сервера.
    /// </summary>
    public async Task WriteServerAsync(IPv4Address value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteIPv4AddressAsync(
            1036, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение максимальной температуры.
    /// </summary>
    /// <returns>
    /// Максимальная температура.
    /// </returns>
    public async Task<float> ReadMaxTemperatureAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadFloat32Async(
            1038, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись максимальной температуры.
    /// </summary>
    public async Task WriteMaxTemperatureAsync(float value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteFloat32Async(
            1038, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение минимальной температуры.
    /// </summary>
    /// <returns>
    /// Минимальная температура.
    /// </returns>
    public async Task<float> ReadMinTemperatureAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadFloat32Async(
            1040, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись минимальной температуры.
    /// </summary>
    public async Task WriteMinTemperatureAsync(float value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteFloat32Async(
            1040, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение максимального напряжения питания.
    /// </summary>
    /// <returns>
    /// Максимальное напряжение питания.
    /// </returns>
    public async Task<float> ReadMaxVoltageAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadFloat32Async(
            1042, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись максимального напряжения питания.
    /// </summary>
    public async Task WriteMaxVoltageAsync(float value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteFloat32Async(
            1042, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение минимального напряжения питания.
    /// </summary>
    /// <returns>
    /// Минимальное напряжение питания.
    /// </returns>
    public async Task<float> ReadMinVoltageAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadFloat32Async(
            1044, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись минимального напряжения питания.
    /// </summary>
    public async Task WriteMinVoltageAsync(float value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteFloat32Async(
            1044, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение диагностического значения.
    /// </summary>
    /// <returns>
    /// Диагностическое значение.
    /// </returns>
    public async Task<float> ReadDiagnosticValueAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadFloat32Async(
            1046, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение информации о сохранённых ошибках.
    /// </summary>
    /// <returns>
    /// Информация о сохранённых ошибках.
    /// </returns>
    [CLSCompliant(false)]
    public async Task<uint> ReadErrorCodesAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadUInt32Async(
            1048, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет сброс информации о сохранённых ошибках.
    /// </summary>
    [CLSCompliant(false)]
    public async Task ResetErrorCodesAsync(CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteUInt16Async(
            1048, 0, cancellationToken).ConfigureAwait(false);
        await _ModbusConnect.WriteUInt16Async(
            1049, 0, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение состояния.
    /// </summary>
    /// <returns>
    /// Состояние.
    /// </returns>
    [CLSCompliant(false)]
    public async Task<ushort> ReadStateAsync(CancellationToken cancellationToken)
    {
        //  Чтение регистров.
        return await _ModbusConnect.ReadUInt16Async(
            1050, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет запись состояния.
    /// </summary>
    [CLSCompliant(false)]
    public async Task WriteStateAsync(ushort value, CancellationToken cancellationToken)
    {
        //  Запись регистров.
        await _ModbusConnect.WriteUInt16Async(
            1050, value, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Выполняет чтение типа датчика.
    /// </summary>
    /// <returns>
    /// Тип датчика.
    /// </returns>
    public string ReadSensorType()
    {
        //  Чтение регистров.
        return ReadSensorTypeAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполнеят чтение версии прошивки.
    /// </summary>
    /// <returns>
    /// Версия прошивки.
    /// </returns>
    public string ReadFirmwareVersion()
    {
        //  Чтение регистров.
        return ReadFirmwareVersionAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение даты изготовление прошивки.
    /// </summary>
    /// <returns>
    /// Дата изготовления прошивки.
    /// </returns>
    public string ReadFirmwareDate()
    {
        //  Чтение регистров.
        return ReadFirmwareDateAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение серийного номера.
    /// </summary>
    /// <returns>
    /// Серийный номер.
    /// </returns>
    [CLSCompliant(false)]
    public uint ReadSerialNumber()
    {
        //  Чтение регистров.
        return ReadSerialNumberAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение значения, определяющего включен ли DHCP.
    /// </summary>
    /// <returns>
    /// Значение, определяющек включен ли DHCP.
    /// </returns>
    public bool ReadUseDhcp()
    {
        //  Чтение регистров.
        return ReadUseDhcpAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись значения, определяющего включен ли DHCP.
    /// </summary>
    public void WriteUseDhcp(bool value)
    {
        //  Запись регистров.
        WriteUseDhcpAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение UDP-порта.
    /// </summary>
    /// <returns>
    /// UDP-порт.
    /// </returns>
    [CLSCompliant(false)]
    public ushort ReadUdpPort()
    {
        //  Чтение регистров.
        return ReadUdpPortAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись UDP-порта.
    /// </summary>
    [CLSCompliant(false)]
    public void WriteUdpPort(ushort value)
    {
        //  Запись регистров.
        WriteUdpPortAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение TCP-порта.
    /// </summary>
    /// <returns>
    /// TCP-порт.
    /// </returns>
    [CLSCompliant(false)]
    public ushort ReadTcpPort()
    {
        //  Чтение регистров.
        return ReadTcpPortAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись TCP-порта.
    /// </summary>
    [CLSCompliant(false)]
    public void WriteTcpPort(ushort value)
    {
        //  Запись регистров.
        WriteTcpPortAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение диапазона измерения.
    /// </summary>
    /// <returns>
    /// Диапазон измерения.
    /// </returns>
    [CLSCompliant(false)]
    public ushort ReadMeasuringRange()
    {
        //  Чтение регистров.
        return ReadMeasuringRangeAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись диапазона измерения.
    /// </summary>
    [CLSCompliant(false)]
    public void WriteMeasuringRange(ushort value)
    {
        //  Запись регистров.
        WriteMeasuringRangeAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение значения HighPass фильтра.
    /// </summary>
    /// <returns>
    /// Значение HighPass фильтра.
    /// </returns>
    [CLSCompliant(false)]
    public ushort ReadHighPassFilter()
    {
        //  Чтение регистров.
        return ReadHighPassFilterAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись значения HighPass фильтра.
    /// </summary>
    [CLSCompliant(false)]
    public void WriteHighPassFilter(ushort value)
    {
        //  Запись регистров.
        WriteHighPassFilterAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение частоты дискретизации.
    /// </summary>
    /// <returns>
    /// Частота дискретизации.
    /// </returns>
    [CLSCompliant(false)]
    public ushort ReadSampling()
    {
        //  Чтение регистров.
        return ReadSamplingAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись частоты дискретизации.
    /// </summary>
    [CLSCompliant(false)]
    public void WriteSampling(ushort value)
    {
        //  Запись регистров.
        WriteSamplingAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение смещения по оси Ox.
    /// </summary>
    /// <returns>
    /// Смещение по оси Ox.
    /// </returns>
    public float ReadXOffset()
    {
        //  Чтение регистров.
        return ReadXOffsetAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись смещения по оси Ox.
    /// </summary>
    public void WriteXOffset(float value)
    {
        //  Запись регистров.
        WriteXOffsetAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение смещения по оси Oy.
    /// </summary>
    /// <returns>
    /// Смещение по оси Oy.
    /// </returns>
    public float ReadYOffset()
    {
        //  Чтение регистров.
        return ReadYOffsetAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись смещения по оси Oy.
    /// </summary>
    public void WriteYOffset(float value)
    {
        //  Запись регистров.
        WriteYOffsetAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение смещения по оси Oz.
    /// </summary>
    /// <returns>
    /// Смещение по оси Oz.
    /// </returns>
    public float ReadZOffset()
    {
        //  Чтение регистров.
        return ReadZOffsetAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись смещения по оси Oz.
    /// </summary>
    public void WriteZOffset(float value)
    {
        //  Запись регистров.
        WriteZOffsetAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение адреса датчика.
    /// </summary>
    /// <returns>
    /// Адрес датчика.
    /// </returns>
    public IPv4Address ReadAddress()
    {
        //  Чтение регистров.
        return ReadAddressAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись адреса датчика.
    /// </summary>
    public void WriteAddress(IPv4Address value)
    {
        //  Запись регистров.
        WriteAddressAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение маски сети.
    /// </summary>
    /// <returns>
    /// Маска сети.
    /// </returns>
    public IPv4Address ReadMask()
    {
        //  Чтение регистров.
        return ReadMaskAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись маски сети.
    /// </summary>
    public void WriteMask(IPv4Address value)
    {
        //  Запись регистров.
        WriteMaskAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение шлюза по умолчанию.
    /// </summary>
    /// <returns>
    /// Шлюз по умолчанию.
    /// </returns>
    public IPv4Address ReadGateway()
    {
        //  Чтение регистров.
        return ReadGatewayAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись шлюза по умолчанию.
    /// </summary>
    public void WriteGateway(IPv4Address value)
    {
        //  Запись регистров.
        WriteGatewayAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение адреса сервера.
    /// </summary>
    /// <returns>
    /// Адрес сервера.
    /// </returns>
    public IPv4Address ReadServer()
    {
        //  Чтение регистров.
        return ReadServerAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись адреса сервера.
    /// </summary>
    public void WriteServer(IPv4Address value)
    {
        //  Запись регистров.
        WriteServerAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение максимальной температуры.
    /// </summary>
    /// <returns>
    /// Максимальная температура.
    /// </returns>
    public float ReadMaxTemperature()
    {
        //  Чтение регистров.
        return ReadMaxTemperatureAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись максимальной температуры.
    /// </summary>
    public void WriteMaxTemperature(float value)
    {
        //  Запись регистров.
        WriteMaxTemperatureAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение минимальной температуры.
    /// </summary>
    /// <returns>
    /// Минимальная температура.
    /// </returns>
    public float ReadMinTemperature()
    {
        //  Чтение регистров.
        return ReadMinTemperatureAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись минимальной температуры.
    /// </summary>
    public void WriteMinTemperature(float value)
    {
        //  Запись регистров.
        WriteMinTemperatureAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение максимального напряжения питания.
    /// </summary>
    /// <returns>
    /// Максимальное напряжение питания.
    /// </returns>
    public float ReadMaxVoltage()
    {
        //  Чтение регистров.
        return ReadMaxVoltageAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись максимального напряжения питания.
    /// </summary>
    public void WriteMaxVoltage(float value)
    {
        //  Запись регистров.
        WriteMaxVoltageAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение минимального напряжения питания.
    /// </summary>
    /// <returns>
    /// Минимальное напряжение питания.
    /// </returns>
    public float ReadMinVoltage()
    {
        //  Чтение регистров.
        return ReadMinVoltageAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись минимального напряжения питания.
    /// </summary>
    public void WriteMinVoltage(float value)
    {
        //  Запись регистров.
        WriteMinVoltageAsync(value, default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение диагностического значения.
    /// </summary>
    /// <returns>
    /// Диагностическое значение.
    /// </returns>
    public float ReadDiagnosticValue()
    {
        //  Чтение регистров.
        return ReadDiagnosticValueAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение информации о сохранённых ошибках.
    /// </summary>
    /// <returns>
    /// Информация о сохранённых ошибках.
    /// </returns>
    [CLSCompliant(false)]
    public uint ReadErrorCodes()
    {
        //  Чтение регистров.
        return ReadErrorCodesAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет чтение состояния.
    /// </summary>
    /// <returns>
    /// Состояние.
    /// </returns>
    [CLSCompliant(false)]
    public ushort ReadState()
    {
        //  Чтение регистров.
        return ReadStateAsync(default).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Выполняет запись состояния.
    /// </summary>
    [CLSCompliant(false)]
    public void WriteState(ushort value)
    {
        //  Запись регистров.
        WriteStateAsync(value, default).GetAwaiter().GetResult();
    }
}
