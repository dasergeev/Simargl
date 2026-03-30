namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет схему типа.
/// </summary>
/// <typeparam name="TTypeScheme">
/// Тип схемы типа.
/// </typeparam>
public abstract class TypeScheme<TTypeScheme>
    where TTypeScheme : TypeScheme<TTypeScheme>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal TypeScheme()
    {

    }
}
