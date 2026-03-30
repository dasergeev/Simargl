namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes.Attributes;

/// <summary>
/// Представляет значение, определяющее формат атрибута узла.
/// </summary>
public enum NodeAttributeFormat
{
    /// <summary>
    /// Имя узла.
    /// </summary>
    Name,

    /// <summary>
    /// Идентификатор.
    /// </summary>
    Identifier,

    /// <summary>
    /// Пространство имён.
    /// </summary>
    Namespace,

    /// <summary>
    /// Версия.
    /// </summary>
    Version,

    /// <summary>
    /// Тип значения.
    /// </summary>
    ValueType,

    /// <summary>
    /// Размер.
    /// </summary>
    Size,

    /// <summary>
    /// Описание.
    /// </summary>
    Description,

    /// <summary>
    /// Значение.
    /// </summary>
    Value,

    /// <summary>
    /// Количесто отладочных строк.
    /// </summary>
    RowCount,

    /// <summary>
    /// Размер отладочной строки.
    /// </summary>
    RowSize,

    /// <summary>
    /// Пусть к файлу C++.
    /// </summary>
    CppPath,

    /// <summary>
    /// Пусть к файлу C#.
    /// </summary>
    CSharpPath,

    /// <summary>
    /// Имя основного класса.
    /// </summary>
    CppMainName,
}
