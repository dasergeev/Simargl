//using System.Collections.Concurrent;
//using System.IO;
//using System.Net.Security;

//namespace Simargl.Synergy.Core;

///// <summary>
///// Представляет отправителя данных через поток.
///// </summary>
//internal sealed class StreamSender :
//    Critical
//{
//    /// <summary>
//    /// Поле для хранения подключения.
//    /// </summary>
//    private Connection _Connection = null!;

//    /// <summary>
//    /// Поле для хранения очереди на отправку.
//    /// </summary>
//    private ConcurrentQueue<BlockInfo>? _Queue;

//    /// <summary>
//    /// Инициализирует новый экземпляр.
//    /// </summary>
//    private StreamSender()
//    {

//    }

//    /// <summary>
//    /// Асинхронно отправляет данные.
//    /// </summary>
//    /// <param name="block">
//    /// Блок данных.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая отправку данных.
//    /// </returns>
//    public async Task SendAsync(Block block, CancellationToken cancellationToken)
//    {
//        //  Создание источника связанного токена отмены.
//        using CancellationTokenSource linkedTokenSource = new Func<CancellationTokenSource>(delegate
//        {
//            //  Блок перехвата всех исключений.
//            try
//            {
//                //  Создание связанного источника токена отмены.
//                return CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
//            }
//            catch
//            {
//                //  Проверка токена отмены.
//                cancellationToken.ThrowIfCancellationRequested();

//                //  Повторный выброс исключения.
//                throw;
//            }
//        }).Invoke();

//        //  Получение связанного токена отмены.
//        CancellationToken linkedToken = linkedTokenSource.Token;

//        //  Получение очереди на отправку.
//        ConcurrentQueue<BlockInfo> queue = Volatile.Read(ref _Queue) ?? throw new ObjectDisposedException(nameof(StreamSender));

//        //  Создание информации о блоке памяти.
//        BlockInfo blockInfo = new(block);

//        //  Добавление обработчика отмены.
//        linkedToken.Register(delegate
//        {
//            //  Блок перехвата всех исключений.
//            try
//            {
//                //  Извлечение данных.
//                Block? block = blockInfo.TryGetBlock();

//                //  Проверка данных.
//                if (block is not null)
//                {
//                    //  Установка результата.
//                    blockInfo.CompletionSource.TrySetException(new OperationCanceledException());
//                }
//            }
//            catch { }
//        });

//        //  Добавление данных в очередь.
//        queue.Enqueue(blockInfo);

//        //  Проверка очереди на отправку.
//        if (Volatile.Read(ref _Queue) is null)
//        {
//            //  Разрушение очереди на отправку.
//            DisposeQueue(queue);

//            //  Установка исключения.
//            blockInfo.CompletionSource.TrySetException(new ObjectDisposedException(nameof(StreamSender)));
//        }

//        //  Ожидание завершения задачи.
//        await blockInfo.CompletionSource.Task.ConfigureAwait(false);
//    }

//    /// <summary>
//    /// Асинхронно создаёт отправителя данных через поток.
//    /// </summary>
//    /// <param name="connection">
//    /// Подключение.
//    /// </param>
//    /// <param name="delay">
//    /// Максимальная задержка при отправке данных.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, создающая отправителя данных через поток.
//    /// </returns>
//    public static async Task<StreamSender> CreateAsync(Connection connection, TimeSpan delay, CancellationToken cancellationToken)
//    {
//        //  Создание отправителя данных.
//        StreamSender sender = new();

//        //  Блок перехвата всех исключений.
//        try
//        {
//            //  Установка подключения.
//            sender._Connection = connection;
//            await sender.AddDestroyerAsync(connection.DisposeAsync);

//            //  Создание очереди.
//            sender._Queue = [];
//            await sender.AddDestroyerAsync(delegate
//            {
//                //  Извлечение очереди на отправку.
//                ConcurrentQueue<BlockInfo>? queue = Interlocked.Exchange(ref sender._Queue, null);

//                //  Проверка очереди на отправку.
//                if (queue is not null)
//                {
//                    //  Разрушение очереди на отправку.
//                    DisposeQueue(queue);
//                }
//            });

//            //  Запуск аснихронного выполнения.
//            _ = Task.Run(async delegate
//            {
//                //  Отправка данных.
//                await sender.InvokeAsync(delay, sender.CancellationToken).ConfigureAwait(false);
//            }, cancellationToken);

