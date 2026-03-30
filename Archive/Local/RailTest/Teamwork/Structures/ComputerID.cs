using System;

namespace RailTest.Teamwork
{
    /// <summary>
    /// Представляет идентификатор компьютера.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
    public struct ComputerID
    {
        /// <summary>
        /// Возвращает значение, которое используется для идентификации неизвестного компьютера.
        /// </summary>
        public static ComputerID Unknown { get; }

        /// <summary>
        /// Возвращает идентификатор сервера.
        /// </summary>
        public static ComputerID Server { get; }

        /// <summary>
        /// Инициализирует статические члены.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2207:InitializeValueTypeStaticFieldsInline")]
        static ComputerID()
        {
            Unknown = new ComputerID(0UL);
            Server = new ComputerID(1UL);
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
        private ComputerID(ulong value)
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
        public ComputerID(int value) :
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
        public ComputerID(long value) :
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
        public static bool operator ==(ComputerID left, ComputerID right)
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
        public static bool operator !=(ComputerID left, ComputerID right)
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
            if (obj is ComputerID id)
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
