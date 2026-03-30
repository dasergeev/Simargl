using Apeiron.Platform.Databases.Entities;

namespace Apeiron.Platform.Databases.OsmDatabase.Entities;

/// <summary>
/// Представляет информацию о файлах содержащих данные о картах.
/// </summary>
public sealed class OsmFile : Entity
{
    /// <summary>
    /// Возвращает или задаёт полный путь к файлам.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт флаг обработан или не обработан файл.
    /// </summary>
    public bool IsAnalyzed { get; set; } = false;

    /// <summary>
    /// Возвращает или задаёт флаг содержит или нет файл полезную информацию.
    /// </summary>
    public bool IsNodesEmpty { get; set; } = false;

    /// <summary>
    /// Возвращает или задаёт флаг содержит или нет файл полезную информацию.
    /// </summary>
    public bool IsWaysEmpty { get; set; } = false;

    /// <summary>
    /// Возвращает или задаёт флаг загружены или нет Node узлы карты в базу.
    /// </summary>
    public bool IsNodesLoad { get; set; } = false;

    /// <summary>
    /// Возвращает или задаёт флаг загружены или нет Tag узлов карты в базу.
    /// </summary>
    public bool IsNodeTagsLoad { get; set; } = false;

    /// <summary>
    /// Возвращает или задаёт флаг загружены или нет Way узлы карты в базу.
    /// </summary>
    public bool IsWaysLoad { get; set; } = false;

    /// <summary>
    /// Возвращает или задаёт флаг загружены или нет Way узлы карты в базу.
    /// </summary>
    public bool IsWayTagsLoad { get; set; } = false;


    /// <summary>
    /// Возвращает коллекцию информации об узлах (Nodes).
    /// </summary>
    public ICollection<OsmNode> OsmNodes { get; } = new HashSet<OsmNode>();

    /// <summary>
    /// Возвращает коллекцию информации о линиях (Nodes).
    /// </summary>
    public ICollection<OsmWay> OsmWays { get; } = new HashSet<OsmWay>();

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void BuildAction(EntityTypeBuilder<OsmFile> typeBuilder)
    {
        //  Настройка первичного ключа.
        typeBuilder.HasKey(entity => entity.Id);
        typeBuilder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        typeBuilder.HasIndex(entity => entity.Id);

        //  Настройка индексов и ключей
        typeBuilder.Property(service => service.FilePath);
        typeBuilder.HasAlternateKey(service => service.FilePath);
        typeBuilder.HasIndex(service => service.FilePath);

        typeBuilder.Property(service => service.IsAnalyzed);
        typeBuilder.HasIndex(service => service.IsAnalyzed);

        typeBuilder.Property(service => service.IsNodesEmpty);
        typeBuilder.HasIndex(service => service.IsNodesEmpty);

        typeBuilder.Property(service => service.IsWaysEmpty);
        typeBuilder.HasIndex(service => service.IsWaysEmpty);

        typeBuilder.Property(service => service.IsNodesLoad);
        typeBuilder.HasIndex(service => service.IsNodesLoad);

        typeBuilder.Property(service => service.IsNodeTagsLoad);
        typeBuilder.HasIndex(service => service.IsNodeTagsLoad);

        typeBuilder.Property(service => service.IsWaysLoad);
        typeBuilder.HasIndex(service => service.IsWaysLoad);

        typeBuilder.Property(service => service.IsWayTagsLoad);
        typeBuilder.HasIndex(service => service.IsWayTagsLoad);
    }
}

