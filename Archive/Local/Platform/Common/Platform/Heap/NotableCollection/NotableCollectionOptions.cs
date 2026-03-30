namespace Apeiron.Collections;

/// <summary>
/// Представляет настройки отслеживаемой коллекции.
/// </summary>
public sealed class NotableCollectionOptions :
    CollectionOptions
{
    /// <summary>
    /// Возвращает настройки коллекции по умолчанию.
    /// </summary>
    public static new readonly NotableCollectionOptions Default = new();

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public NotableCollectionOptions()
    {

    }

    /// <summary>
    /// Возвращает или инициализирует метод, который выполняет делегат в базовом потоке.
    /// </summary>
    /// <remarks>
    /// Используется для вызова событий.
    /// </remarks>
    public Action<Action>? Invoker { get; init; }
}
