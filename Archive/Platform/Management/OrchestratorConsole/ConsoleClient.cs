//using Apeiron.Net;
//using Apeiron.Platform.Server.Services.Orchestrator.Packages;
//using Apeiron.Support;
//using System.Net;
//using System.Net.Sockets;
//using System.Runtime.CompilerServices;

//namespace Apeiron.Platform.Server.Services.Orchestrator.OrchestratorConsole;

///// <summary>
///// Представляет TcpClient для работы с серверной частью Оркестратора.
///// </summary>
//public class ConsoleClient : IDisposable
//{
//    /// <summary>
//    /// Tcp порт подключения к серверу.
//    /// </summary>
//    private const int _TcpPort = 48888;

//    /// <summary>
//    /// Задержка между повторением переподключения к серверу.
//    /// </summary>
//    private const int _TcpReconnectDelay = 7000;

//    /// <summary>
//    /// Ip адрес сервера.
//    /// </summary>
//    private readonly IPAddress _ServerIpAdress = IPAddress.Parse("10.69.16.237");

//    /// <summary>
//    /// Поле представляющее Tcp подключение.
//    /// </summary>
//    private TcpClient? _TcpClient;
   
//    /// <summary>
//    /// Поле для реализации шаблона IDisposable
//    /// </summary>
//    private bool disposedValue;


//    /// <summary>
//    /// Инициализирует класс.
//    /// </summary>
//    public ConsoleClient()
//    {
//    }


//    /// <summary>
//    /// Единоразовый обмен с сервером Оркестратора.
//    /// </summary>
//    /// <param name="cancellationToken">Токен отмены.</param>
//    public async Task SimpleTcpClientAsync(PackageFormat packageFormat, CancellationToken cancellationToken)
//    {
//        {
//            //  Проверка токена отмены.
//            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//            // Основной цикл микрослужбы серверной части Оркестратора.
//            // Перезапускает подключение к серверу и обработку подключения.
//            while (!cancellationToken.IsCancellationRequested)
//            {
//                //  Блок перехвата исключений для вывода в журнал.
//                try
//                {
//                    // Проверка, что подключение уже установлено, если не устанволено, то устанавливаем.
//                    if (_TcpClient is null)
//                    {
//                        //  Вывод информации в журнал о попытке подключения к адресу.
//                        Console.WriteLine("Попытка подключения к {0}:{1}", _ServerIpAdress, _TcpPort);

//                        // Создаём новый TcpClient.
//                        _TcpClient = new();

//                        // Устанавливаем подключение ассинхронно, если не устанавливает подключение то улетает на блок обработки исключения и перезапускается цикл.
//                        await _TcpClient.ConnectAsync(_ServerIpAdress, _TcpPort, cancellationToken).ConfigureAwait(false);

//                        // Объявляем переменную для работы с сетевым потоком.
//                        NetworkStream networkStream;

//                        // Получаем сетевой поток для чтения и записи
//                        networkStream = _TcpClient.GetStream();

//                        // Если подключение установлено, то выводим информацию на экран.
//                        if (_TcpClient.IsConnected())
//                        {
//                            Console.WriteLine("Подключен к {0}:{1}", _ServerIpAdress, _TcpPort);
//                        }

//                        // Запускаем задачу единоразовой отправки на сервер идентификационного пакета.
//                        _ = Task.Run(async () =>
//                        {
//                            // Проверяет, что соединение с сервером установлено.
//                            if ((_TcpClient is not null) && (_TcpClient.IsConnected()))
//                            {
//                                try
//                                {
//                                    // Создаём пакет данных для отправки.
//                                    GeneralPackage generalPackage = new(packageFormat, string.Empty);

//                                    // Передаём данные в сетевой поток.
//                                    await generalPackage.PackageToStreamAsync(_TcpClient.GetStream(), cancellationToken).ConfigureAwait(false);

//                                    Console.WriteLine("Отправлен пакет размером {0} байт\n\r", generalPackage.Size);
//                                }
//                                catch (Exception ex)
//                                {
//                                    //  Проверка остановки службы.
//                                    if (!cancellationToken.IsCancellationRequested)
//                                    {
//                                        //  Вывод информации в журнал.
//                                        Console.WriteLine("{0}", ex);
//                                    }
//                                }
//                            }
//                            else
//                            {
//                                TcpDestroyer();
//                            }
//                        }, cancellationToken).ConfigureAwait(false);


//                        // Запускаем задачу получения пакетов от сервера.
//                        _ = Task.Run(async () =>
//                        {
//                            //  Проверка токена отмены.
//                            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//                            // Цикл обрабатывает данные подключения клиента.
//                            while (!cancellationToken.IsCancellationRequested)
//                            {

//                                if ((_TcpClient is not null) && (_TcpClient.IsConnected()))
//                                {
//                                    try
//                                    {

//                                        // Получение пакета из потока данных.
//                                        var receivePackage = await GeneralPackage
//                                                        .PackageFromStreamAsync(_TcpClient.GetStream(), cancellationToken)
//                                                        .ConfigureAwait(false);

//                                        // Доа. проверка поступившего пакета.
//                                        if ((receivePackage is not null) && (receivePackage is not null))
//                                        {
//                                            Console.WriteLine("Получен пакет от сервера - {0}: {1}: {2}: {3} :{4}",
//                                                    receivePackage?.IdSequence,
//                                                    receivePackage?.Format,
//                                                    receivePackage?.HostId,
//                                                    receivePackage?.Size,
//                                                    receivePackage?.HostsList);
//                                            break;
//                                        }
//                                        else
//                                        {
//                                            throw new FormatException("Поступил некорректный пакет.");
//                                        }
//                                    }
//                                    catch (Exception ex)
//                                    {
//                                        //  Проверка остановки службы.
//                                        if (!cancellationToken.IsCancellationRequested)
//                                        {
//                                            //  Вывод информации в журнал.
//                                            Console.WriteLine("{0}", ex);
//                                        }
//                                    }
//                                }
//                                else
//                                {
//                                    TcpDestroyer();
//                                }

//                                // Задержка перед следующей итерацией получения данных.
//                                await Task.Delay(200, cancellationToken).ConfigureAwait(false);
//                            }

//                            return Task.CompletedTask;
//                        }, cancellationToken).ConfigureAwait(true);

//                    }

//                    // Добавляем задержку перед следующей итерацией цикла и передаём ресурс другим потокам.
//                    await Task.Delay(_TcpReconnectDelay, cancellationToken).ConfigureAwait(false);
//                }
//                catch (Exception ex)
//                {
//                    //  Проверка остановки службы.
//                    if (!cancellationToken.IsCancellationRequested)
//                    {
//                        //  Вывод информации в журнал.
//                        Console.WriteLine($"{0}", ex);
//                    }

//                    TcpDestroyer();
//                }
//            }
//        }

//    }

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


//    protected virtual void Dispose(bool disposing)
//    {
//        if (!disposedValue)
//        {
//            if (disposing)
//            {
//                _TcpClient?.Dispose();
//                // TODO: освободить управляемое состояние (управляемые объекты)
//            }

//            // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
//            // TODO: установить значение NULL для больших полей
//            disposedValue = true;
//        }
//    }

//    public void Dispose()
//    {
//        // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
//        Dispose(disposing: true);
//        GC.SuppressFinalize(this);
//    }
//}

