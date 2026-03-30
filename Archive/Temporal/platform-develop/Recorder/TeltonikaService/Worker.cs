using Apeiron.Platform.Teltonika;
using System.Net;

namespace TeltonikaService;

/// <summary>
/// Представляет класс службы.
/// </summary>
public class Worker :
    BackgroundService
{
    /// <summary>
    /// Представляет свойство конфигурации программы.
    /// </summary>
    public static Options Configuration { get; set; } = new();

    /// <summary>
    /// Предствляет логер.
    /// </summary>
    private readonly ILogger<Worker> _logger;

    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    /// <param name="logger">
    /// Логер.
    /// </param>
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Выполняет основную работу службы.
    /// </summary>
    /// <param name="stoppingToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задачу.
    /// </returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //Ожидание логера.
        await Task.Delay(1000,stoppingToken).ConfigureAwait(false);

        //  Проверка полученной конфигурации.
        var options = Check.IsNotNull(Configuration, nameof(Configuration));    

        //  Создание массива интерфейсов
        ICommandLine[] interfaces = new ICommandLine[1] { new SshCommander(options.TetlonikaIP!, Teltonika.SshPort, "root", options.TeltonikaPassword!) };

        //  Получения адресса тельтоники
        IPAddress address = IPAddress.Parse(options.TetlonikaIP!);

        //  Создание объекта.
        Teltonika teltonika = new(address,options.NmeaUdpPort,options.GeolocationUdpPort,interfaces);

        TeltonikaLoger teltonikaLoger = new(teltonika, _logger);

        //Логирование
        _logger.LogInformation("Запуск служб Tetlonika.");

        //  Заппуск служб Teltonika.
        teltonikaLoger.Start();

        //Цикл
        while (!stoppingToken.IsCancellationRequested)
        {
            //Логирование
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            //Ожидание
            await Task.Delay(30000, stoppingToken);
        }

        //  Остановка служб Teltonika.
        teltonikaLoger.Stop();
    }
}
