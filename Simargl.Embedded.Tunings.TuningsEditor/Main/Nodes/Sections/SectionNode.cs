using Simargl.Embedded.Tunings.TuningsEditor.Main.Enumerations;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;

/// <summary>
/// Представляет узел раздела спецификации.
/// </summary>
public abstract class SectionNode :
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
    /// <param name="sectionType">
    /// Значение, определяющее тип раздела спецификации.
    /// </param>
    public SectionNode(NodeFormat format, Project project, SectionType sectionType) :
        base(format, project)
    {
        //  Установка имени.
        Name.Value = "Раздел";
    }
}
