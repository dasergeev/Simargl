using Simargl.Embedded.Tunings.TuningsEditor.Core;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Values;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;

/// <summary>
/// Представляет узел параметра-перечисления.
/// </summary>
public sealed class EnumParamNode :
    ParamNode
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public EnumParamNode(Project project) :
        base(NodeFormat.EnumParam, project)
    {
        //  Установка имени.
        Name.Value = "Перечисление";

        //  Создание команд.
        AddEnumValueCommand = new(true, delegate
        {
            //  Добавление параметра.
            Nodes.Add(new EnumValueNode(project));
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
    public Command AddEnumValueCommand { get; }

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

//    /// <summary>
//    /// Возвращает полное имя.
//    /// </summary>
//    /// <returns>
//    /// Полное имя.
//    /// </returns>
//    public string GetFullName()
//    {
//        //Parent.Identifier.Value

//    }

//    /// <summary>
//    /// Возвращает код С++.
//    /// </summary>
//    /// <returns>
//    /// Возврат кода C++.
//    /// </returns>
//    public string GetCppCode()
//    {
//        //  Создание построителя строки.
//        StringBuilder builder = new();

//        //  Запись пролога.
//        builder.AppendLine(
//@"enum class ");

//        //  Запись эпилога.
//        builder.AppendLine(
//@"");


//        /*

//enum class tunings_section_id : std::uint32_t {
//			unknown,
//			spec,		//	Раздел спецификации.
//			identity,	//	Раздел идентификации.
//			firmware,	//	Раздел информации о прошивке.
//			ports,		//	Раздел настройки портов.
//		};

//        */

//        //  Возврат строки.
//        return builder.ToString();
//    }
}
