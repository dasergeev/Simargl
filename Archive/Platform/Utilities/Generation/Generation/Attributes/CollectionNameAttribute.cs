namespace Apeiron.Platform.Utilities.Generation.Attributes;

/// <summary>
/// Представляет атрибут, указывающий имя коллекции.
/// </summary>
[AttributeUsage(
    AttributeTargets.Class,
    AllowMultiple = false,
    Inherited = false)]
public sealed class CollectionNameAttribute :
    Attribute
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="collectionName">
    /// Имя коллекции.
    /// </param>
    public CollectionNameAttribute([ParameterNoChecks] string collectionName)
    {
        //  Установка имени коллекции.
        CollectionName = collectionName;
    }

    /// <summary>
    /// Возвращает имя коллекции.
    /// </summary>
    public string CollectionName { get; }
}
