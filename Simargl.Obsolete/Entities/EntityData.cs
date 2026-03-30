using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Entities;

/// <summary>
/// Представляет данные сущности.
/// </summary>
public class EntityData
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
    [CLSCompliant(false)]
    public static void BuildAction<TEntityData>(
        EntityTypeBuilder<TEntityData> typeBuilder)
        where TEntityData : EntityData
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entityData => entityData.Key);
    }
}
