namespace Simargl.Recording.AccelEth3T;

/// <summary>
/// Представляет коллекцию синхронных сигналов датчика AccelEth3T.
/// </summary>
public sealed class AccelEth3TSignalCollection :
    IEnumerable<AccelEth3TSignal>
{
    /// <summary>
    /// Поле для хранения элементов коллекции.
    /// </summary>
    private readonly AccelEth3TSignal[] _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal AccelEth3TSignalCollection()
    {
        //  Создание сигналов ускорений.
        XSignal = new();
        YSignal = new();
        ZSignal = new();

        //  Создание хранилища элементов коллекции.
        _Items = [XSignal, YSignal, ZSignal];
    }

    /// <summary>
    /// Возвращает сигнал ускорения по оси Ox.
    /// </summary>
    public AccelEth3TSignal XSignal { get; }

    /// <summary>
    /// Возвращает сигнал ускорения по оси Oy.
    /// </summary>
    public AccelEth3TSignal YSignal { get; }

    /// <summary>
    /// Возвращает сигнал ускорения по оси Oz.
    /// </summary>
    public AccelEth3TSignal ZSignal { get; }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => _Items.Length;

    /// <summary>
    /// Возвращает элемент с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс элемента.
    /// </param>
    /// <returns>
    /// Элемент с указанным индексом.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Count"/>.
    /// </exception>
    public AccelEth3TSignal this[int index] => _Items[IsIndex(index, Count, nameof(index))];

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<AccelEth3TSignal> GetEnumerator()
    {
        //  Возврат перечислителя хранилища элементов.
        return ((IEnumerable<AccelEth3TSignal>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя хранилища элементов.
        return _Items.GetEnumerator();
    }
}
