namespace Apeiron.Memory;

/// <summary>
/// Предоставляет методы расширения для циклического буфера <see cref="CircularBuffer{T}"/>.
/// </summary>
public static class CircularBufferExtensions
{
    /// <summary>
    /// Возвращает поток для чтения и записи в буфер.
    /// </summary>
    /// <param name="buffer">
    /// Буфер.
    /// </param>
    /// <returns>
    /// Поток для чтения и записи в буфер.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="buffer"/> передана пустая ссылка.
    /// </exception>
    public static CircularBufferStream GetStream(this CircularBuffer<byte> buffer)
    {
        //  Создание потока.
        return new CircularBufferStream(buffer);
    }
}
