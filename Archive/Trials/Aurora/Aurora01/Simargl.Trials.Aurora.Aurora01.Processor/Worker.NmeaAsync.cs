using Microsoft.EntityFrameworkCore;
using Simargl.Recording.Geolocation.Nmea;
using Simargl.Trials.Aurora.Aurora01.Storage;
using Simargl.Trials.Aurora.Aurora01.Storage.Entities;
using System.IO;
using System.Linq;

namespace Simargl.Trials.Aurora.Aurora01.Processor;

using NmeaData = Storage.Entities.NmeaData;

partial class Worker
{
    /// <summary>
    /// Асинхронно выполняет работу с файлами Nmea.
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
    private static async Task NmeaAsync(ProcessorContext context, CancellationToken cancellationToken)
    {
        //  Основной цикл работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Параллельное выполнение.
            await Parallel.ForAsync(
                0, Aurora01Tunings.NmeaMaxDegreeOfParallelism,
                new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                    MaxDegreeOfParallelism = Aurora01Tunings.NmeaMaxDegreeOfParallelism,
                },
                async delegate (int index, CancellationToken cancellationToken)
                {
                    //  Основной цикл разбора.
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        //  Запрос необработанного файла.
                        NmeaFileData? nmeaFile = await context.TransactionAsync(
                            async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
                            {
                                //  Поиск файла.
                                return await context.NmeaFiles
                                    .Where(x => x.State == NmeaFileState.Registered)
                                    .Include(x => x.HourDirectory)
                                    .OrderBy(x => EF.Functions.Random())
                                    .FirstOrDefaultAsync(cancellationToken)
                                    .ConfigureAwait(false);
                            }, cancellationToken).ConfigureAwait(false);

                        //  Проверка файла.
                        if (nmeaFile is null)
                        {
                            //  Завершение разбора.
                            break;
                        }

                        //  Получение пути к файлу.
                        string path = Path.Combine(
                            Aurora01Tunings.RawDataPath,
                            nmeaFile.HourDirectory.GetName(),
                            "nmea",
                            nmeaFile.GetFileName());

                        //  Проверка файла.
                        if (!File.Exists(path))
                        {
                            //  Переход к следующему файлу.
                            continue;
                        }

                        //  Получение данных.
                        var messages = (await File.ReadAllLinesAsync(path, cancellationToken)
                            .ConfigureAwait(false))
                            .Select(x => x.Trim())
                            .Where(x => x.Length > 1)
                            .Select(x =>
                            {
                                try
                                {
                                    return NmeaMessage.Parse(x);
                                }
                                catch
                                {
                                    return null!;
                                }
                            })
                            .Where(x => x is NmeaRmcMessage rmc && rmc.Date.HasValue && rmc.Time.HasValue && rmc.Speed.HasValue && rmc.Latitude.HasValue && rmc.Longitude.HasValue)
                            .Select(x => new
                            {
                                Time = new DateTime(
                                    2000 + ((NmeaRmcMessage)x)!.Date!.Value.Year, ((NmeaRmcMessage)x)!.Date!.Value.Month, ((NmeaRmcMessage)x)!.Date!.Value.Day,
                                    ((NmeaRmcMessage)x)!.Time!.Value.Hour, ((NmeaRmcMessage)x)!.Time!.Value.Minute, ((NmeaRmcMessage)x)!.Time!.Value.Second),
                                Speed = ((NmeaRmcMessage)x)!.Speed!.Value,
                                Latitude = ((NmeaRmcMessage)x)!.Latitude!.Value,
                                Longitude = ((NmeaRmcMessage)x)!.Longitude!.Value,
                            })
                            .OrderBy(x => x.Time)
                            .ToArray();

                        //  Проверка количества сообщений.
                        if (messages.Length > 0)
                        {
                            //  Получение времени файла.
                            DateTime fileTime = HourDirectoryData.ToTime(nmeaFile.HourDirectoryTimestamp)
                                + TimeSpan.FromMinutes(nmeaFile.Minute);

                            //  Определение смещения.
                            TimeSpan deltaTime = TimeSpan.FromHours((int)Math.Round(messages.Select(x => (fileTime - x.Time).TotalHours).Average()));

                            //  Перебор сообщений.
                            foreach (var message in messages)
                            {
                                //  Получение времени сообщения.
                                DateTime time = message.Time + deltaTime;

                                //  Создание данных.
                                NmeaData nmeaData = new()
                                {
                                    Timestamp = NmeaData.FromTime(time),
                                    Speed = message.Speed,
                                    Latitude = message.Latitude,
                                    Longitude = message.Longitude,
                                };

                                //  Блок перехвата всех исключений.
                                try
                                {
                                    //  Подключение к базе данных.
                                    await context.TransactionAsync(
                                        delegate (Aurora01StorageContext context)
                                        {
                                            //  Добавление данных.
                                            context.Nmeas.Add(nmeaData);
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
                                    NmeaFileData? data = await context.NmeaFiles.FindAsync(
                                        [nmeaFile.HourDirectoryTimestamp, nmeaFile.Minute], cancellationToken)
                                        .ConfigureAwait(false);

                                    //  Проверка данных.
                                    if (data is not null)
                                    {
                                        //  Установка состояния.
                                        data.State = NmeaFileState.Parsed;
                                    }
                                }, cancellationToken).ConfigureAwait(false);
                        }
                        catch (DbUpdateException)
                        {

                        }
                    }
                });

            //  Ожидание перед повторным сканированием.
            await Task.Delay(Aurora01Tunings.NmeaPeriod, cancellationToken).ConfigureAwait(false); ;
        }
    }
}
