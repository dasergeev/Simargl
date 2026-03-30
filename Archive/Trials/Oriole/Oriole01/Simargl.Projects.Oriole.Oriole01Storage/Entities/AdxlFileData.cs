using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Projects.Oriole.Oriole01Storage.Entities;

/// <summary>
/// Представляет данные файла Adxl.
/// </summary>
public class AdxlFileData
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
    /// Возвращает или задаёт ключ датчика.
    /// </summary>
    public long AdxlKey { get; set; }

    /// <summary>
    /// Возвращает или задаёт датчик.
    /// </summary>
    public AdxlData Adxl { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт значение, определяющее состояние данных файла датчика.
    /// </summary>
    public AdxlFileState State { get; set; }

    /// <summary>
    /// Возвращает или задаёт размер файла в байтах.
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<AdxlFileData> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(x => x.Key);

        //  Установка альтернативного ключа.
        typeBuilder.HasAlternateKey(x => new
        {
            x.Timestamp,
            x.AdxlKey,
        });

        //  Настройка часового каталога.
        typeBuilder.HasOne(x => x.HourDirectory)
            .WithMany(x => x.AdxlFiles)
            .HasForeignKey(x => x.HourDirectoryKey)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_HourDirectories_AdxlFiles");

        //  Настройка датичка.
        typeBuilder.HasOne(x => x.Adxl)
            .WithMany(x => x.AdxlFiles)
            .HasForeignKey(x => x.AdxlKey)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Adxls_AdxlFiles");
    }
}
