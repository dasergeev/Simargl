//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Mgc конкретная реализация аналогового выходного канала.
//    /// </summary>
//    public class HbmMgcAnalogOutChannel : HbmAnalogOutChannel
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
//        internal HbmMgcAnalogOutChannel(global::Hbm.Api.Mgc.Channels.MgcAnalogOutChannel target, HbmConnector connector) :
//            base(target, connector)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.Mgc.Channels.MgcAnalogOutChannel Target;
//    }
//}
