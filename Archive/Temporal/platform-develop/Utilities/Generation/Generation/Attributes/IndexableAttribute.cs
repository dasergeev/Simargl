namespace Apeiron.Platform.Utilities.Generation.Attributes;

/// <summary>
/// Представляет атрибут, указывающий на индексируемое значение.
/// </summary>
[AttributeUsage(
    AttributeTargets.Property,
    AllowMultiple = false,
    Inherited = false)]
public sealed class IndexableAttribute :
    Attribute
{

}
