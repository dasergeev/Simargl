using System;

namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет сообщение, содержащее данные местоположения.
    /// </summary>
    class GgaMessage : NmeaMessage
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="preamble">
        /// Преамбула сообщения.
        /// </param>
        /// <param name="time">
        /// Время определения координат.
        /// </param>
        /// <param name="latitude">
        /// Широта.
        /// </param>
        /// <param name="longitude">
        /// Долгота.
        /// </param>
        /// <param name="solution">
        /// Тип решения.
        /// </param>
        /// <param name="satellites">
        /// Количество спутников в решении.
        /// </param>
        /// <param name="precision">
        /// Снижение точности в горизонтальной плоскости.
        /// </param>
        /// <param name="altitude">
        /// Высота над средним уровнем моря.
        /// </param>
        /// <param name="deviation">
        /// Отклонение геоида.
        /// </param>
        /// <param name="age">
        /// Возраст дифференциальных поправок
        /// </param>
        /// <param name="id">
        /// Идентификатор дифференциальной станции.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="preamble"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="preamble"/> передана строка, длина которой не равна двум.
        /// </exception>
        public GgaMessage(string preamble, DateTime time, double latitude, double longitude,
            int solution, int satellites, double precision, double altitude, double deviation,
            double age, int id) :
            base(preamble, NmeaMnemonics.Gga)
        {
            Time = time;
            Latitude = latitude;
            Longitude = longitude;
            Solution = solution;
            Satellites = satellites;
            Precision = precision;
            Altitude = altitude;
            Deviation = deviation;
            Age = age;
            ID = id;
        }

        /// <summary>
        /// Возвращает время определения координат.
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// Возвращает широту.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Возвращает долготу.
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// Возвращает тип решения.
        /// </summary>
        public int Solution { get; }

        /// <summary>
        /// Возвращает количество спутников в решении.
        /// </summary>
        public int Satellites { get; }

        /// <summary>
        /// Возвращает снижение точности в горизонтальной плоскости.
        /// </summary>
        public double Precision { get; }

        /// <summary>
        /// Возвращает высоту над средним уровнем моря.
        /// </summary>
        public double Altitude { get; }

        /// <summary>
        /// Возвращает отклонение геоида.
        /// </summary>
        public double Deviation { get; }

        /// <summary>
        /// Возвращает возраст дифференциальных поправок.
        /// </summary>
        public double Age { get; }

        /// <summary>
        /// Возвращает идентификатор дифференциальной станции.
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Выполняет разбор текстовых строк.
        /// </summary>
        /// <param name="preamble">
        /// Преамбула сообщения.
        /// </param>
        /// <param name="parts">
        /// Текстовые строки.
        /// </param>
        /// <returns>
        /// Сообщение, содержащее данные местоположения.
        /// </returns>
        /// <exception cref="FormatException">
        /// В параметре <paramref name="parts"/> переданы строки, которые не соответствует протоколу NMEA.
        /// </exception>
        internal static GgaMessage Parse(string preamble, string[] parts)
        {
            if (parts.Length != 14)
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            if (parts[0].Length != 9 && parts[0][6] != '.')
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            DateTime time = DateTime.UtcNow;
            try
            {
                int hour = ToInt32(parts[0].Substring(0, 2));
                int minute = ToInt32(parts[0].Substring(2, 2));
                int second = ToInt32(parts[0].Substring(4, 2));
                int millisecond = ToInt32(parts[0].Substring(7, 2));
                time = new DateTime(time.Year, time.Month, time.Day, hour, minute, second, millisecond);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            if (parts[1].Length < 7 && parts[1][5] != '.')
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }
            double latitude = ToInt32(parts[1].Substring(0, 2)) + ToDouble(parts[1].Substring(2)) / 60.0;

            switch (parts[2])
            {
                case "N":
                    break;
                case "S":
                    latitude = -latitude;
                    break;
                default:
                    throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            if (parts[3].Length < 7 && parts[3][6] != '.')
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }
            double longitude = ToInt32(parts[3].Substring(0, 3)) + ToDouble(parts[3].Substring(3)) / 60.0;

            switch (parts[4])
            {
                case "E":
                    break;
                case "W":
                    longitude = -longitude;
                    break;
                default:
                    throw new FormatException("Строка не соответствует протоколу NMEA.");
            }
            
            int solution = ToInt32(parts[5]);
            int satellites = ToInt32(parts[6]);
            double precision = ToDouble(parts[7]);
            double altitude = ToDouble(parts[8]);
            if (parts[9] != "M")
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }
            double deviation = ToDouble(parts[10]);
            if (parts[11] != "M")
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            double age = parts[12] != "" ? ToDouble(parts[12]) : 0.0;
            int id = parts[13] != "" ? ToInt32(parts[13]) : 0;

            return new GgaMessage(preamble,
                time, latitude, longitude, solution, satellites,
                precision, altitude, deviation, age, id);
        }
    }
}
