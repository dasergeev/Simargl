using Apeiron.IO;
using Apeiron.Oriole.Server.Workers.Common;
using Apeiron.Platform.Databases.OrioleDatabase;
using Apeiron.Platform.Databases.OrioleDatabase.Entities;
using Apeiron.Recording.Adxl357;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Text;

namespace Apeiron.Oriole.Server.Workers.Collectors;

/// <summary>
/// Представляет фоновый процесс, выполняющий сборку кадров регистрации.
/// </summary>
public class FrameCollector :
    Worker<FrameCollector>
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
    public FrameCollector(ILogger<FrameCollector> logger) :
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
            try
            {
                //  Выполнение работы с базой данных.
                await WorkDatabaseAsync(cancellationToken);

                //  Ожидание перед следующим поиском.
                await Task.Delay(60000, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (ex.IsCritical())
                {
                    throw;
                }
            }
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

        //  Запрос информации о регистраторах.
        Registrar[] registrars = await OrioleDatabaseManager.RequestAsync(
            async (database, cancellationToken) => await database.Registrars
                .AsNoTracking()
                .Include(registrar => registrar.RawDirectories)
                .Include(registrar => registrar.RecordDirectories)
                .Include(registrar => registrar.Channels)
                .ThenInclude(channel => channel.Sources)
                .ThenInclude(source => source.Sensor)
                .OrderBy(registrar => registrar.Id)
                .ToArrayAsync(cancellationToken),
            cancellationToken).ConfigureAwait(false);

        //foreach (Registrar registrar in registrars)
        //{
        //    await WorkInRegistrarAsync(registrar, cancellationToken).ConfigureAwait(false);
        //}

        //  Асинхронная работа с регистратором.
        await Parallel.ForEachAsync(
            registrars,
            new ParallelOptions()
            {
                CancellationToken = cancellationToken,
            },
            WorkInRegistrarAsync).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно выполняет работу с регистратором.
    /// </summary>
    /// <param name="registrar">
    /// Регистратор.
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
    private async ValueTask WorkInRegistrarAsync(Registrar registrar, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Проверка количества источников.
        if (registrar.Channels.Count == 0)
        {
            //  Нет данных для работы.
            return;
        }

        //  Проверка каталога для выходных данных.
        if (registrar.RecordDirectories.Count == 0)
        {
            //  Нет данных для работы.
            return;
        }

        //  Получение пути к корневому каталогу.
        string rootPath = PathBuilder.Normalize(registrar.RecordDirectories.First().Path);

        //  Получение временных границ для поиска.
        (DateTime beginBorderlineTime, DateTime endBorderlineTime) = GetTimeBoundaries(registrar.Channels);

        //  Проверка токена отмены.
        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

        //  Определение времени первого файла с пакетами, требующими анализа.
        DateTime beginPackageFileTime = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database
            .PackageFiles
            .Where(packageFile => packageFile.RawDirectory.RegistrarId == registrar.Id &&
                !packageFile.IsAnalyzed &&
                packageFile.LocationType == PackageFileLocationType.Internal)
            .Select(packageFile => packageFile.NormalizedBeginTime)
            .OrderBy(beginTime => beginTime)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false);

        if (beginPackageFileTime == default)
        {
            beginPackageFileTime = DateTime.Now;
        }

        //  Определение времени первых геолокационных данных, требующих анализа.
        DateTime beginGeolocationTime = Geolocation.FromTimestamp(await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database
            .Geolocations
            .Where(geolocation => geolocation.RegistrarId == registrar.Id &&
                !geolocation.IsAnalyzed)
            .Select(geolocation => geolocation.Timestamp)
            .OrderBy(timestamp => timestamp)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false),
            cancellationToken).ConfigureAwait(false));

        if (beginGeolocationTime == default)
        {
            beginGeolocationTime = DateTime.Now;
        }

        //  Определение времени начала не проанализированных данных.
        DateTime beginDataTime = new (Math.Min(beginPackageFileTime.Ticks, beginGeolocationTime.Ticks));

        //  Корректировка временой границы.
        beginBorderlineTime = new(Math.Max(beginDataTime.Ticks, beginBorderlineTime.Ticks));

        //  Шаг времени в секундах.
        int timeStep = 60;

        //  Создание списка временных интервалов.
        List<DateTime> allTimes = new();

        //  Перебор временных интервалов.
        for (DateTime beginTime = beginBorderlineTime; beginTime < endBorderlineTime; beginTime = beginTime.AddSeconds(timeStep))
        {
            //  Добавление в список.
            allTimes.Add(beginTime);
        }

        //  Создание генератора случайных чисел.
        Random random = new(unchecked((int)(DateTime.Now.Ticks % int.MaxValue)));

        //  Перетасовка массива кадров.
        for (int i = 0; i < allTimes.Count; i++)
        {
            //  Получение нового индекса.
            int index = random.Next() % allTimes.Count;

            //  Перестановка файлов.
            (allTimes[i], allTimes[index]) = (allTimes[index], allTimes[i]);
        }

        //  Параллельная обработка временных интервалов.
        await Parallel.ForEachAsync(allTimes,
             new ParallelOptions()
             {
                 CancellationToken = cancellationToken
             },
             async (beginTime, cancellationToken) =>
             {
                 //  Проверка токена отмены.
                 await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                 //  Время конца кадра.
                 DateTime endTime = beginTime.AddSeconds(timeStep);

                 Logger.LogInformation("{message}", $"{registrar.Name}: {beginTime} - {endTime}");

                 //  Определение количества файлов с пакетами, требующими анализа.
                 int packageFileCount = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database
                     .PackageFiles
                     .Where(packageFile => packageFile.RawDirectory.RegistrarId == registrar.Id &&
                         !packageFile.IsAnalyzed &&
                         packageFile.LocationType == PackageFileLocationType.Internal &&
                         packageFile.NormalizedBeginTime < endTime &&
                         packageFile.NormalizedEndTime > beginTime)
                     .CountAsync(cancellationToken).ConfigureAwait(false),
                     cancellationToken).ConfigureAwait(false);

                 //  Определение геолокационных данных, требующих анализа.
                 int geolocationCount = await OrioleDatabaseManager.RequestAsync(async (database, cancellationToken) => await database
                     .Geolocations
                     .Where(geolocation => geolocation.RegistrarId == registrar.Id &&
                         !geolocation.IsAnalyzed &&
                         Geolocation.ToTimestamp(beginTime) <= geolocation.Timestamp &&
                         geolocation.Timestamp < Geolocation.ToTimestamp(endTime))
                     .CountAsync(cancellationToken).ConfigureAwait(false),
                     cancellationToken).ConfigureAwait(false);

                 //  Проверка необходимости формирования кадра.
                 if (packageFileCount == 0 && geolocationCount == 0)
                 {
                     //  Переход к следующему временному интервалу.
                     return;
                 }

                 //  Получение метки времени кадра.
                 long frameTimestamp = Frame.ToTimestamp(beginTime);

                 //  Получение номера кадра.
                 int frameNumber = 60 * beginTime.Hour + beginTime.Minute + 1;

                 //  Получение расширения файла.
                 string extension = $".{frameNumber:0000}";

                 //  Определение пути к каталогу, содержащему кадр.
                 string directoryPath = PathBuilder.Combine(rootPath, $"{beginTime:yyyy-MM-dd}");

                 //  Проверка каталога.
                 if (!Directory.Exists(directoryPath))
                 {
                     //  Создание каталога.
                     Directory.CreateDirectory(directoryPath);
                 }

                 await SafeCallAsync(
                   async cancellationToken =>
                   {
                       //  Проверка токена отмены.
                       await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                       //  Поиск кадра в базе данных.
                       await OrioleDatabaseManager.TransactionAsync(async (database, cancellationToken) =>
                       {
                           //  Запрос в базу данных.
                           Frame? dbFrame = await database.Frames.FindAsync(
                               new object?[] { registrar.Id, frameTimestamp },
                               cancellationToken).ConfigureAwait(false);

                           //  Проверка записи.
                           if (dbFrame is not null)
                           {
                               //  Поиск файла.
                               FileInfo? fileInfo = new DirectoryInfo(directoryPath).GetFiles()
                                   .Where(fileInfo => fileInfo.Extension == extension)
                                   .FirstOrDefault();

                               //  Проверка файла.
                               if (fileInfo is not null)
                               {
                                   //  Удаление файла.
                                   fileInfo.Delete();
                               }

                               //  Удаление записи о кадре.
                               database.Frames.Remove(dbFrame);
                           }
                       },
                        cancellationToken).ConfigureAwait(false);
                   },
                   cancellationToken).ConfigureAwait(false);

                 //  Вывод в журнал.
                 Logger.LogInformation("Начало транзакции");

                 await SafeCallAsync(
                    async cancellationToken =>
                    {
                        //  Проверка токена отмены.
                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                        //  Начало транзакции.
                        await OrioleDatabaseManager.TransactionAsync(async (database, cancellationToken) =>
                        {
                            //  Проверка токена отмены.
                            await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                            //  Загрузка файлов с пакетами.
                            PackageFile[] allPackageFiles = await database.PackageFiles
                                .Include(packageFile => packageFile.RawDirectory)
                                .Where(packageFile => packageFile.RawDirectory.RegistrarId == registrar.Id &&
                                    packageFile.LocationType == PackageFileLocationType.Internal &&
                                    packageFile.NormalizedBeginTime < endTime &&
                                    packageFile.NormalizedEndTime > beginTime)
                                .OrderBy(packageFile => packageFile.NormalizedBeginTime)
                                .ToArrayAsync(cancellationToken)
                                .ConfigureAwait(false);

                            //  Пребор всех пакетов.
                            foreach (PackageFile packageFile in allPackageFiles)
                            {
                                //  Установка флага анализа.
                                //  Не выносить код из транзакции!!!
                                packageFile.IsAnalyzed = true;
                            }

                            //  Создание кадра регистрации.
                            Frames.Frame frame = new();

                            //  Список основных каналов.
                            List<Frames.Channel> mainChannels = new();

                            //  Список информационных каналов.
                            List<Frames.Channel> infoChannels = new();

                            //  Создание основных каналов и каналов с информацией о состоянии датчиков.
                            foreach (Channel dbChannel in registrar.Channels)
                            {
                                //  Проверка токена отмены.
                                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                //  Определение частоты дискретизации канала.
                                int channelSampling = (int)Math.Round(dbChannel.Sampling);

                                //  Определение длины канала.
                                int channelLength = channelSampling * timeStep;

                                //  Создание канала кадра регистрации.
                                Frames.Channel channel = new(dbChannel.Name, "g", channelSampling, dbChannel.Cutoff, channelLength);

                                //  Добавление канала в список.
                                mainChannels.Add(channel);

                                //  Получение источников канала.
                                IOrderedEnumerable<Source> sources = dbChannel.Sources
                                    .Where(source => source.BeginTime < endTime && source.EndTime > beginTime)
                                    .OrderBy(source => source.BeginTime);

                                //  Перебор источников канала.
                                foreach (Source source in sources)
                                {
                                    //  Проверка токена отмены.
                                    await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                    //  Получение формата датчика.
                                    int format = source.Format;

                                    //  Получение серийного номера датчика.
                                    string serialNumber = source.Sensor.SerialNumber;

                                    //  Получение индекса сигнала.
                                    int signalIndex = source.Signal;

                                    //  Отбор файлов, содержащих пакеты данных.
                                    IOrderedEnumerable<PackageFile> packageFiles = allPackageFiles
                                        .Where(packageFile => packageFile.Format == format &&
                                            packageFile.NormalizedBeginTime < source.EndTime &&
                                            packageFile.NormalizedEndTime > source.BeginTime)
                                        .OrderBy(packageFile => packageFile.NormalizedBeginTime);

                                    //  Получение канала температуры процессора в градусах цельсия.
                                    string channelName = $"Adxl{format:000}TCpu";

                                    //  Поиск канала.
                                    Frames.Channel? tempCpu = infoChannels.Where(channel => channel.Name == channelName).FirstOrDefault();

                                    //  Проверка канала.
                                    if (tempCpu is null)
                                    {
                                        //  Создание канала.
                                        tempCpu = new(channelName, "º", channelSampling, dbChannel.Cutoff, channelLength);

                                        //  Добавление канала в список.
                                        infoChannels.Add(tempCpu);
                                    }

                                    //  Получение канала температура датчика ADXL-357 в градусах цельсия.
                                    channelName = $"Adxl{format:000}TSensor";

                                    //  Поиск канала.
                                    Frames.Channel? tempSensor = infoChannels.Where(channel => channel.Name == channelName).FirstOrDefault();

                                    //  Проверка канала.
                                    if (tempSensor is null)
                                    {
                                        //  Создание канала.
                                        tempSensor = new(channelName, "º", channelSampling, dbChannel.Cutoff, channelLength);

                                        //  Добавление канала в список.
                                        infoChannels.Add(tempSensor);
                                    }

                                    //  Получение канала напряжение питания процессора в милливольтах.
                                    channelName = $"Adxl{format:000}VCpu";

                                    //  Поиск канала.
                                    Frames.Channel? voltageCpu = infoChannels.Where(channel => channel.Name == channelName).FirstOrDefault();

                                    //  Проверка канала.
                                    if (voltageCpu is null)
                                    {
                                        //  Создание канала.
                                        voltageCpu = new(channelName, "mV", channelSampling, dbChannel.Cutoff, channelLength);

                                        //  Добавление канала в список.
                                        infoChannels.Add(voltageCpu);
                                    }

                                    //  Получение канала напряжение питания процессора в милливольтах.
                                    channelName = $"Adxl{format:000}Sampling";

                                    //  Поиск канала.
                                    Frames.Channel? samplingSensor = infoChannels.Where(channel => channel.Name == channelName).FirstOrDefault();

                                    //  Проверка канала.
                                    if (samplingSensor is null)
                                    {
                                        //  Создание канала.
                                        samplingSensor = new(channelName, "Hz", channelSampling, dbChannel.Cutoff, channelLength);

                                        //  Добавление канала в список.
                                        infoChannels.Add(samplingSensor);
                                    }

                                    //  Перебор файлов, содержащих пакеты данных.
                                    foreach (PackageFile packageFile in packageFiles)
                                    {
                                        //  Проверка токена отмены.
                                        await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                        //  Определение количества пакетов.
                                        int packageCount = packageFile.PackageCount;

                                        //  Проверка количества пакетов.
                                        if (packageCount == 0)
                                        {
                                            //  Переход к следующему файлу.
                                            continue;
                                        }

                                        //  Определение частоты дискретизации пакетов.
                                        double packageSampling = packageFile.Sampling;

                                        //  Получение пути к файлу.
                                        string path = PathBuilder.Combine(
                                            packageFile.RawDirectory.Path,
                                            $"{packageFile.Time:yyyy-MM-dd-HH}",
                                            $"Adxl{packageFile.Format:000}",
                                            $"{packageFile.Time:yyyy-MM-dd-HH-mm-ss-fff}.adxl");

                                        //  Чтение массива данных из файла.
                                        byte[] bytes = await File.ReadAllBytesAsync(path, cancellationToken);

                                        //  Создание потока для чтения файла.
                                        using MemoryStream stream = new(bytes);

                                        //  Создание средства чтения двоичных данных.
                                        Spreader spreader = new(stream, Encoding.UTF8);

                                        //  Текущее положение в потоке.
                                        int position = 0;

                                        //  Длина синхронных сигналов.
                                        int length = 0;

                                        try
                                        {
                                            //  Чтение всех пакетов.
                                            while (position < stream.Length)
                                            {
                                                //  Проверка токена отмены.
                                                await Check.IsNotCanceledAsync(cancellationToken).ConfigureAwait(false);

                                                //  Чтение очередного пакета.
                                                Adxl357DataPackage dataPackage = await Adxl357DataPackage.LoadAsync(stream, cancellationToken);

                                                //  Определение времени начала записи пакета.
                                                DateTime beginPackageTime = packageFile.NormalizedBeginTime.AddSeconds(length / packageSampling);

                                                //  Корректировка длины синхронных сигналов.
                                                length += dataPackage.Length;

                                                //  Определение времени конца записи пакета.
                                                DateTime endPackageTime = packageFile.NormalizedBeginTime.AddSeconds(length / packageSampling);

                                                //  Определение начального индекса для записи в канал.
                                                int beginIndex = (int)Math.Round((beginPackageTime - beginTime).TotalSeconds * channelSampling);

                                                //  Определение следующего за последним индекса для записи в канал.
                                                int endIndex = (int)Math.Round((endPackageTime - beginTime).TotalSeconds * channelSampling);

                                                //  Получение длины сигнала из пакета.
                                                int signalLength = dataPackage.Length;

                                                //  Получение сигнала из пакета.
                                                Adxl357Signal signal = dataPackage.Signals[signalIndex];

                                                //  Коэффициент преобразования индексов.
                                                double indexFactor = signalLength / (double)(endIndex - beginIndex);

                                                //  Смещение при преобразовании индексов.
                                                double indexOffset = -indexFactor * beginIndex;

                                                //  Перебор индексов канала.
                                                for (int i = beginIndex; i < endIndex; i++)
                                                {
                                                    //  Проверка вхождения индекса в канал.
                                                    if (i < 0 || i >= channelLength)
                                                    {
                                                        //  Переход к следующему индексу.
                                                        continue;
                                                    }

                                                    //  Определение индекса в сигнале.
                                                    double floatSignalIndex = indexFactor * i + indexOffset;

                                                    //  Определение первого индеска в сигнале.
                                                    int firstSignalIndex = (int)Math.Floor(floatSignalIndex);

                                                    //  Определение второго индекса в сигнале.
                                                    int secondSignalIndex = firstSignalIndex + 1;

                                                    //  Проверка первого индекса в сигнале.
                                                    if (firstSignalIndex < 0)
                                                    {
                                                        //  Проверка второго индекса в сигнале.
                                                        if (0 <= secondSignalIndex && secondSignalIndex < signalLength)
                                                        {
                                                            //  Установка значения канала по второму индексу.
                                                            channel[i] = signal[secondSignalIndex];
                                                        }
                                                    }
                                                    else if (firstSignalIndex < signalLength)
                                                    {
                                                        //  Проверка второго индекса в сигнале.
                                                        if (secondSignalIndex < signalLength)
                                                        {
                                                            //  Получение значений сигнала.
                                                            double firstValue = signal[firstSignalIndex];
                                                            double secondValue = signal[secondSignalIndex];

                                                            //  Коэффициент преобразования.
                                                            double factor = secondValue - firstValue;

                                                            //  Смещение при преобразовании.
                                                            double offset = firstValue - factor * firstSignalIndex;

                                                            //  Расчёт значения.
                                                            double value = factor * floatSignalIndex + offset;

                                                            //  Установка значения канала.
                                                            channel[i] = value;
                                                        }
                                                        else
                                                        {
                                                            //  Установка значения канала по первому индексу.
                                                            channel[i] = signal[firstSignalIndex];
                                                        }
                                                    }
                                                    //  Установка значений информационных каналов.
                                                    tempCpu[i] = dataPackage.AsyncValues[0];
                                                    tempSensor[i] = dataPackage.AsyncValues[1];
                                                    voltageCpu[i] = dataPackage.AsyncValues[2];
                                                    samplingSensor[i] = packageSampling;
                                                }

                                                //  Определение положения следующего пакета.
                                                position = (int)stream.Position;
                                            }
                                        }
                                        catch (InvalidDataException)
                                        {
                                            //  Переход к следующему файлу.
                                            continue;
                                        }
                                    }
                                }
                            }

                            //  Запрос геолокационных данных.
                            Geolocation[] geolocations = await database.Geolocations
                                .Where(geolocation => geolocation.RegistrarId == registrar.Id &&
                                Geolocation.ToTimestamp(beginTime.AddSeconds(-10)) <= geolocation.Timestamp &&
                                geolocation.Timestamp <= Geolocation.ToTimestamp(endTime.AddSeconds(10)))
                                .ToArrayAsync(cancellationToken).ConfigureAwait(false);

                            //  Перебор геолокационных данных.
                            foreach (Geolocation geolocation in geolocations)
                            {
                                //  Установка флага анализа.
                                //  Не выносить код из транзакции!!!
                                geolocation.IsAnalyzed = true;
                            }

                            //  Создание геолокационных каналов.
                            Frames.Channel gpsSpeed = new("V_GPS", "kph", 1, 1, timeStep);
                            Frames.Channel gpsLatitude = new("Lat_GPS", "º", 1, 1, timeStep);
                            Frames.Channel gpsLongitude = new("Lon_GPS", "º", 1, 1, timeStep);
                            Frames.Channel gpsAltitude = new("A_GPS", "m", 1, 1, timeStep);
                            Frames.Channel gpsTime = new("dTime_GPS", "s", 1, 1, timeStep);
                            Frames.Channel gpsPoleCourse = new("Pole_GPS", "º", 1, 1, timeStep);
                            Frames.Channel gpsMagneticCourse = new("Magnetic_GPS", "º", 1, 1, timeStep);
                            Frames.Channel gpsSolution = new("Solution_GPS", "", 1, 1, timeStep);
                            Frames.Channel gpsSatellites = new("Sat_GPS", "", 1, 1, timeStep);
                            Frames.Channel gpsHdop = new("Hdop_GPS", "", 1, 1, timeStep);
                            Frames.Channel gpsGeoidal = new("Geoidal_GPS", "", 1, 1, timeStep);
                            Frames.Channel gpsAge = new("Age_GPS", "", 1, 1, timeStep);
                            Frames.Channel gpsKnots = new("Knots_GPS", "kn", 1, 1, timeStep);

                            //  Перебор индексов геолокационных каналов.
                            for (int i = 0; i < timeStep; i++)
                            {
                                //  Определение текущей метки времени.
                                long timestamp = Geolocation.ToTimestamp(beginTime.AddSeconds(i));

                                //  Установка значений геолокационных каналов.
                                gpsSpeed[i] = getValue(geolocation => geolocation.Speed, 0);
                                gpsLatitude[i] = getValue(geolocation => geolocation.Latitude, 0);
                                gpsLongitude[i] = getValue(geolocation => geolocation.Longitude, 0);
                                gpsAltitude[i] = getValue(geolocation => geolocation.Altitude, 0);
                                gpsTime[i] = (getValue(geolocation => geolocation.Time, beginTime.AddSeconds(i)) - beginTime.AddSeconds(i)).TotalSeconds;
                                gpsPoleCourse[i] = getValue(geolocation => geolocation.PoleCourse, 0);
                                gpsMagneticCourse[i] = getValue(geolocation => geolocation.MagneticCourse, 0);
                                gpsSolution[i] = getValue(geolocation => (int?)geolocation.Solution, 0);
                                gpsSatellites[i] = getValue(geolocation => geolocation.Satellites, 0);
                                gpsHdop[i] = getValue(geolocation => geolocation.Hdop, 0);
                                gpsGeoidal[i] = getValue(geolocation => geolocation.Geoidal, 0);
                                gpsAge[i] = getValue(geolocation => geolocation.Age, 0);
                                gpsKnots[i] = getValue(geolocation => geolocation.Knots, 0);

                                T getValue<T>(Func<Geolocation, T?> selector, T defaultValue)
                                    where T : struct
                                {
                                    var result = geolocations
                                        .Select(geolocation => new
                                        {
                                            Value = selector(geolocation),
                                            DeltaTimestamp = Math.Abs(geolocation.Timestamp - timestamp)
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

                            //  Добавление каналов в кадр.
                            frame.Channels.AddRange(mainChannels);
                            frame.Channels.AddRange(new Frames.Channel[]
                            {
                                gpsSpeed,
                                gpsLatitude,
                                gpsLongitude,
                                gpsAltitude,
                                gpsTime,
                                gpsPoleCourse,
                                gpsMagneticCourse,
                                gpsSolution,
                                gpsSatellites,
                                gpsHdop,
                                gpsGeoidal,
                                gpsAge,
                                gpsKnots,
                            });
                            frame.Channels.AddRange(infoChannels);

                            //  Запрос скоростей.
                            double[] allSpeeds = await database.Geolocations
                                .Where(geolocation => geolocation.RegistrarId == registrar.Id &&
                                    Geolocation.ToTimestamp(beginTime) <= geolocation.Timestamp &&
                                    geolocation.Timestamp <= Geolocation.ToTimestamp(endTime) &&
                                    geolocation.Speed.HasValue)
                                .Select(geolocation => geolocation.Speed!.Value)
                                .ToArrayAsync(cancellationToken).ConfigureAwait(false);

                            //  Определение средней скорости.
                            double speed = allSpeeds.Length > 0 ? allSpeeds.Average() : 0;

                            //  Имя файла.
                            string name = $"Vp{speed:000}_0{extension}";

                            //  Определение пути к файлу.
                            string framePath = PathBuilder.Combine(directoryPath, name);

                            //  Сохранение кадра.
                            frame.Save(framePath, Frames.StorageFormat.TestLab);

                            //  Установка времен файла.
                            File.SetCreationTime(framePath, endTime);
                            File.SetLastAccessTime(framePath, endTime);
                            File.SetLastWriteTime(framePath, endTime);

                            //  Проверка кадра в базе данных.
                            Frame? dbFrame = await database.Frames.FindAsync(
                                new object?[] { registrar.Id, frameTimestamp },
                                cancellationToken).ConfigureAwait(false);

                            //  Проверка кадра.
                            if (dbFrame is not null)
                            {
                                //  Удаление кадра.
                                database.Frames.Remove(dbFrame);
                            }

                            //  Добавление записи в базу данных.
                            database.Frames.Add(new()
                            {
                                RegistrarId = registrar.Id,
                                Timestamp = frameTimestamp,
                                IsSpectrum = false,
                                Time = beginTime,
                                Number = frameNumber,
                                Path = framePath,
                            });
                        }, cancellationToken).ConfigureAwait(false);
                    },
                    cancellationToken).ConfigureAwait(false);
             }).ConfigureAwait(false);
    }

    /// <summary>
    /// Определяет границы поиска.
    /// </summary>
    /// <param name="channels">
    /// Коллекция каналов.
    /// </param>
    /// <returns>
    /// Границы поиска.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static (DateTime beginTime, DateTime endTime) GetTimeBoundaries(IEnumerable<Channel> channels)
    {
        //  Получение всех источников.
        Source[] sources = channels.SelectMany(channel => channel.Sources).ToArray();

        //  Определение времени для начала поиска.
        DateTime beginTime = sources.Select(source => source.BeginTime).OrderBy(beginTime => beginTime).FirstOrDefault();

        //  Определение времени для конца поиска.
        DateTime endTime = sources.Select(source => source.EndTime).OrderBy(endTime => endTime).LastOrDefault();

        //  Нормализация времени начала поиска.
        beginTime = new(beginTime.Year, beginTime.Month, beginTime.Day, beginTime.Hour, beginTime.Minute, 0);

        //  Нормализация времени конца поиска.
        endTime = new DateTime(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, endTime.Minute, 0).AddMinutes(1);

        //  Возврат границ поиска.
        return (beginTime, endTime);
    }
}
