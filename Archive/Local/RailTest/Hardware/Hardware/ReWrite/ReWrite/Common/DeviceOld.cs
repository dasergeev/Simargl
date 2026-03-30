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
//    public abstract class DeviceOld : Device
//    {
//        /// <summary>
//        /// Поле для хранения объекта, который используется для синхронизации операций подключения/отключения.
//        /// </summary>
//        private readonly object _SyncConnect;

//        /// <summary>
//        /// Поле для хранения объекта, который используется для синхронизации работы.
//        /// </summary>
//        private readonly object _SyncStart;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="count">
//        /// Количество сигналов.
//        /// </param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="count"/> передано отрицательное или равное нулю значение.
//        /// </exception>
//        public DeviceOld(int count) :
//            base(count)
//        {
//            _SyncConnect = new object();
//            _SyncStart = new object();
//            Signals = new SignalCollectionOld();
//        }

//        /// <summary>
//        /// Разрушает объект.
//        /// </summary>
//        ~DeviceOld()
//        {
//            if (IsConnected)
//            {
//                try
//                {
//                    CoreDisconnect();
//                }
//                catch
//                {

//                }
//            }
//        }

//        /// <summary>
//        /// Возвращает поставщика устройства.
//        /// </summary>
//        internal protected override IDeviceProvider Provider
//        {
//            get
//            {
//                return null;
//            }
//        }

//        /// <summary>
//        /// Возвращает коллекцию сигналов.
//        /// </summary>
//        public new SignalCollectionOld Signals { get; }

//        /// <summary>
//        /// Асинхронно подключает устройство.
//        /// </summary>
//        /// <param name="attempts">
//        /// Максимальное количество попыток подключения.
//        /// </param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="attempts"/> передано отрицательное или равное нулю значение.
//        /// </exception>
//        public async void ConnectAsync(int attempts)
//        {
//            if (attempts <= 0)
//            {
//                throw new ArgumentOutOfRangeException("attempts", "Передано отрицательное или равное нулю значение.");
//            }
//            await Task.Run(() =>
//            {
//                Exception exception = null;
//                for (int i = 0; i != attempts; ++i)
//                {
//                    try
//                    {
//                        Connect();
//                        exception = null;
//                        break;
//                    }
//                    catch (Exception ex)
//                    {
//                        exception = ex;
//                    }
//                    Thread.Sleep(100);
//                }
//                if (exception is object)
//                {
//                    throw exception;
//                }
//            });
//        }

//        /// <summary>
//        /// Подключает устройство.
//        /// </summary>
//        public void Connect()
//        {
//            lock (_SyncConnect)
//            {
//                if (!IsConnected)
//                {
//                    CoreConnect();
//                    Signals.Connect(CoreLoadSignals());
//                    OnConnected(EventArgs.Empty);
//                }
//            }
//        }

//        /// <summary>
//        /// Отключает устройство.
//        /// </summary>
//        public void Disconnect()
//        {
//            lock (_SyncConnect)
//            {
//                Stop();
//                if (IsConnected)
//                {
//                    CoreDisconnect();
//                    Signals.Disconnect();
//                    OnDisconnected(EventArgs.Empty);
//                }
//            }
//        }

//        /// <summary>
//        /// Запускает устройство.
//        /// </summary>
//        public void Start()
//        {
//            lock (_SyncStart)
//            {
//                Connect();
//                if (!IsStarted)
//                {
//                    CoreStart();
//                    OnStarted(EventArgs.Empty);
//                }
//            }
//        }

//        /// <summary>
//        /// Останавливает устройство.
//        /// </summary>
//        public void Stop()
//        {
//            lock (_SyncStart)
//            {
//                if (IsStarted)
//                {
//                    CoreStop();
//                    OnStopped(EventArgs.Empty);
//                }
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
//        public void ZeroBalance(int values)
//        {
//            Signals.ZeroBalance(values);
//        }

//        /// <summary>
//        /// Подключает устройство.
//        /// </summary>
//        protected abstract void CoreConnect();

//        /// <summary>
//        /// Отключает устройство.
//        /// </summary>
//        protected abstract void CoreDisconnect();

//        /// <summary>
//        /// Запускает устройство.
//        /// </summary>
//        protected abstract void CoreStart();

//        /// <summary>
//        /// Останавливает устройство.
//        /// </summary>
//        protected abstract void CoreStop();

//        /// <summary>
//        /// Загружает доступные сигналы.
//        /// </summary>
//        /// <returns>
//        /// Коллекция доступных сигналов.
//        /// </returns>
//        internal protected abstract IList<SignalOld> CoreLoadSignals();
//    }
//}
