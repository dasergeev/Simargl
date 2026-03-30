using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Values;

/// <summary>
/// Представляет узел значения команды.
/// </summary>
public sealed class CommandValueNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public CommandValueNode(Project project) :
        base(NodeFormat.CommandValue, project)
    {
        //  Установка имени.
        Name.Value = "Значение";

        //  Создание атрибутов.
        Value = Attributes.Add(
            NodeAttributeFormat.ValueType,
            "Значение",
            "Значение, которое используется в коде.",
            "0",
            x => byte.TryParse(x, out byte value));
    }

    /// <summary>
    /// Возвращает атрибут, определяющий значение.
    /// </summary>
    public NodeAttribute Value { get; }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => 0;
}
