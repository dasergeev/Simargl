using Apeiron.Platform.Transmitters;
using Apeiron.Platform.Teltonika;
using ExternalPackage;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;
using Apeiron.Platform.Journals;

namespace TeltonikaService;

/// <summary>
/// Предствляет класс задачи сохранения данных геолокации.
/// </summary>
internal sealed class TeltonikaLoger
{

    /// <summary>
    /// Представляет интерфейс логирования.
    /// </summary>
    private readonly Journal _Journal;

    /// <summary>
    /// Представляет максимальный интервал хранения счетчика ошибок.
    /// </summary>
    private readonly TimeSpan _ClearErrorInterval = new(1, 0, 0);

    /// <summary>
    /// Представляет минимальный интервал для перезагрузки модема.
    /// </summary>
    private readonly TimeSpan _ModemRebootInterval = new (3, 0, 0);

    /// <summary>
    /// Представляет минимальный интервал для перезагрузки Teltonika.
    /// </summary>
    private readonly TimeSpan _TeltonikaRebootInterval = new (6, 0, 0);

    /// <summary>
    /// Константа максимального количества ошибок для перехода на новый уровень восстановления.
    /// </summary>
    private const byte _MaxRepairCount = 5;

    /// <summary>
    /// Константа количества попыток пинга для проверки интернета.
    /// </summary>
    private const byte _PingCount = 5;

    /// <summary>
    /// Поле хранящее количество ошибок Nmea.
    /// </summary>
    private int _NmeaErrorCount;

    /// <summary>
    /// Поле хранящее количество ошибок доступности интернета.
    /// </summary>
    private int _GsmErrorCount;

    /// <summary>
    /// Представляет время последней перезагузки Teltonika.
    /// </summary>
    private DateTime _LastTeltonikaResetTime = new();

    /// <summary>
    /// Представляет время последнего перезапуска модема.
    /// </summary>
    private DateTime _LastModemResetTime = new();

    /// <summary>
    /// Представляет время последнего перезапуска сервиса gpsd.
    /// </summary>
    private DateTime _LastServiceResetTime = new();

    /// <summary>
    /// Представляет время последнего переключения сим карты.
    /// </summary>
    private DateTime _LastSimChangedTime = new();

    /// <summary>
    /// Представляет время последнего получение GeolocationInfo.
    /// </summary>
    private DateTime _LastGeolocationDateTime = new();

    /// <summary>
    /// Представляет объект устройства.
    /// </summary>
    private readonly Teltonika _Teltonika;

    /// <summary>
    /// Представляет объект логгер.
    /// </summary>
    private readonly ILogger<Worker> _Logger;

    /// <summary>
    /// Представляет минимальный уровень сигнала при котором должен быть доступен интернет.
    /// </summary>
    private readonly int _MimimumGsmSignal;

    /// <summary>
    /// Представляет переадресатор Nmea.
    /// </summary>
    private readonly BinaryTransmitter _NmeaTransmitter;

    /// <summary>
    /// Представляет переадресатор обработанных данных геолокации.
    /// </summary>
    private readonly BinaryTransmitter _GeolocationTransmitter;

    /// <summary>
    /// Представляет переадресатор ModemInfo.
    /// </summary>
    private readonly BinaryTransmitter _ModemTransmitter;

    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    /// <param name="teltonika">
    /// Интерфейс управления тельтоникой.
    /// </param>
    /// <param name="savedFileTime">
    /// Время сохранения файла в секундах.
    /// </param>
    /// <param name="logger">
    /// Логер.
    /// </param>  
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="teltonika"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре конфигурации передана пустая ссылка.
    /// </exception>
    public TeltonikaLoger(Teltonika teltonika, ILogger<Worker> logger)
    {
        //  Создание системы логирования.
        _Journal = Journal.FromLogger(logger);

        //  Установка минимального уровеня сигнала.
        _MimimumGsmSignal = Worker.Configuration.Strength;  

        //  Проверка и сохранения объекта устройства
        _Teltonika = Check.IsNotNull(teltonika,nameof(teltonika));

        //  Подписка на событие.
        _Teltonika.GeolocationInterface.Nmea += ReceivedNmea;
        
        //  Подписка на событие.
        _Teltonika.GeolocationInterface.PropertyChanged += ReceivedGeolocation;

        //  Подписка на событие.
        _Teltonika.GeolocationInterface.NmeaError += StopedNmea;

        //  Подписка на событие.
        _Teltonika.GeolocationInterface.ScriptError += StopedScript;

        //  Подписка на событие.
        _Teltonika.ModemInterface.PropertyChanged += ReceivedModemInfo;

        //  Установка логера.
        _Logger = Check.IsNotNull(logger, nameof(logger));

        //  Инициализация инструмента переадресации
        _NmeaTransmitter = new(_Journal, Check.IsNotNull(Worker.Configuration.NmeaTransmitteOptions,nameof(Worker.Configuration)));
 
        //  Инициализация инструмента переадресации
        _ModemTransmitter = new(_Journal, Check.IsNotNull(Worker.Configuration.ModemsTransmitteOptions, nameof(Worker.Configuration)));

        //  Инициализация инструмента переадресации
        _GeolocationTransmitter = new(_Journal, Check.IsNotNull(Worker.Configuration.GeoloctionTransmitteOptions, nameof(Worker.Configuration)));
    }

