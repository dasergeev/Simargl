//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Канал, к которому может быть подключен датчик.
//    /// </summary>
//    public abstract class HbmAnalogInChannel : HbmChannel
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <param name="connector">
//        /// Разъём, к которому присоединён канал.
//        /// </param>
//        internal HbmAnalogInChannel(global::Hbm.Api.Common.Entities.Channels.AnalogInChannel target, HbmConnector connector) :
//            base(target, connector)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.Common.Entities.Channels.AnalogInChannel Target;
//    }
//}
