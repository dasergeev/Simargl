using Apeiron.Analysis.Transforms;
using Apeiron.Frames;
using Apeiron.Platform.Databases.CentralDatabase;
using Apeiron.Platform.Databases.CentralDatabase.Entities;

namespace Apeiron.Platform.Server.Services.Microservices;

/// <summary>
/// Представляет микрослужбу, выполняющую анализ кадров.
/// </summary>
public sealed class FrameAnalysis :
    ServerMicroservice<FrameAnalysis>
{
    /// <summary>
    /// Поле для хранения генератора случайных чисел.
    /// </summary>
    private readonly Random _Random;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public FrameAnalysis(ILogger<FrameAnalysis> logger) :
        base(logger)
    {
        //  Создание генератора случайных чисел.
        _Random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));
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
    protected override sealed async ValueTask MakeStepAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос идентификаторов кадров регистрации.
        long[] frameIds = await GetFrameIdsAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка количества кадров.
        if (frameIds.Length == 0)
        {
            //  Во всех кадрах загружены каналы.
            return;
        }

        //  Изменение порядка списка кадров.
        for (int i = 0; i < frameIds.Length; i++)
        {
            //  Получение нового индекса.
            int index = _Random.Next() % frameIds.Length;

            //  Перестановка элементов.
            (frameIds[i], frameIds[index]) = (frameIds[index], frameIds[i]);
        }

        //  Загрузка фильтров.
        FilterInfo[] filters = await CentralDatabaseAgent.RequestAsync(
            async (session, cancellationToken) => await session.FilterInfos
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Создание массива преобразований.
        Transform[] transforms = new Transform[filters.Length];

        //  Создание преобразований.
        for (int i = 0; i < transforms.Length; i++)
        {
            //  Создание преобразования.
            transforms[i] = await CentralDatabaseAgent.Recording.GetTransformAsync(
                filters[i], cancellationToken).ConfigureAwait(false);
        }

        //  Аснихронная работа с кадрами.
        await Parallel.ForEachAsync(
            frameIds,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken,
            },
            async (frameId, cancellationToken) =>
            {
                //  Безопасное выполнение действия.
                await SafeCallAsync(async cancellationToken =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Запрос информации о кадре.
                    FrameInfo? frameInfo = await CentralDatabaseAgent.RequestAsync(
                        async (session, cancellationToken) => await session.FrameInfos
                            .FirstOrDefaultAsync(frameInfo => frameInfo.Id == frameId, cancellationToken)
                            .ConfigureAwait(false),
                        cancellationToken).ConfigureAwait(false);

                    //  Проверка ссылки на информацию о кадре.
                    if (frameInfo is null)
                    {
                        //  Завершение работы с идентификатором.
                        return;
                    }

                    //  Загрузка кадра.
                    Frame frame = await CentralDatabaseAgent.Recording.LoadFrameAsync(
                        frameInfo, cancellationToken).ConfigureAwait(false);

                    //  Список информации о каналах.
                    List<ChannelInfo> channelInfos = new();

                    //  Проверка количества каналов.
                    if (frameInfo.Channels.Count == 0)
                    {
                        //  Загрузка каналов.
                        await CentralDatabaseAgent.TransactionAsync(
                            async (session, cancellationToken) =>
                            {
                                //  Проверка токена отмены.
                                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                //  Перебор каналов.
                                for (int i = 0; i < frame.Channels.Count; i++)
                                {
                                    //  Получение канала.
                                    Channel channel = frame.Channels[i];

                                    //  Создание информации о канале.
                                    ChannelInfo channelInfo = new(
                                        frameInfo.Id,
                                        (await CentralDatabaseAgent.Recording.ChannelNameRegistrationAsync(
                                            channel.Name, cancellationToken).ConfigureAwait(false)).Id,
                                        (await CentralDatabaseAgent.Recording.ChannelUnitRegistrationAsync(
                                            channel.Unit, cancellationToken).ConfigureAwait(false)).Id
                                        )
                                    {
                                        Index = i,
                                        Sampling = channel.Sampling,
                                        Cutoff = channel.Cutoff
                                    };

                                    //  Добавление информации о канале.
                                    await session.ChannelInfos.AddAsync(
                                        channelInfo, cancellationToken).ConfigureAwait(false);
                                }

                                //  Запись количества каналов в информацию о кадре.
                                (await session.FrameInfos
                                    .FirstAsync(
                                        frameInfo => frameInfo.Id == frameId,
                                        cancellationToken)
                                    .ConfigureAwait(false)).ChannelsCount = frame.Channels.Count;
                            },
                            cancellationToken).ConfigureAwait(false);
                    }
                    else
                    {
                        //  Загрузка информации о каналах.
                        channelInfos.AddRange(frameInfo.Channels);

                        //  Проверка количества каналов в информации о кадре.
                        if (frameInfo.ChannelsCount != frameInfo.Channels.Count)
                        {
                            //  Загрузка каналов.
                            await CentralDatabaseAgent.TransactionAsync(
                                async (session, cancellationToken) =>
                                {
                                    //  Проверка токена отмены.
                                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                    //  Запись количества каналов в информацию о кадре.
                                    (await session.FrameInfos
                                        .FirstAsync(
                                            frameInfo => frameInfo.Id == frameId,
                                            cancellationToken)
                                        .ConfigureAwait(false)).ChannelsCount = frame.Channels.Count;
                                },
                                cancellationToken).ConfigureAwait(false);
                        }
                    }

                    //  Статистический анализ кадра.
                    await StatisticsAnalysisAsync(
                        filters, transforms,
                        frameInfo, channelInfos, frame,
                        cancellationToken).ConfigureAwait(false);

                    //  Анализ рапределений кадра.
                    await DistributionsAnalysisAsync(
                        filters, transforms,
                        frameInfo, channelInfos, frame,
                        cancellationToken).ConfigureAwait(false);

                    //  Вывод информации в журнал.
                    Logger.LogInformation(
                        "Обработан кадр {frameId}",
                        frameId);

                }, cancellationToken).ConfigureAwait(false);
            }).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно загружает идентификаторы кадров регистрации, которые необходимо обработать.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая загрузку идентификаторов.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private static async Task<long[]> GetFrameIdsAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Коллекция идентификаторов.
        List<long> frameIds = new();

        //  Запрос кадров, у которых не загружены каналы.
        frameIds.AddRange(await CentralDatabaseAgent.RequestAsync(
            async (session, cancellationToken) => await session.FrameInfos
                .Where(frameInfo => frameInfo.ChannelsCount == 0)
                .Select(frameInfo => frameInfo.Id)
                .ToArrayAsync(cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false));

        //  Получение количества фильтров.
        int filterCount = CentralDatabaseAgent.Request(session => session.FilterInfos.Count());

        //  Запрос кадров, у которых статистические данные расчитаны не для всех фильтров.
        frameIds.AddRange(await CentralDatabaseAgent.RequestAsync(
            async (session, cancellationToken) => await session.ChannelInfos
                .Where(channelInfo => channelInfo.StatisticsCount != filterCount)
                .Select(channelInfo => channelInfo.FrameId)
                .Distinct()
                .ToArrayAsync(cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false));

        //  Запрос кадров, у которых распределения получены не для всех фильтров.
        frameIds.AddRange(await CentralDatabaseAgent.RequestAsync(
            async (session, cancellationToken) => await session.ChannelInfos
                .Where(channelInfo => channelInfo.DistributionsCount != filterCount)
                .Select(channelInfo => channelInfo.FrameId)
                .Distinct()
                .ToArrayAsync(cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false));

        //  Возврат уникальных идентификаторов.
        return frameIds.Distinct().ToArray();
    }

    /// <summary>
    /// Асинхронно выполняет статистический анализ кадра.
    /// </summary>
    /// <param name="filters">
    /// Массив фильтров.
    /// </param>
    /// <param name="transforms">
    /// Массив преобразований канала.
    /// </param>
    /// <param name="frameInfo">
    /// Информация о кадре.
    /// </param>
    /// <param name="channelInfos">
    /// Массив информации о каналах.
    /// </param>
    /// <param name="frame">
    /// Кадр.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая статистический анализ.
    /// </returns>
    private static async Task StatisticsAnalysisAsync(
        FilterInfo[] filters, Transform[] transforms,
        FrameInfo frameInfo, List<ChannelInfo> channelInfos, Frame frame,
        CancellationToken cancellationToken)
    {
        //  Получение идентификаторов метрик файла.
        long[] metricIds = await CentralDatabaseAgent.RequestAsync(
            async (session, cancellationToken) => await session.InternalFileMetrics
                .Where(metric => metric.FileId == frameInfo.FileId)
                .Select(metric => metric.Id)
                .ToArrayAsync(cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Перебор всех фильтров.
        for (int i = 0; i < filters.Length; i++)
        {
            //  Получение фильтра.
            FilterInfo filter = filters[i];

            //  Получение преобразования каналов.
            Transform transform = transforms[i];

            //  Перебор всех метрик.
            foreach (long metricId in metricIds)
            {
                //  Проверка необходимости обработки.
                if (frameInfo.Channels.Any(
                        channel => channel.Statistics.All(statistic => statistic.FilterId != filter.Id) &&
                        channel.Statistics.All(statistic => statistic.FileMetricId != metricId)))
                {
                    try
                    {
                        //  Выполнение транзакции.
                        await CentralDatabaseAgent.TransactionAsync(
                            async (session, cancellationToken) =>
                            {
                                //  Проверка токена отмены.
                                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                //  Перебор каналов.
                                for (int j = 0; j < frame.Channels.Count; j++)
                                {
                                    //  Получение копии канала.
                                    Channel channel = frame.Channels[j].Clone();

                                    //  Получение информации о канале.
                                    ChannelInfo channelInfo = channelInfos[j];

                                    //  Фильтрация канала.
                                    transform.Invoke(channel, channel);

                                    //  Минимальное значени.
                                    double min = double.MaxValue;

                                    //  Максимальное значение.
                                    double max = double.MinValue;

                                    //  Сумма значений.
                                    double sum = 0;

                                    //  Сумма квадратов.
                                    double squaresSum = 0;

                                    //  Минимальное значение по модулю.
                                    double minModulo = double.MaxValue;

                                    //  Максимальное значение по модулю.
                                    double maxModulo = double.MinValue;

                                    //  Сумма значений по модулю.
                                    double sumModulo = 0;

                                    //  Перебор значений канала.
                                    foreach (double value in channel)
                                    {
                                        //  Проверка минимального значения.
                                        if (min > value)
                                        {
                                            //  Установка минимального значения.
                                            min = value;
                                        }

                                        //  Проверка максимального значения.
                                        if (max < value)
                                        {
                                            //  Установка максимального значения.
                                            max = value;
                                        }

                                        //  Корректировка суммы значений.
                                        sum += value;

                                        //  Корректировка суммы квадратов.
                                        squaresSum += value * value;

                                        //  Получение значения по модулю.
                                        double valueModulo = Math.Abs(value);

                                        //  Проверка минимального значения по модулю.
                                        if (minModulo > valueModulo)
                                        {
                                            //  Установка минимального значения по модулю.
                                            minModulo = valueModulo;
                                        }

                                        //  Проверка максимального значения по модулю.
                                        if (maxModulo < valueModulo)
                                        {
                                            //  Установка максимального значения по модулю.
                                            maxModulo = valueModulo;
                                        }

                                        //  Корректировка суммы значений по модулю.
                                        sumModulo += valueModulo;
                                    }

                                    //  Определение длины канала.
                                    int length = channel.Length;

                                    //  Создание статистических данных.
                                    ChannelStatistic statistic = new(channelInfo.Id, filter.Id, metricId)
                                    {
                                        Count = length,
                                        Min = min,
                                        Max = max,
                                        Average = sum / length,
                                        Deviation = Math.Sqrt((squaresSum - sum * sum / length) / (length - 1)),
                                        Sum = sum,
                                        SquaresSum = squaresSum,
                                        MinModulo = minModulo,
                                        MaxModulo = maxModulo,
                                        AverageModulo = sumModulo / length,
                                        DeviationModulo = Math.Sqrt((squaresSum - sumModulo * sumModulo / length) / (length - 1)),
                                        SumModulo = sumModulo,
                                    };

                                    //  Добавление статистических данных.
                                    await session.ChannelStatistics.AddAsync(statistic, cancellationToken).ConfigureAwait(false);
                                }
                            },
                            cancellationToken).ConfigureAwait(false);
                    }
                    catch (DbUpdateException ex)
                    {
                        _ = ex;
                    }
                }
            }
        }

        //  Обновление количества статистических данных каналов.
        foreach (ChannelInfo channelInfo in channelInfos)
        {
            //  Выполнение транзакции.
            await CentralDatabaseAgent.TransactionAsync(
                async (session, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Обновление количества.
                    session.ChannelInfos.First(info => info.Id == channelInfo.Id).StatisticsCount
                        = session.ChannelStatistics.Count(statistics => statistics.ChannelId == channelInfo.Id);
                },
                cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет анализ распределений кадра.
    /// </summary>
    /// <param name="filters">
    /// Массив фильтров.
    /// </param>
    /// <param name="transforms">
    /// Массив преобразований канала.
    /// </param>
    /// <param name="frameInfo">
    /// Информация о кадре.
    /// </param>
    /// <param name="channelInfos">
    /// Массив информации о каналах.
    /// </param>
    /// <param name="frame">
    /// Кадр.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая анализ распределений.
    /// </returns>
    private static async Task DistributionsAnalysisAsync(
        FilterInfo[] filters, Transform[] transforms,
        FrameInfo frameInfo, List<ChannelInfo> channelInfos, Frame frame,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка количества распределений
        if (channelInfos.All(channelInfo => channelInfo.DistributionsCount == filters.Length))
        {
            //  Кадр не требует анализа распределений.
            return;
        }

        //  Получение идентификаторов метрик файла.
        long[] metricIds = await CentralDatabaseAgent.RequestAsync(
            async (session, cancellationToken) => await session.InternalFileMetrics
                .Where(metric => metric.FileId == frameInfo.FileId)
                .Select(metric => metric.Id)
                .ToArrayAsync(cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Перебор всех фильтров.
        for (int i = 0; i < filters.Length; i++)
        {
            //  Получение фильтра.
            FilterInfo filter = filters[i];

            //  Получение преобразования каналов.
            Transform transform = transforms[i];

            //  Перебор всех метрик.
            foreach (long metricId in metricIds)
            {
                //  Проверка необходимости обработки.
                if (frameInfo.Channels.Any(
                        channel => channel.Distributions.All(distribution => distribution.FilterId != filter.Id) &&
                        channel.Distributions.All(distribution => distribution.FileMetricId != metricId)))
                {
                    try
                    {
                        //  Выполнение транзакции.
                        await CentralDatabaseAgent.TransactionAsync(
                            async (session, cancellationToken) =>
                            {
                                //  Проверка токена отмены.
                                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                //  Перебор каналов.
                                for (int j = 0; j < frame.Channels.Count; j++)
                                {
                                    //  Получение копии канала.
                                    Channel channel = frame.Channels[j].Clone();

                                    //  Получение информации о канале.
                                    ChannelInfo channelInfo = channelInfos[j];

                                    //  Фильтрация канала.
                                    transform.Invoke(channel, channel);

                                    //  Создание словаря распределений.
                                    SortedDictionary<double, long> map = new();

                                    //  Перебор всех значений канала.
                                    foreach (double value in channel)
                                    {
                                        //  Расчёт ключа.
                                        double key = Math.Round(value);

                                        //  Проверка ключа.
                                        if (map.TryGetValue(key, out long actualValue))
                                        {
                                            //  Увеличение счётчика.
                                            map[key] = actualValue + 1;
                                        }
                                        else
                                        {
                                            //  Добавление счётчика.
                                            map.Add(key, 1);
                                        }
                                    }

                                    //  Создание распределения.
                                    ChannelDistribution distribution = new(channelInfo.Id, filter.Id, metricId)
                                    {
                                        Count = map.Count,
                                    };

                                    //  Добавлние распределения в базу.
                                    await session.ChannelDistributions.AddAsync(distribution, cancellationToken)
                                        .ConfigureAwait(false);

                                    //  Создание значений.
                                    foreach (var key in map.Keys)
                                    {
                                        //  Создание значения.
                                        ChannelDistributionValue value = new(distribution.Id)
                                        {
                                            Value = key,
                                            Count = map[key],
                                            Distribution = distribution,
                                        };

                                        //  Добавление значения в базу.
                                        await session.ChannelDistributionValues.AddAsync(value, cancellationToken)
                                            .ConfigureAwait(false);
                                    }
                                }
                            },
                            cancellationToken).ConfigureAwait(false);
                    }
                    catch (DbUpdateException ex)
                    {
                        _ = ex;
                    }
                }
            }
        }
    }
}
