using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Satellite.Autonomic.Logging
{
    /// <summary>
    /// Представляет аргументы отслеживателя самописца.
    /// </summary>
    public class LoggerTrackerEventArgs : EventArgs
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="message">
        /// Сообщение.
        /// </param>
        public LoggerTrackerEventArgs(string message)
        {
            Message = message ?? string.Empty;
        }

        /// <summary>
        /// Возвращает сообщение.
        /// </summary>
        public string Message { get; }
    }
}
