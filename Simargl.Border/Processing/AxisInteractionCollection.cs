
namespace Simargl.Border.Processing;

/// <summary>
/// Представляет коллекцию взаимодействий оси.
/// </summary>
public sealed class AxisInteractionCollection :
    IEnumerable<AxisInteraction?>
{
    /// <summary>
    /// Поле для хранения элементов.
    /// </summary>
    private readonly AxisInteraction?[] _Items = new AxisInteraction?[BasisConstants.SectionCount];

    /// <summary>
    /// Возвращает или задаёт значение.
    /// </summary>
    public AxisInteraction? this[int section]
    {
        get => _Items[section - 1];
        set => _Items[section - 1] = value;
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<AxisInteraction?> GetEnumerator()
    {
        return ((IEnumerable<AxisInteraction?>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _Items.GetEnumerator();
    }
}
