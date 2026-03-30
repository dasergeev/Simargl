using Microsoft.Extensions.Logging;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Simargl.Synergy.Hub;

partial class Worker
{
    /// <summary>
    /// Асинхронно выполняет работу с клиентом.
    /// </summary>
    /// <param name="tcpClient">
    /// Клиент.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с клиентом.
    /// </returns>
    private async Task TcpClientAsync(TcpClient tcpClient, CancellationToken cancellationToken)
    {
        //  Захват клиента.
        using (tcpClient)
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Получение потока данных TCP-соединения.
            using NetworkStream tcpStream = tcpClient.GetStream();

            //  Вывод информации в журнал.
            _Logger.LogInformation("Новое входящее TCP-подключение {point}", tcpClient.Client.RemoteEndPoint);

            //  Получение сертификата.
            X509Certificate2 certificate = X509Certificate2.CreateFromPemFile(
                Path.Combine(_Tunings.EncryptPath, "cert.pem"),
                Path.Combine(_Tunings.EncryptPath, "privkey.pem"));

            //  Получение SSL-потока.
            using SslStream sslStream = new(tcpStream);

            //  Проверка подлинности.
            await sslStream.AuthenticateAsServerAsync(certificate, false, true);

            //  Сброс данных в поток.
            await sslStream.FlushAsync(cancellationToken).ConfigureAwait(false);

            //  Вывод информации в журнал.
            _Logger.LogInformation("TCP-клиент {point} подключён с использованием SSL.", tcpClient.Client.RemoteEndPoint);

            //  Выполнение работы с SSL-потоком.
            await SslStreamAsync(sslStream, cancellationToken).ConfigureAwait(false);
        }
    }
}
