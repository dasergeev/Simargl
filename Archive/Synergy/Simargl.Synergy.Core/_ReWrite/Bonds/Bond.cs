//using Simargl.Synergy.Core.Portions;
//using System.IO;

//namespace Simargl.Synergy.Core.Bonds;

///// <summary>
///// Представляет соединение.
///// </summary>
//internal sealed class Bond :
//    Critical
//{
//    /// <summary>
//    /// Происходит при получении порции данных.
//    /// </summary>
//    public event BondEventHandler? Received;

//    /// <summary>
//    /// Поле для хранения отправителя данных.
//    /// </summary>
//    private readonly StreamSender _Sender;

//    /// <summary>
//    /// Поле для хранения счётчика отправки.
//    /// </summary>
//    private long _SendCounter;

//    /// <summary>
//    /// Инициализирует новый экземпляр.
//    /// </summary>
//    /// <param name="sender">
//    /// Отправитель данных.
//    /// </param>
//    private Bond(StreamSender sender)
//    {
//        //  Установка отправителя данных.
//        _Sender = sender;

//        //  Установка счётчика отправки данных.
//        _SendCounter = 0;

//        //  Добавление метода завершения.
//        AddDestroyer(sender.Dispose);
//    }

//    /// <summary>
//    /// Асинхронно создаёт соединение.
//    /// </summary>
//    /// <param name="stream">
//    /// Поток.
//    /// </param>
//    /// <param name="timeout">
//    /// Тайм-аут.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, создающая соединение.
//    /// </returns>
//    public static async Task<Bond> CreateAsync(
//        Stream stream, TimeSpan timeout,
//        CancellationToken cancellationToken)
//    {
//        //  Оправитель данных.
//        StreamSender? sender = null;

//        //  Соединение.
//        Bond? bond = null;

//        //  Блок перехвата всех исключений.
//        try
//        {
//            //  Тайм-аут в милисекундах.
//            int timeoutValue = checked((int)(timeout.Ticks / TimeSpan.TicksPerMillisecond));

//            //  Проверка тайм-аута.
//            IsPositive(timeoutValue, nameof(timeout));

//            //  Получение максимальной задержки при отправке данных.
//            TimeSpan delay = 0.1 * timeout;
//            if (delay.Ticks < TimeSpan.TicksPerMillisecond) delay = new(TimeSpan.TicksPerMillisecond);

//            //  Создание отправителя.
//            sender = await StreamSender.CreateAsync(
//                stream, delay, cancellationToken).ConfigureAwait(false);

//            //  Создание соединения.
//            bond = new(sender);

//            //  Добавление методов разрушения.
//            bond.AddDestroyer(stream.Close);
//            bond.AddDestroyer(stream.Dispose);

//            //  Запуск асинхронной задачи.
//            _ = Task.Run(async delegate
//            {
//                //  Получение пакетов.
//                await bond.ReceivingAsync(stream, delay, timeout, bond.CancellationToken).ConfigureAwait(false);
//            }, CancellationToken.None);

//            //  Корректировка тайм-аута.
//            timeout *= 0.5;
//            if (timeout.Ticks < TimeSpan.TicksPerMillisecond) timeout = new(TimeSpan.TicksPerMillisecond);

//            //  Запуск асинхронной задачи.
//            _ = Task.Run(async delegate
//            {
//                //  Отправка пакетов.
//                await bond.SendingAsync(timeout, bond.CancellationToken).ConfigureAwait(false);
//            }, CancellationToken.None);

//            //  Возврат соединения.
//            return bond;
//        }
//        catch
//        {
//            //  Блок перехвата всех исключений.
//            try
//            {
//                //  Разрушение отправителя.
//                sender?.Dispose();
//            }
//            catch { }

//            //  Блок перехвата всех исключений.
//            try
//            {
//                //  Разрушение соединения.
//                bond?.Dispose();
//            }
//            catch { }

//            //  Повторный выброс исключения.
//            throw;
//        }
//    }

