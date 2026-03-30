namespace Simargl.Recording.AccelEth3T;

/// <summary>
/// Представляет информацию о пакете данных датчика AccelEth3T.
/// </summary>
/// <param name="time">
/// Время начала регистрации данных пакета.
/// </param>
/// <param name="duration">
/// Длительность регистрации данных пакета.
/// </param>
/// <param name="package">
/// Пакет данных.
/// </param>
public sealed class AccelEth3TDataPackageInfo(
    DateTime time, TimeSpan duration, AccelEth3TDataPackage package)
{
    /// <summary>
    /// Возвращает время начала регистрации данных пакета.
    /// </summary>
    public DateTime Time { get; } = time;

    /// <summary>
    /// Возвращает длительность регистрации данных пакета.
    /// </summary>
    public TimeSpan Duration { get; } = duration;

    /// <summary>
    /// Возвращает пакет данных.
    /// </summary>
    public AccelEth3TDataPackage Package { get; } = IsNotNull(package);
}
