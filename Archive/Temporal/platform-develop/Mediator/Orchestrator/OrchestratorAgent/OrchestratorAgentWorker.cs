using System.Net;
using Apeiron.Platform.MediatorLibrary;
using Apeiron.Platform.MediatorLibrary.Messages.Requests;
using Apeiron.Platform.MediatorLibrary.Messages.Responce;

namespace OrchestratorAgent;

/// <summary>
/// Представляет службу агента установленную на хосте для управления целевыми службами.
/// </summary>
public class OrchestratorAgentWorker : BackgroundService
{
    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<OrchestratorAgentWorker> _Logger;

    /// <summary>
    /// Конечная точка сервера.
    /// </summary>
    private readonly IPEndPoint _IPEndPoint;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="logger">Логгер.</param>
    public OrchestratorAgentWorker(ILogger<OrchestratorAgentWorker> logger)
    {
        // Инициализирует логгер.
        _Logger = logger;
        // Создаёт точку для подключению к серверу.
        _IPEndPoint = new IPEndPoint(MediatorSettings.MediatorServerAddress, MediatorSettings.MediatorServerPort);
    }

    /// <summary>
    /// Основная задача службы.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        // Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Доп. задержка, для вывода изначальных сообщений в консоль.
        await Task.Delay(50, cancellationToken).ConfigureAwait(false);

        //  Запуск асинхронной работы с подключением.
        _ = Task.Run(async () =>
        {
            // Задержка имитация.
            await Task.Delay(2000, cancellationToken).ConfigureAwait(false);

            await GetInformationFromMediatorServerAsync(cancellationToken).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);

        //// Основной цикл службы.
        //while (!cancellationToken.IsCancellationRequested)
        //{
        //    //  Запуск асинхронной работы с подключением.
        //    _ = Task.Run(async () =>
        //    {
        //        await GetInformationFromMediatorServerAsync(cancellationToken).ConfigureAwait(false);
        //    }, cancellationToken).ConfigureAwait(false);

        //    // Задержка перед следующей итерацией.
        //    await Task.Delay(70000, cancellationToken).ConfigureAwait(false);
        //}
    }
   

    /// <summary>
    /// Задача получения команд с сервера.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    private async Task GetInformationFromMediatorServerAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        TestMessage testMessage = new()
        {
            Text = "Тест"
        };

        SimpleResponceMessage result = await MediatorClient.RemoteMethodCallAsync(_IPEndPoint, testMessage, cancellationToken).ConfigureAwait(false);
        if (result is not null)
            _Logger.LogInformation("Резльтат - {dataText}", result.DataText);
    }


    ///// <summary>
    ///// Задача получения команд с сервера.
    ///// </summary>
    ///// <param name="cancellationToken">Токен отмены.</param>
    //private async Task GetInformationFromMediatorServerAsync(CancellationToken cancellationToken)
    //{
    //    //  Проверка токена отмены.
    //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //    // Получаем имя хоста.
    //    HostInfo hostServiceInfo = new()
    //    {
    //        Hostname = Environment.MachineName.ToLower(),
    //        RequestTime = DateTime.Now
    //    };

    //    _Logger.LogInformation($"Отправка запроса {nameof(MediatorMethodId.GetHostInfo)} на сервер на получение команды");

    //    // Получаем результат.
    //    string? result = await MediatorTcpClient.RemoteMethodCallAsync(_IPEndPoint, MediatorMethodId.GetCommandsFromMediatorServer,
    //        async (writer, cancellationToken) =>
    //        {
    //            //  Проверка токена отмены.
    //            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //            //  Отправляем имя машины с которой отправлен запрос.
    //            await writer.WriteAsync(hostServiceInfo, cancellationToken).ConfigureAwait(false);
    //        },
    //        async (reader, cancellationToken) =>
    //        {
    //            //  Проверка токена отмены.
    //            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);


    //            //await reader.ReadInt32Async(cancellationToken).ConfigureAwait(false);
    //            await reader.ReadStringAsync(cancellationToken).ConfigureAwait(false);

    //            // Заглушка.
    //            return string.Empty;
    //        },
    //        cancellationToken);

    //    _Logger.LogInformation($"Ответ от Mediator сервера: {result}");
    //}
}
