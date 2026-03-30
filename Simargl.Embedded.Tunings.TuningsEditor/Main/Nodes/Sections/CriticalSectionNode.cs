using Simargl.Embedded.Tunings.TuningsEditor.Main.Enumerations;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;

/// <summary>
/// Представляет узел критического раздела спецификации.
/// </summary>
public sealed class CriticalSectionNode :
    CommonSectionNode
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public CriticalSectionNode(Project project) :
        base(NodeFormat.CriticalSection, project, SectionType.Critical)
    {
        //  Установка имени.
        Name.Value = "Критический раздел";
    }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => Nodes.Sum(x => x.GetDataSize());
}
