namespace Apeiron.Platform.Databases.OsmRussiaDatabase.Entities;

/// <summary>
/// Представляет информацию об участниках отношения.
/// </summary>
public partial class RelationMember
{
    /// <summary>
    /// Возращает или задаёт идентификатор отношения.
    /// </summary>
    public long RelationId { get; set; }
    /// <summary>
    /// Возращает или задаёт идентификатор участника.
    /// </summary>
    public long MemberId { get; set; }
    /// <summary>
    /// Возвращает или задаёт тип участника.
    /// </summary>
    public char MemberType { get; set; }
    /// <summary>
    /// Возращает или задаёт роль участника.
    /// </summary>
    public string MemberRole { get; set; } = null!;
    /// <summary>
    /// Возращает или задаёт идентификатор последовательности.
    /// </summary>
    public int SequenceId { get; set; }


    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<RelationMember> typeBuilder)
    {
        typeBuilder.ToTable("relation_members");
        typeBuilder.HasKey(entity => new { entity.RelationId, entity.SequenceId }).HasName("pk_relation_members");
        typeBuilder.HasIndex(entity => new { entity.MemberId, entity.MemberType }, "idx_relation_members_member_id_and_type");
        typeBuilder.Property(entity => entity.RelationId).HasColumnName("relation_id");
        typeBuilder.Property(entity => entity.SequenceId).HasColumnName("sequence_id");
        typeBuilder.Property(entity => entity.MemberId).HasColumnName("member_id");
        typeBuilder.Property(entity => entity.MemberRole).HasColumnName("member_role");
        typeBuilder.Property(entity => entity.MemberType).HasMaxLength(1).HasColumnName("member_type");
    }
}
