using Apeiron.Platform.Transmitters;

namespace TeltonikaService
{
    /// <summary>
    /// Представляет класс настроек программы.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Возвращает и устанавливает сетевые параметр IP.
        /// </summary>
        public string? TetlonikaIP { get; set; }

        /// <summary>
        /// Возвращает и устанавливает сетевые параметр SSH.
        /// </summary>
        public string? TeltonikaPassword { get; set; }

        /// <summary>
        /// Возвращает и устанавливает номер порта, по которому программа получает NMEA данные.
        /// </summary>
        public int NmeaUdpPort { get; set; }

        /// <summary>
        /// Возвращает и устанавливает номер порта, по которому программа получает GeolocationInfo данные.
        /// </summary>
        public int GeolocationUdpPort { get; set; }

        /// <summary>
        /// Возвращает и устанавливает минимальный уровень сигнала при котором должен быть доступен интернет.
        /// </summary>
        public int Strength { get; set; }   

        /// <summary>
        /// Представляет конфигурацию передачи Nmea.
        /// </summary>
        public TransmittersOptions? NmeaTransmitteOptions { get; set; }

        /// <summary>
        /// Представляет конфигурацию передачи обработанных данных геолокации.
        /// </summary>
        public TransmittersOptions? GeoloctionTransmitteOptions { get; set; }

        /// <summary>
        /// Представляет конфигурацию передачи данных модема.
        /// </summary>
        public TransmittersOptions? ModemsTransmitteOptions { get; set; }
    }
}

