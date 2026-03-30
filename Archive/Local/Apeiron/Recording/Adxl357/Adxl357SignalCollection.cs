namespace Apeiron.Recording.Adxl357;

/// <summary>
/// Представляет коллекцию синхронных сигналов датчика ADXL357.
/// </summary>
public sealed class Adxl357SignalCollection :
    IEnumerable<Adxl357Signal>
{
    /// <summary>
    /// Поле для хранения элементов коллекции.
    /// </summary>
    private readonly Adxl357Signal[] _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal Adxl357SignalCollection()
    {
        //  Создание сигналов ускорений.
        XSignal = new();
        YSignal = new();
        ZSignal = new();

        //  Создание хранилища элементов коллекции.
        _Items = new Adxl357Signal[] { XSignal, YSignal, ZSignal };
    }

    /// <summary>
    /// Возвращает сигнал ускорения по оси Ox.
    /// </summary>
    public Adxl357Signal XSignal { get; }

    /// <summary>
    /// Возвращает сигнал ускорения по оси Oy.
    /// </summary>
    public Adxl357Signal YSignal { get; }

    /// <summary>
    /// Возвращает сигнал ускорения по оси Oz.
    /// </summary>
    public Adxl357Signal ZSignal { get; }

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
    public Adxl357Signal this[int index] => _Items[IsIndex(index, Count, nameof(index))];

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<Adxl357Signal> GetEnumerator()
    {
        //  Возврат перечислителя хранилища элементов.
        return ((IEnumerable<Adxl357Signal>)_Items).GetEnumerator();
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
