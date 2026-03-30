using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Simargl.Trials.Aurora.Aurora01.Storage;
using Simargl.Trials.Aurora.Aurora01.Storage.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Simargl.Trials.Aurora.Aurora01.Analyzer;

/// <summary>
/// Представляет основной фоновый процесс.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public partial class Worker(ILogger<Worker> logger) :
    BackgroundService
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
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Ожидание инициализации консоли.
        await Task.Delay(1000, cancellationToken).ConfigureAwait(false);

        //  Блок перехвата всех исключений.
        try
        {
            //  Вывод информации в журнал.
            logger.LogInformation("Начало анализа.");

            //  Создание источника токена отмены.
            using CancellationTokenSource linkedTokenSource =
                CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            //  Получение токена отмены.
            CancellationToken linkedToken = linkedTokenSource.Token;

            //  Выполнение основной работы.
            await InvokeAsync(linkedToken).ConfigureAwait(false);

            //  Вывод информации в журнал.
            logger.LogInformation("Завершение анализа.");
        }
        catch (Exception ex)
        {
            //  Проверка токена отмены.
            if (!cancellationToken.IsCancellationRequested)
            {
                //  Вывод сообщения в журнал.
                logger.LogError("{ex}", ex);
            }
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
    public async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Загрузка карты Nmea.
        NmeaMap nmeaMap = await NmeaMap.LoadAsync(cancellationToken).ConfigureAwait(false);

        //  Вывод информации в журнал.
        logger.LogInformation("Загружена карта NMEA: {map}", nmeaMap);

        //  Создание коллекции результатов.
        var results = Enumerable.Range(0, 32)
            .ToDictionary(
                x => x,
                x => Aurora01Tunings.Frequencies
                    .ToDictionary(
                        x => x,
                        x => new List<(
                            DateTime Time,
                            double Sum,
                            double SquaresSum,
                            double Min,
                            double Max,
                            int Count,
                            double Speed,
                            double Latitude,
                            double Longitude,
                            double Zero)>()));

        //  Перебор карты.
        foreach (NmeaFragment nmeaFragment in nmeaMap.NmeaFragments)
        {
            //  Начало и завершение фрагмента определения нуля.
            DateTime firstZeroTime = nmeaFragment.FirstTime;
            DateTime lastZeroTime = nmeaFragment.FirstTime;

            //  Перебор времени.
            for (DateTime time = nmeaFragment.FirstTime; time <= nmeaFragment.LastTime; time = time.AddSeconds(1))
            {
                //  Проверка скорости.
                if (nmeaFragment[time].Speed != 0)
                {
                    //  Завершение прохода.
                    break;
                }

                //  Установка времени завершения фрагмента опеределения нуля.
                lastZeroTime = time;
            }

            //  Проверка не нулевой скорости.
            if (lastZeroTime == nmeaFragment.LastTime)
            {
                //  Переход к следующему фрагменту.
                continue;
            }

            //  Подключение к базе данных.
            using Aurora01StorageContext context = new();

            //  Определение меток времени для поиска кадров для определения нуля.
            long firstZeroTimestamp = firstZeroTime.Ticks;
            long lastZeroTimestamp = lastZeroTime.AddMinutes(-1).Ticks;

            //  Поиск кадров для определения нуля.
            Dictionary<int, Dictionary<double, RawFrameChannelData[]>> zeroData = await context
                .RawFrameChannels
                .Where(x => x.Timestamp >= firstZeroTimestamp && x.Timestamp <= lastZeroTimestamp)
                .GroupBy(x => x.Index)
                .ToDictionaryAsync(x => x.Key,
                    x => x
                        .GroupBy(x => x.Frequency)
                        .ToDictionary(x => x.Key, x => x.ToArray()),
                    cancellationToken)
                .ConfigureAwait(false);

            //  Флаг действительных данных.
            bool isValid = true;

            //  Перебор основных каналов.
            for (int i = 0; i < 32; i++)
            {
                //  Проверка флага.
                if (!isValid)
                {
                    //  Завершение проверки.
                    break;
                }

                //  Проверка данных.
                if (!zeroData.TryGetValue(i, out Dictionary<double, RawFrameChannelData[]>? map))
                {
                    //  Сброс флага.
                    isValid = false;

                    //  Завершение проверки.
                    break;
                }

                //  Перебор частот.
                foreach (double frequency in Aurora01Tunings.Frequencies)
                {
                    //  Проверка данных.
                    if (!map.TryGetValue(frequency, out RawFrameChannelData[]? data) ||
                        data.Length == 0)
                    {
                        //  Сброс флага.
                        isValid = false;

                        //  Завершение проверки.
                        break;
                    }
                }
            }

            //  Проверка данных.
            if (!isValid)
            {
                //  Переход к следующему фрагменту.
                continue;
            }

            //  Сброс флага.
            isValid = true;

            //  Определение меток времени для поиска целевых кадров.
            long firstTimestamp = lastZeroTime.Ticks;
            long lastTimestamp = nmeaFragment.LastTime.AddMinutes(-1).Ticks;

            //  Поиск целевых кадров.
            Dictionary<int, Dictionary<double, RawFrameChannelData[]>> targetData = await context
                .RawFrameChannels
                .Where(x => x.Timestamp >= firstTimestamp && x.Timestamp <= lastTimestamp)
                .GroupBy(x => x.Index)
                .ToDictionaryAsync(x => x.Key,
                    x => x
                        .GroupBy(x => x.Frequency)
                        .ToDictionary(x => x.Key, x => x.ToArray()),
                    cancellationToken)
                .ConfigureAwait(false);

            //  Перебор основных каналов.
            for (int i = 0; i < 32; i++)
            {
                //  Проверка флага.
                if (!isValid)
                {
                    //  Завершение проверки.
                    break;
                }

                //  Проверка данных.
                if (!targetData.TryGetValue(i, out Dictionary<double, RawFrameChannelData[]>? map))
                {
                    //  Сброс флага.
                    isValid = false;

                    //  Завершение проверки.
                    break;
                }

                //  Перебор частот.
                foreach (double frequency in Aurora01Tunings.Frequencies)
                {
                    //  Проверка данных.
                    if (!map.TryGetValue(frequency, out RawFrameChannelData[]? data) ||
                        data.Length == 0)
                    {
                        //  Сброс флага.
                        isValid = false;

                        //  Завершение проверки.
                        break;
                    }
                }
            }

            //  Проверка данных.
            if (!isValid)
            {
                //  Переход к следующему фрагменту.
                continue;
            }

            //  Перебор основных каналов.
            for (int i = 0; i < 32; i++)
            {
                //  Перебор частот.
                foreach (double frequency in Aurora01Tunings.Frequencies)
                {
                    //  Определение нуля.
                    double zero = zeroData[i][frequency]
                        .Select(x => x.Sum / x.Count).Average();

                    //  Формирование данных.
                    foreach (var result in targetData[i][frequency]
                        .OrderBy(x => x.Timestamp)
                        .Select(x => new
                        {
                            Time = new DateTime(x.Timestamp),
                            Data = x,
                        })
                        .Select(x => new
                        {
                            x.Time,
                            x.Data,
                            Nmeas = nmeaFragment.GetRange(x.Time, TimeSpan.FromMinutes(1)),
                        })
                        .Select(x => new
                        {
                            x.Time,
                            x.Data.Sum,
                            x.Data.SquaresSum,
                            x.Data.Min,
                            x.Data.Max,
                            x.Data.Count,
                            Speed = x.Nmeas.Select(x => x.Speed).Average(),
                            Latitude = x.Nmeas.Select(x => x.Latitude).Average(),
                            Longitude = x.Nmeas.Select(x => x.Longitude).Average(),
                            Zero = zero,
                        }))
                    {
                        results[i][frequency].Add(
                            (result.Time,
                            result.Sum,
                            result.SquaresSum,
                            result.Min,
                            result.Max,
                            result.Count,
                            result.Speed,
                            result.Latitude,
                            result.Longitude,
                            result.Zero));
                    }
                }
            }

            //  Вывод в консоль.
            Console.WriteLine($"Фрагмент {nmeaFragment.FirstTime}: " +
                $"Длительность: {(nmeaFragment.LastTime - nmeaFragment.FirstTime).TotalHours}, " +
                $"Ноль: {(lastZeroTime - firstZeroTime).TotalHours}.");
        }

        //  Создание средства посторения текста.
        StringBuilder builder = new();

        //  Формирование заголовка.
        builder.Append("Channel;");
        builder.Append("Frequency;");
        builder.Append("Time;");
        builder.Append("Sum;");
        builder.Append("SquaresSum;");
        builder.Append("Min;");
        builder.Append("Max;");
        builder.Append("Count;");
        builder.Append("Speed;");
        builder.Append("Latitude;");
        builder.Append("Longitude;");
        builder.Append("Zero;");
        builder.AppendLine();


        //{


        //    //  Перебор основных каналов.
        //    for (int i = 0; i < 32; i++)
        //    {
        //        //  Перебор частот.
        //        foreach (double frequency in Aurora01Tunings.Frequencies)
        //        {
        //            //  Формирование метки.
        //            string label = $"{i:00}_{frequency:000}_";

        //            //  Перебор имён.
        //            foreach (string name in new string[]
        //            {
        //                "Time",
        //                "Sum",
        //                "SquaresSum",
        //                "Min",
        //                "Max",
        //                "Count",
        //                "Speed",
        //                "Latitude",
        //                "Longitude",
        //                "Zero",
        //            })
        //            {
        //                builder.Append(label + name);
        //                builder.Append(';');
        //            }
        //        }
        //    }

        //    //  Переход на новую строку.
        //    builder.AppendLine();
        //}

        //  Получение количества строк.
        int rows = results.Values
            .Select(x => x.Values
                .Select(x => x.Count)
                .Max())
            .Max();

        //  Перебор строк.
        for (int row = 0; row < rows; row++)
        {
            //  Перебор основных каналов.
            for (int i = 0; i < 32; i++)
            {
                //  Перебор частот.
                foreach (double frequency in Aurora01Tunings.Frequencies)
                {
                    //  Получение списка значений.
                    var data = results[i][frequency];


                    //  Проверка индекса.
                    if (row < data.Count)
                    {
                        //  Получение элемента.
                        var (time, sum, squaresSum, min, max, count, speed, latitude, longitude, zero) = data[row];

                        //  Запись данных.
                        write(i);
                        write(frequency);
                        write(time.ToOADate());
                        write(sum);
                        write(squaresSum);
                        write(min);
                        write(max);
                        write(count);
                        write(speed);
                        write(latitude);
                        write(longitude);
                        write(zero);
                    }
                    else
                    {
                        //  Запись данных.
                        builder.Append(";;;;;;;;;;;;");
                    }
                    builder.AppendLine();

                    ////  Проверка индекса.
                    //if (row < data.Count)
                    //{
                    //    //  Получение элемента.
                    //    var (time, sum, squaresSum, min, max, count, speed, latitude, longitude, zero) = data[row];

                    //    //  Запись данных.
                    //    write(time.ToOADate());
                    //    write(sum);
                    //    write(squaresSum);
                    //    write(min);
                    //    write(max);
                    //    write(count);
                    //    write(speed);
                    //    write(latitude);
                    //    write(longitude);
                    //    write(zero);
                    //}
                    //else
                    //{
                    //    //  Запись данных.
                    //    builder.Append(';');
                    //    builder.Append(';');
                    //    builder.Append(';');
                    //    builder.Append(';');
                    //    builder.Append(';');
                    //    builder.Append(';');
                    //    builder.Append(';');
                    //    builder.Append(';');
                    //    builder.Append(';');
                    //    builder.Append(';');
                    //}
                }
            }

            ////  Переход на новую строку.
            //builder.AppendLine();
        }

        //  Добавляет значение.
        void write(double value)
        {
            builder.Append(value.ToString().Replace('.', ','));
            builder.Append(';');
        }

        //  Сохранение результата.
        await File.WriteAllTextAsync(
            "C:\\Environs\\Data\\Simargl.Trials.Aurora.Aurora01.Analyzer\\Results.csv",
            builder.ToString(),
            cancellationToken)
            .ConfigureAwait(false);
    }
}
