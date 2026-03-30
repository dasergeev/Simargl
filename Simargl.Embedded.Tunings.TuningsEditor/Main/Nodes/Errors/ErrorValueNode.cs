using Simargl.Code;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Errors;

/// <summary>
/// Представляет узел значения ошибки.
/// </summary>
public sealed class ErrorValueNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public ErrorValueNode(Project project) :
        base(NodeFormat.ErrorValue, project)
    {
        //  Установка имени.
        Name.Value = "Значение";

        //  Создание атрибутов.
        Value = Attributes.Add(
            NodeAttributeFormat.ValueType,
            "Значение",
            "Значение, которое используется в коде.",
            "1",
            x => SyntaxValidator.IsIntegerLiteral(x) && uint.TryParse(x, out uint value) && value > 0);
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
