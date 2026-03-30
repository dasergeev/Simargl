using Apeiron.Platform.Databases.Entities;

namespace Apeiron.Platform.Databases.OsmDatabase.Entities;

/// <summary>
/// Представляет информацию о метках Tags в линиях файлов Osm.
/// </summary>
public class OsmWayTag : Entity
{
    /// <summary>
    /// Возвращает или задаёт идентификатор точки Nodes.
    /// </summary>
    public long OsmWayId { get; set; }

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
    public OsmWay OsmWay { get; set; } = null!;


    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<OsmWayTag> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);
        typeBuilder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        typeBuilder.HasIndex(entity => entity.Id);

        //  Настройка связи один ко многим.
        typeBuilder.HasOne(osmWayTag => osmWayTag.OsmWay)
            .WithMany(way => way.OsmWayTags)
            .HasForeignKey(osmWayTag => osmWayTag.OsmWayId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_OsmWayTags_OsmWay");

        //  Настройка индексов и ключей.
        typeBuilder.Property(service => service.Key);
        typeBuilder.HasIndex(service => service.Key);

        typeBuilder.Property(service => service.Value);
        typeBuilder.HasIndex(service => service.Value);
    }
}
