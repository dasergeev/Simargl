using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Values;

/// <summary>
/// Представляет узел бита битового поля.
/// </summary>
public sealed class BitValueNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public BitValueNode(Project project) :
        base(NodeFormat.BitValue, project)
    {
        //  Установка имени.
        Name.Value = "Значение";

        //  Создание атрибутов.
        Value = Attributes.Add(
            NodeAttributeFormat.ValueType,
            "Значение",
            "Значение, которое используется в коде.",
            "0",
            x => byte.TryParse(x, out byte value) && value < 64);
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
