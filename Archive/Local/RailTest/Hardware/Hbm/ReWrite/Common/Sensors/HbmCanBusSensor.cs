//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет собой датчик, подключенный по шине CAN (Controller Area Network).
//    /// </summary>
//    public class HbmCanBusSensor : HbmSensor
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        internal HbmCanBusSensor(global::Hbm.Api.SensorDB.Entities.Sensors.CanBusSensor target) :
//            base(target)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.SensorDB.Entities.Sensors.CanBusSensor Target { get; }
//    }
//}
