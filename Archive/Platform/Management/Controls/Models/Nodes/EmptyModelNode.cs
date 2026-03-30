namespace Apeiron.Platform.Management.Models.Nodes;

/// <summary>
/// Представляет пустой узел.
/// </summary>
internal class EmptyModelNode :
    ModelNode
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public EmptyModelNode() :
        base("Загрузка...")
    {

    }

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    protected override sealed void LoadCore()
    {

    }
}
