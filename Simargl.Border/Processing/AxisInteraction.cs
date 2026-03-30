using Simargl.Border.Processing.Core;

namespace Simargl.Border.Processing;

/// <summary>
/// Представляет взаимодействие оси.
/// </summary>
public sealed class AxisInteraction
{
    /// <summary>
    /// Возвращает сечение.
    /// </summary>
    public required int Section { get; init; }

    /// <summary>
    /// Возвращает положение сечения.
    /// </summary>
    public required double Position { get; init; }

    /// <summary>
    /// Возвращает нажим.
    /// </summary>
    public required Pressure Pressure { get; init; }

    /// <summary>
    /// Возвращает время нажима.
    /// </summary>
    public double Time => 0.5 * (Pressure.Begin + Pressure.End) / 2000.0;

    /// <summary>
    /// Возвращает или задаёт скорость.
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// Возвращает или задаёт длину данных.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму скоростей.
    /// </summary>
    public double SpeedSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму квадратов скоростей.
    /// </summary>
    public double SpeedSquaresSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт среднюю скорость.
    /// </summary>
    public double SpeedAverage { get; set; }

    /// <summary>
    /// Возвращает или задаёт СКО скорости.
    /// </summary>
    public double SpeedDeviation { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумуу левых значений.
    /// </summary>
    public double LeftSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму квадратов левых значений.
    /// </summary>
    public double LeftSquaresSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт среднее левое значение.
    /// </summary>
    public double LeftAverage { get; set; }

    /// <summary>
    /// Возвращает или задаёт СКО левых значений.
    /// </summary>
    public double LeftDeviation { get; set; }

    /// <summary>
    /// Возвращает или задаёт максимальное левое значение.
    /// </summary>
    public double LeftMax { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму правых значений.
    /// </summary>
    public double RightSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт сумму квадратов правых значений.
    /// </summary>
    public double RightSquaresSum { get; set; }

    /// <summary>
    /// Возвращает или задаёт максимальное правое значение.
    /// </summary>
    public double RightMax { get; set; }

    /// <summary>
    /// Возвращает или задаёт среднее правое значение.
    /// </summary>
    public double RightAverage { get; set; }

    /// <summary>
    /// Возвращает или задаёт СКО правых значений.
    /// </summary>
    public double RightDeviation { get; set; }
}
