//using Apeiron.Net;
//using System.Net;
//using System.Net.Sockets;
//using System.Runtime.CompilerServices;

//namespace Apeiron.Platform.Server.Services.Orchestrator.OrchestratorHub;

//#region Делегаты
///// <summary>Делегат события, возникающего при подключении нового клиента</summary>
///// <param name="client">Клиент.</param>
//public delegate void ClientConnectedHandler(HostClient client);

///// <summary>Делегат события, возникающего при отключении нового клиента</summary>
///// <param name="client">Клиент.</param>
//public delegate void ClientDisconnectedHandler(HostClient client);

/////// <summary>Делегат события, возникающего при получении сообщения от клиента</summary>
/////// <param name="client">Клиент, от которого получено сообщение.</param>
/////// <param name="package">Полученный пакет.</param>
////public delegate void ReceivePackageHandler(HostClient client, GeneralPackage package);

//#endregion

///// <summary>
///// Представляет клиента подключенного к службе Оркестратора на сервере.
///// </summary>
//public class HostClient : IDisposable
//{
//    /// <summary>
//    /// Логгер.
//    /// </summary>
//    private readonly ILogger _Logger;

//    /// <summary>
//    /// Поле представляет TCP клиент.
//    /// </summary>
//    private readonly TcpClient _TcpClient;

//    /// <summary>
//    /// Поле представляет поток для работы с TcpClient.
//    /// </summary>
//    private readonly NetworkStream _NetworkStream;

//    /// <summary>
//    /// Флаг для реализации паттерна IDisposable
//    /// </summary>
//    private bool disposedValue;

//    /// <summary>
//    /// Объект синхронизации.
//    /// </summary>
//    private readonly object _SyncRoot;

//    /// <summary>
//    /// Представляет ресурс токена отмены.
//    /// </summary>
//    private readonly CancellationTokenSource _LocalCancellationTokenSource;

//    /// <summary>
//    /// Строковая сигнатура подключенного TCP клиента.
//    /// </summary>
//    private string _ClientStringSignature;

//    /// <summary>
//    /// Содержит делегат события, возникающего при подключении нового клиента.
//    /// </summary>
//    private ClientConnectedHandler? _ClientConnectedHandler;

//    /// <summary>
//    /// Содержит делегат события, возникающего при отключении клиента.
//    /// </summary>
//    private ClientDisconnectedHandler? _ClientDisconnectedHandler;

//    ///// <summary>
//    ///// Содержит делегат события, возникающего при получении данных от клиента.
//    ///// </summary>
//    //private ReceivePackageHandler? _ReceivePackageHandler;


//    /// <summary>Возвращает TcpClient.</summary>
//    /// <value>Клиент.</value>
//    public TcpClient Client
//    {
//        get
//        {
//            return _TcpClient;
//        }
//    }

//    /// <summary>Возвращает строковую сигнатуру TCP клиента.</summary>
//    /// <value>По-умолчанию - ip адрес клиента, порт, TTL, с которого поступило обращение в формате "xxx.xxx.xxx.xxx : yyyy TTL = zzzz"</value>
//    public string ClientStringSignature
//    {
//        get
//        {
//            lock (_SyncRoot)
//            {
//                return _ClientStringSignature;
//            }
//        }
//        private set
//        {
//            lock (_SyncRoot)
//            {
//                _ClientStringSignature = value;
//            }
//        }
//    }

//    /// <summary>Событие возникает при подключении нового клиента.</summary>
//    public event ClientConnectedHandler ClientConnected
//    {
//        add
//        {
//            lock (_SyncRoot)
//            {
//                _ClientConnectedHandler += value;
//            }
//        }
//        remove
//        {
//            lock (_SyncRoot)
//            {
//                _ClientConnectedHandler -= value;
//            }
//        }
//    }

//    /// <summary>Событие возникает при отключении клиента.</summary>
//    public event ClientDisconnectedHandler ClientDisconnected
//    {
//        add
//        {
//            lock (_SyncRoot)
//            {
//                _ClientDisconnectedHandler += value;
//            }
//        }
//        remove
//        {
//            lock (_SyncRoot)
//            {
//                _ClientDisconnectedHandler -= value;
//            }
//        }
//    }

