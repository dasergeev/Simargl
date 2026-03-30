namespace Apeiron.Platform.Utilities.Generation.Models;

/// <summary>
/// Представляет альтернативный ключ.
/// </summary>
public sealed class AlternateKeyModel :
    Model<AlternateKeyModel>
{
    /// <summary>
    /// Возвращает название ключа.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Возвращает коллекцию полей альтернативного ключа.
    /// </summary>
    public List<string> Fields { get; init; } = null!;

    /// <summary>
    /// Выполняет построение модели объекта.
    /// </summary>
    /// <param name="generator">
    /// Генератор кода.
    /// </param>
    public override void Build([ParameterNoChecks] Generator generator)
    {
        _ = generator;
    }
}
