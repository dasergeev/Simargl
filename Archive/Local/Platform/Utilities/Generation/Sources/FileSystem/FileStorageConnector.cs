namespace Apeiron.Platform.Utilities.Sources;

[Name("Информация о соединении с файловым хранилищем"), Category("FileSystem"),
    TableName("FileStorageConnectors"), CollectionName("Connectors")]
internal sealed class FileStorageConnector :
    Entity
{
    [Order(1)]
    [Name("Приоритет соединения"), DisplayName("Приоритет")]
    [Indexable]
    public int Priority { get; }

    [Order(2)]
    [Name("Путь к файловому хранилищу"), DisplayName("Путь")]
    public string? Path { get; }

    [Order(3)]
    [Name("Файловое хранилище"), DisplayName("Хранилище")]
    [Cascade]
    public FileStorage? FileStorage { get;}
}
