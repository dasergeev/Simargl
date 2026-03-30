using TeltonikaService;
using System.Runtime.InteropServices;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    }).UseEnvironment("Prodaction")  // Задаёт режим запуска приложения
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddSimpleConsole(options =>
        {
            options.IncludeScopes = false;
            options.SingleLine = true;
            options.UseUtcTimestamp = false;
            options.TimestampFormat = "HH:mm:ss dd.MM.yyyy ";
        });
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))    // Если запущено в Windows то добавляем ведение лога в журнал.
        {
            logging.AddEventLog();
        }
    })
    .UseWindowsService().UseSystemd()   // Добавляем режим запуска службы.
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        Worker.Configuration = configuration.GetSection("Options").Get<Options>()!;
    }).Build();

await host.RunAsync();