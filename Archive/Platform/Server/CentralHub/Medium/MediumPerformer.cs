//using Apeiron.Platform.Journals;
//using Apeiron.Platform.Performers;
//using Apeiron.Threading;
//using System.Collections.Concurrent;
//using System.Net;
//using System.Net.Sockets;

//namespace Apeiron.Platform.Medium;

///// <summary>
///// Представляет исполнителя, выполняющего обмен данными.
///// </summary>
//internal sealed class MediumPerformer :
//    Performer
//{
//    /// <summary>
//    /// Порт для подключения клиентов.
//    /// </summary>
//    private const int _Port = 7032;

//    /// <summary>
//    /// Постоянная, определяющая сигнатуру датаграммы.
//    /// </summary>
//    private const uint _Signature = 0x6e727041;

//    /// <summary>
//    /// Постоянная, определяющая формат датаграммы.
//    /// </summary>
//    private const uint _Format = 1;

//    /// <summary>
//    /// Поле для хранения очереди входящих датаграмм.
//    /// </summary>
//    private readonly ConcurrentQueue<UdpReceiveResult> _ReceiveQueue;

//    /// <summary>
//    /// Поле для хранения словаря конечных точек подключения.
//    /// </summary>
//    private readonly ConcurrentDictionary<int, IPEndPoint> _IPEndPoints;

//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    /// <param name="journal">
//    /// Журнал.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="journal"/> передана пустая ссылка.
//    /// </exception>
//    public MediumPerformer(Journal journal) :
//        base(journal)
//    {
//        //  Создание очереди входящих датаграмм.
//        _ReceiveQueue = new();

//        //  Создание словаря конечных точек подключения.
//        _IPEndPoints = new();
//    }

//    /// <summary>
//    /// Асинхронно выполняет работу исполнителя.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая работу исполнителя.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    public override sealed async Task PerformAsync(CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        //  Выполнение действий.
//        await KeepAsync(new AsyncAction[]
//        {
//            ReceiverAsync,
//            ParseAsync,
//        }, cancellationToken).ConfigureAwait(false);
//    }

//    /// <summary>
//    /// Асинхронно выполняет приём данных.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая приём данных.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    private async Task ReceiverAsync(CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        //  Создание UDP-клиента для получения пакетов.
//        using UdpClient udpClient = new()
//        {
//            //  Разрешение использовать порт нескольким клиентам.
//            ExclusiveAddressUse = false,
//        };

//        //  Настройка разрешения для UDP-клиента для использования одного адреса.
//        udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

//        //  Связывание UDP-клиента с конечной точкой.
//        udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, _Port));

//        //  Основной цикл работы приёма данных.
//        while (!cancellationToken.IsCancellationRequested)
//        {
//            //  Получение UDP-датаграммы.
//            UdpReceiveResult result = await udpClient.ReceiveAsync(cancellationToken).ConfigureAwait(false);
            
//            //  Добавление датаграммы в очередь.
//            _ReceiveQueue.Enqueue(result);
//        }
//    }

//    /// <summary>
//    /// Асинхронно выполняет разбор входящих датаграмм.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая разбор входящих датаграмм.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    private async Task ParseAsync(CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        //  Список входящих датаграмм.
//        List<UdpReceiveResult> results = new();

//        //  Получение UDP-датаграммы.
//        while (
//            _ReceiveQueue.TryDequeue(out UdpReceiveResult result) &&
//            !cancellationToken.IsCancellationRequested)
//        {
//            //  Добавление датаграммы в список.
//            results.Add(result);
//        }

//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        //  Проверка списка входящих датаграмм.
//        if (results.Count == 0)
//        {
//            //  Завершение работы.
//            return;
//        }

//        //  Асинхронная работа с датаграммами.
//        await Parallel.ForEachAsync(
//            results,
//            new ParallelOptions
//            {
//                CancellationToken = cancellationToken
//            },
//            AnalysisAsync).ConfigureAwait(false);
//    }

//    /// <summary>
//    /// Асинхронно выполняет анализ датаграммы.
//    /// </summary>
//    /// <param name="result">
//    /// UDP-датаграмма.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая анализ датаграммы.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    private async ValueTask AnalysisAsync(
//        [ParameterNoChecks] UdpReceiveResult result, CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        //  Безопасная работа с датаграммой.
//        await SafeCallAsync(async cancellationToken =>
//        {
//            //  Проверка токена отмены.
//            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//            //  Проверка удалённой сетевой конечной точки.
//            if (result.RemoteEndPoint is not IPEndPoint senderIPEndPoint)
//            {
//                //  Неизвестный источник.
//                return;
//            }

//            //  Получение буфера.
//            byte[] buffer = result.Buffer;

//            //  Создание потока для чтения данных.
//            await using MemoryStream stream = new(buffer);

//            //  Создание средства чтения двоичных данных.
//            using BinaryReader reader = new(stream);

//            //  Проверка сигнатуры.
//            if (reader.ReadUInt32() != _Signature)
//            {
//                //  Неверная сигнатура.
//                return;
//            }

//            //  Проверка формата.
//            if (reader.ReadUInt32() != _Format)
//            {
//                //  Неверный формат.
//                return;
//            }

//            //  Проверка размера.
//            if (reader.ReadInt64() != buffer.Length)
//            {
//                //  Неверный размер.
//                return;
//            }

//            //  Чтение идентификатора отправителя.
//            int senderId = reader.ReadInt32();

//            //  Чтение идентификатора получателя.
//            int recipientId = reader.ReadInt32();

//            //  Проверка токена отмены.
//            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//            //  Обновление словаря конечных точек.
//            _IPEndPoints.AddOrUpdate(senderId, senderIPEndPoint, (key, oldValue) => senderIPEndPoint);

//            //  Отправка пакета.
//            await SendAsync(recipientId, buffer, cancellationToken).ConfigureAwait(false);
//        }, cancellationToken).ConfigureAwait(false);
//    }

//    /// <summary>
//    /// Асинхронно отправляет датаграмму на узел с заданным идентификатором.
//    /// </summary>
//    /// <param name="id">
//    /// Идентификатор узла.
//    /// </param>
//    /// <param name="datagram">
//    /// Датаграмма.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая отправку датаграммы.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    private async Task SendAsync(int id, [ParameterNoChecks] ReadOnlyMemory<byte> datagram, CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        //  Безопасная работа с датаграммой.
//        await SafeCallAsync(async cancellationToken =>
//        {
//            //  Проверка токена отмены.
//            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//            //  Получение конечной точки для пересылки.
//            if (!_IPEndPoints.TryGetValue(id, out IPEndPoint? recipientIPEndPoint) ||
//                recipientIPEndPoint is null)
//            {
//                //  Не найдена конечная точка.
//                return;
//            }

//            //  Проверка токена отмены.
//            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//            //  Создание UDP-клиента для пересылки пакета.
//            using UdpClient recipientUdpClient = new()
//            {
//                //  Разрешение использовать порт нескольким клиентам.
//                ExclusiveAddressUse = false,
//            };

//            //  Настройка разрешения для UDP-клиента для использования одного адреса.
//            recipientUdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

//            //  Связывание UDP-клиента с конечной точкой.
//            recipientUdpClient.Client.Bind(new IPEndPoint(IPAddress.Any, _Port));

//            //  Установка соедиения.
//            recipientUdpClient.Connect(recipientIPEndPoint.Address, recipientIPEndPoint.Port);

//            //  Отправка ответного пакета.
//            await recipientUdpClient.SendAsync(datagram, cancellationToken).ConfigureAwait(false);
//        }, cancellationToken).ConfigureAwait(false);
//    }
//}
