namespace Apeiron.Platform.Utilities.Sources;

[Name("Информация о фильтре"), Category("Processing")]
[TableName("FilterInfos"), CollectionName("Filters")]
[AlternateKey("Уникальные характеристики фильтра",
    nameof(LowerFrequency), nameof(UpperFrequency), nameof(IsInverted), nameof(Format) + "Id")]
internal sealed class FilterInfo :
    Entity
{
    [Order(1)]
    [Name("Нижняя частота фильтра"), DisplayName("Нижняя частота")]
    [Indexable]
    public double LowerFrequency { get; }

    [Order(2)]
    [Name("Верхняя частота фильтра"), DisplayName("Верхняя частота")]
    [Indexable]
    public double UpperFrequency { get; }

    [Order(3)]
    [Name("Значение, определяющее, следует ли инфертировать область частот")]
    [DisplayName("Фильтр")]
    [Indexable]
    public bool IsInverted { get; }

    [Order(4)]
    [Name("Формат фильтра"), DisplayName("Формат")]
    [Cascade]
    public FilterFormat? Format { get; }
}
