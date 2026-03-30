using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Teamwork
{
    /// <summary>
    /// Представляет идентификатор участника.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
    public struct PartnerID
    {
        /// <summary>
        /// Инициализирует новый экземпляр.
        /// </summary>
        /// <param name="computerID">
        /// Идентификатор компьютера.
        /// </param>
        /// <param name="processID">
        /// Идентификатор процесса.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
        public PartnerID(ComputerID computerID, ProcessID processID)
        {
            ComputerID = computerID;
            ProcessID = processID;
        }

        /// <summary>
        /// Возвращает идентификатор компьютера.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
        public ComputerID ComputerID { get; }

        /// <summary>
        /// Возвращает идентификатор процесса.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "ID")]
        public ProcessID ProcessID { get; }

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
        public static bool operator ==(PartnerID left, PartnerID right)
        {
            return left.ComputerID == right.ComputerID && left.ProcessID == right.ProcessID;
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
        public static bool operator !=(PartnerID left, PartnerID right)
        {
            return left.ComputerID != right.ComputerID || left.ProcessID != right.ProcessID;
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
            if (obj is PartnerID id)
            {
                return ComputerID == id.ComputerID && ProcessID == id.ProcessID; ;
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
            return ComputerID.GetHashCode();
        }

        /// <summary>
        /// Возвращает текстовое представление.
        /// </summary>
        /// <returns>
        /// Текстовое представление.
        /// </returns>
        public override string ToString()
        {
            StringBuilder text = new StringBuilder("[");
            text.Append(ComputerID.ToString());
            text.Append(": ");
            text.Append(ProcessID.ToString());
            text.Append("]");
            return text.ToString();
        }
    }
}
