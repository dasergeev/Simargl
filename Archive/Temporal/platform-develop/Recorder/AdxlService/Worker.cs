using Apeiron.Support;
using System.Net;
using Apeiron.Platform.Transmitters;
using Apeiron.Platform.Journals;
using Apeiron.Platform.Sensors;

namespace Apeiron.Adxl;

public class Worker : BackgroundService
{
    /// <summary>
    /// Представляет интерфейс логирования.
    /// </summary>
    private readonly Journal _Journal;

    /// <summary>
    /// Устанавливает и возвращает конфигурацию службы.
    /// </summary>
    public static Options? Configuration { get; set; }

    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<Worker> _Logger;


    /// <summary>
    /// Представляет список сконфигурированных датчиков.
    /// </summary>
    private readonly List<AdxlSensor> _SensorsList = new();

    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    /// <param name="logger"></param>
    public Worker(ILogger<Worker> logger)
    {
        //  Установка значения системы логирования по умолчанию
        _Logger = logger;
        
        //  Создание системы логирования.
        _Journal = Journal.FromLogger(_Logger);
    }

    /// <summary>
    /// Выполняет основную работу службы.
    /// </summary>
    /// <param name="stoppingToken">Токен остановки.</param>
    /// <returns>Задача</returns>
    /// <exception cref="ArgumentNullException">
    /// Не получены настройки: <see cref="Configuration"/>.
    /// </exception>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        //  Проверка конфигурации.
        var options = Check.IsNotNull(Configuration, nameof(Configuration));

        //  Проверка конфигурации.
        var transmittersConfig = Check.IsNotNull(options.SensorTransmitterConfiguration, nameof(options.SensorTransmitterConfiguration));

        //  Создание управляющего.
        AdxlManager manager = new(stoppingToken);


        //  Цикл по всем датчикам
        foreach (TransmitterSensorOptions config in transmittersConfig)
        {
            if (config.SensorIP is not null && config.TranmitterConfig is not null)
            {
                //  Создание датчика.
                var sensor = new AdxlSensor(_Journal, IPAddress.Parse(config.SensorIP), config.TranmitterConfig);

                //  Добавление датчика
                _SensorsList.Add(sensor);

                //  Добавление датчика.
                _ = Task.Run(async () => await manager.AddAsync(sensor),stoppingToken);
            }
        }

        //  Основной цикл.
        while (!stoppingToken.IsCancellationRequested)
        {
            //  Ожидaние.
            await Task.Delay(60000, stoppingToken).ConfigureAwait(false);

            //  Логирование.
            _Logger.LogInformation("Working: {time}",DateTime.Now);

            foreach(var sensor in _SensorsList)
            {
                try
                {

                    //  Чтение данных
                    await sensor.Modbus.LoadAsync(stoppingToken).ConfigureAwait(false);

                    //  Проверка значения самодиагностики.
                    if (sensor.Modbus.ErrorCodes != 0)
                    {
                        //  Логирование.
                        _Logger.LogError("Обнаружена ошибка самодиагностики датчика: {ErrorCodes}", sensor.Modbus.ErrorCodes);

                        //  Сброс ошибок.
                        await sensor.Modbus.ClearErrorAsync(stoppingToken).ConfigureAwait(false);
                    }

                    //  Проверка значения самотестирования.
                    if (sensor.Modbus.DiagnosticValue < 0.5 || sensor.Modbus.DiagnosticValue > 3.5)
                    {
                        //  Логирование.
                        _Logger.LogError("Обнаружена ошибка тестирования микросхемы Adxl: {DiagnosticValue}", sensor.Modbus.DiagnosticValue);
                    }
                }
                catch (Exception ex)
                {
                    //  Логирование.
                    _Logger.LogError("Обнаружена ошибка чтения Modbus: {Message}", ex.Message);

                    //  Проверка исключения
                    if (ex.IsCritical())
                    {
                        //  Переброс исключения
                        throw;
                    }
                }
            }
        }


       
    }
}
