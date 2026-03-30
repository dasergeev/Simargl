using System.Net.Sockets;

namespace Simargl.Net;

/// <summary>
/// Предоставляет методы расширения для класса <see cref="TcpClient"/>.
/// </summary>
public static class TcpClientExtensions
{
    /// <summary>
    /// Выполняет проверку подключения клиента.
    /// </summary>
    /// <param name="client">
    /// Клиент для проверки.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если клиент подключён;
    /// <c>false</c> - в противном случае.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="client" /> передана пустая ссылка.
    /// </exception>
    public static bool IsConnected(this TcpClient client)
    {
        //  Проверка ссылки на объект.
        IsNotNull(client, nameof(client));

        try
        {
            //  Проверка подключения клиента.
            return !(client.Client.Poll(1000, SelectMode.SelectRead) && client.Client.Available == 0);
        }
        catch (Exception ex) when(ex is NotSupportedException || ex is SocketException || ex is ObjectDisposedException)
        {
            return false;
        }
    }
}
