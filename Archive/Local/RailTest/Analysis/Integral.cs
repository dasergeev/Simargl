using System;
using System.Numerics;

namespace RailTest.Analysis
{
    /// <summary>
    /// Предоставляет методы для интегрирования.
    /// </summary>
    public static class Integral
    {
        /// <summary>
        /// Вычисляет определённый интеграл функции методом трапеции.
        /// </summary>
        /// <param name="func">
        /// Функция для интегрирования.
        /// </param>
        /// <param name="beginIndex">
        /// Начальный индекс.
        /// </param>
        /// <param name="endIndex">
        /// Конечный индекс.
        /// </param>
        /// <param name="increment">
        /// Приращение.
        /// </param>
        /// <returns>
        /// Результат интегрирования.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="func"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="endIndex"/> передано значение меньшее или равное значению <paramref name="beginIndex"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="increment"/> передано отрицательное или равное нулю значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="increment"/> передано нечисловое значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="increment"/> передано бесконечное значение.
        /// </exception>
        public static double Trapezium(Func<int, double> func, int beginIndex, int endIndex, double increment)
        {
            //  Проверка делегата.
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func), "Передана пустая ссылка.");
            }

            //  Проверка индексов.
            if (beginIndex >= endIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(endIndex), "Конечный индекс меньше или равен начальному.");
            }

            //  Проверка приращения.
            if (increment <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(increment), "Передано отрицательное или равное нулю значение.");
            }
            if (double.IsNaN(increment))
            {
                throw new ArgumentOutOfRangeException(nameof(increment), "Передано нечисловое значение.");
            }
            if (double.IsInfinity(increment))
            {
                throw new ArgumentOutOfRangeException(nameof(increment), "Передано бесконечное значение.");
            }

            double result = -0.5 * (func(beginIndex) + func(endIndex));
            for (int i = beginIndex; i <= endIndex; i++)
            {
                result += func(i);
            }

            return result * increment;
        }

        /// <summary>
        /// Вычисляет определённый интеграл функции методом трапеции.
        /// </summary>
        /// <param name="func">
        /// Функция для интегрирования.
        /// </param>
        /// <param name="beginIndex">
        /// Начальный индекс.
        /// </param>
        /// <param name="endIndex">
        /// Конечный индекс.
        /// </param>
        /// <param name="increment">
        /// Приращение.
        /// </param>
        /// <returns>
        /// Результат интегрирования.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="func"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="endIndex"/> передано значение меньшее или равное значению <paramref name="beginIndex"/>.
        /// </exception>
        public static Complex Trapezium(Func<int, Complex> func, int beginIndex, int endIndex, Complex increment)
        {
            //  Проверка делегата.
            if (func is null)
            {
                throw new ArgumentNullException(nameof(func), "Передана пустая ссылка.");
            }

            //  Проверка индексов.
            if (beginIndex >= endIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(endIndex), "Конечный индекс меньше или равен начальному.");
            }

            Complex result = -0.5 * (func(beginIndex) + func(endIndex));
            for (int i = beginIndex; i <= endIndex; i++)
            {
                result += func(i);
            }

            return result * increment;
        }
    }
}
