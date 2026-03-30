//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет разъем, к которому можно прикрепить датчик.
//    /// </summary>
//    public class HbmPmxAnalogInConnector : HbmAnalogInConnector
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <param name="device">
//        /// Устройство, которому принадлежит разъём.
//        /// </param>
//        internal HbmPmxAnalogInConnector(global::Hbm.Api.Pmx.Connectors.PmxAnalogInConnector target, HbmDevice device) :
//            base(target, device)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.Pmx.Connectors.PmxAnalogInConnector Target { get; }
//    }
//}
