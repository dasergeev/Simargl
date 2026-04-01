using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Simargl.Entities;

/// <summary>
/// Представляет данные именованной сущности.
/// </summary>
public class NamedEntityData :
    EntityData
{
    /// <summary>
    /// Возвращает или задаёт имя сущности.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [CLSCompliant(false)]
    public static new void BuildAction<TNamedEntityData>(
        EntityTypeBuilder<TNamedEntityData> typeBuilder)
        where TNamedEntityData : NamedEntityData
    {
        //  Настройка базовой сущности.
        EntityData.BuildAction(typeBuilder);

        //  Настройка уникального имени.
        typeBuilder.HasIndex(entityData => entityData.Name).IsUnique(true);
    }
}
