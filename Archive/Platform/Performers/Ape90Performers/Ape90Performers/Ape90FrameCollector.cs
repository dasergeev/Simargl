using Apeiron.Frames;
using Apeiron.Frames.TestLab;

namespace Apeiron.Platform.Performers.Ape90Performers;

/// <summary>
/// Представляет исполнителя, выполняющего сборку кадров регистрации.
/// </summary>
public sealed class Ape90FrameCollector :
    Performer
{
    /// <summary>
    /// Путь к кадрам.
    /// </summary>
    public const string RecordsRootPath = @"\\NtoData\NTO_Files\Records\2021\0002";

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="journal">
    /// Журнал.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="journal"/> передана пустая ссылка.
    /// </exception>
    public Ape90FrameCollector(Journal journal) :
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
        //  Поддержка выполнения.
        await KeepAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных.
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            //  Получение каталогов исходных файлов.
            RawDirectory[] rawDirectories = await context.RawDirectories
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Выполнение работы в каталогах.
            await Parallel.ForEachAsync(
                rawDirectories,
                new ParallelOptions
                {
                    CancellationToken = cancellationToken,
                },
                WorkInRawDirectoryAsync).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Представляет временной интервал.
    /// </summary>
    private struct TimeInterval
    {
        /// <summary>
        /// Каталог исходных данных.
        /// </summary>
        public RawDirectory RawDirectory { get; set; }

        /// <summary>
        /// Время начала интервала.
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// Время окончания интервала.
        /// </summary>
        public DateTime EndTime { get; set; }
    }

    /// <summary>
    /// Асинхронно выполняет работу с каталогом исходных файлов.
    /// </summary>
    /// <param name="rawDirectory">
    /// Каталог исходных файлов.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с каталогом исходных файлов.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask WorkInRawDirectoryAsync(RawDirectory rawDirectory, CancellationToken cancellationToken)
    {
        //  Безопасный вызов.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Список временных интервалов.
            List<TimeInterval> intervals = new();

            //  Начало интервала.
            DateTime beginTime = rawDirectory.BeginTime;

            //  Нормализация начала интервала.
            beginTime = new(
                beginTime.Year, beginTime.Month, beginTime.Day,
                beginTime.Hour, beginTime.Minute, beginTime.Second);

            //  Формирование списка временных интервалов.
            while (beginTime < rawDirectory.EndTime)
            {
                //  Добавление временного интервала.
                intervals.Add(new()
                {
                    RawDirectory = rawDirectory,
                    BeginTime = beginTime,
                    EndTime = beginTime.AddMinutes(1),
                });

                //  Смещение начала интервала.
                beginTime = beginTime.AddMinutes(1);
            }

            //  Создание генератора случайных чисел.
            Random random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));

            //  Изменение порядка временных интервалов.
            for (int i = 0; i < intervals.Count; i++)
            {
                //  Получение нового индекса.
                int index = random.Next() % intervals.Count;

                //  Перестановка элементов.
                (intervals[i], intervals[index]) = (intervals[index], intervals[i]);
            }

            //  Выполнение обработки временных интервалов.
            await Parallel.ForEachAsync(
                intervals,
                new ParallelOptions()
                {
                    CancellationToken = cancellationToken
                },
                WorkInTimeIntervalAsync).ConfigureAwait(false);
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с временным интервалом.
    /// </summary>
    /// <param name="timeInterval">
    /// Каталог исходных файлов.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с временным интервалом.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private async ValueTask WorkInTimeIntervalAsync(TimeInterval timeInterval, CancellationToken cancellationToken)
    {
        //  Вывод информации в журнал.
        await Journal.LogInformationAsync(
            $"Работа с интервалом: {timeInterval.BeginTime} - {timeInterval.EndTime}",
            cancellationToken).ConfigureAwait(false);

        //  Безопасный вызов.
        await SafeCallAsync(async cancellationToken =>
        {
            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Создание контекста сеанса работы с базой данных.
            using Ape90DatabaseContext context = Ape90DatabaseContextProvider.CreateContext();

            //  Запрос информации об исходных кадрах.
            RawFrame[] rawFrames = await context.RawFrames
                .Where(rawFrame => rawFrame.BeginTime < timeInterval.EndTime && rawFrame.EndTime > timeInterval.BeginTime)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Запрос геолокационных данных.
            Geolocation[] geolocations = await context.Geolocations
                .Where(geolocation =>
                    timeInterval.BeginTime.AddSeconds(-10) < geolocation.RecTime &&
                    geolocation.RecTime < timeInterval.EndTime.AddSeconds(10))
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Проверка необходимости формирования кадра регистрации.
            if (rawFrames.All(rawFrame => rawFrame.IsAnalyzed) &&
                geolocations.All(geolocation => geolocation.IsAnalyzed ||
                    geolocation.RecTime < timeInterval.BeginTime ||
                    geolocation.RecTime >= timeInterval.EndTime))
            {
                //  Кадр уже сформирован.
                return;
            }

            //  Начало транзакции.
            using var transaction = context.Database.BeginTransaction();

            //  Построение кадра.
            Frame frame = await BuildFrameAsync(
                timeInterval, rawFrames, geolocations, cancellationToken).ConfigureAwait(false);

            //  Скорость движения.
            double speed = 0;
            int count = 0;

            Channel IsGPS_Sr = frame.Channels["IsGPS_Sr"];
            Channel V_GPS_Sr = frame.Channels["V_GPS_Sr"];

            //  Перебор скоростей.
            for (int i = 0; i < 60; i++)
            {
                if (IsGPS_Sr[i] != 0)
                {
                    speed += V_GPS_Sr[i];
                    count++;
                }
            }

            if (count > 0)
            {
                speed /= count;
            }

            int frameNumber = 1 + (int)(timeInterval.BeginTime - new DateTime(
                timeInterval.BeginTime.Year, timeInterval.BeginTime.Month, timeInterval.BeginTime.Day)).TotalMinutes;

            string fileName = $"Vp{(int)speed:000}_{(int)(speed * 10) % 10:0}.{frameNumber:0000}";
            string filePath = CreateFolderIfNotExist(RecordsRootPath);
            filePath = CreateFolderIfNotExist(Path.Combine(filePath, $"{timeInterval.BeginTime:yyyy-MM-dd}"));
            filePath = Path.Combine(filePath, fileName);

            //  Сохранение кадра.
            frame.Save(filePath, StorageFormat.TestLab);

            //  Запрос информации о кадре.
            FrameInfo? frameInfo = await context.FrameInfos.FirstOrDefaultAsync(
                frameInfo => frameInfo.BeginTime == timeInterval.BeginTime, cancellationToken)
                .ConfigureAwait(false);

            //  Проверка записи.
            if (frameInfo is not null)
            {
                //  Удаление записи из базы.
                context.FrameInfos.Remove(frameInfo);
            }

            //  Создание записи.
            frameInfo = new()
            {
                Path = filePath,
                Duration = 60,
                BeginTime = timeInterval.BeginTime,
                EndTime = timeInterval.EndTime,
            };

            //  Добавление записи в базу данных.
            context.FrameInfos.Add(frameInfo);

            //  Проверка токена отмены.
            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

            //  Установка отметок об анализе исходных кадров.
            foreach (RawFrame rawFrame in rawFrames)
            {
                //  Исходный кадр проанализирован.
                rawFrame.IsAnalyzed = true;
            }

            //  Установка отметок об анализе геолокационных данных.
            foreach (Geolocation geolocation in geolocations)
            {
                //  Геолокационные данные проанализированы.
                geolocation.IsAnalyzed = true;
            }

            //  Сохранение изменений в базу данных.
            context.SaveChanges();

            //  Фиксирование изменений.
            transaction.Commit();
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет построение кадра.
    /// </summary>
    /// <param name="timeInterval">
    /// Временной интервал.
    /// </param>
    /// <param name="rawFrames">
    /// Массив исходных кадров.
    /// </param>
    /// <param name="geolocations">
    /// Массив геолокационных данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая построение кадра.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// Операция отменена.
    /// </exception>
    private static async ValueTask<Frame> BuildFrameAsync(TimeInterval timeInterval,
        RawFrame[] rawFrames, Geolocation[] geolocations, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Частота и срез основного канала.
        const int mainSampling = 1200, mainCutoff = 500;

        //  Частота и срез вспомогательного канала.
        const int auxiliarySampling = 1, auxiliaryCutoff = 1;

        //  Длительность записи.
        const int duration = 60;

        //  Список основных каналов.
        string[] mainNames = {
            "P1_1", "P1_2", "L1_1", "L1_2", "UX1", "UY1", "UZ1",
            "P2_1", "P2_2", "L2_1", "L2_2",
            "P3_1", "P3_2", "L3_1", "L3_2",
            "P4_1", "P4_2", "L4_1", "L4_2",
            "P5_1", "P5_2", "L5_1", "L5_2",
            "P6_1", "P6_2",  "L6_1", "L6_2", "UX6", "UY6", "UZ6",
        };

        //  Создание кадра.
        Frame frame = new();

        //  Создание основных каналов.
        createMainChannel("IsData", "bool");
        createCouplerChannels(1);
        createAccelerationChannels(1);
        createCouplerChannels(2);
        createCouplerChannels(3);
        createCouplerChannels(4);
        createCouplerChannels(5);
        createCouplerChannels(6);
        createAccelerationChannels(6);

        //  Создание вспомогательных каналов.
        Channel IsGPS_Sr = createAuxiliaryChannel("IsGPS_Sr", "bool");
        Channel V_GPS_Sr = createAuxiliaryChannel("V_GPS_Sr", "kph");
        Channel Lat_GPS_Sr = createAuxiliaryChannel("Lat_GPS_Sr", "º");
        Channel Lon_GPS_Sr = createAuxiliaryChannel("Lon_GPS_Sr", "º");
        Channel IsGPS = createAuxiliaryChannel("IsGPS", "bool");
        Channel V_GPS = createAuxiliaryChannel("V_GPS", "kph");
        Channel Lat_GPS = createAuxiliaryChannel("Lat_GPS", "º");
        Channel Lon_GPS = createAuxiliaryChannel("Lon_GPS", "º");
        Channel A_GPS = createAuxiliaryChannel("A_GPS", "m");
        Channel dTime_GPS = createAuxiliaryChannel("dTime_GPS", "s");
        Channel Pole_GPS = createAuxiliaryChannel("Pole_GPS", "º");
        Channel Magnetic_GPS = createAuxiliaryChannel("Magnetic_GPS", "º");
        Channel Solution_GPS = createAuxiliaryChannel("Solution_GPS", "");
        Channel Sat_GPS = createAuxiliaryChannel("Sat_GPS", "");
        Channel Hdop_GPS = createAuxiliaryChannel("Hdop_GPS", "");
        Channel Geoidal_GPS = createAuxiliaryChannel("Geoidal_GPS", "");
        Channel Age_GPS = createAuxiliaryChannel("Age_GPS", "");
        Channel Knots_GPS = createAuxiliaryChannel("Knots_GPS", "kn");

        //  Получение спеуиальных каналов.
        Channel isData = frame.Channels["IsData"];

        //  Загрузка исходных данных каналов.
        foreach (RawFrame rawFrame in rawFrames)
        {
            //  Загрузка исходного кадра.
            Frame sourceFrame = new(rawFrame.Path);

            //  Определение начальных индексов.
            int mainBeginIndex = mainSampling * (int)(rawFrame.BeginTime - timeInterval.BeginTime).TotalSeconds;
            int auxiliaryBeginIndex = auxiliarySampling * (int)(rawFrame.BeginTime - timeInterval.BeginTime).TotalSeconds;

            //  Перебор основных каналов.
            foreach (string name in mainNames)
            {
                //  Получение основных каналов.
                Channel targetChannel = frame.Channels[name];
                Channel offsetChannel = frame.Channels[name + "_VMove"];

                //  Получение исходного канала.
                Channel sourceChannel = timeInterval.RawDirectory.Id switch
                {
                    1 => name switch
                    {
                        "P1_1" => sourceFrame.Channels["P4_1"],
                        "P1_2" => sourceFrame.Channels["P4_2"],
                        "L1_1" => sourceFrame.Channels["L4_1"],
                        "L1_2" => sourceFrame.Channels["L4_2"],
                        "UX1" => sourceFrame.Channels["P5_1"],
                        "UY1" => sourceFrame.Channels["P5_2"],
                        "UZ1" => sourceFrame.Channels["L5_1"],
                        "P2_1" => sourceFrame.Channels["L5_2"],
                        "P2_2" => sourceFrame.Channels["P6_1"],
                        "L2_1" => sourceFrame.Channels["P6_2"],
                        "L2_2" => sourceFrame.Channels["L6_1"],
                        "P3_1" => sourceFrame.Channels["L6_2"],
                        "P3_2" => sourceFrame.Channels["UX6"],
                        "L3_1" => sourceFrame.Channels["UY6"],
                        "L3_2" => sourceFrame.Channels["UZ6"],
                        "P4_1" => sourceFrame.Channels["P1_1"],
                        "P4_2" => sourceFrame.Channels["P1_2"],
                        "L4_1" => sourceFrame.Channels["L1_1"],
                        "L4_2" => sourceFrame.Channels["L1_2"],
                        "P5_1" => sourceFrame.Channels["UX1"],
                        "P5_2" => sourceFrame.Channels["UY1"],
                        "L5_1" => sourceFrame.Channels["UZ1"],
                        "L5_2" => sourceFrame.Channels["P2_1"],
                        "P6_1" => sourceFrame.Channels["P2_2"],
                        "P6_2" => sourceFrame.Channels["L2_1"],
                        "L6_1" => sourceFrame.Channels["L2_2"],
                        "L6_2" => sourceFrame.Channels["P3_1"],
                        "UX6" => sourceFrame.Channels["P3_2"],
                        "UY6" => sourceFrame.Channels["L3_1"],
                        "UZ6" => sourceFrame.Channels["L3_2"],
                        _ => throw new InvalidDataException("Канал не найден"),
                    },
                    2 => sourceFrame.Channels[name],
                    _ => throw new InvalidDataException("Неизвестный источник данных."),
                };

                //  Получение заголовка канала.
                TestLabChannelHeader header = (TestLabChannelHeader)sourceChannel.Header;

                //  Определение длины исходного канала.
                int sourceLength = sourceChannel.Length;

                //  Перебор значений исходного канала.
                for (int i = 0; i < sourceLength; i++)
                {
                    //  Определение индекса в основном канале.
                    int index = mainBeginIndex + i;

                    //  Проверка индекса.
                    if (index >= 0 && index < mainSampling * duration)
                    {
                        //  Получение значения.
                        targetChannel[index] = sourceChannel[i] + header.Offset;

                        //  Установка вспомогательных каналов.
                        offsetChannel[index] = header.Offset;
                        isData[index] = 1;
                    }
                }
            }

            //  Получение каналов GPS.
            Channel sourceV_GPS = sourceFrame.Channels["V_GPS"];
            Channel sourceLat_GPS = sourceFrame.Channels["Lat_GPS"];
            Channel sourceLon_GPS = sourceFrame.Channels["Lon_GPS"];

            //  Перебор значений.
            for (int i = 0; i < sourceV_GPS.Length; i++)
            {
                //  Определение индекса в основном канале.
                int index = auxiliaryBeginIndex + i;

                //  Проверка индекса.
                if (index >= 0 && index < duration)
                {
                    //  Установка значений.
                    IsGPS_Sr[index] = sourceLat_GPS[i] > 0 && sourceLon_GPS[i] > 0 ? 1 : 0;
                    V_GPS_Sr[index] = sourceV_GPS[i];
                    Lat_GPS_Sr[index] = sourceLat_GPS[i];
                    Lon_GPS_Sr[index] = sourceLon_GPS[i];
                }
            }
        }

        //  Перебор индексов геолокационных каналов.
        for (int i = 0; i < duration; i++)
        {
            //  Определение текущей метки времени.
            DateTime time = timeInterval.BeginTime.AddSeconds(i);

            //  Установка значений геолокационных каналов.
            V_GPS[i] = getValue(geolocation => geolocation.Speed, 0);
            Lat_GPS[i] = getValue(geolocation => geolocation.Latitude, 0);
            Lon_GPS[i] = getValue(geolocation => geolocation.Longitude, 0);
            A_GPS[i] = getValue(geolocation => geolocation.Altitude, 0);
            dTime_GPS[i] = (getValue(geolocation => geolocation.GpsTime, timeInterval.BeginTime.AddSeconds(i)) - timeInterval.BeginTime.AddSeconds(i)).TotalSeconds;
            Pole_GPS[i] = getValue(geolocation => geolocation.PoleCourse, 0);
            Magnetic_GPS[i] = getValue(geolocation => geolocation.MagneticCourse, 0);
            Solution_GPS[i] = getValue(geolocation => (int?)geolocation.Solution, 0);
            Sat_GPS[i] = getValue(geolocation => geolocation.Satellites, 0);
            Hdop_GPS[i] = getValue(geolocation => geolocation.Hdop, 0);
            Geoidal_GPS[i] = getValue(geolocation => geolocation.Geoidal, 0);
            Age_GPS[i] = getValue(geolocation => geolocation.Age, 0);
            Knots_GPS[i] = getValue(geolocation => geolocation.Knots, 0);
            IsGPS[i] = Lat_GPS[i] > 0 && Lon_GPS[i] > 0 ? 1 : 0;

            T getValue<T>(Func<Geolocation, T?> selector, T defaultValue)
                where T : struct
            {
                var result = geolocations
                    .Select(geolocation => new
                    {
                        Value = selector(geolocation),
                        DeltaTimestamp = Math.Abs((geolocation.RecTime - time).TotalSeconds)
                    })
                    .Where(entity => entity.Value.HasValue && entity.DeltaTimestamp < 10)
                    .OrderBy(entity => entity.DeltaTimestamp)
                    .FirstOrDefault();

                if (result is not null && result.Value.HasValue)
                {
                    return result.Value.Value;
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        //  Возврат собранного кадра.
        return frame;

        //  Создаёт основной канал.
        void createMainChannel(string name, string unit)
        {
            frame.Channels.Add(new Channel(name, unit, mainSampling, mainCutoff, mainSampling * duration));
            frame.Channels.Add(new Channel(name + "_VMove", unit, mainSampling, mainCutoff, mainSampling * duration));
        }

        //  Создаёт каналы автосцепки.
        void createCouplerChannels(int number)
        {
            createMainChannel($"P{number}_1", "kN");
            createMainChannel($"P{number}_2", "kN");
            createMainChannel($"L{number}_1", "mm");
            createMainChannel($"L{number}_2", "mm");
        }

        //  Создаёт каналы ускорений.
        void createAccelerationChannels(int number)
        {
            createMainChannel($"UX{number}", "g");
            createMainChannel($"UY{number}", "g");
            createMainChannel($"UZ{number}", "g");
        }

        //  Создаёт вспомогательный канал.
        Channel createAuxiliaryChannel(string name, string unit)
        {
            Channel channel = new(name, unit, auxiliarySampling, auxiliaryCutoff, auxiliarySampling * duration);
            frame.Channels.Add(channel);
            return channel;
        }
            
    }

    /// <summary>
    /// Представляет функцию создания директории, если она отсутствует.
    /// </summary>
    /// <param name="path"></param>
    /// <exception cref="ArgumentNullException">В параметре <paramref name="path"/> переданна пустая ссылка.</exception>
    /// <exception cref="ArgumentOutOfRangeException">В параметре <paramref name="path"/> переданна пустая строка.</exception>
    private static string CreateFolderIfNotExist(string path)
    {
        //Проверка на пустую ссылку и пустую строку.
        Check.IsNotNull(path, nameof(path));
        Check.IsNotEmpty(path, nameof(path));

        //  Перехват не системных исключений.
        Invoker.SafeNotSystem(delegate
        {
            //  Проверка каталога.
            if (!Directory.Exists(path))
            {
                //  Создание каталога.
                Directory.CreateDirectory(path);
            }
        });

        return path;
    }
}
