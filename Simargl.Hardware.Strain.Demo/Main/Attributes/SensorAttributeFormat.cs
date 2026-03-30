namespace Simargl.Hardware.Strain.Demo.Main.Attributes;

/// <summary>
/// Представляет значение, определяющее формат атрибута.
/// </summary>
public enum SensorAttributeFormat
{
    /// <summary>
    /// Атрибут содержит статическое значение.
    /// </summary>
    Static,

    /// <summary>
    /// Атрибут, доступный только для чтения.
    /// </summary>
    Readable,

    /// <summary>
    /// Сбрасываемый атрибут.
    /// </summary>
    Resettable,

    /// <summary>
    /// Записываемый атрибут.
    /// </summary>
    Writable,

    /// <summary>
    /// Выбираемый атрибут.
    /// </summary>
    Selectable,
}
