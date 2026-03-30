//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Цифровой сжатый сигнал является частью DigitalCompressedGroupSignal, который предоставляет одно значение измерения для n цифровых сигналов. Например: устройство, которое выдает одно 16-битное значение для 16 цифровых входов, содержит 16 цифровых сжатых входных сигналов, каждый из которых отображается на один бит значения измерения (в соответствии с его маскированным битом).
//    /// </summary>
//    public abstract class HbmDigitalCompressedSignal : HbmDigitalSignal
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
//        internal HbmDigitalCompressedSignal(global::Hbm.Api.Common.Entities.Signals.DigitalCompressedSignal target, HbmChannel channel) :
//            base(target, channel)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.Common.Entities.Signals.DigitalCompressedSignal Target;
//    }
//}
