namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет коллекцию схем простых типов.
/// </summary>
public sealed class SimpleTypeSchemeCollection
{
    /// <summary>
    /// Поле для хранения словаря схем.
    /// </summary>
    private readonly SortedDictionary<string, SimpleTypeScheme> _Schemes;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal SimpleTypeSchemeCollection()
    {
        //  Создание словаря схем.
        _Schemes = new();

        //  Заполнение словаря схем.
        foreach (SimpleTypeScheme scheme in new SimpleTypeScheme[]
        {
            SimpleTypeScheme.Byte,
            SimpleTypeScheme.DateTime,
            SimpleTypeScheme.Flag,
            SimpleTypeScheme.Boolean,
            SimpleTypeScheme.Float64,
            SimpleTypeScheme.Double,
            SimpleTypeScheme.Int32,
            SimpleTypeScheme.Int32Positive,
            SimpleTypeScheme.Int64,
            SimpleTypeScheme.Int64Positive,
            SimpleTypeScheme.String,
            SimpleTypeScheme.StringNullable
        })
        {
            //  Добавление схемы в слварь.
            _Schemes.Add(scheme.TypeName, scheme);
        }
    }

    /// <summary>
    /// Возвращает схему с указанным именем.
    /// </summary>
    /// <param name="typeName">
    /// Имя схемы.
    /// </param>
    /// <returns>
    /// Схема с указанным именем.
    /// </returns>
    public SimpleTypeScheme this[string typeName] => _Schemes[typeName];
}
