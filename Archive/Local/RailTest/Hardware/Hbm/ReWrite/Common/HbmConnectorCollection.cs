//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представялет коллекцию разъёмов.
//    /// </summary>
//    public class HbmConnectorCollection : IEnumerable<HbmConnector>
//    {
//        /// <summary>
//        /// Происходит при подключении к разъёмам.
//        /// </summary>
//        public event EventHandler Connected;

//        /// <summary>
//        /// Происходит при отключении от разъёмов.
//        /// </summary>
//        public event EventHandler Disconnected;

//        /// <summary>
//        /// Поле для хранения списка разъёмов.
//        /// </summary>
//        private readonly List<HbmConnector> _Items;

//        /// <summary>
//        /// Поле для хранения устройства, которому принадлежат разъёмы.
//        /// </summary>
//        private readonly HbmDevice _Device;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="device">
//        /// Устройство, которому принадлежат разъёмы.
//        /// </param>
//        internal HbmConnectorCollection(HbmDevice device)
//        {
//            _Items = new List<HbmConnector>();
//            _Device = device;
//        }

//        /// <summary>
//        /// Подключает разъёмы.
//        /// </summary>
//        internal void Connect()
//        {
//            lock (_Items)
//            {
//                foreach (var connectorTarget in _Device.Target.Connectors)
//                {
//                    _Items.Add(HbmConnector.Create(connectorTarget, _Device));
//                }
//                OnConnected(EventArgs.Empty);
//            }
//        }

//        /// <summary>
//        /// Отключает разъёмы.
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
//        public IEnumerator<HbmConnector> GetEnumerator()
//        {
//            return ((IEnumerable<HbmConnector>)_Items).GetEnumerator();
//        }

//        /// <summary>
//        /// Возвращает перечислитель коллекции.
//        /// </summary>
//        /// <returns>
//        /// Перечислитель коллекции.
//        /// </returns>
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return ((IEnumerable<HbmConnector>)_Items).GetEnumerator();
//        }
//    }
//}
