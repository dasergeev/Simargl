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
/// <param name="frequency">
/// Частота.
/// </param>
/// <param name="count">
/// Количество превышений.
/// </param>
public sealed class SpectrumViolation(double actual, double permissible, double frequency, int count) :
    Violation(actual, permissible, count)
{
    /// <summary>
    /// Возвращает частоту.
    /// </summary>
    public double Frequency { get; } = frequency;
}
