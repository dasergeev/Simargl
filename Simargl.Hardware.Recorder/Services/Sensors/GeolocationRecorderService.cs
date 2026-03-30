using Microsoft.Extensions.Logging;
using Simargl.Hardware.Receiving;
using Simargl.Hardware.Receiving.Net;
using Simargl.Hardware.Recorder.Core;
using Simargl.Hardware.Recording;
using System.IO;
using System.Net;

namespace Simargl.Hardware.Recorder.Services.Sensors;

/// <summary>
/// Представляет службу регистрации данных геолокации.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="heart">
/// Сердце приложения.
/// </param>
public sealed class GeolocationRecorderService(
    ILogger<GeolocationRecorderService> logger, Heart heart) :
    Service<GeolocationRecorderService>(logger, heart)
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
                //  Определение времени приёма.
                DateTime time = data.ReceiptTime;

                //  Получение пути к файлу.
                string path = Path.Combine(
                    Heart.Options.DataPath,
                    $"{time:yyyy-MM-dd-HH}",
                    "Nmea",
                    $"Nmea-{time:yyyy-MM-dd-HH-mm}.nmea");

                //  Возврат пути.
                return path;
            }, cancellationToken);

        //  Добавление обработчика ошибок при записи.
        recorder.Failed += failed;

        //  Создание локальной точки.
        IPEndPoint localEndPoint = new(IPAddress.Any, 8500);

        //  Создание приёмника UDP-датаграмм.
        UdpDataReceiver receiver = new(localEndPoint, cancellationToken);

        //  Добавление обработчиков событий.
        receiver.Received += received;
        receiver.Started += started;
        receiver.Stopped += stopped;
        receiver.Failed += failed;

        //  Запуск приёма данных.
        receiver.Start();

        //  Ожидание завершения задачи.
        await Task.Delay(-1, cancellationToken).ConfigureAwait(false);

        //  Обрабатывает событие запуска приёма данных.
        void started(object? sender, EventArgs e)
        {
            //  Вывод информации в журнал.
            Logger.LogInformation(
                "Запуск получения UDP-датаграмм в локальной точке {point}.",
                localEndPoint);
        }
        
        //  Обрабатывает событие остановки приёма данных.
        void stopped(object? sender, EventArgs e)
        {
            //  Вывод информации в журнал.
            Logger.LogInformation(
                "Остановка получения UDP-датаграмм в локальной точке {point}.",
                localEndPoint);

            //  Удаление обработчиков событий.
            receiver.Received -= received;
            receiver.Started -= started;
            receiver.Stopped -= stopped;
            receiver.Failed -= failed;
        }

        //  Обрабатывает событие получения данных.
        void received(object? sender, DataReceiverReceivedEventArgs e)
        {
            //  Получение данных.
            if (e.Data is UdpDataReceiveResult data)
            {
                //  Запись данных.
                recorder.AddData(data);

                //  Регистрация данных.
                Heart.Measurer.AddGeolocationData(data);
            }
        }

        //  Обрабатывает ошибки.
        void failed(object? sender, FailedEventArgs e)
        {
            //  Вывод информации об ошибке.
            Logger.LogError("Произошла ошибка при регистрации данных геолокации {ex}", e.Exception);
        }
    }
}
