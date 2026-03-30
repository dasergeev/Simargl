using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apeiron.Platform.Databases.Ape90Database.Entities;

/// <summary>
/// Представляет исходный кадр регистрации.
/// </summary>
public sealed class RawFrame :
    RawFile
{
    /// <summary>
    /// Возвращает или задаёт значение, определяющее проанализированы ли данные.
    /// </summary>
    public bool IsAnalyzed { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее нормализованно ли время.
    /// </summary>
    public bool IsNormalizedTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт длительность кадра.
    /// </summary>
    public double Duration { get; set; }

    /// <summary>
    /// Возвращает время начала записи кадра.
    /// </summary>
    public DateTime BeginTime { get; set; }

    /// <summary>
    /// Возвращает время окончания записи кадра.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<RawFrame> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);

        //  Настрока каталога исходных данных.
        typeBuilder.HasOne(rawFrame => rawFrame.RawDirectory)
            .WithMany(rawDirectory => rawDirectory.RawFrames)
            .HasForeignKey(rawFrame => rawFrame.RawDirectoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_RawFrames_RawDirectories");

        //  Установка альтернативного ключа.
        typeBuilder.HasAlternateKey(entity => entity.Path);

        //  Настройка времени начала и окончания записи кадра.
        typeBuilder.HasIndex(entity => entity.BeginTime);
        typeBuilder.HasIndex(entity => entity.EndTime);
    }
}
