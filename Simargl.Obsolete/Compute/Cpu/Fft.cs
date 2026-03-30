using Simargl.Designing;
using System;
using System.Numerics;

namespace Simargl.Compute.Cpu;

/// <summary>
/// Предоставляет методы, реализующие алгоритм быстрого преобразования Фурье.
/// </summary>
public static class Fft
{
    /// <summary>
    /// Выполняет прямое преобразование и результат помещает в исходный массив.
    /// </summary>
    /// <param name="sequence">
    /// Массив для преобразования.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sequence"/> передана пустая ссылка.
    /// </exception>
    public static void Direct(Complex[] sequence)
    {
        //  Проверка ссылки на массив.
        IsNotNull(sequence, nameof(sequence));

		//	Выполнение преобразования.
		Transform(sequence, false);
    }

    /// <summary>
    /// Выполняет обратное преобразование и результат помещает в исходный массив.
    /// </summary>
    /// <param name="sequence">
    /// Массив для преобразования.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="sequence"/> передана пустая ссылка.
    /// </exception>
    public static void Inverse(Complex[] sequence)
    {
        //  Проверка ссылки на массив.
        IsNotNull(sequence, nameof(sequence));

		//  Выполнение преобразования.
		Transform(sequence, true);

        //  Определение длины массива.
        var length = sequence.Length;

		//	Нормировочный множитель.
		var factor = 1.0 / length;

        //	Нормирование.
        for (int i = 0; i < length; i++)
        {
			//	Масштабирование.
			sequence[i] *= factor;
		}
	}

    /// <summary>
    /// Вычисляет дискретное преобразование Фурье (ДПФ)
    /// или обратное преобразование заданного комплексного массива,
    /// сохраняя результат обратно в массив.
    /// </summary>
    /// <param name="sequence">
    /// Массив для преобразования.
    /// </param>
    /// <param name="inverse">
    /// Значение, определяющее нужно ли выполнить обратное преобразование.
    /// </param>
    private static void Transform(Complex[] sequence, bool inverse)
	{
		//	Определение длины последовательности.
		int length = sequence.Length;

		//	Проверка длины.
		if (length == 0)
        {
			//	Выполнять преобразование не требуется.
			return;
		}				
		else if ((length & (length - 1)) == 0)
        {
			// Длина последовательности является степенью двух.
			TransformRadix2(sequence, inverse);
		}
		else
        {
			// Выполнение алгоритма для произвольного размера.
			TransformBluestein(sequence, inverse);
		}
	}

	private static void TransformRadix2(Complex[] sequence, bool inverse)
	{
		int n = sequence.Length;

		int levels = 0;  // compute levels = floor(log2(n))
		for (int temp = n; temp > 1; temp >>= 1)
		{
            levels++;
        }

		if (1 << levels != n)
		{
            throw new ArgumentException("Длина не является степенью 2.");
        }

		// Trigonometric table
		Complex[] expTable = new Complex[n / 2];
		double coef = 2 * Math.PI / n * (inverse ? 1 : -1);
		for (int i = 0; i < n / 2; i++)
			expTable[i] = Complex.FromPolarCoordinates(1, i * coef);

		// Bit-reversed addressing permutation
		for (int i = 0; i < n; i++)
		{
			int j = ReverseBits(i, levels);
			if (j > i)
			{
                (sequence[j], sequence[i]) = (sequence[i], sequence[j]);
            }
        }

		// Cooley-Tukey decimation-in-time radix-2 FFT
		for (int size = 2; size <= n; size *= 2)
		{
			int halfsize = size / 2;
			int tablestep = n / size;
			for (int i = 0; i < n; i += size)
			{
				for (int j = i, k = 0; j < i + halfsize; j++, k += tablestep)
				{
					Complex temp = sequence[j + halfsize] * expTable[k];
					sequence[j + halfsize] = sequence[j] - temp;
					sequence[j] += temp;
				}
			}
			if (size == n)  // Prevent overflow in 'size *= 2'
			{
                break;
            }
		}
	}

	private static void TransformBluestein(Complex[] vec, bool inverse)
	{
		// Find a power-of-2 convolution length m such that m >= n * 2 + 1
		int n = vec.Length;
		if (n >= 0x20000000)
		{
            throw new ArgumentException("Массив слишком большой");
        }

		int m = 1;

		while (m < n * 2 + 1)
		{
            m *= 2;
        }

		// Trigonometric table
		Complex[] expTable = new Complex[n];
		double coef = Math.PI / n * (inverse ? 1 : -1);
		for (int i = 0; i < n; i++)
		{
			int j = (int)((long)i * i % (n * 2));  // This is more accurate than j = i * i
			expTable[i] = Complex.Exp(new Complex(0, j * coef));
		}

		// Temporary vectors and preprocessing
		Complex[] avec = new Complex[m];
		for (int i = 0; i < n; i++)
			avec[i] = vec[i] * expTable[i];
		Complex[] bvec = new Complex[m];
		bvec[0] = expTable[0];
		for (int i = 1; i < n; i++)
			bvec[i] = bvec[m - i] = Complex.Conjugate(expTable[i]);

		// Convolution
		Complex[] cvec = new Complex[m];
		Convolve(avec, bvec, cvec);

		// Postprocessing
		for (int i = 0; i < n; i++)
			vec[i] = cvec[i] * expTable[i];
	}

	private static void Convolve(Complex[] left, Complex[] right, Complex[] result)
	{
		int length = left.Length;

		if (length != right.Length || length != result.Length)
		{
            throw new ArgumentException("Несоответствующие длины.");
        }

		left = (Complex[])left.Clone();
		right = (Complex[])right.Clone();
		Transform(left, false);
		Transform(right, false);

		for (int i = 0; i < length; i++)
		{
            left[i] *= right[i];
        }

		Transform(left, true);
		for (int i = 0; i < length; i++)
		{
            result[i] = left[i] / length;
        }
	}

	private static int ReverseBits(int value, int width)
	{
		int result = 0;

		for (int i = 0; i < width; i++, value >>= 1)
		{
            result = (result << 1) | (value & 1);
        }

		return result;
	}
}
