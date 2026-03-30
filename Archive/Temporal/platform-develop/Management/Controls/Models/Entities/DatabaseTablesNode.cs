using Apeiron.Platform.Management.Controls;

namespace Apeiron.Platform.Management.Models.Entities;

/// <summary>
/// Представляет узел, связанный с таблицами базы данных.
/// </summary>
public sealed class DatabaseTablesNode :
    ModelNode
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public DatabaseTablesNode() :
        base("Таблицы")
    {

    }

    /// <summary>
    /// Возвращает формат рабочего элемента управления, отображающего узел.
    /// </summary>
    internal override WorkControlFormat ControlFormat => WorkControlFormat.DatabaseTables;

    /// <summary>
    /// Выполняет загрузку узла.
    /// </summary>
    protected override sealed void LoadCore()
    {

    }

}
