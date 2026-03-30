using Microsoft.Extensions.Logging;
using Simargl.Border.Hardware;
using Simargl.Border.Processing;
using Simargl.Border.Recorder.Configuring;
using Simargl.Net;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Simargl.Border.Recorder.Services.Main;

/// <summary>
/// Представляет службу прослушивания TCP-подключений.
/// </summary>
/// <param name="program">
/// Программа.
/// </param>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public sealed class TcpListeningService(Program program, ILogger<TcpListeningService> logger) :
    Service(program, logger)
{
    /// <summary>
    /// Поле для хранения следующего ключа соединения.
    /// </summary>
    private static long _NextConnectionKey;

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
        //  Получение конфигурации.
        Configuration configuration = await Program.GetConfigurationAsync(cancellationToken).ConfigureAwait(false);

        //  Создание локальной точки.
        IPEndPoint localEndPoint = new(IPAddress.Any, configuration.ConnectingSensorsPort);

        //  Создание средства прослушивания сети.
        TcpListener listener = new(localEndPoint);

        //  Блок с гарантированным завершением.
        try
        {
            //  Запуск прослушивания сети.
            listener.Start();

            //  Основной цикл ожидания подключений.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Ожидание подключения.
                TcpClient client = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

                //  Отключить задержку буферов.
                client.NoDelay = true;

                //  Запуск асинхронной задачи.
                _ = Task.Run(async delegate
                {
                    //  Блок с гарантированным завершением.
                    try
                    {
                        //  Выполнение работы с клиентом.
                        await InvokeAsync(client, cancellationToken).ConfigureAwait(false);
                    }
                    finally
                    {
                        //  Разрушение клиента.
                        client.Dispose();
                    }
                }, CancellationToken.None);
            }
        }
        finally
        {
            //  Остановка прослушивания сети.
            listener.Stop();
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с клиентом.
    /// </summary>
    /// <param name="client">
    /// Клиент.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу.
    /// </returns>
    private async Task InvokeAsync(TcpClient client, CancellationToken cancellationToken)
    {
        //  Получение конфигурации.
        Configuration configuration = await Program.GetConfigurationAsync(cancellationToken).ConfigureAwait(false);

        //  Получения пути к данным.
        string dataPath = configuration.DataPath;

        //  Проверка источника.
        if (client.Client.RemoteEndPoint is not IPEndPoint endPoint)
        {
            //  Вывод информации в журнал.
            Logger.LogWarning("Подключилось неизвестное устройство \"{EndPoint}\".", client.Client.RemoteEndPoint);

            //  Завершение работы.
            return;
        }

        //  Получение адреса.
        byte[] address = endPoint.Address.GetAddressBytes();
        if (address.Length != 4)
        {
            //  Вывод информации в журнал.
            Logger.LogWarning("Подключилось неизвестное устройство \"{EndPoint}\".", client.Client.RemoteEndPoint);

            //  Завершение работы.
            return;
        }

        //  Получение IP-адреса.
        IPv4Address ipAddress = new(endPoint.Address);

        //  Получение устройства обработки.
        Processor processor = await Program.GetProcessorAsync(cancellationToken).ConfigureAwait(false);

        //  Блок перехвата всех исключений.
        try
        {
            //  Получение устройства.
            Device device = processor.Devices[ipAddress];

            //Sensor sensor = Program.Sensors[$"{address[0]:000}.{address[1]:000}.{address[2]:000}.{address[3]:000}"];

            //  Получение потока для чтения данных.
            await using NetworkStream stream = client.GetStream();

            ////  Создание распределителя данных.
            //Spreader spreader = new(stream);

            //  Настройка данных соединения.
            long connectionKey = Interlocked.Increment(ref _NextConnectionKey) - 1;
            int blockIndex = 0;
            DateTime startTime = DateTime.Now;

            //  Основной цикл чтения.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Создание источника токена отмены.
                using CancellationTokenSource timeoutTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

                //  Настройка отмены.
                timeoutTokenSource.CancelAfter(BasisConstants.SensorTimeout);

                //  Создание буфера.
                byte[] buffer = new byte[BasisConstants.SensorPackageSize];

                //  Чтение данных.
                await stream.ReadExactlyAsync(buffer.AsMemory(), timeoutTokenSource.Token).ConfigureAwait(false);

                //  Получение текущего времени.
                DateTime receiptTime = DateTime.Now;

                //  Создание пакета данных.
                DevicePackage package = new(endPoint, startTime, connectionKey, blockIndex, receiptTime, buffer);

                //  Регистрация данных.
                await device.RegisterAsync(package, cancellationToken).ConfigureAwait(false);

                //  Смещение блока.
                ++blockIndex;
            }
        }
        catch (Exception ex)
        {
            //  Проверка токена отмены.
            if (!cancellationToken.IsCancellationRequested)
            {
                //  Вывод информации в журнал.
                Logger.LogError(ex, "Ошибка при работе с датчиком.");
            }

            //  Повторный выброс исключения.
            throw;
        }
    }
}
