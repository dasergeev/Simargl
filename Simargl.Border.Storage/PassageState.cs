namespace Simargl.Border.Storage;

/// <summary>
/// Представляет значение, определяющее состояние данных одного проезда.
/// </summary>
public enum PassageState
{
    /// <summary>
    /// Данные зарегистрированы.
    /// </summary>
    Registered,

    /// <summary>
    /// Данные обработаны.
    /// </summary>
    Processed,

    /// <summary>
    /// Данные сжаты.
    /// </summary>
    Zip,

    /// <summary>
    /// Данные сохранены.
    /// </summary>
    Conservated,

    /// <summary>
    /// Данные преобразованы.
    /// </summary>
    Converted,
}
