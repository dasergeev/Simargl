namespace Simargl.Hardware.Strain.Demo.Main.Properties;

/// <summary>
/// Представляет атрибут свойства.
/// </summary>
/// <param name="number">
/// Номер свойства.
/// </param>
/// <param name="format">
/// Значение, определяющее формат свойства датчика.
/// </param>
/// <param name="start">
/// Номер первого регистра.
/// </param>
/// <param name="count">
/// Количество регистров.
/// </param>
/// <param name="name">
/// Имя свойства.
/// </param>
/// <param name="description">
/// Описание свойства.
/// </param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class SensorPropertyAttribute(
    int number,
    SensorPropertyFormat format,
    int start, int count,
    string name, string description) :
    Attribute
{
    /// <summary>
    /// Возвращает номер свойства.
    /// </summary>
    public int Number { get; } = number;

    /// <summary>
    /// Возвращает значение, определяющее формат свойства датчика.
    /// </summary>
    public SensorPropertyFormat Format { get; } = format;

    /// <summary>
    /// Возвращает номер первого регистра.
    /// </summary>
    public int Start { get; } = start;

    /// <summary>
    /// Возвращает количество регистров.
    /// </summary>
    public int Count { get; } = count;

    /// <summary>
    /// Возвращает имя свойства.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Возвращает описание свойства.
    /// </summary>
    public string Description { get; } = description;
}
