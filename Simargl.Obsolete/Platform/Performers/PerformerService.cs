//using Simargl.Platform.Journals;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using System.Reflection;

//namespace Simargl.Platform.Performers;

///// <summary>
///// Представляет фоновый процесс, выполняющий работу исполнителя.
///// </summary>
///// <typeparam name="TPerformer">
///// Тип исполнителя.
///// </typeparam>
//public sealed class PerformerService<TPerformer> :
//    BackgroundService
//    where TPerformer : Performer
//{
//    /// <summary>
//    /// Постоянная, определяющая задержку в милисекундах запуска службы
//    /// для инициализации консоли и выдачи служебных сообщений.
//    /// </summary>
//    private const int _InitializationDelay = 50;

//    /// <summary>
//    /// Поле для хранения журнал исполнителя.
//    /// </summary>
//    private readonly Journal _Journal;

//    /// <summary>
//    /// Поле для хранения исполнителя.
//    /// </summary>
//    private readonly TPerformer _Performer;

//    /// <summary>
//    /// Поле для хранения таймаута работы службы.
//    /// </summary>
//    private TimeSpan _Timeout;

//    /// <summary>
//    /// Инициализирует новый экземпляр класса.
//    /// </summary>
//    /// <param name="logger">
//    /// Средство записи в журнал службы.
//    /// </param>
//    /// <exception cref="ArgumentNullException">
//    /// В параметре <paramref name="logger"/> передана пустая ссылка.
//    /// </exception>
//    /// <exception cref="NotSupportedException">
//    /// Не найден конструктор исполнителя.
//    /// </exception>
//    public PerformerService(ILogger<PerformerService<TPerformer>> logger)
//    {
//        //  Создание журнала исполнителя.
//        _Journal = Journal.FromLogger(IsNotNull(logger, nameof(logger)));

//        //  Поиск конструктора исполнителя.
//        ConstructorInfo? constructor = typeof(TPerformer).GetConstructor(new Type[] { typeof(Journal) }) ?? throw Exceptions.OperationNotSupported();

//        //  Создание исполнителя.
//        _Performer = (TPerformer)constructor.Invoke(new object?[] { _Journal });

//        //  Установка таймаута работы службы.
//        _Timeout = TimeSpan.FromMinutes(1);
//    }

//    /// <summary>
//    /// Возвращает или задаёт таймаут работы службы.
//    /// </summary>
//    /// <exception cref="ArgumentOutOfRangeException">
//    /// Передан недопустимый таймаут.
//    /// </exception>
//    public TimeSpan Timeout
//    {
//        get => _Timeout;
//        set
//        {
//            //  Проверка таймаута.
//            IsTimeout(value, nameof(Timeout));

//            //  Установка таймаута.
//            _Timeout = value;
//        }
//    }

//    /// <summary>
//    /// Ассинхронно выполняет фоновую работу с поддержкой.
//    /// </summary>
//    /// <param name="cancellationToken">
//    /// Токен отмены.
//    /// </param>
//    /// <returns>
//    /// Задача, выполняющая фоновую работу с поддержкой.
//    /// </returns>
//    /// <exception cref="OperationCanceledException">
//    /// Операция отменена.
//    /// </exception>
//    protected override sealed async Task ExecuteAsync(CancellationToken cancellationToken)
//    {
//        //  Проверка токена отмены.
//        await IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

//        // Задержка для инициализации консоли и выдачи служебных сообщений.
//        await Task.Delay(_InitializationDelay, cancellationToken).ConfigureAwait(false);

//        //  Основной цикл службы.
//        while (!cancellationToken.IsCancellationRequested)
//        {
//            //  Блок перехвата всех некритических исключений.
//            try
//            {
//                //  Выполнение работы исполнителя.
//                await _Performer.PerformAsync(cancellationToken).ConfigureAwait(false);
//            }
//            catch (Exception ex)
//            {
//                //  Проверка критического исключения.
//                if (ex.IsCritical())
//                {
//                    //  Повторный выброс исключения.
//                    throw;
//                }

//                //  Проверка токена отмены.
//                if (!cancellationToken.IsCancellationRequested)
//                {
//                    //  Вывод сообщения об ошибке в журнал.
//                    await _Journal.LogErrorAsync($"{ex}", cancellationToken);
//                }
//            }

//            //  Ожидание перед повторным запуском.
//            await Task.Delay(Timeout, cancellationToken).ConfigureAwait(false);
//        }
//    }
//}
