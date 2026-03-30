using Simargl.Server.Hub.Components;
using Simargl.Server.Hub.Core;

namespace Simargl.Server.Hub;

/// <summary>
/// Предоставляет точку входа приложения.
/// </summary>
public sealed class Program
{
    /// <summary>
    /// Точка входа приложения.
    /// </summary>
    /// <param name="args">
    /// Праметры командной строки:
    /// массив строк, куда попадают аргументы командной строки,
    /// переданные при запуске (например, порты, URL или настройки среды).
    /// </param>
    public static void Main(string[] args)
    {
        //  Создание объекта для настройки и запуска веб-приложения.
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        //  Регистрация в DI-контейнере поддержки Razor Components (Blazor).
        builder.Services
            //  Включение системы компонентов.
            .AddRazorComponents()
            //  Активация режима Blazor Server,
            //  при котором компоненты работают на сервере и общаются с браузером
            //  через SignalR (постоянное двустороннее соединение).
            .AddInteractiveServerComponents();


        // регистрации ДО builder.Build():
        builder.Services.AddSingleton<HostedServicesRegistry>();
        builder.Services.AddSingleton<MemoryLogProvider>();
        builder.Services.AddSingleton<ILoggerProvider>(sp => sp.GetRequiredService<MemoryLogProvider>());
        // это добавляет наш провайдер в систему логирования той же самой инстанцией

        //  Регистрация фоновых служб.
        builder.Services.AddHostedService<TestService>();


        builder.Services.DecorateAllHostedServices();
        // это «подменит» все IHostedService в DI на TrackingHostedService(..)
        //    — без правки их исходников, в том числе чужих



        //

        //  Создание объекта приложения.
        WebApplication app = builder.Build();

        //  Проверка окружения.
        if (!app.Environment.IsDevelopment())
        {
            //  Настройка обработчика ошибок:
            //  при ошибке пользователя перенаправит на страницу /Error.
            app.UseExceptionHandler("/Error");
        }

        //  Подключение защиты от CSRF-атак:
        //  выполняется проверка antiforgery-токенов в запросах, чтобы нельзя было подделать POST.
        app.UseAntiforgery();

        //  Включение отдачи статических файлов из wwwroot (CSS, JS, картинки).
        app.MapStaticAssets();

        // Регистрация корневого компонента App.razor.
        app.MapRazorComponents<App>()
            //  Включение работы Blazor Server (живой UI через SignalR).
            .AddInteractiveServerRenderMode();

        //  Запуск веб-сервера Kestrel и начало прослушивания HTTP-запросов.
        app.Run();
    }
}
