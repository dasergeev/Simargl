namespace Simargl.Projects.OneTimes.Tiv.Aurora01;

/// <summary>
/// Представляет информацию о датчике Adxl.
/// </summary>
public sealed class AdxlInfo(
    string number, string address,
    string xName, string yName, string zName,
    double xScale, double yScale, double zScale,
    double xOffset, double yOffset, double zOffset)
{
    /// <summary>
    /// Возвращает серийный номер датчика.
    /// </summary>
    public string Number { get; } = number;

    /// <summary>
    /// Возвращает адрес датчика.
    /// </summary>
    public string Address { get; } = address;

    /// <summary>
    /// Возвращает имя канала по оси Ox.
    /// </summary>
    public string XName { get; } = xName;

    /// <summary>
    /// Возвращает имя канала по оси Oy.
    /// </summary>
    public string YName { get; } = yName;

    /// <summary>
    /// Возвращает имя канала по оси Oz.
    /// </summary>
    public string ZName { get; } = zName;

    /// <summary>
    /// Возвращает масштаб канала по оси Ox.
    /// </summary>
    public double XScale { get; } = xScale;

    /// <summary>
    /// Возвращает масштаб канала по оси Oy.
    /// </summary>
    public double YScale { get; } = yScale;

    /// <summary>
    /// Возвращает масштаб канала по оси Oz.
    /// </summary>
    public double ZScale { get; } = zScale;

    /// <summary>
    /// Возвращает смещение канала по оси Ox.
    /// </summary>
    public double XOffset { get; } = xOffset;

    /// <summary>
    /// Возвращает смещение канала по оси Oy.
    /// </summary>
    public double YOffset { get; } = yOffset;

    /// <summary>
    /// Возвращает смещение канала по оси Oz.
    /// </summary>
    public double ZOffset { get; } = zOffset;
}
