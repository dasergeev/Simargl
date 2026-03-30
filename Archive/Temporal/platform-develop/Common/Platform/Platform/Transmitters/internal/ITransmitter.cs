namespace Apeiron.Platform.Transmitters
{
    /// <summary>
    /// Представляет интерфей переадресации.
    /// </summary>
    public interface ITransmitter
    {
        /// <summary>
        /// Представляет функцию отправки массива.
        /// </summary>
        /// <param name="data">
        /// Массив
        /// </param>
        public void Send(byte[] data);

        /// <summary>
        /// Представляет функцию пересылки массива асинхронно.
        /// </summary>
        /// <param name="data">
        /// Массив
        /// </param>
        /// <param name="token">
        /// Токен отмены.
        /// </param>
        /// <returns>
        /// Задача.
        /// </returns>
        public Task SendAsync(byte[] data, CancellationToken token);

    }
}
