namespace Apeiron.Oriole.Server.Performance;

/// <summary>
/// Представляет индикатор.
/// </summary>
/// <typeparam name="T">
/// Тип значения индикатора.
/// </typeparam>
public readonly struct Indicator<T>
    where T : struct
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="current">
    /// Текущее значение.
    /// </param>
    /// <param name="previous">
    /// Предыдущее значение.
    /// </param>
    public Indicator(T current, Indicator<T> previous)
    {
        //  Устанавливает текущее значение.
        Current = current;

        //  Устанавливает предыдущее значение.
        Previous = previous.Current;
    }

    /// <summary>
    /// Возвращает текущее значение.
    /// </summary>
    public readonly T Current { get; }

    /// <summary>
    /// Возвращает предыдущее значение.
    /// </summary>
    public readonly T Previous { get; }
}
