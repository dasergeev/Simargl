using System;
using System.Collections.Generic;
using System.Linq;

namespace Simargl.Mathematics;

/// <summary>
/// Содержит статические функции для вычисления элементарных статистических параметров выборок.
/// </summary>
public static class Statistics
{
    /// <summary>
    /// Пересчёт среднего арифметического с учётом дополнительных данных по выборке.
    /// </summary>
    /// <param name="oldMean">
    /// Значение среднего арифметического по предыдущей выборке.
    /// </param>
    /// <param name="oldVolume">
    /// Объём предыдущей выборки.
    /// </param>
    /// <param name="addData">
    /// Дополнительные значения в выборку.
    /// </param>
    /// <returns>
    /// Среднее арифметическое значение с учётом дополнительных данных по выборке.
    /// </returns>
    public static double MeanReCalcByAddData(double oldMean, long oldVolume, IEnumerable<double> addData)
    {
        int newVolume = addData.Count();
        double totalVolume = (double)(oldVolume + newVolume);

        double ow = ((double)oldVolume) / ((double)totalVolume);
        double nw = ((double)newVolume) / ((double)totalVolume);

        return ow * oldMean + nw * addData.Average();
    }

    /// <summary>
    /// Стандартное отклонение входной выборки.
    /// </summary>
    /// <param name="tuple">
    /// Коллекция вещественных чисел.
    /// </param>
    /// <returns>
    /// Квадратный корень из несмещённой оценки дисперсии.
    /// </returns>
    public static double Std(IEnumerable<double> tuple)
    {
        int n = 0;
        double s = 0.0;
        double s2 = 0.0;

        foreach (double item in tuple)
        {
            ++n;
            s += item;
            s2 += item * item;
        }

        if (n < 2)
        {
            return 0.0;
        }
        else
        {
            return Math.Sqrt((s2 - s * s / n) / (n - 1));
        }            
    }

    /// <summary>
    /// Стандартное отклонение входной выборки (вариант, когда среднее уже посчитано).
    /// </summary>
    /// <param name="tuple">
    /// Коллекция вещественных чисел.
    /// </param>
    /// <param name="m">
    /// Среднее входной коллекции.
    /// </param>
    /// <returns>
    /// Квадратный корень из несмещённой оценки дисперсии.
    /// </returns>
    public static double Std(IEnumerable<double> tuple, double m)
    {
        int n = 0;
        double s = 0.0;
        double y;          

        foreach (double item in tuple)
        {
            ++n;
            y = item - m;
            s += y * y;                
        }

        if (n < 2)
        {
            return 0.0;
        }
        else
        {
            return Math.Sqrt(s / (n - 1));
        }            
    }

    /// <summary>
    /// Дисперсия входной выборки.
    /// </summary>
    /// <param name="tuple">
    /// Коллекция вещественных чисел.
    /// </param>
    /// <returns>
    /// Несмещённая оценка дисперсии.
    /// </returns>
    public static double Var(IEnumerable<double> tuple)
    {
        int n = 0;
        double s = 0.0;
        double s2 = 0.0;

        foreach (double item in tuple)
        {
            ++n;
            s += item;
            s2 += item * item;
        }

        if (n < 2)
        {
            return 0.0;
        }
        else
        {
            return (s2 - s * s / n) / (n - 1);
        }                        
    }

    /// <summary>
    /// Дисперсия входной выборки (вариант, когда среднее уже посчитано).
    /// </summary>
    /// <param name="tuple">
    /// Коллекция вещественных чисел.
    /// </param>
    /// <param name="m">
    /// Среднее входной коллекции.
    /// </param>
    /// <returns>
    /// Несмещённая оценка дисперсии.
    /// </returns>
    public static double Var(IEnumerable<double> tuple, double m)
    {
        int n = 0;
        double s = 0.0;
        double y;

        foreach (double item in tuple)
        {
            ++n;
            y = item - m;
            s += y * y;
        }

        if (n < 2)
        {
            return 0.0;
        }
        else
        {
            return s / (n - 1);
        }
    }

