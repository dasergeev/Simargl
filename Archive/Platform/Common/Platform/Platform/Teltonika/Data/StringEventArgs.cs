namespace Apeiron.Platform.Teltonika;


/// <summary>
/// Представляет аргумент события, данные которого в строковом виде.
/// </summary>
public sealed class StringEventArgs:EventArgs
{
    /// <summary>
    /// Возвращает данные.
    /// </summary>
    public string Data { get; private set; }

    /// <summary>
    /// Инициализирует объект.
    /// </summary>
    /// <param name="data">
    /// Данные.
    /// </param>
    public StringEventArgs(string data)
    {
        // Устанавливает данные.
        Data = data;
    }
}
