global using Microsoft.EntityFrameworkCore;
global using Apeiron.Platform.Server.Microservices;
using Apeiron.Platform.Server.Services.Microservices;
using System.Runtime.InteropServices;
using Apeiron.Platform.Databases.CentralDatabase;
using Apeiron.Platform.Databases.CentralDatabase.Entities;
using Apeiron.Platform.Performers;
using Apeiron.Platform.Performers.OsmPerformers;
using Apeiron.Platform.Performers.Ape90Performers;
using Apeiron.Platform.Performers.OrioleKmtPerformers;

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
        //  Получение имени компьютера.
        string name = Environment.MachineName.ToLower();

        //  Загрузка информации о компьютере из базы данных.
        HostComputerInfo? computer = CentralDatabaseAgent.Request(
            session => session.HostComputerInfos
                .Where(computer => computer.Name == name)
                .FirstOrDefault());

        //  Проверка информации о компьютере.
        if (computer is null)
        {
            //  Не удалось загрузить информацию из базы данных.
            throw new InvalidOperationException("Не удалось загрузить из базы данных информацию о компьютере.");
        }

        //  Установка времени ожидания ответа от базы данных.
        CentralDatabaseAgent.Logic.CommandTimeout = TimeSpan.FromHours(1);

        //  Установка имени приложения.
        CentralDatabaseAgent.Logic.Connection.ApplicationName += $"({name})";

        //  Получение информации о запускаемых микрослужбах.
        foreach (MicroserviceLaunchInfo launch in computer.Launches)
        {
            //  Проверка необходимости запуска микрослужбы.
            if (launch.IsEnable)
            {
                //  Определение и запуск микрослужбы.
                switch (launch.Microservice.Name)
                {
                    case "FileSearch":
                        //  Добавление микрослужбы, выполняющей поиск файлов.
                        services.AddHostedService<FileSearch>();
                        break;
                    case "FileMetrizator":
                        //  Добавление микрослужбы, выполняющей расчёт метрик файлов.
                        services.AddHostedService<FileMetrizator>();
                        break;
                    case "DefiningFileFormats":
                        //  Добавление микрослужбы, определяющей форматы файлов.
                        services.AddHostedService<DefiningFileFormats>();
                        break;
                    case "FrameSearch":
                        //  Добавление микрослужбы, выполняющей поиск кадров регистрации.
                        services.AddHostedService<FrameSearch>();
                        break;
                    case "StatisticalChannelAnalysis":
                        //  Добавление микрослужбы, выполняющей статистический анализ каналов.
                        services.AddHostedService<StatisticalChannelAnalysis>();
                        break;
                    case "ChannelSearch":
                        //  Добавление микрослужбы, выполняющей поиск каналов кадров регистрации.
                        services.AddHostedService<ChannelSearch>();
                        break;
                    case "FrameAnalysis":
                        //  Добавление микрослужбы, выполняющей анализ кадров регистрации.
                        services.AddHostedService<FrameAnalysis>();
                        break;
                    case "FileVisor":
                        //  Добавление микрослужбы, выполняющей поиск файлов регистрации (изначально для проекта РА-2)
                        services.AddHostedService<FileVisor>();
                        break;
                    case "Ra2Analysis":
                        //  Добавление микрослужбы, выполняющей поиск файлов регистрации (изначально для проекта РА-2)
                        services.AddHostedService<Ra2Analysis>();
                        break;
                    case "BaseProcessor":
                        //  Добавление микрослужбы, выполняющей базовую обработку
                        services.AddHostedService<BaseProcessor>();
                        break;
                    case "Ape90SearchRawFiles":
                        //  Добавление микрослужбы, выполняющей поиск исходных файлов по проекту АПЭ-90.
                        services.AddPerformer<Ape90SearchRawFiles>();
                        break;
                    case "Ape90DefinitionRawFileMetrics":
                        //  Добавление микрослужбы, определяющей метрики исходных файлов по проекту АПЭ-90.
                        services.AddPerformer<Ape90DefinitionRawFileMetrics>();
                        break;
                    case "Ape90NormalizationRawFrameTime":
                        //  Добавление микрослужбы, выполняющей нормализацию времени исходных кадров регистрации по проекту АПЭ-90.
                        services.AddPerformer<Ape90NormalizationRawFrameTime>();
                        break;
                    case "Ape90AnalysisGeolocationFiles":
                        //  Добавление микрослужбы, выполняющей анализ геолокационных файлов по проекту АПЭ-90.
                        services.AddPerformer<Ape90AnalysisGeolocationFiles>();
                        break;
                    case "Ape90GeolocationCollector":
                        //  Добавление микрослужбы, выполняющей сбор геолокационных данных по проекту АПЭ-90.
                        services.AddPerformer<Ape90GeolocationCollector>();
                        break;
                    case "Ape90FrameCollector":
                        //  Добавление микрослужбы, выполняющей сбор кадров регистрации по проекту АПЭ-90.
                        services.AddPerformer<Ape90FrameCollector>();
                        break;
                    case "Ape90Preprocessing":
                        //  Добавление микрослужбы, выполняющей предобработку данных.
                        services.AddPerformer<Ape90Preprocessing>();
                        break;
                    case "Ape90LongFrameCollector":
                        //  Добавление микрослужбы, выполняющей сборку длинных кадров.
                        services.AddPerformer<Ape90LongFrameCollector>();
                        break;
                    case "OsmFileVisor":
                        //  Добавление исполнителя, выполняющего поиск файлов OSM-данных.
                        services.AddPerformer<OsmFileVisor>();
                        break;
                    case "OsmNodeLoader":
                        //  Добавление исполнителя, выполняющего загрузку узлов OSM-карт (Nodes) в базу.
                        services.AddPerformer<OsmNodeLoader>();
                        break;
                    case "OrioleKmtFileVisor":
                        //  Добавление исполнителя, выполняющего загрузку узлов OSM-карт (Nodes) в базу.
                        services.AddPerformer<OrioleKmtFileVisor>();
                        break;
                    case "OrioleKmtNormalizationRawFrame":
                        //  Добавление исполнителя, выполняющего загрузку узлов OSM-карт (Nodes) в базу.
                        services.AddPerformer<OrioleKmtNormalizationRawFrame>();
                        break;
                    case "OrioleKmtNormalizationRawFrameTime":
                        //  Добавление исполнителя, выполняющего загрузку узлов OSM-карт (Nodes) в базу.
                        services.AddPerformer<OrioleKmtNormalizationRawFrameTime>();
                        break;
                    case "OrioleKmtProcessing":
                        //  Добавление исполнителя, выполняющего загрузку узлов OSM-карт (Nodes) в базу.
                        services.AddPerformer<OrioleKmtProcessing>();
                        break;
                    default:
                        break;
                }
            }
        }

        ////  Добавление микрослужбы, выполняющей поиск файлов.
        //services.AddHostedService<FileSearch>();

        ////  Добавление микрослужбы, выполняющей расчёт метрик файлов.
        //services.AddHostedService<FileMetrizator>();

        ////  Добавление микрослужбы, определяющей форматы файлов.
        //services.AddHostedService<DefiningFileFormats>();

        ////  Добавление микрослужбы, выполняющей поиск кадров регистрации.
        //services.AddHostedService<FrameSearch>();

        ////  Добавление микрослужбы, выполняющей статистический анализ каналов.
        //services.AddHostedService<StatisticalChannelAnalysis>();

        ////  Добавление микрослужбы, выполняющей поиск каналов кадров регистрации.
        //services.AddHostedService<ChannelSearch>();

        ////  Добавление микрослужбы, выполняющей анализ кадров регистрации.
        //services.AddHostedService<FrameAnalysis>();
    })
    .Build();

//  Запуск приложения.
await host.RunAsync().ConfigureAwait(false);
