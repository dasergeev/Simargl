//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware
//{
//    /// <summary>
//    /// Представляет аргументы для события, которое происходит при поступлении новых данных от устройства.
//    /// </summary>
//    public class ResponseEventArgsOld : EventArgs
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="values">
//        /// Коллекция значений, полученных от устройства.
//        /// </param>
//        /// <exception cref="ValueCollectionOld">
//        /// В параметре <paramref name="values"/> передана пустая ссылка.
//        /// </exception>
//        public ResponseEventArgsOld(ValueCollectionOld values)
//        {
//            Values = values ?? throw new ArgumentNullException("values", "Передана пустая ссылка.");
//        }

//        /// <summary>
//        /// Возвращает коллекцию значений, полученных от устройства.
//        /// </summary>
//        public ValueCollectionOld Values { get; }
//    }
//}
