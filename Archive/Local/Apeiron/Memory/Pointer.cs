namespace Apeiron.Memory;

/// <summary>
/// Представляет объект, имитирующий работу указателя Си.
/// </summary>
/// <typeparam name="T">
/// Тип данных.
/// </typeparam>
internal sealed class Pointer<T>
{
    /// <summary>
    /// Поле для хранения массива данных.
    /// </summary>
    private T[] _Data;

    /// <summary>
    /// Поле для хранения смещения в массиве данных.
    /// </summary>
    private int _Offset;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="data">
    /// Массив данных.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="data"/> передана пустая ссылка.
    /// </exception>
    public Pointer(T[] data)
    {
        //  Установка массива данных.
        _Data = IsNotNull(data, nameof(data));

        //  Установка смещения.
        _Offset = 0;
    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="data">
    /// Массив данных.
    /// </param>
    /// <param name="offset">
    /// Смещение в массиве данных.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="data"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="offset"/> передано значение
    /// большее или равное длине массива <paramref name="data"/>.
    /// </exception>
    public Pointer(T[] data, int offset)
    {
        //  Установка массива данных.
        _Data = IsNotNull(data, nameof(data));

        //  Установка смещения.
        _Offset = IsIndex(offset, data.Length, nameof(offset));
    }

    /// <summary>
    /// Возвращает или задаёт значение по указанному индексу.
    /// </summary>
    /// <param name="index">
    /// Индекс значения.
    /// </param>
    /// <returns>
    /// Значение по указанному индексу.
    /// </returns>
    public T this[int index]
    {
        get => _Data[IsIndex(index, _Data.Length - _Offset, nameof(index))];
        set => _Data[IsIndex(index, _Data.Length - _Offset, nameof(index))] = value;
    }
}
