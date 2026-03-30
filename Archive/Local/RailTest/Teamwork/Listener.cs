using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RailTest.Teamwork
{
    /// <summary>
    /// Прослушивает подключения от клиентов сети.
    /// </summary>
    public class Listener : Ancestor
    {
        ///// <summary>
        ///// Поле для хранения прослушивателя подключений от TCP-клиентов сети.
        ///// </summary>
        //private readonly TcpListener _TcpListener;

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="localaddr">
        /// Локальный IP-адрес.
        /// </param>
        /// <param name="port">
        /// Порт, на котором производится ожидание входящих попыток подключений.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "port")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "localaddr")]
        public Listener(IPAddress localaddr, int port)
        {

        }

        ////     
        ////
        //// Исключения:
        ////   T:System.ArgumentNullException:
        ////     Свойство localaddr имеет значение null.
        ////
        ////   T:System.ArgumentOutOfRangeException:
        ////     Значение параметра port не находится в диапазоне между значениями System.Net.IPEndPoint.MinPort
        ////     и System.Net.IPEndPoint.MaxPort.
        //public TcpListener(IPAddress localaddr, int port);


        /// <summary>
        /// Возвращает локальный IP-адрес.
        /// </summary>
        public IPAddress Address { get; }

    }
}
