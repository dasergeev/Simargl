namespace Apeiron.Platform.Utilities.Sources;

[Name("Информация о формате фильтра"), Category("Processing")]
[TableName("FilterFormats")]
internal sealed class FilterFormat :
    NamedEntity
{
    [Order(1)]
    [Name("Описание формата"), DisplayName("Описание")]
    public string? Description { get; }
}
