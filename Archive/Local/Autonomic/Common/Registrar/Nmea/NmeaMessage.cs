using System;
using System.Globalization;
using System.Text;

namespace RailTest.Satellite.Autonomic.Registrar
{
    /// <summary>
    /// Представляет базовый класс для всех сообщений NMEA протокола.
    /// </summary>
    abstract class NmeaMessage
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="preamble">
        /// Преамбула сообщения.
        /// </param>
        /// <param name="mnemonics">
        /// Мнемоника сообщения.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="preamble"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// В параметре <paramref name="preamble"/> передана строка, длина которой не равна двум.
        /// - или -
        /// В параметре <paramref name="mnemonics"/> передано значение, которое не содержится в перечислении <see cref="NmeaMnemonics"/>.
        /// </exception>
        internal NmeaMessage(string preamble, NmeaMnemonics mnemonics)
        {
            if (preamble is null)
            {
                throw new ArgumentNullException("preamble", "Передана пуста ссылка.");
            }
            if (preamble.Length != 2)
            {
                throw new ArgumentOutOfRangeException("preamble", "Число символов не равно двум.");
            }
            Preamble = preamble;
            if (!Enum.IsDefined(typeof(NmeaMnemonics), mnemonics))
            {
                throw new ArgumentOutOfRangeException("mnemonics", "Значение не содержится в допустимом диапазоне.");
            }
            Mnemonics = mnemonics;
        }

        /// <summary>
        /// Возвращает преамбулу сообщения.
        /// </summary>
        public string Preamble { get; }

        /// <summary>
        /// Возвращает мнемонику сообщения.
        /// </summary>
        public NmeaMnemonics Mnemonics { get; }

        /// <summary>
        /// Выполняет разбор сообщения в текстовом формате.
        /// </summary>
        /// <param name="message">
        /// Сообщение, которое необходимо разобрать.
        /// </param>
        /// <returns>
        /// Сообщение NMEA протокола.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// В параметре <paramref name="message"/> передана пустая ссылка.
        /// </exception>
        /// <exception cref="FormatException">
        /// В параметре <paramref name="message"/> передана строка, которая не соответствует протоколу NMEA.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// В параметре <paramref name="message"/> передана строка с сообщением, которае не поддерживается.
        /// </exception>
        public static NmeaMessage Parse(string message)
        {
            if (message is null)
            {
                throw new ArgumentNullException("message", "Передана пустая ссылка.");
            }

            message = message.ToUpper();
            if (message.Length < 10)
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }
            if (message[0] != '$' || message[6] != ',' || message[message.Length - 3] != '*')
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            int checkSum;
            try
            {
                checkSum = int.Parse(message.Substring(message.Length - 2, 2), NumberStyles.AllowHexSpecifier);
            }
            catch (FormatException)
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            message = message.Substring(1, message.Length - 4);
            if (GetCheckSum(message) != checkSum)
            {
                throw new FormatException("Строка не соответствует протоколу NMEA.");
            }

            string preamble = message.Substring(0, 2);
            string mnemonics = message.Substring(2, 3);
            message = message.Substring(6);
            string[] parts = message.Split(',');

            switch (mnemonics)
            {
                case "GGA":
                    return GgaMessage.Parse(preamble, parts);
                case "VTG":
                    return VtgMessage.Parse(preamble, parts);
                case "RMC":
                    return RmcMessage.Parse(preamble, parts);
                default:
                    throw new NotSupportedException("Тип сообщения не поддерживается.");
            }
        }

        /// <summary>
        /// Возвращает контрольную сумму, соответствующую содержимому строки.
        /// </summary>
        /// <param name="text">
        /// Строка.
        /// </param>
        /// <returns>
        /// Контрольная сумма.
        /// </returns>
        private static int GetCheckSum(string text)
        {
            int checkSum = GetCode(text[0]);
            for (int i = 1; i != text.Length; ++i)
            {
                checkSum ^= GetCode(text[i]);
            }
            return checkSum;
        }

        /// <summary>
        /// Возвращает код символа.
        /// </summary>
        /// <param name="value">
        /// Символ.
        /// </param>
        /// <returns>
        /// Код символа.
        /// </returns>
        private static int GetCode(char value)
        {
            return Encoding.ASCII.GetBytes(new char[] { value })[0];
        }

        /// <summary>
        /// Преобразует текстовое значение в целое число.
        /// </summary>
        /// <param name="value">
        /// Текстовое значение.
        /// </param>
        /// <param name="message">
        /// Строка с описанием ошибки.
        /// </param>
        /// <returns>
        /// Целое число.
        /// </returns>
        internal static int ToInt32(string value, string message = "Строка не соответствует протоколу NMEA.")
        {
            try
            {
                return int.Parse(value);
            }
            catch (FormatException)
            {
                throw new FormatException(message);
            }
        }

        /// <summary>
        /// Возвращает сведения о языке, который используется для перевода чисел.
        /// </summary>
        internal static CultureInfo CultureInfo { get; } = CultureInfo.CreateSpecificCulture("en-US");

        /// <summary>
        /// Преобразует текстовое значение в число.
        /// </summary>
        /// <param name="value">
        /// Текстовое значение.
        /// </param>
        /// <param name="message">
        /// Строка с описанием ошибки.
        /// </param>
        /// <returns>
        /// Число.
        /// </returns>
        internal static double ToDouble(string value, string message = "Строка не соответствует протоколу NMEA.")
        {
            try
            {
                return double.Parse(value, CultureInfo);
            }
            catch (FormatException)
            {
                throw new FormatException(message);
            }
        }
    }
}
