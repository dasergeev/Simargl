namespace Apeiron.Platform.Utilities.Sources;

[Name("Параметр микрослужбы"), Category("Settings")]
[TableName("MicroserviceSettings")]
internal abstract class MicroserviceSetting :
    Entity
{
    [Order(1)]
    [Name("Имя параметра"), DisplayName("Имя")]
    [Indexable]
    public string? Name { get; }

    [Order(2)]
    [Name("Описание параметра"), DisplayName("Описание")]
    public string? Description { get; }
}
