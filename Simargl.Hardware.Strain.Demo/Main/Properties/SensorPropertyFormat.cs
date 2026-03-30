namespace Simargl.Hardware.Strain.Demo.Main.Properties;

/// <summary>
/// Представляет значение, определяющее формат свойства датчика.
/// </summary>
public enum SensorPropertyFormat
{
    /// <summary>
    /// Только для чтения.
    /// </summary>
    ReadOnly,

    /// <summary>
    /// Для чтения и записи.
    /// </summary>
    ReadWrite,

    /// <summary>
    /// Сбрасываемый.
    /// </summary>
    Reset,

    /// <summary>
    /// Служебный.
    /// </summary>
    Service,
}
