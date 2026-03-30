using Apeiron.Platform.Databases.Entities;

namespace Apeiron.Platform.Databases.OsmDatabase.Entities;

/// <summary>
/// Представляет информацию о точках(Node) содержащихся в файлах Osm.
/// </summary>
public sealed class OsmNode : Entity
{
    /// <summary>
    /// Возвращает или задаёт идентификатор файла.
    /// </summary>
    public long OsmFileId { get; set; }

    /// <summary>
    /// Возвращает или задаёт широту.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт долготу.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт информацию о файле.
    /// </summary>
    public OsmFile OsmFile { get; set; } = null!;

    /// <summary>
    /// Возвращает коллекцию информации о метках Tag.
    /// </summary>
    public ICollection<OsmNodeTag> OsmNodeTags { get; } = new HashSet<OsmNodeTag>();

    /// <summary>
    /// Возвращает коллекцию информации о линиях в которые входит точка.
    /// </summary>
    public ICollection<OsmWayNode> OsmWayNodes { get; } = new HashSet<OsmWayNode>();

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<OsmNode> typeBuilder)
    {
        //  Настройка идентификатора сущности.
        typeBuilder.HasKey(service => service.Id);
        typeBuilder.Property(service => service.Id).ValueGeneratedNever();
        typeBuilder.HasIndex(service => service.Id);

        // Настройка связи один ко многим OsmFiles -< OsmNodes
        typeBuilder.HasOne(osmNode => osmNode.OsmFile)
            .WithMany(file => file.OsmNodes)
            .HasForeignKey(osmNode => osmNode.OsmFileId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_OsmNodes_OsmFile");

        //  Настройка широты.
        typeBuilder.HasIndex(service => service.Latitude);

        //  Настройка долготы.
        typeBuilder.HasIndex(service => service.Longitude);
    }
}
