using Simargl.Embedded.Tunings.TuningsEditor.Core;
using Simargl.Embedded.Tunings.TuningsEditor.Logging;
using Simargl.Embedded.Tunings.TuningsEditor.Services;

namespace Simargl.Embedded.Tunings.TuningsEditor;

/// <summary>
/// Представляет приложение.
/// </summary>
partial class App
{
    private IHost? _Host;

    /// <summary>
    /// 
    /// </summary>
    protected override async void OnStartup(StartupEventArgs e)
    {
        //  Создание сердца приложения.
        Heart? heart = new(Dispatcher);

        base.OnStartup(e);

        //  Создание инициализатора службы.
        IHostBuilder builder = Host.CreateDefaultBuilder(e.Args);

        //  Настройка средства ведения журнала.
        builder.ConfigureLogging(
            delegate (ILoggingBuilder loggingBuilder)
            {
                loggingBuilder.ClearProviders();

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

                //  Добавление средства ведения журнала событий
                //  с именем EventLog в фабрику.
                loggingBuilder.AddEventLog();
            });

        //  Настройка служб.
        builder.ConfigureServices(
            delegate (HostBuilderContext context, IServiceCollection services)
            {
                //  Получение конфигурации приложения.
                IConfiguration configuration = context.Configuration;

                //  Добавление фоновых процессов.
                services.AddHostedService<LogService>();
                services.AddHostedService<GenerationService>();

                services.AddSingleton(heart);

                // регистрируем главное окно через DI
                services.AddSingleton<MainWindow>();

                services.AddSingleton<UILoggerProvider>();
            });

        //  Построение службы.
        _Host = builder.Build();

        var uiLogger = _Host.Services.GetRequiredService<UILoggerProvider>();
        var factory = _Host.Services.GetRequiredService<ILoggerFactory>();
        factory.AddProvider(uiLogger);

        // Запуск инфраструктуры.
        await _Host.StartAsync();

        // Создание и показ главного окна.
        var mainWindow = _Host.Services.GetRequiredService<MainWindow>();
        MainWindow = mainWindow;
        mainWindow.DataContext = heart;
        mainWindow.Show();
    }

    /// <summary>
    /// 
    /// </summary>
    protected override async void OnExit(ExitEventArgs e)
    {
        if (_Host is not null)
        {
            await _Host.StopAsync();
            _Host.Dispose();
        }

        base.OnExit(e);
    }
}
