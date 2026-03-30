using RailTest.Algebra;
using RailTest.Frames;
using System;

namespace RailTest.Analysis
{
    /// <summary>
    /// Предоставляет методы спектральной обраюотки.
    /// </summary>
    public static class Spectral
    {
        /// <summary>
        /// Возвращает эффективную частоту процесса.
        /// </summary>
        /// <param name="channel">
        /// Канал, содержащий процесс.
        /// </param>
        /// <param name="beginFrequency">
        /// Начальная частота.
        /// </param>
        /// <param name="endFrequency">
        /// Конечная частота.
        /// Если частота не ограничена сверху передать <see cref="double.PositiveInfinity"/>.
        /// </param>
        /// <param name="blockCount">
        /// Количество блоков, на которые разбивается процесс при вычислении спектральной плотности.
        /// </param>
        /// <returns>
        /// Эффективная частота.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="channel"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="blockCount"/> передано отрицательное или равное нулю значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="channel"/> передан канал, длина которого меньше количества блоков.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="beginFrequency"/> передано нечисловое значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="beginFrequency"/> передано бесконечное значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="endFrequency"/> передано нечисловое значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="endFrequency"/> передано отрицательное бесконечное значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="endFrequency"/> передано значение меньшее или равное значению <paramref name="beginFrequency"/>.
        /// </exception>
        public static double EffectiveFrequency(Channel channel, double beginFrequency, double endFrequency, int blockCount)
        {
            //  Проверка начальной частоты.
            if (double.IsNaN(beginFrequency))
            {
                throw new ArgumentOutOfRangeException(nameof(beginFrequency), "Передано нечисловое значение.");
            }
            if (double.IsInfinity(beginFrequency))
            {
                throw new ArgumentOutOfRangeException(nameof(beginFrequency), "Передано бесконечное значение.");
            }

            //  Проверка конечной частоты.
            if (double.IsNaN(endFrequency))
            {
                throw new ArgumentOutOfRangeException(nameof(endFrequency), "Передано нечисловое значение.");
            }
            if (double.IsNegativeInfinity(endFrequency))
            {
                throw new ArgumentOutOfRangeException(nameof(endFrequency), "Передано отрицательное бесконечное значение.");
            }

            //  Получение нормализованной спектральной плотности.
            var density = NormalizedSpectralDensity(channel, blockCount);

            //  Частота дискретизации спектральной плотности.
            var sampling = density.Sampling;

            //  Длина процесса спектральной плотности.
            var length = density.Length;

            //  Вычисление начального индекса интегрирования.
            int beginIndex = (int)Math.Round(beginFrequency / sampling);

            //  Вычисление конечного индекса интегрирования.
            int endIndex = double.IsPositiveInfinity(endFrequency) ? length - 1 : (int)Math.Round(endFrequency / sampling);

            //  Нормализация индесков.
            if (beginIndex < 0) beginIndex = 0;
            if (beginIndex > length - 1) beginIndex = length - 1;
            if (endIndex < 0) endIndex = 0;
            if (endIndex > length - 1) endIndex = length - 1;

            //  Проверка индексов.
            if (beginIndex >= endIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(endFrequency), "Конечный индекс меньше или равен начальному.");
            }

            //  Возврат эффективной частоты процесса.
            return Math.Sqrt(Integral.Trapezium((int index) => (index * index) * density[index], beginIndex, endIndex, 1 / sampling) * sampling * sampling);
        }

        /// <summary>
        /// Возвращает нормализованную спектральную плотность процесса.
        /// </summary>
        /// <param name="channel">
        /// Канал, содержащий процесс.
        /// </param>
        /// <param name="blockCount">
        /// Количество блоков, на которые разбивается процесс при вычислении спектральной плотности.
        /// </param>
        /// <returns>
        /// Спектральная плотность процесса.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="channel"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="blockCount"/> передано отрицательное или равное нулю значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="channel"/> передан канал, длина которого меньше количества блоков.
        /// </exception>
        public static Channel NormalizedSpectralDensity(Channel channel, int blockCount)
        {
            //  Получение спектральной плотности.
            var density = SpectralDensity(channel, blockCount);

            //  Вычисление дисперсии.
            var dispersion = Integral.Trapezium((int index) => density[index], 0, density.Length - 1, 1 / density.Sampling);

            //  Проверка дисперсии.
            if (dispersion != 0)
            {
                //  Нормирование.
                density.Scale(1 / dispersion);
            }

            //  Возврат нормализованной спектральной плотности.
            return density;
        }

