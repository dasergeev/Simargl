using Simargl.Embedded.Tunings.TuningsEditor.Core;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Enumerations;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;

/// <summary>
/// Представляет узел общего раздела спецификации.
/// </summary>
public abstract class CommonSectionNode :
    SectionNode
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="format">
    /// Формат узла.
    /// </param>
    /// <param name="project">
    /// Проект.
    /// </param>
    /// <param name="sectionType">
    /// Значение, определяющее тип раздела спецификации.
    /// </param>
    public CommonSectionNode(NodeFormat format, Project project, SectionType sectionType) :
        base(format, project, sectionType)
    {
        //  Установка имени.
        Name.Value = "Общий раздел";

        //  Создание команд.
        AddSimpleParamCommand = new(true, delegate
        {
            //  Добавление пареметра.
            Nodes.Add(new SimpleParamNode(project));
        });
        EnumParamCommand = new(true, delegate
        {
            //  Добавление пареметра.
            Nodes.Add(new EnumParamNode(project));
        });
        BitFieldParamCommand = new(true, delegate
        {
            //  Добавление пареметра.
            Nodes.Add(new BitFieldParamNode(project));
        });
        ArrayParamCommand = new(true, delegate
        {
            //  Добавление пареметра.
            Nodes.Add(new ArrayParamNode(project));
        });
    }

    /// <summary>
    /// Возвращает команду добавления простого параметра.
    /// </summary>
    public Command AddSimpleParamCommand { get; }

    /// <summary>
    /// Возвращает команду добавления параметра-перечисления.
    /// </summary>
    public Command EnumParamCommand { get; }

    /// <summary>
    /// Возвращает команду добавления битового параметра.
    /// </summary>
    public Command BitFieldParamCommand { get; }

    /// <summary>
    /// Возвращает команду добавления параметра-массива.
    /// </summary>
    public Command ArrayParamCommand { get; }
}
