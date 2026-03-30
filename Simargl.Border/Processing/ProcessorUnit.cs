namespace Simargl.Border.Processing;

/// <summary>
/// Представляет блок устройства обработки.
/// </summary>
/// <param name="processor">
/// Устройство обработки.
/// </param>
public abstract class ProcessorUnit(Processor processor)
{
    /// <summary>
    /// Вовзвращает устройство обработки.
    /// </summary>
    protected Processor Processor { get; } = processor;
}
