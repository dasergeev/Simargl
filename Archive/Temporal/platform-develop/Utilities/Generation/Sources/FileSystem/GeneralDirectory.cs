namespace Apeiron.Platform.Utilities.Sources;

[Name("Общий каталог файловой системы"), Category("FileSystem"),
    TableName("GeneralDirectories"), CollectionName("GeneralDirectories")]
internal sealed class GeneralDirectory :
    Directory
{
    [Order(1)]
    [Name("Относительный путь к каталогу"), DisplayName("Путь")]
    public string? Path { get; }

    [Order(2)]
    [Name("Файловое хранилище"), DisplayName("Хранилище")]
    [Cascade]
    public FileStorage? FileStorage { get;}
}
