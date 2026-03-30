using Microsoft.Extensions.DependencyInjection.Extensions;
using Simargl.Detecting.Bearing.Simulator.Data;
using Simargl.Detecting.Bearing.Simulator.Generating;
using Simargl.Detecting.Bearing.Simulator.Logging;
using Simargl.Detecting.Bearing.Simulator.Services;

namespace Simargl.Detecting.Bearing.Simulator;

/// <summary>
/// Представляет приложение.
/// </summary>
partial class App
{
    /// <summary>
    /// Возвращает контейнер зависимостей, конфигурации, логирования и зарегистрированных служб.
    /// </summary>
    public IHost Host { get; }

    /// <summary>
    /// Поле для хранения источника токена отмены.
    /// </summary>
    private readonly CancellationTokenSource _CancellationTokenSource;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public App()
    {
        //  Создание источника токена отмены.
        _CancellationTokenSource = new();

        //  Установка токена отмены.
        CancellationToken = _CancellationTokenSource.Token;

        //  Создание хоста.
        Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()

            //  Регистрация служб.
            .ConfigureServices((context, services) =>
            {
                //  Регистрация основных служб.
                services.AddHostedService<Worker>();

                services.AddSingleton<UILogSink>();

                services.AddSingleton<ILoggerProvider, UILoggerProvider>();

                //  Регистрация уникального экземпляра окна приложения.
                services.AddSingleton<MainWindow>();

                //  Добавление общих данных.
                services.AddSingleton<DataHub>();
                services.AddSingleton<BearingGenerator>();
            })

            //  Настройка логирования.
            .ConfigureLogging(logging =>
            {
                //  Удаление стандартных провайдеров.
                logging.ClearProviders();

                //  Вывод логов в окно Debug Visual Studio.
                logging.AddDebug();

                logging.Services.TryAddEnumerable(
                    ServiceDescriptor.Singleton<ILoggerProvider, UILoggerProvider>());
            })

            //  Построение экземпляра.
            .Build();

        //  Инициализация основных компонентов.
        InitializeComponent();
    }

    /// <summary>
    /// Возвращает токен отмены.
    /// </summary>
    public CancellationToken CancellationToken { get; }

    /// <summary>
    /// Вызывает событие запуска приложения.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override async void OnStartup(StartupEventArgs e)
    {
        //  Запуск всех зарегистрированных служб.
        await Host.StartAsync();

        //  Получение главного окна приложения.
        MainWindow window = Host.Services.GetRequiredService<MainWindow>();

        //  Отображение главного окна приложения.
        window.Show();

        //  Вызов метода базового класса.
        base.OnStartup(e);
    }

    /// <summary>
    /// Вызывает событие выхода из приложения.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override async void OnExit(ExitEventArgs e)
    {
        //  Блок перехвата всех исключений.
        try
        {
            //  Разрушение источника токена отмены.
            _CancellationTokenSource.Dispose();
        }
        catch { }

        //  Остановка всех служб.
        await Host.StopAsync();

        //  Вызов метода базового класса.
        base.OnExit(e);
    }
}
