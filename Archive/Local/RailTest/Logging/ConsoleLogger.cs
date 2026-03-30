using RailTest.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Logging
{
    /// <summary>
    /// Представляет средство для ведения журнала в консоль.
    /// </summary>
    public class ConsoleLogger : Logger
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="sender">
        /// Имя отправителя.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="sender"/> передана пустая ссылка.
        /// </exception>
        public ConsoleLogger(string sender) :
            base(sender)
        {

        }

        /// <summary>
        /// Регистрирует строку журнала.
        /// </summary>
        /// <param name="row">
        /// Строка журнала.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="row"/> передана пустая ссылка.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object,System.Object)")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Не передавать литералы в качестве локализованных параметров", MessageId = "System.Console.WriteLine(System.String)")]
        protected override void Log(LoggerRow row)
        {
            //  Проверка ссылки на строку журнала.
            ExceptionHelper.CheckReference(row, nameof(row));

            //  Вывод в консоль.
            Console.WriteLine($"[{row.Time}][{row.Sender}] {row.Message}");
        }
    }
}
