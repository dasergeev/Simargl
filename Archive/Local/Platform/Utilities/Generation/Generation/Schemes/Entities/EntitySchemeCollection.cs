using System.Collections;

namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет коллекцию схем сущностей.
/// </summary>
public sealed class EntitySchemeCollection :
    IEnumerable<EntityScheme>
{
    /// <summary>
    /// Поле для хранения карты сущностей.
    /// </summary>
    private readonly SortedDictionary<string, EntityScheme> _Map;

    /// <summary>
    /// Инициализириует новый экземпляр класса.
    /// </summary>
    /// <param name="collection">
    /// Коллекция сущностей.
    /// </param>
    internal EntitySchemeCollection(
        [ParameterNoChecks] IEnumerable<EntityScheme> collection)
    {
        //  Создание карты сущностей.
        _Map = new();

        //  Перебор коллекции сущностей.
        foreach (EntityScheme entity in collection)
        {
            //  Добавление сущности в карту.
            _Map.Add(entity.TypeName, entity);
        }
    }

    /// <summary>
    /// Возвращает сущность с указанным именем типа.
    /// </summary>
    /// <param name="typeName">
    /// Имя свойства.
    /// </param>
    /// <returns>
    /// Сущность с указанным именем типа.
    /// </returns>
    public EntityScheme this[[ParameterNoChecks] string typeName] => _Map[typeName];

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<EntityScheme> GetEnumerator()
    {
        //  Возврат перечислителя коллекции значений карты сущностей.
        return _Map.Values.GetEnumerator();
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