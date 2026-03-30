namespace Simargl.Border.Processing.Core;

/// <summary>
/// Представляет коллекцию нажимов.
/// </summary>
public sealed class PressureCollection :
    IEnumerable<Pressure>
{
    /// <summary>
    /// Поле для хранения списка элементов.
    /// </summary>
    private readonly List<Pressure> _Items = [];

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => _Items.Count;

    /// <summary>
    /// Возвращает элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns>
    /// Элемент с указанным индексом.
    /// </returns>
    public Pressure this[int index] => _Items[index];

    /// <summary>
    /// Добавляет элемент в коллекицю.
    /// </summary>
    /// <param name="item">
    /// Элемент, добавляемый в коллекцию.
    /// </param>
    public void Add(Pressure item) => _Items.Add(item);

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Pressure> GetEnumerator()
    {
        return ((IEnumerable<Pressure>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_Items).GetEnumerator();
    }
}
