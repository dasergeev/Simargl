using Apeiron.Analysis.Transforms;
using Apeiron.Oriole.Server.Workers.Common;
using Apeiron.Platform.Databases.OrioleDatabase;
using Apeiron.Platform.Databases.OrioleDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using Apeiron.Platform.Heap;

namespace Apeiron.Oriole.Server.Workers.Analysis;

/// <summary>
/// Представляет фоновый процесс, выполняющий экстремальный анализ.
/// </summary>
public class ExtremumAnalysis :
    Worker<ExtremumAnalysis>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="logger">
    /// Средство записи в журнал службы.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="logger"/> передана пустая ссылка.
    /// </exception>
    public ExtremumAnalysis(ILogger<ExtremumAnalysis> logger) :
        base(logger)
    {

    }

    /// <summary>
    /// Асинхронно выполняет фоновую работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая фоновую работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    protected override async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Выполнение работы с базой данных.
            await WorkDatabaseAsync(cancellationToken);

            //  Ожидание перед следующим поиском.
            await Task.Delay(60000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с базой данных.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async Task WorkDatabaseAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Запрос информации о кадрах.
        Tuple<int, long>[] frames = await OrioleDatabaseManager.RequestAsync(
            async (database, cancellationToken) => await database.Frames
                .AsNoTracking()
                .Where(frame => !frame.IsExtremum)
                .Select(frame => new Tuple<int, long>(frame.RegistrarId, frame.Timestamp))
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        //  Создание генератора случайных чисел.
        Random random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));

        //  Перестановка кадров.
        for (int i = 0; i < frames.Length; i++)
        {
            //  Получение нового индекса.
            int index = random.Next() % frames.Length;

            //  Перестановка элементов.
            (frames[i], frames[index]) = (frames[index], frames[i]);
        }

        //  Асинхронная работа с кадрами.
        await Parallel.ForEachAsync(
            frames,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken,
            },
            WorkInFrameAsync).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с кадром.
    /// </summary>
    /// <param name="frameKey">
    /// Ключ кадра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask WorkInFrameAsync(Tuple<int, long> frameKey, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Получение идентификатора регистратора.
        int registrarId = frameKey.Item1;

        //  Получение метки времени получения данных.
        long timestamp = frameKey.Item2;

        //  Проверка необходимости обработки кадра.
        if (!await OrioleDatabaseManager.RequestAsync(
            async (database, cancellationToken) => await database.Frames
                .AnyAsync(frame => frame.RegistrarId == registrarId &&
                    frame.Timestamp == timestamp &&
                    !frame.IsExtremum, cancellationToken),
            cancellationToken).ConfigureAwait(false))
        {
            //  Кадр уже обработан.
            return;
        }

        //  Начало транзакции.
        await OrioleDatabaseManager.TryTransactionAsync(async (database, cancellationToken) =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Блок перехвата исключений для записи ошибки в журнал.
            try
            {
                //  Получение кадра.
                Frame? dbFrame = await database.Frames
                    .Where(frame => frame.RegistrarId == registrarId &&
                        frame.Timestamp == timestamp &&
                        !frame.IsExtremum)
                    .Include(frame => frame.Registrar)
                    .ThenInclude(registrar => registrar.Channels)
                    .Include(frame => frame.Registrar)
                    .ThenInclude(registrar => registrar.RecordDirectories)
                    .Include(frame => frame.Extremums)
                    .FirstOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);

                //  Проверка полученного кадра.
                if (dbFrame is null)
                {
                    //  Завершение анализа.
                    return;
                }

                //  Удаление информации о текущих данных.
                dbFrame.Extremums.Clear();

                //  Установка флага обработки.
                dbFrame.IsExtremum = true;

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

                //  Проверка токена отмены.
                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                //  Цикл по частотам.
                for (int cutoffIndex = 0; cutoffIndex < 13; cutoffIndex++)
                {
                    //  Определение частоты среза фильтра.
                    double cutoff = 20.0 * (13 - cutoffIndex);

                    //  Создание преобразования фильтрации.
                    SincFilter transform = new(cutoff);

                    //  Перебоер всех каналов для фильтрации.
                    foreach (var channel in frame.Channels)
                    {
                        //  Фильтрация канала.
                        transform.Invoke(channel, channel);
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

                        // Определение максимума канала.
                        double max;

                        // Определение индекса максимума канала.
                        int imax;

                        // Определение минимума канала.
                        double min1;

                        // Определение индекса минимума канала.
                        int imin1;


                        //Создание копии канала.
                        Frames.Channel clonChannel = channel.Clone();

                        //Нахождение максимума и индекса.
                        max = clonChannel.Max();
                        imax = clonChannel.Vector.IndexMax();

                        //Нахождение минимума и индекса.
                        min1 = clonChannel.Min();
                        imin1 = clonChannel.Vector.IndexMin();

                        //Создание списка с парами, содержащими название канала и значение максимума.
                        List<Tuple<string, double>> dataMax = new();

                        //Создание списка с парами, содержащими название канала и значение минимума.
                        List<Tuple<string, double>> dataMin = new();

                        //Добавление в список максимума текущего канала.
                        dataMax.Add(new Tuple<string, double>("max1", max));

                        //Добавление в список минимума текущего канала.
                        dataMin.Add(new Tuple<string, double>("min1", min1));

                        //Цикл по всем каналам.
                        foreach (var secondChannel in frame.Channels)
                        {
                            // Проверка равенс
                            if (secondChannel.Length == length)
                            {
                                //Добавление в списки значений из других каналов в момент времени максимумов.
                                dataMax.Add(new Tuple<string, double>($"{secondChannel.Name}", secondChannel[imax]));

                                //Добавление в списки значений из других каналов в момент времени минимумов.
                                dataMin.Add(new Tuple<string, double>($"{secondChannel.Name}", secondChannel[imin1]));
                            }
                        }

                        // Добавление максимума.
                        await addExtremumAsync(false, speed, imax, max, dataMax, cancellationToken).ConfigureAwait(false);

                        // Добавление минимума.
                        await addExtremumAsync(true, speed, imin1, min1, dataMin, cancellationToken).ConfigureAwait(false);

                        async Task addExtremumAsync(
                            bool isMin, double speed, int index, double value, List<Tuple<string, double>> data,
                            CancellationToken cancellationToken)
                        {
                            // Функция получения значения по имени канала.
                            double getValue(string name)
                            {
                                return data
                                    .Where(entity => entity.Item1 == name)
                                    .Select(entity => entity.Item2)
                                    .First();
                            }

                            //  Создание результатов анализа.
                            Extremum extremum = new()
                            {
                                RegistrarId = registrarId,
                                FrameTimestamp = dbFrame.Timestamp,
                                ChannelId = dbChannel.Id,
                                Cutoff = cutoff,
                                LocationId = location.Id,
                                Path = path,
                                Speed = speed,
                                IsMin = isMin,
                                ExtremumIndex = index,
                                ExtremumValue = value,

                                Uxb1Value = getValue("Uxb1"),
                                Uyb1Value = getValue("Uyb1"),
                                Uzb1Value = getValue("Uzb1"),
                                Uxb2Value = getValue("Uxb2"),
                                Uyb2Value = getValue("Uyb2"),
                                Uzb2Value = getValue("Uzb2"),
                                UxrValue = getValue("Uxr"),
                                UyrValue = getValue("Uyr"),
                                UzrValue = getValue("Uzr"),
                                Uxk1Value = getValue("Uxk1"),
                                Uyk1Value = getValue("Uyk1"),
                                Uzk1Value = getValue("Uzk1"),
                                Uxk2Value = getValue("Uxk2"),
                                Uyk2Value = getValue("Uyk2"),
                                Uzk2Value = getValue("Uzk2"),
                            };

                            //  Добавление результатов в базу данных.
                            await database.Extremums.AddAsync(extremum, cancellationToken).ConfigureAwait(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Вывод информации об ошибке в журнал.
                    Logger.LogError("{exception}", ex);
                }

                //  Повторный выброс исключения.
                throw;
            }
        },
        cancellationToken).ConfigureAwait(false);
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
