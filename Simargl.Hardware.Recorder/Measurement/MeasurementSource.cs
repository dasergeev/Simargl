namespace Simargl.Hardware.Recorder.Measurement;

/// <summary>
/// Представляет источник измерения.
/// </summary>
public sealed class MeasurementSource
{
    /// <summary>
    /// Возвращает или задаёт тип источника.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт серийный номер источника.
    /// </summary>
    public string Serial { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт источник.
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт имена источника.
    /// </summary>
    public List<string> Names { get; set; } = [];

    /// <summary>
    /// Возвращает или задаёт единицы измерения.
    /// </summary>
    public List<string> Units { get; set; } = [];

    /// <summary>
    /// Возвращает или задаёт масштабы.
    /// </summary>
    public List<double> Scales { get; set; } = [];
}
