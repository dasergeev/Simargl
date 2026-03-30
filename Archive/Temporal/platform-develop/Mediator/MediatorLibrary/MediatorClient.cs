using Apeiron.Platform.MediatorLibrary.Messages.Requests;
using Apeiron.Platform.MediatorLibrary.Messages.Responce;
using System.Net;
using System.Net.Sockets;

namespace Apeiron.Platform.MediatorLibrary;

/// <summary>
/// Представляет Tcp клиент.
/// </summary>
public class MediatorClient
{
    /// <summary>
    /// Представляет Tcp клиент.
    /// </summary>
    /// <param name="iPEndPoint"></param>
    /// <param name="generalMessage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<SimpleResponceMessage> RemoteMethodCallAsync(IPEndPoint iPEndPoint,
        TestMessage generalMessage, 
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Создание TCP клиента.
        using TcpClient tcpClient = new();

        // Устанавливает соединение к удалённому TCP серверу.
        await tcpClient.ConnectAsync(iPEndPoint);

        // Сохранение и отправка сообщения.
        await generalMessage.SavePackageAsync(tcpClient.GetStream(), cancellationToken);

        // Сброс данных в поток.
        await tcpClient.GetStream().FlushAsync(cancellationToken).ConfigureAwait(false);

        // Создание результирующего сообщения.
        SimpleResponceMessage resultMessage = new();
        SimpleResponceMessage result;
        try
        {
            // Получение результата выполнения команды.
            result = (SimpleResponceMessage)await resultMessage.LoadPackageAsync(tcpClient.GetStream(), cancellationToken);
        }
        catch (AggregateException)
        {

            throw;
        }
       

        var a = result;

        // Возврат результата.
        return result;   
    }
}
