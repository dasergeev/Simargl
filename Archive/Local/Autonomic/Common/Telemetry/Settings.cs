using System.Net;

namespace RailTest.Satellite.Autonomic.Telemetry
{
    /// <summary>
    /// Представляет настройки.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Возвращает порт для подключения к серверу.
        /// </summary>
        public const int TcpPort = 7002;

        /// <summary>
        /// Возвращает порт для подключения клиента.
        /// </summary>
        public const int ClientPort = 7003;

        /// <summary>
        /// Возвращает адрес сервера.
        /// </summary>
        public static IPAddress Address { get; } = IPAddress.Parse("176.114.211.115");
    }
}
