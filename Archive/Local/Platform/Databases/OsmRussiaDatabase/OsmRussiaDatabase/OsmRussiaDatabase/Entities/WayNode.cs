namespace Apeiron.Platform.Databases.OsmRussiaDatabase.Entities;

/// <summary>
/// Представляет информацию об отношении токек и линий. Служебная таблица для связывания сущностей.
/// </summary>
public partial class WayNode
{
    /// <summary>
    /// Возвращает или задаёт идентификатор линии.
    /// </summary>
    public long WayId { get; set; }
    /// <summary>
    /// Возвращает или задаёт идентификатор узла.
    /// </summary>
    public long NodeId { get; set; }
    /// <summary>
    /// Возвращает или задаёт идентификатор последовательности.
    /// </summary>
    public int SequenceId { get; set; }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<WayNode> typeBuilder)
    {
        typeBuilder.ToTable("way_nodes");
        typeBuilder.HasKey(entity => new { entity.WayId, entity.SequenceId }).HasName("pk_way_nodes");
        typeBuilder.HasIndex(entity => entity.NodeId, "idx_way_nodes_node_id");
        typeBuilder.Property(entity => entity.WayId).HasColumnName("way_id");
        typeBuilder.Property(entity => entity.SequenceId).HasColumnName("sequence_id");
        typeBuilder.Property(entity => entity.NodeId).HasColumnName("node_id");
    }
}
