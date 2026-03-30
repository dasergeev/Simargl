namespace Simargl.EventsArgs;

/// <summary>
/// Представляет класс события, передающего массив данных
/// </summary>
public class ByteArrayEventArgs :
    EventArgs
{
    /// <summary>
    /// Возвращает данные.
    /// </summary>
    public byte[] Data { get; }

    /// <summary>
    /// Инициализирует объект класса
    /// </summary>
    /// <param name="data">Массив</param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="data"/> передана пустая ссылка.
    /// </exception>
    public ByteArrayEventArgs(byte[] data)
    {
        //  Установка массива.
        Data = IsNotNull(data, nameof(data));
    }
}