        /// <summary>
        /// Возвращает спектральную плотность процесса.
        /// </summary>
        /// <param name="channel">
        /// Канал, содержащий процесс.
        /// </param>
        /// <param name="blockCount">
        /// Количество блоков, на которые разбивается процесс при вычислении спектральной плотности.
        /// </param>
        /// <returns>
        /// Спектральная плотность процесса.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="channel"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="blockCount"/> передано отрицательное или равное нулю значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="channel"/> передан канал, длина которого меньше количества блоков.
        /// </exception>
        public static Channel SpectralDensity(Channel channel, int blockCount)
        {
            //  Проверка ссылки на канал.
            if (channel is null)
            {
                throw new ArgumentNullException(nameof(channel), "Передана пустая ссылка.");
            }

            //  Проверка количества блоков.
            if (blockCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(blockCount), "Передано отрицательное или равное нулю значение.");
            }

            //  Длина канала.
            var length = channel.Length;

            //  Проверка длины канала.
            if (length < blockCount)
            {
                throw new ArgumentOutOfRangeException(nameof(channel), "Передан канал, длина которого меньше количества блоков.");
            }

            //  Длина блока.
            var blockLength = BaseTwo(length / blockCount);

            //  Уточнение количества блоков.
            blockCount = length / blockLength;

            //  Создание канала со спектральной плотности.
            var density = SpectralDensityBaseTwo(channel.GetSubChannel(0, blockLength));

            //  Проверка количества блоков.
            if (blockCount > 1)
            {
                //  Длина канала со спектральной плотности.
                var responseLength = density.Length;

                //  Цикл по оставшимся блокам.
                for (int i = 1; i != blockCount; ++i)
                {
                    //  Получение добавочного канала со спектральной плотности.
                    var extraDensity = SpectralDensityBaseTwo(channel.GetSubChannel(i * blockLength, blockLength));

                    //  Добаление значений дополнительного канала.
                    for (int j = 0; j != responseLength; ++j)
                    {
                        //  Добавление значений.
                        density[j] += extraDensity[j];
                    }
                }

                //  Усреднение значений.
                density.Scale(1.0 / blockCount);
            }

            //  Изменение частоты дискретизации.
            density.Resampling(Math.Ceiling(density.Sampling));