    /// <summary>
    /// Пересчёт дисперсии с учётом дополнительных данных по выборке.
    /// </summary>
    /// <param name="oldMean">
    /// Значение среднего арифметического по предыдущей выборке.
    /// </param>
    /// <param name="oldVar">
    /// Значение дисперсии по предыдущей выборке.
    /// </param>
    /// <param name="oldVolume">
    /// Объём предыдущей выборки.
    /// </param>
    /// <param name="addData">
    /// Дополнительные значения в выборку.
    /// </param>
    /// <returns>
    /// Значение дисперсии с учётом дополнительных данных по выборке.
    /// </returns>
    public static double VarReCalcByAddData(double oldMean, double oldVar, long oldVolume, IEnumerable<double> addData)
    {
        int newVolume = addData.Count(); // Объём дополнительных данных.
        double totalVolume = (double)(oldVolume + newVolume); // Объём совокупности данных.

        double newMean = addData.Average(); // Среднее дополнительных данных.            
        double newVar = Var(addData); // Дисперсия дополнительных данных.

        // Среднее совокупности данных:
        double ow = ((double)oldVolume) / ((double)totalVolume);
        double nw = ((double)newVolume) / ((double)totalVolume);

        double totalMean = ow * oldMean + nw * addData.Average();
        // ...среднее совокупности данных.

        double dmo = oldMean - totalMean; double dmn = newMean - totalMean;

        return (oldVar * (oldVolume - 1) + dmo * dmo * oldVolume + newVar * (newVolume - 1) + dmn * dmn * newVolume) / (totalVolume - 1);
    }

    /// <summary>
    /// Пересчёт дисперсии с учётом дополнительных данных по выборке (вариант, когда среднее уже посчитано).
    /// </summary>
    /// <param name="oldMean">
    /// Значение среднего арифметического по предыдущей выборке.
    /// </param>
    /// <param name="totalMean">
    /// Среднее арифметическое значение с учётом дополнительных данных по выборке.
    /// </param>
    /// <param name="oldVar">
    /// Значение дисперсии по предыдущей выборке.
    /// </param>
    /// <param name="oldVolume">
    /// Объём предыдущей выборки.
    /// </param>
    /// <param name="addData">
    /// Дополнительные значения в выборку.
    /// </param>
    /// <returns>
    /// Значение дисперсии с учётом дополнительных данных по выборке.
    /// </returns>
    public static double VarReCalcByAddData(double oldMean, double totalMean, double oldVar, long oldVolume, IEnumerable<double> addData)
    {
        int newVolume = addData.Count(); // Объём дополнительных данных.
        double totalVolume = (double)(oldVolume + newVolume); // Объём совокупности данных.

        double newMean = addData.Average(); // Среднее дополнительных данных.            
        double newVar = Var(addData); // Дисперсия дополнительных данных.            

        double dmo = oldMean - totalMean; double dmn = newMean - totalMean;

        return (oldVar * (oldVolume - 1) + dmo * dmo * oldVolume + newVar * (newVolume - 1) + dmn * dmn * newVolume) / (totalVolume - 1);
    }
        
    /// <summary>
    /// Находит минимум и максимум коллекции вещественных чисел.
    /// </summary>
    /// <param name="tuple">
    /// Коллекция вещественных чисел.
    /// </param>
    /// <returns>
    /// Минимум (result[0]) и Максимум (result[1]) входной коллекции.
    /// </returns>
    public static double[] MinAndMax(IEnumerable<double> tuple)
    {            
        double min = tuple.ElementAt(0);
        double max = min;

        foreach (double item in tuple)
        {                
            if (item < min) { min = item; }
            if (item > max) { max = item; }
        }

        return new double[] { min, max };
    }

    /// <summary>
    /// Правый квантиль по выборке значений случайной величины (теоретическое распределение не известно).
    /// </summary>
    /// <param name="variateValues">
    /// Выборка значений случайной величины.
    /// </param>
    /// <param name="level">
    /// Уровень квантиля (вероятность непревышения), от 0 до 1.
    /// </param>
    /// <returns>
    /// Значение правого квантиля.
    /// </returns>
    public static double RightQuantileEmpirical(double[] variateValues, double level)
    {
        double q = 0.0; // Возвращаемое значение.

        int n = variateValues.Length;

        if (n == 0)
        {
            return q;
        }

        double leftPoint = variateValues.Min();
        double rightPoint = variateValues.Max();
        double midPoint = 0.5 * (leftPoint + rightPoint);

        double plprev = 0.0;

        bool doIt = true;

        while (doIt)
        {
            int[] il = Arrays.FindIndexes(variateValues, x => x <= midPoint);
            int[] ig = Arrays.FindIndexes(variateValues, x => x > midPoint);

            double pl = (double)il.Length / (double)n;

            if (plprev == pl)
            {
                doIt = false;
                double x1 = Arrays.SubArray(variateValues, il).Max();
                double x2 = Arrays.SubArray(variateValues, ig).Min();
                q = (level - pl) * (x2 - x1) * (double)n + x1;
            }
            else
            {
                if (pl < level)
                {
                    leftPoint = midPoint;
                }

                if (pl > level)
                {
                    rightPoint = midPoint;
                }

                midPoint = 0.5 * (leftPoint + rightPoint);

            }

            plprev = pl;
        }

            return q;
        }
                
