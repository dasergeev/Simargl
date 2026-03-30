namespace Apeiron.Platform.Utilities.Sources;

[Name("Метрика таблицы базы данных"), Category("Reflection")]
[TableName("DatabaseTableMetrics"), CollectionName("Metrics")]
[AlternateKey("Уникальное время создания метрики для данной таблицы",
    nameof(Table) + "Id", nameof(DeterminationTime))]
internal sealed class DatabaseTableMetric :
    Entity
{
    [Order(1)]
    [Name("Время определения метрики"), DisplayName("Определено")]
    [Indexable]
    public DateTime DeterminationTime { get; }

    [Order(2)]
    [Name("Количество элементов в таблице"), DisplayName("Количество")]
    [Indexable]
    public long Count { get; }

    [Order(3)]
    [Name("Таблица базы данных"), DisplayName("Таблица")]
    [Cascade]
    public DatabaseTable? Table { get; }
}
