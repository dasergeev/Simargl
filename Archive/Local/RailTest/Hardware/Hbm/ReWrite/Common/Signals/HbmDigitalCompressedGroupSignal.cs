//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет цифровой сигнал, который содержит n цифровых сжатых сигналов, которые все были поданы с тем же самым значением измерения. Таким образом, битовая маска цифрового сжатого сигнала определяет бит значения измерения, который представляет его собственное значение измерения.
//    /// </summary>
//    public class HbmDigitalCompressedGroupSignal : HbmDigitalSignal
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
//        internal HbmDigitalCompressedGroupSignal(global::Hbm.Api.Common.Entities.Signals.DigitalCompressedGroupSignal target, HbmChannel channel) :
//            base(target, channel)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.Common.Entities.Signals.DigitalCompressedGroupSignal Target;
//    }
//}
