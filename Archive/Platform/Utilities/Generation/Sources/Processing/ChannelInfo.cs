namespace Apeiron.Platform.Utilities.Sources;

[Name("Информация о канале кадра регистрации"), Category("Processing")]
[TableName("ChannelInfos"), CollectionName("Channels")]
[AlternateKey("Уникальные индекс канала и кадр регистрации, содержащий канал",
    nameof(Index), nameof(Frame) + "Id")]
internal sealed class ChannelInfo :
    Entity
{
    [Order(1)]
    [Name("Индекс канала в кадре"), DisplayName("Индекс")]
    [Indexable]
    public int Index { get; }

    [Order(2)]
    [Name("Частота дискретизации канала"), DisplayName("Частота")]
    [Indexable]
    public double Sampling { get; }

    [Order(3)]
    [Name("Частота среза фильтра канала"), DisplayName("Фильтр")]
    [Indexable]
    public double Cutoff { get; }

    [Order(4)]
    [Name("Количество статистических данных"), DisplayName("Статистика")]
    [Indexable]
    public int StatisticsCount { get; }

    [Order(5)]
    [Name("Количество распределений"), DisplayName("Распределение")]
    [Indexable]
    public int DistributionsCount { get; }

    [Order(6)]
    [Name("Кадр регистрации, содержащий канал"), DisplayName("Кадр")]
    [Cascade]
    public FrameInfo? Frame { get; }

    [Order(7)]
    [Name("Имя канала"), DisplayName("Имя")]
    public ChannelName? Name { get; }

    [Order(8)]
    [Name("Единица измерения канала"), DisplayName("Единица")]
    public ChannelUnit? Unit { get; }
}
