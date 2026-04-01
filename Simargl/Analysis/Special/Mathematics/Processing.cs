namespace Simargl.Mathematics;

/// <summary>
/// Содержит статические функции, используемые в обработке.
/// </summary>
public class Processing
{
    /*
    /// <summary>
    /// Возвращает среднее из трёх максимальных амплитуд, вычисленных методом полных циклов.
    /// </summary>
    /// <param name="array">
    /// Массив вещественных чисел.
    /// </param>
    /// <returns>
    /// Среднее из трёх максимальных амплитуд.
    /// </returns>
    public static double FullCycleAmplitude3(double[] array)
    {
        double maxScope = array.Max() - array.Min();

        Histogram hist = new(maxScope, 30);

        hist.FullCycles(array);

        double[] amplitudes = new double[3];

        int counter = 0; // Счётчик элементов массива amplitudes.
        int i = hist.Count - 1; // Счётчик элементов гистограммы.

        while (counter < 3)
        {
            int n = hist[i].Count;
            if (n > 0)
            {
                for (int j = 0; j < n; j++)
                {
                    if (counter < 3)
                    {
                        amplitudes[counter++] = hist[i].Amplitude;
                    }
                }
            }

            i--;
        }

        double meanAmplitude = amplitudes.Average();

        return meanAmplitude;
    }
    */
}
