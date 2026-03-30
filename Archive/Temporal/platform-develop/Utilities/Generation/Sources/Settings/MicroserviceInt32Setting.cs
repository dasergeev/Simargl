namespace Apeiron.Platform.Utilities.Sources;

[Name("Целочисленный параметр микрослужбы"), Category("Settings")]
[TableName("MicroserviceInt32Settings"), CollectionName("Int32Settings")]
[AlternateKey("Уникальное значение в пределах микрослужбы",
    nameof(Microservice) + "Id", nameof(Name))]
internal sealed class MicroserviceInt32Setting :
    MicroserviceSetting
{
    [Order(1)]
    [Name("Значение параметра"), DisplayName("Значение")]
    public int Value { get; }

    [Order(2)]
    [Name("Микрослужба"), DisplayName("Микрослужба")]
    [Cascade]
    public MicroserviceInfo? Microservice { get; }
}
