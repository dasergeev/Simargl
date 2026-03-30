using Apeiron.Platform.Databases.Entities;

namespace Apeiron.Platform.Databases.OrioleKmtDatabase.Entities;

/// <summary>
/// Представляет информацио о файле содержащий данные.
/// </summary>
public class RawFrame : Entity
{
    /// <summary>
    /// Возвращает или задаёт полный путь к файлам.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт время записи файла.
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Возвращает или задаёт время начала поступления данных.
    /// </summary>
    public DateTime BeginTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт время окончания поступления данных.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт значение, определяющее нормализовано ли время начала поступления данных.
    /// </summary>
    public bool IsNormalizedTime { get; set; }

    /// <summary>
    /// Представляет флаг о признаке проанализирован файл или нет.
    /// </summary>
    public bool IsAnalyzed { get; set; }

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
        typeBuilder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        typeBuilder.HasIndex(entity => entity.Id);

        //  Настройка индексов и ключей
        typeBuilder.Property(entity => entity.FilePath);
        typeBuilder.HasAlternateKey(entity => entity.FilePath);
        typeBuilder.HasIndex(entity => entity.FilePath);

        typeBuilder.Property(entity => entity.Time);
        typeBuilder.HasIndex(entity => entity.Time);

        typeBuilder.Property(entity => entity.BeginTime);
        typeBuilder.HasIndex(entity => entity.BeginTime);

        typeBuilder.Property(entity => entity.EndTime);
        typeBuilder.HasIndex(entity => entity.EndTime);

        typeBuilder.Property(entity => entity.IsNormalizedTime);
        typeBuilder.HasIndex(entity => entity.IsNormalizedTime);

        typeBuilder.Property(entity => entity.IsAnalyzed);
        typeBuilder.HasIndex(entity => entity.IsAnalyzed);
    }
}
