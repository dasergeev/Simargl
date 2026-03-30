using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simargl.Hardware.Recorder.Components;
using Simargl.Hardware.Recorder.Components.Account;
using Simargl.Hardware.Recorder.Core;
using Simargl.Hardware.Recorder.Data;
using Simargl.Hardware.Recorder.Services.Common;
using Simargl.Hardware.Recorder.Services.Sensors;

namespace Simargl.Hardware.Recorder;

/// <summary>
/// Предоставляет точку входа приложения.
/// </summary>
public class Program
{
    /// <summary>
    /// Точка входа приложения.
    /// </summary>
    /// <param name="args">
    /// Аргументы командной строки.
    /// </param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //  Чтение настроек.
        Heart.Unique.Bind(builder.Configuration);

        //  Регистрация сердца приложения.
        builder.Services.AddSingleton(Heart.Unique);

        //  Регистрация фоновых служб.
        builder.Services.AddHostedService<AccelEth3TRecorderService>();
        builder.Services.AddHostedService<GeolocationRecorderService>();
        builder.Services.AddHostedService<StrainOrganizerService>();
        builder.Services.AddHostedService<StrainRecorderService>();
        builder.Services.AddHostedService<IndicatorService>();
        builder.Services.AddHostedService<AdxlService>();
        builder.Services.AddHostedService<StrainService>();
        builder.Services.AddHostedService<GeolocationService>();
        builder.Services.AddHostedService<TransferringService>();
        builder.Services.AddHostedService<RS485RecorderService>();
        builder.Services.AddHostedService<RS485Service>();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        var usePostgres = OperatingSystem.IsLinux();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (usePostgres)
                options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
            else
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerLocal"));
        });

        //builder.Services.AddDbContext<ApplicationDbContext>(options =>
        //    options.UseSqlServer(connectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();


        var app = builder.Build();

        // Автосоздание схемы: локально применяем миграции, на Postgres — создаём таблицы по модели
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (db.Database.IsNpgsql())
                db.Database.EnsureCreated();   // Debian/Postgres: без миграций, просто создать схему
            else
                db.Database.Migrate();         // Debug/SQL Server: как и раньше, твои миграции
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        // Add additional endpoints required by the Identity /Account Razor components.
        app.MapAdditionalIdentityEndpoints();

        app.Run();
    }
}
