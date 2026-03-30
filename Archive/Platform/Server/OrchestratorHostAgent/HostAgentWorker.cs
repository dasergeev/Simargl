//using Apeiron.Net;
//using Apeiron.Platform.Server.Services.Orchestrator.Packages;
//using Apeiron.Support;
//using System.Net;
//using System.Net.Sockets;
//using System.Runtime.CompilerServices;

//namespace Apeiron.Platform.Server.Services.Orchestrator.OrchestratorHostAgent;

///// <summary>
///// Представляет микрослужбу управления службами на локальных хостах.
///// </summary>
//public class HostAgentWorker : BackgroundService
//{
//    /// <summary>
//    /// Вовращает или устанавливает настройки службы.
//    /// </summary>
//    private readonly WorkerOptions _WorkerOptions;

//    /// <summary>
//    /// Логгер.
//    /// </summary>
//    private readonly ILogger<HostAgentWorker> _Logger;

//    /// <summary>
//    /// Поле представляющее Tcp подключение.
//    /// </summary>
//    private TcpClient? _TcpClient;

//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    /// <param name="logger">
//    /// Средство записи в журнал.
//    /// </param>
//    /// <param name="options"></param>
//    public HostAgentWorker(ILogger<HostAgentWorker> logger, WorkerOptions options)
//    {
//        _Logger = logger;
//        _WorkerOptions = options;
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
//        // Перезапускает подключение к серверу и обработку подключения.
//        while (!stoppingToken.IsCancellationRequested)
//        {
//            //  Блок перехвата исключений для вывода в журнал.
//            try
//            {
//                // Проверка, что подключение уже установлено, если не устанволено, то устанавливаем.
//                if (_TcpClient is null)
//                {
//                    //  Вывод информации в журнал о попытке подключения к адресу.
//                    _Logger.LogInformation("Попытка подключения к {address}:{tcpPort}", IPAddress.Parse(_WorkerOptions.ServerIpAdress), _WorkerOptions.ServerPortAdress);

//                    // Создаём новый TcpClient.
//                    _TcpClient = new();

//                    // Устанавливаем подключение ассинхронно, если не устанавливает подключение то улетает на блок обработки исключения и перезапускается цикл.
//                    await _TcpClient.ConnectAsync(IPAddress.Parse(_WorkerOptions.ServerIpAdress), _WorkerOptions.ServerPortAdress, stoppingToken).ConfigureAwait(false);

//                    // Объявляем переменную для работы с сетевым потоком.
//                    NetworkStream networkStream;

//                    // Получаем сетевой поток для чтения и записи
//                    networkStream = _TcpClient.GetStream();

//                    // Если подключение установлено, то выводим информацию на экран.
//                    if (_TcpClient.IsConnected())
//                    {
//                        _Logger.LogInformation("Подключен к {address}:{tcpPort}", IPAddress.Parse(_WorkerOptions.ServerIpAdress), _WorkerOptions.ServerPortAdress);
//                    }

//                    // Запускаем задачу переодической отправки на сервер идентификационного пакета.
//                    _ = Task.Run(async () =>
//                    {
//                        await IdentificationPackageSendAsync(_WorkerOptions.IdentificationPackageSendDelay, stoppingToken);
//                    }, stoppingToken).ConfigureAwait(false);


//                    // Запускаем задачу получения пакетов от сервера.
//                    _ = Task.Run(async () =>
//                    {
//                        await ReceivePackageAsync(stoppingToken);
//                    }, stoppingToken).ConfigureAwait(false);

//                }
//            }
//            catch (Exception ex)
//            {
//                //  Проверка остановки службы.
//                if (!stoppingToken.IsCancellationRequested)
//                {
//                    //  Вывод информации в журнал.
//                    _Logger.LogError("{exception}", ex);
//                }

//                TcpDestroyer();
//            }

//            // Добавляем задержку перед следующей итерацией цикла переподключения клиента, передаём ресурс другим потокам.
//            await Task.Delay(_WorkerOptions.TcpReconnectDelay, stoppingToken).ConfigureAwait(false);
//        }
//    }

//    /// <summary>
//    /// Периодически отправляет идентификационный пакет.
//    /// </summary>
//    /// <param name="sendDelay">Задержка между отправкой.</param>
//    /// <param name="cancellationToken">Токен отмены.</param>
//    /// <returns></returns>
//    private async Task IdentificationPackageSendAsync(int sendDelay, CancellationToken cancellationToken)
//    {
//        Check.IsPositive(sendDelay, nameof(sendDelay));

//        // Цикл обрабатывает данные подключения клиента.
//        while (!cancellationToken.IsCancellationRequested)
//        {
//            // Проверяет, что соединение с сервером установлено.
//            if ((_TcpClient is not null) && (_TcpClient.IsConnected()))
//            {
//                try
//                {
//                    // Создаём пакет данных для отправки.
//                    GeneralPackage generalPackage = new(PackageFormat.IdHostPackage, string.Empty);

//                    // Передаём данные в сетевой поток.
//                    await generalPackage.PackageToStreamAsync(_TcpClient.GetStream(), cancellationToken).ConfigureAwait(false);

