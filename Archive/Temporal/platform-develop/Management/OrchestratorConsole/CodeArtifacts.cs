//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Apeiron.Platform.Server.Services.Orchestrator.OrchestratorConsole
//{
//    internal class CodeArtifacts
//    {
//        ///// <summary>
//        ///// Осуществляет работу Tcp сервера.
//        ///// </summary>
//        ///// <param name="stoppingToken">Токен отмены.</param>
//        ///// <returns></returns>
//        //public async Task TcpClientAsync(CancellationToken stoppingToken)
//        //{
//        //    //  Проверка токена отмены.
//        //    await Check.IsNotCanceledAsync(stoppingToken).ConfigureAwait(false);

//        //    // Основной цикл микрослужбы серверной части Оркестратора.
//        //    // Перезапускает подключение к серверу и обработку подключения.
//        //    while (!stoppingToken.IsCancellationRequested)
//        //    {
//        //        //  Блок перехвата исключений для вывода в журнал.
//        //        try
//        //        {
//        //            // Проверка, что подключение уже установлено, если не устанволено, то устанавливаем.
//        //            if (_TcpClient is null)
//        //            {
//        //                //  Вывод информации в журнал о попытке подключения к адресу.
//        //                Console.WriteLine("Попытка подключения к {0}:{1}", _ServerIpAdress, _TcpPort);

//        //                // Создаём новый TcpClient.
//        //                _TcpClient = new();

//        //                // Устанавливаем подключение ассинхронно, если не устанавливает подключение то улетает на блок обработки исключения и перезапускается цикл.
//        //                await _TcpClient.ConnectAsync(_ServerIpAdress, _TcpPort, stoppingToken).ConfigureAwait(false);

//        //                // Объявляем переменную для работы с сетевым потоком.
//        //                NetworkStream networkStream;

//        //                // Получаем сетевой поток для чтения и записи
//        //                networkStream = _TcpClient.GetStream();

//        //                // Если подключение установлено, то выводим информацию на экран.
//        //                if (_TcpClient.IsConnected())
//        //                {
//        //                    Console.WriteLine("Подключен к {0}:{1}", _ServerIpAdress, _TcpPort);
//        //                }

//        //                // Запускаем задачу переодической отправки на сервер идентификационного пакета.
//        //                _ = Task.Run(async () =>
//        //                {
//        //                    await IdentificationPackageSendAsync(4000, stoppingToken);
//        //                }, stoppingToken).ConfigureAwait(false);


//        //                // Запускаем задачу получения пакетов от сервера.
//        //                _ = Task.Run(async () =>
//        //                {
//        //                    await ReceivePackageAsync(stoppingToken);
//        //                }, stoppingToken).ConfigureAwait(false);

//        //            }

//        //            // Добавляем задержку перед следующей итерацией цикла и передаём ресурс другим потокам.
//        //            await Task.Delay(500, stoppingToken).ConfigureAwait(false);
//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            //  Проверка остановки службы.
//        //            if (!stoppingToken.IsCancellationRequested)
//        //            {
//        //                //  Вывод информации в журнал.
//        //                Console.WriteLine($"{0}", ex);
//        //            }

//        //            // Закрывает подключение.
//        //            _TcpClient?.Close();
//        //            // Разрушает объект.
//        //            _TcpClient?.Dispose();
//        //            // Присваиваем клиенту пустую ссылку.
//        //            _TcpClient = null;
//        //        }
//        //    }
//        //}

//        ///// <summary>
//        ///// Периодически отправляет идентификационный пакет.
//        ///// </summary>
//        ///// <param name="sendDelay">Задержка между отправкой.</param>
//        ///// <param name="cancellationToken">Токен отмены.</param>
//        ///// <returns></returns>
//        //private async Task IdentificationPackageSendAsync(int sendDelay, CancellationToken cancellationToken)
//        //{
//        //    Check.IsPositive(sendDelay, nameof(sendDelay));

//        //    // Цикл обрабатывает данные подключения клиента.
//        //    while (!cancellationToken.IsCancellationRequested)
//        //    {
//        //        // Проверяет, что соединение с сервером установлено.
//        //        if ((_TcpClient is not null) && (_TcpClient.IsConnected()))
//        //        {
//        //            try
//        //            {
//        //                // Создаём пакет данных для отправки.
//        //                GeneralPackage headerPackage = new(PackageFormat.IdHostPackage, string.Empty);

