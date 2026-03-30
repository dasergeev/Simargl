using Apeiron.Platform.Modbus;
using System.Net;
using System.Text;

namespace Apeiron.Platform.Teltonika;

/// <summary>
/// Представляет интерфейс получения данных по Modbus от Teltonika.
/// </summary>
public sealed class ModbusManager
{
    /// <summary>
    /// Представляет семафор для блокировки многопоточного доступа к класу.
    /// </summary>
    private readonly SemaphoreSlim _Semaphore = new(1, 1);

    /// <summary>
    /// Размер строк в байтах
    /// </summary>
    private const byte _StringTeltonikaLength = 32;

    /// <summary>
    /// Интервал на отмену операции чтения в мс.
    /// </summary>
    private const ushort _ModbusTimeout = 5000;

    /// <summary>
    /// Поле - драйвер комуникации Modbus Tcp.
    /// </summary>
    private readonly ModbuTcpMaster _ModbusMaster;

    /// <summary>
    /// Возвращает время от момента запуска.
    /// </summary>
    internal uint SystemUpTime { get; private set; }

    /// <summary>
    /// Возвращает уровень сигнала GSM текущей SIM-карты.
    /// </summary>
    internal int MobileSignalStrength { get; private set; }

    /// <summary>
    /// Возвращает температуру системы.
    /// </summary>
    internal int SystemTemperature { get; private set; }

    /// <summary>
    /// Возвращает имя системы.
    /// </summary>
    internal string SystemHostName { get; private set; } = string.Empty;

    /// <summary>
    /// Возвращает имя оператора текущей Sim-карты.
    /// </summary>
    internal string GsmOperatorName { get; private set; } = string.Empty;

    /// <summary>
    /// Возвращает серийный номер роутера.
    /// </summary>
    internal string SerialNumber { get; private set; } = string.Empty;

    /// <summary>
    /// Возвращает MAC-адрес.
    /// </summary>
    internal string MacAddress { get; private set; } = string.Empty;

    /// <summary>
    /// Возвращает имя роутера.
    /// </summary>
    internal string RouterName { get; private set; } = string.Empty;

    /// <summary>
    /// Возвращает текущий активный слот SIM-карты.
    /// </summary>
    internal string ActiveSimCardSlot { get; private set; } = string.Empty;

    /// <summary>
    /// Возвращает информацию о состоянии регистрации.
    /// </summary>
    internal string NetworkRegistrationInfo { get; private set; } = string.Empty;

    /// <summary>
    /// Возвращает тип сети.
    /// </summary>
    internal string NetworkType { get; private set; } = string.Empty;

    /// <summary>
    /// Возвращает состояние дискретного входа 1.
    /// </summary>
    internal int DigitalInput1 { get; private set; }

    /// <summary>
    /// Возвращает состояние дискретного входа 2.
    /// </summary>
    internal int DigitalInput2 { get; private set; }

    /// <summary>
    /// Возвращает Wan IP.
    /// </summary>
    internal uint WanIP { get; private set; }

    /// <summary>
    /// Возвращает состояние аналого входа.
    /// </summary>
    internal int AnalogInput { get; private set; }

    /// <summary>
    /// Возвращает широту.
    /// </summary>
    internal float Latitude { get; private set; }

    /// <summary>
    /// Возвращает долготу.
    /// </summary>
    internal float Longitude { get; private set; }

    /// <summary>
    /// Возвращает Unix-время.
    /// </summary>
    internal string UnixTime { get; private set; } = string.Empty;

    /// <summary>
    /// Возвращает время GPS.
    /// </summary>
    internal string Time { get; private set; } = string.Empty;

    /// <summary>
    /// Возвращает скорость.
    /// </summary>
    internal int Speed { get; private set; }

    /// <summary>
    /// Возвращает количество спутников.
    /// </summary>
    internal int SateliteCount { get; private set; }

    /// <summary>
    /// Возвращает точность.
    /// </summary>
    internal float Accuracy { get; private set; }

    /// <summary>
    /// Возвращает количество данных полученых сегодня.
    /// </summary>
    internal uint DataReceiveTodaySim1 { get; private set; }

    /// <summary>
    /// Возвращает количество данных отправленых сегодня.
    /// </summary>
    internal uint DataSentTodaySim1 { get; private set; }

