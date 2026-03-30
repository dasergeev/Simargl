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
/// Представляет службу регистрации данных с датчиков ускорений.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="heart">
/// Сердце приложения.
/// </param>
public sealed class AccelEth3TRecorderService(
    ILogger<AccelEth3TRecorderService> logger, Heart heart) :
    Service<AccelEth3TRecorderService>(logger, heart)
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
                    $"Adxl-{address}",
                    $"Adxl-{address}-{time:yyyy-MM-dd-HH-mm}.adxl");

                //  Возврат пути.
                return path;
            }, cancellationToken);

        //  Добавление обработчика ошибок при записи.
        recorder.Failed += failed;

        //  Создание локальной точки.
        IPEndPoint localEndPoint = new(IPAddress.Any, 49001);

        //  Создание приёмника данных от TCP-клиента.
        TcpDataReceiver receiver = new(localEndPoint, 636, TimeSpan.FromSeconds(60), cancellationToken);

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
            while (Heart.BadAdxlKeys.TryDequeue(out long key) &&
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
                Heart.Measurer.AddAdxlData(data);
            }
        }

        //  Обрабатывает ошибки.
        void failed(object? sender, FailedEventArgs e)
        {
            //  Вывод информации об ошибке.
            Logger.LogError("Произошла ошибка при регистрации данных ускорений {ex}", e.Exception);
        }

        //  Обрабатывает событие подключения датчика.
        void connected(object? sender, TcpDataReceiverEventArgs e)
        {
            //  Вывод информации в журнал.
            Logger.LogInformation("Подключился датчик ускорения {endPoint} в {time}.", e.IPEndPoint, e.Time);
        }

        //  Обрабатывает событие отключения датчика.
        void disconnected(object? sender, TcpDataReceiverEventArgs e)
        {
            //  Вывод информации в журнал.
            Logger.LogInformation("Отключился датчик ускорения {endPoint} в {time}.", e.IPEndPoint, e.Time);
        }
    }
}
