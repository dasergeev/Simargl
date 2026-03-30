namespace Apeiron.Platform.Databases.OsmRussiaDatabase.Entities;

/// <summary>
/// Представляет информацию об отношении.
/// </summary>
public partial class Relation
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
    /// Возвращает или задаёт идентификатор пользователя. Внешний ключ.
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// Возвращает или задаёт временную метку.
    /// </summary>
    public DateTime Tstamp { get; set; }
    /// <summary>
    /// Возравщает или задаёт идентификатор изменения.
    /// </summary>
    public long ChangesetId { get; set; }
    /// <summary>
    /// Возвращает коллекцию меток.
    /// </summary>
    public Dictionary<string, string>? Tags { get; set; }


    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<Relation> typeBuilder)
    {
        typeBuilder.ToTable("relations");
        typeBuilder.Property(entity => entity.Id).ValueGeneratedNever().HasColumnName("id");
        typeBuilder.Property(entity => entity.ChangesetId).HasColumnName("changeset_id");
        typeBuilder.Property(entity => entity.Tags).HasColumnName("tags");
        typeBuilder.Property(entity => entity.Tstamp).HasColumnType("timestamp without time zone").HasColumnName("tstamp");
        typeBuilder.Property(entity => entity.UserId).HasColumnName("user_id");
        typeBuilder.Property(entity => entity.Version).HasColumnName("version");     
    }
}
