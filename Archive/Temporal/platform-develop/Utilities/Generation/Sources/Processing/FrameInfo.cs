namespace Apeiron.Platform.Utilities.Sources;

[Name("Информация о кадре регистрации"), Category("Processing")]
[TableName("FrameInfos"), CollectionName("Frames")]
[AlternateKey("Уникальный файл, содержащий кадр регистрации",
    nameof(File) + "Id")]
internal sealed class FrameInfo :
    Entity
{
    [Order(1)]
    [Name("Количество каналов, которое содержится в кадре")]
    [DisplayName("Каналов")]
    [Indexable]
    public int ChannelsCount { get; }

    [Order(2)]
    [Name("Файл, содержащий кадр регистрации"), DisplayName("Файл")]
    public InternalFile? File { get; }
}
