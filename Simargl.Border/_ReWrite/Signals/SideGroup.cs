//using System.Collections.Generic;

//namespace RailTest.Border
//{
//    /// <summary>
//    /// Представляет группу сигналов на одной стороне, на одном рельсе, в одном сечении.
//    /// </summary>
//    public class SideGroup
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="section">
//        /// Номер сечения.
//        /// </param>
//        /// <param name="rail">
//        /// Значение, определяющее рельс.
//        /// </param>
//        /// <param name="side">
//        /// Значение, определяющее сторону.
//        /// </param>
//        internal SideGroup(int section, Rail rail, Side side)
//        {
//            Section = section;
//            Rail = rail;
//            Side = side;

//            string name = "S";
//            if (rail == Rail.Right)
//            {
//                name += "R";
//            }
//            else
//            {
//                name += "L";
//            }
//            if (side == Side.External)
//            {
//                name += "e";
//            }
//            else
//            {
//                name += "i";
//            }
//            name += section.ToString("00");
//            Signal0 = new Signal(name + "_0", "", false);
//            Signal1 = new Signal(name + "_1", "", false);
//            Signal2 = new Signal(name + "_2", "", false);

//            Signals = new List<Signal>
//            {
//                Signal0,
//                Signal1,
//                Signal2
//            };
//        }

//        /// <summary>
//        /// Возвращает номер сечения.
//        /// </summary>
//        public int Section { get; }

//        /// <summary>
//        /// Возвращает значение, определяющее рельс.
//        /// </summary>
//        public Rail Rail { get; }

//        /// <summary>
//        /// Возвращает сторону рельса.
//        /// </summary>
//        public Side Side { get; }

//        /// <summary>
//        /// Возвращает сигнал нулевого датчика.
//        /// </summary>
//        public Signal Signal0 { get; }

//        /// <summary>
//        /// Возвращает сигнал первого датчика.
//        /// </summary>
//        public Signal Signal1 { get; }

//        /// <summary>
//        /// Возвращает сигнал второго датчика.
//        /// </summary>
//        public Signal Signal2 { get; }

//        /// <summary>
//        /// Возвращает все сигналы.
//        /// </summary>
//        public List<Signal> Signals { get; }

//        /// <summary>
//        /// Устанавливает ноль.
//        /// </summary>
//        internal void Zero()
//        {
//            Signal0.Zero();
//            Signal1.Zero();
//            Signal2.Zero();
//        }

//        /// <summary>
//        /// Обновляет данные.
//        /// </summary>
//        /// <param name="blockIndex">
//        /// Индекс чтения.
//        /// </param>
//        internal void Update(int blockIndex)
//        {

//        }
//    }
//}
