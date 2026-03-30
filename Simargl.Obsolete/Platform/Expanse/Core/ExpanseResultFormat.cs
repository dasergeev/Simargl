namespace Simargl.Platform.Expanse.Core;

/// <summary>
/// Возвращает тип результата метода, выполняемого в серверном пространстве.
/// </summary>
public enum ExpanseResultFormat
{
    /// <summary>
    /// Пустой результат.
    /// </summary>
    Void,

    /// <summary>
    /// Информация.
    /// </summary>
    Information,

    /// <summary>
    /// Исключение.
    /// </summary>
    Exception,
}
