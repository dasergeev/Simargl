using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace Simargl.Synergy.Transferring.Client;

/// <summary>
/// Предоставляет точку входа приложения.
/// </summary>
public static class Program
{
    /// <summary>
    /// Точка входа приложения.
    /// </summary>
    /// <param name="arguments">
    /// Аргументы командной строки.
    /// </param>
    public static void Main(string[] arguments)
    {
        //  Создание инициализатора службы.
        IHostBuilder builder = Host.CreateDefaultBuilder(arguments);

        //  Задание режима запуска приложения для чтения правильного файла настроек.
        builder.UseEnvironment("Prodaction");

        //  Настройка средства ведения журнала.
        builder.ConfigureLogging(
            delegate (ILoggingBuilder loggingBuilder)
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
            });

        //  Добавление режима запуска службы в Windows.
        builder.UseWindowsService();

        //  Добавление режима запуска службы в Linux.
        builder.UseSystemd();

        //  Настройка служб.
        builder.ConfigureServices(
            delegate (HostBuilderContext context, IServiceCollection services)
            {
                //  Получение конфигурации приложения.
                IConfiguration configuration = context.Configuration;

                //  Загрузка настроек.
                Tunings tunings = configuration.GetSection("Tunings").Get<Tunings>()!;

                //  Добавление уникального объекта.
                services.AddSingleton(tunings);

                //  Добавление основного фонового процесса.
                services.AddHostedService<Worker>();
            });

        //  Построение службы.
        IHost host = builder.Build();

        //  Запуск приложения.
        host.Run();
    }
}