            //  Возврат канала со спектральной плотности.
            return density;
        }

        /// <summary>
        /// Возвращает амплитудно-частотную характеристику процесса.
        /// </summary>
        /// <param name="channel">
        /// Канал, содержащий процесс.
        /// </param>
        /// <param name="blockCount">
        /// Количество блоков, на которые разббивается процесс при вычислении АЧХ.
        /// </param>
        /// <returns>
        /// Амплитудно-частотная характеристика процесса.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="channel"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="blockCount"/> передано отрицательное или равное нулю значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="channel"/> передан канал, длина которого меньше количества блоков.
        /// </exception>
        public static Channel FrequencyResponse(Channel channel, int blockCount)
        {
            //  Проверка ссылки на канал.
            if (channel is null)
            {
                throw new ArgumentNullException(nameof(channel), "Передана пустая ссылка.");
            }

            //  Проверка количества блоков.
            if (blockCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(blockCount), "Передано отрицательное или равное нулю значение.");
            }

            //  Длина канала.
            var length = channel.Length;

            //  Проверка длины канала.
            if (length < blockCount)
            {
                throw new ArgumentOutOfRangeException(nameof(channel), "Передан канал, длина которого меньше количества блоков.");
            }

            //  Длина блока.
            var blockLength = BaseTwo(length / blockCount);

            //  Уточнение количества блоков.
            blockCount = length / blockLength;

            //  Создание АЧХ канала.
            var response = FrequencyResponseBaseTwo(channel.GetSubChannel(0, blockLength));

            //  Проверка количества блоков.
            if (blockCount > 1)
            {
                //  Длина АЧХ канала.
                var responseLength = response.Length;

                //  Цикл по оставшимся блокам.
                for (int i = 1; i != blockCount; ++i)
                {
                    //  Получение добавочного АЧХ канала.
                    var extraResponse = FrequencyResponseBaseTwo(channel.GetSubChannel(i * blockLength, blockLength));

                    //  Добаление значений дополнительного канала.
                    for (int j = 0; j != responseLength; ++j)
                    {
                        //  Добавление значений.
                        response[j] += extraResponse[j];
                    }
                }

                //  Усреднение значений.
                response.Scale(1.0 / blockCount);
            }

            //  Изменение частоты дискретизации.
            response.Resampling(Math.Ceiling(response.Sampling));

            //  Возврат АЧХ канала.
            return response;
        }

        /// <summary>
        /// Возвращает наибольшее целое число, являющееся степенью двух,
        /// которое меньше или равно заданному.
        /// </summary>
        /// <param name="value">
        /// Значение.
        /// </param>
        /// <returns>
        /// Результат вычислений.
        /// </returns>
        /// <remarks>
        /// В целях оптимизации метод не выполняет никаких проверок.
        /// Перед вызовом метода необходимо убедиться, что значение больше нуля.
        /// </remarks>
        public static int BaseTwo(int value)
        {
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            value -= (value >> 1);
            return value;
        }

        /// <summary>
        /// Возвращает амплитудно-частотную характеристику процесса, длина которого является степенью двух.
        /// </summary>
        /// <param name="channel">
        /// Канал, содержащий процесс.
        /// </param>
        /// <returns>
        /// Амплитудно-частотная характеристика процесса.
        /// </returns>
        /// <remarks>
        /// В целях оптимизации метод не выполняет никаких проверок.
        /// Перед вызовом метода необходимо убедиться, что ссылка на канал не пустая, длина канала является степенью двух.
        /// </remarks>
        static Channel FrequencyResponseBaseTwo(Channel channel)
        {
            //  Длина канала.
            var length = channel.Length;

            //  Количество частот.
            var count = (length >> 1) + 1;

            //  Определение комплексных амплитуд.
            var phasors = new ComplexVector(channel.Vector);
            phasors.FourierTransform();

            //  Создание АЧХ канала.
            var response = channel.GetSubChannel(0, count);

            //  Определение вещественных амплитуд.
            for (int i = 0; i != count; ++i)
            {
                //  Вещественная амплитуда.
                response[i] = 2.0 * phasors[i].Magnitude / length;
            }

            response[0] *= 0.5;

            //  Настройка свойств АЧХ канала.
            {
                //  Имя АЧХ процесса.
                response.Name += "FR";

                //  Размерность АЧХ процесса.
                response.Unit += "·s";

                //  Частота дискретизации АЧХ процесса.
                response.Sampling = length / channel.Sampling;
            }

            //  Возврат АЧХ канала.
            return response;
        }

        /// <summary>
        /// Возвращает спектральную плотность процесса, длина которого является степенью двух.
        /// </summary>
        /// <param name="channel">
        /// Канал, содержащий процесс.
        /// </param>
        /// <returns>
        /// Спектральная плотность процесса.
        /// </returns>
        /// <remarks>
        /// В целях оптимизации метод не выполняет никаких проверок.
        /// Перед вызовом метода необходимо убедиться, что ссылка на канал не пустая, длина канала является степенью двух.
        /// </remarks>
        static Channel SpectralDensityBaseTwo(Channel channel)
        {
            //  Получение АЧХ процесса.
            var density = FrequencyResponseBaseTwo(channel);

            //  Длина результирующего процесса.
            var length = density.Length;

            //  Получение спектральной плотности.
            for (int i = 0; i != length; ++i)
            {
                //  Получение текущего значения.
                var value = density[i];

                //  Установка нового значения.
                density[i] = value * value;
            }

            //  Настройка свойств канала спектральной плотности.
            {
                //  Имя процесса.
                density.Name = channel.Name + "Density";

                //  Размерность процесса.
                density.Unit = $"({density.Unit})^2";
            }

            //  Возврат спектральной плотности.
            return density;
        }
    }
}
