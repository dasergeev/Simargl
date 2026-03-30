using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Simargl.Projects.Oriole.Oriole01Storage;
using Simargl.Projects.Oriole.Oriole01Storage.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Simargl.Projects.Oriole.Oriole01Collector;

/// <summary>
/// Представляет основной фоновый процесс.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public sealed class Worker(ILogger<Worker> logger) :
    BackgroundService
{
    private const double MinCompression = 0.999;
    private const double MaxCompression = 1.0 / MinCompression;
    private const long MinBias = - MaxBias;
    private const long MaxBias = TimeSpan.TicksPerSecond;

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
    /// Асинхронно устанавливает размеры файлов.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task SetFileSizesAsync(CancellationToken cancellationToken)
    {
        //  Подключение к базе данных.
        await using Oriole01StorageContext context = new();

        //  Вывод информации в консоль.
        Console.WriteLine("Начало установки размеров файлов.");

        //  Блок с гарантированным завершением.
        try
        {
            //  Общее количество.
            long count = 0;

            //  Перебор путей.
            foreach (string rootPath in await context
                .Paths
                    .OrderBy(x => x.Priority)
                    .Select(x => x.FullPath)
                    .ToArrayAsync(cancellationToken)
                    .ConfigureAwait(false))
            {
                //  Проверка пути.
                if (!Directory.Exists(rootPath))
                {
                    //  Переход к следующему пути.
                    continue;
                }

                //  Получение данных.
                List<AdxlFileData> files = await context
                    .AdxlFiles
                    .Where(x => x.Size == 0)
                    .Include(x => x.HourDirectory)
                    .Include(x => x.Adxl)
                    .ToListAsync(cancellationToken);

                //  Асинхронная работа.
                await Parallel.ForEachAsync(
                    files,
                    new ParallelOptions()
                    {
                        MaxDegreeOfParallelism = 64,
                        CancellationToken = cancellationToken,
                    },
                    async delegate (AdxlFileData adxlFile, CancellationToken cancellationToken)
                    {
                        //  Получение времени файла.
                        DateTime fileTime = new(adxlFile.Timestamp);

                        //  Получение информации о файле.
                        FileInfo fileInfo = new(Path.Combine(
                                rootPath,
                                adxlFile.HourDirectory.GetName(),
                                $"Adxl-{adxlFile.Adxl.GetIPAddress()}",
                                $"Adxl-{adxlFile.Adxl.GetIPAddress()}-{fileTime.Year:0000}-{fileTime.Month:00}-{fileTime.Day:00}-{fileTime.Hour:00}-{fileTime.Minute:00}-{fileTime.Second:00}-{fileTime.Millisecond:000}.adxl"));

                        //  Проверка файла.
                        if (fileInfo.Exists)
                        {
                            //  Блок перехвата всех исключений.
                            try
                            {
                                //  Получение размера файла.
                                adxlFile.Size = fileInfo.Length;

                                //  Увеличение счётчика.
                                if (Interlocked.Increment(ref count) % 1000 == 0)
                                {
                                    //  Сохранение изменией.
                                    await context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

                                    //  Вывод информации в консоль.
                                    Console.WriteLine($"Установлено размеров файлов: {count}.");
                                }
                            }
                            catch { }
                        }
                    }).ConfigureAwait(false);
            }
        }
        finally
        {
            //  Сохранение изменией.
            await context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);

            //  Вывод информации в консоль.
            Console.WriteLine("Завершение установки размеров файлов.");
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
            //  Установка размеров файлов.
            await SetFileSizesAsync(cancellationToken).ConfigureAwait(false);

            //  Подключение к базе данных.
            await using Oriole01StorageContext context = new();

            //  Получение датчиков.
            long[] adxlKeys = await context.Adxls
                .Select(x => x.Key)
                .ToArrayAsync(cancellationToken)
                .ConfigureAwait(false);

            //  Перебор датчиков.
            for (int adxlIndex = 0; adxlIndex < adxlKeys.Length && !cancellationToken.IsCancellationRequested; adxlIndex++)
            {
                //  Получение ключа датчика.
                long adxlKey = adxlKeys[adxlIndex];

                //  Получение меток времени.
                long[] timestamps = await context.AdxlFiles
                    .Where(x => x.AdxlKey == adxlKey && x.Size == 254400)
                    .Select(x => x.Timestamp)
                    .OrderBy(x => x)
                    .ToArrayAsync(cancellationToken)
                    .ConfigureAwait(false);

                //  Определение коэффициентов.
                long timestamp0 = timestamps[0];
                double beta = 1.0 / (10.0 * TimeSpan.TicksPerSecond);

                //  Количество файлов во фрагменте.
                int n = timestamps.Length;

                //  Коэффициент сжатия.
                double compression = 0;

                //  Предварительный проход по сжатию.
                while (n > 1 && !cancellationToken.IsCancellationRequested)
                {
                    //  Определение коэффициента сжатия.
                    compression = (timestamps[n - 1] - timestamp0) * beta / (n - 1);

                    //  Проверка коэффициента сжатия.
                    if (MinCompression <= compression && compression <= MaxCompression)
                    {
                        //  Завершение прохода.
                        break;
                    }

                    //  Уменьшение количества файлов.
                    n--;
                }

                //  Определение общей суммы.
                BigInteger sum = 0;

                //  Перебор значений.
                for (int i = 0; i < n; i++)
                {
                    //  Корректировка суммы.
                    sum += timestamps[i];
                }

                //  Определение смещения.
                long bias = (long)(sum / n) - ((timestamps[n - 1] - timestamp0) >> 1);

                //  Проходи по сжатию и смещению.
                while (n > 1 && !cancellationToken.IsCancellationRequested)
                {
                    //  Определение коэффициента сжатия.
                    compression = (timestamps[n - 1] - timestamp0) * beta / (n - 1);

                    //  Корректировка смещения.


                    //  Проверка коэффициента сжатия.
                    if (MinCompression <= compression && compression <= MaxCompression)
                    {
                        //  Завершение прохода.
                        break;
                    }

                    //  Уменьшение количества файлов.
                    n--;
                }


                //  Проверка количества файлов во фрагменте.
                if (n == 1)
                {
                    //  Отстутсвует сжатие.
                    compression = 1;
                }

                Console.WriteLine($"{adxlKey}: {timestamps.Length}, {n}, {compression}, {bias}");
            }


            ////  Получение меток времени.
            //var t = await context.Adxl


            //  Ожидание перед следующим сканированием.
            await Task.Delay(60_000, cancellationToken).ConfigureAwait(false);
        }
    }
}
