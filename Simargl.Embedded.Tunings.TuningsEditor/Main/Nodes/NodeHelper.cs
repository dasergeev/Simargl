using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Sections;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Params;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Values;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Errors;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;

/// <summary>
/// Предоставляет вспомогательные методы для узлов.
/// </summary>
public static class NodeHelper
{
    /// <summary>
    /// Поле для хранения карты данных.
    /// </summary>
    private static readonly Dictionary<NodeFormat,
        (Func<Project, Node> Creator, string ImageName, bool IsMovable, NodeFormat[] Childs)> _DataMap = new()
        {
            {NodeFormat.Root, (x => new RootNode(x),
                "Default.ico", false, [NodeFormat.Spec, NodeFormat.Error, NodeFormat.Generator]) },

            {NodeFormat.Spec, (x => new SpecNode(x),
                "Spec.ico", false, [
                NodeFormat.InfoSection,
                NodeFormat.SeparateSection,
                NodeFormat.CriticalSection,
                NodeFormat.ControlSection,
                NodeFormat.DebugSection]) },

            {NodeFormat.InfoSection, (x => new InfoSectionNode(x),
                "BlankGreen.ico", true, [NodeFormat.SimpleParam, NodeFormat.EnumParam, NodeFormat.BitFieldParam, NodeFormat.ArrayParam]) },
            {NodeFormat.SeparateSection, (x => new SeparateSectionNode(x),
                "BlankBlue.ico", true, [NodeFormat.SimpleParam, NodeFormat.EnumParam, NodeFormat.BitFieldParam, NodeFormat.ArrayParam]) },
            {NodeFormat.CriticalSection, (x => new CriticalSectionNode(x),
                "BlankRed.ico", true, [NodeFormat.SimpleParam, NodeFormat.EnumParam, NodeFormat.BitFieldParam, NodeFormat.ArrayParam]) },
            {NodeFormat.ControlSection, (x => new ControlSectionNode(x),
                "BlankPurple.ico", true, [NodeFormat.CommandParam]) },
            {NodeFormat.DebugSection, (x => new DebugSectionNode(x),
                "BlankOrange.ico", true, []) },

            {NodeFormat.SimpleParam, (x => new SimpleParamNode(x),
                "FlagGreen.png", true, []) },
            {NodeFormat.EnumParam, (x => new EnumParamNode(x),
                "FlagBlue.png", true, [NodeFormat.EnumValue]) },
            {NodeFormat.BitFieldParam, (x => new BitFieldParamNode(x),
                "FlagYellow.png", true, [NodeFormat.BitValue]) },
            {NodeFormat.ArrayParam, (x => new ArrayParamNode(x),
                "FlagRed.png", true, []) },
            {NodeFormat.CommandParam, (x => new CommandParamNode(x),
                "Command.png", true, [NodeFormat.CommandValue]) },

            {NodeFormat.EnumValue, (x => new EnumValueNode(x),
                "Enum.ico", true, []) },
            {NodeFormat.BitValue, (x => new BitValueNode(x),
                "Enum.ico", true, []) },
            {NodeFormat.CommandValue, (x => new CommandValueNode(x),
                "Enum.ico", true, []) },

            {NodeFormat.Error, (x => new ErrorNode(x),
                "ErrorNode.ico", false, [NodeFormat.ErrorValue]) },
            {NodeFormat.ErrorValue, (x => new ErrorValueNode(x),
                "ErrorValue.ico", true, []) },

            {NodeFormat.Generator, (x => new GeneratorNode(x),
                "Generator.ico", true, []) },
        };

    /// <summary>
    /// Создаёт новый узел.
    /// </summary>
    /// <param name="format">
    /// Формат узла, который требуется создать.
    /// </param>
    /// <param name="project">
    /// Проект.
    /// </param>
    /// <returns>
    /// Новый узел.
    /// </returns>
    public static Node Create(NodeFormat format, Project project)
    {
        //  Поиск в карте.
        return _DataMap[format].Creator(project);
    }

    /// <summary>
    /// Возвращает значение, определяющее является ли узел перемещаемым.
    /// </summary>
    public static bool IsMovable(NodeFormat format)
    {
        //  Проверка в карте.
        return _DataMap[format].IsMovable;
    }

    /// <summary>
    /// Проверяет, может ли узел с форматом <paramref name="parent"/>
    /// содержать узел с форматом <paramref name="child"/>.
    /// </summary>
    /// <param name="parent">
    /// Формат родительского узла.
    /// </param>
    /// <param name="child">
    /// Формат дочернего узла.
    /// </param>
    /// <returns>
    /// Результат проверки.
    /// </returns>
    public static bool CanContain(NodeFormat parent, NodeFormat child)
    {
        //  Проверка в карте.
        return _DataMap[parent].Childs.Contains(child);
    }

    /// <summary>
    /// Возвращает имя изображения для узла.
    /// </summary>
    /// <param name="format">
    /// Формат узла.
    /// </param>
    /// <returns>
    /// Имя изображения.
    /// </returns>
    public static string GetImageName(NodeFormat format)
    {
        //  Возврат имени изображения из карты.
        return _DataMap[format].ImageName;
    }
}
