//namespace RailTest.Border
//{
//    /// <summary>
//    /// Представляет метку времени.
//    /// </summary>
//    public class TimeLabel
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="frameNumber">
//        /// Номер кадра.
//        /// </param>
//        /// <param name="blockIndex">
//        /// Индекс блока.
//        /// </param>
//        public TimeLabel(int frameNumber, int blockIndex)
//        {
//            FrameNumber = frameNumber;
//            BlockIndex = blockIndex;
//        }

//        /// <summary>
//        /// Возвращает или задаёт номер кадра.
//        /// </summary>
//        public int FrameNumber { get; set; }

//        /// <summary>
//        /// Возвращает или задаёт индекс блока.
//        /// </summary>
//        public int BlockIndex { get; set; }

//        /// <summary>
//        /// Возвращает количество отсчётов.
//        /// </summary>
//        public long Ticks
//        {
//            get
//            {
//                return (FrameNumber * ((long)SignalBuilder.BlockCount) + BlockIndex) * SignalBuilder.BlockSize;
//            }
//        }
//    }
//}
