using Apeiron.IO;
using Apeiron.Platform.MediatorLibrary;
using Apeiron.Platform.MediatorLibrary.Requests;
using Apeiron.Platform.MediatorLibrary.Responce;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text;

namespace Apeiron.Platform.MediatorServer;

/// <summary>
/// Представляет класс для работы с подключением к Mediator серверу.
/// </summary>
internal sealed class MediatorConnection
{
    /// <summary>
    /// Логгер.
    /// </summary>
    internal ILogger<MediatorServerWorker> _Logger;

    /// <summary>
    /// Список команд.
    /// </summary>
    internal RequestCollection RequestsList { get; private set; } = new(new List<Request>());

    /// <summary>
    /// Потокобезопасный словарь для хранения актуального списка хостов.
    /// </summary>
    internal ConcurrentDictionary<string, HostInfo> HostServiceCollection { get; private set; } = new();


    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="logger">Логгер.</param>
    public MediatorConnection(ILogger<MediatorServerWorker> logger)
    {
        _Logger = logger;
    }

    /// <summary>
    /// Ассинхронный обработчик подключившегося клиента.
    /// </summary>
    /// <param name="tcpClient">Клиентское подключение.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Задача, выполняющая работу с клиентом.</returns>
    /// <exception cref="OperationCanceledException">Операция отменена.</exception>
    internal async Task HandleConnectionAsync(TcpClient tcpClient, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Для гарантированного освобождения TCP-клиента.
        using TcpClient client = tcpClient;

        //  Создание распределителя данных потока.
        Spreader spreader = new(tcpClient.GetStream(), Encoding.UTF8);

        // Получение ID метода.
        MediatorMethodId method = (MediatorMethodId)await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        foreach (var hostListItem in HostServiceCollection)
        {
            _Logger.LogInformation("Список хостов: {hostname}, {requestTime}", hostListItem.Value.Hostname, hostListItem.Value.RequestTime);
        }

        // Обработка методов.
        switch (method)
        {
            case MediatorMethodId.CheckConnectionToMediator:
                {
                    string result;

                    try
                    {                    
                        result = await MediatorMethods.TestConnectionToMediatorAsync(cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        await spreader.WriteInt32Async((int)MediatorResult.Error, cancellationToken).ConfigureAwait(false);
                        await spreader.WriteStringAsync(ex.ToString(), cancellationToken).ConfigureAwait(false);
                        return;
                    }

                    await spreader.WriteInt32Async((int)MediatorResult.Data, cancellationToken).ConfigureAwait(false);
                    await spreader.WriteStringAsync(result, cancellationToken).ConfigureAwait(false);

                    break;
                }
            case MediatorMethodId.GetCommandsFromMediatorServer:
                {
                    // Получаем хост.
                    HostInfo hostServiceInfo = await new HostInfo().LoadAsync(spreader.Stream, cancellationToken).ConfigureAwait(false);
                    
                    // Добавляем или обновляем словарь содержаций список хостов.
                    HostServiceCollection.AddOrUpdate(hostServiceInfo.Hostname, hostServiceInfo, (k,v) => hostServiceInfo);

                    //// Отправка списка
                    //foreach (var item in RequestsList)
                    //{
                    //    if (item. == hostServiceInfo.Hostname)
                    //    {

                    //    }
                    //}

                    break;
                }
            case MediatorMethodId.GetHostListFromMediatorServer:
                {
                    try
                    {
                        await spreader.WriteInt32Async((int)MediatorResult.Data, cancellationToken).ConfigureAwait(false);
                        await spreader.WriteInt32Async(HostServiceCollection.Count, cancellationToken).ConfigureAwait(false);
                        
                        foreach (var itemHostInfo in HostServiceCollection)
                        {
                            await itemHostInfo.Value.SaveAsync(spreader.Stream, cancellationToken).ConfigureAwait(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        await spreader.WriteInt32Async((int)MediatorResult.Error, cancellationToken).ConfigureAwait(false);
                        await spreader.WriteStringAsync(ex.ToString(), cancellationToken).ConfigureAwait(false);
                        return;
                    }

                    break;
                }
            case MediatorMethodId.GetHostInfo:
                {
                    string askHostName = await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

                    RequestsList.Add(new GetHostInfo()
                    {
                        Hostname = askHostName,
                        MediatorMethodId = MediatorMethodId.GetHostInfo
                    });


                    // Отправка результатов выполнения команды.

                    
                    // Заглушка имитатор.
                    await Task.Delay(5000, cancellationToken).ConfigureAwait(false);

                    break;
                }
            default:
                break;
        }
    }
}
