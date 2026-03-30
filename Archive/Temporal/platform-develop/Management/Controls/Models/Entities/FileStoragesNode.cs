namespace Apeiron.Platform.Management.Models.Entities;

/// <summary>
/// Представляет узел, связанный с файловыми хранилищами.
/// </summary>
public sealed class FileStoragesNode :
    ModelNode
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public FileStoragesNode() :
        base("Файловые хранилища")
    {

    }

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    protected override sealed void LoadCore()
    {

    }
}