        /// <summary>
        /// Правый квантиль по частотному распределению случайной величины.
        /// </summary>
        /// <param name="binBounds">
        /// Границы бинов.
        /// </param>
        /// <param name="frequencies">
        /// Относительные частоты попадания в бины.
        /// </param>
        /// <param name="level">
        /// Уровень квантиля (вероятность непревышения), от 0 до 1.
        /// </param>
        /// <returns>
        /// Значение правого квантиля.
        /// </returns>
        public static double RightQuantileEmpirical(double[] binBounds, double[] frequencies, double level)
        {
            int n = binBounds.Length - 1;
            double q = binBounds[n];
            double p = 1.0;
            double currentFreq = 0.0;

            for (int i = n - 1; i >= 0; i--)
            {                
                if (p <= level)
                {
                    if (currentFreq == 0)
                    {
                        q = binBounds[i + 1];
                    }
                    else
                    {
                        double nearb = binBounds[i + 1];
                        double dx = binBounds[i + 2] - nearb;
                        q = nearb + dx * (level - p) / currentFreq;
                    }
                    
                    break;
                }

                p -= currentFreq;
            }

        return q;
    }

    /// <summary>
    /// Левый квантиль по выборке значений случайной величины (теоретическое распределение не известно).
    /// </summary>
    /// <param name="variateValues">
    /// Выборка значений случайной величины.
    /// </param>
    /// <param name="level">
    /// Уровень квантиля.
    /// </param>
    /// <returns>
    /// Левый квантиль.
    /// </returns>
    public static double LeftQuantileEmpirical(double[] variateValues, double level)
    {
        double q = 0.0; // Возвращаемое значение.

        int n = variateValues.Length;

        if (n == 0)
        {
            return q;
        }

        double leftPoint = variateValues.Min();
        double rightPoint = variateValues.Max();
        double midPoint = 0.5 * (leftPoint + rightPoint);

        double pgprev = 0.0;

        bool doIt = true;

        while (doIt)
        {
            int[] ig = Arrays.FindIndexes(variateValues, x => x >= midPoint);
            int[] il = Arrays.FindIndexes(variateValues, x => x < midPoint);
            double pg = (double)ig.Length / (double)n;

            if (pgprev == pg)
            {
                doIt = false;
                double x1 = Arrays.SubArray(variateValues, il).Max();
                double x2 = Arrays.SubArray(variateValues, ig).Min();
                q = (level - pg) * (x2 - x1) * (double)n + x2;
            }
            else
            {
                if (pg < level)
                {
                    rightPoint = midPoint;
                }

                if (pg > level)
                {
                    leftPoint = midPoint;
                }

                midPoint = 0.5 * (leftPoint + rightPoint);

            }

            pgprev = pg;
        }

        return q;
    }

        /// <summary>
        /// Левый квантиль по частотному распределению случайной величины.
        /// </summary>
        /// <param name="distr">
        /// Структура с информацией о распределении частот.
        /// </param>
        /// <param name="level">
        /// Уровень квантиля (вероятность непревышения), от 0 до 1.
        /// </param>
        /// <returns>
        /// Значение левого квантиля.
        /// </returns>
        public static double LeftQuantileEmpirical(DistributionFreqs distr, double level)
        {
            int n = distr.Bins;
            double q = distr.BinBounds[0];
            double p = 1.0;

            for (int i = 0; i < n; i++)
            {                
                if (p <= level)
                {
                    q = distr.BinBounds[i] * level / p;
                    break;
                }

                p -= distr.Frequencies[i];
            }

        return q;
    }

    /// <summary>
    /// Конвертация распределения с количествами попадания в распределение с частотами попадания.
    /// </summary>
    /// <param name="distrQ">
    /// Распределение с количествами попадания.
    /// </param>
    /// <returns>
    /// Распределение с относительными частотами попадания.
    /// </returns>
    public static DistributionFreqs ConvertQuants2Freqs(DistributionQuants distrQ)
    {
        DistributionFreqs distrF = new();
            
        int bns = distrQ.Bins;
        distrF.Bins = bns; // Поле Bins.

        int bns1 = bns + 1;
        double[] binBnds = new double[bns1];

        for (int i = 0; i < bns1; i++)
        {
            binBnds[i] = distrQ.BinBounds[i];
        }

        distrF.BinBounds = binBnds; // Поле BinBounds.

        double totalVolume = distrQ.Quantities.Sum();   // (double)Arrays.Sum(distrQ.Quantities);

        double[] freqs = new double[bns];
        for (int i = 0; i < bns; i++)
        {
            freqs[i] = distrQ.Quantities[i] / totalVolume;
        }

        distrF.Frequencies = freqs;

        return distrF;
    }

