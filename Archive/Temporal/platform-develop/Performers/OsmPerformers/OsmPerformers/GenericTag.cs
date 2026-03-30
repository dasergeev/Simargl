namespace Apeiron.Platform.Performers.OsmPerformers;

/// <summary>
/// Представляет общий класс для всех подтипов Tag.
/// </summary>
public class GenericTag
{
    /// <summary>
    /// Представляет ID элемента родителя.
    /// </summary>
    public long ParentId { get; set; }

    /// <summary>
    /// Представляет ключ элемента.
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Представялет значение элемента.
    /// </summary>
    public string Value { get; set; } = string.Empty;
}
