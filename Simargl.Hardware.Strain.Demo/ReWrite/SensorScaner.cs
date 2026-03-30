using Simargl.Designing.Utilities;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Simargl.Hardware.Strain.Demo.ReWrite;

/// <summary>
/// Представляет класс сканирования датчиков.
/// </summary>
public static class SensorScaner
{
    /// <summary>
    /// Возвращает очередь результатов.
    /// </summary>
    [CLSCompliant(false)]
    public static ConcurrentQueue<(uint Serial, IPEndPoint EndPoint)> Results { get; } = [];

    /// <summary>
    /// Представляет порт отправителя шировещательного пакета.
    /// </summary>
    private static readonly ushort _TargetPort = 49001;


    /// <summary>
    /// Представляет порт отправителя шировещательного пакета.
    /// </summary>
    private static readonly ushort _CountIteration = 4;//50;

#pragma warning disable VSTHRD002



    /// <summary>
    /// Представляет функцию сканирования датчиков.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public static async Task Scan2Async(CancellationToken cancellationToken)
    {
        try
        {
            int broadcastPort = 49001;

            //  Перебор описаний сетевых интерфейсов локального компьютера.
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                //  Проверка статуса интерфейса.
                if (networkInterface.OperationalStatus != OperationalStatus.Up)
                {
                    //  Переход к следующему интерфейсу.
                    continue;
                }

                //  Получение объекта, описывающего конфигурацию сетевого интерфейса.
                IPInterfaceProperties interfaceProperties = networkInterface.GetIPProperties();

                //  Перебор адресов одноадресной рассылки, назначенные интерфейсу.
                foreach (UnicastIPAddressInformation unicastInformation in interfaceProperties.UnicastAddresses)
                {
                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Получение адреса.
                        IPAddress address = unicastInformation.Address;

                        //  Проверка семейства адресов.
                        if (address.AddressFamily != AddressFamily.InterNetwork)
                        {
                            //  Переход к следующей рассылки.
                            continue;
                        }

                        //  Получение данных адресов.
                        byte[] addressBytes = address.GetAddressBytes();
                        byte[] maskBytes = unicastInformation.IPv4Mask.GetAddressBytes();

                        //  Проверка размера данных.
                        if (addressBytes.Length != maskBytes.Length)
                        {
                            //  Переход к следующей рассылки.
                            continue;
                        }

                        //  Создание данных широковещательного адреса.
                        byte[] broadcastBytes = new byte[addressBytes.Length];
                        for (int i = 0; i < addressBytes.Length; i++)
                            broadcastBytes[i] = (byte)(addressBytes[i] | (maskBytes[i] ^ 255));

                        //  Получение широковещаетльного адреса.
                        IPAddress broadcast = new(broadcastBytes);

                        //  Создание клиента.
                        using UdpClient udpClient = new();

                        //  Разрешение широковещательных пакетов.
                        udpClient.EnableBroadcast = true;

                        //  Привязка к адресу.
                        udpClient.Client.Bind(new IPEndPoint(address, 0));

                        //  Получение локальной точки.
                        if (udpClient.Client.LocalEndPoint is not IPEndPoint localEndPoint)
                        {
                            //  Переход к следующей рассылки.
                            continue;
                        }

                        //  Создание широковещательно точки.
                        IPEndPoint broadcastEndPoint = new(broadcast, broadcastPort);

                        //  Отправка данных.
                        await udpClient.SendAsync(Array.Empty<byte>().AsMemory(), broadcastEndPoint, cancellationToken).ConfigureAwait(false);

                        //Console.WriteLine();
                        //Console.WriteLine($"IPAddress: {address}");
                        //Console.WriteLine($"  broadcast: {broadcast}");
                        //Console.WriteLine("  Автоматически выделенный порт: " + localEndPoint.Port);

                        CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                        CancellationToken token = tokenSource.Token;
                        tokenSource.CancelAfter(10000);

                        try
                        {
                            //  Цикл ожидания.
                            while (!token.IsCancellationRequested)
                            {
                                //  Ожидание ответа.
                                UdpReceiveResult result = await udpClient.ReceiveAsync(token).ConfigureAwait(false);


                                //  Получение пакета.
                                var packageRecv = BinPackage.FromArray(result.Buffer);

                                //  Создание потока памяти
                                using MemoryStream memory = new(packageRecv.Data);

                                //  Создание читателя.
                                using BinaryReader reader = new(memory);

                                //  Чтение типа.
                                ulong type = reader.ReadUInt64();

                                //  Чтение серийного номера.
                                uint serial = reader.ReadUInt32();

                                (uint Serial, IPEndPoint EndPoint) info = new(serial, result.RemoteEndPoint);
                                //  Печать IP
                                //Console.WriteLine($"{result.RemoteEndPoint}");

                                //  Продолжение цикла 
                                switch (type)
                                {
                                    case 0xA0A0A0A0A0A0A0A0UL:
                                        //  Печать типа датчика.
                                        //Console.WriteLine("TYPE = TEST_PROGRAM");
                                        break;
                                    case 0x842E783B54D5AA4CUL:
                                        //  Печать типа датчика.
                                        //Console.WriteLine("TYPE = ADXL357");
                                        break;
                                    case 0xA2758EE46E4B43CFUL:
                                        //  Печать типа датчика.
                                        //Console.WriteLine("TYPE = AD7730");
                                        Results.Enqueue(info);
                                        break;
                                    default:
                                        //  Печать типа датчика.
                                        //Console.WriteLine("TYPE = UNKNOWN");
                                        break;

                                }


                                ////  Вывод информации.
                                //Console.WriteLine($"Получен ответ от {result.RemoteEndPoint}.");
                            }
                        }
                        catch
                        {
                            //Console.WriteLine("Время ожидания вышло.");
                        }


                        //    //throw new ArgumentException("IP address and subnet mask lengths do not match.");


                        //// Создаем сокет с привязкой к интерфейсу
                        //using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                        //{
                        //    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

                        //    var point = new IPEndPoint(unicastInformation.Address, 0);
                        //    Console.WriteLine($"Bind {point}");
                        //    socket.Bind(point);  // Привязываем сокет к IP-адресу интерфейса
                        //    int port = 49001;
                        //    IPEndPoint endPoint = new IPEndPoint(broadcast, port);

                        //    try
                        //    {
                        //        socket.SendTo([], endPoint);
                        //        Console.WriteLine($"Сообщение отправлено на {broadcast}:{port} через интерфейс {networkInterface.Name}");
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Console.WriteLine($"Ошибка отправки: {ex.Message}");
                        //    }
                        //}

                        //Console.WriteLine($"{broadcast}");
                    }
                    catch
                    {
                        //Console.WriteLine(ex);
                    }
                }
            }
        }
        catch 
        {
            //Console.WriteLine();
            //Console.WriteLine("Произошла ошибка:");
            //Console.WriteLine(ex.ToString());
            //Console.WriteLine();
        }
    }

    /// <summary>
    /// Представляет функцию сканирования датчиков.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public static async Task ScanAsync(CancellationToken cancellationToken)
    {
        ////  Создание списка задач
        //List<Task> tasks = [];

        //  Получение интерфейсов.
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

        //  Цикл по всем интерфейсам
        await Parallel.ForEachAsync(
            interfaces,
            cancellationToken,
            async delegate (NetworkInterface @interface, CancellationToken cancellationToken)
            {
                var ni = @interface;

                try
                {
                    //  Проверка статуса интерфейса.
                    if (@interface.OperationalStatus != OperationalStatus.Up)
                    {
                        //  Переход к следующему интерфейсу.
                        return;
                    }

                    //  Получение адресов.
                    UnicastIPAddressInformationCollection addresses = @interface.GetIPProperties().UnicastAddresses;

                    //   Цикл по всем адрессам
                    await Parallel.ForEachAsync(
                        addresses,
                        cancellationToken,
                        async delegate (UnicastIPAddressInformation info, CancellationToken cancellationToken)
                        {
                            try
                            {
                                //  Проверка что ipv4 и то что не 127.0.0.1
                                if (info.Address.AddressFamily == AddressFamily.InterNetwork
                                    &&
                                    !info.Address.Equals(IPAddress.Loopback))
                                {
                                    //  Массив IP адреса
                                    byte[] ipArray = info.Address.GetAddressBytes();

                                    //  Массив маски сети
                                    byte[] maskArray = info.IPv4Mask.GetAddressBytes();

                                    //  Создание массива IP для broadcast
                                    byte[] broadcastArray = new byte[4];

                                    //  Логирование
                                    // Console.WriteLine($"Address={info.Address}");
                                    // Console.WriteLine($"IPv4Mask={info.IPv4Mask}");

                                    //  Цикл по массиву IP
                                    for (int i = 0; i < 4; i++)
                                    {
                                        //  Расчет байта broadcast ip
                                        broadcastArray[i] = (byte)(ipArray[i] & maskArray[i] | ~maskArray[i]);
                                    }

                                    //  Получение broadcast IP
                                    IPAddress broadcastIP = new(broadcastArray);

                                    //  Логирование
                                    // Console.WriteLine($"broadcastIP={broadcastIP}");

                                    //  Отправка широковещательного запроса.
                                    await SendAsync(broadcastIP, cancellationToken).ConfigureAwait(false);

                                    //Task task = Task.Run(async () =>
                                    //await SendAsync(broadcastIP, cancellationToken).ConfigureAwait(false), cancellationToken);

                                    ////  Добавление задачи в списко
                                    //tasks.Add(task);
                                }
                            }
                            catch { }
                        }).ConfigureAwait(false);
                }
                catch { }
            }).ConfigureAwait(false);

        ////  Синхронное ожидание выполнения всех задач
        //await Task.WhenAll([.. tasks]).ConfigureAwait(false);
    }