    /// <summary>
    /// Коррекция распределения по дополнительным данным.
    /// </summary>
    /// <param name="oldDistr">
    /// Исходное распределение.
    /// </param>
    /// <param name="addData">
    /// Дополнительные данные.
    /// </param>
    /// <param name="rightCheck">
    /// Проверка попадания в крайне правую граничную точку.
    /// /// </param>
    /// <returns>
    /// Скорректированное распределение.
    /// </returns>
    public static DistributionQuants DistributionAddData(DistributionQuants oldDistr, double[] addData, bool rightCheck)
    {
        int bns = oldDistr.Bins; // Число бинов.
        int knts = bns + 1;      // Число узлов.            

        double[] binBnds = new double[knts];
        for (int i = 0; i < knts; i++)
        {
            binBnds[i] = oldDistr.BinBounds[i]; // Клон массива границ бинов.
        }

        double bnd1 = binBnds[0]; double bnde = binBnds[bns];
        double dx = binBnds[1] - binBnds[0]; // ширина бина

        long[] quants = new long[bns];
        for (int i = 0; i < bns; i++)
        {
            quants[i] = oldDistr.Quantities[i]; // Клон массива количеств попаданий.
        }

        double[] extremums = MinAndMax(addData);
        double newMin = extremums[0];
        double newMax = extremums[1];

        // Дополнительные бины слева:

        double[] binBndsLeft;
        long[] addQntsLeft;

        if (newMin < bnd1)
        {
            int nnb = (int)Math.Ceiling((bnd1 - newMin) / dx); // Число дополнительных бинов слева.
            //binBndsLeft = Arrays.EquallySpacedDouble(bnd1 - nnb * dx, dx, bnd1 - dx);
            binBndsLeft = Arrays.LinSpace(bnd1 - nnb * dx, dx, bnd1 - dx);
            long o = 0;
            addQntsLeft = Arrays.IdenticalElements(o, nnb);
        }
        else
        {
            binBndsLeft = Array.Empty<double>();
            addQntsLeft = Array.Empty<long>();
        }

        //---------

        // Дополнительные бины справа:

        double[] binBndsRight;
        long[] addQntsRight;

        if (newMax > bnde)
        {
            int nnb = (int)Math.Ceiling((newMax - bnde) / dx); // Число дополнительных бинов справа.
            //binBndsRight = Arrays.EquallySpacedDouble(bnde + dx, dx, bnde + nnb * dx);
            binBndsRight = Arrays.LinSpace(bnde + dx, dx, bnde + nnb * dx);
            long o = 0;
            addQntsRight = Arrays.IdenticalElements(o, nnb);
        }
        else
        {
            binBndsRight = Array.Empty<double>();
            addQntsRight = Array.Empty<long>();
        }

        binBnds = Arrays.Concatenation(binBndsLeft, binBnds, binBndsRight);
        quants = Arrays.Concatenation(addQntsLeft, quants, addQntsRight);
                        
        // Подсчитываем количества попаданий в бины...
        bns = binBnds.Length - 1;
        int nDt = addData.Length;
        double leftPoint; double rightPoint;
        double currentValue;
        for (int i = 0; i < nDt; i++)
        {
            currentValue = addData[i];

            for (int k = 0; k < bns; k++)
            {
                leftPoint = binBnds[k];
                rightPoint = binBnds[k + 1];
                if ((currentValue >= leftPoint) && (currentValue < rightPoint))
                {
                    quants[k] += 1;
                    break;
                }
            }

            if (rightCheck)
            {
                if (currentValue == binBnds[bns])
                {
                    quants[bns - 1] += 1;
                }
            }
        }
        // ...подсчитали количества попаданий в бины.

        DistributionQuants newDistr = new()
        {
            Bins = binBnds.Length - 1,
            BinBounds = binBnds,
            Quantities = quants
        };

        return newDistr;            
    }

