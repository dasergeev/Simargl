using System;
using System.Diagnostics.CodeAnalysis;

namespace RailTest.Support
{
    /// <summary>
    /// Предоставляет вспомогательные методы для проверки значений.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Выполняет проверку на принадлежность значения перечислению.
        /// </summary>
        /// <typeparam name="T">
        /// Тип перечисления.
        /// </typeparam>
        /// <param name="value">
        /// Значение, которое необходимо проверить.
        /// </param>
        /// <param name="paramName">
        /// Имя проверяемого параметра.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано значение, которое не содержится в перечислении <typeparamref name="T"/>.
        /// </exception>
        public static void CheckDefined<T>(T value, string paramName) where T : Enum
        {
            //  Проверка значения.
            if (!Enum.IsDefined(typeof(T), value))
            {
                throw new ArgumentOutOfRangeException(paramName, ExceptionMessages.NotContainedInEnumeration);
            }
        }

        /// <summary>
        /// Выполняет проверку индекса элемента.
        /// </summary>
        /// <param name="index">
        /// Индекс элемента.
        /// </param>
        /// <param name="count">
        /// Количество элементов в коллекции.
        /// </param>
        /// <param name="paramName">
        /// Имя проверяемого параметра.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="index"/> передано отрицательное значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="index"/> передано значение большее или равное <paramref name="count"/>.
        /// </exception>
        public static void CheckIndex(int index, int count, string paramName)
        {
            //  Проверка неотрицательности индекса.
            CheckNegative(index, nameof(index));

            //  Проверка превышения допустимого значения индекса.
            if (index >= count)
            {
                throw new ArgumentOutOfRangeException(paramName, ExceptionMessages.OutOfRange);
            }
        }

        /// <summary>
        /// Выполняет проверку на отрицательность.
        /// </summary>
        /// <param name="value">
        /// Значение, которое необходимо проверить.
        /// </param>
        /// <param name="paramName">
        /// Имя проверяемого параметра.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано отрицательное значение.
        /// </exception>
        public static void CheckNegative(int value, string paramName)
        {
            //  Проверка неотрицательности значения.
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, ExceptionMessages.NegativeValue);
            }
        }

        /// <summary>
        /// Выполняет проверку на отрицательность.
        /// </summary>
        /// <param name="value">
        /// Значение, которое необходимо проверить.
        /// </param>
        /// <param name="paramName">
        /// Имя проверяемого параметра.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано отрицательное значение.
        /// </exception>
        public static void CheckNegative(double value, string paramName)
        {
            //  Проверка неотрицательности значения.
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, ExceptionMessages.NegativeValue);
            }
        }

        /// <summary>
        /// Выполняет проверку на превышение максимального значения.
        /// </summary>
        /// <param name="value">
        /// Значение, которое необходимо проверить.
        /// </param>
        /// <param name="maxValue">
        /// Максимально допустимое значение.
        /// </param>
        /// <param name="paramName">
        /// Имя проверяемого параметра.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано значение большее <paramref name="maxValue"/>.
        /// </exception>
        public static void CheckStrictlyLarger(int value, int maxValue, string paramName)
        {
            //  Проверка превышения.
            if (value > maxValue)
            {
                throw new ArgumentOutOfRangeException(paramName, ExceptionMessages.OutOfRange);
            }
        }

        /// <summary>
        /// Выполняет проверку ссылки на объект.
        /// </summary>
        /// <param name="reference">
        /// Ссылка на объект.
        /// </param>
        /// <param name="paramName">
        /// Имя проверяемого параметра.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="reference"/> передана пустая ссылка.
        /// </exception>
        public static void CheckReference(object reference, string paramName)
        {
            //  Проверка ссылки на объект.
            if (reference is null)
            {
                throw new ArgumentNullException(paramName, ExceptionMessages.NullReference);
            }
        }

        /// <summary>
        /// Выполняет проверку длины строки.
        /// </summary>
        /// <param name="value">
        /// Значение, которое необходимо проверить.
        /// </param>
        /// <param name="maxLength">
        /// Максимальная длина строки.
        /// </param>
        /// <param name="paramName">
        /// Имя проверяемого параметра.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="value"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="maxLength"/> передано отрицательное значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передана строка, длина которой больше максимально допустимой.
        /// </exception>
        [SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы",
            Justification = "Ссылка на объект проверяется в дополнительном методе")]
        public static void CheckStringLength(string value, int maxLength, string paramName)
        {
            //  Проверка ссылки на объект.
            CheckReference(value, paramName);

            //  Проверка неотрицательности максимальной длины строки.
            CheckNegative(maxLength, nameof(maxLength));

            //  Проверка длины строки.
            if (value.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(paramName, ExceptionMessages.StringLargerTheMaximum);
            }
        }

        /// <summary>
        /// Проверяет, входит ли диапазон в массив.
        /// </summary>
        /// <typeparam name="T">
        /// Тип элементов массива.
        /// </typeparam>
        /// <param name="array">
        /// Массив, который необходимо проверить.
        /// </param>
        /// <param name="offset">
        /// Смещение в массиве <paramref name="array"/>, начиная с которого расположены целевые элементы.
        /// </param>
        /// <param name="count">
        /// Количество целевых элементов.
        /// </param>
        /// <param name="paramArrayName">
        /// Имя параметра, в котором передаётся массив.
        /// </param>
        /// <param name="paramOffsetName">
        /// Имя параметра, в котором передаётся смещение.
        /// </param>
        /// <param name="paramCountName">
        /// Имя параметра, в котором передаётся количество элементов.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="array"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="offset"/> передано отрицательное значение.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="count"/> передано отрицательное значение.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// В параметре <paramref name="array"/> передан массив,
        /// который не вмещает <paramref name="count"/> элементов начиная с позиции <paramref name="offset"/>.
        /// </exception>
        [SuppressMessage("Design", "CA1062:Проверить аргументы или открытые методы",
            Justification = "Ссылка проверяется в другом методе.")]
        public static void CheckArrayRange<T>(T[] array, int offset, int count,
            string paramArrayName, string paramOffsetName, string paramCountName)
        {
            //  Проверка ссылки на массив.
            CheckReference(array, paramArrayName);

            //  Проверка смещения в массиве.
            CheckNegative(offset, paramOffsetName);

            //  Проверка количества элементов, которые необходимо записать в текущий буфер.
            CheckNegative(count, paramCountName);

            //  Проверка длины буфера.
            if (offset + (long)count > array.LongLength)
            {
                throw new ArgumentException(ExceptionMessages.ArrayLessTheMinimum, nameof(array));
            }
        }
    }
}
