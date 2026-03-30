namespace Apeiron.Platform.Databases.OrioleDatabase.Entities;

/// <summary>
/// Представляет амплитуду сигнала.
/// </summary>
public class Amplitude
{
    /// <summary>
    /// Возвращает или инициализирует идентификатор амплитуды.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Возвращает или задаёт идентификатор спектра.
    /// </summary>
    public long SpectrumId { get; init; }

    /// <summary>
    /// Возвращает или задаёт частоту.
    /// </summary>
    public double Frequency { get; set; }

    /// <summary>
    /// Возвращает или задаёт действительную часть амплитуды.
    /// </summary>
    public double Real { get; set; }

    /// <summary>
    /// Возвращает или задаёт мнимую часть амплитуды.
    /// </summary>
    public double Imaginary { get; set; }

    /// <summary>
    /// Возвращает или задаёт абсолютное значение амплитуды.
    /// </summary>
    public double Magnitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт фазу амплитуды.
    /// </summary>
    public double Phase { get; set; }

    /// <summary>
    /// Возвращает или задаёт спектр.
    /// </summary>
    public Spectrum Spectrum { get; set; } = null!;
}
