namespace Simargl.Projects.Egypt.EgyptXXX.Converter;

/// <summary>
/// Главный раздел конфигурации Measurement.
/// </summary>
public sealed class MeasurementOptions
{
    /// <summary>
    /// Источники измерений.
    /// </summary>
    public List<MeasurementSource> Sources { get; set; } = new();
}

/// <summary>
/// Описание одного источника данных.
/// </summary>
public sealed class MeasurementSource
{
    /// <summary>
    /// Тип источника (например, "Adxl" или "Strain").
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Серийный номер устройства.
    /// </summary>
    public string Serial { get; set; } = string.Empty;

    /// <summary>
    /// Адрес (IP или MAC) источника данных.
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Список имён каналов измерения.
    /// </summary>
    public List<string> Names { get; set; } = [];

    /// <summary>
    /// Единицы измерения для каждого канала.
    /// </summary>
    public List<string> Units { get; set; } = [];

    /// <summary>
    /// Множители (масштабы) для каждого канала.
    /// </summary>
    public List<double> Scales { get; set; } = [];
}
