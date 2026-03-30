namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;

/// <summary>
/// Представляет узел параметра.
/// </summary>
public abstract class ParamNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="format">
    /// Формат узла.
    /// </param>
    /// <param name="project">
    /// Проект.
    /// </param>
    public ParamNode(NodeFormat format, Project project) :
        base(format, project)
    {
        //  Установка имени.
        Name.Value = "Параметр";
    }

    ///// <summary>
    ///// Возвращает значение, определяющее является ли узел перемещаемым.
    ///// </summary>
    //public override sealed bool IsMovable => true;

    ///// <summary>
    ///// Возвращает значение, определяющее, может ли узел содержать данный.
    ///// </summary>
    ///// <param name="node">
    ///// Узел для проверки.
    ///// </param>
    ///// <returns>
    ///// Результат проверки.
    ///// </returns>
    //public override sealed bool CanContain(Node node) => false;
}
