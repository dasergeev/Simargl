//using System.Collections;
//using System.Collections.Generic;
//using System.Net;

//namespace RailTest.Border
//{
//    /// <summary>
//    /// Представляет коллекцию модулей.
//    /// </summary>
//    public class ModuleCollection : IEnumerable<Module>
//    {
//        /// <summary>
//        /// Поле для хранения приёмника данных.
//        /// </summary>
//        private readonly Receiver _Receiver;

//        /// <summary>
//        /// Поле для хранения массива модулей.
//        /// </summary>
//        private readonly Module[] _Modules;

//        /// <summary>
//        /// Инициализирует новый экземпляр класса.
//        /// </summary>
//        /// <param name="receiver">
//        /// Приёмник данных.
//        /// </param>
//        internal ModuleCollection(Receiver receiver)
//        {
//            _Receiver = receiver;
//            _Modules = new Module[Receiver.ClientsCount];
//            for (int i = 0; i != Receiver.ClientsCount; ++i)
//            {
//                _Modules[i] = new Module(receiver, i);
//            }
//        }

//        /// <summary>
//        /// Возвращает количество активных модулей.
//        /// </summary>
//        public int Active => _Receiver.Active;

//        /// <summary>
//        /// Возвращает количество модулей в коллекции.
//        /// </summary>
//        public int Count => Receiver.ClientsCount;

//        /// <summary>
//        /// Возвращает модуль с указанным индексом.
//        /// </summary>
//        /// <param name="index">
//        /// Индекс блока.
//        /// </param>
//        /// <returns>
//        /// Модуль.
//        /// </returns>
//        public Module this[int index] => _Modules[index];

//        /// <summary>
//        /// Получает индекс устройства по сетевому адресу.
//        /// </summary>
//        /// <param name="endPoint">
//        /// Сетевой адресс.
//        /// </param>
//        /// <returns>
//        /// Индекс устройства по сетевому адресу.
//        /// </returns>
//        public int GetIndex(EndPoint endPoint)
//        {
//            string ipAddress = endPoint.ToString().Replace($":{Receiver.ConnectionPort}", "");
//            for (int i = 0; i != Count; ++i)
//            {
//                if (this[i].Address == ipAddress)
//                {
//                    return i;
//                }
//            }
//            return -1;
//        }

//        /// <summary>
//        /// Возвращает перечислитель коллекции.
//        /// </summary>
//        /// <returns>
//        /// Перечислитель коллекции.
//        /// </returns>
//        public IEnumerator<Module> GetEnumerator() => ((IEnumerable<Module>)_Modules).GetEnumerator();

//        /// <summary>
//        /// Возвращает перечислитель коллекции.
//        /// </summary>
//        /// <returns>
//        /// Перечислитель коллекции.
//        /// </returns>
//        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Module>)_Modules).GetEnumerator();
//    }
//}
