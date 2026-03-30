using Simargl.Code;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Values;

/// <summary>
/// Представляет узел значения перечисления.
/// </summary>
public sealed class EnumValueNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public EnumValueNode(Project project) :
        base(NodeFormat.EnumValue, project)
    {
        //  Установка имени.
        Name.Value = "Значение";
        
        //  Создание атрибутов.
        Value = Attributes.Add(
            NodeAttributeFormat.ValueType,
            "Значение",
            "Значение, которое используется в коде.",
            "0",
            SyntaxValidator.IsIntegerLiteral);
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
