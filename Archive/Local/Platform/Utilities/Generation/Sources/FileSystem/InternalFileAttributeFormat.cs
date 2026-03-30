namespace Apeiron.Platform.Utilities.Sources;

[Name("Формат атрибута внутреннего файла файловой системы"), Category("FileSystem")]
[TableName("InternalFileAttributeFormats")]
internal sealed class InternalFileAttributeFormat :
    NamedEntity
{
    [Order(1)]
    [Name("Описание атрибута"), DisplayName("Описание")]
    public string? Description { get; }
}
