using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;

/// <summary>
/// Представляет узел простого параметра-массива.
/// </summary>
public sealed class ArrayParamNode :
    ParamNode
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public ArrayParamNode(Project project) :
        base(NodeFormat.ArrayParam, project)
    {
        //  Установка имени.
        Name.Value = "Массив";

        //  Создание атрибутов.
        ValueType = Attributes.Add(
            NodeAttributeFormat.ValueType,
            "Тип значения",
            "Тип, который используется для хранения значения.",
            ["Binary", "String"]);

        //  Создание атрибутов.
        Size = Attributes.Add(
            NodeAttributeFormat.Size,
            "Размер",
            "Размер данных в байтах.",
            "1",
            x => byte.TryParse(x, out byte size) && size > 0);
    }

    /// <summary>
    /// Возвращает атрибут, определяющий тип значения.
    /// </summary>
    public NodeAttribute ValueType { get; }

    /// <summary>
    /// Возвращает атрибут, определяющий размер значения.
    /// </summary>
    public NodeAttribute Size { get; }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => byte.Parse(Size.Value);
}
