using System;
using System.Threading.Tasks;

namespace RailTest.Border.Server
{
    /// <summary>
    /// Представляет экземпляр приложения.
    /// </summary>
    public class Instance
    {
        /// <summary>
        /// Происходит после запуска сервера.
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Происходит после остановки сервера.
        /// </summary>
        public event EventHandler Stopped;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="mainControl">
        /// Главное элемент управления приложения.
        /// </param>
        public Instance(MainControl mainControl)
        {
            IsWork = false;
            MainControl = mainControl;
        }

        /// <summary>
        /// Возвращает значение, определяющее, выполняется ли работа.
        /// </summary>
        public bool IsWork { get; private set; }

        /// <summary>
        /// Возвращает время начала работы.
        /// </summary>
        public DateTime StartTime { get; private set; }

        private Equipment equipment;

        /// <summary>
        /// Возвращает аппаратуру.
        /// </summary>
        public Equipment GetEquipment()
        {
            return equipment;
        }

        /// <summary>
        /// Возвращает аппаратуру.
        /// </summary>
        private void SetEquipment(Equipment value)
        {
            equipment = value;
        }

        /// <summary>
        /// Возвращает главное окно приложения.
        /// </summary>
        public MainForm MainForm
        {
            get
            {
                return MainControl.MainForm;
            }
        }

        /// <summary>
        /// Возвращает главный элемент управления приложения.
        /// </summary>
        public MainControl MainControl { get; }

        /// <summary>
        /// Возвращает средство вывода текстовой информации.
        /// </summary>
        public Output Output
        {
            get
            {
                return MainControl.Output;
            }
        }

        /// <summary>
        /// Запускает сервер.
        /// </summary>
        public void Start()
        {
            Task.Run(() => {
                lock (this)
                {
                    if (!IsWork)
                    {
                        lock (Output)
                        {
                            Output.WriteLine("Запуск сервера...");
                            SetEquipment(new Equipment());
                            IsWork = true;
                            StartTime = DateTime.Now;
                            OnStarted(EventArgs.Empty);
                            Output.WriteLine("Сервер запущен.");
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Останавливает сервер.
        /// </summary>
        public void Stop()
        {
            Task.Run(() => {
                lock (this)
                {
                    if (IsWork)
                    {
                        lock (Output)
                        {
                            Output.WriteLine("Остановка сервера...");
                            GetEquipment().Dispose();
                            SetEquipment(null);
                            IsWork = false;
                            OnStopped(EventArgs.Empty);
                            Output.WriteLine("Сервер остановлен.");
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Вызывает событие <see cref="Started"/>.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected virtual void OnStarted(EventArgs e)
        {
            Started?.Invoke(this, e);
        }

        /// <summary>
        /// Вызывает событие <see cref="Stopped"/>.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected virtual void OnStopped(EventArgs e)
        {
            Stopped?.Invoke(this, e);
        }
    }
}
