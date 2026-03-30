namespace Simargl.AccelEth3T;

/// <summary>
/// Представляет нарушение.
/// </summary>
/// <param name="actual">
/// Фактическое значение.
/// </param>
/// <param name="permissible">
/// Допустимое значение.
/// </param>
/// <param name="count">
/// Количество нарушений.
/// </param>
public class Violation(double actual, double permissible, int count)
{
    /// <summary>
    /// Возвращает время нарушения.
    /// </summary>
    public DateTime Time { get; } = DateTime.Now;

    /// <summary>
    /// Возвращает фактическое значение.
    /// </summary>
    public double Actual { get; } = actual;

    /// <summary>
    /// Возвращает допустимое значение.
    /// </summary>
    public double Permissible { get; } = permissible;

    /// <summary>
    /// Возвращает количество нарушений.
    /// </summary>
    public int Count { get; } = count;
}
