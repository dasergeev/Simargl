using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет данные о дне.
/// </summary>
public sealed class DayData
{
    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<DayData> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => x.Key);

        //  Настройка индекса серийного номера.
        typeBuilder.HasIndex(x => x.Timestamp).IsUnique();
    }

    /// <summary>
    /// Возвращает метку времени.
    /// </summary>
    /// <param name="time">
    /// Время.
    /// </param>
    /// <returns>
    /// Метка времени.
    /// </returns>
    public static long ToTimestamp(DateTime time)
    {
        //  Расчёт метки времени.
        return time.Ticks / TimeSpan.TicksPerDay;
    }

    /// <summary>
    /// Возвращает время.
    /// </summary>
    /// <param name="timestamp">
    /// Метка времени.
    /// </param>
    /// <returns>
    /// Время.
    /// </returns>
    public static DateTime FromTimestamp(long timestamp)
    {
        //  Расчёт времени.
        return new(timestamp * TimeSpan.TicksPerDay);
    }

    /// <summary>
    /// Возвращает или инициализирует ключ сущности.
    /// </summary>
    public long Key { get; init; }

    /// <summary>
    /// Возвращает или задаёт метку времени.
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее, собраны ли кадры.
    /// </summary>
    public bool IsCollect { get; set; }

    /// <summary>
    /// Возвращает или задаёт количество файлов Adxl.
    /// </summary>
    public int AdxlCount { get; set; }

    /// <summary>
    /// Возвращает или задаёт количество файлов Nmea.
    /// </summary>
    public int NmeaCount { get; set; }
}
