using Microsoft.EntityFrameworkCore;
using Simargl.Analysis;
using Simargl.Recording.AccelEth3T;
using Simargl.Trials.Aurora.Aurora01.Storage;
using Simargl.Trials.Aurora.Aurora01.Storage.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Simargl.Trials.Aurora.Aurora01.Processor;

partial class Worker
{
    /// <summary>
    /// Асинхронно выполняет работу с файлами Adxl.
    /// </summary>
    /// <param name="context">
    /// Контекст обработки.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу.
    /// </returns>
    private static async Task AdxlAsync(ProcessorContext context, CancellationToken cancellationToken)
    {
        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Параллельное выполнение.
            await Parallel.ForAsync(
                0, Aurora01Tunings.AdxlMaxDegreeOfParallelism,
                new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                    MaxDegreeOfParallelism = Aurora01Tunings.AdxlMaxDegreeOfParallelism,
                },
                async delegate (int _, CancellationToken cancellationToken)
                {
                    //  Основной цикл выполнения.
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        //  Поиск необработанного файла.
                        AdxlFileData? adxlFile = await context.TransactionAsync(
                            async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
                            {
                                return await context.AdxlFiles
                                    .Where(x => x.State == AdxlFileState.Registered)
                                    .Include(x => x.HourDirectory)
                                    .Include(x => x.Adxl)
                                    .OrderBy(x => EF.Functions.Random())
                                    .FirstOrDefaultAsync(cancellationToken)
                                    .ConfigureAwait(false);
                            }, cancellationToken).ConfigureAwait(false);

                        //  Проверка файла.
                        if (adxlFile is null)
                        {
                            //  Необработанных файлов не осталось.
                            break;
                        }

                        //  Получение пути к файлу.
                        string path = Path.Combine(
                            Aurora01Tunings.RawDataPath,
                            adxlFile.HourDirectory.GetName(),
                            $"adxl-{AdxlData.ToIPAddress(adxlFile.AdxlAddress)}",
                            adxlFile.GetFileName());

                        //  Проверка файла.
                        if (!File.Exists(path))
                        {
                            //  Переход к следующему файлу.
                            continue;
                        }

                        //  Определение времени записи.
                        DateTime fileTime = HourDirectoryData.ToTime(adxlFile.HourDirectoryTimestamp) + AdxlFileData.ToTime(adxlFile.Timestamp);

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
                            foreach (double frequency in Aurora01Tunings.Frequencies)
                            {
                                //  Создание фильтра.
                                Analysis.Transforms.ButterworthFilter filter = new(frequency, 4);

                                //  Перебор каналов.
                                for (byte channelIndex = 0; channelIndex < 3; channelIndex++)
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
                                        await context.TransactionAsync(
                                            async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
                                            {
                                                AdxlFileData? data = await context.AdxlFiles
                                                    .FindAsync(
                                                        [
                                                            adxlFile.AdxlAddress,
                                                            adxlFile.HourDirectoryTimestamp,
                                                            adxlFile.Timestamp
                                                        ], cancellationToken)
                                                    .ConfigureAwait(false);

                                                if (data is null) return;

                                                //  Запрос данных.
                                                AdxlChannelData? channelData = await context.AdxlChannels
                                                    .FindAsync(
                                                        [
                                                            time.Ticks,
                                                            adxlFile.AdxlAddress,
                                                            channelIndex,
                                                            frequency,
                                                        ], cancellationToken).ConfigureAwait(false);

                                                if (channelData is null)
                                                {
                                                    //  Создание данных.
                                                    channelData = new()
                                                    {
                                                        Timestamp = time.Ticks,
                                                        AdxlAddress = adxlFile.AdxlAddress,
                                                        Index = channelIndex,
                                                        Frequency = frequency,
                                                    };

                                                    //  Добавление данных.
                                                    context.AdxlChannels.Add(channelData);
                                                }

                                                //  Запись данных.
                                                channelData.Sum = sum;
                                                channelData.SquaresSum = squaresSum;
                                                channelData.Min = min;
                                                channelData.Max = max;
                                                channelData.Count = count;
                                            }, cancellationToken).ConfigureAwait(false);
                                    }
                                    catch (DbUpdateException)
                                    {

                                    }
                                }
                            }
                        }

                        //  Блок перехвата всех исключений.
                        try
                        {
                            //  Подключение к базе данных.
                            await context.TransactionAsync(
                                async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
                                {
                                    //  Запрос данных.
                                    AdxlFileData? data = await context.AdxlFiles.FindAsync(
                                        [adxlFile.AdxlAddress,
                                            adxlFile.HourDirectoryTimestamp,
                                            adxlFile.Timestamp], cancellationToken)
                                        .ConfigureAwait(false);

                                    //  Проверка данных.
                                    if (data is not null)
                                    {
                                        //  Установка состояния.
                                        data.State = AdxlFileState.Parsed;
                                    }
                                }, cancellationToken).ConfigureAwait(false);
                        }
                        catch (DbUpdateException)
                        {

                        }
                    }
                }).ConfigureAwait(false);

                //  Ожидание перед повторным сканированием.
                await Task.Delay(Aurora01Tunings.AdxlPeriod, cancellationToken).ConfigureAwait(false); ;
        }
    }
}
