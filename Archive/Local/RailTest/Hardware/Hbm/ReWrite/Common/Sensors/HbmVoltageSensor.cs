//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет собой датчик напряжения.
//    /// </summary>
//    public class HbmVoltageSensor : HbmSensor
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        internal HbmVoltageSensor(global::Hbm.Api.SensorDB.Entities.Sensors.VoltageSensor target) :
//            base(target)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.SensorDB.Entities.Sensors.VoltageSensor Target { get; }
//    }
//}
