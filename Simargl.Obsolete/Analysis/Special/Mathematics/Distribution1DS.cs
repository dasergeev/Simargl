using System;
using System.Collections.Generic;
using System.Linq;

namespace Simargl.Mathematics
{

    /// <summary>
    /// Содержит информацию о распределении случайной величины и числу воздействий на 1000 км.
    /// </summary>
    public class Distribution1DS : Distribution1D
    {
        /// <summary>
        /// Пройденный путь, км.
        /// </summary>
        public double TotalPath { get; private set; }

        /// <summary>
        /// Массив из количеств воздействия на 1000км.
        /// </summary>
        public double[] ImpactsNumbers { get; private set; }
                
        /// <summary>
        /// Конструктор по границам бинов.
        /// </summary>
        /// <param name="bounds">
        /// Массив границ бинов.
        /// </param>
        public Distribution1DS(double[] bounds) : base(bounds)
        {
            TotalPath = 0;
            ImpactsNumbers = new double[Frequencies.Length];
        }

        /// <summary>
        /// Конструктор по границам бинов, значениям случайной величины, массиву скоростей.
        /// </summary>
        /// <param name="bounds">
        /// Массив границ бинов.
        /// </param>
        /// <param name="data">
        /// Коллекция значений случайной величины. Значения, не попадающие ни в один из бинов, игнорируются.
        /// </param>
        /// <param name="speed">
        /// Массив значений скорости, км/ч.
        /// </param>
        /// <param name="sampleRate">
        /// Частота выборки для процесса "Скорость", Гц.
        /// </param>
        public Distribution1DS(double[] bounds, IEnumerable<double> data, 
            double[] speed, double sampleRate) : base(bounds, data)
        {                        
            double dt = (1 / sampleRate) / 3600; // шаг по времени, ч.
            TotalPath = Calculus.IntegralTrap(speed) * dt; // Путь, км.

            double[] impacts = new double[Bins]; // Массив из количеств воздействия на 1000км.

            // Подсчитываем количество воздействий на 1000км....
            double scale = 1000 / TotalPath;
            for (int i = 0; i < Bins; i++)
            {
                impacts[i] = scale * Frequencies[i] * Volume;                
            }
            // ...подсчитали количество воздействий на 1000км.
                        
            ImpactsNumbers = impacts;
        }

        /// <summary>
        /// Конструктор по значениям случайной величины, нижней и верхней границе, ширине бина, массиву скоростей.
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
        /// <param name="speed">
        /// Массив значений скорости, км/ч.
        /// </param>
        /// <param name="sampleRate">
        /// Частота выборки для процесса "Скорость", Гц.
        /// </param>
        public Distribution1DS(IEnumerable<double> data, double minValue, double binWidth, double maxValue,
            double[] speed, double sampleRate) : base(data, minValue, binWidth, maxValue)
        {
            double dt = (1 / sampleRate) / 3600; // шаг по времени, ч.
            TotalPath = Calculus.IntegralTrap(speed) * dt; // Путь, км.

            double[] impacts = new double[Bins]; // Массив из количеств воздействия на 1000км.

            // Подсчитываем количество воздействий на 1000км....
            double scale = 1000 / TotalPath;
            for (int i = 0; i < Bins; i++)
            {
                impacts[i] = scale * Frequencies[i] * Volume;
            }
            // ...подсчитали количество воздействий на 1000км.

            ImpactsNumbers = impacts;
        }

        /// <summary>
        /// Конструктор по значениям случайной величины и ширине бина, массиву скоростей.
        /// </summary>
        /// <param name="data">
        /// Коллекция значений случайной величины.
        /// </param>
        /// <param name="binWidth">
        /// Ширина бина.
        /// </param>
        /// <param name="speed">
        /// Массив значений скорости, км/ч.
        /// </param>
        /// <param name="sampleRate">
        /// Частота выборки для процесса "Скорость", Гц.
        /// </param>
        public Distribution1DS(IEnumerable<double> data, double binWidth,
            double[] speed, double sampleRate) : base(data, binWidth)
        {
            double dt = (1 / sampleRate) / 3600; // шаг по времени, ч.
            TotalPath = Calculus.IntegralTrap(speed) * dt; // Путь, км.

            double[] impacts = new double[Bins]; // Массив из количеств воздействия на 1000км.

            // Подсчитываем количество воздействий на 1000км....
            double scale = 1000 / TotalPath;
            for (int i = 0; i < Bins; i++)
            {
                impacts[i] = scale * Frequencies[i] * Volume;
            }
            // ...подсчитали количество воздействий на 1000км.

            ImpactsNumbers = impacts;
        }

        /// <summary>
        /// Корректировка объекта по дополнительным данным в выборку.
        /// </summary>
        /// <param name="addData">
        /// Коллекция дополнительных данных. Значения, не попадающие ни в один из бинов, игнорируются.
        /// </param>
        /// <param name="speed">
        /// Массив значений скорости, соответствующих дополнительным данным, км/ч.
        /// </param>
        /// <param name="sampleRate">
        /// Частота выборки для процесса "Скорость", Гц.
        /// </param>
        public void AddData(IEnumerable<double> addData, double[] speed, double sampleRate)
        {            
            double dt = (1 / sampleRate) / 3600; // шаг по времени, ч.

            // Пройденный путь для некорректированных данных, км:
            double s = TotalPath;

            // Приращение пройденного пути, км:
            double ds = Calculus.IntegralTrap(speed) * dt;

            // Корректировка пройденного пути, км:
            TotalPath += ds;

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

            double[] oldImpacts = ImpactsNumbers;

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

                if (TotalPath != 0)
                {
                    ImpactsNumbers[i] = (s * oldImpacts[i] + 1000 * qnt[i]) / TotalPath;
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
        /// <param name="speed">
        /// Массив значений скорости, соответствующих дополнительным данным, км/ч.
        /// </param>
        /// <param name="sampleRate">
        /// Частота выборки для процесса "Скорость", Гц.
        /// </param>
        public void AddData(IEnumerable<double> addData, double addWidth, double[] speed, double sampleRate)
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
                    addBinBoundsRight[i] = upperBound + (i + 1) * addWidth;
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

            AddData(addData, speed, sampleRate);
        }
    }    
}
