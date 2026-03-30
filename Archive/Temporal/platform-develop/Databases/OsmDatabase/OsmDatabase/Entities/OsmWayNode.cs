using Apeiron.Platform.Databases.Entities;

namespace Apeiron.Platform.Databases.OsmDatabase.Entities;

/// <summary>
/// Представляет сущность для описания отношения многие ко многим, Ways, Nodes.
/// </summary>
public sealed class OsmWayNode : Entity
{
    /// <summary>
    /// Возвращает или задаёт идентификатор файла.
    /// </summary>
    public long OsmWayId { get; set; }

    /// <summary>
    /// Возвращает или задаёт идентификатор файла.
    /// </summary>
    public long OsmNodeId { get; set; }

    /// <summary>
    /// Возвращает или задаёт информацию о линии.
    /// </summary>
    public OsmWay OsmWay { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт информацию о точки.
    /// </summary>
    public OsmNode OsmNode { get; set; } = null!;

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<OsmWayNode> typeBuilder)
    {
        //  Установка имени таблицы.
        typeBuilder.ToTable("OsmWayNodes");

        //  Настройка идентификатора сущности.
        typeBuilder.HasKey(x => x.Id);
        typeBuilder.Property(x => x.Id).ValueGeneratedOnAdd();
        typeBuilder.HasIndex(x => x.Id);

        // Настройка индекса.
        typeBuilder.Property(x => x.OsmWayId);
        typeBuilder.HasIndex(x => x.OsmWayId);

        // Настройка связи один ко многим 
        typeBuilder.HasOne(d => d.OsmWay)
            .WithMany(p => p.OsmWayNodes)
            .HasForeignKey(d => d.OsmWayId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_OsmWayNode_OsmWay");

        // Настройка индекса.
        typeBuilder.Property(x => x.OsmNodeId);
        typeBuilder.HasIndex(x => x.OsmNodeId);

        // Настройка связи один ко многим 
        typeBuilder.HasOne(d => d.OsmNode)
            .WithMany(p => p.OsmWayNodes)
            .HasForeignKey(d => d.OsmNodeId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_OsmWayNode_OsmNode");
    }
}