//        //                // Передаём данные в сетевой поток.
//        //                await headerPackage.PackageToStreamAsync(_TcpClient.GetStream(), cancellationToken).ConfigureAwait(false);

//        //                Console.WriteLine("Отправлен пакет идентификации размером {0} байт", headerPackage.Size);
//        //            }
//        //            catch (Exception ex)
//        //            {
//        //                //  Проверка остановки службы.
//        //                if (!cancellationToken.IsCancellationRequested)
//        //                {
//        //                    //  Вывод информации в журнал.
//        //                    Console.WriteLine("{0}", ex);
//        //                }
//        //            }
//        //        }
//        //        else
//        //        {
//        //            // Закрывает подключение.
//        //            _TcpClient?.Close();
//        //            // Разрушает объект.
//        //            _TcpClient?.Dispose();
//        //            // Присваиваем клиенту пустую ссылку.
//        //            _TcpClient = null;

//        //            // Если подключение отсутствует выходим из цикла.
//        //            break;
//        //        }

//        //        // Задержка перед следующей отправкой.
//        //        await Task.Delay(sendDelay, cancellationToken).ConfigureAwait(false);
//        //    }
//        //}

//        ///// <summary>
//        ///// Получение пакетов от сервера.
//        ///// </summary>
//        ///// <param name="cancellationToken">Токен отмены.</param>
//        //private async Task ReceivePackageAsync(CancellationToken cancellationToken)
//        //{
//        //    //  Проверка токена отмены.
//        //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        //    // Цикл обрабатывает данные подключения клиента.
//        //    while (!cancellationToken.IsCancellationRequested)
//        //    {

//        //        if ((_TcpClient is not null) && (_TcpClient.IsConnected()))
//        //        {
//        //            try
//        //            {

//        //                // Получение пакета из потока данных.
//        //                var receivePackage = await GeneralPackage
//        //                                .PackageFromStreamAsync(_TcpClient.GetStream(), cancellationToken)
//        //                                .ConfigureAwait(false);

//        //                // Доа. проверка поступившего пакета.
//        //                if ((receivePackage is not null) && (receivePackage is GeneralPackage))
//        //                {
//        //                    Console.WriteLine("Получен пакет от сервера - {0}: {1}: {2}: {3}: {4}", 
//        //                            receivePackage?.IdSequence,
//        //                            receivePackage?.Format,
//        //                            receivePackage?.HostId,
//        //                            receivePackage?.Size,
//        //                            receivePackage?.HostsList);
//        //                }
//        //                else
//        //                {
//        //                    throw new FormatException("Поступил некорректный пакет.");
//        //                }
//        //            }
//        //            catch (Exception ex)
//        //            {
//        //                //  Проверка остановки службы.
//        //                if (!cancellationToken.IsCancellationRequested)
//        //                {
//        //                    //  Вывод информации в журнал.
//        //                    Console.WriteLine("{0}", ex);
//        //                }
//        //            }
//        //        }
//        //        else
//        //        {
//        //            // Закрывает подключение.
//        //            _TcpClient?.Close();
//        //            // Разрушает объект.
//        //            _TcpClient?.Dispose();
//        //            // Присваиваем клиенту пустую ссылку.
//        //            _TcpClient = null;
//        //        }

//        //        // Задержка перед следующей отправкой.
//        //        await Task.Delay(500, cancellationToken).ConfigureAwait(false);
//        //    }
//        //}
//    }
//}

//// Выбор действия.
//ConsoleKeyInfo key;
//do
//{
//    key = Console.ReadKey();

//    switch (key.Key)
//    {
//        case ConsoleKey.D1:
//            {
//                Console.Clear();
//                Console.ForegroundColor = ConsoleColor.DarkMagenta;
//                Console.WriteLine("\nПроверка подключения к центральному узлу оркестратора.\n");
//                Console.ResetColor();

//                // Создаём токен отмены.
//                using var cancellationTokenSourceD1 = new CancellationTokenSource();
//                CancellationToken cancellationToken = cancellationTokenSourceD1.Token;

//                // Создаём клиента.
//                ConsoleClient consoleClient = new();

//                _ = Task.Run(async () =>
//                {
//                    // Запускаем задачу тестирования соединения.
//                    await consoleClient.SimpleTcpClientAsync(PackageFormat.GeneralConsolePackage, cancellationToken);
//                }, cancellationToken).ConfigureAwait(true);

