namespace Apeiron.Platform.Utilities.Sources;

[Name("Значение в распределении значений канала"), Category("Processing")]
[TableName("ChannelDistributionValues"), CollectionName("DistributionValues")]
[AlternateKey("Уникальная комбинация распределения и значения",
    nameof(Distribution) + "Id", nameof(Value))]
internal sealed class ChannelDistributionValue :
    Entity
{
    [Order(1)]
    [Name("Значение"), DisplayName("Значение")]
    [Indexable]
    public double Value { get; }

    [Order(2)]
    [Name("Количество значений"), DisplayName("Количество")]
    [Indexable]
    public long Count { get; }

    [Order(3)]
    [Name("Информация о распределении"), DisplayName("Распределение")]
    [Cascade]
    public ChannelDistribution? Distribution { get; }
}
