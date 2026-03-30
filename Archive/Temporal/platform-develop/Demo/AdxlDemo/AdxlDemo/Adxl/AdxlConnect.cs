using Apeiron.Platform.Demo.AdxlDemo.Modbus;
using Apeiron.Platform.Demo.AdxlDemo.Net;
using System.Net;
using System.Text;

namespace Apeiron.Platform.Demo.AdxlDemo.Adxl;

/// <summary>
/// Представялет соединение с датчиком ADXL357.
/// </summary>
public sealed class AdxlConnect :
    Active
{
    /// <summary>
    /// Постоянная, определяющая идентификатор устройства.
    /// </summary>
    private const byte _SlaveIdentifier = 1;

    /// <summary>
    /// Поле для хранения подключения к датчику.
    /// </summary>
    private readonly TcpMaster _TcpMaster;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <param name="network">
    /// Сеть, в которой находится датчик.
    /// </param>
    /// <param name="address">
    /// Адрес датчика.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="network"/> передана пустая ссылка.
    /// </exception>
    public AdxlConnect(Engine engine, Network network, IPv4Address address) :
        base(engine)
    {
        //  Установка сети, в которой находится датчик.
        Network = IsNotNull(network, nameof(network));

        //  Создание подключения к датчику.
        _TcpMaster = new(address, _SlaveIdentifier);

        //  Установка адреса датчика.
        Address = address;
    }

    /// <summary>
    /// Возвращает сеть, в которой находится датчик.
    /// </summary>
    public Network Network { get; }

    /// <summary>
    /// Возвращает адрес датчика.
    /// </summary>
    public IPv4Address Address { get; }

    /// <summary>
    /// Выполняет операцию проверки на равенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    public static bool operator ==(AdxlConnect left, AdxlConnect right)
    {
        //  Проверка ссылок на операнды.
        IsNotNull(left, nameof(left));
        IsNotNull(right, nameof(right));

        //  Создание списков байт операндов.
        List<byte> leftBytes = new();
        List<byte> rightBytes = new();

        //  Формирование списков байт операндов.
        leftBytes.AddRange(left.Network.Address.GetAddressBytes());
        leftBytes.AddRange(left.Network.Mask.GetAddressBytes());
        leftBytes.AddRange(left.Address.GetAddressBytes());
        leftBytes.AddRange(left.Network.Host.GetAddressBytes());

        rightBytes.AddRange(right.Network.Address.GetAddressBytes());
        rightBytes.AddRange(right.Network.Mask.GetAddressBytes());
        rightBytes.AddRange(right.Address.GetAddressBytes());
        rightBytes.AddRange(right.Network.Host.GetAddressBytes());

        //  Сравнение размеров списков.
        if (leftBytes.Count != rightBytes.Count)
        {
            //  Различный размер данных.
            return false;
        }

        //  Сравнение байт.
        for (int i = 0; i < leftBytes.Count; i++)
        {
            if (leftBytes[i] != rightBytes[i])
            {
                //  Данные не совпадают.
                return false;
            }
        }

        //  Данные совпадают.
        return true;
    }

    /// <summary>
    /// Выполняет операцию проверки на неравенство.
    /// </summary>
    /// <param name="left">
    /// Левый операнд.
    /// </param>
    /// <param name="right">
    /// Правый операнд.
    /// </param>
    /// <returns>
    /// Результат операции.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="left"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="right"/> передана пустая ссылка.
    /// </exception>
    public static bool operator !=(AdxlConnect left, AdxlConnect right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Асинхронно выполняет поиск соединения с датчиком.
    /// </summary>
    /// <param name="engine">
    /// Основной активный объект.
    /// </param>
    /// <param name="address">
    /// Адрес датчика.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск датчика.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="engine"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public static async Task<AdxlConnect?> FindAsync(Engine engine, IPv4Address address, CancellationToken cancellationToken)
    {
        //  Проверка основного активного объекта.
        IsNotNull(engine, nameof(engine));

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение коллекции сетей.
        NetworkCollection networks = engine.Root.Networks;

        //  Поиск сети.
        Network? network = await networks.FindAsync(address, cancellationToken).ConfigureAwait(false);

        //  Проверка сети.
        if (network is null)
        {
            //  Обновление сетей.
            await networks.UpdateAsync(cancellationToken).ConfigureAwait(false);

            //  Повторный поиск сети.
            network = await networks.FindAsync(address, cancellationToken).ConfigureAwait(false);

            //  Проверка сети.
            if (network is null)
            {
                //  Сеть не найдена.
                return null;
            }
        }

        //  Создание и возврат соединения с датчиком.
        return new(engine, network, address);
    }

    /// <summary>
    /// Выполняет чтение типа датчика.
    /// </summary>
    /// <returns>
    /// Тип датчика.
    /// </returns>
    public string ReadSensorType()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1000, 5);

            //  Возврат прочитанной строки.
            return ToString(registers);
        }
    }

    /// <summary>
    /// Выполнеят чтение версии прошивки.
    /// </summary>
    /// <returns>
    /// Версия прошивки.
    /// </returns>
    public string ReadFirmwareVersion()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1005, 6);

            //  Возврат прочитанной строки.
            return ToString(registers);
        }
    }

    /// <summary>
    /// Выполняет чтение даты изготовление прошивки.
    /// </summary>
    /// <returns>
    /// Дата изготовления прошивки.
    /// </returns>
    public string ReadFirmwareDate()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1011, 5);

            //  Возврат прочитанной строки.
            return ToString(registers);
        }
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
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1016, 2);

            //  Возврат прочитанного значения.
            return registers[1] | ((uint)registers[0] << 16);
        }
    }

    /// <summary>
    /// Выполняет чтение значения, определяющего включен ли DHCP.
    /// </summary>
    /// <returns>
    /// Значение, определяющек включен ли DHCP.
    /// </returns>
    public bool ReadUseDhcp()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1018, 1);

            //  Возврат прочитанного значения.
            return registers[0] != 0;
        }
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
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1019, 1);

            //  Возврат прочитанного значения.
            return registers[0];
        }
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
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1020, 1);

            //  Возврат прочитанного значения.
            return registers[0];
        }
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
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1021, 1);

            //  Возврат прочитанного значения.
            return registers[0];
        }
    }

    /// <summary>
    /// Выполняет запись диапазона измерения.
    /// </summary>
    /// <param name="value">
    /// Значение для записи.
    /// </param>
    [CLSCompliant(false)]
    public void WriteMeasuringRange(ushort value)
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Получение регистров.
            ushort[] registers = { value };

            //  Запись регистров.
            _TcpMaster.WriteHoldings(1021, registers);
        }
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
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1022, 1);

            //  Возврат прочитанного значения.
            return registers[0];
        }
    }

    /// <summary>
    /// Выполняет запись значения HighPass фильтра.
    /// </summary>
    /// <param name="value">
    /// Значение для записи.
    /// </param>
    [CLSCompliant(false)]
    public void WriteHighPassFilter(ushort value)
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Получение регистров.
            ushort[] registers = { value };

            //  Запись регистров.
            _TcpMaster.WriteHoldings(1022, registers);
        }
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
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1023, 1);

            //  Возврат прочитанного значения.
            return registers[0];
        }
    }

    /// <summary>
    /// Выполняет запись частоты дискретизации.
    /// </summary>
    /// <param name="value">
    /// Значение для записи.
    /// </param>
    [CLSCompliant(false)]
    public void WriteSampling(ushort value)
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Получение регистров.
            ushort[] registers = { value };

            //  Запись регистров.
            _TcpMaster.WriteHoldings(1023, registers);
        }
    }

    /// <summary>
    /// Выполняет чтение смещения по оси Ox.
    /// </summary>
    /// <returns>
    /// Смещение по оси Ox.
    /// </returns>
    public float ReadXOffset()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1024, 2);

            //  Возврат прочитанного значения.
            return ToFloat32(registers);
        }
    }

    /// <summary>
    /// Выполняет запись смещения по оси Ox.
    /// </summary>
    /// <param name="value">
    /// Значение для записи.
    /// </param>
    public void WriteXOffset(float value)
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Получение регистров.
            ushort[] registers = FromFloat32(value);

            //  Запись регистров.
            _TcpMaster.WriteHoldings(1024, registers);
        }
    }

    /// <summary>
    /// Выполняет чтение смещения по оси Oy.
    /// </summary>
    /// <returns>
    /// Смещение по оси Oy.
    /// </returns>
    public float ReadYOffset()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1026, 2);

            //  Возврат прочитанного значения.
            return ToFloat32(registers);
        }
    }

    /// <summary>
    /// Выполняет запись смещения по оси Oy.
    /// </summary>
    /// <param name="value">
    /// Значение для записи.
    /// </param>
    public void WriteYOffset(float value)
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Получение регистров.
            ushort[] registers = FromFloat32(value);

            //  Запись регистров.
            _TcpMaster.WriteHoldings(1026, registers);
        }
    }

    /// <summary>
    /// Выполняет чтение смещения по оси Oz.
    /// </summary>
    /// <returns>
    /// Смещение по оси Oz.
    /// </returns>
    public float ReadZOffset()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1028, 2);

            //  Возврат прочитанного значения.
            return ToFloat32(registers);
        }
    }

    /// <summary>
    /// Выполняет запись смещения по оси Oz.
    /// </summary>
    /// <param name="value">
    /// Значение для записи.
    /// </param>
    public void WriteZOffset(float value)
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Получение регистров.
            ushort[] registers = FromFloat32(value);

            //  Запись регистров.
            _TcpMaster.WriteHoldings(1028, registers);
        }
    }

    /// <summary>
    /// Выполняет чтение адреса датчика.
    /// </summary>
    /// <returns>
    /// Адрес датчика.
    /// </returns>
    public IPv4Address ReadAddress()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1030, 2);

            //  Возврат прочитанного значения.
            return ToIPv4Address(registers);
        }
    }

    /// <summary>
    /// Выполняет чтение маски сети.
    /// </summary>
    /// <returns>
    /// Маска сети.
    /// </returns>
    public IPv4Address ReadMask()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1032, 2);

            //  Возврат прочитанного значения.
            return ToIPv4Address(registers);
        }
    }

    /// <summary>
    /// Выполняет чтение шлюза по умолчанию.
    /// </summary>
    /// <returns>
    /// Шлюз по умолчанию.
    /// </returns>
    public IPv4Address ReadGateway()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1034, 2);

            //  Возврат прочитанного значения.
            return ToIPv4Address(registers);
        }
    }

    /// <summary>
    /// Выполняет чтение адреса сервера.
    /// </summary>
    /// <returns>
    /// Адрес сервера.
    /// </returns>
    public IPv4Address ReadServer()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1036, 2);

            //  Возврат прочитанного значения.
            return ToIPv4Address(registers);
        }
    }

    /// <summary>
    /// Выполняет запись адреса сервера.
    /// </summary>
    /// <param name="value">
    /// Адрес сервера.
    /// </param>
    public void WriteServer(IPv4Address value)
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Получение регистров.
            ushort[] registers = FromIPv4Address(value);

            //  Запись регистров.
            _TcpMaster.WriteHoldings(1036, registers);
        }
    }

    /// <summary>
    /// Выполняет чтение максимальной температуры.
    /// </summary>
    /// <returns>
    /// Максимальная температура.
    /// </returns>
    public float ReadMaxTemperature()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1038, 2);

            //  Возврат прочитанного значения.
            return ToFloat32(registers);
        }
    }

    /// <summary>
    /// Выполняет чтение минимальной температуры.
    /// </summary>
    /// <returns>
    /// Минимальная температура.
    /// </returns>
    public float ReadMinTemperature()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1040, 2);

            //  Возврат прочитанного значения.
            return ToFloat32(registers);
        }
    }

    /// <summary>
    /// Выполняет чтение максимального напряжения питания.
    /// </summary>
    /// <returns>
    /// Максимальное напряжение питания.
    /// </returns>
    public float ReadMaxVoltage()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1042, 2);

            //  Возврат прочитанного значения.
            return ToFloat32(registers);
        }
    }

    /// <summary>
    /// Выполняет чтение минимального напряжения питания.
    /// </summary>
    /// <returns>
    /// Минимальное напряжение питания.
    /// </returns>
    public float ReadMinVoltage()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1044, 2);

            //  Возврат прочитанного значения.
            return ToFloat32(registers);
        }
    }

    /// <summary>
    /// Выполняет чтение диагностического значения.
    /// </summary>
    /// <returns>
    /// Диагностическое значение.
    /// </returns>
    public float ReadDiagnosticValue()
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1046, 2);

            //  Возврат прочитанного значения.
            return ToFloat32(registers);
        }
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
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1048, 2);

            //  Возврат прочитанного значения.
            return registers[1] | ((uint)registers[0] << 16);
        }
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
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Чтение регистров.
            ushort[] registers = _TcpMaster.ReadHoldings(1050, 1);

            //  Возврат прочитанного значения.
            return registers[0];
        }
    }

    /// <summary>
    /// Выполняет запись состояния.
    /// </summary>
    /// <param name="value">
    /// Состояние.
    /// </param>
    [CLSCompliant(false)]
    public void WriteState(ushort value)
    {
        //  Блокировка критического объекта.
        lock (_TcpMaster)
        {
            //  Получение регистров.
            ushort[] registers = { value };

            //  Запись регистров.
            _TcpMaster.WriteHoldings(1050, registers);
        }
    }

    /// <summary>
    /// Преобразует значение регистров в значение типа <see cref="IPv4Address"/>.
    /// </summary>
    /// <param name="registers">
    /// Массив регистров.
    /// </param>
    /// <returns>
    /// Значение типа <see cref="float"/>.
    /// </returns>
    private static IPv4Address ToIPv4Address([ParameterNoChecks] ushort[] registers)
    {
        //  Создание массива данных.
        byte[] data = ToBytes(registers);

        //  Возврат прочитанного значения.
        return new(new IPAddress(data));
    }

    /// <summary>
    /// Преобразует значение регистров в значение типа <see cref="float"/>.
    /// </summary>
    /// <param name="registers">
    /// Массив регистров.
    /// </param>
    /// <returns>
    /// Значение типа <see cref="float"/>.
    /// </returns>
    private static float ToFloat32([ParameterNoChecks] ushort[] registers)
    {
        //  Создание массива данных.
        byte[] data = ToBytes(registers);

        //  Возврат прочитанного значения.
        return BitConverter.ToSingle(data);
    }

    /// <summary>
    /// Преобразует значение регистров в текстовое представление.
    /// </summary>
    /// <param name="registers">
    /// Массив регистров.
    /// </param>
    /// <returns>
    /// Текстовое представление.
    /// </returns>
    private static string ToString([ParameterNoChecks] ushort[] registers)
    {
        //  Создание массива данных.
        byte[] data = ToBytes(registers);

        //  Возврат прочитанной строки.
        return Encoding.ASCII.GetString(data).Trim('\0');
    }

    /// <summary>
    /// Преобразует значение регистров в массив байт.
    /// </summary>
    /// <param name="registers">
    /// Массив регистров.
    /// </param>
    /// <returns>
    /// Массив байт.
    /// </returns>
    private static byte[] ToBytes([ParameterNoChecks] ushort[] registers)
    {
        //  Создание массива данных.
        byte[] data = new byte[registers.Length << 1];

        //  Заполнение массива данных.
        for (int i = 0; i < registers.Length; i++)
        {
            data[2 * i] = unchecked((byte)registers[i]);
            data[2 * i + 1] = unchecked((byte)(registers[i] >> 8));
        }

        //  Возврат массива данных.
        return data;
    }

    /// <summary>
    /// Преобразует значение типа <see cref="IPv4Address"/> в массив регистров.
    /// </summary>
    /// <param name="value">
    /// Значение.
    /// </param>
    /// <returns>
    /// Массив регистров.
    /// </returns>
    private static ushort[] FromIPv4Address([ParameterNoChecks] IPv4Address value)
    {
        //  Создание массива данных.
        byte[] data = value.GetAddressBytes();

        //  Возврат прочитанного значения.
        return FromBytes(data);
    }

    /// <summary>
    /// Преобразует значение типа <see cref="float"/> в массив регистров.
    /// </summary>
    /// <param name="value">
    /// Значение.
    /// </param>
    /// <returns>
    /// Массив регистров.
    /// </returns>
    private static ushort[] FromFloat32([ParameterNoChecks] float value)
    {
        //  Создание массива данных.
        byte[] data = BitConverter.GetBytes(value);

        //  Возврат прочитанного значения.
        return FromBytes(data);
    }

    /// <summary>
    /// Преобразует массив байт в массив регистров.
    /// </summary>
    /// <param name="bytes">
    /// Массив байт.
    /// </param>
    /// <returns>
    /// Массив регистров.
    /// </returns>
    private static ushort[] FromBytes([ParameterNoChecks] byte[] bytes)
    {
        //  Созадние массива регистров.
        ushort[] registers = new ushort[bytes.Length >> 1];

        //  Заполнение массива байт.
        for (int i = 0; i < registers.Length; i++)
        {
            registers[i] = (ushort)(bytes[2 * i] | (bytes[2 * i + 1] << 8));
        }

        //  Возврат массива регистров.
        return registers;
    }

    /// <summary>
    /// Возвращает хэш-код объекта.
    /// </summary>
    /// <returns>
    /// Хэш-код объекта.
    /// </returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Network.Address, Network.Mask, Address, Network.Host);
    }

    /// <summary>
    /// Выполняет проверку на равенство с заданным экземпляром.
    /// </summary>
    /// <param name="obj">
    /// Экземпляр для проверки.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    public override bool Equals(object? obj)
    {
        //  Проверка типа.
        if (obj is not AdxlConnect other)
        {
            //  Экземпляр нельзя привести к типу для сравнения.
            return false;
        }

        //  Выполнение операции проверки на равенство.
        return this == other;
    }
}
