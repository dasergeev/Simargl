namespace Apeiron.Platform.Utilities.Generation.Models;

/// <summary>
/// Представляет многострочную строку.
/// </summary>
public sealed class MultilineString :
    List<string>
{
    /// <summary>
    /// Возвращает полную строку.
    /// </summary>
    /// <returns>
    /// Полная строка.
    /// </returns>
    public override string ToString()
    {
        //  Возврат полной строки.
        return string.Join('\n', this);
    }
}
