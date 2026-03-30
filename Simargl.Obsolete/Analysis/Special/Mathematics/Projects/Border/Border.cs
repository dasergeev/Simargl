using System;
using System.Linq;

namespace Simargl.Mathematics.Projects.Border;

/// <summary>
/// 
/// </summary>
public class Border
{
    /// <summary>
    /// Редукция двух массивов к одному.
    /// </summary>
    /// <param name="a">
    /// Один массив.
    /// </param>
    /// <param name="b">
    /// Другой массив.
    /// </param>
    /// <returns>
    /// Результирующий массив.
    /// </returns>
    public static int[] ArrayReduction2to1(int[] a, int[] b)
    {
        int la = a.Length;
        int lb = b.Length;

        int[] c = new int[Math.Min(la, lb)];
        int[] c2 = new int[Math.Max(la, lb)];

        if (la + lb == 0)
        {
            return c;
        }

        if (la * lb == 0)
        {
            if (la > 0)
            {
                return a;
            }

            if (lb > 0)
            {
                return b;
            }
        }

        int n;  // длина c
        int n2; // длина c2

        if (la > lb)
        {
            Array.ConstrainedCopy(b, 0, c, 0, lb);
            Array.ConstrainedCopy(a, 0, c2, 0, la);
            n = lb;
            n2 = la;
        }
        else
        {
            Array.ConstrainedCopy(b, 0, c2, 0, lb);
            Array.ConstrainedCopy(a, 0, c, 0, la);
            n = la;
            n2 = lb;
        }

        int t; int ct;
        for (int i = 0; i < n; i++)
        {
            t = c[i];

            // Ищем элемент c2, самый близкий к t:
            int minElement = Math.Abs(c2[0] - t);
            int indexMin = 0;

            for (int j = 1; j < n2; j++)
            {
                ct = Math.Abs(c2[j] - t);

                if (ct < minElement)
                {
                    minElement = ct;
                    indexMin = j;
                }
            }

            c[i] = (int)Math.Round(0.5 * (t + c2[indexMin]));
        }

        return c;
    }

    /// <summary>
    /// Выявление временных интервалов, когда колесо находится на сечении.
    /// </summary>
    /// <param name="indEnter">
    /// Индексы моментов времени наезда колеса на сечение.
    /// </param>
    /// <param name="indExit">
    /// Индексы моментов времени съезда колеса с сечения.
    /// </param>
    /// <returns>
    /// Интервалы времени, когда колесо находится на сечении.
    /// </returns>
    public static int[][] SitesChoice(int[] indEnter, int[] indExit)
    {
        int enterLength = indEnter.Length;
        int exitLength = indExit.Length;
        int maxLength = Math.Max(enterLength, exitLength);

        if (enterLength + exitLength == maxLength)
        {
            return Array.Empty<int[]>();
        }

        int[][] resultIndexes = new int[2][];
        int[] enters = new int[maxLength];
        int[] exits = new int[maxLength];
        int c = 0; // Счётчик.

        if (enterLength <= exitLength)
        {
            int iEn; int iEx; int ix;
            for (int i = 0; i < enterLength; i++)
            {
                iEn = indEnter[i];
                ix = Arrays.FindFirstIndex(indExit, x => x > iEn);
                if (ix > -1)
                {
                    iEx = indExit[ix];
                    if (!Arrays.ExistsComponents(indEnter, x => x > iEn && x < iEx))
                    {
                        enters[c] = iEn;
                        exits[c] = iEx;
                        c++;
                    }
                }
            }
        }

        if (enterLength > exitLength)
        {
            int iEn; int iEx; int iend;
            for (int i = 0; i < exitLength; i++)
            {
                iEx = indExit[i];
                iend = Arrays.FindLastIndex(indEnter, x => x < iEx);
                if (iend > -1)
                {
                    iEn = indEnter[iend];
                    if (!Arrays.ExistsComponents(indExit, x => x > iEn && x < iEx))
                    {
                        enters[c] = iEn;
                        exits[c] = iEx;
                        c++;
                    }
                }
            }
        }

        Array.Resize(ref enters, c);
        Array.Resize(ref exits, c);

        resultIndexes[0] = enters;
        resultIndexes[1] = exits;

        return resultIndexes;
    }

