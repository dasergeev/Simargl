namespace PointerDataViewer
{
    /// <summary>
    /// Представляет класс настроек программы.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Постоянная, определяющая номер порта для подключения датчиков Adxl.
        /// </summary>
        public int AdxlStreamPort { get; set; }

        /// <summary>
        /// Постоянная, определяющая номер порта для подключения датчиков MTP.
        /// </summary>
        public int MtpStreamPort { get; set; }

        /// <summary>
        /// Возвращает и устанавливает список пересылки Nmea пакетов.
        /// </summary>
        public string[]? MtpIPAddress { get; set; }

        /// <summary>
        /// Возвращает и устанавливает список пересылки Modbus данных.
        /// </summary>
        public string[]? AdxlIPAddress { get; set; }
    }
}

