using Simargl.Embedded.Tunings.TuningsEditor.Core;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Enumerations;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;

/// <summary>
/// Представляет узел управляющего раздела спецификации.
/// </summary>
public sealed class ControlSectionNode :
    SpecialSectionNode
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public ControlSectionNode(Project project) :
        base(NodeFormat.ControlSection, project, SectionType.Control)
    {
        //  Установка имени.
        Name.Value = "Раздел управления";

        //  Создание команд.
        AddCommandParamCommand = new(true, delegate
        {
            //  Добавление параметра.
            Nodes.Add(new CommandParamNode(project));
        });
    }

    /// <summary>
    /// Возвращает команду добавления параметра-команды.
    /// </summary>
    public Command AddCommandParamCommand { get; }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => 0;// Nodes.Sum(x => x.GetDataSize());
}
