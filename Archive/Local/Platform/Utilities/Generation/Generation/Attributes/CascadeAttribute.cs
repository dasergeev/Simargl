namespace Apeiron.Platform.Utilities.Generation.Attributes;

/// <summary>
/// Представляет атрибут, указывающий на каскадное удаление связанных сущностей.
/// </summary>
[AttributeUsage(
    AttributeTargets.Property,
    AllowMultiple = false,
    Inherited = false)]
public sealed class CascadeAttribute :
    Attribute
{

}