//                    _Logger.LogInformation("Отправлен идентификационный пакет размером {size} байт", generalPackage.Size);
//                }
//                catch (Exception ex)
//                {
//                    //  Проверка остановки службы.
//                    if (!cancellationToken.IsCancellationRequested)
//                    {
//                        //  Вывод информации в журнал.
//                        _Logger.LogError("{exception}", ex);
//                    }
//                }
//            }
//            else
//            {
//                TcpDestroyer();

//                // Если подключение отсутствует выходим из цикла во вшений цикл создания клиента и установления подключения.
//                break;
//            }

//            // Задержка перед следующей отправкой.
//            await Task.Delay(sendDelay, cancellationToken).ConfigureAwait(false);
//        }
//    }

//    ///// <summary>
//    ///// Получение пакетов от сервера.
//    ///// </summary>
//    ///// <param name="cancellationToken">Токен отмены.</param>
//    //private async Task ReceivePackageAsync(CancellationToken cancellationToken)
//    //{
//    //    //  Проверка токена отмены.
//    //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//    //    // Цикл обрабатывает данные подключения клиента.
//    //    while (!cancellationToken.IsCancellationRequested)
//    //    {

//    //        if ((_TcpClient is not null) && (_TcpClient.IsConnected()))
//    //        {
//    //            try
//    //            {
//    //                // Получение пакета из потока данных.
//    //                var receivePackage = await GeneralPackage
//    //                                .PackageFromStreamAsync(_TcpClient.GetStream(), cancellationToken)
//    //                                .ConfigureAwait(false);

//    //                // Доп. проверка поступившего пакета.
//    //                if (receivePackage is not null)
//    //                {
//    //                    _Logger.LogInformation("Получен пакет от сервера - {IdSequence}: {Format}: {HostId}: {Size}",
//    //                            receivePackage?.IdSequence,
//    //                            receivePackage?.Format,
//    //                            receivePackage?.HostId,
//    //                            receivePackage?.Size);

//    //                    switch(receivePackage?.Format)
//    //                    {
//    //                        case (byte)PackageFormat.SendStopServicesOnHostsPackage:
//    //                            {
//    //                                if (ServiceOperator.StopService(_WorkerOptions.ListOfService))
//    //                                    _Logger.LogWarning("Обработан запрос на остановку служб {service}", string.Join(", ", _WorkerOptions.ListOfService));
//    //                                else
//    //                                    _Logger.LogError("Ошибка остановки служб {service}", string.Join(", ", _WorkerOptions.ListOfService));
//    //                                break;
//    //                            }
//    //                        case (byte)PackageFormat.SendStartServicesOnHostPackage:
//    //                            {
//    //                                if (ServiceOperator.StartService(_WorkerOptions.ListOfService))
//    //                                    _Logger.LogWarning("Обработан запрос на остановку служб {service}", string.Join(", ", _WorkerOptions.ListOfService));
//    //                                else
//    //                                    _Logger.LogError("Ошибка запуска служб {service}", string.Join(", ", _WorkerOptions.ListOfService));
//    //                                break;
//    //                            }
//    //                        case (byte)PackageFormat.CopyServicesOnHostPackage:
//    //                            {
//    //                                // Проверяем статус службы
//    //                                if (ServiceOperator.ServiceRunningStatus(_WorkerOptions.ListOfService))
//    //                                {
//    //                                    // Останавливаем службу.
//    //                                    if (ServiceOperator.StopService(_WorkerOptions.ListOfService))
//    //                                    {
//    //                                        ServiceCopy.FilesCopyService(_WorkerOptions.SourceDeploymentPath, _WorkerOptions.LocalPath);
//    //                                    }
//    //                                }
//    //                                else
//    //                                {
//    //                                    // !!! Тут нужна проверка на то, что служба вообще зарегистрирована на хосте !!!
//    //                                    ServiceCopy.FilesCopyService(_WorkerOptions.SourceDeploymentPath, _WorkerOptions.LocalPath);
//    //                                }
                                    
//    //                                // Заново запускаем службу
//    //                                if (ServiceOperator.StartService(_WorkerOptions.ListOfService))
//    //                                    _Logger.LogWarning("Обработан запрос на обновление файлов службы {service}", string.Join(", ", _WorkerOptions.ListOfService));

//    //                                break;
//    //                            }
//    //                        default:
//    //                            break;
//    //                    }                                                
//    //                }
//    //                else
//    //                {
//    //                    throw new FormatException("Поступил некорректный пакет.");
//    //                }
//    //            }
//    //            catch (Exception ex)
//    //            {
//    //                //  Проверка остановки службы.
//    //                if (!cancellationToken.IsCancellationRequested)
//    //                {
//    //                    //  Вывод информации в журнал.
//    //                    _Logger.LogError("{exception}", ex);
//    //                }
//    //            }
//    //        }
//    //        else
//    //        {
//    //            TcpDestroyer();

//    //            // Если подключение отсутствует выходим из цикла во вшешний цикл для пересоздания подключения.
//    //            break;
//    //        }

//    //        // Задержка перед следующей итерацией.
//    //        await Task.Delay(200, cancellationToken).ConfigureAwait(false);
//    //    }
//    //}

//    /// <summary>
//    /// Разрушает объект TcpClient.
//    /// </summary>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    private void TcpDestroyer()
//    {
//        // Закрывает подключение.
//        _TcpClient?.Close();
//        // Разрушает объект.
//        _TcpClient?.Dispose();
//        // Присваиваем клиенту пустую ссылку.
//        _TcpClient = null;
//    }
//}
