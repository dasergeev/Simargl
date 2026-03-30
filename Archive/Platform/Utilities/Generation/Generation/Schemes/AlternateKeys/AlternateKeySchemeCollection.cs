using System.Collections;

namespace Apeiron.Platform.Utilities.Generation.Schemes;

/// <summary>
/// Представляет коллекцию схем альтернативных ключей.
/// </summary>
public sealed class AlternateKeySchemeCollection :
    IEnumerable<AlternateKeyScheme>
{
    /// <summary>
    /// Поле для хранения списка элементов коллекции.
    /// </summary>
    private readonly List<AlternateKeyScheme> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="collection">
    /// Коллекция элементов.
    /// </param>
    public AlternateKeySchemeCollection([ParameterNoChecks] IEnumerable<AlternateKeyScheme> collection)
    {
        //  Создание списка элементов коллекции.
        _Items = new(collection);
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<AlternateKeyScheme> GetEnumerator()
    {
        //  Возврат перечислителя списка элементов.
        return ((IEnumerable<AlternateKeyScheme>)_Items).GetEnumerator();
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
