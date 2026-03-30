namespace Apeiron.Platform.Utilities.Generation.Attributes;

/// <summary>
/// Представляет атрибут, указывающий отображаемое имя.
/// </summary>
[AttributeUsage(
    AttributeTargets.Property,
    AllowMultiple = false,
    Inherited = false)]
public sealed class DisplayNameAttribute :
    Attribute
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="displayName">
    /// Отображаемое имя.
    /// </param>
    public DisplayNameAttribute([ParameterNoChecks] string displayName)
    {
        //  Установка отображаемого имени.
        DisplayName = displayName;
    }

    /// <summary>
    /// Возвращает отображаемое имя.
    /// </summary>
    public string DisplayName { get; }
}
