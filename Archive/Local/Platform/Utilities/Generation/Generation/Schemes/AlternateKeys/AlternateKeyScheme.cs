namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет схему альтернативного ключа.
/// </summary>
public sealed class AlternateKeyScheme :
    Scheme
{
    /// <summary>
    /// Поле для хранения атрибута с описанием ключа.
    /// </summary>
    private readonly AlternateKeyAttribute _Attribute;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="entityScheme">
    /// Схема сущности.
    /// </param>
    /// <param name="attribute">
    /// Атрибут с описанием ключа.
    /// </param>
    internal AlternateKeyScheme(
        [ParameterNoChecks] EntityScheme entityScheme,
        [ParameterNoChecks] AlternateKeyAttribute attribute) :
        base(entityScheme.GeneralScheme)
    {
        //  Установка схемы сущности.
        EntityScheme = entityScheme;

        //  Установка атрибута с описанием ключа.
        _Attribute = attribute;
    }

    /// <summary>
    /// Возвращает схему сущности.
    /// </summary>
    internal EntityScheme EntityScheme { get; }

    /// <summary>
    /// Возвращает имя ключа.
    /// </summary>
    public string Name => _Attribute.Name;

    /// <summary>
    /// Возвращает коллекцию полей ключа.
    /// </summary>
    public IEnumerable<string> Fields => _Attribute.Fields;
}
