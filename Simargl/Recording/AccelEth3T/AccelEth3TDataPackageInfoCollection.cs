namespace Simargl.Recording.AccelEth3T;

/// <summary>
/// Представляет коллекцию информации о пакетах данных датчика AccelEth3T.
/// </summary>
public sealed class AccelEth3TDataPackageInfoCollection :
    IEnumerable<AccelEth3TDataPackageInfo>
{
    /// <summary>
    /// Поле для хранения массива элементов.
    /// </summary>
    private readonly AccelEth3TDataPackageInfo[] _Items;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="items">
    /// Массив элементов.
    /// </param>
    internal AccelEth3TDataPackageInfoCollection(AccelEth3TDataPackageInfo[] items)
    {
        //  Установка массива элементов.
        _Items = IsNotNull(items);
    }

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
    public AccelEth3TDataPackageInfo this[int index] => _Items[IsIndex(index, Count, nameof(index))];

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<AccelEth3TDataPackageInfo> GetEnumerator()
    {
        //  Возврат перечислителя массива элементов.
        return ((IEnumerable<AccelEth3TDataPackageInfo>)_Items).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        //  Возврат перечислителя массива элементов.
        return _Items.GetEnumerator();
    }
}
