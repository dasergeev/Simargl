using Simargl.Border.Recorder.Components;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Simargl.Border.Recorder.Configuring;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Simargl.Border.Recorder.Services.Critical;
using Simargl.Border.Recorder.Services.Main;
using Simargl.Border.Recorder.Services.Common;
using Microsoft.AspNetCore.Hosting;
using Simargl.Border.Processing;
using System.Reflection;

namespace Simargl.Border.Recorder;

/// <summary>
/// Представляет программу.
/// </summary>
public sealed class Program()
{
    /// <summary>
    /// Представляет точку входа в приложение.
    /// </summary>
    /// <param name="args">
    /// Параметры командной строки.
    /// </param>
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        //  Создание программы.
        Program program = new();

        //  Добавление уникального объекта программы.
        builder.Services.AddSingleton(program);

        //  Добавление основных служб.
        builder.Services.AddHostedService<InitializationService>();
        builder.Services.AddHostedService<TcpListeningService>();
        builder.Services.AddHostedService<SynchronizationService>();
        builder.Services.AddHostedService<ProcessingService>();
        builder.Services.AddHostedService<TransferringService>();
        builder.Services.AddHostedService<ZipService>();
        builder.Services.AddHostedService<ConservationService>();
        builder.Services.AddHostedService<ConvertingService>();
        builder.Services.AddHostedService<CleaningService>();

        builder.Host.UseWindowsService();

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(5000); // слушать все IP
        });

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }

    /// <summary>
    /// Поле для хранения версии ПО.
    /// </summary>
    private string? _Version;

    /// <summary>
    /// Возвращает версию ПО.
    /// </summary>
    public string Verion
    {
        get
        {
            //  Проверка версии.
            if (_Version is not string version)
            {
                //  Получение версии ПО.
                try
                {
                    version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.InformationalVersion.Split('+')[0];
                }
                catch
                {
                    version = string.Empty;
                }

                //  Установка версии ПО.
                _Version = version;
            }

            //  Возврат версии ПО.
            return version;
        }
    }

    /// <summary>
    /// Возвращает источник конфигурации приложения.
    /// </summary>
    private readonly TaskCompletionSource<Configuration> _ConfigurationSource = new();

    /// <summary>
    /// Возвращает источник устройства обработки.
    /// </summary>
    private readonly TaskCompletionSource<Processor> _ProcessorSource = new();

    /// <summary>
    /// Асинхронно возвращает конфигурацию приложения.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая конфигурацию приложения.
    /// </returns>
    public async Task<Configuration> GetConfigurationAsync(CancellationToken cancellationToken)
    {
        //  Получение целевой задачи.
        Task<Configuration> targetTask = _ConfigurationSource.Task;

        //  Проверка выполнения целевой задачи.
        if (targetTask.IsCompleted)
        {
            //  Возврат конфигурации.
            return await targetTask.ConfigureAwait(false);
        }

        //  Создание задачи, завершающейся при отмене.
        Task<Configuration> cancelTask = Task.FromCanceled<Configuration>(cancellationToken);

        //  Ожидание первой завершившейся задачи.
        Task<Configuration> completedTask = await Task.WhenAny(targetTask, cancelTask).ConfigureAwait(false);

        //  Возврат результата.
        return await completedTask.ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно возвращает устройство обработки.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, возвращающая устройство обработки.
    /// </returns>
    public async Task<Processor> GetProcessorAsync(CancellationToken cancellationToken)
    {
        //  Получение целевой задачи.
        Task<Processor> targetTask = _ProcessorSource.Task;

        //  Проверка выполнения целевой задачи.
        if (targetTask.IsCompleted)
        {
            //  Возврат конфигурации.
            return await targetTask.ConfigureAwait(false);
        }

        //  Создание задачи, завершающейся при отмене.
        Task<Processor> cancelTask = Task.FromCanceled<Processor>(cancellationToken);

        //  Ожидание первой завершившейся задачи.
        Task<Processor> completedTask = await Task.WhenAny(targetTask, cancelTask).ConfigureAwait(false);

        //  Возврат результата.
        return await completedTask.ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет инициализацию приложения.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала.
    /// </param>
    /// <param name="baseConfiguration">
    /// Набор конфигурационных свойств приложения.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая инициализацию ядра приложения.
    /// </returns>
    internal async Task InitializationAsync(ILogger logger, IConfiguration baseConfiguration, CancellationToken cancellationToken)
    {
        //  Создание конфигурации приложения.
        Configuration configuration = await Configuration.CreateAsync(
            logger, baseConfiguration, cancellationToken).ConfigureAwait(false);

        //  Установка конфигурации приложения.
        _ConfigurationSource.SetResult(configuration);

        //  Создание устройства обработки.
        Processor processor = await Processor.CreateAsync(cancellationToken).ConfigureAwait(false);

        //  Установка устройства обработки.
        _ProcessorSource.SetResult(processor);
    }
}
