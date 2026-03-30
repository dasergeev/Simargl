namespace Apeiron.Platform.Utilities.Generation.Attributes;

/// <summary>
/// Представляет атрибут, указывающий имя.
/// </summary>
[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Property,
    AllowMultiple = false,
    Inherited = false)]
public sealed class NameAttribute :
    Attribute
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="name">
    /// Имя.
    /// </param>
    public NameAttribute([ParameterNoChecks] string name)
    {
        //  Установка имени.
        Name = name;
    }

    /// <summary>
    /// Возвращает имя.
    /// </summary>
    public string Name { get; }
}
