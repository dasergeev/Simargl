namespace Simargl.Border.Graphics;

/// <summary>
/// Представляет точку.
/// </summary>
/// <param name="time">
/// Время.
/// </param>
/// <param name="y">
/// Координата по оси Oy.
/// </param>
public class PointOld(DateTime time, double y)
{
    /// <summary>
    /// Возвращает координату по оси Ox.
    /// </summary>
    public double X { get; private set; }

    /// <summary>
    /// Возвращает координату по оси Oy.
    /// </summary>
    public double Y { get; } = y;
    
    /// <summary>
    /// Обновляет значения.
    /// </summary>
    /// <param name="now">
    /// Текущее время.
    /// </param>
    public PointOld Update(DateTime now)
    {
        //  Обновление значения.
        X = (time - now).TotalSeconds;

        //  Возврат точки.
        return this;
    }
}
