//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет коллекцию каналов устройства HBM.
//    /// </summary>
//    public class HbmChannelCollection : IEnumerable<HbmChannel>
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
//        /// Поле для хранения элементов коллекции.
//        /// </summary>
//        private readonly List<HbmChannel> _Items;

//        /// <summary>
//        /// Поле для хранения коллекции разъёмов.
//        /// </summary>
//        private readonly HbmConnectorCollection _Connectors;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="connectors">
//        /// Коллекция разъёмов.
//        /// </param>
//        internal HbmChannelCollection(HbmConnectorCollection connectors)
//        {
//            _Items = new List<HbmChannel>();
//            _Connectors = connectors;
//            _Connectors.Connected += Connectors_Connected;
//            _Connectors.Disconnected += Connectors_Disconnected;
//        }

//        /// <summary>
//        /// Возвращает количество элементов коллекции.
//        /// </summary>
//        public int Count => _Items.Count;

//        /// <summary>
//        /// Возвращает канал с указанным индексом.
//        /// </summary>
//        /// <param name="index">
//        /// Индекс канала.
//        /// </param>
//        /// <returns>
//        /// Канал с указанным индексом.
//        /// </returns>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="index"/> передано отрицательное значение.
//        /// </exception>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="index"/> передано значение большее или равное <see cref="Count"/>.
//        /// </exception>
//        public HbmChannel this[int index]
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
//        /// Орабатывает событие <see cref="HbmConnectorCollection.Connected"/>.
//        /// </summary>
//        /// <param name="sender">
//        /// Объект, создавший событие.
//        /// </param>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        private void Connectors_Connected(object sender, EventArgs e)
//        {
//            lock (_Items)
//            {
//                foreach (var connector in _Connectors)
//                {
//                    _Items.Add(connector.Channel);
//                }
//                OnConnected(EventArgs.Empty);
//            }
//        }

//        /// <summary>
//        /// Орабатывает событие <see cref="HbmConnectorCollection.Disconnected"/>.
//        /// </summary>
//        /// <param name="sender">
//        /// Объект, создавший событие.
//        /// </param>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        private void Connectors_Disconnected(object sender, EventArgs e)
//        {
//            lock (_Items)
//            {
//                _Items.Clear();
//                OnDisconnected(EventArgs.Empty);
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
//        public IEnumerator<HbmChannel> GetEnumerator()
//        {
//            return ((IEnumerable<HbmChannel>)_Items).GetEnumerator();
//        }

//        /// <summary>
//        /// Возвращает перечислитель коллекции.
//        /// </summary>
//        /// <returns>
//        /// Перечислитель коллекции.
//        /// </returns>
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return ((IEnumerable<HbmChannel>)_Items).GetEnumerator();
//        }
//    }
//}
