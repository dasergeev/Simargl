//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет собой встроенный электронный пьезоэлектрический датчик.
//    /// </summary>
//    public class HbmIepeSensor : HbmSensor
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        internal HbmIepeSensor(global::Hbm.Api.SensorDB.Entities.Sensors.IepeSensor target) :
//            base(target)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.SensorDB.Entities.Sensors.IepeSensor Target { get; }
//    }
//}
