using Apeiron.Platform.OrchestratorServer;
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
    .ConfigureServices(services =>
    {
        services.AddHostedService<OrchestratorServerWorker>();
    })
    .Build();

//  Запуск приложения.
await host.RunAsync().ConfigureAwait(false);
