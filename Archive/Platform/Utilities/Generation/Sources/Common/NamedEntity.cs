namespace Apeiron.Platform.Utilities.Sources;

[Name("Именованный объект"), Category("Common")]
internal abstract class NamedEntity :
    Entity
{
    [Order(1)]
    [Name("Имя объекта"), DisplayName("Имя")]
    [Uniquable, Indexable]
    public string Name { get; } = null!;
}
