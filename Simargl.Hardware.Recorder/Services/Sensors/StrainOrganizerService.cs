using Simargl.IO;
using Microsoft.Extensions.Logging;
using Simargl.Hardware.Modbus.Core;
using Simargl.Hardware.Recorder.Core;
using Simargl.Hardware.Recorder.ReWrite;
using Simargl.Payload.Obsolete.Strain;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Simargl.Hardware.Recorder.Services.Sensors;

/// <summary>
/// Представляет службу организации тензодатчиков.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="heart">
/// Сердце приложения.
/// </param>
public sealed class StrainOrganizerService(
    ILogger<StrainOrganizerService> logger, Heart heart) :
    Service<StrainOrganizerService>(logger, heart)
{
    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override sealed async Task InvokeAsync(CancellationToken cancellationToken)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "arp-scan",
                Arguments = "--localnet",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            var output = new StringBuilder();
            var error = new StringBuilder();

            using var proc = new Process { StartInfo = psi, EnableRaisingEvents = true };
            proc.OutputDataReceived += (s, e) => { if (e.Data != null) output.AppendLine(e.Data); };
            proc.ErrorDataReceived += (s, e) => { if (e.Data != null) error.AppendLine(e.Data); };



            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            await Task.Run(proc.WaitForExit, cancellationToken); // не блокируем поток вызова

            StringBuilder builder = new();

            builder.AppendLine();
            builder.AppendLine("ExitCode: " + proc.ExitCode);
            builder.AppendLine("--- STDOUT ---");
            builder.AppendLine(output.ToString());
            if (error.Length > 0)
            {
                builder.AppendLine("--- STDERR ---");
                builder.AppendLine(error.ToString());
            }

            Logger.LogInformation("{message}", builder);


            //await Task.CompletedTask.ConfigureAwait(false);

            ////  Создание конечной точки.
            //IPEndPoint endPoint = new(IPAddress.Parse("192.168.1.32"), 502);

            ////  Создание клиента
            //using TcpClient tcpClient = new();

            ////  Подключение по TCP/IP
            //await tcpClient.ConnectAsync(endPoint, cancellationToken).ConfigureAwait(false);

            ////  Получение потока.
            //await using NetworkStream stream = tcpClient.GetStream();



            ////  Создание распределителя данных потока.
            //Spreader spreader = new(stream);

            ////  Вывод сообщения в журнал.
            //Logger.LogInformation("Установлено Modbus-соединение: {endPoint}", endPoint);

            ////  Создание запроса.
            //TcpAduPackage request = RequestReadHoldings(19, 4);

            ////  Запись в поток.
            //await request.SaveAsync(spreader, cancellationToken).ConfigureAwait(false);

            ////  Принудительная отправка данных.
            //await stream.FlushAsync(cancellationToken).ConfigureAwait(false);

            ////  Чтение ответа.
            //TcpAduPackage response = await TcpAduPackage.LoadAsync(spreader, cancellationToken).ConfigureAwait(false);

            ////  Получение массива данных.
            //byte[] data = response.PduPackage.Data;

            //StringBuilder output = new();

            //foreach (var value in data)
            //{
            //    output.Append($"0x{value:x2}, ");
            //}

            //Logger.LogInformation("Ответ: {output}", output.ToString());
        }
        catch (Exception ex)
        {
            Logger.LogError("{ex}", ex);
        }
        


        //


        ////  Основной цикл выполнения.
        //while (!cancellationToken.IsCancellationRequested)
        //{
        //    await ScanAsync(cancellationToken).ConfigureAwait(false);
        //}
    }


    /// <summary>
    /// Представляет функцию сканирования датчиков.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    public async Task ScanAsync(CancellationToken cancellationToken)
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

    /// <summary>
    /// Рассылает сообщение серии адресатов.
    /// </summary>
    /// <returns>
    /// Задача.
    /// </returns>
    private async Task SendAsync(IPAddress broadcast, CancellationToken cancellationToken)
    {
        //  Пересылка данных
        try
        {
            //  Получение IP адреса сервера NMEA
            IPAddress ipAddressSendServer = broadcast;

            //  Получение конечной точки сервера NMEA
            IPEndPoint endPoint = new(ipAddressSendServer, _TargetPort);

            //  Создание UDP-клиента.
            using UdpClient udpResender = new();

            //  Создание токена отмены
            using CancellationTokenSource source = new();

            //  Отмена задачи пересылки в случае приостановки 
            source.CancelAfter(200);

            //  Пересылка данных
            await udpResender.SendAsync(
                StrainIdentificationObsoleteRequest.Datagram,
                endPoint, source.Token).ConfigureAwait(false);

            //  Основной цикл.
            for (int i = 0; i < _CountIteration; i++)
            {
                if (udpResender.Available > 0)
                {
                    //  Получение UDP-датаграммы.
                    UdpReceiveResult result = await udpResender.ReceiveAsync(cancellationToken).ConfigureAwait(false);

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
                        StringBuilder output = new();
                        foreach (byte b in result.Buffer)
                        {
                            output.Append($"0x{b:x2}, ");
                        }
                        Logger.LogInformation("{message}", output.ToString());

                        //  Найден датчик.
                        Logger.LogInformation("Найден датчик: {point}, {serial:X8}", result.RemoteEndPoint, serial);
                    }

                    //  Продолжение цикла.
                    continue;
                }

                //  Ожидaние.
                await Task.Delay(100, cancellationToken).ConfigureAwait(false);
            }
        }
        catch { }
    }

    /// <summary>
    /// Представляет порт отправителя шировещательного пакета.
    /// </summary>
    private static readonly ushort _TargetPort = 49001;

    /// <summary>
    /// Представляет порт отправителя шировещательного пакета.
    /// </summary>
    private static readonly ushort _CountIteration = 4;//50;


    /// <summary>
    /// Поле для хранения идентификатора транзакции.
    /// </summary>
    private int _TransactionIdentifier;

    /// <summary>
    /// Возвращает идентификатор устройства.
    /// </summary>
    private byte SlaveIdentifier { get; } = 1;

    /// <summary>
    /// Создаёт запрос на чтение регистров хранения.
    /// </summary>
    /// <param name="start">
    /// Номер первого регистра.
    /// </param>
    /// <param name="count">
    /// Количество регистров.
    /// </param>
    /// <returns>
    /// Запрос на чтение.
    /// </returns>
    private TcpAduPackage RequestReadHoldings([NoVerify] int start, [NoVerify] int count)
    {
        //  Получение идентификатора транзакции.
        int transactionIdentifier = Interlocked.Increment(ref _TransactionIdentifier);

        //  Создание запроса.
        TcpAduPackage request = new(transactionIdentifier, SlaveIdentifier,
            PduFunctionCode.ReadHoldings,
            [
                unchecked((byte)(start >> 8)),
                unchecked((byte)(start >> 0)),
                unchecked((byte)(count >> 8)),
                unchecked((byte)(count >> 0))
            ]);

        //  Возврат запроса.
        return request;
    }

}
