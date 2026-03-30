using Apeiron.Platform.Server.Services.Orchestrator.OrchestratorHostAgent;
using System.Runtime.InteropServices;

//  Создание инициализатора службы.
IHost host = Host.CreateDefaultBuilder(args)
    .UseEnvironment("Prodaction")           // Задаёт режим запуска приложения необходимо для чтения правильного файла настроек.
    .ConfigureLogging(loggingBuilder =>     // Настройка логгера.
    {
        //  Добавление модуля форматирования журнала консоли.
        loggingBuilder.AddSimpleConsole(options =>
        {
            //  Отключить области.
            options.IncludeScopes = false;

            //  Записывать все сообщения в журнал в одной строке.
            options.SingleLine = true;

            //  Не использовать часовой пояс UTC для меток времени в сообщениях журнала.
            options.UseUtcTimestamp = false;

            //  Строка формата, используемая для форматирования метки времени в сообщениях журнала.
            options.TimestampFormat = "HH:mm:ss dd.MM.yyyy ";
        });

        //  Проверка платформы.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            //  Добавляет средство ведения журнала событий с именем EventLog в фабрику.
            loggingBuilder.AddEventLog();
        }
    })
    .UseWindowsService().UseSystemd()       // Добавляет режим запуска службы в Windows и Linux.
    .ConfigureServices((hostContext, services) =>
    {
        // Получаем конфигурацию и окружение
        IConfiguration configuration = hostContext.Configuration;
        // Привязываем класс к соответствующей секции в json файле.
        WorkerOptions options = configuration.GetSection("Options").Get<WorkerOptions>()!;

        services.AddSingleton(options);

        ////  Добавление микрослужбы, выполняющей управление хостами и обновления ПО.
        //services.AddHostedService<HostAgentWorker>();
    })
    .Build();

//  Запуск приложения.
await host.RunAsync().ConfigureAwait(false);
