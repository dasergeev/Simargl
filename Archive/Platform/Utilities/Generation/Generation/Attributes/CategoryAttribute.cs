namespace Apeiron.Platform.Utilities.Generation.Attributes;

/// <summary>
/// Представляет атрибут, указывающий категорию.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class CategoryAttribute :
    Attribute
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="category">
    /// Категория.
    /// </param>
    public CategoryAttribute([ParameterNoChecks] string category)
    {
        //  Установка категории.
        Category = category;
    }

    /// <summary>
    /// Возвращает категорию.
    /// </summary>
    public string Category { get; }
}
