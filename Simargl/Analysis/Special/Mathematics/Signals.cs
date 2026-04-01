using System;
using System.Linq;

namespace Simargl.Mathematics;

/// <summary>
/// Содержит статические функции для простейшей обработки массивов значений сигнала из кадра регистрации. 
/// </summary>
public class Signals
{
    /// <summary>
    /// Передискретизация на более низкую частоту выборки.
    /// </summary>
    /// <param name="array">
    /// Массив числовых данных.
    /// </param>
    /// <param name="nSites">
    /// Число точек, "склеивающихся" в одну.
    /// </param>
    /// <returns>
    /// Передискретизированный массив.
    /// </returns>
    public static double[] ResamplingLowering(double[] array, int nSites)
    {
        int length = array.Length;

        int n = (int)Math.Floor(((double)length) / ((double)nSites)); // Примерное число кусков по nSites отсчётов.
        int newLength;

        if (n * nSites == length)
        {
            newLength = n;
        }
        else
        {
            newLength = n + 1;
        }

        double[] newArray = new double[newLength]; // Возвращаемый массив.

        for (int i = 0; i < n; i++)
        {
            int[] indexes = Arrays.IndexesArray(i * nSites, 1, (i + 1) * nSites - 1);

            newArray[i] = Arrays.SubArray(array, indexes).Average();
        }

        if (newLength > n)
        {
            int[] indexes = Arrays.IndexesArray(n * nSites, 1, length - 1);
            newArray[n] = Arrays.SubArray(array, indexes).Average();
        }

        return newArray;
    }

    /// <summary>
    /// Передискретизация с 1 герца на более высокую частоту выборки.
    /// </summary>
    /// <param name="array">
    /// Массив числовых данных.
    /// </param>
    /// <param name="sampleRate">
    /// Новая частота дискретизации, Гц.
    /// </param>
    /// <returns>
    /// Передискретизированный массив.
    /// </returns>
    public static double[] ResamplingFrom1toHigher(double[] array, double sampleRate)
    {
        int n = (int)Math.Round(sampleRate);
        int length = array.Length;

        double[] newArray = new double[n * length];

        double value0 = array[0];
        double value1;

        for (int j = 0; j < n; j++)
        {
            newArray[j] = value0;
        }
        
        for (int i = 1; i < length; i++)
        {
            value0 = array[i - 1];
            value1 = array[i];
                        
            int j0 = i * n;
            int j1 = j0 + n;
            double c = (value1 - value0)/(n - 1);

            for (int j = j0; j < j1; j++)
            {
                //newArray[j] = array[i];
                newArray[j] = value0 + c * (j - j0);
            }
        }

        return newArray;
    }

    /// <summary>
    /// Пересемплирование на другую частоту дискретизации.
    /// </summary>
    /// <param name="y">
    /// Вещественный массив.
    /// </param>
    /// <param name="oldSampleRate">
    /// Старая частота дискретизации.
    /// </param>
    /// <param name="newSampleRate">
    /// Новая частота дискретизации.
    /// </param>
    /// <returns>
    /// Результат пересемплирования исходного массива.
    /// </returns>
    public static double[] Resampling(double[] y, double oldSampleRate, double newSampleRate)
    {
        int oldLength = y.Length; // Длина исходного массива.
        double tLast = oldLength / oldSampleRate; // Последний момент времени.

        // Длина пересемплированного массива:
        int newLength = (int)Math.Round(tLast * newSampleRate);

        // Массивы моментов времени:
        double[] timesOld = Arrays.LinSpace(1 / oldSampleRate, tLast, oldLength);
        double[] timesNew = Arrays.LinSpace(1 / newSampleRate, tLast, newLength);

        return Interpolations.Linear(timesOld, y, timesNew);
    }

    /// <summary>
    /// Пересемплирование на другую частоту дискретизации.
    /// </summary>
    /// <param name="y">
    /// Вещественный массив.
    /// </param>
    /// <param name="oldSampleRate">
    /// Старая частота дискретизации.
    /// </param>
    /// <param name="newLength">
    /// Длина пересемплированного массива.
    /// </param>
    /// <returns>
    /// Результат пересемплирования исходного массива.
    /// </returns>
    public static double[] Resampling(double[] y, double oldSampleRate, int newLength)
    {
        int oldLength = y.Length; // Длина исходного массива.
        double tLast = oldLength / oldSampleRate; // Последний момент времени.

        // Частота дискретизации пересемплированного массива:
        double newSampleRate = newLength / tLast;
        
        // Массивы моментов времени:
        double[] timesOld = Arrays.LinSpace(1 / oldSampleRate, tLast, oldLength);
        double[] timesNew = Arrays.LinSpace(1 / newSampleRate, tLast, newLength);

        return Interpolations.Linear(timesOld, y, timesNew);
    }
}
