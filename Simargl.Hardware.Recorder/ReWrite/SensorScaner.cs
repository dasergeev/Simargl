using Simargl.Designing.Utilities;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Simargl.Hardware.Recorder.ReWrite;

/// <summary>
/// Представляет класс сканирования датчиков.
/// </summary>
public static class SensorScaner
{
    /// <summary>
    /// Возвращает очередь результатов.
    /// </summary>
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
                            broadcastBytes[i] = (byte)(addressBytes[i] | maskBytes[i] ^ 255);

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

                                if (type == 0xA2758EE46E4B43CFUL)
                                {
                                    Results.Enqueue(info);
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
        catch 
        {

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

                                    //  Цикл по массиву IP
                                    for (int i = 0; i < 4; i++)
                                    {
                                        //  Расчет байта broadcast ip
                                        broadcastArray[i] = (byte)(ipArray[i] & maskArray[i] | ~maskArray[i]);
                                    }

                                    //  Получение broadcast IP
                                    IPAddress broadcastIP = new(broadcastArray);

                                    //  Отправка широковещательного запроса.
                                    await SendAsync(broadcastIP, cancellationToken).ConfigureAwait(false);
                                }
                            }
                            catch { }
                        }).ConfigureAwait(false);
                }
                catch { }
            }).ConfigureAwait(false);
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

            //  Получение конечной точки сервера NMEA
            IPEndPoint endPoint = new(ipAddressSendServer, _TargetPort);

            //  Создание массива нулевой длинны.
            byte[] data = [];

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

                    if (type == 0xA2758EE46E4B43CFUL)
                    {
                        Results.Enqueue(info);
                    }

                    //  Продолжение цикла.
                    continue;
                }

                //  Ожидaние.
                await Task.Delay(100, token).ConfigureAwait(false);
            }
        }
        catch { }
    }
}
