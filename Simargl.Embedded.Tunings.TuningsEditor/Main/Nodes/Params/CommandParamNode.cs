using Simargl.Embedded.Tunings.TuningsEditor.Core;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Values;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;

/// <summary>
/// Представляет узел параметра-команды.
/// </summary>
public sealed class CommandParamNode :
    ParamNode
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public CommandParamNode(Project project) :
        base(NodeFormat.CommandParam, project)
    {
        //  Установка имени.
        Name.Value = "Команда";

        //  Создание команд.
        AddCommandValueCommand = new(true, delegate
        {
            //  Добавление параметра.
            Nodes.Add(new CommandValueNode(project));
        });
    }

    /// <summary>
    /// Возвращает команду добавления значения.
    /// </summary>
    public Command AddCommandValueCommand { get; }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => 0;
}
