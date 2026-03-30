namespace Simargl.Embedded.Tunings.TuningsEditor.Main.Nodes;

/// <summary>
/// Представляет значение, определяющее формат узла.
/// </summary>
public enum NodeFormat
{
    /// <summary>
    /// Корневой узел.
    /// </summary>
    Root,

    /// <summary>
    /// Узел спецификации.
    /// </summary>
    Spec,

    /// <summary>
    /// Узел раздела спецификации: Раздел информации.
    /// </summary>
    InfoSection,

    /// <summary>
    /// Узел раздела спецификации: Раздел произвольного доступа.
    /// </summary>
    SeparateSection,

    /// <summary>
    /// Узел раздела спецификации: Критический раздел.
    /// </summary>
    CriticalSection,

    /// <summary>
    /// Узел раздела спецификации: Раздел управления.
    /// </summary>
    ControlSection,

    /// <summary>
    /// Узел раздела спецификации: Раздел отладки.
    /// </summary>
    DebugSection,

    /// <summary>
    /// Узел простого параметра.
    /// </summary>
    SimpleParam,

    /// <summary>
    /// Узел параметра-перечисления.
    /// </summary>
    EnumParam,

    /// <summary>
    /// Узел битового параметра.
    /// </summary>
    BitFieldParam,

    /// <summary>
    /// Узел параметра-массива.
    /// </summary>
    ArrayParam,

    /// <summary>
    /// Узел параметра-команды.
    /// </summary>
    CommandParam,

    /// <summary>
    /// Узел значения перечисления.
    /// </summary>
    EnumValue,

    /// <summary>
    /// Узел бита битового поля.
    /// </summary>
    BitValue,

    /// <summary>
    /// Узел значения команды.
    /// </summary>
    CommandValue,

    /// <summary>
    /// Узел кодов ошибок.
    /// </summary>
    Error,

    /// <summary>
    /// Узел значения ошибки.
    /// </summary>
    ErrorValue,

    /// <summary>
    /// Узел генерации кода.
    /// </summary>
    Generator,
}
