//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет часть цифрового сигнала (фактически один бит "собственного" цифрового входного или цифрового выходного сигнала PMX) в соответствии с его маскированным битом.
//    /// </summary>
//    public class HbmPmxDigitalSignal : HbmDigitalCompressedSignal
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
//        internal HbmPmxDigitalSignal(global::Hbm.Api.Pmx.Signals.PmxDigitalSignal target, HbmChannel channel) :
//            base(target, channel)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.Pmx.Signals.PmxDigitalSignal Target;
//    }
//}
