//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет базовый класс для всех объектов, реализующих потоковую передачу данных.
//    /// </summary>
//    public abstract class HbmStreamingDevice : HbmDevice
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <param name="count">
//        /// Количество сигналов.
//        /// </param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="count"/> передано отрицательное или равное нулю значение.
//        /// </exception>
//        internal HbmStreamingDevice(global::Hbm.Api.Common.Entities.StreamingDevice target, int count) :
//            base(target, count)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.Common.Entities.StreamingDevice Target { get; }
//    }
//}