    /// <summary>
    /// Возвращает количество данных полученых за текущую неделю.
    /// </summary>
    internal uint DataReceiveThisWeekSim1 { get; private set; }

    /// <summary>
    /// Возвращает количество данных отправленых за текущую неделю.
    /// </summary>
    internal uint DataSentThisWeekSim1 { get; private set; }

    /// <summary>
    /// Возвращает количество данных полученых за текущий месяц.
    /// </summary>
    internal uint DataReceiveThisMonthSim1 { get; private set; }

    /// <summary>
    /// Возвращает количество данных отправленых за текущий месяц.
    /// </summary>
    internal uint DataSentThisMonthSim1 { get; private set; }

    /// <summary>
    /// Возвращает количество данных полученых за 24 часа.
    /// </summary>
    internal uint DataReceive24hSim1 { get; private set; }

    /// <summary>
    /// Возвращает количество данных отправленых за 24 часа.
    /// </summary>
    internal uint DataSent24hSim1 { get; private set; }

    /// <summary>
    /// Возвращает состояния открытого колектора.
    /// </summary>
    internal ushort OpenCollectorStatus { get; private set; }

    /// <summary>
    /// Возвращает состояния повтора выхода.
    /// </summary>
    internal ushort RelayOutput { get; private set; }

    /// <summary>
    /// Возвращает активную SIM-карту.
    /// </summary>
    internal ushort ActiveSIM { get; private set; }


    /// <summary>
    /// Возвращает количество данных полученых за последнюю неделю.
    /// </summary>
    internal uint DataReceiveLastWeekSim1 { get; private set; }

    /// <summary>
    /// Возвращает количество данных отправленых за последнюю неделю.
    /// </summary>
    internal uint DataSentLastWeekSim1 { get; private set; }


    /// <summary>
    /// Возвращает количество данных полученых за последний месяц.
    /// </summary>
    internal uint DataReceiveLastMonthSim1 { get; private set; }

    /// <summary>
    /// Возвращает количество данных отправленых за последний месяц.
    /// </summary>
    internal uint DataSentLastMonthSim1 { get; private set; }


    /// <summary>
    /// Возвращает количество данных полученых сегодня.
    /// </summary>
    internal uint DataReceiveTodaySim2 { get; private set; }

    /// <summary>
    /// Возвращает количество данных отправленых сегодня.
    /// </summary>
    internal uint DataSentTodaySim2 { get; private set; }

    /// <summary>
    /// Возвращает количество данных полученых за текущую неделю.
    /// </summary>
    internal uint DataReceiveThisWeekSim2 { get; private set; }

    /// <summary>
    /// Возвращает количество данных отправленых за текущую неделю.
    /// </summary>
    internal uint DataSentThisWeekSim2 { get; private set; }

    /// <summary>
    /// Возвращает количество данных полученых за текущий месяц.
    /// </summary>
    internal uint DataReceiveThisMonthSim2 { get; private set; }

    /// <summary>
    /// Возвращает количество данных отправленых за текущий месяц.
    /// </summary>
    internal uint DataSentThisMonthSim2 { get; private set; }

    /// <summary>
    /// Возвращает количество данных полученых за 24 часа.
    /// </summary>
    internal uint DataReceive24hSim2 { get; private set; }

    /// <summary>
    /// Возвращает количество данных отправленых за 24 часа.
    /// </summary>
    internal uint DataSent24hSim2 { get; private set; }

    /// <summary>
    /// Возвращает количество данных полученых за последнюю неделю.
    /// </summary>
    internal uint DataReceiveLastWeekSim2 { get; private set; }

    /// <summary>
    /// Возвращает количество данных отправленых за последнюю неделю.
    /// </summary>
    internal uint DataSentLastWeekSim2 { get; private set; }

    /// <summary>
    /// Возвращает количество данных полученых за последний месяц.
    /// </summary>
    internal uint DataReceiveLastMonthSim2 { get; private set; }

    /// <summary>
    /// Возвращает количество данных отправленых за последний месяц.
    /// </summary>
    internal uint DataSentLastMonthSim2 { get; private set; }