//            //  Возврат отправителя данных.
//            return sender;
//        }
//        catch
//        {
//            //  Разрушение объекта.
//            await sender.DisposeAsync().ConfigureAwait(false);

//            //  Повторный выброс исключения.
//            throw;
//        }
//    }

//    /// <summary>
//    /// Асинхронно выполняет основную работу.
//    /// </summary>
//    /// <param name="delay">
//    /// Максимальная задержка при отправке данных.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая основную работу.
//    /// </returns>
//    private async Task InvokeAsync(TimeSpan delay, CancellationToken cancellationToken)
//    {
//        //  Блок с гарантированным разрушением.
//        await using (this)
//        {
//            //  Получение очереди на отправку.
//            ConcurrentQueue<BlockInfo> queue = Volatile.Read(ref _Queue) ?? throw new ObjectDisposedException(nameof(StreamSender));

//            //  Основной цикл отправки данных.
//            while (!cancellationToken.IsCancellationRequested)
//            {
//                //  Извлечение элементов из очереди.
//                while (
//                    queue.TryDequeue(out BlockInfo? blockInfo) &&
//                    !cancellationToken.IsCancellationRequested)
//                {
//                    //  Проверка ссылки.
//                    if (blockInfo is not null)
//                    {
//                        //  Блок перехвата всех исключений.
//                        try
//                        {
//                            //  Извлечение блока памяти.
//                            Block? block = blockInfo.TryGetBlock();

//                            //  Проверка ссылки на память.
//                            if (block is not null)
//                            {
//                                //  Выполнение работы с потоком.
//                                await _Connection.InvokeAsync(
//                                    async delegate (SslStream stream, CancellationToken cancellationToken)
//                                    {
//                                        //  Запись данных в поток.
//                                        await block.SaveAsync(stream, cancellationToken).ConfigureAwait(false);
//                                    }, cancellationToken).ConfigureAwait(false);

//                                //  Установка результата.
//                                blockInfo.CompletionSource.TrySetResult();
//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            //  Проверка токена отмены.
//                            if (cancellationToken.IsCancellationRequested)
//                            {
//                                //  Установка исключения.
//                                blockInfo.CompletionSource.TrySetException(new ObjectDisposedException(nameof(StreamSender)));
//                            }
//                            else
//                            {
//                                //  Установка исключения.
//                                blockInfo.CompletionSource.TrySetException(ex);
//                            }

//                            //  Повторный выброс исключения.
//                            throw;
//                        }
//                    }
//                }

//                //  Ожидание перед следующим проходом.
//                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
//            }
//        }
//    }

//    /// <summary>
//    /// Представляет информацию о блоке памяти.
//    /// </summary>
//    /// <param name="block">
//    /// Блок памяти.
//    /// </param>
//    private sealed class BlockInfo(Block block)
//    {
//        /// <summary>
//        /// Поле для хранения области памяти.
//        /// </summary>
//        private Block? _Memory = block;

//        /// <summary>
//        /// Возвращает источник завершения задачи ожидания.
//        /// </summary>
//        public TaskCompletionSource CompletionSource { get; } = new();

//        /// <summary>
//        /// Выполняет попытку получить блок данных.
//        /// </summary>
//        /// <returns>
//        /// Ссылка на текущее значение.
//        /// </returns>
//        public Block? TryGetBlock()
//        {
//            //  Чтение поля.
//            return Interlocked.Exchange(ref _Memory, null);
//        }
//    }

//    /// <summary>
//    /// Разрушает очередь на отправку.
//    /// </summary>
//    /// <param name="queue">
//    /// Очередь на отправку.
//    /// </param>
//    private static void DisposeQueue(ConcurrentQueue<BlockInfo> queue)
//    {
//        //  Блок перехвата всех исключений.
//        try
//        {
//            //  Извлечение элементов из очереди.
//            while (queue.TryDequeue(out BlockInfo? blockInfo))
//            {
//                //  Блок перехвата всех исключений.
//                try
//                {
//                    //  Установка исключения.
//                    blockInfo?.CompletionSource.TrySetException(new ObjectDisposedException(nameof(StreamSender)));
//                }
//                catch { }
//            }
//        }
//        catch { }
//    }
//}
