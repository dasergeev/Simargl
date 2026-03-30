using RailTest.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Logging
{
    /// <summary>
    /// Представляет строку журнала.
    /// </summary>
    public class LoggerRow : Ancestor
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="sender">
        /// Имя отправителя.
        /// </param>
        /// <param name="time">
        /// Время записи.
        /// </param>
        /// <param name="message">
        /// Сообщение.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="sender"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="message"/> передана пустая ссылка.
        /// </exception>
        public LoggerRow(string sender, DateTime time, string message)
        {
            //  Проверка ссылки на имя отправителя.
            ExceptionHelper.CheckReference(sender, nameof(sender));

            //  Проверка ссылки на сообщение.
            ExceptionHelper.CheckReference(message, nameof(message));

            //  Установка имени отправителя.
            Sender = sender;

            //  Установка времени записи.
            Time = time;

            //  Установка сообщения.
            Message = message;
        }

        /// <summary>
        /// Возвращает имя отправителя.
        /// </summary>
        public string Sender { get; }

        /// <summary>
        /// Возвращает время записи.
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// Возвращает сообщение.
        /// </summary>
        public string Message { get; }
    }
}
