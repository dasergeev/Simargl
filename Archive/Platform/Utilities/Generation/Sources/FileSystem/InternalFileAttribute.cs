namespace Apeiron.Platform.Utilities.Sources;

[Name("Атрибут внутреннего файла файловой системы"), Category("FileSystem")]
[TableName("InternalFileAttributes"), CollectionName("Attributes")]
internal sealed class InternalFileAttribute :
    Entity
{
    [Order(1)]
    [Name("Значение атрибута"), DisplayName("Значение")]
    [Indexable]
    public long Value { get; }

    [Order(2)]
    [Name("Формат атрибута"), DisplayName("Формат")]
    [Cascade]
    public InternalFileAttributeFormat? Format { get; }

    [Order(3)]
    [Name("Файл, с которым связан атрибут"), DisplayName("Файл")]
    [Cascade]
    public InternalFile? File { get; }
}