    ///*
    /// <summary>
    /// Анализ временного ряда на наличие тренда.
    /// </summary>
    /// <param name="sampleRate">
    /// Частота выборки, Гц.
    /// </param>
    /// <param name="signalArray">
    /// Массив значений анализируемого временного ряда.
    /// </param>
    /// <param name="timeStep">
    /// Шаг по времени для вычисления усреднённых наклонов (производных).
    /// </param>
    /// <param name="nitS">
    /// Число итераций для сглаживания интегрированием-дифференцированием.
    /// </param>
    /// <param name="nitE">
    /// Число итераций для выбрасывания экстремумов.
    /// </param>
    /// <param name="nsN">
    /// Число СКО для идентификации "шума".
    /// </param>
    /// <param name="ns0">
    /// Число СКО для корректировки нуля.
    /// </param>
    /// <param name="ns0S">
    /// Число СКО для идентификации наклонов "близких к нулю".
    /// </param>
    /// <param name="minInd">
    /// Минимальное количество элементов - параметр, передаваемый в функцию WorkWithArrays.AllContSubArrays.
    /// </param>
    /// <returns>
    /// Структура типа TrendNoiseEstimationOutputData, содержащая результат анализа.
    /// </returns>
    public static TrendNoiseEstimationOutputData TrendNoiseEstimation(int sampleRate, double[] signalArray, double timeStep, int nitS, int nitE, double nsN, double ns0, double ns0S, int minInd)
    {
        double[] trend = Calculus.SmoothingByIntDiff(signalArray, nitS); // Оценка тренда сглаживанием.

        // Выделение шума:
        double[] smt = Pointwise.Difference(signalArray, trend); // Сигнал минус тренд = шум.
        double[] noise = Pointwise.ArrayScalarSum(smt, -smt.Average()); // Обнулили среднее значение шума.

        double stdNoise = Statistics.Std(noise); // СКО шума.

        // Статистическая оценка границ шума:
        double upperNoise = nsN * stdNoise;  // Верхняя оценка шума.
        double lowerNoise = -nsN * stdNoise; // Нижняя оценка шума.

        double[][] slopesData = Calculus.GetSlopes(sampleRate, signalArray, timeStep); // Вычисление наклонов.
        double[] slopes = slopesData[0];
        double[] indB = slopesData[1];
        double[] indE = slopesData[2];

        double slope;
        double prevslope;
        double nextslope;
        double[] nslopes = Arrays.NeglectExtremums(slopes, nitE);

        /*
        // Проверка !!!!!!!!!

        for (int i = 0; i < nslopes.Length; i++)
        {
            Console.WriteLine($"nslope[{i}] = {nslopes[i]};"); //    indB[{i}] = {indB[i]};   indE[{i}] = {indE[i]}
        }

        // Проверка !!!!!!!!!
        */

        // Статистическая оценка границ для идентификации наклонов "близких к нулю": //       
        double muSlps = nslopes.Average();    //
        double stdSlps = Statistics.Std(nslopes);    //
        double upperSlopes = muSlps + ns0S * stdSlps;//
        double lowerSlopes = muSlps - ns0S * stdSlps;//
                                                        ///////////////////////////////////////////////


        // Корректировка нуля - BEGIN:
        //int[] inull = new int[0];
        int[] in0 = Array.Empty<int>();// new int[0];

        int dataLength = slopes.Length;

        for (int i = 0; i < dataLength; i++)
        {
            slope = slopes[i];
            if (slope >= lowerSlopes && slope <= upperSlopes)
            {
                in0 = Arrays.ConcatenationNeat(in0, Arrays.IndexesArray((int)indB[i], 1, (int)indE[i]));
            }
        }

        // 1-й этап коррекции нуля:
        int[] inull1 = Arrays.LargestContSubArray(in0);
        double nullLevel1 = Arrays.SubArray(signalArray, inull1).Average();

        // 2-й этап коррекции нуля:
        // Статистическая оценка границ шума для коррекции нуля:
        double uppN0 = ns0 * stdNoise;
        double lowN0 = -ns0 * stdNoise;

        int[] inull2 = Arrays.FindIndexes(trend, x => x >= lowN0 && x <= uppN0);
        double nullLevel2 = Arrays.SubArray(signalArray, inull2).Average();

        double nullLevel = nullLevel1 + nullLevel2; // ...корректировка нуля.

        double[] correctedSignal = Pointwise.ArrayScalarSum(signalArray, -nullLevel);
        double[] correctedTrend = Pointwise.ArrayScalarSum(trend, -nullLevel);
        // Корректировка нуля - END.

        // Поиск моментов наезда/съезда колеса на сечение - BEGIN:

        // Индексы моментов времени наезда/съезда колеса на сечение:
        int[] indEnterSLP = Array.Empty<int>(); // Определяемые по наклонам.
        int[] indExitSLP = Array.Empty<int>();  // Определяемые по наклонам.            

        for (int i = 0; i < dataLength; i++)
        {
            slope = slopes[i];
            if (slope > upperSlopes)
            {
                if (i > 0)
                {
                    prevslope = slopes[i - 1];
                    if (prevslope >= lowerSlopes && prevslope <= upperSlopes)
                    {
                        indEnterSLP = Arrays.ConcatenationNeat(indEnterSLP, new int[] { (int)Math.Round(0.5 * (indB[i - 1] + indE[i - 1])) });
                    }
                }
            }

            if (slope < lowerSlopes)
            {
                if (i < dataLength - 1)
                {
                    nextslope = slopes[i + 1];
                    if (nextslope >= lowerSlopes && nextslope <= upperSlopes)
                    {
                        indExitSLP = Arrays.Concatenation(indExitSLP, new int[] { (int)Math.Round(0.5 * (indB[i + 1] + indE[i + 1])) });
                    }
                }
            }
        }

        int[] n0 = Arrays.FindIndexes(trend, x => x >= upperNoise);
        int[][] arrn0 = Arrays.AllContSubArrays(n0, minInd);
        int ln0 = arrn0.Length;

        int[] indEnterSGN = Array.Empty<int>(); // Определяемые по превышению сигналом шума.
        int[] indExitSGN = Array.Empty<int>();  // Определяемые по превышению сигналом шума.

        if (ln0 > 0)
        {
            indEnterSGN = new int[ln0]; // Определяемые по превышению сигналом шума.
            indExitSGN = new int[ln0];  // Определяемые по превышению сигналом шума.

            int lastInd;
            for (int i = 0; i < ln0; i++)
            {
                lastInd = arrn0[i].Length - 1;
                indEnterSGN[i] = arrn0[i][0];
                indExitSGN[i] = arrn0[i][lastInd];
            }
        }

        int[] indEnter = ArrayReduction2to1(indEnterSLP, indEnterSGN);
        int[] indExit = ArrayReduction2to1(indExitSLP, indExitSGN);
        // Поиск моментов наезда/съезда колеса на сечение - END.

        int opN = 0; int opS = 0;

        if (trend[Arrays.IndexOfMax(trend)] <= upperNoise)
        {
            opN = 1;
        }

        if (indEnter.Length + indExit.Length == 0)
        {
            opS = 1;
        }

        TrendNoiseEstimationOutputDataCharacter isNoise = (TrendNoiseEstimationOutputDataCharacter)(opN + opS);

        int[][] sites = SitesChoice(indEnter, indExit);

        int numberIntervals = sites[0].Length; // число интервалов проезда по сечению
        int[,] indIntervals = new int[numberIntervals, 2]; // интервалы проезда по сечению

        double[] minimums = new double[numberIntervals]; // массив минимумов.
        double[] maximums = new double[numberIntervals];
        double[] averages = new double[numberIntervals];
        double[] stds = new double[numberIntervals];

        int i0; int i1; // вспомогательные индексы.            

        if (numberIntervals > 0)
        {
            for (int i = 0; i < numberIntervals; i++)
            {
                i0 = sites[0][i];
                i1 = sites[1][i];
                indIntervals[i, 0] = i0;
                indIntervals[i, 1] = i1;
                int[] indArray = Arrays.IndexesArray(i0, 1, i1);
                //double[] currentSubArray = WorkWithArrays.SubArray(correctedSignal, indArray);
                double[] currentSubArray = Arrays.SubArray(correctedTrend, indArray);

                minimums[i] = currentSubArray[Arrays.IndexOfMin(currentSubArray)];
                maximums[i] = currentSubArray[Arrays.IndexOfMax(currentSubArray)];
                averages[i] = currentSubArray.Average();
                stds[i] = Statistics.Std(currentSubArray);
            }
        }

        TrendNoiseEstimationOutputData outputData = new(isNoise, nullLevel, indEnter, indExit, numberIntervals, indIntervals, minimums, maximums, averages, stds);

        return outputData;
    }
}