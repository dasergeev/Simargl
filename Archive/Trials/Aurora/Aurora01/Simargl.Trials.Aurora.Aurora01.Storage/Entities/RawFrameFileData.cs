using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Trials.Aurora.Aurora01.Storage.Entities;

/// <summary>
/// Представляет данные файла сырого кадра.
/// </summary>
public class RawFrameFileData
{
    /// <summary>
    /// Возвращает или задаёт метку времени часового каталога.
    /// </summary>
    public int HourDirectoryTimestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт часовой каталог.
    /// </summary>
    public HourDirectoryData HourDirectory { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт метку времени.
    /// </summary>
    public int Timestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее состояние данных файла сырого кадра.
    /// </summary>
    public RawFrameFileState State { get; set; }

    /// <summary>
    /// Возвращает время.
    /// </summary>
    /// <param name="timestamp">
    /// Метка времени.
    /// </param>
    /// <returns>
    /// Время.
    /// </returns>
    public static TimeSpan ToTime(int timestamp) => new(timestamp * TimeSpan.TicksPerMillisecond);

    /// <summary>
    /// Преобразует время.
    /// </summary>
    /// <param name="time">
    /// Время.
    /// </param>
    /// <returns>
    /// Метка времени.
    /// </returns>
    public static int FromTime(TimeSpan time)
    {
        //  Нормализация.
        time = new(0, 0, time.Minutes, time.Seconds, time.Milliseconds);

        //  Возврат метки времени.
        return (int)(time.Ticks / TimeSpan.TicksPerMillisecond);
    }

    /// <summary>
    /// Возвращает имя файла.
    /// </summary>
    /// <returns>
    /// Имя файла.
    /// </returns>
    public string GetFileName()
    {
        //  Определение времени записи.
        DateTime time = HourDirectoryData.ToTime(HourDirectoryTimestamp) + AdxlFileData.ToTime(Timestamp);

        //  Возврат имени файла.
        return $"vp000_0 {time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}-{time.Minute:00}-{time.Second:00}-{time.Millisecond:000}.0001";
    }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<RawFrameFileData> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => new
        {
            x.HourDirectoryTimestamp,
            x.Timestamp,
        });

        //  Настройка часового каталога.
        typeBuilder.HasOne(x => x.HourDirectory)
            .WithMany(x => x.RawFrameFiles)
            .HasForeignKey(x => x.HourDirectoryTimestamp)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_HourDirectories_RawFrameFiles");
    }
}
