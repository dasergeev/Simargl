//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Общий потоковый виртуальный сигнал.
//    /// </summary>
//    public class HbmGenericStreamingVirtualSignal : HbmVirtualSignal
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
//        internal HbmGenericStreamingVirtualSignal(global::Hbm.Api.GenericStreaming.Signals.GenericStreamingVirtualSignal target, HbmChannel channel) :
//            base(target, channel)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.GenericStreaming.Signals.GenericStreamingVirtualSignal Target;
//    }
//}
