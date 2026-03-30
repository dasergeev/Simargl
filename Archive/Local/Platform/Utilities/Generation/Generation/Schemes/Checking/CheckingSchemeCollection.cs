namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет коллекцию схем проверки значений.
/// </summary>
public sealed class CheckingSchemeCollection
{
    /// <summary>
    /// Поле для хранения словаря схем.
    /// </summary>
    private readonly SortedDictionary<string, CheckingScheme> _Schemes;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal CheckingSchemeCollection()
    {
        //  Создание словаря схем.
        _Schemes = new();

        //  Заполнение словаря схем.
        foreach (CheckingScheme scheme in new CheckingScheme[]
        {
            CheckingScheme.IsAnything,
            CheckingScheme.IsNotNull,
            CheckingScheme.IsPositive
        })
        {
            //  Добавление схемы в слварь.
            _Schemes.Add(scheme.SchemeName, scheme);
        }
    }

    /// <summary>
    /// Возвращает схему с указанным именем.
    /// </summary>
    /// <param name="name">
    /// Имя схемы.
    /// </param>
    /// <returns>
    /// Схема с указанным именем.
    /// </returns>
    public CheckingScheme this[string name] => _Schemes[name];
}
