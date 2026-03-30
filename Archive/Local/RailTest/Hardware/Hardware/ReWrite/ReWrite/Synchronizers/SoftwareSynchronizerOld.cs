//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace RailTest.Hardware
//{
//    /// <summary>
//    /// Представляет программный синхронизатор.
//    /// </summary>
//    public sealed class SoftwareSynchronizerOld : SynchronizerOld
//    {
//        /// <summary>
//        /// Возвращает значение, определяющее минимальный интервал в милисекундах, через который срабатывает синхронизатор.
//        /// </summary>
//        public const int MinInterval = 10;

//        /// <summary>
//        /// Поле для хранения значения, определяющего выполняется ли работа.
//        /// </summary>
//        private bool _IsWork;

//        /// <summary>
//        /// Поле для хранения потока, в котором выполняется работа.
//        /// </summary>
//        private Thread _Thread;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="interval">
//        /// Интервал в милисекундах, через который срабатывает синхронизатор.
//        /// </param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="interval"/> передано значение меньшее <see cref="MinInterval"/>.
//        /// </exception>
//        public SoftwareSynchronizerOld(int interval) :
//            base(interval)
//        {
//            _IsWork = false;
//            _Thread = null;
//        }

//        /// <summary>
//        /// Запускает синхронизатор.
//        /// </summary>
//        protected override void CoreStart()
//        {
//            _IsWork = true;
//            _Thread = new Thread(ThreadEntry) { Priority = ThreadPriority.Highest, IsBackground = true };
//            _Thread.Start();
//        }

//        /// <summary>
//        /// Останавливает синхронизатор.
//        /// </summary>
//        protected override void CoreStop()
//        {
//            _IsWork = false;
//            _Thread.Join();
//        }

//        /// <summary>
//        /// Точка входа рабочего потока.
//        /// </summary>
//        private void ThreadEntry()
//        {
//            long interval = Interval * (long)10000;
//            long last = DateTime.Now.Ticks;
//            while (_IsWork)
//            {
//                long current = DateTime.Now.Ticks;
//                while (current - last >= interval && _IsWork)
//                {
//                    OnTick(EventArgs.Empty);
//                    last += interval;
//                }
//                Thread.Sleep(1);
//            }
//        }
//    }
//}
