namespace Simargl.Hardware.Recorder.Measurement;

/// <summary>
/// Представляет настройки измерения.
/// </summary>
public sealed class MeasurementOptions
{
    /// <summary>
    /// Возвращает или задаёт список источников.
    /// </summary>
    public List<MeasurementSource> Sources { get; set; } = [];
}
