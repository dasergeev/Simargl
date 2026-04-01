using Simargl.Designing;
using System;

namespace Simargl.Analysis.Transforms;

/// <summary>
/// Представляет Sinc-фильтр.
/// </summary>
public sealed class SincFilter :
    LowPassFilter
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="cutoff">
    /// Частота среза фильтра.
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
    public SincFilter(double cutoff) :
        base(cutoff)
    {

    }

    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="source">
    /// Объект для преобразования.
    /// </param>
    internal protected override sealed void InvokeCore([NoVerify] Spectrum source)
    {
        //  Определение начального индекса.
        int beginIndex = (int)Math.Floor(Cutoff / source.FrequencyStep);

        //  Цикл по амплитудам.
        for (int i = beginIndex; i < source.Count; i++)
        {
            //  Обнуление частоты.
            source[i] = 0;
        }
    }
}
