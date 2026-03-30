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
//    public class SignalCollectionOld : Ancestor, IEnumerable<SignalOld>
//    {
//        /// <summary>
//        /// Происходит при подключении к каналам.
//        /// </summary>
//        public event EventHandler Connected;

//        /// <summary>
//        /// Происходит при отключении от каналов.
//        /// </summary>
//        public event EventHandler Disconnected;

//        /// <summary>
//        /// Поле для хранения списка сигналов.
//        /// </summary>
//        private readonly List<SignalOld> _Items;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        internal SignalCollectionOld()
//        {
//            _Items = new List<SignalOld>();
//        }

//        /// <summary>
//        /// Возвращает количество элементов в коллекции.
//        /// </summary>
//        public int Count => _Items.Count;

//        /// <summary>
//        /// Возвращает сигнал с указанным индексом.
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
//        public SignalOld this[int index]
//        {
//            get
//            {
//                lock (_Items)
//                {
//                    if (index < 0)
//                    {
//                        throw new ArgumentOutOfRangeException("index", "Передано отрицательное значение.");
//                    }
//                    if (index >= _Items.Count)
//                    {
//                        throw new ArgumentOutOfRangeException("index", "Передано недопустимо большое значение.");
//                    }
//                    return _Items[index];
//                }
//            }
//        }

//        /// <summary>
//        /// Подключает сигналы.
//        /// </summary>
//        /// <param name="signals">
//        /// Коллекция сигналов.
//        /// </param>
//        internal void Connect(IEnumerable<SignalOld> signals)
//        {
//            lock (_Items)
//            {
//                _Items.AddRange(signals);
//                OnConnected(EventArgs.Empty);
//            }
//        }

//        /// <summary>
//        /// Отключает сигналы.
//        /// </summary>
//        internal void Disconnect()
//        {
//            lock (_Items)
//            {
//                _Items.Clear();
//                OnDisconnected(EventArgs.Empty);
//            }
//        }

//        /// <summary>
//        /// Выполняет балансировку нуля всех активных сигналов.
//        /// </summary>
//        /// <param name="values">
//        /// Количество измерений, используемое при балансировке.
//        /// </param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="values"/> передано отрицательное или равное нулю значение.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        internal void ZeroBalance(int values)
//        {
//            if (values < 1)
//            {
//                throw new ArgumentOutOfRangeException("values", "Передано отрицательное или равное нулю значение.");
//            }
//            lock (_Items)
//            {
//                foreach (var signal in _Items)
//                {
//                    if (signal.IsActive)
//                    {
//                        signal.ZeroBalance(values);
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="Connected"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnConnected(EventArgs e)
//        {
//            Connected?.Invoke(this, e);
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="Disconnected"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnDisconnected(EventArgs e)
//        {
//            Disconnected?.Invoke(this, e);
//        }

//        /// <summary>
//        /// Возвращает перечислитель коллекции.
//        /// </summary>
//        /// <returns>
//        /// Перечислитель коллекции.
//        /// </returns>
//        public IEnumerator<SignalOld> GetEnumerator()
//        {
//            return ((IEnumerable<SignalOld>)_Items).GetEnumerator();
//        }

//        /// <summary>
//        /// Возвращает перечислитель коллекции.
//        /// </summary>
//        /// <returns>
//        /// Перечислитель коллекции.
//        /// </returns>
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return ((IEnumerable<SignalOld>)_Items).GetEnumerator();
//        }
//    }
//}
