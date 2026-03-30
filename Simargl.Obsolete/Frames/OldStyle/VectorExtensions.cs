using Simargl.Algebra;
using System;
using Complex = System.Numerics.Complex;

namespace Simargl.Frames.OldStyle;

/// <summary>
/// Предоставляет методы расширения для класса <see cref="Vector{TItem}"/>,
/// реализующие методы старого стиля работы с кадрами.
/// </summary>
public static class VectorExtensions
{
    /// <summary>
    /// Находит индекс первого элемента, удовлетворяющего условию предиката.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="predicate">
    /// Предикат.
    /// </param>
    /// <returns>
    /// Индекс первого элемента, удовлетворяющего условию предиката
    /// или -1, если элемент не найден.
    /// </returns>
    public static int FindIndex(this Vector<double> vector, Func<double, bool> predicate)
    {
        for (int i = 0; i < vector.Length; i++)
        {
            if (predicate(vector[i]))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="offset"></param>
    public static void Move(this Vector<double> vector, double offset)
    {
        double[] items = vector.Items;

        for (int i = 0; i < items.Length; i++)
        {
            items[i] += offset;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="factor"></param>
    public static void Scale(this Vector<double> vector, double factor)
    {
        double[] items = vector.Items;

        for (int i = 0; i < items.Length; i++)
        {
            items[i] *= factor;
        }
    }

    /// <summary>
    /// Возвращает подвектор.
    /// </summary>
    /// <param name="vector">
    /// Вектор.
    /// </param>
    /// <param name="index">
    /// Индекс, с которого начинается подвектор.
    /// </param>
    /// <param name="length">
    /// Размерность подвектора.
    /// </param>
    /// <returns>
    /// Подвектор.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Происходит в случае, если значение параметра <paramref name="index"/> меньше нуля
    /// - или -
    /// значение параметра <paramref name="length"/> меньше нуля
    /// - или -
    /// сумма значений параметров <paramref name="index"/> и <paramref name="length"/> больше длины исходного вектора.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Происходит в случае, если неуправляемая библиотека выдала исключение.
    /// </exception>
    public static Vector<double> GetSubVector(this Vector<double> vector, int index, int length)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Произошла попытка получить подвектор отрицательного индекса.");
        }
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Произошла попытка получить подвектор отрицательной размерности.");
        }
        if ((ulong)index + (ulong)length > (ulong)vector.Length)
        {
            throw new ArgumentOutOfRangeException("index + length", "Произошла попытка получить подвектор, который не умещается в векторе.");
        }
        Vector<double> subVector = new(length);
        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                subVector[i] = vector[i + index];
            }
        }
        return subVector;
    }


    /// <summary>
    /// Выполняет преобразование Фурье данного вектора.
    /// </summary>
    public static void FourierTransform(this Simargl.Algebra.Vector<Complex> vector)
    {
        int length = vector.Length;
        Complex[] buffer = new Complex[length];
        for (int i = 0; i < length; i++)
        {
            buffer[i] = vector[i];
        }

        Compute.Cpu.Fft.Direct(buffer);

        for (int i = 0; i < length; i++)
        {
            vector[i] = buffer[i];
        }
    }

    /// <summary>
    /// Выполняет обратное преобразование Фурье данного вектора.
    /// </summary>
    public static void InverseFourierTransform(this Vector<Complex> vector)
    {
        int length = vector.Length;
        Complex[] buffer = new Complex[length];
        for (int i = 0; i < length; i++)
        {
            buffer[i] = vector[i];
        }

        Compute.Cpu.Fft.Inverse(buffer);

        for (int i = 0; i < length; i++)
        {
            vector[i] = buffer[i];
        }
    }
}
