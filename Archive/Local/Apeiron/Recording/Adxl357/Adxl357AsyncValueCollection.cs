namespace Apeiron.Recording.Adxl357;

/// <summary>
/// Представляет коллекцию асинхронных значений датчика ADXL357.
/// </summary>
public sealed class Adxl357AsyncValueCollection :
    IEnumerable<float>
{
    /// <summary>
    /// Постоянная, определяющая максимальное число элементов в коллекции.
    /// </summary>
    private const int _MaxCount = byte.MaxValue;

    /// <summary>
    /// Поле для хранения элементов коллекции.
    /// </summary>
    private readonly List<float> _Items;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal Adxl357AsyncValueCollection()
    {
        //  Создание хранилища элементов.
        _Items = new();
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции.
    /// </summary>
    public int Count => _Items.Count;

    /// <summary>
    /// Возвращает или задаёт значение с указанным индексом.
    /// </summary>
    /// <param name="index">
    /// Индекс значения.
    /// </param>
    /// <returns>
    /// Значение с указанным индексом.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Count"/>.
    /// </exception>
    public float this[int index]
    {
        get => _Items[IsIndex(index, Count, nameof(index))];
        set => _Items[IsIndex(index, Count, nameof(index))] = value;
    }

    /// <summary>
    /// Очищает коллекцию.
    /// </summary>
    public void Clear() => _Items.Clear();

    /// <summary>
    /// Добавляет асинхронное значение.
    /// </summary>
    /// <param name="value">
    /// Добавляемое асинхронное значений.
    /// </param>
    /// <exception cref="OverflowException">
    /// Операция привела к переполнению.
    /// </exception>
    public void Add(float value)
    {
        //  Проверка размера.
        if (Count == _MaxCount)
        {
            //  Операция привела к переполнению.
            throw Exceptions.OperationOverflow();
        }

        //  Добавление элемента в коллекцию.
        _Items.Add(value);
    }

    /// <summary>
    /// Добавляет коллекцию элементов.
    /// </summary>
    /// <param name="collection">
    /// Коллекция элементов, которую необходимо добавить.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="collection"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OverflowException">
    /// Операция привела к переполнению.
    /// </exception>
    public void AddRange(IEnumerable<float> collection)
    {
        //  Проверка ссылки на коллекцию.
        collection = IsNotNull(collection, nameof(collection));

        //  Получение массива значений.
        float[] values = collection as float[] ?? collection.ToArray();

        //  Проверка размера.
        if (Count + values.Length > _MaxCount)
        {
            //  Операция привела к переполнению.
            throw Exceptions.OperationOverflow();
        }

        //  Добавление элементов в коллекцию.
        _Items.AddRange(values);
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<float> GetEnumerator()
    {
        //  Возврат перечислителя хранилища элементов.
        return ((IEnumerable<float>)_Items).GetEnumerator();
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
        return ((IEnumerable)_Items).GetEnumerator();
    }
}
