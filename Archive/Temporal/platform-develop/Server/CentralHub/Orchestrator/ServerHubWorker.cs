//using Apeiron.Platform.Server.Services.Orchestrator.Packages;
//using System.Collections.Concurrent;
//using System.Net;
//using System.Net.Sockets;

//namespace Apeiron.Platform.Server.Services.Orchestrator.OrchestratorHub;

///// <summary>
///// Представяет микрослужбу, выполняющую управление хостами и развёртыванием ПО на хостах.
///// </summary>
//public class ServerHubWorker : BackgroundService
//{
//    /// <summary>
//    /// Порт TCP сервера.
//    /// </summary>
//    private const int _Port = 48888;

//    /// <summary>
//    /// Список подключенных клиентов к серверу оркестрации.
//    /// </summary>
//    private readonly List<HostClient> _TcpClientsList;

//    /// <summary>
//    /// Список подключенных клиентов к серверу оркестрации.
//    /// </summary>
//    private readonly ConcurrentDictionary<EndPoint, (string, HostClient)> _ConnectedClientsList;

//    /// <summary>
//    /// Логгер.
//    /// </summary>
//    private readonly ILogger<ServerHubWorker> _Logger;

//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    /// <param name="logger">
//    /// Средство записи в журнал.
//    /// </param>
//    public ServerHubWorker(ILogger<ServerHubWorker> logger)
//    {        
//        _Logger = logger;
//        _TcpClientsList = new List<HostClient>();
//        _ConnectedClientsList = new();
//    }

//    /// <summary>
//    /// Асинхронно выполняет основную фоновую работу микрослужбы.
//    /// </summary>
//    /// <param name="stoppingToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая основную фоновую работу микрослужбы.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(stoppingToken).ConfigureAwait(false);

//        // Задержка для инициализации консоли и выдачи служебных сообщений.
//        await Task.Delay(50, stoppingToken).ConfigureAwait(false);


//        // Основной цикл микрослужбы серверной части Оркестратора.
//        while (!stoppingToken.IsCancellationRequested)
//        {
//            TcpListener? tcpListener = null;
//            _TcpClientsList.Clear();

//            //  Блок перехвата исключений для вывода в журнал.
//            try
//            {
//                //  Создание средства прослушивания сети.
//                tcpListener = new(IPAddress.Any, _Port);

//                //  Запуск средства прослушивания сети.
//                tcpListener.Start();

//                //  Вывод записи в журнал.
//                _Logger.LogInformation("Запуск прослушивания сети {endpoint}", tcpListener.LocalEndpoint);

//                // Цикл обработки новых подключений.
//                while (!stoppingToken.IsCancellationRequested)
//                {
//                    // Определяет, имеются ли ожидающие запросы на подключение.
//                    if (tcpListener.Pending())
//                    {
//                        // Получение входящего подключения.
//                        TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync(stoppingToken).ConfigureAwait(false);

//                        // Запускаем обработку клиента на пуле потоков.
//                        _ = Task.Run(async () =>
//                        {
//                            //  Проверка токена отмены.
//                            await Check.IsNotCanceledAsync(stoppingToken).ConfigureAwait(false);

//                            // Создаём объкт клиента для работы с ним.
//                            HostClient hostClient = new(tcpClient, _Logger);

//                            // Подписка на событие получение корректного пакета.
//                            hostClient.ReceivePackage += HostClient_ReceivePackage;
//                            // Подписка на событие подключения клиента.
//                            hostClient.ClientConnected += HostClient_ClientConnected;
//                            // Подписка на событие отключения клиента.
//                            hostClient.ClientDisconnected += HostClient_ClientDisconnected;

//                            // Обработка клиента.
//                            await hostClient.CommunicationAsync(stoppingToken).ConfigureAwait(false);

//                        }, stoppingToken).ConfigureAwait(false);

//                    }
//                    else
//                    {
//                        // Если входящих соединений нет, то отдаём ресурс потока на 0,5с. и идём на следующую итерацию цикла.
//                        await Task.Delay(500, stoppingToken).ConfigureAwait(false);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                //  Проверка остановки службы.
//                if (!stoppingToken.IsCancellationRequested)
//                {
//                    //Вывод информации в журнал.
//                _Logger.LogError("{exception}", ex);
//                }
//            }
//            finally
//            {
//                // Останавливает работу слушателя TCP.
//                tcpListener?.Stop();
//            }
//        }
//    }


//    /// <summary>
//    /// Обработчик события получения пакета.
//    /// </summary>
//    /// <param name="client">TCP клиент.</param>
//    /// <param name="package">Пакет данных.</param>
//    private void HostClient_ReceivePackage(HostClient client, GeneralPackage package)
//    {
//        Check.IsNotNull(client, nameof(client));
//        Check.IsNotNull(package, nameof(package));

//        // Добавляем клиента в словарь подключенных клиентов.
//        if (client.Client is TcpClient tcpClient)
//        {
//            if ((tcpClient.Client.RemoteEndPoint) is not null && (!_ConnectedClientsList.ContainsKey(tcpClient.Client.RemoteEndPoint)))
//            {
//                _ConnectedClientsList.TryAdd(tcpClient.Client.RemoteEndPoint, (package!.HostId, client));
//                _Logger.LogInformation("Клиент добавлен в список подключений - {endPoint}", client.ClientStringSignature);
//            }
//        }


