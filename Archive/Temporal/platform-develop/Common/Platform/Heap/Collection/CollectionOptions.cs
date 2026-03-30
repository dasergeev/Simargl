namespace Apeiron.Collections;

/// <summary>
/// Представляет настройки коллекции.
/// </summary>
public class CollectionOptions
{
    /// <summary>
    /// Возвращает настройки коллекции по умолчанию.
    /// </summary>
    public static readonly CollectionOptions Default = new();

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public CollectionOptions()
    {

    }

    /// <summary>
    /// Возвращает или инициализирует значение, определяющее, является ли коллекция потокобезопасной.
    /// </summary>
    public bool IsConcurrent { get; init; }

    /// <summary>
    /// Возвращает или инициализирует значение, определяющее, выполняется ли проверка пустых ссылок.
    /// </summary>
    public bool IsCheckNull { get; init; }

    /// <summary>
    /// Возвращает или инициализирует значение, определяющее, выполняется ли проверка дублирования элементов.
    /// </summary>
    public bool IsCheckDuplicate { get; init; }
}
