namespace Apeiron.Platform.Utilities.Sources;

[Name("Внутренний каталог файловой системы"), Category("FileSystem"),
    TableName("InternalDirectories"), CollectionName("InternalDirectories")]
[AlternateKey("Уникальное расположение в файловой системе",
    nameof(FileStorage) + "Id", nameof(GeneralDirectory) + "Id", nameof(Path))]
internal sealed class InternalDirectory :
    Directory
{
    [Order(1)]
    [Name("Относительный путь к каталогу"), DisplayName("Путь")]
    [Indexable]
    public string? Path { get; }

    [Order(2)]
    [Name("Файловое хранилище"), DisplayName("Хранилище")]
    public FileStorage? FileStorage { get;}

    [Order(3)]
    [Name("Общий каталог"), DisplayName("Корень")]
    [Cascade]
    public GeneralDirectory? GeneralDirectory { get; }

    [Order(4)]
    [Name("Родительский каталог"), DisplayName("Родитель")]
    [Nullable]
    public InternalDirectory? ParentDirectory { get; }
}
