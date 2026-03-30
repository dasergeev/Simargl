using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Simargl.Analysis;
using Simargl.Projects.Oriole.Oriole01Storage;
using Simargl.Projects.Oriole.Oriole01Storage.Entities;
using Simargl.Recording.AccelEth3T;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Channels;

namespace Simargl.Projects.Oriole.Oriole01Adxl;

/// <summary>
/// Представляет основной фоновый процесс.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public sealed class Worker(ILogger<Worker> logger) :
    BackgroundService
{
    /// <summary>
    /// Поле для хранения периода сканирования в миллисекундах.
    /// </summary>
    private const int _Period = 60 * 60 * 1000;

    /// <summary>
    /// Предоставляет точку входа фонового процесса.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, представляющая точку входа фонового процесса.
    /// </returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл поддержки.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Блок перехвата всех исключений.
            try
            {
                //  Выполнение основной работы.
                await InvokeAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                //  Проверка токена отмены.
                if (!cancellationToken.IsCancellationRequested)
                {
                    //  Вывод информации в журнал.
                    logger.LogError("{ex}", ex);
                }
            }

            //  Ожидание перед следующим проходом.
            await Task.Delay(1000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    private async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл выполнения.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Подключение к базе данных.
            using Oriole01StorageContext context = new();
            logger.LogInformation("Выполнено подключение к базе данных.");

            //  Получение путей.
            string[] rootPaths = await context.Paths
                .OrderBy(x => x.Priority)
                .Select(x => x.FullPath)
                .ToArrayAsync(cancellationToken).ConfigureAwait(false);

            //  Перебор путей.
            foreach (string rootPath in rootPaths)
            {
                //  Проверка пути.
                if (!Directory.Exists(rootPath))
                {
                    //  Переход к следующему пути.
                    continue;
                }

                //  Создание списка задач.
                List<Task> tasks = [];

                //  Формирование списка задач.
                for (int i = 0; i < 10; i++)
                {
                    tasks.Add(Task.Run(async delegate
                    {
                        //  Основной цикл обработки.
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            //  Подключение к базе данных.
                            using Oriole01StorageContext local = new();

                            //  Поиск необработанного файла.
                            AdxlFileData? adxlFile = await local.AdxlFiles
                                    .Where(x => x.State == AdxlFileState.Registered)
                                    .Include(x => x.HourDirectory)
                                    .Include(x => x.Adxl)
                                    .OrderBy(x => EF.Functions.Random())
                                    .FirstOrDefaultAsync(cancellationToken)
                                    .ConfigureAwait(false);

                            //  Проверка файла.
                            if (adxlFile is null)
                            {
                                //  Необработанных файлов не осталось.
                                break;
                            }

                            //  Получение времени файла.
                            DateTime fileTime = new(adxlFile.Timestamp);

                            //  Получение пути к файлу.
                            string path = Path.Combine(
                                    rootPath,
                                    adxlFile.HourDirectory.GetName(),
                                    $"Adxl-{adxlFile.Adxl.GetIPAddress()}",
                                    $"Adxl-{adxlFile.Adxl.GetIPAddress()}-{fileTime.Year:0000}-{fileTime.Month:00}-{fileTime.Day:00}-{fileTime.Hour:00}-{fileTime.Minute:00}-{fileTime.Second:00}-{fileTime.Millisecond:000}.adxl");

                            //  Проверка файла.
                            if (!File.Exists(path))
                            {
                                //  Переход к следующему файлу.
                                continue;
                            }

                            //  Блок перехвата всех исключений.
                            try
                            {
                                //  Запрос данных.
                                AdxlFileData? data = await local.AdxlFiles.FirstOrDefaultAsync(
                                        x => x.Key == adxlFile.Key, cancellationToken)
                                        .ConfigureAwait(false);

                                //  Проверка данных.
                                if (data is null)
                                {
                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Замена данных.
                                adxlFile = data;

                                //  Получение данных.
                                byte[] buffer = await File.ReadAllBytesAsync(path, cancellationToken).ConfigureAwait(false);

                                //  Создание средства чтения данных.
                                using MemoryStream stream = new(buffer);

                                //  Список пакетов.
                                List<AccelEth3TDataPackage> packages = [];

                                //  Разбор потока.
                                while (!cancellationToken.IsCancellationRequested)
                                {
                                    try
                                    {
                                        //  Чтение пакета.
                                        packages.Add(await AccelEth3TDataPackage.LoadAsync(stream, cancellationToken).ConfigureAwait(false));
                                    }
                                    catch (EndOfStreamException)
                                    {
                                        //  Заверешение разбора.
                                        break;
                                    }
                                }

                                //  Проверка количества пакетов.
                                if (packages.Count != 0)
                                {

                                    //  Определение длины канала.
                                    int length = 50 * packages.Count;

                                    //  Корректировка времени.
                                    DateTime time = fileTime.AddSeconds(-length / 2000.0);
                                    time = new(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);

                                    //  Создание каналов.
                                    Frames.Channel[] channels = [
                                        new(string.Empty, string.Empty, 2000, 500, length),
                                    new(string.Empty, string.Empty, 2000, 500, length),
                                    new(string.Empty, string.Empty, 2000, 500, length),
                                    ];

                                    //  Перебор пакетов.
                                    for (int packgeIndex = 0; packgeIndex < packages.Count; packgeIndex++)
                                    {
                                        //  Получение пакета.
                                        AccelEth3TDataPackage package = packages[packgeIndex];

                                        //  Перебор каналов.
                                        for (int channelIndex = 0; channelIndex < 3; channelIndex++)
                                        {
                                            //  Получение канала.
                                            Frames.Channel channel = channels[channelIndex];

                                            //  Получение сигнала.
                                            AccelEth3TSignal signal = package.Signals[channelIndex];

                                            //  Перебор значений.
                                            for (int valueIndex = 0; valueIndex < 50; valueIndex++)
                                            {
                                                //  Установка значения канала.
                                                channel[50 * packgeIndex + valueIndex] = signal[valueIndex];
                                            }
                                        }
                                    }

                                    //  Перебор каналов.
                                    for (int channelIndex = 0; channelIndex < 3; channelIndex++)
                                    {
                                        //  Получение канала.
                                        Frames.Channel channel = channels[channelIndex];

                                        //  Получение спектра.
                                        Spectrum spectrum = new(channel.Signal);

                                        //  Определение максимального индекса.
                                        int maxIndex = (int)Math.Ceiling(0.1 / spectrum.FrequencyStep);

                                        //  Перебор частот.
                                        for (int index = 0; index <= maxIndex; index++)
                                        {
                                            //  Зануление частоты.
                                            spectrum[index] = 0;
                                        }

                                        //  Восстановление канала.
                                        spectrum.Restore(channel.Signal);
                                    }

                                    //  Перебор частот.
                                    foreach (double frequency in new double[] { 100, 80, 40, 20 })
                                    {
                                        //  Создание фильтра.
                                        Analysis.Transforms.ButterworthFilter filter = new(frequency, 4);

                                        //  Перебор каналов.
                                        for (int channelIndex = 0; channelIndex < 3; channelIndex++)
                                        {
                                            //  Получение канала.
                                            Frames.Channel channel = channels[channelIndex];

                                            //  Фильтрация.
                                            filter.Invoke(channel.Signal);

                                            //  Параметры.
                                            double sum = 0;
                                            double squaresSum = 0;
                                            double min = double.MaxValue;
                                            double max = double.MinValue;
                                            int count = 0;

                                            //  Перебор значений.
                                            foreach (double value in channel.Items)
                                            {
                                                //  Корректирочка значений.
                                                sum += value;
                                                squaresSum += value * value;
                                                min = Math.Min(min, value);
                                                max = Math.Max(max, value);
                                                ++count;
                                            }

                                            //  Блок перехвата всех исключений.
                                            try
                                            {
                                                //  Подключение к базе данных.
                                                using Oriole01StorageContext localContext = new();

                                                //  Запрос данных.
                                                ChannelData? channelData = await localContext.Channels
                                                    .FirstOrDefaultAsync(x =>
                                                        x.Timestamp == time.Ticks &&
                                                        x.AdxlKey == adxlFile.AdxlKey &&
                                                        x.Index == channelIndex &&
                                                        x.Frequency == frequency, cancellationToken).ConfigureAwait(false);

                                                if (channelData is null)
                                                {
                                                    //  Создание данных.
                                                    channelData = new()
                                                    {
                                                        Timestamp = time.Ticks,
                                                        AdxlKey = adxlFile.AdxlKey,
                                                        Index = channelIndex,
                                                        Frequency = frequency,
                                                    };

                                                    //  Добавление данных.
                                                    localContext.Channels.Add(channelData);
                                                }

                                                //  Запись данных.
                                                channelData.Sum = sum;
                                                channelData.SquaresSum = squaresSum;
                                                channelData.Min = min;
                                                channelData.Max = max;
                                                channelData.Count = count;

                                                //  Сохранение изменений.
                                                await localContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                            }
                                            catch (DbUpdateException)
                                            {

                                            }
                                        }
                                    }
                                }

                                //  Установка состояния.
                                adxlFile.State = AdxlFileState.Parsed;

                                //  Сохранение изменений.
                                await local.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                                Console.WriteLine($"{path}: {packages.Count}");
                            }
                            catch { throw; }
                        }
                    }, cancellationToken));
                }

                //  Ожидание выполнения задач.
                await Task.WhenAll(tasks).ConfigureAwait(false);
            }

            //  Ожидание перед следующим сканированием.
            await Task.Delay(_Period, cancellationToken).ConfigureAwait(false);
        }
    }
}
