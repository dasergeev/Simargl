using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Trials.Aurora.Aurora01.Storage.Entities;

/// <summary>
/// Представляет данные файла Nmea.
/// </summary>
public class NmeaFileData
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
    /// Возвращает или задаёт минуту записи.
    /// </summary>
    public byte Minute { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее состояние данных файла датчика.
    /// </summary>
    public NmeaFileState State { get; set; }

    /// <summary>
    /// Возвращает имя файла.
    /// </summary>
    /// <returns>
    /// Имя файла.
    /// </returns>
    public string GetFileName()
    {
        //  Определение времени записи.
        DateTime time = HourDirectoryData.ToTime(HourDirectoryTimestamp) + TimeSpan.FromMinutes(Minute);

        //  Возврат имени файла.
        return $"nmea-{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}-{time.Minute:00}.nmea";
    }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<NmeaFileData> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => new
        {
            x.HourDirectoryTimestamp,
            x.Minute,
        });

        //  Настройка часового каталога.
        typeBuilder.HasOne(x => x.HourDirectory)
            .WithMany(x => x.NmeaFiles)
            .HasForeignKey(x => x.HourDirectoryTimestamp)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_HourDirectories_NmeaFiles");
    }
}
