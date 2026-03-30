using System;

namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет сообщение, содержащее минимальный рекомендованный набор данных.
    /// </summary>
    class RmcMessage : NmeaMessage
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
        /// <param name="status">
        /// Статус.
        /// </param>
        /// <param name="latitude">
        /// Широта.
        /// </param>
        /// <param name="longitude">
        /// Долгота.
        /// </param>
        /// <param name="knot">
        /// Скорость в узлах.
        /// </param>
        /// <param name="pole">
        /// Курс на истинный полюс.
        /// </param>
        /// <param name="magnetic">
        /// Курс на магнитное склонение.
        /// </param>
        /// <param name="mode">
        /// Индикатор режима.
        /// </param>
        public RmcMessage(string preamble, DateTime time, string status, double latitude, double longitude,
            double knot, double pole, double magnetic, string mode) :
            base(preamble, NmeaMnemonics.Rmc)
        {
            Time = time;
            Status = status;
            Latitude = latitude;
            Longitude = longitude;
            Knot = knot;
            Pole = pole;
            Magnetic = magnetic;
            Mode = mode;
        }

        /// <summary>
        /// Возвращает время определения координат.
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// Возвращает статус.
        /// </summary>
        public string Status { get; }
        
        /// <summary>
        /// Возвращает широту.
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Возвращает долготу.
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// Возвращает скорость в узлах.
        /// </summary>
        public double Knot { get; }

        /// <summary>
        /// Возвращает скорость в километрах в час.
        /// </summary>
        public double Speed => Knot * 1.852;

        /// <summary>
        /// Возвращает курс на истинный полюс.
        /// </summary>
        public double Pole { get; }

        /// <summary>
        /// Возвращает курс на магнитное склонение.
        /// </summary>
        public double Magnetic { get; }

        /// <summary>
        /// Возвращает индикатор режима.
        /// </summary>
        public string Mode { get; }

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
        internal static RmcMessage Parse(string preamble, string[] parts)
        {
            if (parts.Length != 12)
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            if (parts[0].Length != 9 && parts[0][6] != '.')
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            DateTime time;
            try
            {
                int year = 2000 + ToInt32(parts[8].Substring(4, 2));
                int month = ToInt32(parts[8].Substring(2, 2));
                int day = ToInt32(parts[8].Substring(0, 2));
                int hour = ToInt32(parts[0].Substring(0, 2));
                int minute = ToInt32(parts[0].Substring(2, 2));
                int second = ToInt32(parts[0].Substring(4, 2));
                int millisecond = ToInt32(parts[0].Substring(7, 2));
                time = new DateTime(year, month, day, hour, minute, second, millisecond);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            string status = parts[1];

            if (parts[2].Length < 7 && parts[2][5] != '.')
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }
            double latitude = ToInt32(parts[2].Substring(0, 2)) + ToDouble(parts[2].Substring(2)) / 60.0;

            switch (parts[3])
            {
                case "N":
                    break;
                case "S":
                    latitude = -latitude;
                    break;
                default:
                    throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            if (parts[4].Length < 7 && parts[4][6] != '.')
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }
            double longitude = ToInt32(parts[4].Substring(0, 3)) + ToDouble(parts[4].Substring(3)) / 60.0;

            switch (parts[5])
            {
                case "E":
                    break;
                case "W":
                    longitude = -longitude;
                    break;
                default:
                    throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            double knot = ToDouble(parts[6]);
            double pole = ToDouble(parts[7]);
            double magnetic = ToDouble(parts[9]);
            string mode = parts[11];

            return new RmcMessage(preamble, time, status, latitude, longitude, knot, pole, magnetic, mode);
        }
    }
}
