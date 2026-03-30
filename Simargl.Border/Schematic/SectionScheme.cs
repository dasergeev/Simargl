namespace Simargl.Border.Schematic;

/// <summary>
/// Представляет схему сечения.
/// </summary>
public sealed class SectionScheme
{
    /// <summary>
    /// Возвращает или инициализирует номер.
    /// </summary>
    public required int Number { get; init; }

    /// <summary>
    /// Возвращает или инициализирует положение.
    /// </summary>
    public required double Position { get; init; }
}
