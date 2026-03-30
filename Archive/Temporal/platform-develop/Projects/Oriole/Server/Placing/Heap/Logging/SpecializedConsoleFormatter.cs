using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Apeiron.Oriole.Server.Logging;

/// <summary>
/// Представляет специализированный модуль форматирования журнала консоли.
/// </summary>
internal sealed class SpecializedConsoleFormatter :
    ConsoleFormatter
{
    /// <summary>
    /// Постоянная, определяющая имя, связанное с модулем форматирования журнала консоли.
    /// </summary>
    internal const string NameFormatter = "specialized";

    /// <summary>
    /// Постоянная, определяющая длину времени и разделяющих пробелов.
    /// </summary>
    private const int _TimeAndSpacesLength = 10;

    /// <summary>
    /// Постоянная, определяющая длину названия категории.
    /// </summary>
    private const int _CategoryLength = 16;

    /// <summary>
    /// Постоянная, определяющая строку конца блока.
    /// </summary>
    private const string _EndBlock = "\x1b[0m ";

    /// <summary>
    /// Постоянная, определяющая цвет отображения времени.
    /// </summary>
    private const string _TimeColor = "\x1B[36m";    //  "\x1b[38;2;43;145;175m";

    /// <summary>
    /// Постоянная, определяющая цвет имени категории.
    /// </summary>
    private const string _CategoryColor = "\x1B[32m";       //  "\x1b[38;2;78;201;176m";

    /// <summary>
    /// Поле для хранения массива цветов уровней.
    /// </summary>
    private static readonly string[] _LevelColor = new string[]
    {
        "\x1B[36m",         //  Trace       //  "\x1b[38;2;118;118;118m"
        "\x1B[33m",         //  Debug       //  "\x1b[38;2;160;160;160m"
        "\x1B[37m",         //  Information //  "\x1b[38;2;220;220;220m"
        "\x1B[1m\x1B[37m",  //  Warning     //  "\x1b[38;2;86;156;214m"
        "\x1B[1m\x1B[35m",  //  Error       //  "\x1b[38;2;216;80;80m"
        "\x1B[1m\x1B[31m",  //  Critical    //  "\x1b[38;2;238;0;0m"
    };

    /// <summary>
    /// Поле для хранения наполнителя для новой строки.
    /// </summary>
    private static readonly string _NewLineFiller = Environment.NewLine + new string(' ', _TimeAndSpacesLength + _CategoryLength);

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="monitor">
    /// Наблюдатель за изменениями параметров модуля форматирования журнала консоли.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="monitor"/> передана пустая ссылка.
    /// </exception>
    public SpecializedConsoleFormatter(IOptionsMonitor<ConsoleFormatterOptions> monitor) :
        base(NameFormatter)
    {
        //  Проверка ссылки на наблюдателя.
        Check.IsNotNull(monitor, nameof(monitor));
    }

    /// <summary>
    /// Выполняет запись в журнал.
    /// </summary>
    /// <typeparam name="TState">
    /// Тип значения состояния.
    /// </typeparam>
    /// <param name="entry">
    /// Сведения для одной записи в журнал.
    /// </param>
    /// <param name="scope">
    /// Хранилище данных общей области.
    /// </param>
    /// <param name="writer">
    /// Средство записи текстовой информации.
    /// </param>
    public override void Write<TState>(in LogEntry<TState> entry, IExternalScopeProvider? scope, TextWriter writer)
    {
        //  Получение форматированного сообщения.
        string? message = entry.Formatter?.Invoke(entry.State, entry.Exception);

        //  Проверка результата форматирования.
        if (message is null)
        {
            //  Нет данных для вывода в журнал.
            return;
        }

        //  Получение уровня.
        int level = (int)entry.LogLevel;

        //  Проверка уровня.
        if (level < 0 || level > 5)
        {
            //  Уровень не подразумевает вывод в журнал.
            return;
        }

        //  Получение имени категории.
        string category = entry.Category ?? string.Empty;

        {
            //  Определение длины имени категории.
            int length = category.Length;

            //  Проверка длины имени категории.
            if (length > 1)
            {
                //  Получение индекса разделителя имени категории.
                int delimiter = category.LastIndexOf('.', length - 1);

                //  Проверка возможности обрезать имя категории.
                if (delimiter != -1)
                {
                    //  Обрезка имени категории.
                    category = category[(delimiter + 1)..];
                }

                //  Обновление длины имени категории.
                length = category.Length;
            }

            //  Корректировка длины имени категории.
            if (length < _CategoryLength)
            {
                //  Дополнение имени категории.
                category += new string(' ', _CategoryLength - length);
            }
            else if (length > _CategoryLength)
            {
                //  Обрезка имени категории.
                category = category[..(_CategoryLength - 1)] + "'";
            }
        }

        //  Запись времени.
        writer.Write(_TimeColor + $"{DateTime.Now:HH:mm:ss}" + _EndBlock);

        //  Запись категории.
        writer.Write(_CategoryColor + category + _EndBlock);

        //  Запись сообщения.
        writer.Write(_LevelColor[level] + message.Replace(Environment.NewLine, _NewLineFiller) + _EndBlock);

        //  Переход на новую строку.
        writer.Write(Environment.NewLine);
    }
}
