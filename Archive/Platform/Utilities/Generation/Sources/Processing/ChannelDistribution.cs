namespace Apeiron.Platform.Utilities.Sources;

[Name("Данные о распределении значений канала"), Category("Processing")]
[TableName("ChannelDistributions"), CollectionName("Distributions")]
[AlternateKey("Уникальная комбинация канала, фильтра и метрики файла",
    nameof(Channel) + "Id", nameof(Filter) + "Id", nameof(FileMetric) + "Id")]
internal sealed class ChannelDistribution :
    Entity
{
    [Order(1)]
    [Name("Количество различных значений в распределении"), DisplayName("Значений")]
    [Indexable]
    public long Count { get; }

    [Order(2)]
    [Name("Информация о канале"), DisplayName("Канал")]
    [Cascade]
    public ChannelInfo? Channel { get; }

    [Order(3)]
    [Name("Информация о фильтре"), DisplayName("Фильтр")]
    [Cascade]
    public FilterInfo? Filter { get; }

    [Order(4)]
    [Name("Информация о метрике файла"), DisplayName("Метрика")]
    [Cascade]
    public InternalFileMetric? FileMetric { get; }
}
