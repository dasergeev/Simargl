namespace Apeiron.Platform.Utilities.Sources;

[Name("Статистические данные канала"), Category("Processing")]
[TableName("ChannelStatistics"), CollectionName("Statistics")]
[AlternateKey("Уникальная комбинация канала, фильтра и метрики файла",
    nameof(Channel) + "Id", nameof(Filter) + "Id", nameof(FileMetric) + "Id")]
internal sealed class ChannelStatistic :
    Entity
{
    [Order(1)]
    [Name("Количество значений"), DisplayName("Количество")]
    [Indexable]
    public long Count { get; }

    [Order(2)]
    [Name("Минимальное значение"), DisplayName("Минимум")]
    [Indexable]
    public double Min { get; }

    [Order(3)]
    [Name("Максимальное значение"), DisplayName("Максимум")]
    [Indexable]
    public double Max { get; }

    [Order(4)]
    [Name("Среднее значение"), DisplayName("Среднее")]
    [Indexable]
    public double Average { get; }

    [Order(5)]
    [Name("Среднеквадратическое отклонение"), DisplayName("СКО")]
    [Indexable]
    public double Deviation { get; }

    [Order(6)]
    [Name("Сумма значений"), DisplayName("Сумма")]
    [Indexable]
    public double Sum { get; }

    [Order(7)]
    [Name("Сумма квадратов значений"), DisplayName("Сумма квадратов")]
    [Indexable]
    public double SquaresSum { get; }

    [Order(8)]
    [Name("Минимальное значение по модулю"), DisplayName("Минимум по модулю")]
    [Indexable]
    public double MinModulo { get; }

    [Order(9)]
    [Name("Максимальное значение по модулю"), DisplayName("Максимум по модулю")]
    [Indexable]
    public double MaxModulo { get; }

    [Order(10)]
    [Name("Среднее значение по модулю"), DisplayName("Среднее по модулю")]
    [Indexable]
    public double AverageModulo { get; }

    [Order(11)]
    [Name("Среднеквадратическое отклонение по модулю"), DisplayName("СКО по модулю")]
    [Indexable]
    public double DeviationModulo { get; }

    [Order(12)]
    [Name("Сумма значений по модулю"), DisplayName("Сумма по модулю")]
    [Indexable]
    public double SumModulo { get; }

    [Order(13)]
    [Name("Информация о канале"), DisplayName("Канал")]
    [Cascade]
    public ChannelInfo? Channel { get; }

    [Order(14)]
    [Name("Информация о фильтре"), DisplayName("Фильтр")]
    [Cascade]
    public FilterInfo? Filter { get; }

    [Order(15)]
    [Name("Информация о метрике файла"), DisplayName("Метрика")]
    [Cascade]
    public InternalFileMetric? FileMetric { get; }
}
