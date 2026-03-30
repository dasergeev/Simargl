using System.Collections.Generic;

namespace Simargl.Platform.Transmitters
{
    /// <summary>
    /// Представляет конфигурацию передатчиков.
    /// </summary>
    public class TransmittersOptions
    {
        /// <summary>
        /// Возвращает и устанавливает порт сервера передачи TcpServer.
        /// </summary>
        public int TcpServerPort { get; set; }

        /// <summary>
        /// Возвращает и устанавливает список адресов передачи по Udp.
        /// </summary>
        public List<TransmitterEndPoint>? UdpEndPoint { get; set; }

        /// <summary>
        /// Возвращает и устанавливает корневой каталог для записи данных.
        /// </summary>
        public string? RootPath { get; set; } = @"D:/Teltonika";

        /// <summary>
        /// Возвращает и устанавливает целевой каталог для записи данных.
        /// </summary>
        public string? FolderName { get; set; } = @"Nmea";

        /// <summary>
        /// Возвращает и устанавливает расширение файлов записи.
        /// </summary>
        public string? FileExtension { get; set; }

    }
}