    /// <summary>
    /// Представляет обработчик события <see cref="GeolocationManager.Nmea"/>
    /// </summary>
    /// <param name="obj">
    /// Объект.
    /// </param>
    /// <param name="nmea">
    /// Аргумент.
    /// </param>
    private void ReceivedNmea(object? obj,StringEventArgs nmea)
    {
        try
        {
            //  Пересылка Nmea адресатам в настройках
            _NmeaTransmitter.SendTransparent(nmea.Data);
        }
        catch (Exception ex)
        {
            if(ex.IsSystem())
            {
                throw;
            }

            // Логирование
            _Logger.LogError("Ошибка сохранения NMEA в файл.");
        }
        
    }

    /// <summary>
    /// Представляет обработчик события <see cref="GeolocationManager.NmeaError"/>
    /// </summary>
    /// <param name="obj">
    /// Объект.
    /// </param>
    /// <param name="action">
    /// Аргумент.
    /// </param>
    private void StopedNmea(object? obj, ErrorActionEventArgs action)
    {
  
        //  Проверка количества ошибок.
        if (_NmeaErrorCount >= _MaxRepairCount)
        {
            //  Проверка времени последней перезагрузки.
            if ((DateTime.Now - _LastModemResetTime) > _ModemRebootInterval)
            {
                //  Установка времени перезапуска модема.
                _LastModemResetTime = DateTime.Now;

                // Логирование
                _Logger.LogError("Перезапуск модема в связи частого сбоя NMEA.");

                //  Установка действия по перезапуску модема.
                action.NeedAction = GeolocationManager.ConstResetModem;

                //  Сброс счетчика.
                _NmeaErrorCount = 0;

                //  Возврат из функции.
                return;
            }
        }
        //  Проверка количества ошибок.
        if (_NmeaErrorCount >= _MaxRepairCount)
        {
            //  Проверка времени последней перезагрузки.
            if ((DateTime.Now - _LastTeltonikaResetTime) > _TeltonikaRebootInterval)
            {
                //  Установка времени перезапуска Teltonika.
                _LastTeltonikaResetTime = DateTime.Now;

                // Логирование
                _Logger.LogError("Перезапуск Teltonika в связи частого сбоя NMEA.");

                //  Установка действия по перезапуску Teltonika.
                action.NeedAction = GeolocationManager.ConstResetTeltonika;

                //  Сброс счетчика.
                _NmeaErrorCount = 0;

                //  Возврат из функции.
                return;
            }
        }
        // Логирование
        _Logger.LogError("Перезапуск сервиса gpsd в связи со сбоем NMEA.");

        //  Установка действия по перезапуску службы.
        action.NeedAction = GeolocationManager.ConstResetService;

        //  Проверка времени последнего события.
        if ((DateTime.Now - _LastServiceResetTime) > _ClearErrorInterval)
        {
            //  Сброс счетчика ошибок
            _NmeaErrorCount = 0;
        }

        //  Увеличение счетчика ошибок.
        _NmeaErrorCount++;

        //  Установка времени перезапуска службы.
        _LastServiceResetTime = DateTime.Now;
    }



    /// <summary>
    /// Представляет обработчик события <see cref="GeolocationManager.ScriptError"/>
    /// </summary>
    /// <param name="obj">
    /// Объект.
    /// </param>
    /// <param name="args">
    /// Аргумент.
    /// </param>
    private void StopedScript(object? obj, EventArgs args)
    {
        // Логирование
        _Logger.LogError("Обнаружено падение скрипта в Teltonika.");
    }

    /// <summary>
    /// Представляет обработчик события <see cref="GeolocationManager.PropertyChanged"/>, для получения геолокации.
    /// </summary>
    /// <param name="obj">
    /// Объект.
    /// </param>
    /// <param name="action">
    /// Аргумент.
    /// </param>
    private void ReceivedGeolocation(object? obj, PropertyChangedEventArgs args)
    {
        try
        {
            var geolocation = _Teltonika.GeolocationInterface.Geolocation;

            if (geolocation != null)
            {
                //  Инициализация флага.
                bool isValide = false;

                //  Проверка условия.
                if (geolocation.Time > _LastGeolocationDateTime)
                {
                    // Установка флага
                    isValide = true;
                }

                //  Установка нового времени.
                _LastGeolocationDateTime = geolocation.Time;

                //  Создание пакета.
                GeolocationPackage info = new(
                    (double)geolocation.Latitude,
                    (double)geolocation.Longitude,
                    geolocation.Speed,
                    isValide);

                //  Пересылка данных.
                _GeolocationTransmitter.SendTransparent(info.GetDatagram());
            }
        }
        catch (Exception ex)
        {
            if (ex.IsSystem())
            {
                throw;
            }

            // Логирование
            _Logger.LogError("Ошибка сохранения GeolocationInfo в файл.");
        }

    }

