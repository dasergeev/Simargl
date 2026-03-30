using System;

namespace RailTest.Teamwork
{
    /// <summary>
    /// Представляет идентификатор процесса.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
    public struct ProcessID
    {
        /// <summary>
        /// Возвращает значение, которое используется для идентификации неизвестного процесса.
        /// </summary>
        public static ProcessID Unknown { get; }

        /// <summary>
        /// Инициализирует статические члены.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2207:InitializeValueTypeStaticFieldsInline")]
        static ProcessID()
        {
            Unknown = new ProcessID(0UL);
        }

        /// <summary>
        /// Поле для хранения значения идентификатора.
        /// </summary>
        private readonly ulong _Value;

        /// <summary>
        /// Инициализирует новый экземпляр.
        /// </summary>
        /// <param name="value">
        /// Значение идентификатора.
        /// </param>
        private ProcessID(ulong value)
        {
            _Value = value;
        }

        /// <summary>
        /// Инициализирует новый экземпляр.
        /// </summary>
        /// <param name="value">
        /// Значение идентификатора.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано отрицательное значение.
        /// </exception>
        public ProcessID(int value) :
            this(value >= 0 ? (ulong)value : throw new ArgumentOutOfRangeException("value", "Передано отрицательное значение."))
        {

        }

        /// <summary>
        /// Инициализирует новый экземпляр.
        /// </summary>
        /// <param name="value">
        /// Значение идентификатора.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="value"/> передано отрицательное значение.
        /// </exception>
        public ProcessID(long value) :
            this(value >= 0 ? (ulong)value : throw new ArgumentOutOfRangeException("value", "Передано отрицательное значение."))
        {

        }

        /// <summary>
        /// Выполняет операцию проверки на равенство.
        /// </summary>
        /// <param name="left">
        /// Левый операнд.
        /// </param>
        /// <param name="right">
        /// Правый операнд.
        /// </param>
        /// <returns>
        /// Результат операции.
        /// </returns>
        public static bool operator == (ProcessID left, ProcessID right)
        {
            return left._Value == right._Value;
        }

        /// <summary>
        /// Выполняет операцию проверки на неравенство.
        /// </summary>
        /// <param name="left">
        /// Левый операнд.
        /// </param>
        /// <param name="right">
        /// Правый операнд.
        /// </param>
        /// <returns>
        /// Результат операции.
        /// </returns>
        public static bool operator !=(ProcessID left, ProcessID right)
        {
            return left._Value != right._Value;
        }

        /// <summary>
        /// Указывает, равен ли этот экземпляр заданному объекту.
        /// </summary>
        /// <param name="obj">
        /// Объект для сравнения с текущим экземпляром.
        /// </param>
        /// <returns>
        /// Значение true, если <paramref name="obj"/> и данный экземпляр относятся к одному типу и представляют одинаковые значения;
        /// в противном случае - значение false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is ProcessID id)
            {
                return _Value == id._Value;
            }
            return false;
        }

        /// <summary>
        /// Возвращает хэш-код данного экземпляра.
        /// </summary>
        /// <returns>
        /// 32-разрядное целое число со знаком, являющееся хэш-кодом для данного экземпляра.
        /// </returns>
        public override int GetHashCode()
        {
            return _Value.GetHashCode();
        }

        /// <summary>
        /// Возвращает текстовое представление.
        /// </summary>
        /// <returns>
        /// Текстовое представление.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.UInt64.ToString")]
        public override string ToString()
        {
            return _Value.ToString();
        }
    }
}
