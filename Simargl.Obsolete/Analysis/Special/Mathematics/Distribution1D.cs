using System;
using System.Collections.Generic;
using System.Linq;

namespace Simargl.Mathematics
{
    /// <summary>
    /// Содержит информацию о распределении случайной величины (частоты попаданий в бины).
    /// </summary>
    public class Distribution1D
    {
        /// <summary>
        /// Поле для хранения объёма выборки.
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Поле для хранения количества бинов.
        /// </summary>
        public int Bins { get; set; }

        /// <summary>
        /// Поле для хранения границ бинов (длина BinBounds > 2).
        /// </summary>
        public double[] BinBounds { get; set; }

        /// <summary>
        /// Поле для хранения частот попаданий в бины.
        /// </summary>
        public double[] Frequencies { get; set; }

        /// <summary>
        /// Конструктор по границам бинов.
        /// </summary>
        /// <param name="bounds">
        /// Массив границ бинов.
        /// </param>
        public Distribution1D(double[] bounds)
        {
            int bns = bounds.Length - 1; // Число бинов.
                        
            // Относительные частоты попадания в бины...
            double[] freqs = new double[bns];
                        
            Volume = 0.0;
            Bins = bns;
            BinBounds = bounds;
            Frequencies = freqs;
        }

        /// <summary>
        /// Конструктор по границам бинов и значениям случайной величины.
        /// </summary>
        /// <param name="bounds">
        /// Массив границ бинов.
        /// </param>
        /// <param name="data">
        /// Коллекция значений случайной величины. Значения, не попадающие ни в один из бинов, игнорируются.
        /// </param>
        public Distribution1D(double[] bounds, IEnumerable<double> data)
        {
            int bns = bounds.Length - 1; // Число бинов.

            double[] qnt = new double[bns]; // Количества попаданий в бины.

            double n = 0.0; // Объём выборки.
            int k;
            foreach (double value in data)
            {
                k = Arrays.IntervalNumber(bounds, value);
                if (k >= 0)
                {
                    qnt[k] += 1.0;
                    ++n;
                }                
            }

            // Подсчитываем относительные частоты попадания в бины...
            double[] freqs = new double[bns];
            for (int i = 0; i < bns; i++)
            {
                freqs[i] = qnt[i] / n;
            }
            // ...подсчитали относительные частоты попадания в бины.

            Volume = n;
            Bins = bns;
            BinBounds = bounds;
            Frequencies = freqs;
        }

        /// <summary>
        /// Конструктор по значениям случайной величины, нижней и верхней границе, ширине бина.
        /// </summary>
        /// <param name="data">
        /// Коллекция значений случайной величины. Значения, не попадающие ни в один из бинов, игнорируются.
        /// </param>
        /// <param name="minValue">
        /// Нижняя граница.
        /// </param>
        /// <param name="binWidth">
        /// Ширина бина.
        /// </param>
        /// <param name="maxValue">
        /// Верхняя граница.
        /// </param>
        public Distribution1D(IEnumerable<double> data, double minValue, double binWidth, double maxValue)
        {
            double[] bounds = Arrays.LinSpace(minValue, binWidth, maxValue);

            int bns = bounds.Length - 1; // Число бинов.

            double[] qnt = new double[bns]; // Количества попаданий в бины.

            double n = 0.0; // Объём выборки.
            int k;
            foreach (double value in data)
            {
                k = Arrays.IntervalNumber(bounds, value);
                if (k >= 0)
                {
                    qnt[k] += 1.0;
                    ++n;
                }
            }

            // Подсчитываем относительные частоты попадания в бины...
            double[] freqs = new double[bns];
            for (int i = 0; i < bns; i++)
            {
                freqs[i] = qnt[i] / n;
            }
            // ...подсчитали относительные частоты попадания в бины.

            Volume = n;
            Bins = bns;
            BinBounds = bounds;
            Frequencies = freqs;
        }

        /// <summary>
        /// Конструктор по значениям случайной величины и ширине бина.
        /// </summary>
        /// <param name="data">
        /// Коллекция значений случайной величины.
        /// </param>
        /// <param name="binWidth">
        /// Ширина бина.
        /// </param>
        public Distribution1D(IEnumerable<double> data, double binWidth)
        {
            double minValue = data.Min();
            double maxValue = data.Max();

            double[] bounds = Arrays.LinSpace(minValue, binWidth, maxValue);

            int bns = bounds.Length - 1; // Число бинов.

            double[] qnt = new double[bns]; // Количества попаданий в бины.

            double n = 0.0; // Объём выборки.
            int k;
            foreach (double value in data)
            {
                ++n;

                k = Arrays.IntervalNumber(bounds, value); // Проверка условия (k>=0) не нужна.
                qnt[k] += 1.0;                
            }

            // Подсчитываем относительные частоты попадания в бины...
            double[] freqs = new double[bns];
            for (int i = 0; i < bns; i++)
            {
                freqs[i] = qnt[i] / n;
            }
            // ...подсчитали относительные частоты попадания в бины.

            Volume = n;
            Bins = bns;
            BinBounds = bounds;
            Frequencies = freqs;
        }

