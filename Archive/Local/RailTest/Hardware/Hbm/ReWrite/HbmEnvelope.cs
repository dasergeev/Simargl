//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm.Core
//{
//    /// <summary>
//    /// Представляет универсальную оболочку вокруг объекта библиотеки <see cref="Hbm"/>.
//    /// </summary>
//    /// <typeparam name="T">
//    /// Тип объекта, вокруг которого создаётся оболочка.
//    /// </typeparam>
//    public class HbmEnvelope<T> : Envelopes.Envelope<T>
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="target"/> передана пустая ссылка.
//        /// </exception>
//        public HbmEnvelope(T target) :
//            base(target)
//        {

//        }
//    }
//}
