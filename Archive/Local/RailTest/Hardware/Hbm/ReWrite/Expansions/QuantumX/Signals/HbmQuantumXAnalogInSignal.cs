//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет конкретный измерительный сигнал QuantumX, передаваемый физическим датчиком.
//    /// </summary>
//    public class HbmQuantumXAnalogInSignal : HbmAnalogInSignal
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <param name="channel">
//        /// Канал, к которому принадлежит сигнал.
//        /// </param>
//        internal HbmQuantumXAnalogInSignal(global::Hbm.Api.QuantumX.Signals.QuantumXAnalogInSignal target, HbmChannel channel) :
//            base(target, channel)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.QuantumX.Signals.QuantumXAnalogInSignal Target;
//    }
//}
