using System.Collections.Generic;
using System.Linq;

namespace Simargl.Mathematics
{
    /// <summary>
    /// Структура, содержащая информацию о статистических параметрах выборки случайной величины.
    /// </summary>
    public struct EmpiricalStatistics
    {
        /// <summary>
        /// Поле для хранения объёма выборки.
        /// </summary>
        public double Volume { get; private set; }

        /// <summary>
        /// Поле для хранения наименьшего значения выборки.
        /// </summary>
        public double Min { get; private set; }

        /// <summary>
        /// Поле для хранения среднего значения выборки.
        /// </summary>
        public double Mean { get; private set; }

        /// <summary>
        /// Поле для хранения наибольшего значения выборки.
        /// </summary>
        public double Max { get; private set; }

        /// <summary>
        /// Поле для хранения дисперсии выборки.
        /// </summary>
        public double Variance { get; private set; }

        /// <summary>
        /// Поле для хранения количества бинов.
        /// </summary>
        public int Bins { get; private set; }

        /// <summary>
        /// Поле для хранения границ бинов (длина BinBounds > 2).
        /// </summary>
        public double[] BinBounds { get; private set; }

        /// <summary>
        /// Поле для хранения частот попаданий в бины.
        /// </summary>
        public double[] Frequencies { get; private set; }

        /// <summary>
        /// Конструктор по границам бинов.
        /// </summary>
        /// <param name="binBounds">
        /// Массив границ бинов.
        /// </param>             
        public EmpiricalStatistics(double[] binBounds)
        {
            int bns = binBounds.Length - 1; // Число бинов.            
            
            Volume = 0.0;
            Min = 0.0;
            Mean = 0.0;
            Max = 0.0;
            Variance = 0.0;
            Bins = bns;
            BinBounds = binBounds;
            Frequencies = new double[bns];
        }

        /// <summary>
        /// Конструктор по границам бинов и значениям случайной величины.
        /// </summary>
        /// <param name="binBounds">
        /// Массив границ бинов.
        /// </param>
        /// <param name="data">
        /// Коллекция значений случайной величины.
        /// </param>        
        public EmpiricalStatistics(double[] binBounds, IEnumerable<double> data)
        {
            int bns = binBounds.Length - 1; // Число бинов.            

            double[] qnt = new double[bns]; // Количества попаданий в бины.                     

            double n = 0.0;
            double s = 0.0;
            double s2 = 0.0;

            double min = data.ElementAt(0);
            double max = min;

            double leftPoint; double rightPoint;
            foreach (double value in data)
            {
                ++n;
                s += value;
                s2 += value * value;

                if (value < min) { min = value; }
                if (value > max) { max = value; }

                for (int k = 0; k < bns; k++)
                {
                    leftPoint = binBounds[k];
                    rightPoint = binBounds[k + 1];
                    if ((value >= leftPoint) && (value < rightPoint))
                    {
                        qnt[k] += 1;
                        break;
                    }
                }

                if (value == binBounds[bns])
                {
                    qnt[bns - 1] += 1;
                }
            }

            Volume = n;
            Min = min;
            Mean = s / n;
            Max = max;
            Variance = (s2 - s * s / n) / (n - 1);
            Bins = bns;
            BinBounds = binBounds;

            // Подсчитываем относительные частоты попадания в бины...
            double[] freqs = new double[bns];
            for (int i = 0; i < bns; i++)
            {
                freqs[i] = qnt[i] / n;
            }
            // ...подсчитали относительные частоты попадания в бины.

            Frequencies = freqs;
        }

        /// <summary>
        /// Корректировка объекта по дополнительным данным в выборку.
        /// </summary>
        /// <param name="addData">
        /// Коллекция дополнительных данных.
        /// </param>        
        public void AddData(IEnumerable<double> addData)
        {            
            double oldVolume = Volume;
            double addVolume = 0.0;

            double min = Min;

            double oldMean = Mean;
            double addMean;

            double max = Max;

            double oldVariance = Variance;
            double addVariance;

            double s = 0.0;
            double s2 = 0.0;

            double[] qnt = new double[Bins]; // Количества попаданий в бины допДанных.

            int k;

            foreach (double value in addData)
            {
                ++addVolume;

                if (value < min) { min = value; }
                if (value > max) { max = value; }

                s += value;
                s2 += value * value;

                k = Arrays.IntervalNumber(BinBounds, value);
                if (k >= 0)
                {
                    qnt[k] += 1.0;
                }
            }

            addMean = s / addVolume;
            addVariance = (s2 - s * s / addVolume) / (addVolume - 1);

            double totalVolume = oldVolume + addVolume;

            Volume = totalVolume;

            Min = min;

            Mean = oldMean * oldVolume/totalVolume + addMean * addVolume/totalVolume;

            Max = max;

            Variance = ( oldVariance * (oldVolume - 1) + addVariance * (addVolume - 1) + 
                oldVolume * (Mean - oldMean) + addVolume * (Mean - addMean) ) / (totalVolume - 1);
                                    
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
    }    
}
