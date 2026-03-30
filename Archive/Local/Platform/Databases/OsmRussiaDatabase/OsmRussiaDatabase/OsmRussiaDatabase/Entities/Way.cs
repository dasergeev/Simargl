using NetTopologySuite.Geometries;

namespace Apeiron.Platform.Databases.OsmRussiaDatabase.Entities;

/// <summary>
/// Представляет информацию о линиях.
/// </summary>
public partial class Way
{
    /// <summary>
    /// Возвращает или задаёт идентификатор линии.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Возвращает или задаёт версию.
    /// </summary>
    public int Version { get; set; }
    /// <summary>
    /// Возвращает или задаёт идентификатор пользователя. Внешний ключ.
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// Возвращает или задаёт метку времени.
    /// </summary>
    public DateTime Tstamp { get; set; }
    /// <summary>
    /// Возвращает или задаёт идентификатор изменения.
    /// </summary>
    public long ChangesetId { get; set; }
    /// <summary>
    /// Возвращает коллекцию информации о метках.
    /// </summary>
    public Dictionary<string, string>? Tags { get; set; }
    /// <summary>
    /// Возвращает коллекцию узлов(точек).
    /// </summary>
    public long[]? Nodes { get; set; }

    /// <summary>
    /// Возвращает или задаёт пространственный тип Geometry.
    /// </summary>
    public Geometry? Linestring { get; set; }


    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<Way> typeBuilder)
    {
        typeBuilder.ToTable("ways");
        typeBuilder.HasIndex(entity => entity.Linestring, "idx_ways_linestring").HasMethod("gist");
        typeBuilder.Property(entity => entity.Id).ValueGeneratedNever().HasColumnName("id");
        typeBuilder.Property(entity => entity.ChangesetId).HasColumnName("changeset_id");
        typeBuilder.Property(entity => entity.Linestring).HasColumnType("geometry(Geometry,4326)").HasColumnName("linestring");
        typeBuilder.Property(entity => entity.Nodes).HasColumnName("nodes");
        typeBuilder.Property(entity => entity.Tags).HasColumnName("tags");
        typeBuilder.Property(entity => entity.Tstamp).HasColumnType("timestamp without time zone").HasColumnName("tstamp");
        typeBuilder.Property(entity => entity.UserId).HasColumnName("user_id");
        typeBuilder.Property(entity => entity.Version).HasColumnName("version");
    }
}
