using RailTest.Satellite.Autonomic.Logging;
using System;
using System.ServiceProcess;
using System.Threading;

namespace RailTest.Satellite.Autonomic.Services
{
    /// <summary>
    /// Представляет базовый класс для всех служб.
    /// </summary>
    public abstract partial class AutonomicService : ServiceBase
    {
        /// <summary>
        /// Поле для хранения рабочего потока.
        /// </summary>
        private Thread _Thread;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="serviceID">
        /// Идентификатор службы.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="serviceID"/> передано значение,
        /// которое не содержится в перечислении <see cref="ServiceID"/>.
        /// </exception>
        public AutonomicService(ServiceID serviceID)
        {
            InitializeComponent();
            if (!Enum.IsDefined(typeof(ServiceID), serviceID))
            {
                throw new ArgumentOutOfRangeException("serviceID", "Значение не содержится в перечислении.");
            }
            ServiceName = serviceID.GetServiceName();
            Logger = new Logger(ServiceName);
            IsWork = false;
        }

        /// <summary>
        /// Возвращает идентификатор службы.
        /// </summary>
        public ServiceID ServiceID { get; }

        /// <summary>
        /// Возвращает самописец.
        /// </summary>
        public Logger Logger { get; }

        /// <summary>
        /// Возвращает значение, определяющее выполняется ли работа.
        /// </summary>
        public bool IsWork { get; private set; }

        /// <summary>
        /// Происходит при запуске службы.
        /// </summary>
        /// <param name="args">
        /// Аргументы командной строки.
        /// </param>
        protected override void OnStart(string[] args)
        {
            IsWork = true;
            _Thread = new Thread(ThreadEntry) { IsBackground = true };
            _Thread.Start();
        }

        /// <summary>
        /// Происходит при остановке службы.
        /// </summary>
        protected override void OnStop()
        {
            Thread thread = _Thread;
            IsWork = false;
            if (thread is object)
            {
                try
                {
                    for (int i = 0; i != 10; ++i)
                    {
                        if (thread.Join(500))
                        {
                            break;
                        }
                    }
                }
                catch
                {

                }
            }

            thread = _Thread;
            if (thread is object)
            {
                try
                {
                    thread.Abort();
                }
                catch
                {

                }
            }

            _Thread = null;
        }

        /// <summary>
        /// Выполняет основную работу.
        /// </summary>
        protected abstract void Invoke();

        /// <summary>
        /// Точка входа рабочего потока.
        /// </summary>
        private void ThreadEntry()
        {
            try
            {
                Logger.WriteLine("Запуск.");
                try
                {
                    Invoke();
                }
                catch (Exception ex)
                {
                    bool isAbort = false;
                    Exception exception = ex;
                    while (exception is object)
                    {
                        if (exception is ThreadAbortException)
                        {
                            isAbort = true;
                            exception = null;
                        }
                        else
                        {
                            exception = exception.InnerException;
                        }
                    }
                    if (!isAbort)
                    {
                        Logger.WriteLine(ex.ToString());
                        throw ex;
                    }
                }
                Logger.WriteLine("Остановка.");
            }
            finally
            {
                IsWork = false;
                _Thread = null;
            }
        }
    }
}
