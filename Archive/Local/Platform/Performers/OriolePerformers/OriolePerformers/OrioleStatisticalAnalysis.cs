using Apeiron.Analysis.Transforms;

namespace Apeiron.Platform.Performers.OriolePerformers;

/// <summary>
/// Представляет исполнителя, выполняющего статистический анализ.
/// </summary>
public sealed class OrioleStatisticalAnalysis :
    Performer
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="journal">
    /// Журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="journal"/> передана пустая ссылка.
    /// </exception>
    public OrioleStatisticalAnalysis(Journal journal) :
        base(journal)
    {

    }

    /// <summary>
    /// Асинхронно выполняет работу исполнителя.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу исполнителя.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    public override sealed async Task PerformAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание генератора случайных чисел.
        Random random = new(unchecked((int)DateTime.Now.Ticks));

        //  Постоянная, определяющая количество параллельно обрабатываемых кадров.
        const int count = 100;

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Запрос массива идентификаторов необработанных кадров.
            Tuple<int, long>[] allKeys = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database.Frames
                .Where(frame => !frame.IsStatistic)
                .Select(frame => new Tuple<int, long>(frame.RegistrarId, frame.Timestamp))
                .ToArrayAsync(cancellationToken).ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);

            //  Определение количества необработанных кадров.
            int allCount = allKeys.Length;

            //  Проверка количества необработанных кадров.
            if (allCount != 0)
            {
                //  Проверка превышения количества допустимого значения.
                if (allCount > count)
                {
                    //  Создание массива ключей для обработки.
                    Tuple<int, long>[] keys = new Tuple<int, long>[count];

                    //  Заполнение массива ключей для обработки.
                    for (int i = 0; i < count; i++)
                    {
                        //  Установка ключа.
                        keys[i] = allKeys[random.Next() % allCount];
                    }

                    //  Замена массива ключей.
                    allKeys = keys;
                }

                //  Асинхронная обработка кадров.
                await Parallel.ForEachAsync(
                    allKeys,
                    new ParallelOptions()
                    {
                        CancellationToken = cancellationToken,
                    },
                    WorkInFrameAsync).ConfigureAwait(false);
            }
            else
            {
                //  Завершение работы исполнителя.
                return;
            }
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с кадром регистрации.
    /// </summary>
    /// <param name="frameKey">
    /// Ключ кадра регистрации.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с кадром регистрации.
    /// </returns>
    private async ValueTask WorkInFrameAsync(Tuple<int, long> frameKey, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение идентификатора регистратора.
        int registrarId = frameKey.Item1;

        //  Получение метки времени получения данных.
        long timestamp = frameKey.Item2;

        try
        {
            //  Начало транзакции.
            await OrioleDatabaseManager.TransactionAsync(async (database, cancellationToken) =>
            {
                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Блок перехвата исключений для вывода в журнал.
                try
                {
                    //  Загрузка кадра.
                    Frame? dbFrame = await database.Frames
                        .Where(frame => frame.RegistrarId == registrarId && frame.Timestamp == timestamp && !frame.IsStatistic)
                        .Include(frame => frame.Registrar)
                        .ThenInclude(registrar => registrar.Channels)
                        .Include(frame => frame.Registrar)
                        .ThenInclude(registrar => registrar.RecordDirectories)
                        .FirstOrDefaultAsync(cancellationToken)
                        .ConfigureAwait(false);

                    //  Проверка полученного кадра.
                    if (dbFrame is null || dbFrame.IsStatistic)
                    {
                        //  Завершение анализа.
                        return;
                    }

                    //  Установка флага обработки.
                    dbFrame.IsStatistic = true;

                    //  Получение пути к файлу.
                    string path = dbFrame.GetPath();

                    //  Открытие кадра.
                    Frames.Frame frame = new(path);

                    //  Время начала записи в кадр.
                    DateTime beginTime = Frame.FromTimestamp(dbFrame.Timestamp);

                    //  Время окончания записи в кадр.
                    DateTime endTime = beginTime.AddMinutes(1);

                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Определение скорости и местоположения.
                    if (await GetSpeedAsync(registrarId, beginTime, endTime, cancellationToken).ConfigureAwait(false) is not double speed ||
                        await GetLocationAsync(registrarId, beginTime, endTime, cancellationToken).ConfigureAwait(false) is not Location location)
                    {
                        //  Завершение анализа.
                        return;
                    }

                    //  Перебор каналов в кадре.
                    foreach (Channel dbChannel in dbFrame.Registrar.Channels)
                    {
                        //  Получение канала.
                        Frames.Channel channel = frame.Channels[dbChannel.Name];

                        //  Проверка пустых значений.
                        if (channel.Where(value => value == 0).Count() >= 10)
                        {
                            //  Переход к следующему каналу.
                            continue;
                        }

                        //  Определение длины канала.
                        int length = channel.Length;

                        //  Цикл по частотам.
                        for (int cutoffIndex = 0; cutoffIndex < 13; cutoffIndex++)
                        {
                            //  Определение частоты среза фильтра.
                            double cutoff = 20.0 * (13 - cutoffIndex);

                            ////  Обновление фильтра.
                            //await UpdateFilterAsync(cutoff, cancellationToken).ConfigureAwait(false);

                            //  Создание преобразования фильтрации.
                            SincFilter transform = new(cutoff);

                            //ChannelSpectralTransform transform = new((frequency, amplitude) =>
                            //{
                            //    //  Проверка частоты.
                            //    if (frequency > cutoff)
                            //    {
                            //        return 0;
                            //    }
                            //    else
                            //    {
                            //        return amplitude;
                            //    }
                            //});

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

                            Statistic? statistic = database.Statistics.FirstOrDefault(
                                x => x.ChannelId == dbChannel.Id && x.RegistrarId == registrarId &&
                                x._Timestamp == dbFrame.Timestamp && x.Cutoff == cutoff);

                            if (statistic is null)
                            {
                                statistic = new()
                                {
                                    ChannelId = dbChannel.Id,
                                    RegistrarId = registrarId,
                                    FrameTime = beginTime,
                                    Cutoff = cutoff,
                                    Latitude = location.Latitude,
                                    Longitude = location.Longitude,
                                    Speed = speed,
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

                                //  Добавление результатов в базу данных.
                                await database.Statistics.AddAsync(statistic, cancellationToken).ConfigureAwait(false);
                            }
                            else
                            {
                                //statistic.ChannelId = dbChannel.Id;
                                //statistic.RegistrarId = registrarId;
                                //statistic.FrameTime = beginTime;
                                //statistic.Cutoff = cutoff;
                                statistic.Latitude = location.Latitude;
                                statistic.Longitude = location.Longitude;
                                statistic.Speed = speed;
                                statistic.Count = length;
                                statistic.Min = min;
                                statistic.Max = max;
                                statistic.Average = sum / length;
                                statistic.Deviation = Math.Sqrt((squaresSum - sum * sum / length) / (length - 1));
                                statistic.Sum = sum;
                                statistic.SquaresSum = squaresSum;
                                statistic.MinModulo = minModulo;
                                statistic.MaxModulo = maxModulo;
                                statistic.AverageModulo = sumModulo / length;
                                statistic.DeviationModulo = Math.Sqrt((squaresSum - sumModulo * sumModulo / length) / (length - 1));
                                statistic.SumModulo = sumModulo;
                            }

                            /*

                                    //  Настройка альтернативного ключа.
        typeBuilder.HasAlternateKey(statistic => new
        {
            statistic.ChannelId,
            statistic.RegistrarId,
            Timestamp = statistic._Timestamp,
            statistic.Cutoff
        });


                            */

                            ////  Создание результатов статистического анализа.
                            //Statistic statistic = new()
                            //{
                            //    ChannelId = dbChannel.Id,
                            //    RegistrarId = registrarId,
                            //    FrameTime = beginTime,
                            //    Cutoff = cutoff,
                            //    Latitude = location.Latitude,
                            //    Longitude = location.Longitude,
                            //    Speed = speed,
                            //    Count = length,
                            //    Min = min,
                            //    Max = max,
                            //    Average = sum / length,
                            //    Deviation = Math.Sqrt((squaresSum - sum * sum / length) / (length - 1)),
                            //    Sum = sum,
                            //    SquaresSum = squaresSum,
                            //    MinModulo = minModulo,
                            //    MaxModulo = maxModulo,
                            //    AverageModulo = sumModulo / length,
                            //    DeviationModulo = Math.Sqrt((squaresSum - sumModulo * sumModulo / length) / (length - 1)),
                            //    SumModulo = sumModulo,
                            //};

                            ////  Добавление результатов в базу данных.
                            //await database.Statistics.AddAsync(statistic, cancellationToken).ConfigureAwait(false);
                        }
                    }

                    await Journal.LogInformationAsync($"Обработан кадр: {path}", cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    //  Проверка токена отмены.
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        //  Вывод сообщения об ошибке в журнал.
                        await Journal.LogErrorAsync($"{ex}", cancellationToken).ConfigureAwait(false);
                    }

                    //  Повторный выброс исключения.
                    throw;
                }
            }, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            if (ex.IsCritical())
            {
                throw;
            }

            //  Проверка токена отмены.
            if (!cancellationToken.IsCancellationRequested)
            {
                //  Вывод сообщения об ошибке в журнал.
                await Journal.LogErrorAsync($"{ex}", cancellationToken).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Асинхронно определяет скорость.
    /// </summary>
    /// <param name="registrarId">
    /// Идентификатор регистратора.
    /// </param>
    /// <param name="beginTime">
    /// Время начала периода.
    /// </param>
    /// <param name="endTime">
    /// Время оконачания периода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск данных.
    /// </returns>
    private static async Task<double?> GetSpeedAsync(int registrarId, DateTime beginTime, DateTime endTime,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос данных по скорости.
        double[] speeds = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database.Geolocations
            .Where(geolocation => geolocation.RegistrarId == registrarId &&
                Geolocation.ToTimestamp(beginTime) <= geolocation.Timestamp &&
                geolocation.Timestamp <= Geolocation.ToTimestamp(endTime) &&
                geolocation.Speed.HasValue)
            .Select(geolocation => geolocation.Speed!.Value)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false),
            cancellationToken)
            .ConfigureAwait(false);

        //  Проверка данных.
        if (speeds.Length == 0)
        {
            //  Данные о местоположении отсутствуют.
            return null;
        }
        else
        {
            //  Возврат среднего значения.
            return speeds.Average();
        }
    }

    /// <summary>
    /// Асинхронно определяет данные местоположения.
    /// </summary>
    /// <param name="registrarId">
    /// Идентификатор регистратора.
    /// </param>
    /// <param name="beginTime">
    /// Время начала периода.
    /// </param>
    /// <param name="endTime">
    /// Время оконачания периода.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая поиск данных местоположения.
    /// </returns>
    private static async Task<Location?> GetLocationAsync(int registrarId, DateTime beginTime, DateTime endTime,
        CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос данных по широте.
        double[] latitudes = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database.Geolocations
            .Where(geolocation => geolocation.RegistrarId == registrarId &&
                Geolocation.ToTimestamp(beginTime) <= geolocation.Timestamp &&
                geolocation.Timestamp <= Geolocation.ToTimestamp(endTime) &&
                geolocation.Latitude.HasValue)
            .Select(geolocation => geolocation.Latitude!.Value)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false),
            cancellationToken)
            .ConfigureAwait(false);

        //  Запрос данных по долготе.
        double[] longitudes = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database.Geolocations
            .Where(geolocation => geolocation.RegistrarId == registrarId &&
                Geolocation.ToTimestamp(beginTime) <= geolocation.Timestamp &&
                geolocation.Timestamp <= Geolocation.ToTimestamp(endTime) &&
                geolocation.Longitude.HasValue)
            .Select(geolocation => geolocation.Longitude!.Value)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false),
            cancellationToken)
            .ConfigureAwait(false);

        //  Запрос данных по высоте.
        double[] altitudes = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database.Geolocations
            .Where(geolocation => geolocation.RegistrarId == registrarId &&
                Geolocation.ToTimestamp(beginTime) <= geolocation.Timestamp &&
                geolocation.Timestamp <= Geolocation.ToTimestamp(endTime) &&
                geolocation.Altitude.HasValue)
            .Select(geolocation => geolocation.Altitude!.Value)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false),
            cancellationToken)
            .ConfigureAwait(false);

        //  Проверка данных.
        if (latitudes.Length == 0 ||
            longitudes.Length == 0 ||
            altitudes.Length == 0)
        {
            //  Данные о местоположении отсутствуют.
            return null;
        }
        else
        {
            //  Определение средних значений.
            double latitude = latitudes.Average();
            double longitude = longitudes.Average();
            double altitude = altitudes.Average();

            //  Поиск записи в базе данных.
            Location? location = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database.Locations
                .Where(location => location.Latitude == latitude && location.Longitude == longitude)
                .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);

            //  Проверка записи.
            if (location is null)
            {
                //  Создание данных о метоположении.
                location = new(latitude, longitude, altitude);

                //  Добавление данных в базу данных.
                await OrioleDatabaseManager.TransactionAsync(async (database, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                    //  Добавление новой записи.
                    await database.Locations.AddAsync(location, cancellationToken).ConfigureAwait(false);
                }, cancellationToken).ConfigureAwait(false);
            }

            //  Возврат данных о метоположении.
            return location;
        }
    }
}
