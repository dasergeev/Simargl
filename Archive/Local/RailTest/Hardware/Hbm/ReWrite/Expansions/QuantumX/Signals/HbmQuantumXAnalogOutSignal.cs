//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет конкретный аналоговый выходной сигнал QuantumX.
//    /// </summary>
//    public class HbmQuantumXAnalogOutSignal : HbmAnalogOutSignal
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
//        internal HbmQuantumXAnalogOutSignal(global::Hbm.Api.QuantumX.Signals.QuantumXAnalogOutSignal target, HbmChannel channel) :
//            base(target, channel)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.QuantumX.Signals.QuantumXAnalogOutSignal Target;
//    }
//}
