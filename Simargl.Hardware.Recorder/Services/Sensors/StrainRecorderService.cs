using Microsoft.Extensions.Logging;
using Simargl.Hardware.Receiving;
using Simargl.Hardware.Receiving.Net;
using Simargl.Hardware.Recorder.Core;
using Simargl.Hardware.Recording;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Simargl.Hardware.Recorder.Services.Sensors;

/// <summary>
/// Представляет службу регистрации данных с тензомодулей.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="heart">
/// Сердце приложения.
/// </param>
public sealed class StrainRecorderService(
    ILogger<StrainRecorderService> logger, Heart heart) :
    Service<StrainRecorderService>(logger, heart)
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
        //  Создание средства записи данных.
        DataRecorder recorder = new(
            delegate (DataReceiveResult data)
            {
                //  Проверка данных.
                if (data is not TcpDataReceiveResult tcpData)
                {
                    //  Недопустимый блок данных.
                    throw new InvalidDataException("Получен недопустимый блок данных.");
                }

                //  Определение времени приёма.
                DateTime time = tcpData.ReceiptTime;

                //  Определение адреса датчика.
                string address = string.Join('-', tcpData.EndPoint.Address.GetAddressBytes().Select(x => x.ToString()));

                //  Получение пути к файлу.
                string path = Path.Combine(
                    Heart.Options.DataPath,
                    $"{time:yyyy-MM-dd-HH}",
                    $"Strain-{address}",
                    $"Strain-{address}-{time:yyyy-MM-dd-HH-mm}.strain");

                //  Возврат пути.
                return path;
            }, cancellationToken);

        //  Добавление обработчика ошибок при записи.
        recorder.Failed += failed;

        //  Создание локальной точки.
        IPEndPoint localEndPoint = new(IPAddress.Any, 49003);

        //  Создание приёмника данных от TCP-клиента.
        TcpDataReceiver receiver = new(localEndPoint, 1409, TimeSpan.FromSeconds(60), cancellationToken);

        //  Добавление обработчиков событий.
        receiver.Received += received;
        receiver.Started += started;
        receiver.Stopped += stopped;
        receiver.Failed += failed;
        receiver.Connected += connected;
        receiver.Disconnected += disconnected;

        //  Запуск приёма данных.
        receiver.Start();

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Извлечение ключей.
            while (Heart.BadStrainKeys.TryDequeue(out long key) &&
                !cancellationToken.IsCancellationRequested)
            {
                //  Остановка соединения.
                receiver.Stop(key);
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }

        //  Обрабатывает событие запуска приёма данных.
        void started(object? sender, EventArgs e)
        {
            //  Вывод информации в журнал.
            Logger.LogInformation(
                "Запуск получения TCP-данных в локальной точке {point}.",
                localEndPoint);
        }

        //  Обрабатывает событие остановки приёма данных.
        void stopped(object? sender, EventArgs e)
        {
            //  Вывод информации в журнал.
            Logger.LogInformation(
                "Остановка получения TCP-данных в локальной точке {point}.",
                localEndPoint);

            //  Удаление обработчиков событий.
            receiver.Received -= received;
            receiver.Started -= started;
            receiver.Stopped -= stopped;
            receiver.Failed -= failed;
            receiver.Connected -= connected;
            receiver.Disconnected -= disconnected;
        }

        //  Обрабатывает событие получения данных.
        void received(object? sender, DataReceiverReceivedEventArgs e)
        {
            //  Получение данных.
            if (e.Data is TcpDataReceiveResult data)
            {
                //  Запись данных.
                recorder.AddData(data);

                //  Регистрация данных.
                Heart.Measurer.AddStrainData(data);
            }
        }

        //  Обрабатывает ошибки.
        void failed(object? sender, FailedEventArgs e)
        {
            //  Вывод информации об ошибке.
            Logger.LogError("Произошла ошибка при регистрации тнезометрических данных {ex}", e.Exception);
        }

        //  Обрабатывает событие подключения датчика.
        void connected(object? sender, TcpDataReceiverEventArgs e)
        {
            //  Вывод информации в журнал.
            Logger.LogInformation("Подключился тензометрический модуль {endPoint} в {time}.", e.IPEndPoint, e.Time);
        }

        //  Обрабатывает событие отключения датчика.
        void disconnected(object? sender, TcpDataReceiverEventArgs e)
        {
            //  Вывод информации в журнал.
            Logger.LogInformation("Отключился тензометрический модуль {endPoint} в {time}.", e.IPEndPoint, e.Time);
        }
    }

    //private async Task ListenerAsync(CancellationToken cancellationToken)
    //{
    //    TcpListener listener = new(IPAddress.Any, 49003);

    //    try
    //    {
    //        listener.Start();

    //        TcpClient client = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);
    //        _ = Task.Run(async delegate
    //        {
    //            await ClientAsync(client, cancellationToken).ConfigureAwait(false);
    //        }, CancellationToken.None);
    //    }
    //    finally
    //    {
    //        listener.Stop();
    //    }
    //}

    //private async Task ClientAsync(TcpClient tcpClient, CancellationToken cancellationToken)
    //{
    //    const int size = 400 * 636;
    //    byte[] buffer = new byte[size];
    //    using TcpClient client = tcpClient;
    //    if (client.Client.RemoteEndPoint is not IPEndPoint endPoint ||
    //        endPoint.AddressFamily != AddressFamily.InterNetwork)
    //    {
    //        return;
    //    }
    //    string address = endPoint.Address.ToString();

    //    Logger.LogInformation("Подключился тензомодуль {address}.", address);
    //    using NetworkStream stream = client.GetStream();

    //    while (!cancellationToken.IsCancellationRequested)
    //    {
    //        await stream.ReadExactlyAsync(buffer, cancellationToken).ConfigureAwait(false);
    //        DateTime time = DateTime.Now;


    //        string path = Path.Combine(Heart.Options.DataPath, $"{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}", $"Strain-{address}");
    //        if (!Directory.Exists(path))
    //        {
    //            Directory.CreateDirectory(path);
    //        }
    //        path = Path.Combine(path, $"Strain-{address}-{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}-{time.Minute:00}-{time.Second:00}-{time.Millisecond:000}.strain");
    //        File.WriteAllBytes(path, buffer);
    //    }
    //}
}
