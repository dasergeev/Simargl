//using Apeiron.Platform.Databases.CentralDatabase.Entities;
//using Apeiron.Platform.Databases.GlobalIdentityDatabase;
//using Apeiron.Platform.Databases.GlobalIdentityDatabase.Entities;
//using Apeiron.Services.GlobalIdentity;
//using Apeiron.Services.GlobalIdentity.Packets;
//using Microsoft.EntityFrameworkCore.Storage;
//using System.Net;
//using System.Net.Sockets;

//namespace Apeiron.Platform.Server.Services.Microservices;

///// <summary>
///// Представляет микрослужбу, выполняющую приём идентификационных пакетов от удалённых устройств.
///// </summary>
//public sealed class GlobalIdentity :
//    ServerMicroservice<GlobalIdentity>
//{
//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    /// <param name="logger">
//    /// Средство записи в журнал.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="logger"/> передана пустая ссылка.
//    /// </exception>
//    public GlobalIdentity(ILogger<GlobalIdentity> logger) :
//        base(logger)
//    {

//    }

//    /// <summary>
//    /// Асинхронно выполняет шаг работы микрослужбы.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая шаг работы микрослужбы.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    protected override sealed async ValueTask MakeStepAsync(CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        //  Получение информации о микрослужбе.
//        MicroserviceInfo microservice = await GetMicroserviceInfoAsync(cancellationToken).ConfigureAwait(false);

//        //  Получение номера прослушиваемого порта.
//        int port = await microservice.GetInt32SettingAsync("Port", cancellationToken).ConfigureAwait(false);

//        //  Создание UDP-клиента для получения пакетов.
//        using UdpClient udpClient = new()
//        {
//            //  Разрешение использовать порт нескольким клиентам.
//            ExclusiveAddressUse = false,
//        };

//        //  Настройка разрешения для UDP-клиента для использования одного адреса.
//        udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

//        //  Связывание UDP-клиента с конечной точкой.
//        udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));

//        //  Основной цикл службы.
//        while (!cancellationToken.IsCancellationRequested)
//        {
//            //  Очередь датаграмм.
//            List<UdpReceiveResult> results = new();

//            //  Чтение всех датаграмм.
//            while (udpClient.Available > 0)
//            {
//                //  Получение UDP-датаграммы.
//                results.Add(await udpClient.ReceiveAsync(cancellationToken).ConfigureAwait(false));
//            }

//            //  Фиксация времени получения.
//            DateTime receiptTime = DateTime.Now;

//            //  Перебор всех датаграмм.
//            await Parallel.ForEachAsync(
//                results,
//                new ParallelOptions()
//                {
//                    CancellationToken = cancellationToken,
//                    MaxDegreeOfParallelism = 500,
//                },
//                async (receiveResult, cancellationToken) =>
//                {
//                    //  Проверка токена отмены.
//                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//                    //  Проверка удалённой сетевой конечную точку
//                    if (receiveResult.RemoteEndPoint is IPEndPoint ipEndPoint)
//                    {
//                        //  Разбор пакета.
//                        if (StatusPacket.TryParce(receiveResult.Buffer, out StatusPacket packet) /*&& packet.GlobalIdentifier == 1*/)
//                        {
//                            if (packet.Source != StatusPacketSource.RealTime && (DateTime.Now - receiptTime).TotalMilliseconds > 500)
//                            {
//                                return;
//                            }

//                            //  Подключение к базе данных.
//                            GlobalIdentityDatabaseContext database = new();

//                            //  Начало транзакции.
//                            using IDbContextTransaction transaction = database.Database.BeginTransaction();

//                            //  Определение идентификатора.
//                            GlobalIdentifier identifier = database.GlobalIdentifiers
//                                .Where(globalIdentifier => globalIdentifier.Value == packet.GlobalIdentifier)
//                                .FirstOrDefault() ?? (database.GlobalIdentifiers.
//                                Where(globalIdentifier => globalIdentifier.Value == StaticSettings.GlobalUnknownIdentifier)
//                                .FirstOrDefault() ?? throw new InvalidDataException("Не найден идентификатор неизвестных сообщений."));

//                            //  Добавление записи в базу данных.
//                            identifier.IdentityMessages.Add(new()
//                            {
//                                Address = ipEndPoint.Address.ToString(),
//                                Port = ipEndPoint.Port,
//                                Version = packet.Version,
//                                GlobalIdentifier = identifier,
//                                PacketIdentifier = packet.PacketIdentifier,
//                                Source = packet.Source,
//                                Time = packet.Time,
//                                ReceiptTime = receiptTime,
//                            });

//                            //  Обновление данных глобального идентификатора.
//                            identifier.LastTime = receiptTime;
//                            identifier.LastPacketTime = packet.Time;
//                            identifier.LastAddress = ipEndPoint.Address.ToString();
//                            identifier.LastPort = ipEndPoint.Port;
//                            identifier.LastVersion = packet.Version;

//                            //  Сохранение данных в базу данных.
//                            await database.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

//                            //  Применение транзакции.
//                            await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);

//                            //  Запись диагностического сообщения в журнал.
//                            Logger.LogDebug("Новое сообщение: {name}.", identifier.Name);

//                            //  Проверка необходимости ответа.
//                            if (packet.Version >= 2 && packet.Source == StatusPacketSource.History)
//                            {
//                                //  Создание ответного пакета.
//                                AnswerPacket answer = packet.CreateAnswer();

//                                //  Получение датаграммы ответного пакета.
//                                byte[] datagram = answer.GetDatagram();

//                                //  Создание UDP-клиента для получения пакетов.
//                                using UdpClient answerUdpClient = new(/*Tuning.Port*/)
//                                {
//                                    //  Разрешение использовать порт нескольким клиентам.
//                                    ExclusiveAddressUse = false,
//                                };

//                                //  Настройка разрешения для UDP-клиента для использования одного адреса.
//                                answerUdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

//                                //  Связывание UDP-клиента с конечной точкой.
//                                answerUdpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));

//                                //  Проверка токена отмены.
//                                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//                                //  Установка соедиения.
//                                answerUdpClient.Connect(ipEndPoint.Address, ipEndPoint.Port);

//                                //  Проверка токена отмены.
//                                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//                                //  Отправка ответного пакета.
//                                await answerUdpClient.SendAsync(datagram, cancellationToken).ConfigureAwait(false);

//                                //  Проверка токена отмены.
//                                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//                                //  Вывод сообщения в журнал.
//                                Logger.LogDebug("Отправлен ответ: {name}({point})",
//                                    identifier.Name, ipEndPoint);
//                            }
//                        }
//                    }
//                    else
//                    {
//                        //  Запись диагностического сообщения в журнал.
//                        Logger.LogDebug(
//                            "Получены данные от неизвестного источника {point} размером {size}.",
//                            receiveResult.RemoteEndPoint,
//                            receiveResult.Buffer.Length);
//                    }
//                }).ConfigureAwait(true);
//        }
//    }
//}
