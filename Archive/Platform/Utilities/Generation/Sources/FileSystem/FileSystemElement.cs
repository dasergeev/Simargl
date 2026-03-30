namespace Apeiron.Platform.Utilities.Sources;

[Name("Элемент файловой системы"), Category("FileSystem")]
internal abstract class FileSystemElement :
    Entity
{
    [Order(1)]
    [Name("Имя элемента"), DisplayName("Имя")]
    [Indexable]
    public string Name { get; } = null!;
}
