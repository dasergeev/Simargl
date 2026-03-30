using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Signals
{
    /// <summary>
    /// Представляет отметку времени.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
    public struct Timestamp
    {
        /// <summary>
        /// Возвращает минимальное значение.
        /// </summary>
        public static Timestamp MinValue { get; } = new Timestamp(0);

        /// <summary>
        /// Возвращает максимальное значение.
        /// </summary>
        public static Timestamp MaxValue { get; } = new Timestamp(long.MaxValue);

        /// <summary>
        /// Поле для хранения значения метки времени.
        /// </summary>
        private readonly long _Value;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="value">
        /// Значение метки времени.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано отрицательное значение.
        /// </exception>
        private Timestamp(long value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("value", "Передано отрицательное значение.");
            }
            _Value = value;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="value">
        /// Значение метки времени.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано значение большее <see cref="long.MaxValue"/>.
        /// </exception>
        private Timestamp(ulong value)
        {
            if (value > long.MaxValue)
            {
                throw new ArgumentOutOfRangeException("value", "Передано слишком большое значение.");
            }
            _Value = (long)value;
        }

        /// <summary>
        /// Выполняет преобразование в значение времени.
        /// </summary>
        /// <param name="timestamp">
        /// Отметка времени.
        /// </param>
        public static implicit operator DateTime(Timestamp timestamp)
        {
            return Convert(timestamp._Value);
        }

        /// <summary>
        /// Выполняет преобразование в отметку времени.
        /// </summary>
        /// <param name="value">
        /// Значение для реобразования.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано отрицательное значение.
        /// </exception>
        public static implicit operator Timestamp(DateTime value)
        {
            return new Timestamp(Convert(value));
        }

        /// <summary>
        /// Выполняет преобразование в числовое значение.
        /// </summary>
        /// <param name="timestamp">
        /// Отметка времени.
        /// </param>
        public static implicit operator long(Timestamp timestamp)
        {
            return timestamp._Value;
        }

        /// <summary>
        /// Выполняет преобразование в отметку времени.
        /// </summary>
        /// <param name="value">
        /// Значение для реобразования.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано отрицательное значение.
        /// </exception>
        public static implicit operator Timestamp(long value)
        {
            return new Timestamp(value);
        }

        /// <summary>
        /// Выполняет преобразование в отметку времени.
        /// </summary>
        /// <param name="value">
        /// Значение для реобразования.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано отрицательное значение.
        /// </exception>
        public static implicit operator Timestamp(int value)
        {
            return new Timestamp(value);
        }

        /// <summary>
        /// Выполняет преобразование в отметку времени.
        /// </summary>
        /// <param name="value">
        /// Значение для реобразования.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано отрицательное значение.
        /// </exception>
        public static implicit operator Timestamp(short value)
        {
            return new Timestamp(value);
        }

        /// <summary>
        /// Выполняет преобразование в отметку времени.
        /// </summary>
        /// <param name="value">
        /// Значение для реобразования.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано отрицательное значение.
        /// </exception>
        public static implicit operator Timestamp(byte value)
        {
            return new Timestamp(value);
        }

        /// <summary>
        /// Выполняет преобразование в отметку времени.
        /// </summary>
        /// <param name="value">
        /// Значение для реобразования.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано значение большее <see cref="long.MaxValue"/>.
        /// </exception>
        [CLSCompliant(false)]
        public static implicit operator Timestamp(ulong value)
        {
            return new Timestamp(value);
        }

        /// <summary>
        /// Выполняет преобразование в отметку времени.
        /// </summary>
        /// <param name="value">
        /// Значение для реобразования.
        /// </param>
        [CLSCompliant(false)]
        public static implicit operator Timestamp(uint value)
        {
            return new Timestamp(value);
        }

        /// <summary>
        /// Выполняет преобразование в отметку времени.
        /// </summary>
        /// <param name="value">
        /// Значение для реобразования.
        /// </param>
        [CLSCompliant(false)]
        public static implicit operator Timestamp(ushort value)
        {
            return new Timestamp(value);
        }

        /// <summary>
        /// Выполняет преобразование в отметку времени.
        /// </summary>
        /// <param name="value">
        /// Значение для реобразования.
        /// </param>
        [CLSCompliant(false)]
        public static implicit operator Timestamp(sbyte value)
        {
            return new Timestamp(value);
        }

        /// <summary>
        /// Выполняет преобразование из времени в значение отметки времени.
        /// </summary>
        /// <param name="time">
        /// Время.
        /// </param>
        /// <returns>
        /// Значение отметки времени.
        /// </returns>
        private static long Convert(DateTime time)
        {
            return time.Ticks;
        }

        /// <summary>
        /// Выполняет преобразование из значения отметки времени в значение времени.
        /// </summary>
        /// <param name="value">
        /// Значение отметки времени.
        /// </param>
        /// <returns>
        /// Время.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Значение меньше <see cref="DateTime.MinValue"/> или больше <see cref="DateTime.MaxValue"/>.
        /// </exception>
        private static DateTime Convert(long value)
        {
            return new DateTime(value);
        }

        /// <summary>
        /// Возвращает строковое представление.
        /// </summary>
        /// <returns>
        /// Метка времени.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Int64.ToString")]
        public override string ToString()
        {
            return _Value.ToString();
        }
    }
}
