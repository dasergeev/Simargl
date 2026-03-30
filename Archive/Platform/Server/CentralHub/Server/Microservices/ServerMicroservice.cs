using Simargl.Platform.Microservices;

namespace Apeiron.Platform.Server.Microservices;

/// <summary>
/// Представляет серверную микрослужбу.
/// </summary>
/// <typeparam name="TMicroservice">
/// Тип микрослужбы.
/// </typeparam>
public abstract class ServerMicroservice<TMicroservice> :
    Microservice<TMicroservice>
    where TMicroservice : ServerMicroservice<TMicroservice>
{
    /// <summary>
    /// Постоянная, определяющая задержку в милисекундах запуска службы
    /// для инициализации консоли и выдачи служебных сообщений.
    /// </summary>
    private const int _InitializationDelay = 1000;

    /// <summary>
    /// Поле для хранения времени задержки перед выполнением следующего шага в миллисекундах.
    /// </summary>
    private volatile int _NextStepDelay = 10;

    /// <summary>
    /// Поле для хранения идентификатора информации о микрослужбе в базе данных.
    /// </summary>
    private long _MicroserviceInfoId = 0;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала микрослужбы.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    protected ServerMicroservice(ILogger<TMicroservice> logger) :
        base(logger)
    {

    }

    /// <summary>
    /// Асинхронно выполняет запуск микрослужбы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая запуск микрослужбы.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        ////  Проверка токена отмены.
        //await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        ////  Получение информации о микрослужбе из базы данных.
        //MicroserviceInfo? microserviceInfo = await CentralDatabaseAgent.RequestAsync(
        //    async (context, cancellationToken) => await context.MicroserviceInfos
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(microserviceInfo => microserviceInfo.Name == Name, cancellationToken)
        //        .ConfigureAwait(false),
        //    cancellationToken).ConfigureAwait(false);

        ////  Проверка информации о микрослужбе.
        //if (microserviceInfo is null)
        //{
        //    //  Служба не может работать без информации из базы данных.
        //    throw new InvalidOperationException($"Не удалось найти информацию о службе \"{Name}\" в базе данных.");
        //}

        ////  Проверка полного имени службы.
        //if (microserviceInfo.FullName != FullName)
        //{
        //    //  Обновление информации о полном имени службы.
        //    microserviceInfo.FullName = FullName;

        //    //  Выполнение транзакции.
        //    await CentralDatabaseAgent.TransactionAsync(
        //        async (context, cancellationToken) =>
        //        {
        //            //  Проверка токена отмены.
        //            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //            //  Обновление записи в таблице.
        //            context.MicroserviceInfos.Update(microserviceInfo);
        //        },
        //        cancellationToken).ConfigureAwait(false);
        //}

        ////  Установка времени задержки перед выполнением следующего шага в миллисекундах.
        //_NextStepDelay = microserviceInfo.NextStepDelay;

        ////  Установка идентификатора информации о микрослужбе в базе данных.
        //_MicroserviceInfoId = microserviceInfo.Id;

        //  Выполнение базовой задачи.
        await base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Асинхронно выполняет основную фоновую работу микрослужбы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную фоновую работу микрослужбы.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override sealed async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        // Задержка для инициализации консоли и выдачи служебных сообщений.
        await Task.Delay(_InitializationDelay, cancellationToken).ConfigureAwait(false);

        //  Основной цикл работы микрослужбы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата исключений для вывода в журнал.
            try
            {
                //  Выполнение шага работы микрослужбы.
                await MakeStepAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //  Проверка остановки службы.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Вывод информации в журнал.
                    Logger.LogError("{exception}", ex);
                }
            }

            ////  Обновление времени задержки перед выполнением следующего шага.
            //await DefyCriticalAsync(UpdateNextStepDelayAsync, cancellationToken).ConfigureAwait(false);

            //  Задержка перед выполнением следующего шага.
            await Task.Delay(_NextStepDelay, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет шаг работы микрослужбы.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая шаг работы микрослужбы.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected abstract ValueTask MakeStepAsync(CancellationToken cancellationToken);

    ///// <summary>
    ///// Асинхронно возвращает информацию о микрослужбе.
    ///// </summary>
    ///// <param name="cancellationToken">
    ///// Токен отмены.
    ///// </param>
    ///// <returns>
    ///// Задача, возвращающую информацию о микрослужбе.
    ///// </returns>
    ///// <exception cref="OperationCanceledException">
    ///// Операция отменена.
    ///// </exception>
    //protected async Task<MicroserviceInfo> GetMicroserviceInfoAsync(CancellationToken cancellationToken)
    //{
    //    //  Проверка токена отмены.
    //    await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

    //    //  Запрос информации о микрослужбе.
    //    return await CentralDatabaseAgent.RequestAsync(
    //        async (context, cancellationToken) => await context.MicroserviceInfos
    //            .AsNoTracking()
    //            .FirstAsync(microserviceInfo => microserviceInfo.Id == _MicroserviceInfoId, cancellationToken)
    //            .ConfigureAwait(false),
    //        cancellationToken).ConfigureAwait(false);
    //}

    ///// <summary>
    ///// Асинхронно обновляет время задержки перед выполнением следующего шага в миллисекундах.
    ///// </summary>
    ///// <param name="cancellationToken">
    ///// Токен отмены.
    ///// </param>
    ///// <returns>
    ///// Задача, выполняющая обноеление.
    ///// </returns>
    ///// <exception cref="OperationCanceledException">
    ///// Операция отменена.
    ///// </exception>
    //private async Task UpdateNextStepDelayAsync(CancellationToken cancellationToken)
    //{
    //    //  Проверка токена отмены.
    //    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

    //    //  Получение времени задержки из базы данных.
    //    int nextStepDelay = await CentralDatabaseAgent.RequestAsync(
    //        async (context, cancellationToken) => await context.MicroserviceInfos
    //            .AsNoTracking()
    //            .Where(microserviceInfo => microserviceInfo.Id == _MicroserviceInfoId)
    //            .Select(microserviceInfo => microserviceInfo.NextStepDelay)
    //            .FirstOrDefaultAsync(cancellationToken)
    //            .ConfigureAwait(false),
    //        cancellationToken).ConfigureAwait(false);

    //    //  Проверка времени задержки.
    //    if (nextStepDelay > 0)
    //    {
    //        //  Обновление времени задержки перед выполнением следующего шага в миллисекундах.
    //        _NextStepDelay = nextStepDelay;
    //    }
    //}
}
