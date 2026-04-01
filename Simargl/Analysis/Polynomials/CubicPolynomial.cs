using Simargl.Designing;
using System;
using System.Runtime.CompilerServices;

namespace Simargl.Analysis.Polynomials;

/// <summary>
/// Представляет кубический полином.
/// </summary>
public readonly struct CubicPolynomial
{
    /// <summary>
    /// Поле для хранения нулевого коэффициента.
    /// </summary>
    private readonly double _ZeroFactor;

    /// <summary>
    /// Поле для хранения первого коэффициента.
    /// </summary>
    private readonly double _FirstFactor;

    /// <summary>
    /// Поле для хранения второго коэффициента.
    /// </summary>
    private readonly double _SecondFactor;

    /// <summary>
    /// Поле для хранения третьего коэффициента.
    /// </summary>
    private readonly double _ThirdFactor;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="zeroFactor">
    /// Нулевой коэффициент.
    /// </param>
    /// <param name="firstFactor">
    /// Первый коэффициент.
    /// </param>
    /// <param name="secondFactor">
    /// Второй коэффициент.
    /// </param>
    /// <param name="thirdFactor">
    /// Третий коэффициент.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="zeroFactor"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="zeroFactor"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="firstFactor"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="firstFactor"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="secondFactor"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="secondFactor"/> передано бесконечное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="thirdFactor"/> передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="thirdFactor"/> передано бесконечное значение.
    /// </exception>
    public CubicPolynomial(double zeroFactor, double firstFactor, double secondFactor, double thirdFactor)
    {
        //  Установка коэффициентов.
        _ZeroFactor = IsFactor(zeroFactor, nameof(zeroFactor));
        _FirstFactor = IsFactor(firstFactor, nameof(firstFactor));
        _SecondFactor = IsFactor(secondFactor, nameof(secondFactor));
        _ThirdFactor = IsFactor(thirdFactor, nameof(thirdFactor));
    }

    /// <summary>
    /// Возвращает или инициализирует нулевой коэффициент.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    public readonly double ZeroFactor
    {
        get => _ZeroFactor;
        init => _ZeroFactor = IsFactor(value, nameof(ZeroFactor));
    }

    /// <summary>
    /// Возвращает или инициализирует первый коэффициент.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    public readonly double FirstFactor
    {
        get => _FirstFactor;
        init => _FirstFactor = IsFactor(value, nameof(FirstFactor));
    }

    /// <summary>
    /// Возвращает или инициализирует второй коэффициент.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    public readonly double SecondFactor
    {
        get => _SecondFactor;
        init => _SecondFactor = IsFactor(value, nameof(SecondFactor));
    }

    /// <summary>
    /// Возвращает или инициализирует третий коэффициент.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нечисловое значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано бесконечное значение.
    /// </exception>
    public readonly double ThirdFactor
    {
        get => _ThirdFactor;
        init => _ThirdFactor = IsFactor(value, nameof(ThirdFactor));
    }

    /// <summary>
    /// Вычисляет значение.
    /// </summary>
    /// <param name="argument">
    /// Аргумент.
    /// </param>
    /// <returns>
    /// Результат вычисления.
    /// </returns>
    public double Calculate(double argument)
    {
        //  Вычисление значения.
        return _ZeroFactor + (_FirstFactor + (_SecondFactor + _ThirdFactor * argument) * argument) * argument;
    }

    /// <summary>
    /// Выполняет проверку коэффициента.
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double IsFactor(double value, string? paramName)
    {
        //  Проверка нечислового значения.
        IsNotNaN(value, paramName);

        //  Проверка бесконечного значения.
        IsNotInfinity(value, paramName);

        //  Возврат проверенного значения.
        return value;
    }
}
