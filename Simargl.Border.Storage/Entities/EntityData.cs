namespace Simargl.Border.Storage.Entities;

/// <summary>
/// Представляет данные сущности.
/// </summary>
public abstract class EntityData
{
    /// <summary>
    /// Возвращает или инициализирует ключ сущности.
    /// </summary>
    public long Key { get; init; }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction<TEntityData>(
        EntityTypeBuilder<TEntityData> typeBuilder)
        where TEntityData : EntityData
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entityData => entityData.Key);

        //  Настройка автогенерации.
        typeBuilder.Property(e => e.Key).ValueGeneratedOnAdd();
    }
}
