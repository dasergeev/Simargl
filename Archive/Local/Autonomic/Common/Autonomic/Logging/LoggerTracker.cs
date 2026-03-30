using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Satellite.Autonomic.Logging
{
    /// <summary>
    /// Представляет отслеживателя самописца.
    /// </summary>
    public class LoggerTracker
    {
        /// <summary>
        /// Происходит при поступлении нового сообщения.
        /// </summary>
        public event EventHandler<LoggerTrackerEventArgs> NewMessage;

        /// <summary>
        /// Поле для хранения самописца.
        /// </summary>
        private readonly Logger _Logger;

        /// <summary>
        /// Поле для хранения текущего индекса строки.
        /// </summary>
        private int _LineIndex;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="writerName">
        /// Имя писателя.
        /// </param>
        public LoggerTracker(string writerName)
        {
            _Logger = new Logger(writerName);
            WriterName = _Logger.WriterName;
            _LineIndex = _Logger.LineIndex;
        }

        /// <summary>
        /// Возвращает имя писателя.
        /// </summary>
        public string WriterName { get; }

        /// <summary>
        /// Выполняет шаг.
        /// </summary>
        public void MakeStep()
        {
            int lineIndex = _Logger.LineIndex;
            while (_LineIndex != lineIndex)
            {
                string message = _Logger.ReadLine(_LineIndex);
                OnNewMessage(new LoggerTrackerEventArgs(message));
                _LineIndex = (_LineIndex + 1) % Logger.LineCount;
            }
        }

        /// <summary>
        /// Вызывает событие <see cref="NewMessage"/>.
        /// </summary>
        /// <param name="e">
        /// Аргументы, связанные с событием.
        /// </param>
        protected virtual void OnNewMessage(LoggerTrackerEventArgs e)
        {
            NewMessage?.Invoke(this, e);
        }
    }
}
