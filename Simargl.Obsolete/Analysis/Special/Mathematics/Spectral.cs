using System;
using System.Numerics;
using Simargl.Compute.Cpu;

namespace Simargl.Mathematics;

/// <summary>
/// Содержит статические функции, связанные со спектральным анализом, фурье-образами, частотной фильтрацией и т.п.
/// </summary>
public class Spectral
{
    /// <summary>
    /// Быстрое преобразование Фурье вещественного массива.
    /// </summary>
    /// <param name="signal">
    /// Массив значений сигнала (из |R !!!).
    /// </param>
    /// <returns>
    /// Массив значений Фурье-образа.
    /// </returns>
    public static Complex[] FastFourierTransform(double[] signal)
    {
        int n = signal.Length; // Длина входного сигнала.            

        Complex[] c = new Complex[n]; // Комплексный дубликат вещественного массива signal.

        for (int i = 0; i < n; i++)
        {
            c[i] = new Complex(signal[i], 0.0);
        }

        Fft.Direct(c); // Выполнено преобразование Фурье массива c и результат помещён в массив c.

        return c;
    }

    /// <summary>
    /// Абсолютные значения Фурье-образа вещественного массива.
    /// </summary>
    /// <param name="signal">
    /// Массив значений сигнала (из |R !!!).
    /// </param>
    /// <returns>
    /// Массив абсолютных значений Фурье-образа.
    /// </returns>
    public static double[] FftAbs(double[] signal)
    {
        Complex[] ts = FastFourierTransform(signal);

        int n = signal.Length; // Длина входного сигнала.
        double[] a = new double[n];

        for (int i = 0; i < n; i++)
        {
            a[i] = ts[i].Magnitude;
        }

        return a;
    }

    /// <summary>
    /// Квадраты абсолютных значений Фурье-образа вещественного массива.
    /// </summary>
    /// <param name="signal">
    /// Массив значений сигнала (из |R !!!).
    /// </param>
    /// <returns>
    /// Массив квадратов абсолютных значений Фурье-образа.
    /// </returns>
    public static double[] FftAbs2(double[] signal)
    {
        Complex[] ts = FastFourierTransform(signal);

        int n = signal.Length; // Длина входного сигнала.
        double[] a2 = new double[n];

        for (int i = 0; i < n; i++)
        {
            double a = ts[i].Magnitude;
            a2[i] = a * a;
        }

        return a2;
    }

    /// <summary>
    /// Формирует массив частот, отвечающий дискретному Фурье-образу сигнала заданной длины и с заданной частотой дискретизации.
    /// </summary>
    /// <param name="sampleRate">
    /// Частота дискретизации, Гц.
    /// </param>
    /// <param name="signalLenth">
    /// Число отсчётов.
    /// </param>
    /// <returns>
    /// Массив частот, учитывающий симметрию Фурье-образа.
    /// </returns>
    public static double[] FrequenciesForDFT(double sampleRate, int signalLenth)
    {
        double[] freqs = new double[signalLenth];
        freqs[0] = 0.0;

        int k; // Количество значений частоты от 0.0 до sampleRate/2

        if (signalLenth % 2 == 0)
        {
            k = signalLenth / 2;
        }
        else
        {
            k = (signalLenth - 1) / 2;
            freqs[k + 1] = 0.5 * sampleRate;
        }

        freqs[k] = 0.5 * sampleRate; // Максимальная из частот.

        double df = 0.5 * sampleRate / (double)k; // Шаг по частоте.            

        if (k > 1)
        {
            for (int i = 1; i < k; i++)
            {
                double currentFrq = (double)i * df; // Текущая частота.
                freqs[i] = currentFrq;
                freqs[signalLenth - i] = currentFrq;
            }
        }

        return freqs;
    }

