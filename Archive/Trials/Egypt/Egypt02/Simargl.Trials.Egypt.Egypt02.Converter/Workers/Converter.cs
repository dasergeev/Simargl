using Microsoft.Extensions.Logging;
using Simargl.Frames;
using Simargl.Frames.TestLab;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Simargl.Trials.Egypt.Egypt02.Converter.Workers;

/// <summary>
/// Представляет конвертер.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public sealed class Converter(ILogger<Converter> logger) :
    Worker<Converter>(logger)
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
        //  Основной цикл выполнения.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Получение информации о файлах.
            var map = Scanner.RawDataMap
                .Select(x =>
                {
                    //  Проверка данных.
                    bool isRecord = Scanner.RecordMap.TryGetValue(x.Key, out FileInfo? recordFileInfo);
                    bool isCsv = Scanner.CsvMap.TryGetValue(x.Key, out FileInfo? csvFileInfo);

                    //  Возврат данных.
                    return new
                    {
                        Time = x.Key,
                        RawDataFileInfo = x.Value,
                        IsRecord = isRecord,
                        RecordFileInfo = isRecord ? recordFileInfo : null,
                        IsCsv = isCsv,
                        CsvFileInfo = isCsv ? csvFileInfo : null,
                    };
                })
                .Where(x => !x.IsRecord || !x.IsCsv);

            //  Асинхронная работа с файлами.
            await Parallel.ForEachAsync(
                map,
                cancellationToken,
                async (item, cancellationToken) =>
                {
                    //  Проверка токена отмены.
                    cancellationToken.ThrowIfCancellationRequested();

                    //  Ожидание завершённой задачи.
                    await Task.CompletedTask.ConfigureAwait(false);

                    //  Получение данных.
                    DateTime time = item.Time;
                    FileInfo rawDataFileInfo = item.RawDataFileInfo;
                    FileInfo? recordFileInfo = item.RecordFileInfo;
                    FileInfo? csvFileInfo = item.CsvFileInfo;

                    //  Обновление информации о файлах.
                    updateFiles();

                    //  Проверка необходимости конвертации.
                    if (recordFileInfo is not null && csvFileInfo is not null)
                    {
                        //  Конвертация не требуется.
                        return;
                    }

                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Открытие кадра.
                        Frame frame = new(rawDataFileInfo.FullName);

                        for (int i = 0; i < frame.Channels.Count; i++)
                        {
                            Channel channel = frame.Channels[i];
                            if (channel.Name.StartsWith("None"))
                            {
                                frame.Channels.Remove(channel);
                                --i;
                                continue;
                            }
                            TestLabChannelHeader channelHeader = (TestLabChannelHeader)channel.Header;
                            channelHeader.DataFormat = TestLabDataFormat.Float64;
                            channelHeader.Offset = 0;
                        }
                        double speed = frame.Channels["V_GPS"].Average();

                        string speedText = $"{speed:000.0}".Replace(',', '_').Replace('.', '_');
                        string timeText = $"{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}-{time.Minute:00}-{time.Second:00}";
                        int number = time.Second + time.Minute * 60 + time.Hour * 3600;

                        //  Обновление информации о файлах.
                        updateFiles();

                        //  Проверка необходимости записи кадра.
                        if (recordFileInfo is null)
                        {
                            string fileName = $"Vp{speedText} {timeText}.{number:00000}";
                            string targetDirectory = Path.Combine(Tunings.RecordsPath, $"{time.Year:0000}-{time.Month:00}-{time.Day:00}");
                            if (!Directory.Exists(targetDirectory))
                            {
                                Directory.CreateDirectory(targetDirectory);
                            }
                            string fullName = Path.Combine(targetDirectory, fileName);
                            frame.Save(fullName, StorageFormat.TestLab);
                            FileInfo fileInfo = new(fullName);
                            Scanner.RecordMap.AddOrUpdate(time, fileInfo, (time, _) => fileInfo);

                            Console.WriteLine($"Новая запись: {fileName}");
                        }

                        const double tivSampling = 300;
                        const int tivLength = 60 * (int)tivSampling;

                        StringBuilder output = new();

                        for (int i = 0; i < tivLength; i++)
                        {
                            double t = i / tivSampling;
                            output.Append(t);
                            foreach (Channel channel in frame.Channels)
                            {
                                output.Append(';');
                                if (channel.Sampling == tivSampling)
                                {
                                    output.Append(channel[i]);
                                }
                                else
                                {
                                    output.Append(channel[(int)t]);
                                }
                            }
                            output.AppendLine();
                        }

                        //  Обновление информации о файлах.
                        updateFiles();

                        //  Проверка необходимости записи файла.
                        if (csvFileInfo is null)
                        {
                            string fileName = $"{number:00000}.csv";
                            string targetDirectory = Path.Combine(Tunings.CsvPath, $"{time.Year:0000}-{time.Month:00}-{time.Day:00}");
                            if (!Directory.Exists(targetDirectory))
                            {
                                Directory.CreateDirectory(targetDirectory);
                            }
                            string fullName = Path.Combine(targetDirectory, fileName);
                            File.WriteAllText(fullName, output.ToString());

                            FileInfo fileInfo = new(fullName);
                            Scanner.CsvMap.AddOrUpdate(time, fileInfo, (time, _) => fileInfo);

                            Console.WriteLine($"Новый файл: {fileName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        //  Проверка токена отмены.
                        if (cancellationToken.IsCancellationRequested)
                        {
                            //  Повторный выброс исключения.
                            throw;
                        }

                        //  Вывод информации в консоль.
                        Console.WriteLine($"Произошла ошибка при конвертации: {ex}");
                    }

                    //  Обновляет информацию о файлах.
                    void updateFiles()
                    {
                        if (Scanner.RecordMap.TryGetValue(time, out FileInfo? fileInfo) &&
                            fileInfo is not null)
                        {
                            recordFileInfo = fileInfo;
                        }

                        if (Scanner.CsvMap.TryGetValue(time, out fileInfo) &&
                            fileInfo is not null)
                        {
                            csvFileInfo = fileInfo;
                        }
                    }
                }).ConfigureAwait(false);

            //  Ожидание перед следующим проходом.
            await Task.Delay(60_000, cancellationToken).ConfigureAwait(false);
        }
    }
}
