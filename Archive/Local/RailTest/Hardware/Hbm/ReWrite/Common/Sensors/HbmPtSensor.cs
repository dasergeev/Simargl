//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет собой платиновый датчик температуры.
//    /// </summary>
//    public class HbmPtSensor : HbmSensor
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        internal HbmPtSensor(global::Hbm.Api.SensorDB.Entities.Sensors.PtSensor target) :
//            base(target)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.SensorDB.Entities.Sensors.PtSensor Target { get; }
//    }
//}