        /// <summary>
        /// Оценка коэффициента корреляции Пирсона по выборкам.
        /// </summary>
        /// <param name="x">
        /// Выборка значений случайной величины.
        /// </param>
        /// <param name="y">
        /// Выборка значений случайной величины.
        /// </param>
        /// <returns>
        /// Выборочный коэффициент корреляции.
        /// </returns>
        public static double CorrelationCoefficientPearson(double[] x, double[] y)
        {
            // Оценки матожиданий:
            double mx = x.Average(); double my = y.Average();

        int length = x.Length;

        double s = 0.0; double sx = 0.0; double sy = 0.0;

        for (int i = 0; i < length; i++)
        {
            double x0 = x[i] - mx;
            double y0 = y[i] - my;

            s += x0 * y0;
            sx += x0 * x0;
            sy += y0 * y0;
        }

        return s / Math.Sqrt(sx * sy);
    }

    /// <summary>
    /// Оценка коэффициента корреляции Спирмена по выборкам.
    /// </summary>
    /// <param name="x">
    /// Выборка значений случайной величины.
    /// </param>
    /// <param name="y">
    /// Выборка значений случайной величины.
    /// </param>
    /// <returns>
    /// Выборочный коэффициент корреляции.
    /// </returns>
    public static double CorrelationCoefficientSpearman(double[] x, double[] y)
    {
        double[] rx = Arrays.RangValues(x);
        double[] ry = Arrays.RangValues(y);

        return CorrelationCoefficientPearson(rx, ry);
    }

    /// <summary>
    /// Возвращет приближённые значения функции ошибок
    /// </summary>
    /// <param name="x">
    /// Аргумент - действительное число
    /// </param>
    /// <returns>
    /// Значение - действительное число в диапазоне от -1 до 1
    /// </returns>
    public static double Erf(double x)
    {
        double t = 1 / (1 + 0.5 * Math.Abs(x));
        double tau = t * Math.Exp(-Math.Pow(x, 2) - 1.26551223 + 1.00002368 * t + 0.37409196 * Math.Pow(t, 2) + 0.09678418 * Math.Pow(t, 3) - 0.18628806 * Math.Pow(t, 4) + 0.27886807 * Math.Pow(t, 5) - 1.13520398 * Math.Pow(t, 6) + 1.48851587 * Math.Pow(t, 7) - 0.82215223 * Math.Pow(t, 8) + 0.17087277 * Math.Pow(t, 9));
        double y = Math.Sign(x) * (1 - tau);
        return y;
    }

    /// <summary>
    /// Возвращет приближённые значения функции, обратной к функции ошибок
    /// </summary>
    /// <param name="y">
    /// Аргумент - действительное число в диапазоне от -1 до 1
    /// </param>
    /// <returns>
    /// Значение - действительное число
    /// </returns>
    public static double ErfInv(double y)
    {
        // Используемые числовые константы...
        double tol = 0.0000001;             // порог точности для выхода из цикла итераций
        double sqrtpi = Math.Sqrt(Math.PI); // константа, используемая в вычислениях
        double s = Math.Sign(y);            // сохраняем знак y
                                            // ...используемые числовые константы.


        if (s < 0) { y = -y; } // Пользуясь нечётностью, сводим всё к случаю y>=0


        // Находим точку старта x0...
        double a = 0.147;
        double c = 2 / (Math.PI * a);
        double y1 = Math.Log(1 - Math.Pow(y, 2));
        double x0 = Math.Sqrt(Math.Sqrt(Math.Pow(c + 0.5 * y1, 2) - y1 / a) - (c + 0.5 * y1));
        // ...находим точку старта x0.


        // Уточняющие итерации по методу Ньютона...
        // Решаем уравнение f(x) = Erf(x) - y = 0 относительно x

        // Первая итерация...
        double df0 = 2 * Math.Exp(-x0 * x0) / sqrtpi; // производная f'(x0)
        double x1 = x0 - (Erf(x0) - y) / df0;         // первое приближение
        double dist = Math.Abs(x1 - x0);              // расстояние между первым и нулевым приближением
        // ...первая итерация.

        // Итерационный цикл...
        while (dist >= tol)
        {
            x0 = x1;                                // предыдущее приближение
            df0 = 2 * Math.Exp(-x0 * x0) / sqrtpi;  // производная f'(x0)
            x1 = x0 - (Erf(x0) - y) / df0;          // следующее приближение
            dist = Math.Abs(x1 - x0);               // расстояние между первым и нулевым приближением
        }
        // ...итерационный цикл.

        // ...уточняющие итерации по методу Ньютона.

        double x = s * x1;
        return x;
    }

}
