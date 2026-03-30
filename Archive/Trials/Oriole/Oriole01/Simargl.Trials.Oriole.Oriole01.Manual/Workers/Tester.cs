#define USED_MAX

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Simargl.Analysis;
using Simargl.Frames;
using Simargl.Frames.OldStyle;
using Simargl.Projects.Oriole.Oriole01Storage;
using Simargl.Projects.Oriole.Oriole01Storage.Entities;
using Simargl.Recording.AccelEth3T;
using Simargl.Trials.Oriole.Oriole01.Manual.Basis;
using Simargl.Trials.Oriole.Oriole01.Manual.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace Simargl.Trials.Oriole.Oriole01.Manual.Workers;

/// <summary>
/// Представляет тестовый процесс.
/// </summary>
public sealed class Tester(ILogger<Tester> logger) :
    Worker<Tester>(logger)
{

    /// <summary>
    /// Асинхронно выполняет основную работу.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая основную работу.
    /// </returns>
    protected override sealed async Task InvokeAsync(CancellationToken cancellationToken)
    {
        await MinMaxAsync(cancellationToken).ConfigureAwait(false);
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
    public static async Task MinMaxAsync(CancellationToken cancellationToken)
    {
        //  Параметры.
        const string targetName = "МY1"; //МY1, ТвнY2
        const double frequency = 100;
        const int count = 50;
        const string outputPath = "D:\\Environs\\Data\\Simargl.Trials.Oriole.Oriole01.Manual";
        const string recordsDir = outputPath +  "Records";

        const string indicator =
#if USED_MAX
            "Max"
#else
            "Min"
#endif
        ;

        Directory.CreateDirectory(recordsDir);

        //  Получение данных о канале.
        (long key, int axis, double direct) = await StorageHelper.FindAdxlAsync(
            targetName, cancellationToken).ConfigureAwait(false);

        //  Подключение к базе данных.
        await using Oriole01StorageContext context = new();

        //  Получение имён каналов.
        string[] names = await context.ChannelNames
            .OrderBy(x => x.Key)
            .Select(x => x.Name)
            .ToArrayAsync(cancellationToken)
            .ConfigureAwait(false);

        //  Получение пути к корневому каталогу.
        string rootPath = (await context.Paths
                .OrderBy(x => x.Priority)
                .Select(x => x.FullPath)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false))
                .Where(Directory.Exists)
                .FirstOrDefault() ?? throw new InvalidOperationException("Не найден корневой каталог.");

        StringBuilder filesOutput = new();
        filesOutput.Append("indicator"); filesOutput.Append(';');
        filesOutput.Append("targetName"); filesOutput.Append(';');
        filesOutput.Append("frequency"); filesOutput.Append(';');
        filesOutput.Append("Time"); filesOutput.Append(';');
        filesOutput.Append("Timestamp"); filesOutput.Append(';');
        filesOutput.Append("Sum"); filesOutput.Append(';');
        filesOutput.Append("SquaresSum"); filesOutput.Append(';');
        filesOutput.Append("Max"); filesOutput.Append(';');
        filesOutput.Append("Min"); filesOutput.Append(';');
        filesOutput.Append("Count"); filesOutput.Append(';');
        filesOutput.Append("Speed"); filesOutput.Append(';');
        filesOutput.Append("Latitude"); filesOutput.Append(';');
        filesOutput.Append("Longitude"); filesOutput.Append(';');
        filesOutput.Append("name"); filesOutput.Append(';');
        filesOutput.Append("key"); filesOutput.Append(';');
        filesOutput.Append("axis"); filesOutput.Append(';');
        filesOutput.Append("direct"); filesOutput.Append(';');
        filesOutput.Append("path");
        filesOutput.AppendLine();


        StringBuilder output = new();
        output.Append("Time;Speed;Latitude;Longitude");

        foreach (string name in names)
        {
            output.Append($";{name}.Min");
            output.Append($";{name}.Max");
            output.Append($";{name}.Average");
            output.Append($";{name}.Deviation");
        }
        output.AppendLine();

        void write(double value) => output.Append($"{value}"/*.Replace(',', '.')*/);

        //  Поиск значений.
        await foreach (var item in 
            context.Channels
                .Where(x =>
                    x.AdxlKey == key &&
                    x.Index == axis &&
                    x.Frequency == frequency)
                .Select(x => new
                {
                    Time = x.Timestamp / TimeSpan.TicksPerSecond,
                    Channel = x,
                })
                .Join(
                    context.Nmeas
                        .Select(
                            x => new
                            {
                                Time = x.Timestamp / TimeSpan.TicksPerSecond,
                                Nmea = x,
                            }),
                    x => x.Time,
                    x => x.Time,
                    (x, y) => new
                    {
                        x.Time,
                        x.Channel.Timestamp,
                        x.Channel.Sum,
                        x.Channel.SquaresSum,
                        x.Channel.Max,
                        x.Channel.Min,
                        x.Channel.Count,
                        y.Nmea.Speed,
                        y.Nmea.Latitude,
                        y.Nmea.Longitude,
                    }
                )
#if USED_MAX
                .OrderBy(x => -x.Max)   //  НАСТРОЙКА
#else
                .OrderBy(x => x.Min)   //  НАСТРОЙКА
#endif
                .Take(count)
                .AsAsyncEnumerable()
                .WithCancellation(cancellationToken))
        {
            write(new DateTime(item.Timestamp).ToOADate());
            output.Append(';');
            write(item.Speed);
            output.Append(';');
            write(item.Latitude);
            output.Append(';');
            write(item.Longitude);

            //  Получение метки времени.
            long timestamp = item.Timestamp + 11 * TimeSpan.TicksPerSecond;// item.Time * TimeSpan.TicksPerSecond;

            //  Подключение к базе данных.
            await using Oriole01StorageContext secondContext = new();

            //  Получение информации о файлах.
            Dictionary<long, AdxlFileData?> fileMap = await secondContext.AdxlFiles
                .Where(x => x.Timestamp <= timestamp)
                .Include(x => x.HourDirectory)
                .Include(x => x.Adxl)
                .GroupBy(x => x.AdxlKey)
                .Select(x => new
                {
                    x.Key,
                    Value = x.OrderBy(x => -x.Timestamp)
                        .FirstOrDefault(),
                })
                .ToDictionaryAsync(
                    x => x.Key,
                    x => x.Value,
                    cancellationToken)
                .ConfigureAwait(false);

            //  Карта каналов.
            Dictionary<long, Channel[]> channelMap = [];

            Console.WriteLine($"{targetName}: {new DateTime(timestamp)}, {item.Max}, {item.Speed}:");

            Frame frame = new();

            //  Перебор имён каналов.
            foreach (string name in names)
            {
                //  Получение данных о канале.
                (key, axis, direct) = await StorageHelper.FindAdxlAsync(
                    name, cancellationToken).ConfigureAwait(false);

                //  Получение канала.
                Channel? channel = await getChannel(key, axis, direct).ConfigureAwait(false);

                if (channel is not null)
                {
                    Channel original = channel.Clone();
                    original.Name = name + "_A";
                    original.Sampling = 2000;
                    original.Cutoff = 1000;
                    frame.Channels.Add(original);

                    Channel speed = channel.Clone();
                    speed.Name = name + "_V";
                    speed.Sampling = 2000;
                    speed.Cutoff = 1000;
                    frame.Channels.Add(speed);

                    Channel move = channel.Clone();
                    move.Name = name + "_S";
                    move.Sampling = 2000;
                    move.Cutoff = 1000;
                    frame.Channels.Add(move);

                    integrate(speed);
                    integrate(move);
                    integrate(move);

                    static void integrate(Channel channel)
                    {
                        //  Получение спектра.
                        Spectrum spectrum = new(channel.Signal);

                        spectrum[0] = 0;
                        for (int i = 1; i < spectrum.Count; i++)
                        {
                            spectrum[i] /= 2 * Math.PI * i * spectrum.FrequencyStep;
                        }

                        //  Восстановление канала.
                        spectrum.Restore(channel.Signal);
                    }


                    filtrate(channel, frequency);

                    static void filtrate(Channel channel, double frequency)
                    {
                        //  Создание фильтра.
                        Analysis.Transforms.ButterworthFilter filter = new(frequency, 4);

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

                        //  Фильтрация.
                        filter.Invoke(channel.Signal);
                    }



                    //  Параметры.
                    double sum = 0;
                    double squaresSum = 0;
                    double min = double.MaxValue;
                    double max = double.MinValue;
                    int n = 0;

                    //  Перебор значений.
                    foreach (double value in channel.Items)
                    {
                        //  Корректирочка значений.
                        sum += value;
                        squaresSum += value * value;
                        min = Math.Min(min, value);
                        max = Math.Max(max, value);
                        ++n;
                    }

                    channel.Name = name;
                    channel.Sampling = 2000;
                    channel.Cutoff = 1000;
                    frame.Channels.Add(channel);

                    double average = sum / n;
                    double deviation = Math.Sqrt((squaresSum - sum * sum / n) / (n - 1));

                    output.Append(';');
                    write(min);
                    output.Append(';');
                    write(max);
                    output.Append(';');
                    write(average);
                    output.Append(';');
                    write(deviation);
                }

                async Task<Channel?> getChannel(long key, int axis, double direct)
                {
                    //  Проверка карты каналов.
                    if (channelMap.TryGetValue(key, out Channel[]? channels))
                    {
                        //  Возврат канала.
                        return channels[axis];
                    }

                    //  Проверка возможности загрузки данных.
                    if (fileMap.TryGetValue(key, out AdxlFileData? adxlFile) &&
                        adxlFile is not null)
                    {
                        //  Получение времени файла.
                        DateTime fileTime = new(adxlFile.Timestamp);

                        //  Получение пути к файлу.
                        string path = Path.Combine(
                                rootPath,
                                adxlFile.HourDirectory.GetName(),
                                $"Adxl-{adxlFile.Adxl.GetIPAddress()}",
                                $"Adxl-{adxlFile.Adxl.GetIPAddress()}-{fileTime.Year:0000}-{fileTime.Month:00}-{fileTime.Day:00}-{fileTime.Hour:00}-{fileTime.Minute:00}-{fileTime.Second:00}-{fileTime.Millisecond:000}.adxl");

                        filesOutput.Append(indicator); filesOutput.Append(';');
                        filesOutput.Append(targetName); filesOutput.Append(';');
                        filesOutput.Append(frequency); filesOutput.Append(';');
                        filesOutput.Append(item.Time); filesOutput.Append(';');
                        filesOutput.Append(item.Timestamp); filesOutput.Append(';');
                        filesOutput.Append(item.Sum); filesOutput.Append(';');
                        filesOutput.Append(item.SquaresSum); filesOutput.Append(';');
                        filesOutput.Append(item.Max); filesOutput.Append(';');
                        filesOutput.Append(item.Min); filesOutput.Append(';');
                        filesOutput.Append(item.Count); filesOutput.Append(';');
                        filesOutput.Append(item.Speed); filesOutput.Append(';');
                        filesOutput.Append(item.Latitude); filesOutput.Append(';');
                        filesOutput.Append(item.Longitude); filesOutput.Append(';');
                        filesOutput.Append(name); filesOutput.Append(';');
                        filesOutput.Append(key); filesOutput.Append(';');
                        filesOutput.Append(axis); filesOutput.Append(';');
                        filesOutput.Append(direct); filesOutput.Append(';');
                        filesOutput.Append(path);
                        filesOutput.AppendLine();

                        //  Проверка файла.
                        if (!File.Exists(path))
                        {
                            //  Канал не найден.
                            return null;
                        }

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
                            channels = [
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
                                    Channel channel = channels[channelIndex];

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

                            ////  Перебор каналов.
                            //for (int channelIndex = 0; channelIndex < 3; channelIndex++)
                            //{
                            //    //  Получение канала.
                            //    Channel channel = channels[channelIndex];

                            //    //  Получение спектра.
                            //    Spectrum spectrum = new(channel.Signal);

                            //    //  Определение максимального индекса.
                            //    int maxIndex = (int)Math.Ceiling(0.1 / spectrum.FrequencyStep);

                            //    //  Перебор частот.
                            //    for (int index = 0; index <= maxIndex; index++)
                            //    {
                            //        //  Зануление частоты.
                            //        spectrum[index] = 0;
                            //    }

                            //    //  Восстановление канала.
                            //    spectrum.Restore(channel.Signal);
                            //}

                            //  Перебор каналов.
                            for (int channelIndex = 0; channelIndex < 3; channelIndex++)
                            {
                                //  Получение канала.
                                Channel channel = channels[channelIndex];

                                //  Учёт направления.
                                channel.Scale(direct);
                            }

                            //  Добавление в карту.
                            channelMap.Add(key, channels);

                            //  Возврат канала.
                            return channels[axis];
                        }
                    }

                    //  Канал не найден.
                    return null;
                }


                Console.WriteLine($"    {name}: {(channel is not null ? channel.Max() : "-")} | {fileMap[key]?.Timestamp} - {timestamp}");
            }

            output.AppendLine();

            double valueLabel;
            string textLabel;

#if USED_MAX
            valueLabel = item.Max;
            textLabel = "Max";
#else
            valueLabel = item.Min;
            textLabel = "Min";
#endif
            string frameName = $"Vp000_0 {targetName} {textLabel} = {valueLabel.ToString().Replace(',', '.')}.0001";
            Console.WriteLine(frameName);
            frame.Save(Path.Combine(recordsDir, frameName), StorageFormat.TestLab);

            StringBuilder csv = new();
            foreach (Channel channel in frame.Channels)
            {
                csv.Append(channel.Name);
                csv.Append(';');
            }
            csv.AppendLine();

            for (int i = 0; i < frame.Channels[0].Length; i++)
            {
                foreach (Channel channel in frame.Channels)
                {
                    csv.Append(channel[i]);
                    csv.Append(';');
                }
                csv.AppendLine();
            }

            frameName += ".csv";
            await File.WriteAllTextAsync(Path.Combine(recordsDir, frameName), csv.ToString(), cancellationToken).ConfigureAwait(false);

            //foreach (var pair in fileMap)
            //{
            //    if (pair.Value is not null)
            //    {
            //        Console.WriteLine($"    {pair.Key}: {pair.Value.Timestamp}");
            //    }
            //}

        }
        await File.WriteAllTextAsync(
            Path.Combine(outputPath, 
            $"{targetName}-" + indicator + ".csv"),
            output.ToString(), cancellationToken).ConfigureAwait(false);

        await File.WriteAllTextAsync(
            Path.Combine(outputPath,
            $"Files {targetName}-" + indicator + ".csv"),
            filesOutput.ToString(), cancellationToken).ConfigureAwait(false);

        //

        Console.WriteLine();
        Console.WriteLine("ЗАВЕРШЕНО");

        //output
    }
}