//    ///// <summary>Событие возникает, когда от клиента получено корректное сообщение.</summary>
//    //public event ReceivePackageHandler ReceivePackage
//    //{
//    //    add
//    //    {
//    //        lock (_SyncRoot)
//    //        {
//    //            _ReceivePackageHandler += value;
//    //        }
//    //    }
//    //    remove
//    //    {
//    //        lock (_SyncRoot)
//    //        {
//    //            _ReceivePackageHandler -= value;
//    //        }
//    //    }
//    //}

//    /// <summary>
//    /// Инициализирует экземпляр класса.
//    /// </summary>
//    /// <param name="tcpClient">TCP клиент.</param>
//    /// <param name="logger">Логгер</param>
//    /// <exception cref="ArgumentNullException">
//    /// В качестве одного из параметров передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// В качестве одного из параметров передана пустая ссылка.
//    /// </exception>    
//    public HostClient(TcpClient tcpClient, ILogger logger)
//    {
//        // Проверка входящего параметра.
//        Check.IsNotNull(tcpClient, nameof(tcpClient));
//        Check.IsNotNull(logger, nameof(logger));

//        // Инициализация значений.
//        _Logger = logger;
//        _TcpClient = tcpClient;
//        _NetworkStream = tcpClient.GetStream();
//        _SyncRoot = new object();
//        _LocalCancellationTokenSource = new CancellationTokenSource();

//        // Формирование строковой сигнатуры TCP клиента.
//        var endPoint = tcpClient.Client.RemoteEndPoint;
//        if (endPoint is not null)
//        {
//            _ClientStringSignature = ((IPEndPoint)endPoint).Address.MapToIPv4().ToString() + ":" +
//                        ((IPEndPoint)endPoint).Port.ToString() + " TTL " + tcpClient.Client.Ttl;
//        }
//        else
//        {
//            _ClientStringSignature = string.Empty;
//        }
//    }

//    /// <summary>
//    /// Запускает задачу, осуществляющую работу с клиентом.
//    /// </summary>
//    /// <param name="externalСancellationToken">Внешний токен отмены работы клиента.</param>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    public async Task CommunicationAsync(CancellationToken externalСancellationToken)
//    {
//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(externalСancellationToken).ConfigureAwait(false);

//        //// Запускаем обработку клиента на пуле потоков.
//        //await Task.Run(async () =>
//        //{
//        //    // Генерирует событие о подключении нового клиента.
//        //    OnClientConnected(this);

//        //    // Присоединяем локальный токен отмены к внешнему.
//        //    using CancellationTokenSource combinedTimeoutTokenSource = CancellationTokenSource.CreateLinkedTokenSource(externalСancellationToken, _LocalCancellationTokenSource.Token);
//        //    CancellationToken _LocalCancellationToken = combinedTimeoutTokenSource.Token;

//        //    // ---------------------------------------------------------------
//        //    // Основной цикл обработки клиента и его данных.
//        //    while (!_LocalCancellationToken.IsCancellationRequested && _TcpClient.IsConnected())
//        //    {
//        //        //// Отдаём ресурс другому потоку.
//        //        //await Task.Delay(5, _LocalCancellationToken).ConfigureAwait(false);

//        //        try
//        //        {
//        //            //if (_NetworkStream.DataAvailable == true)
//        //            //{
//        //                // Получение пакета из потока данных.
//        //                var receivePackage = await GeneralPackage
//        //                                .PackageFromStreamAsync(_NetworkStream, _LocalCancellationToken)
//        //                                .ConfigureAwait(false);


//        //                if ((receivePackage is not null) && (receivePackage is not null))
//        //                {
//        //                    // Вызываем событие получение корректного сообщения.
//        //                    OnReceivePackage(this, receivePackage!);
//        //                }
//        //                else
//        //                {
//        //                    Stop();
//        //                    throw new FormatException("Поступил некорректный пакет.");
//        //                }
//        //            //}
//        //            //else
//        //            //{
//        //            //    continue;
//        //            //}

//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            if (ex is FormatException)
//        //            {
//        //                Stop();
//        //            }

//        //            // Вставить обработку отключения клиента
//        //            Stop();
//        //        }
//        //    }
//        //    //----------------------------------------------------------------

