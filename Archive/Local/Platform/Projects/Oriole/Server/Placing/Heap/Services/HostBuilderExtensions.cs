using Apeiron.Oriole.Server.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Apeiron.Oriole.Server.Services;

/// <summary>
/// Предоставляет методы расширения для интерфейса <see cref="IHostBuilder"/>.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Выполняет настройку инициализации программы с одной службой.
    /// </summary>
    /// <typeparam name="THostedService">
    /// Тип службы.
    /// </typeparam>
    /// <param name="hostBuilder">
    /// Инициализатор программы.
    /// </param>
    /// <returns>
    /// Инициализатор программы.
    /// </returns>
    public static IHostBuilder UseSingleService
        <[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] THostedService>
        (this IHostBuilder hostBuilder)
         where THostedService : class, IHostedService
    {
        return hostBuilder.UseEnvironment("Prodaction")           // Задаёт режим запуска приложения необходимо для чтения правильного файла настроек.
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
                //  Добавление службы.
                services.AddHostedService<THostedService>();
            });
    }
}
