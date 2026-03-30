namespace Apeiron.Platform.Utilities.Generation.Attributes;

/// <summary>
/// Представляет атрибут, указывающий на значение, которое может принимать нулевое значение.
/// </summary>
[AttributeUsage(
    AttributeTargets.Property,
    AllowMultiple = false,
    Inherited = false)]
public sealed class NullableAttribute :
    Attribute
{

}
