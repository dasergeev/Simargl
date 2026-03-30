using Apeiron.IO;
using System.Net.Sockets;
using System.Text;

namespace Apeiron.Platform.Transmitters
{
    /// <summary>
    /// Представляет класс передатчика TcpClient.
    /// </summary>
    internal class TcpClientTransmitter : IDisposable, ITransmitter
    {

        private bool _Disposed = false;

        /// <summary>
        /// Представляет интерфейс подключения.
        /// </summary>
        private readonly TcpClient _Client;

        /// <summary>
        /// Представляет интерфейс подключения.
        /// </summary>
        private readonly Stream _Stream;

        /// <summary>
        /// Представляет событие рассоединения.
        /// </summary>
        internal event EventHandler? Disconnected;

        /// <summary>
        /// Инициализирует экземпляр класса.
        /// </summary>
        /// <param name="client">Подключение.</param>
        internal TcpClientTransmitter(TcpClient client)
        {
            // Проверка и установка значения подключения.
            _Client = Check.IsNotNull(client,nameof(client));

            //  Получение потока клиента.
            _Stream = client.GetStream();

        }

        /// <summary>
        /// Предатавляет реализацию интерфейса <see cref="IDisposable"/>
        /// </summary>
        public void Dispose()
        {
            if (_Disposed)
            {
                return;
            }

            _Client.Close();
            _Client.Dispose();

            _Disposed = true;

            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Представляет функцию получения сообщений.
        /// </summary>
        /// <returns>Задача.</returns>
        internal async Task DummyRoutineAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    Spreader spreader = new(_Stream, Encoding.UTF8);

                    var dummy = await spreader.ReadBytesAsync(1024, token).ConfigureAwait(false);

                    await Task.Delay(1, token).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                //  Проверка исключения
                if (ex.IsSystem())
                {
                    //  Перенаправление исключения
                    throw;
                }
            }
            finally
            {
                //  Вызов события.
                OnDisconnected();
            }

        }

        /// <summary>
        /// Представляет функцию вызова события <see cref="Disconnected"/>
        /// </summary>
        private void OnDisconnected()
        {
            //   Вызов события.
            Disconnected?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Представляет функцию отправки массива.
        /// </summary>
        /// <param name="data">
        /// Массив
        /// </param>
        public void Send(byte[] data)
        {
            try
            {
                //  Оправка сообщения.
                _Stream.Write(data);
            }
            catch (Exception ex)
            {
                //  Проверка исключения
                if (ex.IsSystem())
                {
                    //  Перенаправление исключения
                    throw;
                }
            }
        }

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
        public async Task SendAsync(byte[] data, CancellationToken token)
        {
            try
            {
                //  Оправка сообщения.
                await _Stream.WriteAsync(data, token).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //  Проверка исключения
                if (ex.IsSystem())
                {
                    //  Перенаправление исключения
                    throw;
                }
            }
        }
    }
}
