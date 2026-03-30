//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет разъем, к которому нельзя подключить физический датчик.
//    /// </summary>
//    public abstract class HbmVirtualConnector : HbmConnector
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
//        internal HbmVirtualConnector(global::Hbm.Api.Common.Entities.Connectors.VirtualConnector target, HbmDevice device) :
//            base(target, device)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.Common.Entities.Connectors.VirtualConnector Target { get; }
//    }
//}
