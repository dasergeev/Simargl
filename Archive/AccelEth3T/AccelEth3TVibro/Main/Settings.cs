namespace Simargl.AccelEth3T;

/// <summary>
/// Предоставляет общие настройки.
/// </summary>
public static class Settings
{
    /// <summary>
    /// Постоянная, определяющая частоту дискретизации сигнала.
    /// </summary>
    public const int SignalSampling = 4000;

    /// <summary>
    /// Постоянная, определяющая длительность сигнала.
    /// </summary>
    public const int SignalDuration = 5;

    /// <summary>
    /// Постоянная, определяющая длину сигнала.
    /// </summary>
    public const int SignalLength = SignalDuration * SignalSampling;

    /// <summary>
    /// Постоянная, определяющая длину подсигнала спектра.
    /// </summary>
    public const int SpectrumSignalLength = SignalSampling >> 2;

    /// <summary>
    /// Постоянная, определяющая длину новых данных, при которых обновляется спектр.
    /// </summary>
    public const int SpectrumUpdateLength = SignalSampling >> 2;

    /// <summary>
    /// Постоянная, определяющая квантиль сигнала.
    /// </summary>
    public const double SignalQuantile = 3.719; //2.326;

    /// <summary>
    /// Постоянная, определяющая квантиль спектра.
    /// </summary>
    public const double SpectrumQuantile = 3.719; //2.326;

    /// <summary>
    /// Постоянная, определяющая квантиль мощности.
    /// </summary>
    public const double PowerQuantile = 3.719; //2.326;

    /// <summary>
    /// Постоянная, определяющая интервал контроля в секундах.
    /// </summary>
    public const double ControlInterval = 5;
}
