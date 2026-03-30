namespace Apeiron.Platform.Databases.OsmRussiaDatabase.Entities;

/// <summary>
/// Возращает или задаёт информацию о пользователе.
/// </summary>
public partial class User
{
    /// <summary>
    /// Возвращает или задаёт идентификатор пользователя.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Возвращает или задаёт имя пользователя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<User> typeBuilder)
    {
        typeBuilder.ToTable("users");
        typeBuilder.Property(entity => entity.Id).ValueGeneratedNever().HasColumnName("id");
        typeBuilder.Property(entity => entity.Name).HasColumnName("name");
    }
}
