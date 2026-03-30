using Apeiron.Oriole.Server.Logging;
using System.Runtime.InteropServices;

//  Создание инициализатора службы.
IHost host = Host.CreateDefaultBuilder(args)
    .UseEnvironment("Prodaction")           // Задаёт режим запуска приложения необходимо для чтения правильного файла настроек.
    .ConfigureLogging(loggingBuilder =>     // Настройка логгера.
    {
        //  Добавление модуля форматирования журнала консоли.
        loggingBuilder.AddSpecializedConsoleFormatter();

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
        ////────┤ prod-01 ├────────────────────────────────────────────────────────────────

        ////  Добавление службы, управляющей выполнением задач.
        //services.AddHostedService<Informing>();


        ////────┤ rt-pc-025 (prod-db) ├────────────────────────────────────────────────────────────────

        ////  Добавление службы, выполняющей нормализацию файлов с исходными данными.
        //services.AddHostedService<PackageFileNormalization>();

        ////  Добавление службы, выполняющей поиск файлов с исходными данными.
        //services.AddHostedService<PackageFileSearch>();

        ////  Добавление службы, выполняющей поиск пакетов с исходными данными.
        ////  Дублирование.
        //services.AddHostedService<PackageSearch>();



        ////────┤ snickers ├────────────────────────────────────────────────────────────────

        ////  Добавление службы, выполняющей поиск файлов с NMEA данными.
        //services.AddHostedService<NmeaFileSearch>();


        ////────┤ twix ├────────────────────────────────────────────────────────────────

        ////  Добавление службы, выполняющей поиск NMEA сообщений.
        //services.AddHostedService<NmeaMessageSearch>();

        ////  Добавление службы, выполняющей сбор данных геолокации.
        //services.AddHostedService<GeolocationCollector>();


        ////────┤ bounty ├────────────────────────────────────────────────────────────────


        ////────┤ rail-nto-tst-01 ├────────────────────────────────────────────────────────────────


        ////────┤ rt-pc-007 ├────────────────────────────────────────────────────────────────

        ////  Добавление службы, выполняющей поиск пакетов с исходными данными.
        //services.AddHostedService<PackageSearch>();


        ////────┤ rt-pc-014 ├────────────────────────────────────────────────────────────────

        ////  Добавление службы, выполняющей сборку кадров.
        //services.AddHostedService<FrameCollector>();


        ////────┤ rt-pc-015 ├────────────────────────────────────────────────────────────────

        ////  Добавление службы выполняющей экстремальный анализ.
        //services.AddHostedService<ExtremumAnalysis>();


        ////────┤ rt-pc-017 ├────────────────────────────────────────────────────────────────

        ////  Добавление исполнителя, выполняющего статистический анализ.
        //services.AddPerformer<OrioleStatisticalAnalysis>();


        ////────┤ rt-pc-001 ├────────────────────────────────────────────────────────────────


        ////────┤ rt-pc-022 ├────────────────────────────────────────────────────────────────

        ////  Добавление службы, выполняющей спектральный анализ.
        //services.AddHostedService<SpectrumAnalysis>();


        ////────┤ mars ├────────────────────────────────────────────────────────────────






    })
    .Build();

//  Запуск службы.
await host.RunAsync().ConfigureAwait(false);
