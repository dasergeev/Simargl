using Apeiron.Platform.MediatorLibrary.Messages;
using Apeiron.Platform.MediatorLibrary.Messages.Requests;
using Apeiron.Platform.MediatorLibrary.Messages.Responce;
using System.Net.Sockets;

namespace Apeiron.Platform.MediatorServer;

/// <summary>
/// Представляет класс для управлением подключением к Mediator.
/// </summary>
public class MediatorConnectionManager
{
    ///// <summary>
    ///// Потокобезопасный словарь сообщений.
    ///// </summary>
    //private static readonly MediatorMessageCollection MessageList = new();

    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<MediatorServerWorker> _Logger;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public MediatorConnectionManager(ILogger<MediatorServerWorker> logger)
    {
        _Logger = logger;

        //  Для анализатора.
        _ = _Logger;
    }


    /// <summary>
    /// Обработчик полключения клиента.
    /// </summary>
    /// <param name="tcpClient">Клиент.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    internal static async Task HandleConnectionAsync(TcpClient tcpClient, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Для гарантированного освобождения TCP-клиента.
        using TcpClient client = tcpClient;
        
        // Создание входящего сообщения.
        GeneralMessage message = new TestMessage();

        try
        {
            await message.LoadPackageAsync(tcpClient.GetStream(), cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }

        MessageType messageType = (MessageType)message.Type;

        switch (messageType)
        {
            case MessageType.TestTextMessage:
                {
                    try
                    {
                        //Здесь может быть какое-то действие.
                        SimpleResponceMessage simpleResponceMessage = new()
                        {
                            Type = (byte)MessageType.DataResponceMessage,
                            DataText = "The World is Mine!"
                        };

                        throw new FormatException("Error!!!");

                        //await simpleResponceMessage.SavePackageAsync(tcpClient.GetStream(), cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        SimpleResponceMessage errorSimpleResponceMessage = new()
                        {
                            Type = (byte)MessageType.DataResponceMessage,
                            DataText = ex.Message
                        };

                        await errorSimpleResponceMessage.SavePackageAsync(tcpClient.GetStream(), cancellationToken).ConfigureAwait(false);
                    }

                    break;
                }
            default:
                break;
        }
    }
}
