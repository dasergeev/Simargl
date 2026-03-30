using Apeiron.Platform.MediatorLibrary;
using System.Net;
using System.Net.Sockets;

namespace Apeiron.Platform.MediatorServer;

/// <summary>
/// Служба WorkerService.
/// </summary>
public sealed class MediatorServerWorker : BackgroundService
{
    /// <summary>
    /// Представляет логгер.
    /// </summary>
    private readonly ILogger<MediatorServerWorker> _Logger;

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="logger">Логгер.</param>
    public MediatorServerWorker(ILogger<MediatorServerWorker> logger)
    {
        _Logger = logger;
    }

    /// <summary>
    /// Выполняет основное действие службы.
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
            // TCP сервер
            TcpListener? tcpListener = null;

            try
            {
                // Создание и конфигурирование TCP сервера.
                tcpListener = new(IPAddress.Any, MediatorSettings.MediatorServerPort);

                // Запуск TCP сервера.
                tcpListener.Start();

                //// Создаём класс обслуживающий подключения.
                //MediatorConnection mediatorConnection = new(_Logger);

                MediatorConnectionManager mediatorConnectionManager = new(_Logger);

                // Вывод записи в журнал.
                _Logger.LogInformation("Запуск прослушивания сети {endpoint}", tcpListener.LocalEndpoint);

                // Цикл ожидания входящих подключений.
                while (!cancellationToken.IsCancellationRequested)
                {
                    // Получение входящего подключения.
                    TcpClient client = await tcpListener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

                    // Вывод записи в журнал.
                    _Logger.LogInformation("Новое входящее подключение {endPoint}", client.Client.RemoteEndPoint);

                    //  Запуск асинхронной работы с подключением.
                    _ = Task.Run(async () =>
                    {
                        await MediatorConnectionManager.HandleConnectionAsync(client, cancellationToken).ConfigureAwait(false);
                    }, cancellationToken).ConfigureAwait(false);

                }
            }
            catch (Exception ex)
            {
                //  Проверка остановки службы.
                if (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        //Вывод информации в журнал.
                        _Logger.LogError("{exception}", ex);
                    }
                    catch (Exception e)
                    {
                        //  Проверка критического исключения.
                        if (e.IsCritical())
                        {
                            //  Повторный выброс исключения.
                            throw;
                        }
                    }
                }

                //  Проверка критического исключения.
                if (ex.IsCritical())
                {
                    //  Повторный выброс исключения.
                    throw;
                }
            }
            finally
            {
                try
                {
                    // Останавливает работу сервера TCP.
                    tcpListener?.Stop();
                }
                catch (Exception ex)
                {
                    //Вывод информации в журнал.
                    _Logger.LogError("{exception}", ex);
                }
            }

            // Задержка перед следующей итерацией для переключения потока.
            await Task.Delay(3000, cancellationToken);
        }
    }
}