    /// <summary>
    /// Возвращает состояние дискретного входа 4.
    /// </summary>
    internal ushort DigitalInput4 { get; private set; }

    /// <summary>
    /// Возвращает состояние дискретного коллектора 4.
    /// </summary>
    internal ushort DigitalOpenCollector4 { get; private set; }

    /// <summary>
    /// Возвращает состояние дискретного коллектора 4.
    /// </summary>
    internal string IMSI { get; private set; } = string.Empty;

    /// <summary>
    /// Представляет конструктор объекта.
    /// </summary>
    /// <param name="ip">
    /// IP адрес Slave устройства.
    /// </param>
    /// <param name="port">
    /// Порт Slave - устройства.
    /// </param>
    /// <param name="deviceAdress">
    /// Modbus адрес устройства.
    /// </param>
    public ModbusManager(IPAddress ip, int port, byte deviceAdress)
    {
        _ModbusMaster = new(ip, port, deviceAdress);
    }

    /// <summary>
    /// Представляет функцию утановки уровня состояния выхода 1.
    /// </summary>  
    /// <exception cref="IOException">
    /// Не верный протокол обмена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="FormatException">
    /// В modbus header пришел код ошибки.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public async Task SetDigitalOut1Async(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await _Semaphore.WaitAsync(cancellationToken);
        try
        {
            //  Инициализация массива записи
            ushort[] registers = new ushort[1] { 0x0001 };

            //  Запис регистра
            await _ModbusMaster.WriteMultipleRegistersAsync(201, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            //  Освобождение семафора.
            _Semaphore.Release();
        }
    }
    /// <summary>
    /// Представляет функцию очистки уровня состояния выхода 1.
    /// </summary>  
    /// <exception cref="IOException">
    /// Не верный протокол обмена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="FormatException">
    /// В modbus header пришел код ошибки.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public async Task ClearDigitalOut1Async(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await _Semaphore.WaitAsync(cancellationToken);
        try
        {
            //  Инициализация массива записи
            ushort[] registers = new ushort[1] { 0x0000 };

            //  Запис регистра
            await _ModbusMaster.WriteMultipleRegistersAsync(201, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            //  Освобождение семафора.
            _Semaphore.Release();
        }
    }

    /// <summary>
    /// Представляет функцию утановки уровня состояния выхода 2.
    /// </summary>  
    /// <exception cref="IOException">
    /// Не верный протокол обмена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="FormatException">
    /// В modbus header пришел код ошибки.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public async Task SetDigitalOut2Async(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await _Semaphore.WaitAsync(cancellationToken);
        try
        {
            //  Инициализация массива записи
            ushort[] registers = new ushort[1] { 0x0001 };

            //  Запис регистра
            await _ModbusMaster.WriteMultipleRegistersAsync(202, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            //  Освобождение семафора.
            _Semaphore.Release();
        }
    }
    /// <summary>
    /// Представляет функцию очистки уровня состояния выхода 2.
    /// </summary>  
    /// <exception cref="IOException">
    /// Не верный протокол обмена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="FormatException">
    /// В modbus header пришел код ошибки.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public async Task ClearDigitalOut2Async(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await _Semaphore.WaitAsync(cancellationToken);
        try
        {
            //  Инициализация массива записи
            ushort[] registers = new ushort[1] { 0x0000 };

            //  Запис регистра
            await _ModbusMaster.WriteMultipleRegistersAsync(202, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            //  Освобождение семафора.
            _Semaphore.Release();
        }
    }


    /// <summary>
    /// Представляет функцию утановки уровня состояния выхода 4.
    /// </summary>  
    /// <exception cref="IOException">
    /// Не верный протокол обмена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="FormatException">
    /// В modbus header пришел код ошибки.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public async Task SetDigitalOut4Async(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await _Semaphore.WaitAsync(cancellationToken);
        try
        {
            //  Инициализация массива записи
            ushort[] registers = new ushort[1] { 0x0001 };

            //  Запис регистра
            await _ModbusMaster.WriteMultipleRegistersAsync(325, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            //  Освобождение семафора.
            _Semaphore.Release();
        }
    }

