namespace Apeiron.Platform.Databases.CentralDatabase.Entities;

/// <summary>
/// Представляет модель сущности.
/// </summary>
public sealed class EntityModel
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="name">
    /// Название сущности.
    /// </param>
    /// <param name="category">
    /// Категория сущности.
    /// </param>
    /// <param name="typeName">
    /// Имя типа сущности.
    /// </param>
    /// <param name="collectionName">
    /// Имя коллекции сущностей.
    /// </param>
    public EntityModel(string name, string category, string typeName, string collectionName)
    {
        //  Установка названия сущности.
        Name = Check.IsNotNull(name, nameof(name));

        //  Установка категории сущности.
        Category = Check.IsNotNull(category, nameof(category));

        //  Установка имени типа сущности.
        TypeName = Check.IsNotNull(typeName, nameof(typeName));

        //  Установка имени коллекции сущностей.
        CollectionName = Check.IsNotNull(collectionName, nameof(collectionName));
    }

    /// <summary>
    /// Возвращает название сущности.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает категорию сущности.
    /// </summary>
    public string Category { get; }

    /// <summary>
    /// Возвращает имя типа сущности.
    /// </summary>
    public string TypeName { get; }

    /// <summary>
    /// Возвращает имя коллекции сущностей.
    /// </summary>
    public string CollectionName { get; }
}
