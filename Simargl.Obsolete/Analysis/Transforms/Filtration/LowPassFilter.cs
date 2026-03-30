using Simargl.Designing;
using System;
using System.Runtime.CompilerServices;

namespace Simargl.Analysis.Transforms;

/// <summary>
/// Представляет фильтр нижних частот.
/// </summary>
public abstract class LowPassFilter :
    SpectralTransform
{
    /// <summary>
    /// Поле для хранения частоты среза фильтра.
    /// </summary>
    private double _Cutoff;

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
    public LowPassFilter(double cutoff)
    {
        //  Установка частоты среза фильтра.
        _Cutoff = IsCutoff(cutoff, nameof(cutoff));
    }

    /// <summary>
    /// Возвращает или задаёт частоту среза фильтра.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано отрицательное значение.
    /// </exception>
    public double Cutoff
    {
        get => _Cutoff;
        set => _Cutoff = IsCutoff(value, nameof(Cutoff));
    }

    /// <summary>
    /// Выполняет проверку частоты среза фильтра.
    /// </summary>
    /// <param name="value">
    /// Значение, которое необходимо проверить.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенное значение.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передано отрицательное значение.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double IsCutoff(double value, string? paramName)
    {
        //  Проверка нечислового значения.
        IsNotNaN(value, paramName);

        //  Проверка бесконечного значения.
        IsNotInfinity(value, paramName);

        //  Проверка отрицательного значения.
        IsNotNegative(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }
}
