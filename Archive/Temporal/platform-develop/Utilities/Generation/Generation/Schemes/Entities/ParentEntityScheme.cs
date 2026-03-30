namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет родительскую сущность.
/// </summary>
public sealed class ParentEntityScheme :
    EntityScheme
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="generalScheme">
    /// Общая схема.
    /// </param>
    /// <param name="sourceType">
    /// Исходный тип.
    /// </param>
    internal ParentEntityScheme(
        [ParameterNoChecks] GeneralScheme generalScheme,
        [ParameterNoChecks] Type sourceType) :
        base(generalScheme, sourceType, false)
    {

    }
}
