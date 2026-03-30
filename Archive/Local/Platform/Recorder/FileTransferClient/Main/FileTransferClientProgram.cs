using Apeiron.Services.FileTransfer;
using System.Runtime.InteropServices;

//  Создание инициализатора службы.
IHost host = Host.CreateDefaultBuilder(args)
    .UseEnvironment("Prodaction")        // Задаёт режим запуска приложения необходимо для чтения правильного файла настроек.
    .ConfigureLogging(logging =>         // Настройка логгера.
    {
        //  Удаление всех поставщиков средств ведения журнала.
        logging.ClearProviders();

        //  Добавление модуля форматирования журнала консоли.
        logging.AddSimpleConsole(options =>
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
            logging.AddEventLog();
        }
    })
    .UseWindowsService().UseSystemd()    // Добавляем режим запуска службы в Windows и Linux.
    .ConfigureServices((hostContext, services) =>
    {
        //  Получение конфигурации службы.
        IConfiguration configuration = hostContext.Configuration;

        //  Добавление конфигурации службы.
        services.AddSingleton(configuration);

        //  Регистрация службы.
        services.AddHostedService<FileTransferClientWorker>();
    })
    .Build();

//  Запуск службы.
await host.RunAsync().ConfigureAwait(false);
