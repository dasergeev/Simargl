//using RailTest.Signals;
//using System;
//using System.Collections;
//using System.Collections.Generic;

//namespace RailTest.Hardware
//{
//    /// <summary>
//    /// Представляет коллекцию значений.
//    /// </summary>
//    public class ValueCollectionOld : ICloneable, IEnumerable<double>
//    {
//        /// <summary>
//        /// Поле для хранения массива значений.
//        /// </summary>
//        private readonly double[] _Items;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="timestamp">
//        /// Отметка времени.
//        /// </param>
//        /// <param name="count">
//        /// Количество значений в коллекции.
//        /// </param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="count"/> передано отрицательное значение.
//        /// </exception>
//        public ValueCollectionOld(Timestamp timestamp, int count)
//        {
//            if (count < 0)
//            {
//                throw new ArgumentOutOfRangeException("count", "Передано отрицательное значение.");
//            }
//            Timestamp = timestamp;
//            Count = count;
//            _Items = new double[count];
//        }

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="timestamp">
//        /// Отметка времени.
//        /// </param>
//        /// <param name="items">
//        /// Массив значений.
//        /// </param>
//        public ValueCollectionOld(Timestamp timestamp, double[] items)
//        {
//            Timestamp = timestamp;
//            Count = items is object ? items.Length : 0;
//            _Items = new double[Count];
//            Array.Copy(items, 0, _Items, 0, Count);
//        }

//        /// <summary>
//        /// Возвращает отметку времени.
//        /// </summary>
//        public Timestamp Timestamp { get; }

//        /// <summary>
//        /// Возвращает количество значений в коллекции.
//        /// </summary>
//        public int Count { get; }

//        /// <summary>
//        /// Возвращает или задаёт значение по указанному индексу.
//        /// </summary>
//        /// <param name="index">
//        /// Индекс значения.
//        /// </param>
//        /// <returns>
//        /// Значение по указанному индексу.
//        /// </returns>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="index"/> передано отрицательное значение.
//        /// </exception>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="index"/> передано значение равное или превышающее <see cref="Count"/>.
//        /// </exception>
//        public double this[int index]
//        {
//            get
//            {
//                if (index < 0)
//                {
//                    throw new ArgumentOutOfRangeException("index", "Передано отрицательное значение.");
//                }
//                if (index >= Count)
//                {
//                    throw new ArgumentOutOfRangeException("index", "Передано значение равное или превышающее количество элементов в коллекции.");
//                }
//                return _Items[index];
//            }
//            set
//            {
//                if (index < 0)
//                {
//                    throw new ArgumentOutOfRangeException("index", "Передано отрицательное значение.");
//                }
//                if (index >= Count)
//                {
//                    throw new ArgumentOutOfRangeException("index", "Передано значение равное или превышающее количество элементов в коллекции.");
//                }
//                _Items[index] = value;
//            }
//        }

//        /// <summary>
//        /// Возвращает массив, содержащий элеменыт коллекции.
//        /// </summary>
//        /// <returns>
//        /// Массив, содержащий элеменыт коллекции.
//        /// </returns>
//        public double[] ToArray()
//        {
//            double[] items = new double[Count];
//            Array.Copy(_Items, 0, items, 0, Count);
//            return items;
//        }

//        /// <summary>
//        /// Создает новый объект, являющийся копией текущего экземпляра.
//        /// </summary>
//        /// <returns>
//        /// Новый объект, являющийся копией этого экземпляра.
//        /// </returns>
//        public ValueCollectionOld Clone()
//        {
//            return new ValueCollectionOld(Timestamp, _Items);
//        }

//        /// <summary>
//        /// Создает новый объект, являющийся копией текущего экземпляра.
//        /// </summary>
//        /// <returns>
//        /// Новый объект, являющийся копией этого экземпляра.
//        /// </returns>
//        object ICloneable.Clone()
//        {
//            return Clone();
//        }

//        /// <summary>
//        /// Возвращает перечислитель коллекции.
//        /// </summary>
//        /// <returns>
//        /// Перечислитель коллекции.
//        /// </returns>
//        public IEnumerator<double> GetEnumerator() => ((IEnumerable<double>)_Items).GetEnumerator();

//        /// <summary>
//        /// Возвращает перечислитель коллекции.
//        /// </summary>
//        /// <returns>
//        /// Перечислитель коллекции.
//        /// </returns>
//        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<double>)_Items).GetEnumerator();
//    }
//}
