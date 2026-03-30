//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Выходной канал для разъемов CAN.
//    /// </summary>
//    public abstract class HbmCanOutChannel : HbmChannel
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
//        internal HbmCanOutChannel(global::Hbm.Api.Common.Entities.Channels.CanOutChannel target, HbmConnector connector) :
//            base(target, connector)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.Common.Entities.Channels.CanOutChannel Target;
//    }
//}
