namespace Apeiron.Platform.Demo.AdxlDemo.Channels;

/// <summary>
/// Представляет статистику канала.
/// </summary>
public readonly struct ChannelStatistics
{
    /// <summary>
    /// Возвращает или инициализирует количество значений.
    /// </summary>
    public int Count { get; init; }

    /// <summary>
    /// Возвращает или инициализирует сумму значений.
    /// </summary>
    public double Sum { get; init; }

    /// <summary>
    /// Возвращает или инициализирует сумму квадратов значений.
    /// </summary>
    public double SumSquares { get; init; }

    /// <summary>
    /// Возвращает или инициализирует минимальное значение.
    /// </summary>
    public double MinValue { get; init; }

    /// <summary>
    /// Возвращает или инициализирует максимальное значение.
    /// </summary>
    public double MaxValue { get; init; }

    /// <summary>
    /// Возвращает или инициализирует среднее значение.
    /// </summary>
    public double Average { get; init; }

    /// <summary>
    /// Возвращает или инициализирует СКО.
    /// </summary>
    public double Deviation { get; init; }

    /// <summary>
    /// Расчитывает статистику для данных.
    /// </summary>
    /// <param name="series">
    /// Данные.
    /// </param>
    /// <returns>
    /// Статистика.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="series"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="series"/> передана пустая коллекция.
    /// </exception>
    public static ChannelStatistics FromData(IEnumerable<double> series)
    {
        //  Проверка ссылки на данные.
        IsNotNull(series, nameof(series));

        //  Инициализация основных значений.
        int count = 0;
        double sum = 0;
        double sumSquares = 0;
        double minValue = double.MaxValue;
        double maxValue = double.MinValue;

        //  Перебор значений.
        foreach (double value in series)
        {
            //  Корректировка значений.
            count++;
            sum += value;
            sumSquares += value * value;
            minValue = Math.Min(minValue, value);
            maxValue = Math.Max(maxValue, value);
        }

        //  Проверка количества значений.
        if (count == 0)
        {
            throw Exceptions.ArgumentEmptyCollection(nameof(series));
        }

        //  Определение среднего значения и СКО.
        double average = sum / count;
        double deviation = count == 1 ? 0 : (sumSquares - count * average * average) / (count - 1);
        if (deviation < 0)
        {
            deviation = 0;
        }
        else
        {
            deviation = Math.Sqrt(deviation);
        }

        //  Возврат результата.
        return new()
        {
            Count = count,
            Sum = sum,
            SumSquares = sumSquares,
            MinValue = minValue,
            MaxValue = maxValue,
            Average = average,
            Deviation = deviation,
        };
    }
}
