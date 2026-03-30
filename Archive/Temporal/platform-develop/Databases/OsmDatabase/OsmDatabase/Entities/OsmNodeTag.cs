using Apeiron.Platform.Databases.Entities;

namespace Apeiron.Platform.Databases.OsmDatabase.Entities;

/// <summary>
/// Представляет информацию о метках Tags в точках файлов Osm.
/// </summary>
public sealed class OsmNodeTag : Entity
{
    /// <summary>
    /// Возвращает или задаёт идентификатор точки Nodes.
    /// </summary>
    public long OsmNodeId { get; set; }

    /// <summary>
    /// Возвращает или задаёт ключ.
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт значение.
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт информацию о точке Node.
    /// </summary>
    public OsmNode OsmNode { get; set; } = null!;


    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<OsmNodeTag> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);
        typeBuilder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        typeBuilder.HasIndex(entity => entity.Id);

        //  Настройка связи один ко многим.
        typeBuilder.HasOne(osmNodeTag => osmNodeTag.OsmNode)
            .WithMany(node => node.OsmNodeTags)
            .HasForeignKey(osmNodeTag => osmNodeTag.OsmNodeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_OsmNodeTags_OsmNode");

        //  Настройка индексов и ключей.
        typeBuilder.Property(service => service.Key);
        typeBuilder.HasIndex(service => service.Key);

        typeBuilder.Property(service => service.Value);
        typeBuilder.HasIndex(service => service.Value);
    }
}
