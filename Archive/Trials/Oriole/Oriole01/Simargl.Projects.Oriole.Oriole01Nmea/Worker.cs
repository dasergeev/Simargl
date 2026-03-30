using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Simargl.Projects.Oriole.Oriole01Storage;
using Simargl.Projects.Oriole.Oriole01Storage.Entities;
using Simargl.Recording.Geolocation.Nmea;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NmeaData = Simargl.Projects.Oriole.Oriole01Storage.Entities.NmeaData;

namespace Simargl.Projects.Oriole.Oriole01Nmea;

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
                            NmeaFileData? nmeaFile = await local.NmeaFiles
                                    .Where(x => x.State == NmeaFileState.Registered)
                                    .Include(x => x.HourDirectory)
                                    .OrderBy(x => EF.Functions.Random())
                                    .FirstOrDefaultAsync(cancellationToken)
                                    .ConfigureAwait(false);

                            //  Проверка файла.
                            if (nmeaFile is null)
                            {
                                //  Необработанных файлов не осталось.
                                break;
                            }

                            //  Получение времени файла.
                            DateTime fileTime = new(nmeaFile.Timestamp);

                            //  Получение пути к файлу.
                            string path = Path.Combine(
                                    rootPath,
                                    nmeaFile.HourDirectory.GetName(),
                                    "Nmea",
                                    $"Nmea-{fileTime.Year:0000}-{fileTime.Month:00}-{fileTime.Day:00}-{fileTime.Hour:00}-{fileTime.Minute:00}.nmea");

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
                                NmeaFileData? data = await local.NmeaFiles.FirstOrDefaultAsync(
                                        x => x.Key == nmeaFile.Key, cancellationToken)
                                        .ConfigureAwait(false);

                                //  Проверка данных.
                                if (data is null)
                                {
                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Замена данных.
                                nmeaFile = data;

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
                                        .Where(x => x is NmeaRmcMessage rmc && rmc.Time.HasValue && rmc.Speed.HasValue && rmc.Latitude.HasValue && rmc.Longitude.HasValue)
                                        .Select(x => new
                                        {
                                            Time = ((NmeaRmcMessage)x)!.Time!.Value,
                                            Speed = ((NmeaRmcMessage)x)!.Speed!.Value,
                                            Latitude = ((NmeaRmcMessage)x)!.Latitude!.Value,
                                            Longitude = ((NmeaRmcMessage)x)!.Longitude!.Value,
                                        })
                                .ToArray();

                                //  Перебор сообщений.
                                foreach (var message in messages)
                                {
                                    //  Получение времени сообщения.
                                    DateTime time = new(fileTime.Year, fileTime.Month, fileTime.Day, fileTime.Hour, fileTime.Minute, message.Time.Second);

                                    //  Создание данных.
                                    NmeaData nmeaData = new()
                                    {
                                        Timestamp = time.Ticks,
                                        Speed = message.Speed,
                                        Latitude = message.Latitude,
                                        Longitude = message.Longitude,
                                    };

                                    //  Блок перехвата всех исключений.
                                    try
                                    {
                                        //  Подключение к базе данных.
                                        using Oriole01StorageContext localContext = new();

                                        //  Добавление данных.
                                        localContext.Nmeas.Add(nmeaData);

                                        //  Сохранение изменений.
                                        await localContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                    }
                                    catch (DbUpdateException)
                                    {

                                    }
                                }

                                //  Установка состояния.
                                nmeaFile.State = NmeaFileState.Parsed;

                                //  Сохранение изменений.
                                await local.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                                if (messages.Length > 0)
                                {
                                    Console.WriteLine($"{path}: {messages.Length}, {messages[0].Time}");
                                }

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
