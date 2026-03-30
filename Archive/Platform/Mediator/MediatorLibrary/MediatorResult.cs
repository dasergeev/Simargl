namespace Apeiron.Platform.MediatorLibrary;

/// <summary>
/// Представляет значение определяющее формат результата вызова.
/// </summary>
public enum MediatorResult
{
    /// <summary>
    /// Пустые данные.
    /// </summary>
    Void = 0,

    /// <summary>
    /// Данные.
    /// </summary>
    Data = 1,

    /// <summary>
    /// Ошибка.
    /// </summary>
    Error = 2,
}
