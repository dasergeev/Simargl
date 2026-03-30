using System.Net;

namespace Apeiron.Platform.Teltonika;

/// <summary>
/// Представляет класс управления устройства Teltonika.
/// </summary>
public sealed class Teltonika :
    IDisposable
{
    /// <summary>
    /// Представляет объект синхронизации класса.
    /// </summary>
    private readonly object _SyncRoot = new();

    /// <summary>
    /// Представляет флаг выполнения освобождения ресурсов.
    /// </summary>
    private bool _Disposed;

    /// <summary>
    /// Представляет адрес по умолчанию для Modbus интерфейса.
    /// </summary>
    public const int ModbusAddress = 1;

    /// <summary>
    /// Представляет порт по умолчанию для Modbus интерфейса.
    /// </summary>
    public const int ModbusPort = 502;

    /// <summary>
    /// Представляет порт по умолчанию для SSH.
    /// </summary>
    public const int SshPort = 22;

    /// <summary>
    /// Представляет поле адреса Teltonika.
    /// </summary>
    private readonly IPAddress _IP;

    /// <summary>
    /// Представляет список командных интерфейсов доступных для управления Teltonika.
    /// </summary>
    private readonly List<ICommandLine> _ComamandLineInterfaces = new();

    /// <summary>
    /// Представляет интерфейс управления процессами Teltonika.
    /// </summary>
    private readonly ProcessManager _ProcessInterface;

    /// <summary>
    /// Представляет интерфейс низкого уровня модема.
    /// </summary>
    private readonly ModemLowLevel _ModemLowLevelInterface;

    /// <summary>
    /// Представляет интерфейс взаимодействия Modbus.
    /// </summary>
    private readonly ModbusManager _ModbusInterface;

    /// <summary>
    /// Представляет интерфейс получения геолокационных данных.
    /// </summary>
    public GeolocationManager GeolocationInterface { get; private set; }

    /// <summary>
    /// Представляет интерфейс обращению к модему.
    /// </summary>
    public ModemManager ModemInterface { get; private set; }

    /// <summary>
    /// Инициализирует объект
    /// </summary>
    /// <param name="address">
    /// IP тельтоники.
    /// </param>
    /// <param name="nmeaPort">
    /// Порт получения Nmea.
    /// </param>
    /// <param name="geolocationPort">
    /// Порт получения GeolocationInfo.
    /// </param>
    /// <param name="commandLines">
    /// Интерфейсы командной строки.
    /// </param>  
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="nmeaPort"/> передано не допустимое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="geolocationPort"/> передано не допустимое значение.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="address"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="commandLines"/> передана пустая ссылка.
    /// </exception>
    public Teltonika(IPAddress address, int nmeaPort, int geolocationPort, ICommandLine[] commandLines)
    {
        //  Проверка порта.
        Check.IsNotLess(nmeaPort, 0, nameof(nmeaPort));
        //  Проверка порта.
        Check.IsNotLarger(nmeaPort, ushort.MaxValue, nameof(nmeaPort));
        //  Проверка порта.
        Check.IsNotLess(geolocationPort, 0, nameof(geolocationPort));
        //  Проверка порта.
        Check.IsNotLarger(geolocationPort, ushort.MaxValue, nameof(geolocationPort));

        //  Установка и проверка IP адреса Teltonika
        _IP = Check.IsNotNull(address, nameof(address));

        //  Создание объект Modbus интерфейса.
        _ModbusInterface = new(_IP, ModbusPort, ModbusAddress);

        //  Сохранение и проверка коммандных интерфейсов
        _ComamandLineInterfaces.AddRange(Check.IsNotNull(commandLines, nameof(commandLines)));

        //  Создание объекта низкого уровня модема.
        _ModemLowLevelInterface = new(_ComamandLineInterfaces.ToArray());

        //  Создание интерфейса управления процессами.
        _ProcessInterface = new(_ComamandLineInterfaces.ToArray());

        //  Создание объекта управления модемом.
        ModemInterface = new ModemManager(_ModbusInterface, _ModemLowLevelInterface, _ProcessInterface);

        //  Создание объекта получения данных геолокации.
        GeolocationInterface = new(_ModbusInterface, ModemInterface, _ProcessInterface, nmeaPort, geolocationPort);
    }

    /// <summary>
    /// Представляет метод освобождения ресурсов.
    /// </summary>
    public void Dispose()
    {
        //  Выполнения завершения операций.
        Dispose(disposing: true);

        //  Подавления выполнения диструктора.
        GC.SuppressFinalize(this);
    }


    /// <summary>
    /// Представляет метод освобождения ресурсов (виртуальный)
    /// </summary>
    /// <param name="disposing">
    /// Флаг освобождения внутренних ресурсов.
    /// </param>
    private void Dispose(bool disposing)
    {
        //   Проверка выполнения операции.
        if (_Disposed)
        {
            return;
        }

        //  Проверка флага
        if (disposing)
        {
            //   Остановка работы класса, и освобождение ресурсов объекта геолокации.
            GeolocationInterface.Dispose();

            //   Остановка работы класса, и освобождение ресурсов объекта модема.
            ModemInterface.Dispose();
        }

        //  Установка флага выполнения операции.
        _Disposed = true;
    }

    /// <summary>
    /// Запускает выполнение задачи.
    /// </summary>
    public void Start()
    {
        lock (_SyncRoot)
        {
            //  Запуск работы интерфейса геолокации
            GeolocationInterface.Start();

            //  Запуск работы интерфейса модема
            ModemInterface.Start();
        }
    }

    /// <summary>
    /// Останавливает выполнение задачи.
    /// </summary>
    public void Stop()
    {
        lock (_SyncRoot)
        {
            //  Остановка работы интерфейса геолокации
            GeolocationInterface.Stop();

            //  Остановка работы интерфейса модема
            ModemInterface.Stop();
        }
    }
}
