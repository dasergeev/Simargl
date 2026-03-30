namespace Apeiron.Platform.Utilities.Sources;

[Name("Внутренний файл файловой системы"), Category("FileSystem"),
    TableName("InternalFiles"), CollectionName("InternalFiles")]
[AlternateKey("Уникальное расположение в файловой системе",
    nameof(FileStorage) + "Id", nameof(GeneralDirectory) + "Id", nameof(Path))]
internal sealed class InternalFile :
    Directory
{
    [Order(1)]
    [Name("Относительный путь к файлу"), DisplayName("Путь")]
    [Indexable]
    public string? Path { get; }

    [Order(2)]
    [Name("Расширение файла"), DisplayName("Расширение")]
    [Indexable]
    public string? Extension { get; }

    [Order(3)]
    [Name("Время регистрации файла"), DisplayName("Зарегистрирован")]
    [Indexable]
    public DateTime RegistrationTime { get; }

    [Order(4)]
    [Name("Файловое хранилище"), DisplayName("Хранилище")]
    public FileStorage? FileStorage { get;}

    [Order(5)]
    [Name("Общий каталог"), DisplayName("Корень")]
    public GeneralDirectory? GeneralDirectory { get; }

    [Order(6)]
    [Name("Родительский каталог"), DisplayName("Родитель")]
    [Cascade]
    public InternalDirectory? ParentDirectory { get; }
}
