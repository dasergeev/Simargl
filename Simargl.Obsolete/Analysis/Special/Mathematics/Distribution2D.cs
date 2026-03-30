namespace Simargl.Mathematics
{
    /// <summary>
    /// Содержит информацию о распределении 2-мерной случайной величины (частоты попаданий в бины).
    /// </summary>
    public class Distribution2D
    {
        /// <summary>
        /// Поле для хранения объёма выборки.
        /// </summary>
        public double Volume { get; private set; }

        /// <summary>
        /// Поле для хранения количества проекций бинов на 1-ю ось.
        /// </summary>
        public int Bins1 { get; private set; }

        /// <summary>
        /// Поле для хранения количества проекций бинов на 2-ю ось.
        /// </summary>
        public int Bins2 { get; private set; }

        /// <summary>
        /// Поле для хранения границ проекций бинов на 1-ю ось (длина BinBounds > 2).
        /// </summary>
        public double[] BinBounds1 { get; private set; }

        /// <summary>
        /// Поле для хранения границ проекций бинов на 2-ю ось (длина BinBounds > 2).
        /// </summary>
        public double[] BinBounds2 { get; private set; }

        /// <summary>
        /// Поле для хранения частот попаданий в бины.
        /// </summary>
        public double[,] Frequencies { get; private set; }

        /// <summary>
        /// Конструктор по границам бинов.
        /// </summary>
        /// <param name="bounds1">
        /// Границы проекций бинов на 1-ю ось (длина bounds1 > 2).
        /// </param>
        /// <param name="bounds2">
        /// Границы проекций бинов на 2-ю ось (длина bounds2 > 2).
        /// </param>        
        public Distribution2D(double[] bounds1, double[] bounds2)
        {
            int bns1 = bounds1.Length - 1;
            int bns2 = bounds2.Length - 1;
            
            // Относительные частоты попадания в бины...
            double[,] freqs = new double[bns1, bns2];
            for (int i1 = 0; i1 < bns1; i1++)
            {
                for (int i2 = 0; i2 < bns2; i2++)
                {
                    freqs[i1, i2] = 0.0;
                }
            }
            // ...относительные частоты попадания в бины.

            Volume = 0.0;
            Bins1 = bns1;
            Bins2 = bns2;
            BinBounds1 = bounds1;
            BinBounds2 = bounds2;
            Frequencies = freqs;
        }

        /// <summary>
        /// Конструктор по границам бинов и значениям случайных величин.
        /// </summary>
        /// <param name="bounds1">
        /// Границы проекций бинов на 1-ю ось (длина bounds1 > 2).
        /// </param>
        /// <param name="bounds2">
        /// Границы проекций бинов на 2-ю ось (длина bounds2 > 2).
        /// </param>
        /// <param name="data1">
        /// Коллекция значений случайной величины.
        /// </param>
        /// <param name="data2">
        /// Коллекция значений случайной величины.
        /// </param>
        public Distribution2D(double[] bounds1, double[] bounds2,
                              double[] data1, double[] data2)
        {
            int bns1 = bounds1.Length - 1;
            int bns2 = bounds2.Length - 1;

            double[,] qnt = new double[bns1, bns2]; // Количества попаданий в бины.

            int nSites = data1.Length;
            double value1; double value2;            
            int index1; int index2;

             // Цикл по парам значений...
            for (int i = 0; i < nSites; i++)
            {
               value1 = data1[i]; value2 = data2[i]; // текущая пара значений.
               index1 = Arrays.IntervalNumber(bounds1, value1);
               index2 = Arrays.IntervalNumber(bounds2, value2);

                if (index1 >= 0 && index2 >= 0)
                {
                    qnt[index1, index2] += 1.0;
                }                               
            }//...цикл по парам значений.

            // Подсчитываем относительные частоты попадания в бины...
            double[,] freqs = new double[bns1, bns2];
            for (int i1 = 0; i1 < bns1; i1++)
            {
                for (int i2 = 0; i2 < bns2; i2++)
                {
                    freqs[i1, i2] = qnt[i1, i2] / nSites;
                }
            }
            // ...подсчитали относительные частоты попадания в бины.

            Volume = nSites;
            Bins1 = bns1;
            Bins2 = bns2;
            BinBounds1 = bounds1;
            BinBounds2 = bounds2;
            Frequencies = freqs;
        }

        /// <summary>
        /// Корректировка объекта по дополнительным данным в выборку.
        /// </summary>
        /// <param name="addData1">
        /// Коллекция дополнительных данных.
        /// </param>
        /// <param name="addData2">
        /// Коллекция дополнительных данных.
        /// </param>
        public void AddData(double[] addData1, double[] addData2)
        {
            int nSites = addData1.Length;
            double oldVolume = Volume;
            double addVolume = nSites;

            double[,] qnt = new double[Bins1, Bins2]; // Количества попаданий в бины допДанных.
                        
            double value1; double value2;
            int index1; int index2;

            // Цикл по парам значений...
            for (int i = 0; i < nSites; i++)
            {
                value1 = addData1[i]; value2 = addData2[i]; // текущая пара значений.
                index1 = Arrays.IntervalNumber(BinBounds1, value1);
                index2 = Arrays.IntervalNumber(BinBounds2, value2);

                if (index1 >= 0 && index2 >= 0)
                {
                    qnt[index1, index2] += 1.0;
                }
            }//...цикл по парам значений.

            double totalVolume = oldVolume + addVolume;

            double[,] freqs = new double[Bins1, Bins2];
            double denominator = 1.0;
            if (oldVolume > 0.0)
            {
                denominator += addVolume / oldVolume;
            }
            for (int i1 = 0; i1 < Bins1; i1++)
            {
                for (int i2 = 0; i2 < Bins2; i2++)
                {
                    if (oldVolume == 0.0)
                    {
                        freqs[i1, i2] = qnt[i1, i2] / addVolume;
                    }
                    else
                    {
                        freqs[i1, i2] = (Frequencies[i1, i2] + qnt[i1, i2] / oldVolume) / denominator;
                    }                    
                }
            }

            Volume = totalVolume;
            Frequencies = freqs;
        }
    }
}
