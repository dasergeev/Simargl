namespace Apeiron.Platform.Utilities.Generation.Models;

/// <summary>
/// Представляет коллекцию сущностей.
/// </summary>
public sealed class EntityModelCollection :
    ModelCollection<EntityModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public EntityModelCollection() :
        base(".entity.json", entityModel => entityModel.TypeName)
    {

    }
}
