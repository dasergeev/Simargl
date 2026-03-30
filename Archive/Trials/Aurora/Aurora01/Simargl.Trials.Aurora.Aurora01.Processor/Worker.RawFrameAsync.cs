using Microsoft.EntityFrameworkCore;
using Simargl.Analysis;
using Simargl.Frames.OldStyle;
using Simargl.Frames.TestLab;
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
    /// Асинхронно выполняет работу с файлами RawFrame.
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
    private static async Task RawFrameAsync(ProcessorContext context, CancellationToken cancellationToken)
    {
        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Параллельное выполнение.
            await Parallel.ForAsync(
                0, Aurora01Tunings.RawFrameMaxDegreeOfParallelism,
                new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                    MaxDegreeOfParallelism = Aurora01Tunings.RawFrameMaxDegreeOfParallelism,
                },
                async delegate (int _, CancellationToken cancellationToken)
                {
                    //  Основной цикл выполнения.
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        //  Поиск необработанного файла.
                        RawFrameFileData? rawFrameFile = await context.TransactionAsync(
                            async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
                            {
                                return await context.RawFrameFiles
                                    .Where(x => x.State == RawFrameFileState.Registered)
                                    .Include(x => x.HourDirectory)
                                    .OrderBy(x => EF.Functions.Random())
                                    .FirstOrDefaultAsync(cancellationToken)
                                    .ConfigureAwait(false);
                            }, cancellationToken).ConfigureAwait(false);

                        //  Проверка файла.
                        if (rawFrameFile is null)
                        {
                            //  Необработанных файлов не осталось.
                            break;
                        }

                        //  Получение пути к файлу.
                        string path = Path.Combine(
                            Aurora01Tunings.RawDataPath,
                            rawFrameFile.HourDirectory.GetName(),
                            "RawFrames",
                            rawFrameFile.GetFileName());

                        //  Проверка файла.
                        if (!File.Exists(path))
                        {
                            //  Переход к следующему файлу.
                            continue;
                        }

                        //  Определение времени записи.
                        DateTime fileTime = HourDirectoryData.ToTime(rawFrameFile.HourDirectoryTimestamp) + RawFrameFileData.ToTime(rawFrameFile.Timestamp);

                        //  Открытие кадра.
                        Frames.Frame frame = new(path);

                        //  Корректировка времени.
                        DateTime time = fileTime.AddSeconds(-60.0);
                        time = new(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);

                        // Получение каналов.
                        Frames.Channel[] channels = [.. frame.Channels];

                        foreach (Frames.Channel channel in channels)
                        {
                            if (channel.Header is TestLabChannelHeader header)
                            {
                                channel.Move(header.Offset);
                            }
                        }

                        //  Перебор частот.
                        foreach (double frequency in Aurora01Tunings.Frequencies)
                        {
                            //  Проверка токена отмены.
                            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                            //  Создание фильтра.
                            Analysis.Transforms.ButterworthFilter filter = new(frequency, 4);

                            //  Перебор каналов.
                            for (int channelIndex = 0; channelIndex < channels.Length; channelIndex++)
                            {
                                //  Проверка токена отмены.
                                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

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
                                            //  Запрос данных.
                                            RawFrameChannelData? channelData = await context.RawFrameChannels
                                                .FindAsync(
                                                    [
                                                        time.Ticks,
                                                        channelIndex,
                                                        frequency,
                                                    ], cancellationToken).ConfigureAwait(false);

                                            if (channelData is null)
                                            {
                                                //  Создание данных.
                                                channelData = new()
                                                {
                                                    Timestamp = time.Ticks,
                                                    Index = channelIndex,
                                                    Frequency = frequency,
                                                };

                                                //  Добавление данных.
                                                context.RawFrameChannels.Add(channelData);
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
                        //  Блок перехвата всех исключений.
                        try
                        {
                            //  Подключение к базе данных.
                            await context.TransactionAsync(
                                async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
                                {
                                    //  Запрос данных.
                                    RawFrameFileData? data = await context.RawFrameFiles.FindAsync(
                                        [rawFrameFile.HourDirectoryTimestamp,
                                            rawFrameFile.Timestamp], cancellationToken)
                                        .ConfigureAwait(false);

                                    //  Проверка данных.
                                    if (data is not null)
                                    {
                                        //  Установка состояния.
                                        data.State = RawFrameFileState.Parsed;
                                    }
                                }, cancellationToken).ConfigureAwait(false);
                        }
                        catch (DbUpdateException)
                        {

                        }
                    }
                }).ConfigureAwait(false);

                //  Ожидание перед повторным сканированием.
                await Task.Delay(Aurora01Tunings.RawFramePeriod, cancellationToken).ConfigureAwait(false); ;
        }
    }
}
