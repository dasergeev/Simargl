using Apeiron.Platform.Communication.Elements;
using Apeiron.Platform.Communication.Remoting;
using System.Collections.Concurrent;

namespace Apeiron.Platform.Communication;

/// <summary>
/// Представляет диалог.
/// </summary>
public sealed class Dialog :
    Element
{
    /// <summary>
    /// Поле для хранения времени начала диалога.
    /// </summary>
    private long _BeginTimeTicks;

    /// <summary>
    /// Поле для хранения времени окончания диалога.
    /// </summary>
    private long _EndTimeTicks;

    /// <summary>
    /// Поле для хранения интервала отслеживания.
    /// </summary>
    private long _TrackingIntervalTicks;

    /// <summary>
    /// Поле для хранения последнего сообщения.
    /// </summary>
    private Message? _LastMessage;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="communicator">
    /// Коммуникатор с серверным узлом.
    /// </param>
    /// <param name="companion">
    /// Информация о собеседнике.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="communicator"/> передана пустая ссылка.
    /// </exception>
    internal Dialog(Communicator communicator, User companion) :
        base(communicator)
    {
        //  Установка информации о собеседнике.
        Companion = IsNotNull(companion, nameof(companion));

        //  Создание коллекции сообщений.
        Messages = new(Communicator);

        //  Установка интервала отслеживания.
        _TrackingIntervalTicks = TimeSpan.TicksPerDay*15;

        //  Установка последжнего сообщения по умолчанию.
        _LastMessage = null;

        //  Добавление обработчика события изменения коллекции.
        Messages.CollectionChanged += (sender, e) =>
        {
            //  Установка последнего сообщения.
            LastMessage = Messages.LastOrDefault();
        };
    }

    /// <summary>
    /// Возвращает информацию о собеседнике.
    /// </summary>
    public User Companion { get; }

    /// <summary>
    /// Возвращает коллекцию сообщений.
    /// </summary>
    public MessageCollection Messages { get; }

    /// <summary>
    /// Возвращает последнее сообщение диалога.
    /// </summary>
    public Message? LastMessage 
    {
        get => _LastMessage;
        private set
        {
            //  Проверка изменения значения.
            if (!ReferenceEquals(_LastMessage, value))
            {
                //  Установка нового значения.
                _LastMessage = value;

                //  Вызов события об изменении значения свойства.
                OnPropertyChanged(new(nameof(LastMessage)));
            }
        }
    }

    /// <summary>
    /// Возвращает время начала диалога.
    /// </summary>
    public DateTime BeginTime
    {
        get => new(Interlocked.Read(ref _BeginTimeTicks));
        private set
        {
            //  Получение интервалов.
            long ticks = value.Ticks;

            //  Выполнение в основном потоке.
            PrimaryInvoker.Invoke(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_BeginTimeTicks != ticks)
                {
                    //  Установка нового значения.
                    _BeginTimeTicks = ticks;

                    //  Вызов события об изменении значения свойства.
                    OnPropertyChanged(new(nameof(BeginTime)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает время окончания диалога.
    /// </summary>
    public DateTime EndTime
    {
        get => new(Interlocked.Read(ref _EndTimeTicks));
        private set
        {
            //  Получение интервалов.
            long ticks = value.Ticks;

            //  Выполнение в основном потоке.
            PrimaryInvoker.Invoke(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_EndTimeTicks != ticks)
                {
                    //  Установка нового значения.
                    _EndTimeTicks = ticks;

                    //  Вызов события об изменении значения свойства.
                    OnPropertyChanged(new(nameof(EndTime)));
                }
            });
        }
    }

    /// <summary>
    /// Возвращает или задаёт интервал отслеживания.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передано нулевое значение.
    /// </exception>
    public TimeSpan TrackingInterval
    {
        get => new(Interlocked.Read(ref _TrackingIntervalTicks));
        set
        {
            //  Проверка значения.
            IsPositive(value, nameof(TrackingInterval));

            //  Получение интервалов.
            long ticks = value.Ticks;

            //  Выполнение в основном потоке.
            PrimaryInvoker.Invoke(delegate
            {
                //  Проверка необходимости изменения значения.
                if (_TrackingIntervalTicks != ticks)
                {
                    //  Установка нового значения.
                    _TrackingIntervalTicks = ticks;

                    //  Вызов события об изменении значения свойства.
                    OnPropertyChanged(new(nameof(TrackingInterval)));
                }
            });
        }
    }

    /// <summary>
    /// Асинхронно обновляет диалог.
    /// </summary>
    /// <param name="options">
    /// Параметры удалённого вызова.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая обновление.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    public async Task UpdateAsync(RemoteInvokeOptions options, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос диапазона диалога.
        (DateTime beginTime, DateTime endTime) = await RemoteInvoker.RequestDialogRangeAsync(
            Communicator.User.ID, Companion.ID,
            options, cancellationToken).ConfigureAwait(false);

        //  Установка времён.
        BeginTime = beginTime;
        EndTime = endTime;

        //  Определение времени обновления.
        beginTime = DateTime.Now - TrackingInterval - TimeSpan.FromHours(1);
        endTime = DateTime.MaxValue;

        //  Запрос идентификаторов сообщений.
        long[] remoteIDs = await RemoteInvoker.RequestIDMessagesAsync(
            Communicator.User.ID, Companion.ID,
            beginTime, endTime,
            options, cancellationToken).ConfigureAwait(false);

        //  Запрос текущих идентификаторов.
        List<long> localIDs = new(Messages.GetAllIDs());

        //  Создане коллекции исключений.
        ConcurrentBag<Exception> exceptions = new();

        //  Асинхронная работа с идентификаторами.
        await Parallel.ForEachAsync(
            remoteIDs,
            cancellationToken,
            async (id, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Проверка загрузки сообщения.
                if (!localIDs.Contains(id))
                {
                    //  Блок перехвата всех некритических исключений.
                    try
                    {
                        //  Загрузка информации о сообщении.
                        (long senderID, string text, DateTime sendTime, DateTime registrationTime) = await RemoteInvoker.RequestMessageAsync(
                            id, options, cancellationToken).ConfigureAwait(false);

                        //  Отправитель и получатель.
                        User sender;
                        User recipient;

                        //  Проверка идентификатора отправителя.
                        if (senderID != Companion.ID)
                        {
                            sender = Communicator.User;
                            recipient = Companion;
                        }
                        else
                        {
                            sender = Companion;
                            recipient = Communicator.User;
                        }

                        //  Создание сообщения.
                        Message message = new(Communicator, id, text, sendTime, registrationTime, sender, recipient);

                        //  Обновление сообщения.
                        Messages.Update(message);


                        
                    }
                    catch (Exception ex)
                    {
                        //  Проверка критического исключения.
                        if (ex.IsCritical())
                        {
                            //  Повторный выброс исключения.
                            throw;
                        }

                        //  Добавление исключания в список.
                        exceptions.Add(ex);
                    }
                }
            }).ConfigureAwait(false);

        //  Проверка исключений.
        if (!exceptions.IsEmpty)
        {
            //  Выброс исключения.
            throw new InvalidOperationException("При обновлении были исключения",
                new AggregateException(exceptions));
        }
    }

    /// <summary>
    /// Асинхронно отправляет сообщение.
    /// </summary>
    /// <param name="text">
    /// Текс сообщения.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая отправку сообщения.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    public async Task SendMessageAsync(string text, CancellationToken cancellationToken)
    {
        //  Отправка сообщения с параметрами по умолчанию.
        await SendMessageAsync(text, default, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно отправляет сообщение.
    /// </summary>
    /// <param name="text">
    /// Текс сообщения.
    /// </param>
    /// <param name="options">
    /// Параметры удалённого вызова.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая отправку сообщения.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Не удалось установить соединение с сервером.
    /// </exception>
    public async Task SendMessageAsync(string text, RemoteInvokeOptions options, CancellationToken cancellationToken)
    {
        //  Проверка ссылки на текст.
        IsNotNull(text, nameof(text));

        //  Определение времени отправки.
        DateTime sendTime = DateTime.Now;

        //  Проверка токена отмены.
        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Блок с гарантированным завершением.
        try
        {
            //  Отправка сообщения.
            (long messageID, DateTime registrationTime) = await RemoteInvoker.SendMessageAsync(
                Companion.ID, text, sendTime, options, cancellationToken).ConfigureAwait(false);

            //  Создание сообщения.
            Message message = new(Communicator, messageID, text, sendTime, registrationTime,
                Communicator.User, Companion);

            //  Добавление сообщения в коллекцию.
            Messages.Add(message);
        }
        finally
        {
            //  Асинхронный запуск задачи.
            _ = Task.Run(async delegate
            {
                //  Обновление диалога.
                await UpdateAsync(options, cancellationToken).ConfigureAwait(false);
            }, cancellationToken).ConfigureAwait(false);
        }
    }
}