    /// <summary>
    /// Представляет функцию очистки уровня состояния выхода 4.
    /// </summary>  
    /// <exception cref="IOException">
    /// Не верный протокол обмена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="FormatException">
    /// В modbus header пришел код ошибки.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public async Task ClearDigitalOut4Async(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await _Semaphore.WaitAsync(cancellationToken);
        try
        {
            //  Инициализация массива записи
            ushort[] registers = new ushort[1] { 0x0000 };

            //  Запис регистра
            await _ModbusMaster.WriteMultipleRegistersAsync(325, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            //  Освобождение семафора.
            _Semaphore.Release();
        }
    }

    /// <summary>
    /// Представляет функцию переключения Sim карты
    /// </summary>  
    /// <exception cref="IOException">
    /// Не верный протокол обмена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="FormatException">
    /// В modbus header пришел код ошибки.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public async Task ChangeActiveSimAsync(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await _Semaphore.WaitAsync(cancellationToken);
        try
        {
            //  Инициализация массива записи
            ushort[] registers = new ushort[1] { 0x0000 };

            //  Запис регистра
            await _ModbusMaster.WriteMultipleRegistersAsync(205, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            //  Освобождение семафора.
            _Semaphore.Release();
        }
    }

    /// <summary>
    /// Представляет функцию переключения Sim карты
    /// </summary>  
    /// <exception cref="IOException">
    /// Не верный протокол обмена.
    /// </exception>
    /// <exception cref="EndOfStreamException">
    /// Достигнут конец потока.
    /// </exception>
    /// <exception cref="FormatException">
    /// В modbus header пришел код ошибки.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Получено значение <see cref="ModbusTcpHeader.Length"/> вне допустимого диапозона.
    /// </exception>
    public async Task RebootAsync(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await _Semaphore.WaitAsync(cancellationToken);
        try
        {
            //  Инициализация массива записи
            ushort[] registers = new ushort[1] { 0x0001 };

            //  Запис регистра
            await _ModbusMaster.WriteMultipleRegistersWithoutReadAsync(206, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            //  Освобождение семафора.
            _Semaphore.Release();
        }
    }

    /// <summary>
    /// Обновить информацию из регистров чтения.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await _Semaphore.WaitAsync(cancellationToken);
        try
        {
            //  Чтение первого непрерывного массива регистров
            ushort[] registersPack1 = await _ModbusMaster.ReadMultipleRegistersAsync(1, 118, _ModbusTimeout, cancellationToken).ConfigureAwait(false);

            //  Чтение второго непрерывного массива регистров
            ushort[] registersPack2 = await _ModbusMaster.ReadMultipleRegistersAsync(119, 203 - 119, _ModbusTimeout, cancellationToken).ConfigureAwait(false);

            //  Чтение третьего непрерывного массива регистров
            ushort[] registersPack3 = await _ModbusMaster.ReadMultipleRegistersAsync(205, 1, _ModbusTimeout, cancellationToken).ConfigureAwait(false);

            //  Чтение четвертого непрерывного массива регистров
            ushort[] registersPack4 = await _ModbusMaster.ReadMultipleRegistersAsync(292, 326 - 292, _ModbusTimeout, cancellationToken).ConfigureAwait(false);

            //  Чтение пятого непрерывного массива регистров
            ushort[] registersPack5 = await _ModbusMaster.ReadMultipleRegistersAsync(348, 16, _ModbusTimeout, cancellationToken).ConfigureAwait(false);

            //  Создание потока.
            using MemoryStream memory = new();

            //  Создание писателя
            using BinaryWriter writer = new(memory, Encoding.UTF8, true);

            // Запись первого массива.
            foreach (ushort register in registersPack1)
            {
                writer.Write(register);
            }
            // Запись второго массива.
            foreach (ushort register in registersPack2)
            {
                writer.Write(register);
            }
            // Запись третьего массива.
            foreach (ushort register in registersPack3)
            {
                writer.Write(register);
            }
            // Запись четвертого массива.
            foreach (ushort register in registersPack4)
            {
                writer.Write(register);
            }
            // Запись пятого массива.
            foreach (ushort register in registersPack5)
            {
                writer.Write(register);
            }
            

            //  Сброс позиции.
            memory.Position = 0;

            //  Создание читателя.
            using BinaryReader reader = new(memory, Encoding.UTF8, true);

            //  Чтение системного времени
            SystemUpTime = Reverser.ReverseCore(reader.ReadUInt32());

            //  Чтение уровня сигнала.
            MobileSignalStrength = Reverser.ReverseCore(reader.ReadInt32());

            //  Чтение температуры
            SystemTemperature = Reverser.ReverseCore(reader.ReadInt32());

            //  Чтение системного имени.
            SystemHostName = Encoding.ASCII.GetString(Reverser.Reverse(reader.ReadBytes(_StringTeltonikaLength)));

            //  Чтение имени оператора
            GsmOperatorName = Encoding.ASCII.GetString(Reverser.Reverse(reader.ReadBytes(_StringTeltonikaLength)));

            //  Чтение серийного номера.
            SerialNumber = Encoding.ASCII.GetString(Reverser.Reverse(reader.ReadBytes(_StringTeltonikaLength)));

            //  Чтение MAC адреса
            MacAddress = Encoding.ASCII.GetString(Reverser.Reverse(reader.ReadBytes(_StringTeltonikaLength)));

            //  Чтение имени роутера.
            RouterName = Encoding.ASCII.GetString(Reverser.Reverse(reader.ReadBytes(_StringTeltonikaLength)));

            //  Чтение активной SIM
            ActiveSimCardSlot = Encoding.ASCII.GetString(Reverser.Reverse(reader.ReadBytes(_StringTeltonikaLength)));

            //  Чтение состояние регистрации
            NetworkRegistrationInfo = Encoding.ASCII.GetString(Reverser.Reverse(reader.ReadBytes(_StringTeltonikaLength)));

            //  Чтение типа Gsm
            NetworkType = Encoding.ASCII.GetString(Reverser.Reverse(reader.ReadBytes(_StringTeltonikaLength)));

            //  Чтение состояния входа.
            DigitalInput1 = Reverser.ReverseCore(reader.ReadInt32());

            //  Чтение состояния входа.
            DigitalInput2 = Reverser.ReverseCore(reader.ReadInt32());

            //  Чтение wan ip.
            WanIP = Reverser.ReverseCore(reader.ReadUInt32());

            //  Чтение состояния аналого входа.
            AnalogInput = Reverser.ReverseCore(reader.ReadInt32());

            //  Чтение широты.
            Latitude = Reverser.ReverseCore(reader.ReadSingle());

            //  Чтение долготы.
            Longitude = Reverser.ReverseCore(reader.ReadSingle());

            //  Чтение Unix времени
            UnixTime = Encoding.ASCII.GetString(Reverser.Reverse(reader.ReadBytes(_StringTeltonikaLength)));

            //  Чтение времени
            Time = Encoding.ASCII.GetString(Reverser.Reverse(reader.ReadBytes(_StringTeltonikaLength)));

            //  Чтение скорости
            Speed = Reverser.ReverseCore(reader.ReadInt32());

            //  Чтение количества спутников.
            SateliteCount = Reverser.ReverseCore(reader.ReadInt32());

            //  Чтение точности.
            Accuracy = Reverser.ReverseCore(reader.ReadSingle());

            // Чтение мобильных данных
            DataReceiveTodaySim1 = reader.ReadUInt32();
            DataSentTodaySim1 = reader.ReadUInt32();
            DataReceiveThisWeekSim1 = reader.ReadUInt32();
            DataSentThisWeekSim1 = reader.ReadUInt32();
            DataReceiveThisMonthSim1 = reader.ReadUInt32();
            DataSentThisMonthSim1 = reader.ReadUInt32();
            DataReceive24hSim1 = reader.ReadUInt32();
            DataSent24hSim1 = reader.ReadUInt32();

            //  Чтение состояния колектора
            OpenCollectorStatus = reader.ReadUInt16();

            //  Чтение статуса Relay Output
            RelayOutput = reader.ReadUInt16();

            //  Чтение активной Sim карты
            ActiveSIM = reader.ReadUInt16();

            // Чтение мобильных данных
            DataReceiveLastWeekSim1 = Reverser.ReverseCore(reader.ReadUInt32());
            DataSentLastWeekSim1 = Reverser.ReverseCore(reader.ReadUInt32());
            DataReceiveLastMonthSim1 = Reverser.ReverseCore(reader.ReadUInt32());
            DataSentLastMonthSim1 = Reverser.ReverseCore(reader.ReadUInt32());
            DataReceiveTodaySim2 = Reverser.ReverseCore(reader.ReadUInt32());
            DataSentTodaySim2 = Reverser.ReverseCore(reader.ReadUInt32());
            DataReceiveThisWeekSim2 = Reverser.ReverseCore(reader.ReadUInt32());
            DataSentThisWeekSim2 = Reverser.ReverseCore(reader.ReadUInt32());
            DataReceiveThisMonthSim2 = Reverser.ReverseCore(reader.ReadUInt32());
            DataSentThisMonthSim2 = Reverser.ReverseCore(reader.ReadUInt32());
            DataReceive24hSim2 = Reverser.ReverseCore(reader.ReadUInt32());
            DataSent24hSim2 = Reverser.ReverseCore(reader.ReadUInt32());
            DataReceiveLastWeekSim2 = Reverser.ReverseCore(reader.ReadUInt32());
            DataSentLastWeekSim2 = Reverser.ReverseCore(reader.ReadUInt32());
            DataReceiveLastMonthSim2 = Reverser.ReverseCore(reader.ReadUInt32());
            DataSentLastMonthSim2 = Reverser.ReverseCore(reader.ReadUInt32());

            //  Чтение состояния входа.
            DigitalInput4 = reader.ReadUInt16();

            //  Чтение состояния выхода.
            DigitalOpenCollector4 = reader.ReadUInt16();

            //  Чтение IMSI
            IMSI = Encoding.ASCII.GetString(Reverser.Reverse(reader.ReadBytes(_StringTeltonikaLength)));
        }
        finally
        {
            //  Освобождение семафора.
            _Semaphore.Release();
        }
    }

