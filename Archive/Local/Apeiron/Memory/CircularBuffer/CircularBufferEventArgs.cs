namespace Apeiron.Memory;

/// <summary>
/// Представляет обработчик события циклического буфера.
/// </summary>
/// <param name="sender">
/// Объект, создавший событие.
/// </param>
/// <param name="e">
/// Аргументы, связанные с событием.
/// </param>
public delegate void CircularBufferEventHandler(object sender, CircularBufferEventArgs e);

/// <summary>
/// Педставляет данные события циклического буфера.
/// </summary>
/// <seealso cref="EventArgs"/>
public sealed class CircularBufferEventArgs :
    EventArgs
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="beginPosition">
    /// Начальная позиция в буфере.
    /// </param>
    /// <param name="endPosition">
    /// Следующая за последней позиция в буфере.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="beginPosition"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="endPosition"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="beginPosition"/> передано значение,
    /// которое превышает значение <paramref name="endPosition"/>.
    /// </exception>
    public CircularBufferEventArgs(long beginPosition, long endPosition)
    {
        //  Проверка начальной позиции.
        IsNotNegative(beginPosition, nameof(beginPosition));

        //  Проверка следующей за последней позицией.
        IsNotNegative(endPosition, nameof(endPosition));

        //  Проверка превышения начальной позиции.
        IsNotLarger(beginPosition, endPosition, nameof(beginPosition));

        //  Установка начальной позиции в буфере.
        BeginPosition = beginPosition;

        //  Установка следующей за последней позиции в буфере.
        EndPosition = endPosition;
    }

    /// <summary>
    /// Возвращает начальную позицию в буфере.
    /// </summary>
    public long BeginPosition { get; }

    /// <summary>
    /// Возвращает следующую за последней позицию в буфере.
    /// </summary>
    public long EndPosition { get; }
}
