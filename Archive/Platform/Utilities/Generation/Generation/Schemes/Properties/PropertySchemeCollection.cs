using System.Collections;

namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет коллекцию схем свойств.
/// </summary>
public sealed class PropertySchemeCollection :
    IEnumerable<PropertyScheme>
{
    /// <summary>
    /// Поле для хранения списка элементов коллекции.
    /// </summary>
    private readonly List<PropertyScheme> _Items;

    /// <summary>
    /// Поле для хранения карты свойств.
    /// </summary>
    private readonly SortedDictionary<string, PropertyScheme> _Map;

    /// <summary>
    /// Инициализириует новый экземпляр класса.
    /// </summary>
    /// <param name="collection">
    /// Коллекция свойств.
    /// </param>
    internal PropertySchemeCollection(
        [ParameterNoChecks] IEnumerable<PropertyScheme> collection)
    {
        //  Создание списка элементов коллекции.
        _Items = new(collection);

        //  Создание карты свойств.
        _Map = new();

        //  Перебор коллекции свойств.
        foreach (PropertyScheme property in collection)
        {
            //  Добавление свойства в карту.
            _Map.Add(property.PropertyName, property);
        }
    }

    /// <summary>
    /// Возвращает свойство с указанным именем.
    /// </summary>
    /// <param name="propertyName">
    /// Имя свойства.
    /// </param>
    /// <returns>
    /// Свойство с указанным именем.
    /// </returns>
    public PropertyScheme this[[ParameterNoChecks] string propertyName] => _Map[propertyName];

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<PropertyScheme> GetEnumerator()
    {
        //  Возврат перечислителя коллекции значений карты свойств.
        return _Items.GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат универсального перечислителя.
        return GetEnumerator();
    }
}
