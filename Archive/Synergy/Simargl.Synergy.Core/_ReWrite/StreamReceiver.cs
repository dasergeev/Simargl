//using System.Collections.Concurrent;
//using System.IO;

//namespace Simargl.Synergy.Core;

///// <summary>
///// Представляет получателя данных через поток.
///// </summary>
//internal sealed class StreamReceiver :
//    Critical
//{
//    /// <summary>
//    /// Поле для хранения потока.
//    /// </summary>
//    private readonly Stream _Stream;

//    /// <summary>
//    /// Поле для хранения очереди полученных блоков.
//    /// </summary>
//    private ConcurrentQueue<Block>? _Queue;

//    /// <summary>
//    /// Инициализирует новый экземпляр.
//    /// </summary>
//    /// <param name="stream">
//    /// Поток.
//    /// </param>
//    private StreamReceiver(Stream stream)
//    {
//        //  Установка потока.
//        _Stream = stream;

//        //  Создание очереди.
//        _Queue = [];

//        //  Добавление методов разрушения.
//        AddDestroyer(delegate
//        {
//            //  Извлечение очереди на отправку.
//            ConcurrentQueue<Block>? queue = Interlocked.Exchange(ref _Queue, null);

//            //  Проверка очереди на отправку.
//            if (queue is not null)
//            {
//                //  Разрушение очереди на отправку.
//                DisposeQueue(queue);
//            }
//        });
//        AddDestroyer(_Stream.Close);
//        AddDestroyer(_Stream.Dispose);
//    }

//    /// <summary>
//    /// Пытается извлечь блок из очереди.
//    /// </summary>
//    /// <param name="block">
//    /// Ссылка наблок.
//    /// </param>
//    /// <returns>
//    /// Результат попытки.
//    /// </returns>
//    public bool TryDequeue(out Block? block)
//    {
//        //  Получение очереди.
//        ConcurrentQueue<Block> queue = Volatile.Read(ref _Queue) ?? throw new ObjectDisposedException(nameof(StreamReceiver));

//        //  Извлечение блока из очереди.
//        return queue.TryDequeue(out block);
//    }

//    /// <summary>
//    /// Асинхронно создаёт получателя данных через поток.
//    /// </summary>
//    /// <param name="stream">
//    /// Поток.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, создающая получателя данных через поток.
//    /// </returns>
//    public static async Task<StreamReceiver> CreateAsync(Stream stream, CancellationToken cancellationToken)
//    {
//        //  Проверка потока.
//        IsNotNull(stream);
//        if (!stream.CanRead) throw new InvalidOperationException("Поток не поддерживает чтение.");

//        //  Проверка токена отмены.
//        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

//        //  Создание получателя данных.
//        StreamReceiver receiver = new(stream);

//        //  Запуск аснихронного выполнения.
//        _ = Task.Run(async delegate
//        {
//            //  Получение данных.
//            await receiver.InvokeAsync(receiver.CancellationToken).ConfigureAwait(false);
//        }, CancellationToken.None);

//        //  Возврат получателя данных.
//        return receiver;
//    }

//    /// <summary>
//    /// Асинхронно выполняет основную работу.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая основную работу.
//    /// </returns>
//    private async Task InvokeAsync(CancellationToken cancellationToken)
//    {
//        //  Блок с гарантированным завершением.
//        try
//        {
//            //  Получение очереди полученных блоков.
//            ConcurrentQueue<Block> queue = Volatile.Read(ref _Queue) ?? throw new ObjectDisposedException(nameof(StreamSender));

//            //  Основной цикл получения данных.
//            while (!cancellationToken.IsCancellationRequested)
//            {
//                //  Чтение блока.
//                Block block = await Block.LoadAsync(_Stream, cancellationToken).ConfigureAwait(false);

//                //  Добавление блока в очередь.
//                queue.Enqueue(block);
//            }
//        }
//        finally
//        {
//            //  Разрушение объекта.
//            Dispose();
//        }
//    }

//    /// <summary>
//    /// Разрушает очередь полученных блоков.
//    /// </summary>
//    /// <param name="queue">
//    /// Очередь полученных блоков.
//    /// </param>
//    private static void DisposeQueue(ConcurrentQueue<Block> queue)
//    {
//        //  Блок перехвата всех исключений.
//        try
//        {
//            //  Извлечение элементов из очереди.
//            while (queue.TryDequeue(out Block? block))
//            {
//                //  Блок перехвата всех исключений.
//                try
//                {
//                    //  Разрушение блока.
//                    block?.Dispose();
//                }
//                catch { }
//            }
//        }
//        catch { }
//    }
//}
