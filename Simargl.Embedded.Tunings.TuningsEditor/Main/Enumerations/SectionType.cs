namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Enumerations;

/// <summary>
/// Представляет значение, определяющее тип раздела спецификации.
/// </summary>
public enum SectionType
{
    /// <summary>
    /// Раздел информации.
    /// </summary>
    Info,

    /// <summary>
    /// Раздел произвольного доступа.
    /// </summary>
    Separate,

    /// <summary>
    /// Критический раздел.
    /// </summary>
    Critical,

    /// <summary>
    /// Раздел управления.
    /// </summary>
    Control,

    /// <summary>
    /// Раздел отладки.
    /// </summary>
    Debug,
}
