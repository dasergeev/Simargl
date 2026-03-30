//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware
//{
//    /// <summary>
//    /// Представляет базовый класс для всех синхронизаторов.
//    /// </summary>
//    public abstract class SynchronizerOld : Ancestor
//    {
//        /// <summary>
//        /// Происходит при запуске устройства.
//        /// </summary>
//        public event EventHandler Started;

//        /// <summary>
//        /// Происходит при отсановке устройства.
//        /// </summary>
//        public event EventHandler Stopped;

//        /// <summary>
//        /// Поле для хранения списка обработчиков событий.
//        /// </summary>
//        private readonly List<EventHandler> _Handlers;

//        /// <summary>
//        /// Поле для хранения объекта, который используется для синхронизации запуска и остановки работы синхронизатора.
//        /// </summary>
//        private readonly object _SyncStart;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="interval">
//        /// Интервал в милисекундах, через который срабатывает синхронизатор.
//        /// </param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="interval"/> передано отрицательное или равное нулю значение.
//        /// </exception>
//        public SynchronizerOld(int interval)
//        {
//            if (interval <= 0)
//            {
//                throw new ArgumentOutOfRangeException("interval", "Передано отрицательное или равное нулю значение.");
//            }
//            Interval = interval;
//            _Handlers = new List<EventHandler>();
//            _SyncStart = new object();
//        }

//        /// <summary>
//        /// Происходит при срабатывании синхронизатора.
//        /// </summary>
//        public event EventHandler Tick
//        {
//            add
//            {
//                lock (_Handlers)
//                {
//                    if (value is object)
//                    {
//                        if (!_Handlers.Contains(value))
//                        {
//                            _Handlers.Add(value);
//                        }
//                    }
//                }
//            }
//            remove
//            {
//                lock (_Handlers)
//                {
//                    if (value is object)
//                    {
//                        if (_Handlers.Contains(value))
//                        {
//                            _Handlers.Remove(value);
//                        }
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Возвращает интервал в милисекундах, через который срабатывает синхронизатор.
//        /// </summary>
//        public int Interval { get; }

//        /// <summary>
//        /// Возвращает значение, определяющее выполняет ли синхронизатор работу.
//        /// </summary>
//        public bool IsStarted { get; private set; }

//        /// <summary>
//        /// Запускает синхронизатор.
//        /// </summary>
//        public void Start()
//        {
//            lock (_SyncStart)
//            {
//                if (!IsStarted)
//                {
//                    CoreStart();
//                    OnStarted(EventArgs.Empty);
//                }
//            }
//        }

//        /// <summary>
//        /// Останавливает синхронизатор.
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
//        /// Запускает синхронизатор.
//        /// </summary>
//        protected abstract void CoreStart();

//        /// <summary>
//        /// Останавливает синхронизатор.
//        /// </summary>
//        protected abstract void CoreStop();

//        /// <summary>
//        /// Вызывает событие <see cref="Started"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnStarted(EventArgs e)
//        {
//            IsStarted = true;
//            Started?.Invoke(this, e);
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
//            Stopped?.Invoke(this, e);
//        }

//        /// <summary>
//        /// Вызывает событие <see cref="Tick"/>.
//        /// </summary>
//        /// <param name="e">
//        /// Аргументы, связанные с событием.
//        /// </param>
//        protected virtual void OnTick(EventArgs e)
//        {
//            lock (_Handlers)
//            {
//                foreach (EventHandler handler in _Handlers)
//                {
//                    try
//                    {
//                        handler.Invoke(this, e);
//                    }
//                    catch
//                    {

//                    }
//                }
//            }
//        }
//    }
//}
