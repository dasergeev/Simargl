//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;

//namespace RailTest.Hardware.Hbm
//{
//    /// <summary>
//    /// Представляет устройство QuantumX.
//    /// </summary>
//    public class HbmQuantumXDevice : HbmStreamingDevice
//    {
//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="target">
//        /// Целевой объект.
//        /// </param>
//        /// <param name="count">
//        /// Количество сигналов.
//        /// </param>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="count"/> передано отрицательное или равное нулю значение.
//        /// </exception>
//        internal HbmQuantumXDevice(global::Hbm.Api.QuantumX.QuantumXDevice target, int count) :
//            base(target, count)
//        {
//            Target = target;
//        }

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="ipAddress">
//        /// IP-адрес устройства.
//        /// </param>
//        /// <param name="count">
//        /// Количество сигналов.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="ipAddress"/> передана пустая ссылка.
//        /// </exception>
//        /// <exception cref="FormatException">
//        /// В параметре <paramref name="ipAddress"/> передана строка, не содержащая допустимый IP-адрес.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="count"/> передано отрицательное или равное нулю значение.
//        /// </exception>
//        public HbmQuantumXDevice(string ipAddress, int count) :
//            this(ipAddress, global::Hbm.Api.QuantumX.QuantumXDevice.CONNECTION_DEFAULT_PORT, count)
//        {
            
//        }

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="ipAddress">
//        /// IP-адрес устройства.
//        /// </param>
//        /// <param name="port">
//        /// Порт для подключения к устройству.
//        /// </param>
//        /// <param name="count">
//        /// Количество сигналов.
//        /// </param>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="ipAddress"/> передана пустая ссылка.
//        /// </exception>
//        /// <exception cref="FormatException">
//        /// В параметре <paramref name="ipAddress"/> передана строка, не содержащая допустимый IP-адрес.
//        /// </exception>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="port"/> передано значение меньше значения <see cref="IPEndPoint.MinPort"/>.
//        /// </exception>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="port"/> передано значение больше значения <see cref="IPEndPoint.MaxPort"/>.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="count"/> передано отрицательное или равное нулю значение.
//        /// </exception>
//        public HbmQuantumXDevice(string ipAddress, int port, int count) :
//            this(CreateTarget(ipAddress, port), count)
//        {
//            EndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
//        }

//        /// <summary>
//        /// Возвращает целевой объект.
//        /// </summary>
//        internal new global::Hbm.Api.QuantumX.QuantumXDevice Target { get; }

//        /// <summary>
//        /// Возвращает сетевую конечную точку в виде IP-адреса и номер порта.
//        /// </summary>
//        public IPEndPoint EndPoint { get; }

//        /// <summary>
//        /// Создаёт новый целевой объект.
//        /// </summary>
//        /// <param name="ipAddress">
//        /// IP-адрес устройства.
//        /// </param>
//        /// <param name="port">
//        /// Порт для подключения к устройству.
//        /// </param>
//        /// <returns>
//        /// Новый целевой объект.
//        /// </returns>
//        /// <exception cref="ArgumentNullException">
//        /// В параметре <paramref name="ipAddress"/> передана пустая ссылка.
//        /// </exception>
//        /// <exception cref="FormatException">
//        /// В параметре <paramref name="ipAddress"/> передана строка, не содержащая допустимый IP-адрес.
//        /// </exception>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="port"/> передано значение меньше значения <see cref="IPEndPoint.MinPort"/>.
//        /// </exception>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// В параметре <paramref name="port"/> передано значение больше значения <see cref="IPEndPoint.MaxPort"/>.
//        /// </exception>
//        /// <exception cref="InvalidOperationException">
//        /// Не удалось выполнить операцию.
//        /// </exception>
//        private static global::Hbm.Api.QuantumX.QuantumXDevice CreateTarget(string ipAddress, int port)
//        {
//            if (ipAddress is null)
//            {
//                throw new ArgumentNullException("ipAddress", "Передана пустая ссылка.");
//            }
//            if (!IPAddress.TryParse(ipAddress, out IPAddress address))
//            {
//                throw new FormatException("Строка не содержит допустимый IP-адрес.");
//            }
//            if (port < IPEndPoint.MinPort)
//            {
//                throw new ArgumentOutOfRangeException("port", "Передано значение меньше допустимого.");
//            }
//            if (port > IPEndPoint.MaxPort)
//            {
//                throw new ArgumentOutOfRangeException("port", "Передано значение больше допустимого.");
//            }
//            global::Hbm.Api.QuantumX.QuantumXDevice target = null;
//            Performing.Perform(() =>
//            {
//                target = new global::Hbm.Api.QuantumX.QuantumXDevice(ipAddress, port);
//            });
//            return target;
//        }
//    }
//}
