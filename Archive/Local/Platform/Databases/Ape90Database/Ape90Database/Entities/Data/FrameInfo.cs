using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apeiron.Platform.Databases.Ape90Database.Entities;

/// <summary>
/// Представляет информацию об исходном кадре регистрации.
/// </summary>
public sealed class FrameInfo :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт путь к файлу.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт значение, определяющее проанализированы ли данные.
    /// </summary>
    public bool IsAnalyzed { get; set; }

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
    /// Возвращает коллекцию фрагментов данных.
    /// </summary>
    public HashSet<Fragment> Fragments { get; set; } = new();

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<FrameInfo> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);

        //  Установка альтернативного ключа.
        typeBuilder.HasAlternateKey(entity => entity.Path);

        typeBuilder.HasIndex(entity => entity.IsAnalyzed);

        //  Настройка времени начала и окончания записи кадра.
        typeBuilder.HasIndex(entity => entity.BeginTime);
        typeBuilder.HasIndex(entity => entity.EndTime);
        typeBuilder.HasAlternateKey(entity => new
        {
            entity.BeginTime,
            entity.EndTime
        });
    }
}
