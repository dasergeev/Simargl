using Simargl.Embedded.Tunings.TuningsEditor.Main.Enumerations;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;

/// <summary>
/// Представляет узел раздела спецификации с произвольны доступом.
/// </summary>
public sealed class SeparateSectionNode :
    CommonSectionNode
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public SeparateSectionNode(Project project) :
        base(NodeFormat.SeparateSection, project, SectionType.Separate)
    {
        //  Установка имени.
        Name.Value = "Раздел произвольного доступа";
    }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => Nodes.Sum(x => x.GetDataSize());
}
