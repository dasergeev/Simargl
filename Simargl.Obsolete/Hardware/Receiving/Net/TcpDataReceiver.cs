using System.Net;
using System.Net.Sockets;

namespace Simargl.Hardware.Receiving.Net;

/// <summary>
/// Представляет приёмник данных от TCP-клиентов.
/// </summary>
/// <param name="localEndPoint">
/// Локальная конечная точка сокета.
/// Определяет IP-адрес и номер порта, на которых клиент принимает входящие TCP-соединения.
/// </param>
/// <param name="blockSize">
/// Размер блоков для чтения.
/// </param>
/// <param name="timeout">
/// Максимальное время ожидания данных.
/// </param>
/// <param name="cancellationToken">
/// Токен отмены.
/// </param>
public sealed class TcpDataReceiver(
    IPEndPoint localEndPoint, int blockSize, TimeSpan timeout,
    CancellationToken cancellationToken) :
    DataReceiver(cancellationToken)
{
    /// <summary>
    /// Поле для хранения последнего ключа соединения.
    /// </summary>
    private static long _LastConnectionKey = -1;

    /// <summary>
    /// Происходит при подключении клиента.
    /// </summary>
    public event TcpDataReceiverEventHandler? Connected;

    /// <summary>
    /// Происходит при отключении клиента.
    /// </summary>
    public event TcpDataReceiverEventHandler? Disconnected;

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токена отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override sealed async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Создание средства прослушивания сети.
        TcpListener listener = new(localEndPoint);

        //  Блок с гарантированным завершением.
        try
        {
            //  Запуск прослушивания сети.
            listener.Start();

            //  Получение клиента.
            TcpClient client = await listener.AcceptTcpClientAsync(cancellationToken).ConfigureAwait(false);

            //  Запуск асинхронной задачи.
            _ = Task.Run(async delegate
            {
                //  Выполнение работы с TCP-клиентом.
                await InvokeAsync(client, cancellationToken).ConfigureAwait(false);
            }, CancellationToken.None);

        }
        finally
        {
            //  Остановка прослушивания сети.
            listener.Stop();
        }
    }

    private event EventHandler<long>? NeedStopeed;

    /// <summary>
    /// Останавливает соединение.
    /// </summary>
    /// <param name="connectionKey">
    /// Ключ соединения.
    /// </param>
    public void Stop(long connectionKey)
    {
        Volatile.Read(ref NeedStopeed)?.Invoke(this, connectionKey);
    }

    /// <summary>
    /// Асинхронно выполняет работу с TCP-клиентом.
    /// </summary>
    /// <param name="tcpClient">
    /// TCP-клиент.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с TCP-клиентом.
    /// </returns>
    private async Task InvokeAsync(TcpClient tcpClient, CancellationToken cancellationToken)
    {
        //  Захват клиента.
        using TcpClient client = tcpClient;

        //  Проверка клиента.
        if (client.Client.RemoteEndPoint is not IPEndPoint endPoint)
        {
            //  Завершение работы с клиентом.
            return;
        }

        //  Определение времени начала записи.
        DateTime startTime = DateTime.Now;

        //  Получение ключа соединения.
        long connectionKey = Interlocked.Increment(ref _LastConnectionKey);

        //  Создание источника токена отмены.
        using CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        //  Замена токена отмены.
        cancellationToken = source.Token;

        //  Добавление обработчика события.
        NeedStopeed += (_, key) =>
        {
            if (key == connectionKey)
            {
                try
                {
                    source.Cancel();
                }
                catch { }
            }
        };

        //  Инициализация индекса блока.
        int blockIndex = 0;

        //  Создание аргументов события.
        TcpDataReceiverEventArgs e = new(endPoint, startTime);

        //  Вызов события.
        OnConnected(e);

        //  Блок с гарантированным завершением.
        try
        {
            //  Получение потока для чтения данных.
            using NetworkStream stream = client.GetStream();

            //  Основной цикл чтения данных.
            while (!cancellationToken.IsCancellationRequested)
            {
                //  Создание источника токена отмены.
                using CancellationTokenSource tokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

                //  Получение токена отмены.
                CancellationToken token = tokenSource.Token;

                //  Настройка отмены.
                tokenSource.CancelAfter(timeout);

                //  Создание буфера для данных.
                byte[] buffer = new byte[blockSize];

                //  Чтение данных.
                await stream.ReadExactlyAsync(buffer, token).ConfigureAwait(false);

                //  Определение времени получения.
                DateTime receiptTime = DateTime.Now;

                //  Создание результатов.
                TcpDataReceiveResult data = new(
                    endPoint, startTime, connectionKey,
                    blockIndex, receiptTime, buffer);

                //  Создание аргументов события.
                DataReceiverReceivedEventArgs args = new(data);

                //  Вызов события.
                OnReceived(args);

                //  Увеличение индекса блока.
                ++blockIndex;
            }
        }
        finally
        {
            //  Вызов события.
            OnDisconnected(e);
        }
    }

    /// <summary>
    /// Вызывает событие <see cref="Connected"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnConnected(TcpDataReceiverEventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref Connected)?.Invoke(this, e);
    }

    /// <summary>
    /// Вызывает событие <see cref="Disconnected"/>.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void OnDisconnected(TcpDataReceiverEventArgs e)
    {
        //  Вызов события.
        Volatile.Read(ref Disconnected)?.Invoke(this, e);
    }
}
