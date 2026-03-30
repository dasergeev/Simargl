using Apeiron.Platform.Journals;
using Apeiron.Support;

namespace Apeiron.QuantumX;

public class Worker : BackgroundService
{

    /// <summary>
    /// Представляет интерфейс логирования.
    /// </summary>
    private readonly Journal _Journal;

    /// <summary>
    /// Представляет свойство конфигурации программы.
    /// </summary>
    public static Options? Configuration { get; set; }
    
    /// <summary>
    /// Предствляет логер.
    /// </summary>
    private readonly ILogger<Worker> _Logger;

    /// <summary>
    /// Представляет список потоков данных от QuantumX.
    /// </summary>
    private readonly List<QuantumXManager> ListOfQuantum = new();

    /// <summary>
    /// Представляет интервал ошибки.
    /// </summary>
    private readonly TimeSpan ErrorTimeout = new(0, 1, 0);

    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    /// <param name="logger">
    /// Логер.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре конфигурации передана пустая ссылка.
    /// </exception>
    public Worker(ILogger<Worker> logger)
    {      //  Установка логера.
        _Logger = Check.IsNotNull(logger, nameof(logger));

        //  Создание системы логирования.
        _Journal = Journal.FromLogger(_Logger);
    }

    /// <summary>
    /// Асинхронно выполняет основную работу службы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу службы.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <see cref="Configuration"/> передана пустая ссылка.
    /// </exception>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        //  Проверка конфигурации.
        var options = Check.IsNotNull(Configuration, nameof(Configuration));

        //  Проверка конфигурации.
        var deviceOptions = Check.IsNotNull(options.DeviceOptions, nameof(options.DeviceOptions));

        //  Цикл по всем устройствам 
        foreach (var one in deviceOptions)
        {
            //  Создание потока устройства.
            QuantumXManager manager = new( _Journal, one);

            //  Добавление устройства в список.
            ListOfQuantum.Add(manager);

            //  Запуск устройства.
            manager.Start();

            //  Логирование.
            _Logger.LogInformation("Запущен Quantum: {ip}.", one?.TransmitterSensorOptions?.SensorIP);
        }

        //  Выполнять пока не отменят.
        while (!stoppingToken.IsCancellationRequested)
        {
            //  Установка индекса
            int index = 0;

            //  Цикл по всем временым меткам получения данных
            foreach(var one in ListOfQuantum)
            {
                //  Проверка ошибки
                if ((DateTime.Now - one.LastReceiveDataTime) > ErrorTimeout)
                {
                    try
                    {
                        //  Создание токена
                        using CancellationTokenSource source = new();

                        //  Отмена после
                        source.CancelAfter(5000);

                        //  Остановка устройства.
                        await one.StopAsync(source.Token);

                    }
                    catch (Exception ex)
                    {
                        //  Логирование.
                        _Logger.LogError("Ошибка операции остановки сбора данных:{message}", ex.Message);

                        //  Проверка исключения
                        if (ex.IsCritical())
                        {
                            //  Выброс исключения.
                            throw;
                        }
                    }

                    //  Запуск устройства
                    one.Start();

                    //  Логирование.
                    _Logger.LogInformation("Запущен Quantum: {ip}.", deviceOptions[index].TransmitterSensorOptions?.SensorIP);
                }
                    
                //  Инкремент индекса
                index++;
            }

            //  Ожидание.
            await Task.Delay(10000, stoppingToken).ConfigureAwait(false);

        }
    }
}