//        //    // Событие отключение клиента.
//        //    OnClientDisconnected(this);
//        //    // Запуск разрущение объекта HostClient;
//        //    Dispose();

//        //}, CancellationToken.None).ConfigureAwait(false);
//    }

//    /// <summary>Останавливает работу клиента.</summary>
//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public void Stop()
//    {
//        // Отправляем отмену через токен отмены.
//        _LocalCancellationTokenSource.Cancel();
//    }

//    ///// <summary>
//    ///// Отправляет пакет клиенту.
//    ///// </summary>
//    ///// <param name="package">Пакет, который необходимо отправить.</param>
//    ///// <param name="cancellationToken">Токен отмены.</param>
//    ///// <exception cref="OperationCanceledException">
//    ///// Операция отменена.
//    ///// </exception>
//    ///// <returns>Возвращает True, если пакет успешно отправлен.</returns>
//    //public async Task<bool> SendPackageAsync(GeneralPackage package, CancellationToken cancellationToken)
//    //{
//    //    //  Проверка ссылки на объект.
//    //    Check.IsNotNull(package, nameof(package));
//    //    //  Проверка токена отмены.
//    //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//    //    //  Проверка подключения клиента.
//    //    if (!_TcpClient.IsConnected())
//    //    {
//    //        return false;
//    //    }

//    //    try
//    //    {
//    //        // Отправка данных пакета.
//    //        await package.PackageToStreamAsync(_NetworkStream, cancellationToken).ConfigureAwait(false);
//    //        // Возвращаем успех.
//    //        return await Task.FromResult(true);
//    //    }
//    //    catch (Exception ex)
//    //    {
//    //        // Вывод информации в журнал.
//    //        _Logger.LogError("{exception}", ex);
//    //        // Возврат.
//    //        return await Task.FromResult(false);
//    //    }

//    //}


//    #region Вызов событий клиента.
//        /// <summary>Вызывается при подключении клиента.</summary>
//        /// <param name="client">Клиент.</param>
//    protected virtual void OnClientConnected(HostClient client)
//    {
//        //  Проверка ссылки на объект.
//        Check.IsNotNull(client, nameof(client));

//        lock (_SyncRoot)
//        {
//            _ClientConnectedHandler?.Invoke(client);
//        }
//    }

//    /// <summary>Вызывается при отключении клиента.</summary>
//    /// <param name="client">Клиент.</param>
//    protected virtual void OnClientDisconnected(HostClient client)
//    {
//        //  Проверка ссылки на объект.
//        Check.IsNotNull(client, nameof(client));

//        lock (_SyncRoot)
//        {
//            _ClientDisconnectedHandler?.Invoke(client);
//        }
//    }

//    /// <summary>Вызывается, когда от клиента получено корректное сообщение.</summary>
//    /// <param name="client">Клиент, от которого получено сообщение.</param>
//    /// <param name="package">Полученный пакет.</param>
//    ///// <param name="package">Полученный от клиента пакет.</param>
//    protected virtual void OnReceivePackage(HostClient client, GeneralPackage package)
//    {
//        //  Проверка ссылки на объект.
//        Check.IsNotNull(client, nameof(client));
//        //Check.IsNotNull(package, nameof(package));

//        lock (_SyncRoot)
//        {
//            _ReceivePackageHandler?.Invoke(client, package);
//        }
//    }

//    #endregion

//    #region Реализация шаблона IDisposable

//    /// <summary>
//    /// Реализация шаблона IDisposable для высвобождения ресурсов.
//    /// </summary>
//    /// <param name="disposing"></param>
//    protected virtual void Dispose(bool disposing)
//    {
//        if (!disposedValue)
//        {
//            if (disposing)
//            {
//                // освободить управляемое состояние (управляемые объекты)
//                _NetworkStream.Close();
//                _TcpClient.Dispose();
//                _LocalCancellationTokenSource.Dispose();

//                // TODO: освободить управляемое состояние (управляемые объекты)
//            }

//            // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
//            // TODO: установить значение NULL для больших полей
//            disposedValue = true;
//        }
//    }

//    /// <summary>
//    /// Деструктор.
//    /// </summary>
//    public void Dispose()
//    {
//        // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
//        Dispose(disposing: true);
//        GC.SuppressFinalize(this);
//    }

//    #endregion
//}

