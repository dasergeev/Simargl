//using System;

//namespace RailTest.Border
//{
//    /// <summary>
//    /// Представляет измерительный модуль.
//    /// </summary>
//    public class Module
//    {
//        /// <summary>
//        /// Поле для хранения приёмника данных.
//        /// </summary>
//        private readonly Receiver _Receiver;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="receiver">
//        /// Приёмник данных.
//        /// </param>
//        /// <param name="index">
//        /// Индекс модуля.
//        /// </param>
//        internal Module(Receiver receiver, int index)
//        {
//            _Receiver = receiver;
//            Index = index;
//            Address = ModuleAddresses.GetAddress(index);
//        }

//        /// <summary>
//        /// Возвращает индекс модуля.
//        /// </summary>
//        public int Index { get; }

//        /// <summary>
//        /// Возвращает адрес модуля.
//        /// </summary>
//        public string Address { get; }

//        /// <summary>
//        /// Возвращает значение, определяющее, подключен ли модуль.
//        /// </summary>
//        public bool Connected => _Receiver.GetConnected(Index);

//        /// <summary>
//        /// Возвращает последний маркер времени.
//        /// </summary>
//        public long Marker => _Receiver.GetMarker(Index);

//        /// <summary>
//        /// Возвращает количество полученных блоков.
//        /// </summary>
//        public long Blocks => _Receiver.GetBlocks(Index);

//        /// <summary>
//        /// Возвращает время запуска.
//        /// </summary>
//        public DateTime StartTime => _Receiver.GetStartTime(Index);

//        /// <summary>
//        /// Возвращает индекс блока.
//        /// </summary>
//        public int BlockIndex => _Receiver.GetBlockIndex(Index);

//        /// <summary>
//        /// Возвращает частоту дискретизации.
//        /// </summary>
//        public int Samples
//        {
//            get
//            {
//                int samples = 0;
//                if (Blocks > 100)
//                {
//                    double seconds = (DateTime.Now - StartTime).TotalSeconds;
//                    if (seconds > 0)
//                    {
//                        samples = 50 * (int)Math.Round((Blocks - 100) / seconds);
//                    }
//                }
//                return samples;
//            }
//        }
//    }
//}
