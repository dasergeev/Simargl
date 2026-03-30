//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет собой датчик волоконной брэгговской решетки.
//    /// </summary>
//    public abstract class HbmFbgSensor : HbmSensor
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        internal HbmFbgSensor(global::Hbm.Api.SensorDB.Entities.Sensors.FbgSensor target) :
//            base(target)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.SensorDB.Entities.Sensors.FbgSensor Target { get; }
//    }
//}
