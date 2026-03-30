using Simargl.Embedded.Tunings.TuningsEditor.Main.Enumerations;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;

/// <summary>
/// Представляет узел отладочного раздела спецификации.
/// </summary>
public sealed class DebugSectionNode :
    SpecialSectionNode
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public DebugSectionNode(Project project) :
        base(NodeFormat.DebugSection, project, SectionType.Debug)
    {
        //  Установка имени.
        Name.Value = "Раздел отладки";

        //  Создание атрибутов.
        RowCount = Attributes.Add(
            NodeAttributeFormat.RowCount,
            "Количество строк",
            "Количество отладочных строк, образующих циклический буфер.",
            "1",
            x => byte.TryParse(x, out byte value) && value > 0);
        RowSize = Attributes.Add(
            NodeAttributeFormat.RowSize,
            "Размер строки",
            "Размер отладочной строки в байтах.",
            "1",
            x => byte.TryParse(x, out byte value) && value > 0);
    }

    /// <summary>
    /// Возвращает атрибут, определяющий количество отладочных строк.
    /// </summary>
    public NodeAttribute RowCount { get; }

    /// <summary>
    /// Возвращает атрибут, определяющий размер отладочной строки.
    /// </summary>
    public NodeAttribute RowSize { get; }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => byte.Parse(RowCount.Value) * byte.Parse(RowSize.Value);
}
