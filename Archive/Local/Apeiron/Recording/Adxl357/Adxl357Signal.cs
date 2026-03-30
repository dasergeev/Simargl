namespace Apeiron.Recording.Adxl357;

/// <summary>
/// Представляет синхронный сигнал датчика ADXL357.
/// </summary>
public sealed class Adxl357Signal :
    IEnumerable<float>
{
    /// <summary>
    /// Поле для хранения длины сигнала.
    /// </summary>
    private int _Length;

    /// <summary>
    /// Поле для хранения массива значений сигнала.
    /// </summary>
    private float[] _Data;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal Adxl357Signal()
    {
        //  Установка длины сигнала.
        _Length = 0;

        //  Инициализация массива значений сигнала.
        _Data = Array.Empty<float>();
    }

    /// <summary>
    /// Возвращает длину сигнала.
    /// </summary>
    public int Length => _Length;

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
    /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Length"/>.
    /// </exception>
    public float this[int index]
    {
        get => _Data[IsIndex(index, Length, nameof(index))];
        set => _Data[IsIndex(index, Length, nameof(index))] = value;
    }

    /// <summary>
    /// Устанавливает данные сигнала.
    /// </summary>
    /// <param name="data">
    /// Данные сигнала.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="data"/> передана пустая ссылка.
    /// </exception>
    internal void SetData(float[] data)
    {
        //  Установка массива значений сигнала.
        _Data = IsNotNull(data, nameof(data));

        //  Установа длины сигнала.
        _Length = data.Length;
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    public IEnumerator<float> GetEnumerator()
    {
        return ((IEnumerable<float>)_Data).GetEnumerator();
    }

    /// <summary>
    /// Возвращает перечислитель коллекции.
    /// </summary>
    /// <returns>
    /// Перечислитель коллекции.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return _Data.GetEnumerator();
    }
}
