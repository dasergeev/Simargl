namespace Simargl.Border.Hardware;

/// <summary>
/// Представляет статистический параметр устройства.
/// </summary>
public sealed class DeviceStatisticalParameter
{
    /// <summary>
    /// Возвращает имя параметра.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Возвращает значение параметра.
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает единицу измерения параметра.
    /// </summary>
    public required string Unit { get; init; }
}
