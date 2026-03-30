using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Simargl.Platform.Recorder.FileTransferClient.Main;
using System.Runtime.InteropServices;

//  Создание инициализатора службы.
IHost host = Host.CreateDefaultBuilder(args)
    .UseEnvironment("Prodaction")        // Задаёт режим запуска приложения необходимо для чтения правильного файла настроек.
    .ConfigureLogging(delegate (ILoggingBuilder loggingBuilder)
    {
        //  Добавление модуля форматирования журнала консоли.
        loggingBuilder.AddSimpleConsole(options =>
        {
            //  Отключение области.
            options.IncludeScopes = false;

            //  Установка записи сообщений в журнал в одной строке.
            options.SingleLine = false;

            //  Отключение часового пояса UTC для меток времени
            //  в сообщениях журнала.
            options.UseUtcTimestamp = false;

            //  Установка строки формата для форматирования
            //  метки времени в сообщениях журнала.
            options.TimestampFormat = "HH:mm:ss dd.MM.yyyy ";
        });

        //  Проверка платформы.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            //  Добавление средства ведения журнала событий
            //  с именем EventLog в фабрику.
            loggingBuilder.AddEventLog();
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
