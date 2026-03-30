using Simargl.Designing;
using System;

namespace Simargl.Analysis.Schematization;

/// <summary>
/// Представляет единицу гистограммы в виде пары.
/// </summary>
public sealed class HistogramUnit
{
    /// <summary>
    /// Поле для хранения количества амплитуд.
    /// </summary>
    private int _Count;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="amplitude">
    /// Амплитуда.
    /// </param>
    /// <param name="count">
    /// Количество амплитуд.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="amplitude"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="amplitude"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    public HistogramUnit (double amplitude, int count)
    {
        //  Проверка амплитуды на равенство бесконечности.
        IsNotInfinity(amplitude, nameof(amplitude));

        //  Проверка амплитуды на нечисловое значение.
        IsNotNaN(amplitude, nameof(amplitude));

        //  Установака амплитуды.
        Amplitude = amplitude;

        //  Проверка и установка количества амплитуд.
        _Count = IsNotNegative(count, nameof(count));
    }

    /// <summary>
    /// Возвращает или задаёт амплитуду.
    /// </summary>
    public double Amplitude { get; }

    /// <summary>
    /// Возвращает или задаёт количество амплитуд.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано отрицательное значение.
    /// </exception>
    public int Count
    {
        get => _Count;
        set => _Count = IsNotNegative(value, nameof(Count));
    }
}
