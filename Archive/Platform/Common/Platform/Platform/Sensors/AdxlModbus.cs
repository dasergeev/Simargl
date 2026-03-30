using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Apeiron.Platform.Modbus;

namespace Apeiron.Platform.Sensors;

/// <summary>
/// Представляет класс настройки датчика.
/// </summary>
public class AdxlModbus :
    INotifyPropertyChanged
{
    private readonly SemaphoreSlim semaphore = new(1, 1);




    /// <summary>
    /// Интервал на отмену операции чтения в мс.
    /// </summary>
    private const ushort _ModbusTimeout = 5000;

    /// <summary>
    /// Поле - драйвер комуникации Modbus Tcp.
    /// </summary>
    internal ModbuTcpMaster ModbusMaster { get; private set; }

    /// <summary>
    /// Происходит при изменении значения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

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
    /// Поле для хранения серийного номера.
    /// </summary>
    private long _SerialNumber;

    /// <summary>
    /// Поле для хранения значения, определяющего использует ли датчик протокол DHCP.
    /// </summary>
    private bool _UseDhcp;

    /// <summary>
    /// Поле для хранения UDP-порта для подключения к датчику.
    /// </summary>
    private int _UdpPort;

    /// <summary>
    /// Поле для хранения TCP-порта для подключения к датчику.
    /// </summary>
    private int _TcpPort;

    /// <summary>
    /// Поле для хранения диапазона измерения.
    /// </summary>
    private int _MeasuringRange;

    /// <summary>
    /// Поле для хранения значения, определяющего уровень фильтра высоких частот.
    /// </summary>
    private int _HighPassFilter;

    /// <summary>
    /// Поле для хранения частоты дискретизации.
    /// </summary>
    private int _Sampling;

    /// <summary>
    /// Поле для хранения смещения сигнала по оси Ox.
    /// </summary>
    private double _XOffset;

    /// <summary>
    /// Поле для хранения смещения сигнала по оси Oy.
    /// </summary>
    private double _YOffset;

    /// <summary>
    /// Поле для хранения смещения сигнала по оси Oz.
    /// </summary>
    private double _ZOffset;

    /// <summary>
    /// Поле для хранения IP-адреса датчика.
    /// </summary>
    private IPAddress _Address;

    /// <summary>
    /// Поле для хранения маски подсети.
    /// </summary>
    private IPAddress _SubnetMask;

    /// <summary>
    /// Поле для хранения сетевого шлюза.
    /// </summary>
    private IPAddress _Gateway;

    /// <summary>
    /// Поле для хранения адреса сервера.
    /// </summary>
    private IPAddress _ServerAddress;

    /// <summary>
    /// Поле для хранения максимального значения температуры.
    /// </summary>
    private double _MaxTemperature;

    /// <summary>
    /// Поле для хранения минимального значения температуры.
    /// </summary>
    private double _MinTemperature;

    /// <summary>
    /// Поле для хранения максимального значения напряжения питания.
    /// </summary>
    private double _MaxVoltage;

    /// <summary>
    /// Поле для хранения минимального значения напряжения питания.
    /// </summary>
    private double _MinVoltage;

    /// <summary>
    /// Поле для хранения значения самодиагностики.
    /// </summary>
    private double _DiagnosticValue;

    /// <summary>
    /// Поле для хранения значения, определяющего информацию об ошибках.
    /// </summary>
    private long _ErrorCodes;

    /// <summary>
    /// Поле для хранения значения, определяющего состояние датчика.
    /// </summary>
    private int _State;



    /// <summary>
    /// Возвращает тип датчика.
    /// </summary>
    public string SensorType
    {
        get => _SensorType;
        private set
        {
            //  Проверка ссылки на новое значение.
            value = Check.IsNotNull(value, nameof(SensorType));

            //  Проверка изменения значения.
            if (_SensorType != value)
            {
                //  Установка нового значения.
                _SensorType = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SensorType)));
            }
        }
    }

    /// <summary>
    /// Возвращает версию прошивки.
    /// </summary>
    public string FirmwareVersion
    {
        get => _FirmwareVersion;
        private set
        {
            //  Проверка ссылки на новое значение.
            value = Check.IsNotNull(value, nameof(FirmwareVersion));

            //  Проверка изменения значения.
            if (_FirmwareVersion != value)
            {
                //  Установка нового значения.
                _FirmwareVersion = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(FirmwareVersion)));
            }
        }
    }

    /// <summary>
    /// Возвращает дату изготовления прошивки.
    /// </summary>
    public string FirmwareDate
    {
        get => _FirmwareDate;
        private set
        {
            //  Проверка ссылки на новое значение.
            value = Check.IsNotNull(value, nameof(FirmwareDate));

            //  Проверка изменения значения.
            if (_FirmwareDate != value)
            {
                //  Установка нового значения.
                _FirmwareDate = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(FirmwareDate)));
            }
        }
    }

    /// <summary>
    /// Возвращает серийный номер.
    /// </summary>
    public long SerialNumber
    {
        get => (((_SerialNumber & 0xFFFF)<<16) + ((_SerialNumber>>16)&0xFFFF));
        private set
        {
            //  Проверка нового значения.
            Check.IsNotNegative(value, nameof(SerialNumber));
            Check.IsNotLarger(value, uint.MaxValue, nameof(SerialNumber));

            //  Проверка изменения значения.
            if (_SerialNumber != value)
            {
                //  Установка нового значения.
                _SerialNumber = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SerialNumber)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее использует ли датчик протокол DHCP.
    /// </summary>
    public bool UseDhcp
    {
        get => _UseDhcp;
        set
        {
            //  Проверка изменения значения.
            if (_UseDhcp != value)
            {
                //  Установка нового значения.
                _UseDhcp = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(UseDhcp)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт UDP-порт для подключения к датчику.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое меньше значения <see cref="IPEndPoint.MinPort"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое превышает значение <see cref="IPEndPoint.MaxPort"/>.
    /// </exception>
    public int UdpPort
    {
        get => _UdpPort;
        set
        {
            //  Проверка значения.
            Check.IsNotLess(value, IPEndPoint.MinPort, nameof(UdpPort));
            Check.IsNotLarger(value, IPEndPoint.MaxPort, nameof(UdpPort));

            //  Проверка изменения значения.
            if (_UdpPort != value)
            {
                //  Установка нового значения.
                _UdpPort = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(UdpPort)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт TCP-порт для подключения к датчику.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое меньше значения <see cref="IPEndPoint.MinPort"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое превышает значение <see cref="IPEndPoint.MaxPort"/>.
    /// </exception>
    public int TcpPort
    {
        get => _TcpPort;
        set
        {
            //  Проверка значения.
            Check.IsNotLess(value, IPEndPoint.MinPort, nameof(TcpPort));
            Check.IsNotLarger(value, IPEndPoint.MaxPort, nameof(TcpPort));

            //  Проверка изменения значения.
            if (_TcpPort != value)
            {
                //  Установка нового значения.
                _TcpPort = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(TcpPort)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт диапазон измерения.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Значение вне диапазона.
    /// </exception>
    public int MeasuringRange
    {
        get => _MeasuringRange;
        set
        {
            //  Проверка значения.
            if (value != 10 && value != 20 && value != 40)
            {
                throw Exceptions.ArgumentOutOfRange(nameof(MeasuringRange));
            }

            //  Проверка изменения значения.
            if (_MeasuringRange != value)
            {
                //  Установка нового значения.
                _MeasuringRange = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(MeasuringRange)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее уровень фильтра высоких частот.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано значение, которое превышает значение 6.
    /// </exception>
    public int HighPassFilter
    {
        get => _HighPassFilter;
        set
        {
            //  Проверка значения.
            Check.IsNotNegative(value, nameof(HighPassFilter));
            Check.IsNotLarger(value, 6, nameof(HighPassFilter));

            //  Проверка изменения значения.
            if (_HighPassFilter != value)
            {
                //  Установка нового значения.
                _HighPassFilter = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(HighPassFilter)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано недопустипое значение.
    /// </exception>
    public int Sampling
    {
        get => _Sampling;
        set
        {
            //  Проверка значения.
            if (value != 125 && value != 250 && value != 500 && value != 1000 && value != 2000 && value != 4000)
            {
                throw Exceptions.ArgumentOutOfRange(nameof(MeasuringRange));
            }

            //  Проверка изменения значения.
            if (_Sampling != value)
            {
                //  Установка нового значения.
                _Sampling = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Sampling)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт смещение сигнала по оси Ox.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    public double XOffset
    {
        get => _XOffset;
        set
        {
            //  Проверка значения.
            Check.IsNotNaN(value, nameof(XOffset));
            Check.IsNotInfinity(value, nameof(XOffset));

            //  Проверка изменения значения.
            if (_XOffset != value)
            {
                //  Установка нового значения.
                _XOffset = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(XOffset)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт смещение сигнала по оси Oy.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    public double YOffset
    {
        get => _YOffset;
        set
        {
            //  Проверка значения.
            Check.IsNotNaN(value, nameof(YOffset));
            Check.IsNotInfinity(value, nameof(YOffset));

            //  Проверка изменения значения.
            if (_YOffset != value)
            {
                //  Установка нового значения.
                _YOffset = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(YOffset)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт смещение сигнала по оси Oz.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    public double ZOffset
    {
        get => _ZOffset;
        set
        {
            //  Проверка значения.
            Check.IsNotNaN(value, nameof(ZOffset));
            Check.IsNotInfinity(value, nameof(ZOffset));

            //  Проверка изменения значения.
            if (_ZOffset != value)
            {
                //  Установка нового значения.
                _ZOffset = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ZOffset)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт IP-адрес датчика.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пуста ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Переданный адрес не является IPv4-адресом.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Переданный адрес является замыканием на себя.
    /// </exception>
    public IPAddress Address
    {
        get => _Address;
        set
        {
            //  Проверка ссылки на значения.
            value = Check.IsNotNull(value, nameof(Address));

            //  Проверка схемы адресации.
            if (value.AddressFamily != AddressFamily.InterNetwork)
            {
                //  Не является IPv4-адресом.
                throw Exceptions.ArgumentOutOfRange(nameof(Address));
            }

            //  Проверка замыкания на себя.
            if (IPAddress.IsLoopback(value))
            {
                //  Является IP-адресом замыкания на себя.
                throw Exceptions.ArgumentOutOfRange(nameof(Address));
            }

            //  Проверка изменения значения.
            if (_Address.Equals(value) == false)
            {
                //  Установка нового значения.
                _Address = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Address)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт маску подсети.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пуста ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Переданный адрес не является IPv4-адресом.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Переданный адрес является замыканием на себя.
    /// </exception>
    public IPAddress SubnetMask
    {
        get => _SubnetMask;
        set
        {
            //  Проверка ссылки на значения.
            value = Check.IsNotNull(value, nameof(SubnetMask));

            //  Проверка схемы адресации.
            if (value.AddressFamily != AddressFamily.InterNetwork)
            {
                //  Не является IPv4-адресом.
                throw Exceptions.ArgumentOutOfRange(nameof(SubnetMask));
            }

            //  Проверка замыкания на себя.
            if (IPAddress.IsLoopback(value))
            {
                //  Является IP-адресом замыкания на себя.
                throw Exceptions.ArgumentOutOfRange(nameof(SubnetMask));
            }

            //  Проверка изменения значения.
            if (_SubnetMask.Equals(value) == false)
            {
                //  Установка нового значения.
                _SubnetMask = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SubnetMask)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт сетевой шлюз.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пуста ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Переданный адрес не является IPv4-адресом.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Переданный адрес является замыканием на себя.
    /// </exception>
    public IPAddress Gateway
    {
        get => _Gateway;
        set
        {
            //  Проверка ссылки на значения.
            value = Check.IsNotNull(value, nameof(Gateway));

            //  Проверка схемы адресации.
            if (value.AddressFamily != AddressFamily.InterNetwork)
            {
                //  Не является IPv4-адресом.
                throw Exceptions.ArgumentOutOfRange(nameof(Gateway));
            }

            //  Проверка замыкания на себя.
            if (IPAddress.IsLoopback(value))
            {
                //  Является IP-адресом замыкания на себя.
                throw Exceptions.ArgumentOutOfRange(nameof(Gateway));
            }

            //  Проверка изменения значения.
            if (_Gateway.Equals(value) == false)
            {
                //  Установка нового значения.
                _Gateway = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Gateway)));
            }
        }
    }

    /// <summary>
    /// Возвращает или задаёт адрес сервера.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пуста ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Переданный адрес не является IPv4-адресом.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Переданный адрес является замыканием на себя.
    /// </exception>
    public IPAddress ServerAddress
    {
        get => _ServerAddress;
        set
        {
            //  Проверка ссылки на значения.
            value = Check.IsNotNull(value, nameof(ServerAddress));

            //  Проверка схемы адресации.
            if (value.AddressFamily != AddressFamily.InterNetwork)
            {
                //  Не является IPv4-адресом.
                throw Exceptions.ArgumentOutOfRange(nameof(ServerAddress));
            }

            //  Проверка замыкания на себя.
            if (IPAddress.IsLoopback(value))
            {
                //  Является IP-адресом замыкания на себя.
                throw Exceptions.ArgumentOutOfRange(nameof(ServerAddress));
            }

            //  Проверка изменения значения.
            if (_ServerAddress.Equals(value) == false)
            {
                //  Установка нового значения.
                _ServerAddress = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ServerAddress)));
            }
        }
    }

    /// <summary>
    /// Возвращает максимальное значение температуры.
    /// </summary>
    public double MaxTemperature
    {
        get => _MaxTemperature;
        private set
        {
            //  Проверка значения.
            Check.IsNotNaN(value, nameof(MaxTemperature));
            Check.IsNotInfinity(value, nameof(MaxTemperature));

            //  Проверка изменения значения.
            if (_MaxTemperature != value)
            {
                //  Установка нового значения.
                _MaxTemperature = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(MaxTemperature)));
            }
        }
    }
    
    /// <summary>
    /// Возвращает минимальное значение температуры.
    /// </summary>
    public double MinTemperature
    {
        get => _MinTemperature;
        private set
        {
            //  Проверка значения.
            Check.IsNotNaN(value, nameof(MinTemperature));
            Check.IsNotInfinity(value, nameof(MinTemperature));

            //  Проверка изменения значения.
            if (_MinTemperature != value)
            {
                //  Установка нового значения.
                _MinTemperature = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(MinTemperature)));
            }
        }
    }

    /// <summary>
    /// Возвращает максимальное значение напряжения питания.
    /// </summary>
    public double MaxVoltage
    {
        get => _MaxVoltage;
        private set
        {
            //  Проверка значения.
            Check.IsNotNaN(value, nameof(MaxVoltage));
            Check.IsNotInfinity(value, nameof(MaxVoltage));

            //  Проверка изменения значения.
            if (_MaxVoltage != value)
            {
                //  Установка нового значения.
                _MaxVoltage = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(MaxVoltage)));
            }
        }
    }

    /// <summary>
    /// Возвращает минимальное значение напряжения питания.
    /// </summary>
    public double MinVoltage
    {
        get => _MinVoltage;
        private set
        {
            //  Проверка значения.
            Check.IsNotNaN(value, nameof(MinVoltage));
            Check.IsNotInfinity(value, nameof(MinVoltage));

            //  Проверка изменения значения.
            if (_MinVoltage != value)
            {
                //  Установка нового значения.
                _MinVoltage = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(MinVoltage)));
            }
        }
    }

    /// <summary>
    /// Возвращает значение самодиагностики.
    /// </summary>
    public double DiagnosticValue
    {
        get => _DiagnosticValue;
        private set
        {
            //  Проверка значения.
            Check.IsNotNaN(value, nameof(DiagnosticValue));
            Check.IsNotInfinity(value, nameof(DiagnosticValue));

            //  Проверка изменения значения.
            if (_DiagnosticValue != value)
            {
                //  Установка нового значения.
                _DiagnosticValue = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(DiagnosticValue)));
            }
        }
    }

    /// <summary>
    /// Возвращает значение, определяющее информацию об ошибках.
    /// </summary>
    public long ErrorCodes
    {
        get => _ErrorCodes;
        private set
        {
            //  Проверка значения.
            Check.IsNotNegative(value, nameof(ErrorCodes));
            Check.IsNotLarger(value, uint.MaxValue, nameof(ErrorCodes));

            //  Проверка изменения значения.
            if (_ErrorCodes != value)
            {
                //  Установка нового значения.
                _ErrorCodes = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ErrorCodes)));
            }
        }
    }

    /// <summary>
    /// Возвращает значение, определяющее состояние датчика.
    /// </summary>
    public int State
    {
        get => _State;
        set
        {
            //  Проверка значения.
            Check.IsNotNegative(value, nameof(State));
            Check.IsNotLarger(value, ushort.MaxValue, nameof(State));

            //  Проверка изменения значения.
            if (_State != value)
            {
                //  Установка нового значения.
                _State = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(State)));
            }
        }
    }



    /// <summary>
    /// Представляет IP текущей сессии до перезагрузки.
    /// </summary>
    internal IPAddress SessionIP;

    /// <summary>
    /// Представляет порт текущей сессии до перезагрузки.
    /// </summary>
    internal int ModbusPort;

    /// <summary>
    /// Представляет конструктор объекта.
    /// </summary>
    /// <param name="address">
    /// IP адрес датчика
    /// </param>
    /// <param name="port">
    /// Порт TCP сервера датчика.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="address"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Недопустимое значение в параметре <paramref name="port"/>.
    /// </exception>
    public AdxlModbus(IPAddress address, int port)
    {
        //  Проверка ссылки на адресс
        Check.IsNotNull(address, nameof(address));

        //  Проверка минимального значения порта
        Check.IsNotLess(port, ushort.MinValue, nameof(port));

        //  Проверка максимального значения порта.
        Check.IsNotLarger(port, ushort.MaxValue, nameof(port));

        //  Создание интерфейса Modbus
        ModbusMaster = new(address, port, 0x01);

        //  Установка значения адреса.
        _Address = address;

        //  Установка значения адреса
        SessionIP = address;

        //  Установка значения порта
        ModbusPort = port;

        //  Установка значения маски
        _SubnetMask = IPAddress.None;

        //  Установка значения шлюза
        _Gateway = IPAddress.None;

        //  Установка значения адреса сервера.
        _ServerAddress = IPAddress.None;
    }


    /// <summary>
    /// Представляет функцию остановки измерений
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Удаленный узел недоступен.
    /// </exception>
    /// <exception cref="IOException">
    /// Ошибка при выполнении запроса.
    /// </exception>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await semaphore.WaitAsync(cancellationToken);

        try
        {
            //  Проверка токена отмены.
            if (cancellationToken.IsCancellationRequested)
            {
                //  Возврат из функции.
                return;
            }
            //  Инициализация массива записи
            ushort[] registers = new ushort[1] { 0x0000 };

            //  Запис регистра
            await ModbusMaster.WriteMultipleRegistersWithoutReadAsync(1050, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            semaphore.Release();
        }
    }

    /// <summary>
    /// Представляет функцию старта измерений.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Удаленный узел недоступен.
    /// </exception>
    /// <exception cref="IOException">
    /// Ошибка при выполнении запроса.
    /// </exception>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await semaphore.WaitAsync(cancellationToken);

        try
        {
            //  Проверка токена отмены.
            if (cancellationToken.IsCancellationRequested)
            {
                //  Возврат из функции.
                return;
            }
            //  Инициализация массива записи
            ushort[] registers = new ushort[1] { 0x0001 };

            //  Запис регистра
            await ModbusMaster.WriteMultipleRegistersWithoutReadAsync(1050, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            semaphore.Release();
        }
    }

    /// <summary>
    /// Представляет перезагрузки датчика.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Удаленный узел недоступен.
    /// </exception>
    public async Task ResetAsync(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await semaphore.WaitAsync(cancellationToken);

        try
        {
            //  Проверка токена отмены.
            if (cancellationToken.IsCancellationRequested)
            {
                //  Возврат из функции.
                return;
            }

            //  Инициализация массива записи
            ushort[] registers = new ushort[1] { 0xA0A0 };

            //  Запис регистра
            await ModbusMaster.WriteMultipleRegistersWithoutReadAsync(1050, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);

            //  Создание интерфейса Modbus
            ModbusMaster = new(Address, ModbusPort, 0x01);

            //  Установка нового IP
            SessionIP = Address;
        }
        finally
        {
            //  Освобождение семафора.
            semaphore.Release();
        }
    }

    /// <summary>
    /// Представляет cброс значения напряжения.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Удаленный узел недоступен.
    /// </exception>
    /// <exception cref="IOException">
    /// Ошибка при выполнении запроса.
    /// </exception>
    public async Task ClearVoltageAsync(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await semaphore.WaitAsync(cancellationToken);

        try
        {
            //  Проверка токена отмены.
            if (cancellationToken.IsCancellationRequested)
            {
                //  Возврат из функции.
                return;
            }
            //  Инициализация массива записи
            ushort[] registers = new ushort[2] { 0x0000,0x0000 };

            //  Запис регистра
            await ModbusMaster.WriteMultipleRegistersAsync(1042, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            semaphore.Release();
        }
    }

    /// <summary>
    /// Представляет cброс значения температуры.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Удаленный узел недоступен.
    /// </exception>
    /// <exception cref="IOException">
    /// Ошибка при выполнении запроса.
    /// </exception>
    public async Task ClearTempAsync(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await semaphore.WaitAsync(cancellationToken);

        try
        {
            //  Проверка токена отмены.
            if (cancellationToken.IsCancellationRequested)
            {
                //  Возврат из функции.
                return;
            }
            //  Инициализация массива записи
            ushort[] registers = new ushort[2] { 0x0000, 0x0000 };

            //  Запис регистра
            await ModbusMaster.WriteMultipleRegistersAsync(1038, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);

        }
        finally
        {
            semaphore.Release();
        }
    }


    /// <summary>
    /// Представляет сброс значений ошибки.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Удаленный узел недоступен.
    /// </exception>
    public async Task ClearErrorAsync(CancellationToken cancellationToken)
    {
        //  Ожидание семафора.
        await semaphore.WaitAsync(cancellationToken);

        try
        {
            //  Проверка токена отмены.
            if (cancellationToken.IsCancellationRequested)
            {
                //  Возврат из функции.
                return;
            }
            //  Инициализация массива записи
            ushort[] registers = new ushort[2] { 0x0000, 0x0000 };

            //  Запис регистра
            await ModbusMaster.WriteMultipleRegistersAsync(1048, registers, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            semaphore.Release();
        }
    }

    /// <summary>
    /// Представляет функцию чтения всех регистров датчика.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// К удаленному узлу не проходит ping.
    /// </exception>
    /// <exception cref="SocketException">
    /// Ошибка при доступе к сокету.
    /// </exception>
    public async Task LoadAsync(CancellationToken cancellationToken)
    {

        //  Ожидание семафора.
        await semaphore.WaitAsync(cancellationToken);

        try
        {
            //  Проверка токена отмены.
            if (cancellationToken.IsCancellationRequested)
            {
                //  Возврат из функции.
                return;
            }
            //  Чтение непрерывного массива регистров
            ushort[] registersPack = await ModbusMaster.ReadMultipleRegistersAsync(1000, 51, _ModbusTimeout, cancellationToken).ConfigureAwait(false);

            //  Создание потока.
            using MemoryStream memory = new();

            //  Создание писателя
            using BinaryWriter writer = new(memory, Encoding.UTF8, true);

            // Запись первого массива.
            foreach (ushort register in registersPack)
            {
                writer.Write(register);
            }

            //  Сброс позиции.
            memory.Position = 0;

            //  Создание читателя.
            using BinaryReader reader = new(memory, Encoding.UTF8, true);

            // Чтение типа датчика.
            SensorType = Encoding.ASCII.GetString(reader.ReadBytes(10));

            //  Чтение версии прошивки.
            FirmwareVersion = Encoding.ASCII.GetString(reader.ReadBytes(12));

            //  Чтение даты изготовления.
            FirmwareDate = Encoding.ASCII.GetString(reader.ReadBytes(10));

            //  Чтение серийного номера.
            SerialNumber = reader.ReadUInt32();

            //  Чтение флага DHCP.
            UseDhcp = (reader.ReadUInt16() != 0x0000);

            //  Чтение UDP порта.
            UdpPort = reader.ReadUInt16();

            //  Чтение TCP порта.
            TcpPort = reader.ReadUInt16();

            //  Чтение диапазона измерения.
            MeasuringRange = reader.ReadUInt16();

            //  Чтение значения фильтра.
            HighPassFilter = reader.ReadUInt16();

            //  Чтение значение частоты дискретизации.
            Sampling = reader.ReadUInt16();

            //  Чтение смещения X.
            XOffset = reader.ReadSingle();

            //  Чтение смещения Y.
            YOffset = reader.ReadSingle();

            //  Чтение смещения Z.
            ZOffset = reader.ReadSingle();

            //  Чтение IP адреса.
            Address = new IPAddress(reader.ReadUInt32());

            //  Чтение Маски сети.
            SubnetMask = new IPAddress(reader.ReadUInt32());

            //  Чтение шлюза.
            Gateway = new IPAddress(reader.ReadUInt32());

            //  Чтение IP адреса сервера.
            ServerAddress = new IPAddress(reader.ReadUInt32());

            //  Чтение максимальной температуры
            MaxTemperature = reader.ReadSingle();

            //  Чтение минимальной температуры
            MinTemperature = reader.ReadSingle();

            //  Чтение максимального напряжения питания
            MaxVoltage = reader.ReadSingle();

            //  Чтение минимального напряжения питания
            MinVoltage = reader.ReadSingle();

            //  Чтение диагностического значения
            DiagnosticValue = reader.ReadSingle();

            //  Чтение ошибок
            ErrorCodes = reader.ReadUInt32();

            //  Чтение состояния.
            State = reader.ReadUInt16();
        }
        finally
        {
            //  Освобождение семафора.
            semaphore.Release();
        }
    }

    /// <summary>
    /// Представляет функцию чтения всех регистров датчика.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// К удаленному узлу не проходит ping.
    /// </exception>
    /// <exception cref="SocketException">
    /// Ошибка при доступе к сокету.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <see cref="MeasuringRange"/> меньше <see cref="XOffset"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <see cref="MeasuringRange"/> меньше <see cref="YOffset"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <see cref="MeasuringRange"/> меньше <see cref="ZOffset"/>.
    /// </exception>
    public async Task UpdateAsync(CancellationToken cancellationToken)
    {
        //  Ожидание семафора
        await semaphore.WaitAsync(cancellationToken);

        try
        {

            //  Проверка токена отмены.
            if (cancellationToken.IsCancellationRequested)
            {
                //  Возврат из функции.
                return;
            }

            //  Проверка допустимости связанных значений
            if ((float)MeasuringRange < XOffset || (float)MeasuringRange < YOffset || (float)MeasuringRange < ZOffset)
            {
                throw Exceptions.ArgumentOutOfRange(nameof(MeasuringRange));
            }

            //  Создание потока
            using var memory = new MemoryStream();

            //  Создание писателя
            using var writer = new BinaryWriter(memory, Encoding.ASCII, true);

            //  Проверка использования DHCP
            if (UseDhcp)
            {
                //  Запись значения
                writer.Write((ushort)0x0001);
            }
            else
            {
                //  Запись значения
                writer.Write((ushort)0x0000);
            }

            //  Запись UDP порта
            writer.Write((ushort)UdpPort);
            
            //  Запись TCP порта
            writer.Write((ushort)TcpPort);

            //  Запись диапазона измерения
            writer.Write((ushort)MeasuringRange);

            //  Запись значения фильтра
            writer.Write((ushort)HighPassFilter);

            //  Запись частоты дискретизации
            writer.Write((ushort)Sampling);

            //  Запись смещения X
            writer.Write((float)XOffset);
            
            //  Запись смещения Y
            writer.Write((float)YOffset);

            //  Запись смещения Z
            writer.Write((float)ZOffset);

            //  Запись IP адреса.
            writer.Write(Address.GetAddressBytes());

            //  Запись маски сети
            writer.Write(SubnetMask.GetAddressBytes());

            //  Запись шлюза
            writer.Write(Gateway.GetAddressBytes());
            
            //  Запись IP адреса сервера.
            writer.Write(ServerAddress.GetAddressBytes());


            //  Создание буфера
            ushort[] data = new ushort[memory.Length / 2];

            //  Копирование данных в буфер.
            Buffer.BlockCopy(memory.ToArray(),0,data,0,(int)memory.Length);

            //  Запис регистров
            await ModbusMaster.WriteMultipleRegistersAsync(1018, data, _ModbusTimeout, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            //  Освобождение семафора.
            semaphore.Release();
        }


        

    }

    /// <summary>
    /// Вызывает событие <see cref="PropertyChanged"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        //  Вызов обработчиков события.
        PropertyChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Возвращает значение свойства
    /// </summary>
    /// <param name="property">
    /// Имя свойства.
    /// </param>
    /// <returns>
    /// Возвращает значение.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="property"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Ошибка получения свойства или значения свойства.
    /// </exception>
    public string GetValue(string property)
    {
        //  Проверка что передана не пустая ссылка.
        Check.IsNotNull(property, nameof(property));

        try
        {
            //  Получение типа.
            Type type = typeof(AdxlModbus);

            //  Получение свойства.
            PropertyInfo? info = type.GetProperty(property);

            //  Проверка ссылки.
            info = Check.IsNotNull(info, nameof(property));

            //  Получение значения.
            var value = info.GetValue(this);

            //  Проверка ссылки.
            value = Check.IsNotNull(value, nameof(property)).ToString();

            //  Проверка результата.
            string result = Check.IsNotNull(value!.ToString(), nameof(property));

            //  Возврат результата.
            return result;
        }
        catch (Exception ex)
        {
            //  Проверка исключения.
            if (ex.IsSystem())
            {
                //  Выброс исключения
                throw;
            }

            //  Выброс исключения.
            throw new ArgumentException("Ошибка получения свойства или получения значения.", ex);
        }

    }


    /// <summary>
    /// Устанавливает значение свойства.
    /// </summary>
    /// <param name="property">
    /// Имя свойства.
    /// </param>
    /// <param name="value">
    /// Новое значение свойства.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="property"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Ошибка получения свойства или значения свойства.
    /// </exception>
    public void SetValue(string property, string value)
    {
        //  Проверка что передана не пустая ссылка.
        Check.IsNotNull(property, nameof(property));

        try
        {
            //  Получение типа.
            Type type = typeof(AdxlModbus);

            //  Получение свойства.
            PropertyInfo? info = type.GetProperty(property);

            //  Проверка ссылки.
            info = Check.IsNotNull(info, nameof(property));

            //  Проверка, что тип значения IP адресс
            if (info.PropertyType == typeof(IPAddress))
            {
                //  Установка значения.
                info.SetValue(this, IPAddress.Parse(value));
            }
            else
            {
                //  Получение конвертера.
                var converter = TypeDescriptor.GetConverter(info.PropertyType);

                //  Получение значения.
                var result = converter.ConvertFrom(value);

                //  Установка значения.
                info.SetValue(this, result);
            }
        }
        catch (Exception ex)
        {
            //  Проверка исключения.
            if (ex.IsSystem())
            {
                //  Выброс исключения
                throw;
            }

            //  Выброс исключения.
            throw new ArgumentException("Ошибка получения свойства или установки значения.", ex);
        }

    }
}
