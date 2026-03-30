//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware
//{
//    /// <summary>
//    /// Представляет коллекцию сигналов.
//    /// </summary>
//    public sealed class SignalCollection : Ancestor, IEnumerable<Signal>
//    {
//        /// <summary>
//        /// Поле для хранения массива сигналов.
//        /// </summary>
//        private readonly Signal[]  _Items;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="device">
//        /// Устройство, которому принадлежат сигналы.
//        /// </param>
//        /// <param name="count">
//        /// Количество сигналов.
//        /// </param>
//        internal SignalCollection(Device device, int count)
//        {
//            _Items = new Signal[count];
//            Count = count;
//            for (int i = 0; i < count; i++)
//            {
//                _Items[i] = new Signal(device, i);
//            }
//        }

//        /// <summary>
//        /// Возвращает количество сигналов.
//        /// </summary>
//        public int Count { get; }

//        /// <summary>
//        /// Возвращает сигнал по указанному индексу.
//        /// </summary>
//        /// <param name="index">
//        /// Индекс сигнала.
//        /// </param>
//        /// <returns>
//        /// Сигнал с указанным индексом.
//        /// </returns>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="index"/> передано отрицательное значение.
//        /// </exception>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Count"/>.
//        /// </exception>
//        public Signal this[int index]
//        {
//            get
//            {
//                if (index < 0)
//                {
//                    throw new ArgumentOutOfRangeException("index", "Передано отрицательное значение.");
//                }
//                if (index >= Count)
//                {
//                    throw new ArgumentOutOfRangeException("index", "Передано недопустимо большое значение.");
//                }
//                return _Items[index];
//            }
//        }

//        /// <summary>
//        /// Возвращает перечислитель коллекции.
//        /// </summary>
//        /// <returns>
//        /// Перечислитель коллекции.
//        /// </returns>
//        public IEnumerator<Signal> GetEnumerator() => ((IEnumerable<Signal>)_Items).GetEnumerator();

//        /// <summary>
//        /// Возвращает перечислитель коллекции.
//        /// </summary>
//        /// <returns>
//        /// Перечислитель коллекции.
//        /// </returns>
//        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Signal>)_Items).GetEnumerator();
//    }
//}
