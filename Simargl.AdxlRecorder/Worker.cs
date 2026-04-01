namespace Simargl.AdxlRecorder;

/// <summary>
/// Представляет основной фоновый процесс.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="tunings">
/// Настройки.
/// </param>
public class Worker(ILogger<Worker> logger, Tunings tunings) :
    BackgroundService
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
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл поддержки работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Выполнение основной работы.
                await InvokeAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (cancellationToken.IsCancellationRequested)
                {
                    //  Завершение работы.
                    return;
                }

                //  Вывод информации в журнал.
                logger.LogError("Произошло исключение: {ex}", ex);
            }

            //  Ожидание перед следующей попыткой.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    [SuppressMessage("Performance", "CA1873:Избегайте потенциально ресурсоемкого ведения журнала", Justification = "Необходима хзапись в журнал.")]
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Ожидание для инициализации консоли.
        await Task.Delay(1000, cancellationToken).ConfigureAwait(false);

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
                    tunings.Path,
                    $"{time:yyyy-MM-dd-HH}",
                    $"Adxl-{address}",
                    $"Adxl-{address}-{time:yyyy-MM-dd-HH-mm}.adxl");

                //  Возврат пути.
                return path;
            }, cancellationToken);

        //  Добавление обработчика ошибок при записи.
        recorder.Failed += failed;

        //  Создание локальной точки.
        IPEndPoint localEndPoint = new(IPAddress.Any, tunings.Port);

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

        //  Ожидание.
        await Task.Delay(Timeout.Infinite, cancellationToken).ConfigureAwait(false);

        //  Обрабатывает событие запуска приёма данных.
        void started(object? sender, EventArgs e)
        {
            //  Вывод информации в журнал.
            logger.LogInformation(
                "Запуск получения TCP-данных в локальной точке {point}.",
                localEndPoint);
        }

        //  Обрабатывает событие остановки приёма данных.
        void stopped(object? sender, EventArgs e)
        {
            //  Вывод информации в журнал.
            logger.LogInformation(
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
            }
        }

        //  Обрабатывает ошибки.
        void failed(object? sender, FailedEventArgs e)
        {
            //  Вывод информации об ошибке.
            logger.LogError("Произошла ошибка при регистрации данных ускорений {ex}", e.Exception);
        }

        //  Обрабатывает событие подключения датчика.
        void connected(object? sender, TcpDataReceiverEventArgs e)
        {
            //  Вывод информации в журнал.
            logger.LogInformation("Подключился датчик ускорения {endPoint} в {time}.", e.IPEndPoint, e.Time);
        }

        //  Обрабатывает событие отключения датчика.
        void disconnected(object? sender, TcpDataReceiverEventArgs e)
        {
            //  Вывод информации в журнал.
            logger.LogInformation("Отключился датчик ускорения {endPoint} в {time}.", e.IPEndPoint, e.Time);
        }
    }
}
