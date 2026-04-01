using System.Net;
using System.Net.Sockets;

namespace Simargl.Hardware.Receiving.Net;

/// <summary>
/// Представляет приёмник UDP-датаграмм.
/// </summary>
/// <param name="localEndPoint">
/// Локальная конечная точка сокета.
/// Определяет IP-адрес и номер порта, на которых клиент принимает входящие UDP-датаграммы.
/// </param>
/// <param name="cancellationToken">
/// Токен отмены.
/// </param>
public sealed class UdpDataReceiver(IPEndPoint localEndPoint, CancellationToken cancellationToken) :
    DataReceiver(cancellationToken)
{
    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токена отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override sealed async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Создание клиента.
        using UdpClient client = new(localEndPoint);

        //  Основной цикл получения UPD-датаграмм.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Получение датаграммы.
            UdpReceiveResult result = await client.ReceiveAsync(cancellationToken).ConfigureAwait(false);

            //  Определение времени получения.
            DateTime time = DateTime.Now;

            //  Создание результата получения датаграммы.
            UdpDataReceiveResult data = new(time, result);

            //  Создание аргументов события.
            DataReceiverReceivedEventArgs e = new(data);

            //  Вызов события.
            OnReceived(e);
        }
    }
}
