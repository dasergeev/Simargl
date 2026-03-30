//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware
//{
//    /// <summary>
//    /// Представляет сигнал.
//    /// </summary>
//    public class Signal : Ancestor
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="device">
//        /// Устройство, которому принадлежит сигнал.
//        /// </param>
//        /// <param name="index">
//        /// Индекс сигнала.
//        /// </param>
//        internal Signal(Device device, int index)
//        {
//            Device = device;
//            Index = index;
//        }

//        /// <summary>
//        /// Возвращает устройство, которому принадлежит сигнал.
//        /// </summary>
//        public Device Device { get; }

//        /// <summary>
//        /// Возвращает индекс сигнала.
//        /// </summary>
//        public int Index { get; }

//        /// <summary>
//        /// Возвращает или задаёт имя сигнала.
//        /// </summary>
//        public string Name { get; set; }

//        /// <summary>
//        /// Возвращает или задаёт единицу измерения.
//        /// </summary>
//        public string Unit { get; set; }

//        /// <summary>
//        /// Возвращает или задаёт частоту дискретизации.
//        /// </summary>
//        public double Sampling { get; set; }

//        /// <summary>
//        /// Возвращает или задаёт частоту среза фильтра.
//        /// </summary>
//        public double Cutoff { get; set; }
//    }
//}
