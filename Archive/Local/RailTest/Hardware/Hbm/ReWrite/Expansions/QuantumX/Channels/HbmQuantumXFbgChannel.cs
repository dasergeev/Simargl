//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет собой физический канал брэгговской решетки из волокон QuantumX.
//    /// </summary>
//    public class HbmQuantumXFbgChannel : HbmFbgChannel
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
//        internal HbmQuantumXFbgChannel(global::Hbm.Api.QuantumX.Channels.QuantumXFbgChannel target, HbmConnector connector) :
//            base(target, connector)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.QuantumX.Channels.QuantumXFbgChannel Target;
//    }
//}
