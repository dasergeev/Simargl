using System;
using System.Numerics;

namespace Simargl.Mathematics;

/// <summary>
/// Содержит статические функции, выполняющие поточечные операции над массивами.
/// </summary>
public class Pointwise
{
    /// <summary>
    /// Поточечное абсолютное значение массива.
    /// </summary>
    /// <param name="array">
    /// Исходный массив.
    /// </param>
    /// <returns>
    /// Массив из абсолютных значений исходного массива.
    /// </returns>
    public static double[] Abs(double[] array)
    {
        int n = array.Length;
        double[] absarray = new double[n];

        for (int i = 0; i < n; i++)
        {
            absarray[i] = Math.Abs(array[i]);
        }

        return absarray;
    }

    /// <summary>
    /// Возвращает вектор из абсолютных величин компонент комплексного вектора.
    /// </summary>
    /// <param name="vectorC">
    /// Комплексный вектор.
    /// </param>
    /// <returns>
    /// Вещественный вектор из абсолютных величин.
    /// </returns>
    public static double[] Magnitudes(Complex[] vectorC)
    {
        int lnt = vectorC.Length;

        double[] vectorR = new double[lnt];

        for (int i = 0; i < lnt; i++)
        {
            vectorR[i] = vectorC[i].Magnitude;
        }
        return vectorR;
    }

    /// <summary>
    /// Возвращает вектор из квадратов абсолютных величин компонент комплексного вектора.
    /// </summary>
    /// <param name="vectorC">
    /// Комплексный вектор.
    /// </param>
    /// <returns>
    /// Вещественный вектор из квадратов абсолютных величин.
    /// </returns>
    public static double[] Magnitudes2(Complex[] vectorC)
    {
        int lnt = vectorC.Length;
        double m;

        double[] vectorR = new double[lnt];

        for (int i = 0; i < lnt; i++)
        {
            m = vectorC[i].Magnitude;
            vectorR[i] = m * m;
        }
        return vectorR;
    }

    /// <summary>
    /// Покомпонентная сумма одномерных вещественных массивов.
    /// </summary>
    /// <param name="arrayLeft">
    /// Массив вещественных чисел.
    /// </param>
    /// <param name="arrayRight">
    /// Массив вещественных чисел.
    /// </param>
    /// <returns>
    /// Массив из сумм соответствующих элементов исходного массива.
    /// </returns>
    public static double[] Sum(double[] arrayLeft, double[] arrayRight)
    {
        int nl = arrayLeft.Length;
        int nr = arrayRight.Length;
        int n = Math.Min(nl, nr);

        double[] arraySum = new double[n];

        if (n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                arraySum[i] = arrayLeft[i] + arrayRight[i];
            }
        }

        return arraySum;
    }

    /// <summary>
    /// Покомпонентная разность одномерных вещественных массивов.
    /// </summary>
    /// <param name="arrayLeft">
    /// Массив вещественных чисел.
    /// </param>
    /// <param name="arrayRight">
    /// Массив вещественных чисел.
    /// </param>
    /// <returns>
    /// Массив из разностей соответствующих элементов исходного массива.
    /// </returns>
    public static double[] Difference(double[] arrayLeft, double[] arrayRight)
    {
        int nl = arrayLeft.Length;
        int nr = arrayRight.Length;
        int n = Math.Min(nl, nr);

        double[] arrayDiff = new double[n];

        if (n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                arrayDiff[i] = arrayLeft[i] - arrayRight[i];
            }
        }

        return arrayDiff;
    }
    /// <summary>
    /// Прибавление ко всем элементам массива одного и того же числа.
    /// </summary>
    /// <param name="array">
    /// Массив вещественных чисел.
    /// </param>
    /// <param name="scalar">
    /// Вещественное число.
    /// </param>
    /// <returns>
    /// Массив sumArray: sumArray[i] = array[i] + scalar.
    /// </returns>
    public static double[] ArrayScalarSum(double[] array, double scalar)
    {
        int n = array.Length;
        double[] sumArray = new double[n];

        if (n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                sumArray[i] = array[i] + scalar;
            }
        }

        return sumArray;
    }

    /// <summary>
    /// Покомпонентное произведение одномерных вещественных массивов.
    /// </summary>
    /// <param name="arrayLeft">
    /// Массив вещественных чисел.
    /// </param>
    /// <param name="arrayRight">
    /// Массив вещественных чисел.
    /// </param>
    /// <returns>
    /// Массив из произведений соответствующих элементов исходного массива.
    /// </returns>
    public static double[] Product(double[] arrayLeft, double[] arrayRight)
    {
        int nl = arrayLeft.Length;
        int nr = arrayRight.Length;
        int n = Math.Min(nl, nr);

        double[] arrayProduct = new double[n];

        if (n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                arrayProduct[i] = arrayLeft[i] * arrayRight[i];
            }
        }

        return arrayProduct;
    }

    /// <summary>
    /// Покомпонентное деление одного массива на другой.
    /// </summary>
    /// <param name="numerator">
    /// Массив числителей.
    /// </param>
    /// <param name="denominator">
    /// Массив знаменателей.
    /// </param>
    /// <returns>
    /// Массив частных.
    /// </returns>
    public static double[] Division(double[] numerator, double[] denominator)
    {
        int n1 = numerator.Length;
        int n2 = denominator.Length;
        int n = Math.Min(n1, n2);

        double[] quotient = new double[n]; // Возвращаемое значение.
        double denCurrent;

        if (n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                denCurrent = denominator[i];

                if (denCurrent > Double.Epsilon)
                {
                    quotient[i] = numerator[i] / denCurrent;
                }
                else
                {
                    quotient[i] = double.NaN;
                }                
            }
        }

        return quotient;
    }

    /// <summary>
    /// Покомпонентное деление числа на массив.
    /// </summary>
    /// <param name="numerator">
    /// Числитель.
    /// </param>
    /// <param name="denominator">
    /// Массив знаменателей.
    /// </param>
    /// <returns>
    /// Массив частных.
    /// </returns>
    public static double[] Division(double numerator, double[] denominator)
    {
        int n = denominator.Length;

        double[] quotient = new double[n]; // Возвращаемое значение.
        double denCurrent;

        if (n > 0)
        {
            for (int i = 0; i < n; i++)
            {
                denCurrent = denominator[i];

                if (denCurrent > Double.Epsilon)
                {
                    quotient[i] = numerator / denCurrent;
                }
                else
                {
                    quotient[i] = double.NaN;
                }
            }
        }

        return quotient;
    }
}
