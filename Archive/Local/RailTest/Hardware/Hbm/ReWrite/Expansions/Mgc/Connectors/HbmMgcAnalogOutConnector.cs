//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Специальная реализация MgC аналогового выходного разъема.
//    /// </summary>
//    public class HbmMgcAnalogOutConnector : HbmAnalogOutConnector
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
//        internal HbmMgcAnalogOutConnector(global::Hbm.Api.Mgc.Connectors.MgcAnalogOutConnector target, HbmDevice device) :
//            base(target, device)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.Mgc.Connectors.MgcAnalogOutConnector Target { get; }
//    }
//}
