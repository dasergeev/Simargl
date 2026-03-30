namespace Apeiron.Platform.TcpClientReceiver
{
    internal class TcpReceiverOptions
    {

        /// <summary>
        /// Возвращает размер порции данных.
        /// </summary>
        public int PortionSize { get; set; }

        /// <summary>
        /// Возвращает размера буфера.
        /// </summary>
        public int BufferSize { get; set; }

        /// <summary>
        /// Возвращает время ожидания данных в милисекундах.
        /// </summary>
        public TimeSpan Timeout { get; set; }
    }
}
