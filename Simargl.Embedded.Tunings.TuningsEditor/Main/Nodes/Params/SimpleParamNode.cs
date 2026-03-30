using Simargl.Embedded.Tunings.TuningsEditor.Core;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;

/// <summary>
/// Представляет узел простого параметра.
/// </summary>
public sealed class SimpleParamNode :
    ParamNode
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public SimpleParamNode(Project project) :
        base(NodeFormat.SimpleParam, project)
    {
        //  Установка имени.
        Name.Value = "Параметр";

        //  Создание атрибутов.
        ValueType = Attributes.Add(
            NodeAttributeFormat.ValueType,
            "Тип значения",
            "Тип, который используется для хранения значения.",
            SimpleType.AllTypes.Select(x => x.Name));
    }

    /// <summary>
    /// Возвращает атрибут, определяющий тип значения.
    /// </summary>
    public NodeAttribute ValueType { get; }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => SimpleType.FromString(ValueType.Value).Size;
}