    /// <summary>
    /// Возвращает строку с данными
    /// </summary>
    /// <returns>
    /// Строка с данными.
    /// </returns>
    public string Print()
    {
        StringBuilder builder = new(2048);

        //  Вывод системного времени
        builder.AppendLine($"{nameof(SystemUpTime)} = {SystemUpTime}");

        //  Вывод уровня сигнала.
        builder.AppendLine($"{nameof(MobileSignalStrength)} = {MobileSignalStrength}");

        //  Вывод температуры
        builder.AppendLine($"{nameof(SystemTemperature)} = {SystemTemperature}");

        //  Вывод температуры
        builder.AppendLine($"{nameof(SystemHostName)} = {SystemHostName}");

        //  Вывод имени оператора
        builder.AppendLine($"{nameof(GsmOperatorName)} = {GsmOperatorName}");

        //  Чтение серийного номера.
        builder.AppendLine($"{nameof(SerialNumber)} = {SerialNumber}");

        //  Чтение MAC адреса
        builder.AppendLine($"{nameof(MacAddress)} = {MacAddress}");

        //  Чтение имени роутера.
        builder.AppendLine($"{nameof(RouterName)} = {RouterName}");

        //  Чтение активной SIM
        builder.AppendLine($"{nameof(ActiveSimCardSlot)} = {ActiveSimCardSlot}");

        //  Чтение состояние регистрации
        builder.AppendLine($"{nameof(NetworkRegistrationInfo)} = {NetworkRegistrationInfo}");

        //  Чтение типа Gsm
        builder.AppendLine($"{nameof(NetworkType)} = {NetworkType}");

        //  Чтение состояния входа.
        builder.AppendLine($"{nameof(DigitalInput1)} = {DigitalInput1}");

        //  Чтение состояния входа.
        builder.AppendLine($"{nameof(DigitalInput2)} = {DigitalInput2}");

        //  Чтение wan ip.
        builder.AppendLine($"{nameof(WanIP)} = {WanIP}");

        //  Чтение состояния аналого входа.
        builder.AppendLine($"{nameof(AnalogInput)} = {AnalogInput}");

        //  Чтение широты.
        builder.AppendLine($"{nameof(Latitude)} = {Latitude}");

        //  Чтение долготы.
        builder.AppendLine($"{nameof(Longitude)} = {Longitude}");

        //  Чтение Unix времени
        builder.AppendLine($"{nameof(UnixTime)} = {UnixTime}");

        //  Чтение времени
        builder.AppendLine($"{nameof(Time)} = {Time}");

        //  Чтение скорости
        builder.AppendLine($"{nameof(Speed)} = {Speed}");

        //  Чтение количества спутников.
        builder.AppendLine($"{nameof(SateliteCount)} = {SateliteCount}");

        //  Чтение точности.
        builder.AppendLine($"{nameof(Accuracy)} = {Accuracy}");

        // Чтение мобильных данных
        builder.AppendLine($"{nameof(DataReceiveTodaySim1)} = {DataReceiveTodaySim1}");
        builder.AppendLine($"{nameof(DataSentTodaySim1)} = {DataSentTodaySim1}");
        builder.AppendLine($"{nameof(DataReceiveThisWeekSim1)} = {DataReceiveThisWeekSim1}");
        builder.AppendLine($"{nameof(DataSentThisWeekSim1)} = {DataSentThisWeekSim1}");
        builder.AppendLine($"{nameof(DataReceiveThisMonthSim1)} = {DataReceiveThisMonthSim1}");
        builder.AppendLine($"{nameof(DataSentThisMonthSim1)} = {DataSentThisMonthSim1}");
        builder.AppendLine($"{nameof(DataReceive24hSim1)} = {DataReceive24hSim1}");
        builder.AppendLine($"{nameof(DataSent24hSim1)} = {DataSent24hSim1}");

        //  Чтение состояния колектора
        builder.AppendLine($"{nameof(OpenCollectorStatus)} = {OpenCollectorStatus}");

        //  Чтение статуса Relay Output
        builder.AppendLine($"{nameof(RelayOutput)} = {RelayOutput}");

        //  Чтение активной Sim карты
        builder.AppendLine($"{nameof(ActiveSIM)} = {ActiveSIM}");

        // Чтение мобильных данных
        builder.AppendLine($"{nameof(DataReceiveLastWeekSim1)} = {DataReceiveLastWeekSim1}");
        builder.AppendLine($"{nameof(DataSentLastWeekSim1)} = {DataSentLastWeekSim1}");
        builder.AppendLine($"{nameof(DataReceiveLastMonthSim1)} = {DataReceiveLastMonthSim1}");
        builder.AppendLine($"{nameof(DataSentLastMonthSim1)} = {DataSentLastMonthSim1}");
        builder.AppendLine($"{nameof(DataReceiveTodaySim2)} = {DataReceiveTodaySim2}");
        builder.AppendLine($"{nameof(DataSentTodaySim2)} = {DataSentTodaySim2}");
        builder.AppendLine($"{nameof(DataReceiveThisWeekSim2)} = {DataReceiveThisWeekSim2}");
        builder.AppendLine($"{nameof(DataSentThisWeekSim2)} = {DataSentThisWeekSim2}");
        builder.AppendLine($"{nameof(DataReceiveThisMonthSim2)} = {DataReceiveThisMonthSim2}");
        builder.AppendLine($"{nameof(DataSentThisMonthSim2)} = {DataSentThisMonthSim2}");
        builder.AppendLine($"{nameof(DataReceive24hSim2)} = {DataReceive24hSim2}");
        builder.AppendLine($"{nameof(DataSent24hSim2)} = {DataSent24hSim2}");
        builder.AppendLine($"{nameof(DataReceiveLastWeekSim2)} = {DataReceiveLastWeekSim2}");
        builder.AppendLine($"{nameof(DataSentLastWeekSim2)} = {DataSentLastWeekSim2}");
        builder.AppendLine($"{nameof(DataReceiveLastMonthSim2)} = {DataReceiveLastMonthSim2}");
        builder.AppendLine($"{nameof(DataSentLastMonthSim2)} = {DataSentLastMonthSim2}");


        //  Чтение состояния входа.
        builder.AppendLine($"{nameof(DigitalInput4)} = {DigitalInput4}");

        //  Чтение состояния выхода.
        builder.AppendLine($"{nameof(DigitalOpenCollector4)} = {DigitalOpenCollector4}");

        //  Чтение IMSI
        builder.AppendLine($"{nameof(IMSI)} = {IMSI}");

        //  Возврат результата.
        return builder.ToString();
    }
}
