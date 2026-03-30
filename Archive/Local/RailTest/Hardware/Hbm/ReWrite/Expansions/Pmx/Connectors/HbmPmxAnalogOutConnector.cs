//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Специфичная для Pmx реализация аналогового выходного разъема.
//    /// </summary>
//    public class HbmPmxAnalogOutConnector : HbmAnalogOutConnector
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
//        internal HbmPmxAnalogOutConnector(global::Hbm.Api.Pmx.Connectors.PmxAnalogOutConnector target, HbmDevice device) :
//            base(target, device)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.Pmx.Connectors.PmxAnalogOutConnector Target { get; }
//    }
//}
