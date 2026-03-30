using Apeiron.Platform.Databases.Entities;

namespace Apeiron.Platform.Databases.OsmDatabase.Entities;

/// <summary>
/// Представляет информацию о линиях, содержащихся в Osm картах.
/// </summary>
public sealed class OsmWay : Entity
{
    /// <summary>
    /// Возвращает или задаёт идентификатор файла.
    /// </summary>
    public long OsmFileId { get; set; }

    /// <summary>
    /// Возвращает или задаёт информацию о файле.
    /// </summary>
    public OsmFile OsmFile { get; set; } = null!;

    /// <summary>
    /// Возвращает коллекцию информации о метках Tag.
    /// </summary>
    public ICollection<OsmWayTag> OsmWayTags { get; } = new HashSet<OsmWayTag>();

    /// <summary>
    /// Возвращает коллекцию информации о Node точках из которых состоит линия.
    /// </summary>
    public ICollection<OsmWayNode> OsmWayNodes { get; } = new HashSet<OsmWayNode>();

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<OsmWay> typeBuilder)
    {
        //  Настройка идентификатора сущности.
        typeBuilder.HasKey(service => service.Id);
        typeBuilder.Property(service => service.Id).ValueGeneratedNever();
        typeBuilder.HasIndex(service => service.Id);

        // Настройка связи один ко многим.
        typeBuilder.HasOne(osmWay => osmWay.OsmFile)
            .WithMany(file => file.OsmWays)
            .HasForeignKey(osmWay => osmWay.OsmFileId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_OsmWays_OsmFile");
    }
}
