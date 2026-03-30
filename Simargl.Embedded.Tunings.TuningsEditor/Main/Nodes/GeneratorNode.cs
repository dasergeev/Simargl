using Simargl.Code;
using Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;

namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;

/// <summary>
/// Представляет узел генерации кода.
/// </summary>
public sealed class GeneratorNode :
    Node
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="project">
    /// Проект.
    /// </param>
    public GeneratorNode(Project project) :
        base(NodeFormat.Generator, project)
    {
        //  Установка имени.
        Name.Value = "Генератор";

        //  Создание атрибутов.
        Namespace = Attributes.Add(
            NodeAttributeFormat.Namespace,
            "Пространство имён",
            "Корневое пространство имён, в которое помещается весь генерируемый код.",
            x => x.Length == 0 || SyntaxValidator.IsNamespace(x, "::"));
        CppPath = Attributes.Add(
            NodeAttributeFormat.CppPath,
            "Файл C++",
            "Путь к файлу для записи исходного кода на С++.");
        CSharpPath = Attributes.Add(
            NodeAttributeFormat.CSharpPath,
            "Файл C#",
            "Путь к файлу для записи исходного кода на С#.");
        CppMainName = Attributes.Add(
            NodeAttributeFormat.CppMainName,
            "Вход С++",
            "Имя основного класса C++.",
            "Knot",
            SyntaxValidator.IsIdentifier);
    }

    /// <summary>
    /// Возвращает атрибут, определяющий корневое пространство имён.
    /// </summary>
    public NodeAttribute Namespace { get; }

    /// <summary>
    /// Возвращает атрибут, определяющий путь к файлу C++.
    /// </summary>
    public NodeAttribute CppPath { get; }

    /// <summary>
    /// Возвращает атрибут, определяющий путь к файлу C#.
    /// </summary>
    public NodeAttribute CSharpPath { get; }

    /// <summary>
    /// Возвращает атрибут, определяющий имя основного класса.
    /// </summary>
    public NodeAttribute CppMainName { get; }

    /// <summary>
    /// Возвращает размер данных.
    /// </summary>
    /// <returns>
    /// Размер данных.
    /// </returns>
    public override sealed int GetDataSize() => 0;
}
