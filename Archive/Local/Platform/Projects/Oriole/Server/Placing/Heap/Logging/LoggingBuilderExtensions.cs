using Microsoft.Extensions.Logging.Console;

namespace Apeiron.Oriole.Server.Logging;

/// <summary>
/// Предоставляет методы расширения для интерфейса <see cref="ILoggingBuilder"/>.
/// </summary>
public static class LoggingBuilderExtensions
{
    /// <summary>
    /// Добавляет настраиваемый модуль форматирования средства ведения журнала консоли.
    /// </summary>
    /// <param name="builder">
    /// Построитель журналов.
    /// </param>
    /// <returns>
    /// Построитель журналов.
    /// </returns>
    public static ILoggingBuilder AddSpecializedConsoleFormatter(this ILoggingBuilder builder)
    {
        //  Проверка ссылки на построитель журнала.
        builder = Check.IsNotNull(builder, nameof(builder));

        //  Добавляет средство ведения журнала консоли в фабрику.
        builder.AddConsole(options => options.FormatterName = SpecializedConsoleFormatter.NameFormatter);

        //  Добавляет настраиваемый модуль форматирования средства ведения журнала консоли.
        builder.AddConsoleFormatter<SpecializedConsoleFormatter, ConsoleFormatterOptions>(_ => { });

        //  Возврат построителя журнала.
        return builder;
    }
}
