using NetTopologySuite.Geometries;

namespace Apeiron.Platform.Databases.OsmRussiaDatabase.Entities;

/// <summary>
/// Представляет информацию о точке.
/// </summary>
public partial class Node
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Возвращает или задаёт версию.
    /// </summary>
    public int Version { get; set; }
    /// <summary>
    /// Возвращает или задёт идентификатор пользователя. Внешний ключ.
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// Возвращает или задаёт временную метку.
    /// </summary>
    public DateTime Tstamp { get; set; }
    /// <summary>
    /// Возвращает или задаёт идентификатор изменений.
    /// </summary>
    public long ChangesetId { get; set; }
    /// <summary>
    /// Возвращает или задаёт информацию о метках.
    /// </summary>
    public Dictionary<string, string>? Tags { get; set; }

    /// <summary>
    /// Возвращает или задаёт пространственный тип точки.
    /// </summary>
    public Point? Geom { get; set; }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<Node> typeBuilder)
    {
        typeBuilder.ToTable("nodes");
        typeBuilder.HasIndex(entity => entity.Geom, "idx_nodes_geom").HasMethod("gist");
        typeBuilder.Property(entity => entity.Id).ValueGeneratedNever().HasColumnName("id");
        typeBuilder.Property(entity => entity.ChangesetId).HasColumnName("changeset_id");
        typeBuilder.Property(entity => entity.Geom).HasColumnType("geometry(Point,4326)").HasColumnName("geom");
        typeBuilder.Property(entity => entity.Tags).HasColumnName("tags");
        typeBuilder.Property(entity => entity.Tstamp).HasColumnType("timestamp without time zone").HasColumnName("tstamp");
        typeBuilder.Property(entity => entity.UserId).HasColumnName("user_id");
        typeBuilder.Property(entity => entity.Version).HasColumnName("version");
    }
}
