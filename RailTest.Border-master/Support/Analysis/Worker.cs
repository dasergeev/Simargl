using RailTest.Fibers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Border.Support.Analysis
{
    /// <summary>
    /// Представляет обработчик.
    /// </summary>
    public abstract class Worker
    {
        /// <summary>
        /// Путь к каталогу с исходными файлами.
        /// </summary>
        public const string SourcePath = @"E:\OneDrive\Научно-технический отдел\Проекты\Border\Border 01\Border 01.01\Analysis\Part #01\#01 Source";

        /// <summary>
        /// Путь к каталогу с предобработанными файлами.
        /// </summary>
        public const string PreparationPath = @"E:\OneDrive\Научно-технический отдел\Проекты\Border\Border 01\Border 01.01\Analysis\Part #01\#02 Preparation";

        /// <summary>
        /// Путь к каталогу с обработанными файлами.
        /// </summary>
        public const string ProcessingPath = @"E:\OneDrive\Научно-технический отдел\Проекты\Border\Border 01\Border 01.01\Analysis\Part #01\#03 Processing";

        /// <summary>
        /// Происходит при запуске обработчика.
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Происходит при остановке обработчика.
        /// </summary>
        public event EventHandler Stopped;

        /// <summary>
        /// Происходит в случае, если во время выполнения произошла ошибка.
        /// </summary>
        public event EventHandler Failed;

        /// <summary>
        /// Поле для хранения волокна.
        /// </summary>
        private readonly Fiber _Fiber;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="output">
        /// Средство вывода текстовой информации.
        /// </param>
        public Worker(Output output)
        {
            Output = output;
            _Fiber = new Fiber(Work);
            _Fiber.Started += (object sender, EventArgs e) => Started?.Invoke(this, EventArgs.Empty);
            _Fiber.Stopped += (object sender, EventArgs e) => Stopped?.Invoke(this, EventArgs.Empty);
            _Fiber.Failed += (object sender, EventArgs e) => Failed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Возвращает средство вывода текстовой информации.
        /// </summary>
        public Output Output { get; }

        /// <summary>
        /// Возвращает исключение, которое произошло во время выполнения.
        /// </summary>
        public Exception Exception => _Fiber.Exception;

        /// <summary>
        /// Запускает работу.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Волокно уже работает.
        /// </exception>
        public void Start()
        {
            _Fiber.Start();
        }

        /// <summary>
        /// Останавливает работу.
        /// </summary>
        public void Stop()
        {
            _Fiber.Stop(1000);
        }

        /// <summary>
        /// Выполняет работу.
        /// </summary>
        /// <param name="context">
        /// Контекст волокна.
        /// </param>
        protected abstract void Work(FiberContext context);
    }
}
