namespace Simargl.Border.Storage.Entities;

/// <summary>
/// Возвращает данные оси.
/// </summary>
public sealed class AxisData :
    EntityData
{
    /// <summary>
    /// Возвращает или задаёт  индекс оси.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Возвращает или задаёт ключ данных одного проезда.
    /// </summary>
    public long PassageKey { get; set; }

    /// <summary>
    /// Возвращает или задаёт данные одного проезда.
    /// </summary>
    public PassageData Passage { get; set; } = null!;

    /// <summary>
    /// Возвращает коллекцию взаимодействий оси.
    /// </summary>
    public HashSet<AxisInteractionData> Interactions { get; set; } = [];

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<AxisData> typeBuilder)
    {
        //  Настройка базовой сущности.
        EntityData.BuildAction(typeBuilder);

        //  Настройка часового каталога.
        typeBuilder.HasOne(x => x.Passage)
            .WithMany(x => x.Axes)
            .HasForeignKey(x => x.PassageKey)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Passages_Axes");
    }
}
