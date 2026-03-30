using Simargl.Synergy.Core.Criticalling;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Simargl.Synergy.Core;

/// <summary>
/// Представляет подключение.
/// </summary>
internal sealed class Connection :
    Critical
{
    /// <summary>
    /// Поле для хранения SSL-потока.
    /// </summary>
    private SslStream _SslStream = null!;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    private Connection() :
        base()
    {

    }

    /// <summary>
    /// Возвращает SSL-поток.
    /// </summary>
    public SslStream SslStream => _SslStream;

    /// <summary>
    /// Асинхронно создаёт подключение.
    /// </summary>
    /// <param name="host">
    /// DNS-имя удаленного узла.
    /// </param>
    /// <param name="port">
    /// Номер порта удаленного узла.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая подключение.
    /// </returns>
    public static async Task<Connection> CreateAsync(
        string host, int port, CancellationToken cancellationToken)
    {
        //  Создание соединения.
        Connection connection = new();

        //  Блок перехвата всех исключений.
        try
        {
            //  Создание TCP-клиента.
            TcpClient tcpClient = new();
            await connection.AttachAsync(tcpClient).ConfigureAwait(false);

            //  Подключение TCP-клиента.
            await tcpClient.ConnectAsync(host, port, cancellationToken).ConfigureAwait(false);

            //  Получение TCP-потока.
            NetworkStream tcpStream = tcpClient.GetStream();
            await connection.AttachAsync(tcpStream).ConfigureAwait(false);

            //  Создание SSL-потока.
            connection._SslStream = new(tcpStream, false, validation);
            await connection.AttachAsync(connection._SslStream).ConfigureAwait(false);

            //  Аутентификация. 
            await connection._SslStream.AuthenticateAsClientAsync(
                new SslClientAuthenticationOptions()
                {
                    TargetHost = host,
                }, cancellationToken).ConfigureAwait(false);

            //  Сброс данных в поток.
            await connection._SslStream.FlushAsync(cancellationToken).ConfigureAwait(false);

            //  Возврат подключения.
            return connection;
        }
        catch
        {
            //  Разрушение соединения.
            await connection.DisposeAsync().ConfigureAwait(false);

            //  Повторный выброс исключения.
            throw;
        }

        //  Выполняет проверку сертификата.
        static bool validation(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            //  Проверка сертификата.
            return sslPolicyErrors == SslPolicyErrors.None;
        }
    }
}
