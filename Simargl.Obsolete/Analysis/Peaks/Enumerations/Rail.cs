namespace Simargl.Analysis.Peaks;

/// <summary>
/// Представляет значение, определяющее рельс.
/// </summary>
public enum Rail
{
    /// <summary>
    /// Внутренний рельс.
    /// </summary>
    Inner = 0,

    /// <summary>
    /// Наружный рельс.
    /// </summary>
    Outer = 1,

    /// <summary>
    /// Остряк стрелочного перевода.
    /// </summary>
    Wit = 2,

    /// <summary>
    /// Неизвестный рельс.
    /// </summary>
    Unknown = 3,
}