    /// <summary>
    /// Частотный фильтр на основе дискретного переобразования Фурье.
    /// </summary>
    /// <param name="signal">
    /// Массив значений сигнала.
    /// </param>
    /// <param name="sampleRate">
    /// Частота дискретизации, Гц.
    /// </param>
    /// <param name="cutOffs">
    /// Частота (частоты) среза, Гц.
    /// </param>
    /// <param name="filterType">
    /// Тип фильтра: высокочастотный ("H"), низкочастотный ("L"), полоснопропускающий ("P"), полоснозаграждающий ("B").
    /// </param>
    /// <returns>
    /// Массив значений отфильтрованного сигнала.
    /// </returns>
    public static double[] FourierFrequencyFilter(double[] signal, double sampleRate, double[] cutOffs, string filterType)
    {
        int n = signal.Length; // Длина входного сигнала.
        double[] filteredSignal = new double[n];

        Complex[] c = FastFourierTransform(signal); // Фурье-образ.

        double[] freqs = FrequenciesForDFT(sampleRate, n); // Массив частот.

        int nf0;

        int numberCutFreqs = cutOffs.Length; // Число частот среза (1 или 2)

        if (numberCutFreqs == 1) // Фильтр высоких или низких частот.
        {
            double cutOffFrq = cutOffs[0]; // Частота среза.

            if (filterType == "H") // Фильтр высоких частот (гармоники с частотами выше частоты среза отбрасываются).
            {
                int[] i0 = Arrays.FindIndexes(freqs, x => x > cutOffFrq);
                nf0 = i0.Length;
                if (nf0 > 0)
                {
                    for (int i = 0; i < nf0; i++)
                    {
                        c[i0[i]] = new Complex(0.0, 0.0); // Обнуление на отбрасываемых частотах.
                    }
                }
            }

            if (filterType == "L") // Фильтр низких частот (гармоники с частотами ниже частоты среза отбрасываются).
            {
                int[] i0 = Arrays.FindIndexes(freqs, x => x < cutOffFrq);
                nf0 = i0.Length;
                if (nf0 > 0)
                {
                    for (int i = 0; i < nf0; i++)
                    {
                        c[i0[i]] = new Complex(0.0, 0.0); // Обнуление на отбрасываемых частотах.
                    }
                }
            }
        }



        if (numberCutFreqs == 2) // Полоснопропускающий или полоснозаграждающий фильтр.
        {
            double cutFrq0 = cutOffs[0]; // Нижняя частота среза.
            double cutFrq1 = cutOffs[1]; // Верхняя частота среза.

            if (filterType == "P") // Полоснопропускающий фильтр (гармоники с частотами ниже нижней частоты среза и выше верхней отбрасываются).
            {
                int[] i0 = Arrays.FindIndexes(freqs, x => x < cutFrq0 || x > cutFrq1);
                nf0 = i0.Length;
                if (nf0 > 0)
                {
                    for (int i = 0; i < nf0; i++)
                    {
                        c[i0[i]] = new Complex(0.0, 0.0); // Обнуление на отбрасываемых частотах.
                    }
                }
            }

            if (filterType == "B") // Полоснозаграждающий фильтр (гармоники с частотами выше нижней частоты среза и ниже верхней отбрасываются).
            {
                int[] i0 = Arrays.FindIndexes(freqs, x => x > cutFrq0 && x < cutFrq1);
                nf0 = i0.Length;
                if (nf0 > 0)
                {
                    for (int i = 0; i < nf0; i++)
                    {
                        c[i0[i]] = new Complex(0.0, 0.0); // Обнуление на отбрасываемых частотах.
                    }
                }
            }
        }

        Fft.Inverse(c); // Выполнено обратное преобразование Фурье массива c и результат помещён в массив c.

        for (int i = 0; i < n; i++)
        {
            filteredSignal[i] = c[i].Real;
        }

        return filteredSignal;
    }

