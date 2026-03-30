namespace Apeiron.Oriole.Server.Performance;

/// <summary>
/// Предоставляет методы расширения для индикаторов.
/// </summary>
public static class IndicatorExtensions
{
    /// <summary>
    /// Возвращает форматированное представление целого числа.
    /// </summary>
    /// <param name="value">
    /// Значение.
    /// </param>
    /// <returns>
    /// Представление.
    /// </returns>
    private static string ToFormatString(int value)
    {
        if (value < 1000)
        {
            //  Возврат текстового представления.
            return $"{value:##0}";
        }
        else if (value < 1000000)
        {
            //  Возврат текстового представления.
            return $"{value:### ##0}";
        }
        else if (value < 1000000000)
        {
            //  Возврат текстового представления.
            return $"{value:### ### ##0}";
        }
        else
        {
            //  Возврат текстового представления.
            return $"{value:### ### ### ##0}";
        }
    }

    /// <summary>
    /// Возвращает информацию об индикаторе, который должен быть стабильным.
    /// </summary>
    /// <param name="indicator">
    /// Индикатор.
    /// </param>
    /// <returns>
    /// Текстовая информация.
    /// </returns>
    public static string Stable(this Indicator<int> indicator)
    {
        //  Определение изменения значения.
        int delta = indicator.Current - indicator.Previous;

        //  Текстовое представление изменения.
        string deltaText = string.Empty;

        //  Проверка уменьшения значения.
        if (delta < 0)
        {
            //  Изменение текстового представления.
            deltaText = $"(\x1b[31m-{ToFormatString(-delta)}\x1b[37m)";
        }

        //  Проверка увеличения значения.
        if (delta > 0)
        {
            //  Изменение текстового представления.
            deltaText = $"(\x1b[32m+{ToFormatString(delta)}\x1b[37m)";
        }

        //  Возврат текстового представления.
        return $"{ToFormatString(indicator.Current)} {deltaText}";
    }

    /// <summary>
    /// Возвращает информацию об индикаторе, который должен увеличиваться.
    /// </summary>
    /// <param name="indicator">
    /// Индикатор.
    /// </param>
    /// <param name="limit">
    /// Индикатор предельного состояния.
    /// </param>
    /// <param name="interval">
    /// Интервал, за который изменился индикатор.
    /// </param>
    /// <returns>
    /// Текстовая информация.
    /// </returns>
    public static string Process(this Indicator<int> indicator, Indicator<int> limit, TimeSpan interval)
    {
        //  Процент выполнения.
        double percent = 100;

        //  Проверка предельного значения.
        if (limit.Current != 0)
        {
            percent = indicator.Current * 100.0 / limit.Current;
        }

        //  Определение изменения значения.
        int delta = indicator.Current - indicator.Previous;

        //  Текстовое представление изменения.
        string deltaText = string.Empty;

        //  Проверка достижения предельного значения.
        if (indicator.Current != limit.Current)
        {
            //  Проверка изменения значения.
            if (delta > 0)
            {
                //  Изменение текстового представления.
                deltaText = $"({(limit.Current - indicator.Current) * interval / delta})";
            }
            else
            {
                //  Изменение текстового представления.
                deltaText = $"(\x1b[31mбесконечно\x1b[37m)";
            }
        }

        //  Возврат текстового представления.
        return $"{indicator.Stable()} {$"{percent:0.###}".TrimEnd(',', '.')}% {deltaText}";
    }
}
