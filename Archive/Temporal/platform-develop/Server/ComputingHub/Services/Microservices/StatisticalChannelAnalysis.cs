using Apeiron.Analysis.Transforms;
using Apeiron.Frames;
using Apeiron.Platform.Databases.CentralDatabase;
using Apeiron.Platform.Databases.CentralDatabase.Entities;

namespace Apeiron.Platform.Server.Services.Microservices;

/// <summary>
/// Представляет микрослужбу, выполняющую статистический анализ каналов.
/// </summary>
public sealed class StatisticalChannelAnalysis :
    ServerMicroservice<StatisticalChannelAnalysis>
{
    /// <summary>
    /// Поле для хранения списка идентификаторов кадров регистрации.
    /// </summary>
    private readonly List<long> _FrameIds;

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
    public StatisticalChannelAnalysis(ILogger<StatisticalChannelAnalysis> logger) :
        base(logger)
    {
        //  Создание списка идентификаторов кадров регистрации.
        _FrameIds = new();

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

        //  Запрос данных для обработки.
        //      


        //  Получение идентификаторов фильтров.
        long[] filterIds = await CentralDatabaseAgent.RequestAsync(
            async (session, cancellationToken) => await session.FilterInfos
                .Select(filter => filter.Id)
                .ToArrayAsync(cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Получение списка кадров.
        _FrameIds.AddRange(await CentralDatabaseAgent.RequestAsync(
            async (session, cancellationToken) => await session.FrameInfos
                .Select(frame => frame.Id)
                .ToArrayAsync(cancellationToken).ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false));


        List<Task> tasks = new();

        while (_FrameIds.Count > 0)
        {
            //  Определение индекса следующего кадра регистрации.
            int index = _Random.Next() % _FrameIds.Count;

            //  Получение идентификатора кадра регистрации.
            long frameId = _FrameIds[index];

            //  Удаление кадра регистрации из списка.
            _FrameIds.RemoveAt(index);

            //  Загрузка кадра.
            FrameInfo? frameInfo = await CentralDatabaseAgent.RequestAsync(
                async (session, cancellationToken) => await session.FrameInfos
                    .FirstOrDefaultAsync(
                        frameInfo => frameInfo.Id == frameId,
                        cancellationToken)
                    .ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);

            //  Проверка ссылки на кадр.
            if (frameInfo is not null)
            {
                tasks.Add(Task.Run(async () =>
                {
                    //  Проверка метрики файла.
                    if (frameInfo.File.Metrics.Count == 0)
                    {
                        //  Обновление метрики файла.
                        await CentralDatabaseAgent.FileSystem.UpdateFileMetricAsync(
                            frameInfo.File, cancellationToken).ConfigureAwait(false);
                    }

                    //  Получение идентификаторов метрик файла.
                    long[] metricIds = await CentralDatabaseAgent.RequestAsync(
                        async (session, cancellationToken) => await session.InternalFileMetrics
                            .Where(metric => metric.FileId == frameInfo.FileId)
                            .Select(metric => metric.Id)
                            .ToArrayAsync(cancellationToken).ConfigureAwait(false),
                        cancellationToken).ConfigureAwait(false);

                    //  Проверка количества каналов.
                    if (frameInfo.Channels.Count == 0)
                    {
                        //  Загрузка каналов.
                        await CentralDatabaseAgent.Recording.LoadChannelsAsync(frameInfo, cancellationToken).ConfigureAwait(false);
                    }

                    //  Перебор всех фильтров.
                    foreach (long filterId in filterIds)
                    {
                        //  Загрузка фильтра.
                        FilterInfo? filter = await CentralDatabaseAgent.RequestAsync(
                            async (session, cancellationToken) => await session.FilterInfos
                                .FirstOrDefaultAsync(
                                    filter => filter.Id == filterId,
                                    cancellationToken)
                                .ConfigureAwait(false),
                            cancellationToken).ConfigureAwait(false);

                        //  Проверка фильтра.
                        if (filter is null)
                        {
                            //  Переход к следующему фильтру.
                            continue;
                        }

                        //  Получение преобразования каналов.
                        Transform transform = await CentralDatabaseAgent.Recording.GetTransformAsync(
                            filter, cancellationToken).ConfigureAwait(false);

                        //  Перебор всех метрик.
                        foreach (long metricId in metricIds)
                        {
                            //  Перезагрузка кадра.
                            frameInfo = await CentralDatabaseAgent.RequestAsync(
                                async (session, cancellationToken) => await session.FrameInfos
                                    .FirstOrDefaultAsync(
                                        frameInfo => frameInfo.Id == frameInfo.Id,
                                        cancellationToken)
                                    .ConfigureAwait(false),
                                cancellationToken).ConfigureAwait(false);

                            //  Проверка необходимости обработки.
                            if (frameInfo is not null)
                            //if (frameInfo is not null &&
                            //    frameInfo.Channels.Any(
                            //        channel => channel.Statistics.All(statistic => statistic.FilterId != filterId) &&
                            //        channel.Statistics.All(statistic => statistic.FileMetricId != metricId)))
                            {
                                //  Загрузка канала.
                                Frame frame = await CentralDatabaseAgent.Recording.LoadFrameAsync(
                                    frameInfo, cancellationToken).ConfigureAwait(false);

                                try
                                {

                                    //  Выполнение транзакции.
                                    await CentralDatabaseAgent.TransactionAsync(
                                        async (session, cancellationToken) =>
                                        {
                                        //  Проверка токена отмены.
                                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                        //  Перебор каналов.
                                        foreach (Channel channel in frame.Channels)
                                            {
                                            //  Полчение информации о канале.
                                            ChannelInfo channelInfo = frameInfo.Channels
                                                    .First(info => info.Name.Name == channel.Name);

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
                                                ChannelStatistic statistic = new(channelInfo.Id, filterId, metricId)
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
                }, cancellationToken));
            }

            //  Ввод информации в журнал.
            Logger.LogInformation(
                "Кадров: {count}, обработан кадр {id}",
                _FrameIds.Count, frameId);
        }

        await Task.WhenAll(tasks).ConfigureAwait(false);

        ////  Проверка списка идентификаторов кадров регистрации.
        //if (_FrameIds.Count > 0)
        //{

        //}
        //else
        //{
        //    //  Получение идентификаторов кадров регистрации,
        //    //  не содержащих каналы.
        //    _FrameIds.AddRange(await Agent.RequestAsync(
        //        async (session, cancellationToken) => await session.FrameInfos
        //            .Where(frame => frame.Channels.Count == 0)
        //            .Select(frame => frame.Id)
        //            .ToArrayAsync(cancellationToken).ConfigureAwait(false),
        //        cancellationToken).ConfigureAwait(false));

        //    //  Получение идентификаторов кадров регистрации,
        //    //  не содержащих статистических данных.
        //    _FrameIds.AddRange(await Agent.RequestAsync(
        //        async (session, cancellationToken) => await session.FrameInfos
        //            .Where(frame => frame.Channels.Count != 0 &&
        //                frame.Channels.Any(channel => channel.Statistics.Count == 0))
        //            .Select(frame => frame.Id)
        //            .ToArrayAsync(cancellationToken).ConfigureAwait(false),
        //        cancellationToken).ConfigureAwait(false));

        //    //  Перебор всех фильтров.
        //    foreach (long filterId in filterIds)
        //    {
        //        //  Получение идентификаторов кадров регистрации,
        //        //  с необработанными каналами с заданным фильтром.
        //        _FrameIds.AddRange(await Agent.RequestAsync(
        //            async (session, cancellationToken) => await session.FrameInfos
        //                .Where(frame => frame.Channels.Count != 0 &&
        //                    frame.Channels.Any(channel => channel.Statistics.Count != 0))
        //                .SelectMany(
        //                    frame => frame.File.Metrics,
        //                    (frame, metric) => new
        //                    {
        //                        Frame = frame,
        //                        MetricId = metric.Id,
        //                    })
        //                .SelectMany(
        //                    entity => entity.Frame.Channels,
        //                    (entity, channel) => new
        //                    {
        //                        entity.Frame,
        //                        entity.MetricId,
        //                        Channel = channel,
        //                    })
        //                .Where(entity => entity.Channel.Statistics.All(statistic => statistic.FilterId != filterId) &&
        //                    entity.Channel.Statistics.All(statistic => statistic.FileMetricId != entity.MetricId))
        //                .Select(entity => entity.Frame.Id)
        //                .Distinct()
        //                .ToArrayAsync(cancellationToken).ConfigureAwait(false),
        //            cancellationToken).ConfigureAwait(false));
        //    }
        //}
    }
}