        /// <summary>
        /// Корректировка объекта по дополнительным данным в выборку.
        /// </summary>
        /// <param name="addData">
        /// Коллекция дополнительных данных. Значения, не попадающие ни в один из бинов, игнорируются.
        /// </param>
        public void AddData(IEnumerable<double> addData)
        {
            double oldVolume = Volume;
            double addVolume = 0.0;

            double[] qnt = new double[Bins]; // Количества попаданий в бины допДанных.

            int k;
            foreach (double value in addData)
            {
                k = Arrays.IntervalNumber(BinBounds, value);
                if (k >= 0)
                {
                    qnt[k] += 1.0;
                    ++addVolume;
                }
            }

            if (addVolume == 0.0)
            {
                return; // На случай отсутствия дополнительных данных в заданных пределах.
            }

            double totalVolume = oldVolume + addVolume;

            double[] freqs = new double[Bins];

            double denominator = 1.0;
            if (oldVolume > 0.0)
            {
                denominator += addVolume / oldVolume;
            }
            for (int i = 0; i < Bins; i++)
            {
                if (oldVolume == 0.0)
                {
                    freqs[i] = qnt[i] / addVolume;
                }
                else
                {
                    freqs[i] = (Frequencies[i] + qnt[i] / oldVolume) / denominator;
                }                
            }

            Volume = totalVolume;
            Frequencies = freqs;
        }

        /// <summary>
        /// Корректировка объекта по дополнительным данным в выборку.
        /// </summary>
        /// <param name="addData">
        /// Коллекция дополнительных данных. Значения, не попадающие ни в один из бинов, учитываются.
        /// </param>
        /// <param name="addWidth">
        /// Ширина дополнительных бинов (для значений, не попадающих ни в один из имеющихся бинов).
        /// </param>
        public void AddData(IEnumerable<double> addData, double addWidth)
        {
            addWidth = Math.Abs(addWidth);

            double addDataMax = addData.Max();
            double addDataMin = addData.Min();

            double lowerBound = BinBounds.Min();
            double upperBound = BinBounds.Max();
            
            if (addDataMax > upperBound && addWidth > Double.Epsilon)
            {                
                int nU = (int)Math.Ceiling((addDataMax - upperBound) / addWidth);

                double[] addBinBoundsRight = new double[nU];

                for (int i = 0; i < nU; i++)
                {
                    addBinBoundsRight[i] = upperBound + (i+1) * addWidth;
                }

                BinBounds = Arrays.Concatenation(BinBounds, addBinBoundsRight);
                Frequencies = Arrays.Concatenation(Frequencies, new double[nU]);
                Bins += nU;
            }

            if (addDataMin < lowerBound && addWidth > Double.Epsilon)
            {
                int nL = (int)Math.Ceiling((lowerBound - addDataMin) / addWidth);

                double[] addBinBoundsLeft = new double[nL];

                for (int i = 0; i < nL; i++)
                {
                    addBinBoundsLeft[i] = lowerBound - (nL - i) * addWidth;
                }

                BinBounds = Arrays.Concatenation(addBinBoundsLeft, BinBounds);
                Frequencies = Arrays.Concatenation(new double[nL], Frequencies);
                Bins += nL;
            }

            AddData(addData);
        }

        /// <summary>
        /// Вычисляет правый квантиль.
        /// </summary>
        /// <param name="level">
        /// Уровень квантиля (вероятность непревышения), от 0 до 1.
        /// </param>
        /// <returns>
        /// Значение правого квантиля.
        /// </returns>
        public double RightQuantile(double level)
        {
            double q = BinBounds[Bins];
            double p = 1.0;            

            if (level >= 1.0 || level < 0.0)
            {
                return q;
            }

            int i = Bins - 1;
            while (p > level)
            {
                p -= Frequencies[i];
                i--;
            }

            double f = Frequencies[i + 1];
            double nearestBin = BinBounds[i + 1]; // Ближайший бин слева.
            double width = BinBounds[i + 2] - nearestBin; // Ширина бина.
            double dx; 
            
            if (f == 0.0)
            {
                dx = 0.0;
            }
            else
            {
                dx = (level - p) * width / f;
            }
            
            q = nearestBin + dx;

            return q;
        }

        /// <summary>
        /// Вычисляет левый квантиль.
        /// </summary>
        /// <param name="level">
        /// Уровень квантиля (вероятность превышения), от 0 до 1.
        /// </param>
        /// <returns>
        /// Значение левого квантиля.
        /// </returns>
        public double LeftQuantile(double level)
        {
            level = 1.0 - level;

            double q = BinBounds[0];
            double p = 0.0;

            if (level > 1.0 || level <= 0.0)
            {
                return q;
            }

            int i = 0;
            while (p < level)
            {
                p += Frequencies[i];
                i++;
            }

            double f = Frequencies[i - 1];
            double nearestBin = BinBounds[i]; // Ближайший бин справа.
            double width = nearestBin - BinBounds[i - 1]; // Ширина бина.
            double dx;

            if (f == 0.0)
            {
                dx = 0.0;
            }
            else
            {
                dx = (p - level) * width / f;
            }

            q = nearestBin - dx;

            return q;
        }

