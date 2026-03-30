//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// QuantumX конкретная реализация аналога в разъеме.
//    /// </summary>
//    public class HbmQuantumXAnalogInConnector : HbmAnalogInConnector
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
//        internal HbmQuantumXAnalogInConnector(global::Hbm.Api.QuantumX.Connectors.QuantumXAnalogInConnector target, HbmDevice device) :
//            base(target, device)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.QuantumX.Connectors.QuantumXAnalogInConnector Target { get; }
//    }
//}
