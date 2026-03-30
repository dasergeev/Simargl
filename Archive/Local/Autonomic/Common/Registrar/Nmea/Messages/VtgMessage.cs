using System;

namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет сообщение, содержащее данные местоположения GNSS.
    /// </summary>
    class VtgMessage : NmeaMessage
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="preamble">
        /// Преамбула сообщения.
        /// </param>
        /// <param name="pole">
        /// Курс на истинный полюс.
        /// </param>
        /// <param name="magnetic">
        /// Курс на магнитное склонение.
        /// </param>
        /// <param name="knot">
        /// Скорость в узлах.
        /// </param>
        /// <param name="speed">
        /// Скорость в километрах в час.
        /// </param>
        /// <param name="mode">
        /// Индикатор режима.
        /// </param>
        public VtgMessage(string preamble, double pole, double magnetic, double knot, double speed, string mode) :
            base(preamble, NmeaMnemonics.Vtg)
        {
            Pole = pole;
            Magnetic = magnetic;
            Knot = knot;
            Speed = speed;
            Mode = mode;
        }

        /// <summary>
        /// Возвращает курс на истинный полюс.
        /// </summary>
        public double Pole { get; }

        /// <summary>
        /// Возвращает курс на магнитное склонение.
        /// </summary>
        public double Magnetic { get; }

        /// <summary>
        /// Возвращает скорость в узлах.
        /// </summary>
        public double Knot { get; }

        /// <summary>
        /// Возвращает скорость в километрах в час.
        /// </summary>
        public double Speed { get; }

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
        internal static VtgMessage Parse(string preamble, string[] parts)
        {
            if (parts.Length != 9)
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            double pole = ToDouble(parts[0]);
            if (parts[1] != "T")
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            double magnetic = ToDouble(parts[2]);
            if (parts[3] != "M")
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            double knot = ToDouble(parts[4]);
            if (parts[5] != "N")
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            double speed = ToDouble(parts[6]);
            if (parts[7] != "K")
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            string mode = parts[8];
            return new VtgMessage(preamble, pole, magnetic, knot, speed, mode);
        }

    }
}
