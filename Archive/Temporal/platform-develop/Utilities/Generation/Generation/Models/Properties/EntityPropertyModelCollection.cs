namespace Apeiron.Platform.Utilities.Generation.Models;

/// <summary>
/// Представляет коллекцию моделей свойств сущности.
/// </summary>
public sealed class EntityPropertyModelCollection :
    ModelCollection<EntityPropertyModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public EntityPropertyModelCollection() :
        base(".property.json", entityPropertyModel => entityPropertyModel.PropertyName)
    {
        
    }

}
