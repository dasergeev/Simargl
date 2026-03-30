namespace Apeiron.Platform.Databases.OsmRussiaDatabase.Entities;

/// <summary>
/// Представляет информацию о схеме БД.
/// </summary>
public partial class SchemaInfo
{
    /// <summary>
    /// Возрвщает или задаёт номер версии схемы БД.
    /// </summary>
    public int Version { get; set; }


    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<SchemaInfo> typeBuilder)
    {
        typeBuilder.ToTable("schema_info");
        typeBuilder.HasKey(entity => entity.Version).HasName("pk_schema_info");
        typeBuilder.Property(entity => entity.Version).ValueGeneratedNever().HasColumnName("version");
    }
}
