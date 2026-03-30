using System;
using System.IO;
using System.Text;

namespace RailTest.Satellite.Autonomic.Telemetry
{
    /// <summary>
    /// Представляет пакет данных.
    /// </summary>
    public class Package
    {
        /// <summary>
        /// Поле для хранения стандартного идентификатора.
        /// </summary>
        public const long StandardIdentifier = 0xF50844916854044;

        /// <summary>
        /// Возвращает размер пакета в байтах.
        /// </summary>
        public const int Size = 5 * sizeof(long) + 10 * Autonomic.Settings.CountSignals * sizeof(float);

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        public Package() :
            this(DateTime.Now)
        {

        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="time">
        /// Время записи.
        /// </param>
        public Package(DateTime time)
        {
            Identifier = StandardIdentifier;
            Time = time;
            Channels = new PackageValueCollection[Autonomic.Settings.CountSignals];
            for (int i = 0; i < Autonomic.Settings.CountSignals; i++)
            {
                Channels[i] = new PackageValueCollection();
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="time">
        /// Время записи.
        /// </param>
        /// <param name="speed">
        /// Скорость.
        /// </param>
        /// <param name="longitude">
        /// Долгота.
        /// </param>
        /// <param name="latitude">
        /// Широта.
        /// </param>
        public Package(DateTime time, double speed, double longitude, double latitude)
        {
            Identifier = StandardIdentifier;
            Time = time;
            Speed = speed;
            Longitude = longitude;
            Latitude = latitude;

            Channels = new PackageValueCollection[Autonomic.Settings.CountSignals];
            for (int i = 0; i < Autonomic.Settings.CountSignals; i++)
            {
                Channels[i] = new PackageValueCollection();
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="bytes">
        /// Массив байт.
        /// </param>
        public Package(byte[] bytes) :
            this()
        {
            if (bytes.Length != Size)
            {
                throw new InvalidOperationException("Неверный размер.");
            }
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, true))
                {
                    Identifier = reader.ReadInt64();
                    Time = DateTime.FromBinary(reader.ReadInt64());
                    Speed = reader.ReadDouble();
                    Longitude = reader.ReadDouble();
                    Latitude = reader.ReadDouble();

                    for (int i = 0; i < Autonomic.Settings.CountSignals; i++)
                    {
                        Channels[i].Read(reader);
                    }
                }
            }
        }

        /// <summary>
        /// Выполняет запись данных.
        /// </summary>
        /// <param name="writer">
        /// Средство записи.
        /// </param>
        public void Write(BinaryWriter writer)
        {
            writer.Write(Identifier);
            writer.Write(Time.ToBinary());
            writer.Write(Speed);
            writer.Write(Longitude);
            writer.Write(Latitude);

            for (int i = 0; i < Autonomic.Settings.CountSignals; i++)
            {
                Channels[i].Write(writer);
            }
        }

        /// <summary>
        /// Возвращает данные канала.
        /// </summary>
        /// <param name="index">
        /// Индекс канала.
        /// </param>
        /// <returns>
        /// Данные канала.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="index"/> передано значение меньшее 0 или большее 7.
        /// </exception>
        public PackageValueCollection GetChannelData(int index)
        {
            return Channels[index];
        }

        /// <summary>
        /// Возвращает идентификатор.
        /// </summary>
        public long Identifier { get; }

        /// <summary>
        /// Возвращает время записи.
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// Возвращает скорость.
        /// </summary>
        public double Speed { get; }

        /// <summary>
        /// Возвращает долготу.
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// Возвращает широту.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Возвращает значения каналов.
        /// </summary>
        public PackageValueCollection[] Channels { get; }
    }
}
