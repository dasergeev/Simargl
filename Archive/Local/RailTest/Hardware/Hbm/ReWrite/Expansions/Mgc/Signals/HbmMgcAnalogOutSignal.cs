//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет специфический для Mgc аналоговый выходной сигнал.
//    /// </summary>
//    public class HbmMgcAnalogOutSignal : HbmAnalogOutSignal
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
//        internal HbmMgcAnalogOutSignal(global::Hbm.Api.Mgc.Signals.MgcAnalogOutSignal target, HbmChannel channel) :
//            base(target, channel)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.Mgc.Signals.MgcAnalogOutSignal Target;
//    }
//}
