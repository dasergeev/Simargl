using Simargl.Code;
using Simargl.Embedded.Tunings.TuningsEditor.Core;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;
using Simargl.Payload;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;

/// <summary>
/// Представляет узел спецификации.
/// </summary>
public sealed class SpecNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public SpecNode(Project project) :
        base(NodeFormat.Spec, project)
    {
        //  Установка имени.
        Name.Value = "Спецификация";

        //  Создание атрибутов.
        Version = Attributes.Add(
            NodeAttributeFormat.Version,
            "Версия",
            "Версия спецификации, однозначно определяющая структуру данных.",
            DataVersion.Node.ToString(),
            DataVersion.IsValid);

        //  Создание команд.
        AddInfoSectionCommand = new(true, delegate
        {
            //  Добавление узла.
            Nodes.Add(new InfoSectionNode(project));
        });
        AddSeparateSectionCommand = new(true, delegate
        {
            //  Добавление узла.
            Nodes.Add(new SeparateSectionNode(project));
        });
        AddCriticalSectionCommand = new(true, delegate
        {
            //  Добавление узла.
            Nodes.Add(new CriticalSectionNode(project));
        });
        AddControlSectionCommand = new(true, delegate
        {
            //  Добавление узла.
            Nodes.Add(new ControlSectionNode(project));
        });
        AddDebugSectionCommand = new(true, delegate
        {
            //  Добавление узла.
            Nodes.Add(new DebugSectionNode(project));
        });
    }

    /// <summary>
    /// Возвращает атрибут, определяющий версию спецификации.
    /// </summary>
    public NodeAttribute Version { get; }

    /// <summary>
    /// Возвращает команду добавления нового раздела информации.
    /// </summary>
    public Command AddInfoSectionCommand { get; }

    /// <summary>
    /// Возвращает команду добавления нового раздела произвольного доступа.
    /// </summary>
    public Command AddSeparateSectionCommand { get; }

    /// <summary>
    /// Возвращает команду добавления нового критического раздела.
    /// </summary>
    public Command AddCriticalSectionCommand { get; }

    /// <summary>
    /// Возвращает команду добавления нового раздела управления.
    /// </summary>
    public Command AddControlSectionCommand { get; }

    /// <summary>
    /// Возвращает команду добавления нового раздела отладки.
    /// </summary>
    public Command AddDebugSectionCommand { get; }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => Nodes.Sum(x => x.GetDataSize());

    /// <summary>
    /// Возвращает коллекцию узлов параметров-перечислений.
    /// </summary>
    /// <returns>
    /// Коллекция узлов параметров-перечислений.
    /// </returns>
    public IEnumerable<EnumParamNode> GetEnumNodes()
    {
        //  Создание списка узлов.
        List<EnumParamNode> nodes = [];

        //  Перебор дочерних узлов.
        foreach (Node node in Nodes)
        {
            //  Проверка типа узла.
            if (node is SectionNode sectionNode)
            {
                //  Перебор параметров.
                foreach (Node paramNode in sectionNode.Nodes)
                {
                    //  Проверка типа узла.
                    if (paramNode is EnumParamNode enumParamNode)
                    {
                        //  Добавление в список.
                        nodes.Add(enumParamNode);
                    }
                }
            }
        }

        //  Возврат коллекции узлов.
        return nodes;
    }
}
