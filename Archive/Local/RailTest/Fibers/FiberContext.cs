using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RailTest.Fibers
{
    /// <summary>
    /// Представляет контекст волокна.
    /// </summary>
    public class FiberContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="name">
        /// Имя волокна.
        /// </param>
        /// <param name="priority">
        /// Значение, определяющее приоритет потока.
        /// </param>
        /// <param name="isBackground">
        /// Значение, определяющее является ли поток фоновым.
        /// </param>
        internal FiberContext(string name, ThreadPriority priority, bool isBackground)
        {
            IsWork = false;
            Name = name ?? string.Empty;
            Priority = Enum.IsDefined(typeof(ThreadPriority), priority) ? priority : ThreadPriority.Normal;
            IsBackground = isBackground;
        }

        /// <summary>
        /// Возвращает значение, определяющее выполняется ли волокно.
        /// </summary>
        public bool IsWork { get; internal set; }

        /// <summary>
        /// Возвращает имя волокна.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает значение, определяющее приоритет потока. 
        /// </summary>
        public ThreadPriority Priority { get; }

        /// <summary>
        /// Возвращает значение, определяющее является ли поток фоновым.
        /// </summary>
        public bool IsBackground { get; }

        /// <summary>
        /// Возвращает время запуска волокна.
        /// </summary>
        public DateTime StartTime { get; internal set; }

        /// <summary>
        /// Возвращает или задаёт текущий поток.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Thread Thread { get; set; }
    }
}