        /// <summary>
        /// Матожидание.
        /// </summary>
        /// <returns>
        /// Оценка матожидания.
        /// </returns>
        public double Mean()
        {
            double m = 0.0; // Возвращаемое значение (оценка матожидания).

            double b0 = BinBounds[0];
            double b1;
            for (int i = 0; i < Bins; i++)
            {
                b1 = BinBounds[i + 1];
                m += 0.5 * (b0 + b1) * Frequencies[i];

                b0 = b1;
            }

            return m;
        }

        /// <summary>
        /// Дисперсия.
        /// </summary>
        /// <returns>
        /// Оценка дисперсии.
        /// </returns>
        public double Variance()
        {
            double m = 0.0; // Оценка матожидания случайной величины.
            double mSquare = 0.0; // // Оценка матожидания квадрата случайной величины.

            double b0 = BinBounds[0];
            double b1; double x;
            for (int i = 0; i < Bins; i++)
            {
                b1 = BinBounds[i + 1];
                x = 0.5 * (b0 + b1);
                m += x * Frequencies[i];
                mSquare += x * x * Frequencies[i];

                b0 = b1;
            }

            return mSquare - m * m;
        }

        /// <summary>
        /// Дисперсия положительной части данных.
        /// </summary>
        /// <returns>
        /// Оценка дисперсии.
        /// </returns>
        public double VariancePositive()
        {
            double m = 0.0; // Оценка матожидания случайной величины.
            double mSquare = 0.0; // // Оценка матожидания квадрата случайной величины.

            int i0 = Arrays.FindFirstIndex(BinBounds, x => x >= 0.0);
                        
            double b0 = BinBounds[i0];
            double b1; double x;
            double s = 0.0;
            double currentFreq;
            for (int i = i0; i < Bins; i++)
            {
                currentFreq = Frequencies[i];

                b1 = BinBounds[i + 1];
                x = 0.5 * (b0 + b1);
                m += x * currentFreq;
                mSquare += x * x * currentFreq;

                b0 = b1;

                s += currentFreq;
            }

            return (mSquare - m * m / s) / s;
        }

        /// <summary>
        /// Дисперсия отрицательной части данных.
        /// </summary>
        /// <returns>
        /// Оценка дисперсии.
        /// </returns>
        public double VarianceNegative()
        {
            double m = 0.0; // Оценка матожидания случайной величины.
            double mSquare = 0.0; // // Оценка матожидания квадрата случайной величины.

            int i0 = Arrays.FindLastIndex(BinBounds, x => x <= 0.0);
                        
            double b0 = BinBounds[0];
            double b1; double x;
            double s = 0.0;
            double currentFreq;
            for (int i = 0; i < i0; i++)
            {
                currentFreq = Frequencies[i];

                b1 = BinBounds[i + 1];
                x = 0.5 * (b0 + b1);
                m += x * currentFreq;
                mSquare += x * x * currentFreq;

                b0 = b1;

                s += currentFreq;
            }

            return (mSquare - m * m / s) / s;
        }

        /// <summary>
        /// Дисперсия.
        /// </summary>
        /// <param name="m">
        /// Оценка матожидания.
        /// </param>
        /// <returns>
        /// Оценка дисперсии.
        /// </returns>
        public double Variance(double m)
        {
            double mSquare = 0.0; // // Оценка матожидания квадрата случайной величины.

            double b0 = BinBounds[0];
            double b1; double x;
            for (int i = 0; i < Bins; i++)
            {
                b1 = BinBounds[i + 1];
                x = 0.5 * (b0 + b1);                
                mSquare += x * x * Frequencies[i];

                b0 = b1;
            }

            return mSquare - m * m;
        }

        /// <summary>
        /// Матожидание и дисперсия.
        /// </summary>
        /// <returns>
        /// Оценка матожидания (data[0]) и оценка дисперсии (data[1]).
        /// </returns>
        public double[] MeanAndVariance()
        {
            double m = 0.0; // Оценка матожидания случайной величины.
            double mSquare = 0.0; // // Оценка матожидания квадрата случайной величины.

            double b0 = BinBounds[0];
            double b1; double x;
            for (int i = 0; i < Bins; i++)
            {
                b1 = BinBounds[i + 1];
                x = 0.5 * (b0 + b1);
                m += x * Frequencies[i];
                mSquare += x * x * Frequencies[i];

                b0 = b1;
            }

            double[] data = new double[2];

            data[0] = m;
            data[1] = mSquare - m * m;

            return data;
        }
    }
}
