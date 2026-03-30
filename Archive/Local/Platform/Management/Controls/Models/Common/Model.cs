namespace Apeiron.Platform.Management.Models;

/// <summary>
/// Представляет модель.
/// </summary>
public sealed class Model :
    ModelNode
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal Model() :
        base("Модель")
    {
        //  Загрузка узла.
        Load();
    }

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    protected override sealed void LoadCore()
    {

    }
}
