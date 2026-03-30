//using System.Collections.Generic;

//namespace RailTest.Border
//{
//    /// <summary>
//    /// Предоставляет адреса модулей.
//    /// </summary>
//    public static class ModuleAddresses
//    {
//        private static readonly string[] _Addresses;
//        private static readonly Dictionary<string, int> _Indices;

//        static ModuleAddresses()
//        {
//            _Addresses = new string[Receiver.ClientsCount];
//            _Indices = new Dictionary<string, int>();
//            for (int i = 0; i != Receiver.ClientsCount; ++i)
//            {
//                int index = i;
//                index += 2;
//                switch (index)
//                {
//                    case 26:
//                        index = 86;
//                        break;
//                    case 27:
//                        index = 87;
//                        break;
//                }
//                string address = "192.168.1." + index.ToString();
//                _Addresses[i] = address;
//                _Indices.Add(address, i);
//            }
//        }

//        /// <summary>
//        /// Возвращает адрес устройства.
//        /// </summary>
//        /// <param name="index">
//        /// Индекс.
//        /// </param>
//        /// <returns>
//        /// Адрес устройства.
//        /// </returns>
//        public static string GetAddress(int index)
//        {
//            return _Addresses[index];
//        }

//        /// <summary>
//        /// Возвращает индекс устройства.
//        /// </summary>
//        /// <param name="address">
//        /// Адрес устройства.
//        /// </param>
//        /// <returns>
//        /// Индекс.
//        /// </returns>
//        public static int GetIndex(string address)
//        {
//            if (_Indices.ContainsKey(address))
//            {
//                return _Indices[address];
//            }
//            return -1;
//        }
//    }
//}