//                Console.ReadKey();
//                // Отменяем запущенные задачи.
//                cancellationTokenSourceD1.Cancel();
//                consoleClient.Dispose();

//                MenuDisplay();
//                break;
//            }
//        case ConsoleKey.D2:
//            {
//                Console.Clear();
//                Console.ForegroundColor = ConsoleColor.DarkMagenta;
//                Console.WriteLine("\nПолучение списка подключенных хостов.\n");
//                Console.ResetColor();

//                // Создаём токен отмены.
//                using var cancellationTokenSourceD2 = new CancellationTokenSource();
//                CancellationToken cancellationToken = cancellationTokenSourceD2.Token;

//                ConsoleClient consoleClient = new();

//                _ = Task.Run(async () =>
//                {
//                    // Запускаем задачу на получение всех хостов.
//                    await consoleClient.SimpleTcpClientAsync(PackageFormat.GetHostListConsolePackage, cancellationToken);
//                }, cancellationToken).ConfigureAwait(true);

//                Console.ReadKey();
//                // Отменяем запущенные задачи.
//                cancellationTokenSourceD2.Cancel();
//                consoleClient.Dispose();

//                MenuDisplay();
//                break;
//            }
//        case ConsoleKey.D3:
//            {
//                Console.Clear();
//                Console.ForegroundColor = ConsoleColor.DarkMagenta;
//                Console.WriteLine("\nОстановить все управляемые службы\n");
//                Console.ResetColor();

//                // Создаём токен отмены.
//                using var cancellationTokenSourceD3 = new CancellationTokenSource();
//                CancellationToken cancellationToken = cancellationTokenSourceD3.Token;

//                ConsoleClient consoleClient = new();

//                _ = Task.Run(async () =>
//                {
//                    // Запускаем задачу на остановку служб
//                    await consoleClient.SimpleTcpClientAsync(PackageFormat.StopServicesConsolePackage, cancellationToken);
//                }, cancellationToken).ConfigureAwait(true);

//                Console.WriteLine("Отправлен пакет на остановку служб.");

//                Console.ReadKey();
//                // Отменяем запущенные задачи.
//                cancellationTokenSourceD3.Cancel();
//                consoleClient.Dispose();

//                MenuDisplay();
//                break;
//            }
//        case ConsoleKey.D4:
//            {
//                Console.Clear();
//                Console.ForegroundColor = ConsoleColor.DarkMagenta;
//                Console.WriteLine("\nЗапустить все управляемые службы\n");
//                Console.ResetColor();

//                // Создаём токен отмены.
//                using var cancellationTokenSourceD4 = new CancellationTokenSource();
//                CancellationToken cancellationToken = cancellationTokenSourceD4.Token;

//                ConsoleClient consoleClient = new();

//                _ = Task.Run(async () =>
//                {
//                    // Запускаем задачу на запуск служб.
//                    await consoleClient.SimpleTcpClientAsync(PackageFormat.StartServicesConsolePackage, cancellationToken);
//                }, cancellationToken).ConfigureAwait(true);

//                Console.WriteLine("Отправлен пакет на запуск служб.");

//                Console.ReadKey();

//                // Отменяем запущенные задачи.
//                cancellationTokenSourceD4.Cancel();
//                consoleClient.Dispose();

//                MenuDisplay();
//                break;
//            }
//        case ConsoleKey.D5:
//            {
//                Console.Clear();
//                Console.ForegroundColor = ConsoleColor.DarkMagenta;
//                Console.WriteLine("\nОбновить управляемые службы на всех хостах\n");
//                Console.ResetColor();

//                // Создаём токен отмены.
//                using var cancellationTokenSourceD5 = new CancellationTokenSource();
//                CancellationToken cancellationToken = cancellationTokenSourceD5.Token;

//                ConsoleClient consoleClient = new();

//                _ = Task.Run(async () =>
//                {
//                    // Запускаем задачу на запуск служб.
//                    await consoleClient.SimpleTcpClientAsync(PackageFormat.CopyServicesConsolePackage, cancellationToken);
//                }, cancellationToken).ConfigureAwait(true);


//                Console.WriteLine("Отправлен пакет на обновление управляемых служб.");

//                Console.ReadKey();

//                // Отменяем запущенные задачи.
//                cancellationTokenSourceD5.Cancel();
//                consoleClient.Dispose();

//                MenuDisplay();
//                break;
//            }
//        default:
//            break;
//    }
