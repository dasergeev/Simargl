using Apeiron.Platform.MediatorLibrary;
using Apeiron.Platform.MediatorLibrary.Responce;
using System.Net;

namespace Apeiron.Platform.OrchestratorServer;

/// <summary>
/// Представляет класс сервера Оркестратора.
/// </summary>
public class OrchestratorServerWorker : BackgroundService
{
    /// <summary>
    /// Конечная точка сервера.
    /// </summary>
    private readonly IPEndPoint _IPEndPoint;

    /// <summary>
    /// Представляет логгер.
    /// </summary>
    private readonly ILogger<OrchestratorServerWorker> _Logger;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="logger"></param>
    public OrchestratorServerWorker(ILogger<OrchestratorServerWorker> logger)
    {
        _Logger = logger;

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

        // Основной цикл службы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Запуск асинхронной работы с подключением.
            _ = Task.Run(async () =>
            {
                await GetInformationFromMediatorServerAsync(cancellationToken).ConfigureAwait(false);
            }, cancellationToken).ConfigureAwait(false);

            // Задержка перед следующей итерацией.
            await Task.Delay(3000, cancellationToken).ConfigureAwait(false);
        }
    }


    /// <summary>
    /// Задача получения команд с сервера.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    private async Task GetInformationFromMediatorServerAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        // Получаем имя хоста.
        HostInfo hostServiceInfo = new()
        {
            Hostname = Environment.MachineName.ToLower(),
            RequestTime = DateTime.Now
        };

        _Logger.LogInformation($"Отправка запроса {nameof(MediatorMethodId.GetHostInfo)} на сервер на получение команды");

        // Получаем результат.
        string? result = await MediatorTcpClient.RemoteMethodCallAsync(_IPEndPoint, MediatorMethodId.GetCommandsFromMediatorServer,
            async (spreader, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Отправляем имя машины с которой отправлен запрос.
                await hostServiceInfo.SaveAsync(spreader.Stream, cancellationToken).ConfigureAwait(false);
            },
            async (spreader, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);


                //await reader.ReadInt32Async(cancellationToken).ConfigureAwait(false);
                await spreader.ReadStringAsync(cancellationToken).ConfigureAwait(false);
                
                return string.Empty;
            },
            CancellationToken.None);

        _Logger.LogInformation("Ответ от Mediator сервера: {result}", result);
    }
}
