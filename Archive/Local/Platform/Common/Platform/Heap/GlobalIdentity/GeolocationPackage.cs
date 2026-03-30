namespace ExternalPackage
{
    /// <summary>
    /// Представляет пакет, сообщающий данные GPS и состояние Teltonika, полученные по Modbus.
    /// </summary>
    public class GeolocationPackage
    {
        /// <summary>
        /// Постоянная, определяющая размер заголовка.
        /// </summary>
        private const int _SizeOfHeader = sizeof(ulong) + sizeof(int);

        /// <summary>
        /// Постоянная, определяющая неизвестную версию.
        /// </summary>
        private const int _UnknownVersion = 0x00;

        /// <summary>
        /// Постоянная, определяющая сигнатуру пакета.
        /// </summary>
        private const ulong _Signature = 0xB603CF2C3AA44759;
        
        /// <summary>
        /// Возвращает версию пакета.
        /// </summary>
        public int Version { get; private set; }

        /// <summary>
        /// Возвращает широту.
        /// </summary>
        public double Latitude { get; private set; }

        /// <summary>
        /// Возвращает долготу.
        /// </summary>
        public double Longitude { get; private set; }

        /// <summary>
        /// Возвращает скорость.
        /// </summary>
        public int Speed { get; private set; }

        /// <summary>
        /// Возвращает флаг достоверности GPS данных.
        /// </summary>
        public bool IsNewAndValideGps { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="version">
        /// Версия пакета.
        /// </param>
        private GeolocationPackage(int version)
        {
            //  Установка версии пакета.
            Version = version;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="latitude">Широта.</param>
        /// <param name="longitude">Долгота.</param>
        /// <param name="speed">Скорость.</param>
        /// <param name="isNewAndValideGps">Флаг достоверности GPS данных.</param>
        /// <exception cref="ArgumentOutOfRangeException">В параметре <paramref name="speed"/> передано отрицательное значение.</exception>
        public GeolocationPackage(double latitude, double longitude, int speed, bool isNewAndValideGps)
        {
            // Проверка значения скорости и инициализация свойства.
            Speed = Check.IsNotNegative(speed, nameof(speed));

            //  Инициализация версии пакета.
            Version = 0x01;

            //  Инициализация широты.
            Latitude = latitude;

            //  Инициализация долготы.
            Longitude = longitude;

            //  Инициализация флага.
            IsNewAndValideGps = isNewAndValideGps;
        }

        /// <summary>
        /// Представляет функцию получения пакета из массива байт.
        /// </summary>
        /// <param name="datagram">Массив байт.</param>
        /// <param name="package">Полученый пакет.</param>
        /// <returns>true - в случае успеха разбор, false - в случае неудачи.</returns>
        public static bool TryParse(byte[] datagram, out GeolocationPackage package)
        {
            //  Инициализация пакета.
            package = new(_UnknownVersion);

            //  Проверка пустой ссылки.
            if (datagram is null)
            {
                return false;
            }    

            //  Проверка минимальной длинны пакета.
            if (datagram.Length < _SizeOfHeader)
            {
                return false;
            }
            //  Создание пакета.

            //  Создание потока для чтения из памяти.
            using MemoryStream stream = new(datagram);

            //  Создание средства чтения двоичных данных.
            using BinaryReader reader = new(stream);

            //  Проверка сигнатуры пакета.
            if (reader.ReadUInt64() != _Signature)
            {
                //  Датаграмма не содержит пакет.
                return false;
            }
            
            //  Чтение версии.
            package.Version = reader.ReadInt32();


            //  В случае версии #1
            if (package.Version >= 0x01)
            {
                //  Проверка длинны.
                if (datagram.Length < Size(package.Version))
                {
                    //  Датаграмма не содержит пакет.
                    return false;
                }

                //  Чтение широты.
                package.Latitude = reader.ReadDouble();

                //  Чтение долготы.
                package.Longitude = reader.ReadDouble();

                //  Чтение скорости.
                package.Speed = reader.ReadInt32();

                //  Чтение флага достоверности данных
                package.IsNewAndValideGps = reader.ReadBoolean();

                //  Возвращение значения.
                return true;
            }



            //  Датаграмма не содержит пакет.
            return false;

        }


        /// <summary>
        /// Сохраняет пакет в массив байт.
        /// </summary>
        /// <returns>Массив байт содержащих пакет.</returns>
        public byte[] GetDatagram()
        {
            //  Создание потока для чтения из памяти.
            using MemoryStream stream = new();

            //  Создание средства чтения двоичных данных.
            using BinaryWriter writer = new(stream);

            //  Запись заголовка.
            writer.Write(_Signature);
            writer.Write(Version);

            //  В случае версии #1 
            if (Version >= 0x01)
            {
                //  Запись широты.
                writer.Write(Latitude);

                //  Запись долготы.
                writer.Write(Longitude);

                //  Запись скорости
                writer.Write(Speed);

                //  Запись флага достоверности данных.
                writer.Write(IsNewAndValideGps);
            }

            return stream.ToArray();
        }

        /// <summary>
        /// Возвращает размер пакета.
        /// </summary>
        /// <param name="version">Версия.</param>
        /// <returns>Размер пакета.</returns>
        public static int Size(int version)
        { 
            //  Инициализация пакета 
            GeolocationPackage package = new(version);

            // Запись
            byte[] data = package.GetDatagram();

            //  Воврат полученной длинны.
            return data.Length;
        }
    }
}
