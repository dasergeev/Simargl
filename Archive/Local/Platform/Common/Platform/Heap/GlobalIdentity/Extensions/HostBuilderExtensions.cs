using Apeiron.Services.GlobalIdentity.Tunings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace Apeiron.Services.GlobalIdentity.Extensions;

/// <summary>
/// Предоставляет методы расширения интерфейса <see cref="IHostBuilder"/>
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Добавляет делегат для настройки инициализации программы.
    /// </summary>
    /// <typeparam name="TTuning">
    /// Тип настроек.
    /// </typeparam>
    /// <param name="hostBuilder">
    /// Объект, выполняющий инициализацию программы.
    /// </param>
    /// <returns>
    /// Объект, выполняющий инициализацию программы.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="hostBuilder"/> передана пустая ссылка.
    /// </exception>
    public static IHostBuilder ConfigureGlobalIdentity<TTuning>(this IHostBuilder hostBuilder)
        where TTuning : Tuning
    {
        //  Проверка ссылки на объект, выполняющий инициализацию программы.
        hostBuilder = Check.IsNotNull(hostBuilder, nameof(hostBuilder));

        // Задание режима запуска приложения,
        // необходимо для чтения правильного файла настроек.
        hostBuilder = hostBuilder.UseEnvironment("Prodaction");

        //  Настройка журнала.
        hostBuilder = hostBuilder.ConfigureLogging(logging =>
        {
            //  Удаление всех поставщиков средств ведения журнала.
            logging.ClearProviders();

            //  Добавление модуля форматирования журнала консоли.
            logging.AddSimpleConsole(options =>
            {
                //  Отключить области.
                options.IncludeScopes = false;

                //  Записывать все сообщения в журнал в одной строке.
                options.SingleLine = false;

                //  Не использовать часовой пояс UTC для меток времени в сообщениях журнала.
                options.UseUtcTimestamp = false;

                //  Строка формата, используемая для форматирования метки времени в сообщениях журнала.
                options.TimestampFormat = "HH:mm:ss dd.MM.yyyy ";
            });

            //  Проверка платформы.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                //  Добавляет средство ведения журнала событий с именем EventLog в фабрику.
                logging.AddEventLog();
            }
        });

        //  Добавление режима запуска службы в Windows.
        hostBuilder = hostBuilder.UseWindowsService();

        //  Добавление режима запуска службы в Linux.
        hostBuilder = hostBuilder.UseSystemd();

        //  Добавление конфигурации служб.
        hostBuilder = hostBuilder.ConfigureServices((hostContext, services) =>
        {
            //  Получение конфигурации службы.
            IConfiguration configuration = hostContext.Configuration;

            //  Добавление конфигурации службы.
            services.AddSingleton(configuration);

            //  Загрузка настроек.
            TTuning tuning = configuration.GetSection(nameof(Tuning)).Get<TTuning>()!;

            //  Проверка настроек.
            Check.IsNotNull(tuning, nameof(tuning)).Validation();

            //  Добавление настроек службы.
            services.AddSingleton(tuning);
        });

        //  Возврат объекта, выполняющего инициализацию программы.
        return hostBuilder;
    }
}
