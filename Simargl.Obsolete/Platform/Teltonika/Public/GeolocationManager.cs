using System.Text;
using System.ComponentModel;
using System.Net.Sockets;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using Simargl.Designing.Utilities;

namespace Simargl.Platform.Teltonika;

/// <summary>
/// Представляет класс, получения данных геолокации.
/// </summary>
public sealed class GeolocationManager :
    INotifyPropertyChanged,
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
    /// Константа пути для сервиса пересылки и настройки NMEA.
    /// </summary>
    private const string _NmeaServicePath = "/etc/init.d/gpsd";

    /// <summary>
    /// Таймаут индикации ошибки.
    /// </summary>
    private const int _SilentNmeaTimeoutMS = 300000;

    /// <summary>
    /// Константа минимального значения свойства <see cref="UpdateTime"/>"/>
    /// </summary>
    private const int _ConstMimimumUpdateTimeMs = 60000;

    /// <summary>
    /// Константа перезагрузки модема.
    /// </summary>
    public const byte ConstResetModem = 0x01;

    /// <summary>
    /// Константа перезагрузки сервиса.
    /// </summary>
    public const byte ConstResetService = 0x02;

    /// <summary>
    /// Константа перезагрузки Teltonika.
    /// </summary>
    public const byte ConstResetTeltonika = 0x04;

    /// <summary>
    /// Поле хранящее последние данные геолокации.
    /// </summary>
    private GeolocationInfo? _Geolocation;

    /// <summary>
    /// Поле хранящее последние данные геолокации.
    /// </summary>
    private volatile int _UpdateTime = _ConstMimimumUpdateTimeMs;

    /// <summary>
    /// Поле хранящее драйвер интерфейса Modbus.
    /// </summary>
    private readonly ModbusManager _ModbusManager;

    /// <summary>
    /// Поле хранящее драйвер модема.
    /// </summary>
    private readonly ModemManager _ModemManager;

    /// <summary>
    /// Поле хранящее интерфейса управления процессами.
    /// </summary>
    private readonly ProcessManager _ProcessManager;

    /// <summary>
    /// Поле порта получения NMEA.
    /// </summary>
    private readonly int _NmeaUdpPort;

    /// <summary>
    /// Поле порта получения Geolocation.
    /// </summary>
    private readonly int _GeolocationUdpPort;

    /// <summary>
    /// Источник токена отмены задач объекта.
    /// </summary>
    private CancellationTokenSource? _CancellationTokenSource;
    
    /// <summary>
    /// Представляет ссылку на задачу получения Nmea.
    /// </summary>
    private Task? _NmeaTask;
    
    /// <summary>
    /// Представляет ссылку на задачу получения геолокации.
    /// </summary>
    private Task? _GeolocationRequestTask;

    /// <summary>
    /// Представляет ссылку на задачу получения геолокации.
    /// </summary>
    private Task? _GeolocationUdpTask;

    /// <summary>
    /// Представляет событие изменеиния свойства данных геолокации.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Представляет событие возникновения ошибки получения геолокации NMEA.
    /// </summary>
    public event EventHandler<ErrorActionEventArgs>? NmeaError;
    
    /// <summary>
    /// Представляет событие возникновения ошибки получения геолокации обработаных значений.
    /// </summary>
    public event EventHandler? ScriptError;
    
    /// <summary>
    /// Представляет событие получения новой порции данных Nmea.
    /// </summary>
    public event EventHandler<StringEventArgs>? Nmea;

    /// <summary>
    /// Возвращает флаг состояния запуска.
    /// </summary>
    public bool Started { get; private set; } = false;

    /// <summary>
    /// Возвращает последнии данные геолокации
    /// </summary>
    public GeolocationInfo? Geolocation
    {
        get => _Geolocation;
        private set
        {
            _Geolocation = value;
            OnNotifyPropertyChanged();
        }
    }

    /// <summary>
    /// Возвращает и устанавливает значение времени обновления свойства <see cref="Geolocation"/>
    /// </summary>
    /// <remarks>
    /// Минимальное значение 60000 мс.
    /// </remarks>
    public int UpdateTime
    {
        //  Возврат значения.
        get => _UpdateTime;
        set
        {
            //  Проверка на минимальное значение.
            if (value < _ConstMimimumUpdateTimeMs)
            {
                //  Установка значения.
                _UpdateTime = _ConstMimimumUpdateTimeMs;
            }
            else
            {
                //  Установка значения.
                _UpdateTime = value;
            }
        }
    }

    /// <summary>
    /// Возвращает время получения данных геолокации.
    /// </summary>
    public DateTime GeolocationLastReceiveTime { get; private set; } = DateTime.Now;

    /// <summary>
    /// Инициализирует объект
    /// </summary>
    /// <param name="modbusManager">
    /// Драйвер интерфейса Modbus.
    /// </param>
    /// <param name="modemManger">
    /// Драйвер модема.
    /// </param>
    /// <param name="processManager">
    /// Интерфейс управления процессами.
    /// </param>
    /// <param name="nmeaPort">
    /// Порт получения данных NMEA.
    /// </param>
    /// <param name="geolocationPort">
    /// Порт получения данных Geolocation по UDP.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="modbusManager"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="modemManger"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="processManager"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="nmeaPort"/> передано не допустимое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="geolocationPort"/> передано не допустимое значение.
    /// </exception>
    public GeolocationManager(ModbusManager modbusManager, ModemManager modemManger, ProcessManager processManager, int nmeaPort,int geolocationPort)
    {
        //  Установка интерфейса Modbus
        _ModbusManager = IsNotNull(modbusManager,nameof(modbusManager));

        //  Установка интерфейса Modem
        _ModemManager = IsNotNull(modemManger,nameof(modbusManager));

        //  Установка интерфейса Process
        _ProcessManager = IsNotNull(processManager,nameof(processManager));

        //  Проверка порта на минимальное значение
        IsNotLess(nmeaPort, 0, nameof(nmeaPort));

        //  Проверка порта на максимальное значение
        IsNotLarger(nmeaPort,ushort.MaxValue, nameof(nmeaPort));

        //  Установка порта.
        _NmeaUdpPort = nmeaPort;

        //  Проверка порта на минимальное значение
        IsNotLess(geolocationPort, 0, nameof(geolocationPort));

        //  Проверка порта на максимальное значение
        IsNotLarger(geolocationPort, ushort.MaxValue, nameof(geolocationPort));

        //  Установка порта.
        _GeolocationUdpPort = geolocationPort;

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
    /// Флаг завершения внутренних объектов.
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
            //   Остановка работы класса, и освобождение ресурсов.
            Stop();
        }

        //  Установка флага выполнения операции.
        _Disposed = true;
    }


    /// <summary>
    /// Представляет функцию запуса сервисов объекта.
    /// </summary>
    public void Start()
    {
        lock (_SyncRoot)
        {
            //  Проверка флага
            if (Started == false)
            {
                //  Создание источника токена.
                _CancellationTokenSource = new CancellationTokenSource();

                //  Запуск задачи получения NMEA
                _NmeaTask = Task.Run(async () => await NmeaReceiveAsync(_CancellationTokenSource.Token).ConfigureAwait(false));

                //  Запуск задачи получения GeolocationInfo
                _GeolocationUdpTask = Task.Run(async () => await UpdateGeolocationUdpAsync(_CancellationTokenSource.Token).ConfigureAwait(false));

                //  Запуск задачи запроса GeolocationInfo.
                _GeolocationRequestTask = Task.Run(async () => await UpdateGeolocationRequestedAsync(_CancellationTokenSource.Token).ConfigureAwait(false));
                
                //  Установка флага.
                Started = true;
            }
        }
    }


    /// <summary>
    /// Представляет функцию остановки сервисов объекта.
    /// </summary>
    public void Stop()
    {
        lock (_SyncRoot)
        {
            if (Started == true)
            {
                _CancellationTokenSource?.Cancel();
                _NmeaTask?.Wait();
                _GeolocationRequestTask?.Wait();
                _GeolocationUdpTask?.Wait();
                _CancellationTokenSource?.Dispose();

                Started = false;
            }
        }
    }

    /// <summary>
    /// Представляет функцию восстановления работы сервиса.
    /// </summary>
    /// <param name="options">
    /// Опции.
    /// </param>
    public void Reset(byte options)
    {
        lock (_SyncRoot)
        {
            //  Проверка на операцию перезагрузки модема
            if ((options & ConstResetModem) != 0)
            {
                //  Перезагрузка модема
                _ModemManager.ResetModem();
            }

            //  Проверка на операцию перезагрузки сервиса
            if ((options & ConstResetService) != 0)
            {
                //  Перезагрузка сервиса
                _ProcessManager.RestartService(_NmeaServicePath);
            }

            //  Проверка на операцию перезагрузки Teltonika
            if ((options & ConstResetTeltonika) != 0)
            {
                //  Перезагрузка Teltonika
                _ProcessManager.RebootDevice();
            }
        }
    }

    /// <summary>
    /// Выполняет получение Nmea по UDP.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача
    /// </returns>
    private async Task NmeaReceiveAsync(CancellationToken cancellationToken)
    {

        //  Создание UDP-клиента.
        UdpClient udpClient = new(_NmeaUdpPort);

        //  Корректировка времени получения пакета.
        DateTime lastTime = DateTime.Now;

        //  Установка интервала ожидания данных.
        TimeSpan silentTimeout = new (0,0,0,0,_SilentNmeaTimeoutMS);

        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                if (udpClient.Available > 0)
                {
                    //  Получение UDP-датаграммы.
                    UdpReceiveResult result = await udpClient.ReceiveAsync(cancellationToken).ConfigureAwait(false);
                    
                    //  Преобразование.
                    string nmeaString = Encoding.ASCII.GetString(result.Buffer);

                    //  Вызов события.
                    OnNmea(nmeaString);

                    //  Установка времени получения.
                    lastTime = DateTime.Now;

                    continue;
                }

                //  Ожидaние.
                await Task.Delay(50, cancellationToken).ConfigureAwait(false);
                
                //  Проверка величины счетки 
                if (silentTimeout < (DateTime.Now - lastTime))
                {
                    //  Сброс временной метки.
                    lastTime = DateTime.Now;

                    //  Перезапуск UDP
                    udpClient.Close();
                    udpClient.Dispose();
                    udpClient = new(_NmeaUdpPort);

                    //  Вызов события.
                    OnError();
                }
            }
            catch (Exception ex)
            {
                //  Проверка типа исключение
                if (ex.IsSystem())
                {
                    //  Перенаправление исключения.
                    throw;
                }

                //  Перезапуск UDP
                udpClient.Close();
                udpClient.Dispose();
                udpClient = new(_NmeaUdpPort);
            }
        }
        //  Освобождение ресурсов.
        udpClient.Close();
        udpClient.Dispose();
    }

    /// <summary>
    /// Выполняет получения данных геолокации.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача
    /// </returns>
    private async Task UpdateGeolocationRequestedAsync(CancellationToken cancellationToken)
    {

        //  Установка интервала ожидания данных.
        TimeSpan timeout = new (0, 0, 0, 0, _UpdateTime);

        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Ожидaние.
            await Task.Delay(_UpdateTime, cancellationToken).ConfigureAwait(false);
            
            //  Проверка времени последнего получения.
            if(DateTime.Now - GeolocationLastReceiveTime < timeout)
            {
                //  Продолжение ожидания.
                continue;
            }
            try
            {
                //  Получение данных от процесса.
                var info = _ProcessManager.GetGeolocation();

                //  Проверка результата
                if (info != null)
                {
                    //  Установка нового значения.
                    Geolocation = info;
                }
                continue;
            }
            catch (Exception ex)
            {
                //  Проверка типа исключение
                if (ex.IsSystem())
                {
                    //  Перенаправление исключения.
                    throw;
                }
            }

            try
            {
                //  Обновление данных
                await _ModbusManager.UpdateAsync(cancellationToken).ConfigureAwait(false);

                //  Сохранение полученных данных.
                Geolocation = new GeolocationInfo(
                    _ModbusManager.Latitude,
                    _ModbusManager.Longitude,
                    float.NaN,
                    _ModbusManager.Time,
                    _ModbusManager.Speed,
                    _ModbusManager.SateliteCount,
                    _ModbusManager.Accuracy);

                //  Продолжение цикла.
                continue;
            }
            catch (Exception ex)
            {
                //  Проверка типа исключение
                if (ex.IsSystem())
                {
                    //  Перенаправление исключения.
                    throw;
                }

            }

            GeolocationLastReceiveTime = DateTime.Now;
        }
    }

    /// <summary>
    /// Выполняет получение Nmea по UDP.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача.
    /// </returns>
    private async Task UpdateGeolocationUdpAsync(CancellationToken cancellationToken)
    {

        //  Создание UDP-клиента.
        UdpClient udpClient = new(_GeolocationUdpPort);

        //  Корректировка времени получения пакета.
        DateTime lastTime = DateTime.Now;

        //  Установка интервала ожидания данных.
        TimeSpan silentTimeout = new(0, 0, 0, 0, _SilentNmeaTimeoutMS);

        //  Основной цикл.
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                if (udpClient.Available > 0)
                {
                    //  Получение UDP-датаграммы.
                    UdpReceiveResult result = await udpClient.ReceiveAsync(cancellationToken).ConfigureAwait(false);

                    //   Преобразование буфера.
                    var resultString = Encoding.UTF8.GetString(result.Buffer);  

                    //  Создание читателя.
                    using var reader = new StringReader(resultString);

                    //  Чтение строки.
                    var line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

                    //  Чтение префикса.
                    string prefix = IsNotNull(line, nameof(line));

                    //  Проверка префикса.
                    if(prefix.Equals("Apeiron") == false)
                    {
                        //  Продолжение цикла.
                        continue;
                    }
                    
                    //  Чтение строки.
                    line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

                    //  Чтение IMEI
                    var imei = IsNotNull(line, nameof(line));

                    //  Чтение строки.
                    line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

                    //  Чтение ICCID
                    var iccid = IsNotNull(line, nameof(line));

                    //  Чтение строки.
                    line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

                    //  Получение широты
                    float latitude = float.Parse(IsNotNull(line, nameof(line)), CultureInfo.InvariantCulture);

                    //  Чтение строки.
                    line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

                    //  Получение долготы
                    float longitude = float.Parse(IsNotNull(line, nameof(line)), CultureInfo.InvariantCulture);

                    //  Чтение строки.
                    line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

                    //  Получение высоты.
                    float altitude = float.Parse(IsNotNull(line, nameof(line)), CultureInfo.InvariantCulture);

                    //  Чтение строки.
                    line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

                    //  Получение cкорости.
                    float speed = float.Parse(IsNotNull(line, nameof(line)), CultureInfo.InvariantCulture);

                    //  Чтение времени.
                    var time = IsNotNull(await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false), nameof(line));

                    //  Чтение строки.
                    line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

                    //  Получение точности.
                    float accuracy = float.Parse(IsNotNull(line, nameof(line)), CultureInfo.InvariantCulture);

                    //  Чтение строки.
                    line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);

                    //  Получение количества спутников.
                    int satelite = int.Parse(IsNotNull(line, nameof(line)));

                    //  Установка значения.
                    Geolocation = new(latitude, longitude, altitude, time, (int)speed, satelite, accuracy);

                    //  Установка времени получения.
                    GeolocationLastReceiveTime = DateTime.Now;

                    //  Установка времени получения.
                    lastTime = DateTime.Now;

                    //  Продолжение цикла 
                    continue;
                }

                //  Проверка величины счетки 
                if (silentTimeout < (DateTime.Now - lastTime))
                {
                    //  Сброс временной метки.
                    lastTime = DateTime.Now;

                    //  Перезапуск UDP
                    udpClient.Close();
                    udpClient.Dispose();
                    udpClient = new(_GeolocationUdpPort);

                    //  Вызов события.
                    ScriptError?.Invoke(this, EventArgs.Empty);
                }

                //  Ожидaние.
                await Task.Delay(50, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //  Проверка типа исключение
                if (ex.IsSystem())
                {
                    //  Перенаправление исключения.
                    throw;
                }

                //  Перезапуск UDP
                udpClient.Close();
                udpClient.Dispose();
                udpClient = new(_GeolocationUdpPort);
            }
        }

        //  Освобождение ресурсов.
        udpClient.Close();
        udpClient.Dispose();
    }


    /// <summary>
    /// Представляет функцию вызова события <see cref="PropertyChanged"/>.
    /// </summary>
    private void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        //Вызов события.
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Представляет функцию вызова события <see cref="Nmea"/>.
    /// </summary>
    /// <param name="data">
    /// Данные
    /// </param>
    private void OnNmea(string data)
    {
        //Вызов события.
        Nmea?.Invoke(this, new(data));
    }

    /// <summary>
    /// Представляет функцию вызова события <see cref="NmeaError"/>.
    /// </summary>
    private void OnError()
    {
        // Создание аргумента
        var argument = new ErrorActionEventArgs();

        //  Вызов события.
        NmeaError?.Invoke(this, argument);

        //  Проверка необходимости действия.
        if (argument.NeedAction != ErrorActionEventArgs.ConstNoAction)
        {
            //   Выполнение процедуры.
            Reset((byte)argument.NeedAction);
        }
    }
}
