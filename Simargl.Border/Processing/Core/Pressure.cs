namespace Simargl.Border.Processing.Core;

/// <summary>
/// Представляет нажим.
/// </summary>
public readonly struct Pressure
{
    /// <summary>
    /// Возвращает или инициализирует начальный индекс.
    /// </summary>
    public required int Begin { get; init; }

    /// <summary>
    /// Возвращает или инициализирует конечный индекс.
    /// </summary>
    public required int End { get; init; }
}
