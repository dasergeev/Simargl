using Simargl.Designing;
using System;

namespace Simargl.Analysis.Transforms;

/// <summary>
/// Представляет фильтр Баттерворта.
/// </summary>
public sealed class ButterworthFilter :
    LowPassFilter
{
    /// <summary>
    /// Поле для хранения порядка фильтра.
    /// </summary>
    private int _Order;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="cutoff">
    /// Частота среза фильтра.
    /// </param>
    /// <param name="order">
    /// Порядок фильтра.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="cutoff"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="cutoff"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="cutoff"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="order"/> передано отрицательное значение.
    /// </exception>
    public ButterworthFilter(double cutoff, int order) :
        base(cutoff)
    {
        //  Установка порядка фильтра.
        _Order = IsNotNegative(order, nameof(order));
    }

    /// <summary>
    /// Возвращает или задаёт порядок фильтра.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано отрицательное значение.
    /// </exception>
    public int Order
    {
        get => _Order;
        set => _Order = IsNotNegative(value, nameof(Order));
    }

    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="source">
    /// Объект для преобразования.
    /// </param>
    internal protected override sealed void InvokeCore([NoVerify] Spectrum source)
    {
        //  Проверка порядка фильтра.
        if (Order == 0)
        {
            //  Фильтрация не требуется.
            return;
        }

        //  Шаг коэффициента частоты.
        double frequencyFactorStep = source.FrequencyStep / Cutoff;

        //  Коэффициент частоты.
        double frequencyFactor = frequencyFactorStep;

        //  Степень, соответствующая порядку фильтра.
        double power = 2 * Order;

        //  Цикл по амплитудам.
        for (int i = 1; i < source.Count - 1; i++)
        {
            //  Определение коэффициента усиления.
            double factor = Math.Sqrt(1 / (1 + Math.Pow(frequencyFactor, power)));

            //  Корректировка амплитуды.
            source[i] *= factor;

            //  Корректировка текущей частоты.
            frequencyFactor += frequencyFactorStep;
        }

        //  Корректировка последней амплитуды.
        source[^1] = 0;
    }
}
