namespace Simargl.Border.Processing;

/// <summary>
/// Представляет коллекцию осей.
/// </summary>
public sealed class AxisCollection :
    IEnumerable<Axis>
{
    /// <summary>
    /// Поле для хранения элементов.
    /// </summary>
    private readonly List<Axis> _Items = [];

    /// <summary>
    /// Добавляет новый элемент в коллекцию.
    /// </summary>
    /// <param name="item">
    /// Элемент, добавляемый в коллекцию.
    /// </param>
    public void Add(Axis item) => _Items.Add(item);

    /// <summary>
    /// Выполняет построение.
    /// </summary>
    public void Build()
    {
        //  Перебор осей.
        foreach (var item in _Items)
        {
            //  Построение.
            item.Build();
        }
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Axis> GetEnumerator()
    {
        return ((IEnumerable<Axis>)_Items).GetEnumerator();
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
