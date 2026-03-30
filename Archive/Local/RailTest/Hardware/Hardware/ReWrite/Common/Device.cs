//using RailTest.Events;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace RailTest.Hardware
//{
//    /// <summary>
//    /// Представляет базовый класс для всех устройств.
//    /// </summary>
//    public abstract class Device : Ancestor
//    {
//        /// <summary>
//        /// Поле для хранения организатора события <see cref="Connected"/>.
//        /// </summary>
//        private readonly EventImplementer _ConnectedImplementer;

//        /// <summary>
//        /// Поле для хранения организатора события <see cref="Disconnected"/>.
//        /// </summary>
//        private readonly EventImplementer _DisconnectedImplementer;

//        /// <summary>
//        /// Поле для хранения организатора события <see cref="IsConnectedChanged"/>.
//        /// </summary>
//        private readonly EventImplementer _IsConnectedChangedImplementer;

//        /// <summary>
//        /// Поле для хранения организатора события <see cref="Started"/>.
//        /// </summary>
//        private readonly EventImplementer _StartedImplementer;

//        /// <summary>
//        /// Поле для хранения организатора события <see cref="Stopped"/>.
//        /// </summary>
//        private readonly EventImplementer _StoppedImplementer;

//        /// <summary>
//        /// Поле для хранения организатора события <see cref="IsStartedChanged"/>.
//        /// </summary>
//        private readonly EventImplementer _IsStartedChangedImplementer;

//        /// <summary>
//        /// Возвращает поставщика устройства.
//        /// </summary>
//        internal protected abstract IDeviceProvider Provider { get; }

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="count">
//        /// Количество сигналов.
//        /// </param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="count"/> передано отрицательное или равное нулю значение.
//        /// </exception>
//        public Device(int count)
//        {
//            if (count <= 0)
//            {
//                throw new ArgumentOutOfRangeException("count", "Передано отрицательное или равное нулю значение.");
//            }
//            Signals = new SignalCollection(this, count);
//            IsConnected = false;
//            IsStarted = false;

//            _ConnectedImplementer = new EventImplementer(EventInvokeHandler);
//            _DisconnectedImplementer = new EventImplementer(EventInvokeHandler);
//            _IsConnectedChangedImplementer = new EventImplementer(EventInvokeHandler);
//            _StartedImplementer = new EventImplementer(EventInvokeHandler);
//            _StoppedImplementer = new EventImplementer(EventInvokeHandler);
//            _IsStartedChangedImplementer = new EventImplementer(EventInvokeHandler);
//        }

//        /// <summary>
//        /// Выполняет вызов обработчика события.
//        /// </summary>
//        /// <param name="handler">
//        /// Обработчик события.
//        /// </param>
//        /// <param name="sender">
//        /// Объект, создавший событие.
//        /// </param>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        private void EventInvokeHandler(EventHandler handler, object sender, EventArgs e)
//        {
//            try
//            {
//                handler(sender, e);
//            }
//            catch (Exception ex)
//            {
//                Exception exception = ex;
//                while (ex is object)
//                {
//                    if (ex is ThreadAbortException)
//                    {
//                        exception = null;
//                        break;
//                    }
//                    ex = ex.InnerException;
//                }
//                if (exception is object)
//                {
//                    throw exception;
//                }
//            }
//        }

//        /// <summary>
//        /// Происходит при подключении устройства.
//        /// </summary>
//        public event EventHandler Connected
//        {
//            add
//            {
//                _ConnectedImplementer.AddHandler(value);
//            }
//            remove
//            {
//                _ConnectedImplementer.RemoveHandler(value);
//            }
//        }

//        /// <summary>
//        /// Происходит при отключении устройства.
//        /// </summary>
//        public event EventHandler Disconnected
//        {
//            add
//            {
//                _DisconnectedImplementer.AddHandler(value);
//            }
//            remove
//            {
//                _DisconnectedImplementer.RemoveHandler(value);
//            }
//        }

//        /// <summary>
//        /// Происходит при изменении свойства <see cref="IsConnected"/>.
//        /// </summary>
//        public event EventHandler IsConnectedChanged
//        {
//            add
//            {
//                _IsConnectedChangedImplementer.AddHandler(value);
//            }
//            remove
//            {
//                _IsConnectedChangedImplementer.RemoveHandler(value);
//            }
//        }

//        /// <summary>
//        /// Происходит при запуске устройства.
//        /// </summary>
//        public event EventHandler Started
//        {
//            add
//            {
//                _StartedImplementer.AddHandler(value);
//            }
//            remove
//            {
//                _StartedImplementer.RemoveHandler(value);
//            }
//        }

//        /// <summary>
//        /// Происходит при отсановке устройства.
//        /// </summary>
//        public event EventHandler Stopped
//        {
//            add
//            {
//                _StoppedImplementer.AddHandler(value);
//            }
//            remove
//            {
//                _StoppedImplementer.RemoveHandler(value);
//            }
//        }

//        /// <summary>
//        /// Происходит при изменении свойства <see cref="IsStarted"/>.
//        /// </summary>
//        public event EventHandler IsStartedChanged
//        {
//            add
//            {
//                _IsStartedChangedImplementer.AddHandler(value);
//            }
//            remove
//            {
//                _IsStartedChangedImplementer.RemoveHandler(value);
//            }
//        }

//        /// <summary>
//        /// Возвращает коллекцию сигналов устройства.
//        /// </summary>
//        public SignalCollection Signals { get; }

//        /// <summary>
//        /// Возвращает значение, определяющее подключено ли устройство.
//        /// </summary>
//        public bool IsConnected { get; private set; }

//        /// <summary>
//        /// Возвращает значение, определяющее выполняет ли устройство работу.
//        /// </summary>
//        public bool IsStarted { get; private set; }

//        /// <summary>
//        /// Вызывает событие <see cref="Connected"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnConnected(EventArgs e)
//        {
//            IsConnected = true;
//            _ConnectedImplementer.RaiseEvent(this, e);
//            OnIsConnectedChanged(EventArgs.Empty);
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="Disconnected"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnDisconnected(EventArgs e)
//        {
//            IsConnected = false;
//            _DisconnectedImplementer.RaiseEvent(this, e);
//            OnIsConnectedChanged(EventArgs.Empty);
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="IsConnectedChanged"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnIsConnectedChanged(EventArgs e)
//        {
//            _IsConnectedChangedImplementer.RaiseEvent(this, e);
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="Started"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnStarted(EventArgs e)
//        {
//            IsStarted = true;
//            _StartedImplementer.RaiseEvent(this, e);
//            OnIsStartedChanged(EventArgs.Empty);
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="Stopped"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnStopped(EventArgs e)
//        {
//            IsStarted = false;
//            _StoppedImplementer.RaiseEvent(this, e);
//            OnIsStartedChanged(EventArgs.Empty);
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="IsStartedChanged"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnIsStartedChanged(EventArgs e)
//        {
//            _IsStartedChangedImplementer.RaiseEvent(this, e);
//        }
//    }
//}
