namespace RailTest.Logging;

/// <summary>
/// Представляет средство для ведения журнала в консоль.
/// </summary>
public class ConsoleLogger : Logger
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="sender">
    /// Имя отправителя.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sender"/> передана пустая ссылка.
    /// </exception>
    public ConsoleLogger(string sender) :
        base(sender)
    {

    }

    /// <summary>
    /// Регистрирует строку журнала.
    /// </summary>
    /// <param name="row">
    /// Строка журнала.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="row"/> передана пустая ссылка.
    /// </exception>
    protected override void Log(LoggerRow row)
    {
        //  Проверка ссылки на строку журнала.
        IsNotNull(row, nameof(row));

        //  Вывод в консоль.
        Console.WriteLine($"[{row.Time}][{row.Sender}] {row.Message}");
    }
}
