using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет данные файла Nmea.
/// </summary>
public class NmeaFileData
{
    /// <summary>
    /// Возвращает или инициализирует ключ сущности.
    /// </summary>
    public long Key { get; init; }

    /// <summary>
    /// Возвращает или задаёт метку времени.
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт ключ часового каталога.
    /// </summary>
    public long HourDirectoryKey { get; set; }

    /// <summary>
    /// Возвращает или задаёт часовой каталог.
    /// </summary>
    public HourDirectoryData HourDirectory { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт значение, определяющее состояние данных файла датчика.
    /// </summary>
    public NmeaFileState State { get; set; }

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
        typeBuilder.HasKey(x => x.Key);

        //  Настройка уникального адреса.
        typeBuilder.HasIndex(x => x.Timestamp).IsUnique(true);

        //  Настройка часового каталога.
        typeBuilder.HasOne(x => x.HourDirectory)
            .WithMany(x => x.NmeaFiles)
            .HasForeignKey(x => x.HourDirectoryKey)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_HourDirectories_NmeaFiles");
    }
}
