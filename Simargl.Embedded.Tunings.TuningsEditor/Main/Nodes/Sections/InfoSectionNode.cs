using Simargl.Embedded.Tunings.TuningsEditor.Main.Enumerations;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;

/// <summary>
/// Представляет узел информационного раздела спецификации.
/// </summary>
public sealed class InfoSectionNode :
    CommonSectionNode
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public InfoSectionNode(Project project) :
        base(NodeFormat.InfoSection, project, SectionType.Info)
    {
        //  Установка имени.
        Name.Value = "Раздел информации";
    }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => Nodes.Sum(x => x.GetDataSize());
}