    /// <summary>
    /// Амплитудный спектр Фурье.
    /// </summary>
    /// <param name="signal">
    /// Массив значений сигнала.
    /// </param>
    /// <param name="sampleRate">
    /// Частота дискретизации, Гц.
    /// </param>
    /// <returns>
    /// Массив значений амплитудного спектра Фурье (spectrData[1]) и соответствующий массив частот (spectrData[0]).
    /// </returns>
    public static double[][] FourierAmplitudeSpectrum(double[] signal, double sampleRate)
    {
        int n = signal.Length; // Длина входного сигнала.
        double[][] spectrData = new double[2][]; // Выходные данные.

        int spectrLength; // Длина спектра.
        if (n % 2 == 1)
        {
            spectrLength = (n - 1) / 2 + 1;
        }
        else
        {
            spectrLength = n / 2 + 1;
        }

        double[] freqs = Arrays.LinSpace(0.0, 0.5 * sampleRate, spectrLength); // Массив частот.

        double[] spectr = new double[spectrLength]; // Массив значений амплитудного спектра Фурье.

        Complex[] c = FastFourierTransform(signal);

        for (int i = 0; i < spectrLength; i++)
        {
            spectr[i] = Complex.Abs(c[i]);
        }

        spectrData[0] = freqs;
        spectrData[1] = spectr;

        return spectrData;
    }

    /// <summary>
    /// Квадраты значений амплитудного спектра Фурье.
    /// </summary>
    /// <param name="signal">
    /// Массив значений сигнала.
    /// </param>        
    /// <returns>
    /// Массив квадратов значений амплитудного спектра Фурье.
    /// </returns>
    public static double[] FourierAmplitudeSpectrum2(double[] signal)
    {
        int n = signal.Length; // Длина входного сигнала.            

        int spectrLength; // Длина спектра.
        if (n % 2 == 1)
        {
            spectrLength = (n - 1) / 2 + 1;
        }
        else
        {
            spectrLength = n / 2 + 1;
        }

        double[] spectr2 = new double[spectrLength]; // Массив квадратов значений амплитудного спектра Фурье.

        Complex[] c = FastFourierTransform(signal);

        double a;
        for (int i = 0; i < spectrLength; i++)
        {
            a = Complex.Abs(c[i]);
            spectr2[i] = a * a;
        }


        return spectr2;
    }

    /// <summary>
    /// Возвращает автоспектральную плотность процесса.
    /// </summary>
    /// <param name="procValues">
    /// Массив значений процесса.
    /// </param>
    /// <param name="sampleRate">
    /// Частота опроса, Гц.
    /// </param>
    /// <param name="Nblocks">
    /// Число блоков (12-16).        
    /// </param>
    /// <returns>
    /// Массив значений автоспектральной плотности процесса и массив соответствующих частот.
    /// </returns>
    public static double[][] AutoSpectrum(double[] procValues, double sampleRate, byte Nblocks)
    {
        int N = procValues.Length; // длина массива значений процесса

        double p = (double)N / (double)Nblocks;
        int pb = (int)Math.Floor(p); // количество отсчётов в блоке

        double dt = 1.0 / sampleRate; // шаг по времени
        double factor = 2.0 * dt / pb;
                                    
        double[][] spectr2Array = new double[Nblocks][];

        for (int i = 0; i < Nblocks; i++)
        {
            spectr2Array[i] = FourierAmplitudeSpectrum2(Arrays.SubArray(procValues, Arrays.IndexesArray(i * pb, pb)));                
        }

        int Kf = spectr2Array[0].Length; // число частот
        double[] spectr = new double[Kf];

        for (int j = 0; j < Kf; j++)
        {
            spectr[j] = 0;
            for (int i = 0; i < Nblocks; i++)
            {
                spectr[j] = spectr[j] + spectr2Array[i][j];
            }
            spectr[j] = factor * spectr[j] / Nblocks;
        }

        double[][] spectrData = new double[2][]; // Выходные данные.
        spectrData[0] = Arrays.LinSpace(0, 0.5 * sampleRate, Kf);
        spectrData[1] = spectr;

        return spectrData;
    }
}
