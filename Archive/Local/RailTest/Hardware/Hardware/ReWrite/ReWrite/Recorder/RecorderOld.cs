//using RailTest.Signals;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware
//{
//    /// <summary>
//    /// Представляет самописец.
//    /// </summary>
//    public sealed class RecorderOld : Ancestor
//    {
//        /// <summary>
//        /// Поле для хранения списка буферов.
//        /// </summary>
//        private readonly List<RecordBufferOld> _Buffers;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="length">
//        /// Длина буфера.
//        /// </param>
//		/// <exception cref="ArgumentOutOfRangeException">
//		/// В параметре <paramref name="length"/> передано отрицательное или равное нулю значение.
//		/// </exception>
//        public RecorderOld(int length)
//        {
//            if (length <= 0)
//            {
//                throw new ArgumentOutOfRangeException("length", "Передано отрицательное или равное нулю значение.");
//            }
//            _Buffers = new List<RecordBufferOld>();
//            Length = length;
//            Start = DateTime.Now;
//        }

//        /// <summary>
//        /// Длина буфера.
//        /// </summary>
//        public int Length { get; }

//        /// <summary>
//        /// Возвращает отметку времени начала отсчёта.
//        /// </summary>
//        public Timestamp Start { get; }

//        /// <summary>
//        /// Прикрепляет сигнал.
//        /// </summary>
//        /// <param name="link">
//        /// Ссылка на сигнал.
//        /// </param>
//        /// <exception cref="InvalidOperationException">
//        /// Сигнал уже прикреплён.
//        /// </exception>
//        internal void Attach(SignalLinkOld link)
//        {
//            lock (_Buffers)
//            {
//                foreach (RecordBufferOld buffer in _Buffers)
//                {
//                    if (buffer.Link == link)
//                    {
//                        throw new InvalidOperationException("Сигнал уже прикреплён.");
//                    }
//                }
//                _Buffers.Add(new RecordBufferOld(link, Length, Start));
//            }
//        }
//    }
//}
