using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Simargl.Net;

/// <summary>
/// Предоставляет методы расширения для класса <see cref="TcpListener"/>.
/// </summary>
public static class TcpListenerExtensions
{
    /// <summary>
    /// Проверяет порт на доступность.
    /// </summary>
    /// <param name="listener">
    /// The listener.
    /// </param>
    /// <param name="port">
    /// Номер порта.
    /// </param>
    /// <returns>
    /// true - если порт доступен, false в противном случае.
    /// </returns>
    public static bool PortIsBusy(this TcpListener listener, int port)
    {
        _ = listener;
        try
        {
            IPEndPoint[] endpoints = IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            if (endpoints.Length > 0)
            {
                foreach (IPEndPoint endpoint in endpoints)
                {
                    if (endpoint.Port == port)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
}
