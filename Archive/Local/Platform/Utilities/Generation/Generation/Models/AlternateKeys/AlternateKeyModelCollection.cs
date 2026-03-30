namespace Apeiron.Platform.Utilities.Generation.Models;

/// <summary>
/// Представляет коллекцию альтернативных ключей.
/// </summary>
public sealed class AlternateKeyModelCollection :
    ModelCollection<AlternateKeyModel>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public AlternateKeyModelCollection() :
        base(".key.json", alternateKeyModel => alternateKeyModel.Name)
    {

    }
}