#pragma warning restore VSTHRD002
    /// <summary>
    /// Рассылает сообщение серии адресатов.
    /// </summary>
    /// <returns>
    /// Задача.
    /// </returns>
    private static async Task SendAsync(IPAddress broadcastIP , CancellationToken token)
    {
        //  Пересылка данных
        try
        {
            //  Получение IP адреса сервера NMEA
            IPAddress ipAddressSendServer = broadcastIP;
            //IPAddress ipAddressSendServer = IPAddress.Parse("192.168.1.128");

            //  Получение конечной точки сервера NMEA
            IPEndPoint endPoint = new(ipAddressSendServer, _TargetPort);

            //   Создание массива нулевой длинны.
            byte[] data = Array.Empty<byte>();

            //  Создание пакета
            BinPackage package = new((long)FormatPackages.IdentificationUdpPackage, data);

            //  Создание UDP-клиента.
            using UdpClient udpResender = new();

            //  Создание токена отмены
            using var source = new CancellationTokenSource();

            //  Отмена задачи пересылки в случае приостановки 
            source.CancelAfter(200);

            //  Пересылка данных
            await udpResender.SendAsync(package.ToArray(), endPoint, source.Token).ConfigureAwait(false);

            //  Основной цикл.
            for (int i = 0; i < _CountIteration; i++)
            {
                if (udpResender.Available > 0)
                {
                    //  Получение UDP-датаграммы.
                    UdpReceiveResult result = await udpResender.ReceiveAsync(token).ConfigureAwait(false);

                    //  Получение пакета.
                    var packageRecv = BinPackage.FromArray(result.Buffer);

                    //  Создание потока памяти
                    using MemoryStream memory = new(packageRecv.Data);

                    //  Создание читателя.
                    using BinaryReader reader = new(memory);

                    //  Чтение типа.
                    ulong type = reader.ReadUInt64();

                    //  Чтение серийного номера.
                    uint serial = reader.ReadUInt32();

                    (uint Serial, IPEndPoint EndPoint) info = new(serial, result.RemoteEndPoint);
                    //  Печать IP
                    //Console.WriteLine($"{result.RemoteEndPoint}");

                    //  Продолжение цикла 
                    switch (type)
                    {
                        case 0xA0A0A0A0A0A0A0A0UL:
                            //  Печать типа датчика.
                            //Console.WriteLine("TYPE = TEST_PROGRAM");
                            break;
                        case 0x842E783B54D5AA4CUL:
                            //  Печать типа датчика.
                            //Console.WriteLine("TYPE = ADXL357");
                            break;
                        case 0xA2758EE46E4B43CFUL:
                            //  Печать типа датчика.
                            //Console.WriteLine("TYPE = AD7730");
                            Results.Enqueue(info);
                            break;
                        default:
                            //  Печать типа датчика.
                            //Console.WriteLine("TYPE = UNKNOWN");
                            break;

                    }

                    //  Печать серийного номера.
                    //Console.WriteLine($"SERIAL = {serial:X8}");

                    //  Продолжение цикла.
                    continue;
                }

                //  Ожидaние.
                await Task.Delay(100, token).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {

            //Console.WriteLine($"Ошибка: {ex.Message}");
            //  Проверка исключения
            if (ex.IsSystem())
            {
                //  Перенаправление исключения
                throw;
            }

        }
    }
}