//        // Обработка полученных пакетов.
//        switch(package?.Format)
//        {
//            case (byte)PackageFormat.IdHostPackage:
//                {
//                    _Logger.LogInformation("Получен идентификационный пакет от {EndPoint} - {IdSequence}: {Format}: {HostId}: {Size}: {HostsList}",
//                        client.ClientStringSignature,
//                        package?.IdSequence,
//                        package?.Format,
//                        package?.HostId,
//                        package?.Size,
//                        package?.HostsList);

//                    break;
//                }

//            case (byte)PackageFormat.GetHostListConsolePackage:
//                {
//                    // Отправка пакета в ответ. Посмотреть токен отмены!!!
//                    _ = Task.Run(async () =>
//                    {
//                        // Формируем список хостов.
//                        string hostList = string.Empty;
//                        foreach (var item in _ConnectedClientsList)
//                        {
//                            hostList = hostList + item.Value.Item1 + ", ";
//                        }

//                        // Создаём пакет данных для отправки обратно клиенту.
//                        GeneralPackage package = new(PackageFormat.SendHostListConsolePackage, hostList);

//                        // Передаём данные в сетевой поток.
//                        await package.PackageToStreamAsync(client.Client.GetStream(), CancellationToken.None).ConfigureAwait(false);

//                        _Logger.LogInformation("Отправлен ответный пакет со списком подключенных хостов {size} байт", package.Size);
//                    }).ConfigureAwait(false);

//                    break;
//                }
//            case (byte)PackageFormat.StopServicesConsolePackage:
//                {
//                    _ = Task.Run(async () =>
//                    {
//                        // Создаём пакет данных для отправки обратно клиенту.
//                        GeneralPackage package = new(PackageFormat.SendStopServicesOnHostsPackage, string.Empty);

//                        foreach (var tcpClientOnList in _ConnectedClientsList)
//                        {
//                            var stream = tcpClientOnList.Value.Item2.Client.GetStream();

//                            // Передаём данные в сетевой поток.
//                            await package.PackageToStreamAsync(stream, CancellationToken.None).ConfigureAwait(false);
//                        }

//                        _Logger.LogWarning("Отправлен пакет на остановку служб");
//                    }).ConfigureAwait(false);

//                    break;
//                }
//            case (byte)PackageFormat.StartServicesConsolePackage:
//                {
//                    _ = Task.Run(async () =>
//                    {
//                        // Создаём пакет данных для отправки клиенту.
//                        GeneralPackage package = new(PackageFormat.SendStartServicesOnHostPackage, string.Empty);

//                        foreach (var tcpClientOnList in _ConnectedClientsList)
//                        {
//                            var stream = tcpClientOnList.Value.Item2.Client.GetStream();

//                            // Передаём данные в сетевой поток.
//                            await package.PackageToStreamAsync(stream, CancellationToken.None).ConfigureAwait(false);
//                        }

//                        _Logger.LogWarning("Отправлен пакет на запуск служб.");
//                    }).ConfigureAwait(false);

//                    break;
//                }
//            case (byte)PackageFormat.CopyServicesConsolePackage:
//                {
//                    _ = Task.Run(async () =>
//                    {
//                        // Создаём пакет данных для отправки клиенту.
//                        GeneralPackage package = new(PackageFormat.CopyServicesOnHostPackage, string.Empty);

//                        foreach (var tcpClientOnList in _ConnectedClientsList)
//                        {
//                            var stream = tcpClientOnList.Value.Item2.Client.GetStream();

//                            // Передаём данные в сетевой поток.
//                            await package.PackageToStreamAsync(stream, CancellationToken.None).ConfigureAwait(false);
//                        }

//                        _Logger.LogWarning("Отправлен пакет на обновление файлов служб.");
//                    }).ConfigureAwait(false);

//                    break;
//                }
//            default:
//                break;                
//        }

//    }

//    ///// <summary>
//    ///// Выполняет задачу отправки спец. пакета на хосты.
//    ///// </summary>
//    ///// <param name="packageFormat">Формат пакета.</param>
//    ///// <returns></returns>
//    //async Task PackageHandlerTaskAsync(PackageFormat packageFormat)
//    //{
//    //    // Создаём пакет данных для отправки клиенту.
//    //    GeneralPackage package = new(packageFormat, string.Empty);

//    //    foreach (var tcpClientOnList in _ConnectedClientsList)
//    //    {
//    //        var stream = tcpClientOnList.Value.Item2.Client.GetStream();

//    //        // Передаём данные в сетевой поток.
//    //        await package.PackageToStreamAsync(stream, CancellationToken.None).ConfigureAwait(false);
//    //    }
//    //}

//    /// <summary>
//    /// Обработчик события подключения клиента.
//    /// </summary>
//    /// <param name="client">Подключенный клиент.</param>
//    private void HostClient_ClientConnected(HostClient client)
//    {
//        //  Вывод записи в журнал.
//        _Logger.LogWarning("Новое входящее подключение - {endPoint}", client.ClientStringSignature);
//    }

//    /// <summary>
//    /// Обработчик события отключения клиента.
//    /// </summary>
//    /// <param name="client">TCP клиент.</param>
//    private void HostClient_ClientDisconnected(HostClient client)
//    {
//        _Logger.LogWarning("Клиент отключен - {endPoint}", client.ClientStringSignature);

//        // Удаляем подключение из словаря.
//        if (client.Client is TcpClient tcpClient)
//        {
//            if ((tcpClient.Client.RemoteEndPoint) is not null && (_ConnectedClientsList.ContainsKey(tcpClient.Client.RemoteEndPoint)))
//            {
//                _ConnectedClientsList.TryRemove(tcpClient.Client.RemoteEndPoint, out _);
//            }
//        }
//    }
//}