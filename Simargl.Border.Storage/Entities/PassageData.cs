namespace Simargl.Border.Storage.Entities;

/// <summary>
/// Представляет данные одного проезда.
/// </summary>
public sealed class PassageData :
    EntityData
{
    /// <summary>
    /// Возвращает или задаёт начальную метку времени.
    /// </summary>
    public long StartTimestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт конечную метку времени.
    /// </summary>
    public long EndTimestamp { get; set; }

    /// <summary>
    /// Возвращает или задаёт состояние данных.
    /// </summary>
    public PassageState State { get; set; }

    /// <summary>
    /// Возвращает количество осей.
    /// </summary>
    public int AxesCount { get; set; }

    /// <summary>
    /// Возвращает количество фиксаций осей.
    /// </summary>
    public int AxesCommits { get; set; }

    /// <summary>
    /// Возвращает коллекцию данных осей.
    /// </summary>
    public HashSet<AxisData> Axes { get; set; } = [];

    /// <summary>
    /// Выполняет настройку данного типа сущности в модели.
    /// </summary>
    /// <param name="typeBuilder">
    /// Построитель типа.
    /// </param>
    internal static void BuildAction(
        EntityTypeBuilder<PassageData> typeBuilder)
    {
        //  Настройка базовой сущности.
        EntityData.BuildAction(typeBuilder);

        //  Добавление индекса.
        typeBuilder.HasIndex(x => x.State);
    }
}
