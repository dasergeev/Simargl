namespace Simargl.Mathematics.Projects.Border;

/// <summary>
/// Значение, определяющее характер сигнала.
/// </summary>
public enum TrendNoiseEstimationOutputDataCharacter
{
    /// <summary>
    /// Полезный сигнал.
    /// </summary>
    NoNoise,

    /// <summary>
    /// Возможно, это шум.
    /// </summary>
    MayBeNoise,

    /// <summary>
    /// Шум.
    /// </summary>
    Noise,
}
