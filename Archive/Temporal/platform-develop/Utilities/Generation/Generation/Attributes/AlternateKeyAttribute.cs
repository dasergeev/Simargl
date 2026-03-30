namespace Apeiron.Platform.Utilities.Generation.Attributes;

/// <summary>
/// Представляет атрибут, указывающий имя таблицы.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public sealed class AlternateKeyAttribute :
    Attribute
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="name">
    /// Имя ключа.
    /// </param>
    /// <param name="fields">
    /// Коллекция полей ключа.
    /// </param>
    public AlternateKeyAttribute(
        [ParameterNoChecks] string name,
        [ParameterNoChecks] params string[] fields)
    {
        //  Установка имени таблицы.
        Name = name;

        //  Установка коллекции полей ключа.
        Fields = new List<string>(fields).AsReadOnly();
    }

    /// <summary>
    /// Возвращает имя ключа.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Возвращает коллекцию полей ключа.
    /// </summary>
    public IEnumerable<string> Fields { get; }
}
