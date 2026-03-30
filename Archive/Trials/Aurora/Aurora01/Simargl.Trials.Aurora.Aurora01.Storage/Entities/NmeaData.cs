using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Trials.Aurora.Aurora01.Storage.Entities;

/// <summary>
/// Представляет данные Nmea.
/// </summary>
public class NmeaData
{
    /// <summary>
    /// Возвращает или задаёт метку времени.
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт скорость в км/ч.
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// Возвращает или задаёт широту.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт долготу.
    /// </summary>
    public double Longitude { get; set; }

    ///// <summary>
    ///// Возвращает или задёт поколение данных.
    ///// </summary>
    //public NmeaGeneration Generation { get; set; }

    ///// <summary>
    ///// Возвращает или задаёт расчётную скорость по GPS-координатам в км/ч.
    ///// </summary>
    //public double GpsSpeed { get; set; }

    ///// <summary>
    ///// Возвращает индекс фрагмента.
    ///// </summary>
    //public int FragmentIndex { get; set; }

    /// <summary>
    /// Возвращает время.
    /// </summary>
    /// <param name="timestamp">
    /// Метка времени.
    /// </param>
    /// <returns>
    /// Время.
    /// </returns>
    public static DateTime ToTime(long timestamp) => new(timestamp * TimeSpan.TicksPerSecond);

    /// <summary>
    /// Преобразует время.
    /// </summary>
    /// <param name="time">
    /// Время.
    /// </param>
    /// <returns>
    /// Метка времени.
    /// </returns>
    public static long FromTime(DateTime time)
    {
        //  Нормализация времени.
        time = new(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);

        //  Возврат метки времени.
        return time.Ticks / TimeSpan.TicksPerSecond;
    }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<NmeaData> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => x.Timestamp);

        //  Отключение автоинкремента.
        typeBuilder.Property(x => x.Timestamp).ValueGeneratedNever();
    }
}
