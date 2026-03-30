namespace Apeiron.Platform.Databases.CentralDatabase.Entities;

/// <summary>
/// Представляет сущность в базе данных, содержащей узел карты.
/// </summary>
public sealed class ECMapNode
{
    /// <summary>
    /// Возвращает или задаёт идентификатор сущности.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Возвращает или задаёт широту.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Возвращает или задаёт долготу.
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction([ParameterNoChecks] EntityTypeBuilder<ECMapNode> typeBuilder)
    {
        //  Установка имени таблицы.
        typeBuilder.ToTable("MapNodes");

        //  Настройка идентификатора сущности.
        typeBuilder.HasKey(e => e.Id);
        typeBuilder.Property(e => e.Id).ValueGeneratedNever();
        typeBuilder.HasIndex(e => e.Id);

        //  Настройка широты.
        typeBuilder.HasIndex(e => e.Latitude);

        //  Настройка долготы.
        typeBuilder.HasIndex(e => e.Longitude);
    }
}
