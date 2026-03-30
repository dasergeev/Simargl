namespace Apeiron.Platform.Utilities.Generation.Models;

/// <summary>
/// Представляет коллекцию моделей связей.
/// </summary>
public sealed class RelationModelCollection :
    ModelCollection<RelationModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public RelationModelCollection() :
        base(".relation.json", relationModel => relationModel.EntityTypeName)
    {

    }
}
