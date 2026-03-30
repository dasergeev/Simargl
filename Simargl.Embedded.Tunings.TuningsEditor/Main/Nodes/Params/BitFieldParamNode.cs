using Simargl.Embedded.Tunings.TuningsEditor.Core;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Values;
using System.Text.Json;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;

/// <summary>
/// Представляет узел битового параметра.
/// </summary>
public sealed class BitFieldParamNode :
    ParamNode
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public BitFieldParamNode(Project project) :
        base(NodeFormat.BitFieldParam, project)
    {
        //  Установка имени.
        Name.Value = "Битовое поле";

        //  Создание команд.
        AddBitValueCommand = new(true, delegate
        {
            //  Добавление параметра.
            Nodes.Add(new BitValueNode(project));
        });

        //  Создание атрибутов.
        ValueType = Attributes.Add(
            NodeAttributeFormat.ValueType,
            "Тип значения",
            "Тип, который используется для хранения значения.",
            SimpleType.IntegerTypes.Select(x => x.Name));
    }

    /// <summary>
    /// Возвращает команду добавления значения.
    /// </summary>
    public Command AddBitValueCommand { get; }

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