    /// <summary>
    /// Представляет обработчик события <see cref="ModemManager.PropertyChanged"/>, для получения геолокации.
    /// </summary>
    /// <param name="obj">
    /// Объект.
    /// </param>
    /// <param name="action">
    /// Аргумент.
    /// </param>
    private void ReceivedModemInfo(object? obj, PropertyChangedEventArgs args)
    {
        try
        {
            var info = _Teltonika.ModemInterface.Info;

            if (info != null)
            {
                //  Преобразование данных в строку
                var str = info.ToString();

                //  Пересылка данных.
                _ModemTransmitter.SendTransparent(str);
                
                //  Проверка уровня сигнала.
                if (info.GsmSignal > _MimimumGsmSignal)
                {
                    _ = Task.Run(() => RepairEthernet());
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.IsSystem())
            {
                throw;
            }

            // Логирование
            _Logger.LogError("Ошибка сохранения GeolocationInfo в файл.");
        }

    }

    /// <summary>
    /// Запускает выполнение задачи.
    /// </summary>
    public void Start()
    {
        _Teltonika.Start();
    }

    /// <summary>
    /// Останавливает выполнение задачи.
    /// </summary>
    public void Stop()
    {
        _Teltonika.Stop();
    }


    /// <summary>
    /// Представляет функцию пингу удаленного адреса
    /// </summary>
    /// <param name="address">
    /// Удаленный адрес.
    /// </param>
    /// <param name="timeout">
    /// Таймаут.
    /// </param>
    /// <returns>
    /// <c>true</c> - успешно, 
    /// <c>faslse</c> - исчерпан таймаут или ошибка.</returns>
    private static bool PingIP(IPAddress address, int timeout)
    {
        try
        {
            //  Создание отправителя.
            Ping pingSender = new();

            //  Отправка пинга
            PingReply reply = pingSender.Send(address, timeout);

            //  Проверка результата
            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            //  Проверка исключения.
            if (ex.IsSystem())
            {
                throw;
            }
        }
        return false;
    }

    /// <summary>
    /// Представляет функцию проверки и восстановления интернета.
    /// </summary>
    private void RepairEthernet()
    {
        //Проверка доступности интернета
        bool isPingSuccess = false;
        for (int i = 0; i < _PingCount; i++)
        {
            //  Выполнение операции пинга
            if (PingIP(IPAddress.Parse("8.8.8.8"), 3000))
            {
                isPingSuccess = true;
                break;
            }
        }

        if (!isPingSuccess)
        {

            //  Проверка количества ошибок.
            if (_GsmErrorCount >= _MaxRepairCount)
            {
                //  Проверка времени последней перезагрузки.
                if ((DateTime.Now - _LastModemResetTime) > _ModemRebootInterval)
                {
                    //  Установка времени перезапуска модема.
                    _LastModemResetTime = DateTime.Now;

                    // Логирование
                    _Logger.LogError("Перезапуск модема в связи недоступностью интернета.");

                    //  Установка действия по перезапуску модема.
                    _Teltonika.ModemInterface.Reset(ModemManager.ConstResetModem);

                    //  Сброс счетчика.
                    _GsmErrorCount = 0;

                    //  Возврат из функции.
                    return;
                }
            }

            //  Проверка количества ошибок.
            if (_GsmErrorCount >= _MaxRepairCount)
            {
                //  Проверка времени последней перезагрузки.
                if ((DateTime.Now - _LastTeltonikaResetTime) > _TeltonikaRebootInterval)
                {
                    //  Установка времени перезапуска Teltonika.
                    _LastTeltonikaResetTime = DateTime.Now;

                    // Логирование
                    _Logger.LogError("Перезапуск Teltonika в связи недоступностью интернета.");

                    //  Установка действия по перезапуску Teltonika.
                    _Teltonika.ModemInterface.Reset(ModemManager.ConstResetTeltonika);

                    //  Сброс счетчика.
                    _GsmErrorCount = 0;

                    //  Возврат из функции.
                    return;
                }

            }

            // Логирование
            _Logger.LogError("Переключение SIM в связи недоступностью интернета. (не поддерживается.)");

            //  Установка действия по переключению SIM карты.
            _Teltonika.ModemInterface.Reset(ModemManager.ConstChangeSim);

            //  Проверка времени последнего события.
            if ((DateTime.Now - _LastSimChangedTime) > _ClearErrorInterval)
            {
                //  Сброс счетчика ошибок
                _GsmErrorCount = 0;
            }

            //  Увеличение счетчика.
            _GsmErrorCount++;

            //  Установка времени переключения сим карты.
            _LastSimChangedTime = DateTime.Now;
        }
    }
}