//    /// <summary>
//    /// Асинхронно отправляет порцию данных.
//    /// </summary>
//    /// <param name="portion">
//    /// Порция данных.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, отправляющая порцию данных.
//    /// </returns>
//    public async Task SendAsync(Portion portion, CancellationToken cancellationToken)
//    {
//        //  Отправка блока данных.
//        await _Sender.SendAsync(portion.Block, cancellationToken).ConfigureAwait(false);

//        //  Увеличение счётчика отправки.
//        Interlocked.Increment(ref _SendCounter);
//    }

//    /// <summary>
//    /// Асинхронно выполняет получение.
//    /// </summary>
//    /// <param name="stream">
//    /// Основной поток.
//    /// </param>
//    /// <param name="delay">
//    /// Задержка между проверками очереди.
//    /// </param>
//    /// <param name="timeout">
//    /// Тайм-аут.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая получение.
//    /// </returns>
//    private async Task ReceivingAsync(Stream stream, TimeSpan delay, TimeSpan timeout, CancellationToken cancellationToken)
//    {
//        //  Блок с гарантированным завершением.
//        using (this)
//        {
//            //  Создание получателя.
//            using StreamReceiver receiver = await StreamReceiver.CreateAsync(
//                stream, cancellationToken).ConfigureAwait(false);

//            //  Время последнего полученного блока.
//            DateTime lastTime = DateTime.Now;

//            //  Основной цикл получения данных.
//            while (!cancellationToken.IsCancellationRequested)
//            {
//                //  Флаг получения данных.
//                bool isRecive = false;

//                //  Извлечение блоков данных из очереди.
//                while (receiver.TryDequeue(out Block? block) &&
//                    !cancellationToken.IsCancellationRequested)
//                {
//                    //  Проверка блока данных.
//                    if (block is not null)
//                    {
//                        //  Блок с гарантированным разрушением.
//                        using (block)
//                        {
//                            //  Проверка пустого блока.
//                            if (block.Size != 0)
//                            {
//                                //  Получение порции.
//                                using Portion portion = Portion.FromBlock(block);

//                                //  Вызов события.
//                                Volatile.Read(ref Received)?.Invoke(this, new(portion));
//                            }
//                            else
//                            {
//                                Console.WriteLine("Получен пустой блок");
//                            }

//                            //  Установка фалага получения данных.
//                            isRecive = true;
//                        }
//                    }
//                }

//                //  Проверка флага получения данных.
//                if (isRecive)
//                {
//                    //  Корректировка времени последнего полученного блока.
//                    lastTime = DateTime.Now;
//                }

//                //  Проверка тайм-аута.
//                ObjectDisposedException.ThrowIf(DateTime.Now - lastTime > timeout, this);

//                //  Ожидание перед следующим проходом.
//                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
//            }
//        }
//    }

//    /// <summary>
//    /// Асинхронно выполняет отправку пустых пакетов.
//    /// </summary>
//    /// <param name="timeout">
//    /// Тайм-аут.
//    /// </param>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая отправку.
//    /// </returns>
//    private async Task SendingAsync(TimeSpan timeout, CancellationToken cancellationToken)
//    {
//        //  Блок с гарантированным разрушением.
//        using (this)
//        {
//            //  Последнее значение счётчика.
//            long lastSendCounter = Interlocked.Read(ref _SendCounter);

//            //  Основной цикл проверки.
//            while (!cancellationToken.IsCancellationRequested)
//            {
//                //  Текущее значение счётчика.
//                long currentSendCounter = Interlocked.Read(ref _SendCounter);

//                //  Проверка изменения счётчика.
//                if (lastSendCounter == currentSendCounter)
//                {
//                    //  Отправка пустого блока.
//                    await _Sender.SendAsync(Block.Empty, cancellationToken).ConfigureAwait(false);

//                    //  Увеличение счётчика отправки.
//                    Interlocked.Increment(ref _SendCounter);

//                    Console.WriteLine("Отправлен пустой блок");
//                }

//                //  Установка последнего значения счётчика.
//                lastSendCounter = Interlocked.Read(ref _SendCounter);

//                //  Ожидание перед следующим проходом.
//                await Task.Delay(timeout, cancellationToken).ConfigureAwait(false);
//            }
//        }
//    }
//}
