using Simargl.Embedded.Tunings.TuningsEditor.Main.Enumerations;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;

/// <summary>
/// Представляет узел специального раздела спецификации.
/// </summary>
public abstract class SpecialSectionNode :
    SectionNode
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
    public SpecialSectionNode(NodeFormat format, Project project, SectionType sectionType) :
        base(format, project, sectionType)
    {
        //  Установка имени.
        Name.Value = "Специальный раздел";
    }
}
