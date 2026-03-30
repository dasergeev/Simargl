//using RailTest.Collections;
//using RailTest.Frames;
//using RailTest.Signals;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware
//{
//    /// <summary>
//    /// Представляет буфер записи.
//    /// </summary>
//    internal class RecordBufferOld : Ancestor
//    {
//        /// <summary>
//        /// Поле для хранения длины буфера.
//        /// </summary>
//        private readonly long _Length;

//        /// <summary>
//        /// Поле для хранения данных.
//        /// </summary>
//        private readonly double[] _Data;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="link">
//        /// Ссылка на сигнал.
//        /// </param>
//        /// <param name="length">
//        /// Длина буфера.
//        /// </param>
//        /// <param name="start">
//        /// Отметка времени начала отсчёта.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="link"/> передана пустая ссылка.
//        /// </exception>
//		/// <exception cref="ArgumentOutOfRangeException">
//		/// В параметре <paramref name="length"/> передано отрицательное или равное нулю значение.
//		/// </exception>
//        public RecordBufferOld(SignalLinkOld link, int length, Timestamp start)
//        {
//            Link = link ?? throw new ArgumentNullException("link", "Передана пустая сслыка.");
//            if (length <= 0)
//            {
//                throw new ArgumentOutOfRangeException("length", "Передано отрицательное или равное нулю значение.");
//            }
//            _Length = length;
//            _Data = new double[length];
//            Start = start;
//            Link.Response += Link_Response;
//        }

//        /// <summary>
//        /// Возвращает ссылку на сигнал.
//        /// </summary>
//        public SignalLinkOld Link { get; }

//        /// <summary>
//        /// Возвращает отметку времени начала отсчёта.
//        /// </summary>
//        public Timestamp Start { get; }

//        /// <summary>
//        /// Обрабатывает поступление новых данных
//        /// </summary>
//        /// <param name="sender">
//        /// Объект, создавший событие.
//        /// </param>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        private void Link_Response(object sender, ResponseEventArgsOld e)
//        {
//            var values = e.Values;
//            long start = Start - values.Timestamp;
//            int count = values.Count;
//            long length = _Length;
//            for (int i = 0; i != count; ++i)
//            {
//                long index = (start + i) % length;
//                _Data[index] = values[i];
//            }
//        }
//    }
//}
