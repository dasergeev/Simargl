using Apeiron.Platform.Expanse;
using Apeiron.Platform.Medium;
using Apeiron.Platform.Performers;
using Apeiron.Platform.Server.Services.Microservices;
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
            options.SingleLine = false;

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
        //  Добавление микрослужбы, выполняющей анализ базы данных.
        services.AddHostedService<DatabaseAnalyser>();

        //  Добавление микрослужбы, выполняющей приём файлов от удалённых устройств.
        services.AddHostedService<FileTransfer>();

        //  Добавление микрослужбы, выполняющей приём идентификационных пакетов от удалённых устройств.
        services.AddHostedService<GlobalIdentity>();

        //  Добавление исполнителя, выполняющего координацию серверного пространства.
        services.AddPerformer<ExpansePerformer>();

        //  Добавление исполнителя, выполняющего обмен данными.
        services.AddPerformer<MediumPerformer>();

        ////  Добавление микрослужбы, выполняющей управление хостами и обновления ПО.
        //services.AddHostedService<ServerHubWorker>();
    })
    .Build();

//  Запуск приложения.
await host.RunAsync().ConfigureAwait(false);
