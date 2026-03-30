using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Simargl.Projects.Oriole.Oriole01Storage;
using Simargl.Projects.Oriole.Oriole01Storage.Entities;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Simargl.Projects.Oriole.Scanner;

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
                //  Получение корневого каталога.
                DirectoryInfo rootInfo = new(rootPath);

                //  Проверка пути.
                if (rootInfo.Exists)
                {
                    logger.LogInformation("Работа с корневым каталогом: \"{path}\".", rootInfo.FullName);

                    //  Получение часовых каталогов.
                    DirectoryInfo[] subInfos = rootInfo.GetDirectories("*", SearchOption.TopDirectoryOnly);

                    //  Перебор каталогов.
                    foreach (DirectoryInfo subInfo in subInfos)
                    {
                        //  Создание данных.
                        HourDirectoryData hourDirectory = new();

                        //  Установка имени.
                        hourDirectory.SetName(subInfo.Name);

                        //  Проверка базы данных.
                        {
                            HourDirectoryData? data = await context.HourDirectories
                                .FirstOrDefaultAsync(x => x.Timestamp == hourDirectory.Timestamp, cancellationToken)
                                .ConfigureAwait(false);

                            //  Проверка существования данных.
                            if (data is not null)
                            {
                                //  Изменение текущих данных.
                                hourDirectory = data;
                            }
                            else
                            {
                                //  Добавление данных.
                                context.HourDirectories.Add(hourDirectory);

                                //  Сохранение изменений.
                                await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                            }
                        }
                    }

                    //  Получение часовых каталогов.
                    HourDirectoryData[] hourDirectories = await context.HourDirectories
                        .Where(x => x.State == HourDirectoryState.Registered)
                        .ToArrayAsync(cancellationToken).ConfigureAwait(false);

                    //  Перебор часовых каталогов.
                    foreach (HourDirectoryData hourDirectory in hourDirectories)
                    {
                        //  Получение полного пути к каталогу.
                        string fullPath = Path.Combine(rootInfo.FullName, hourDirectory.GetName());
                        logger.LogInformation("Работа с часовым каталогом: \"{path}\".", fullPath);

                        //  Перебор каталогов данных.
                        foreach (DirectoryInfo directoryinfo in new DirectoryInfo(fullPath).GetDirectories("*", SearchOption.TopDirectoryOnly))
                        {
                            //  Проверка токена отмены.
                            cancellationToken.ThrowIfCancellationRequested();

                            //  Получение имени.
                            string directoryName = directoryinfo.Name.Trim().ToLower();

                            //  Проверка типа каталога.
                            switch (directoryName[..4])
                            {
                                case "adxl":
                                    //  Получение данных датчика.
                                    AdxlData adxlData = new();
                                    adxlData.SetIPAddress(directoryName.Split('-')[1]);

                                    //  Проверка базы данных.
                                    {
                                        AdxlData? data = await context.Adxls
                                            .FirstOrDefaultAsync(x => x.Address == adxlData.Address, cancellationToken)
                                            .ConfigureAwait(false);

                                        //  Проверка существования данных.
                                        if (data is not null)
                                        {
                                            //  Изменение текущих данных.
                                            adxlData = data;
                                        }
                                        else
                                        {
                                            //  Добавление данных.
                                            context.Adxls.Add(adxlData);

                                            //  Сохранение изменений.
                                            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                        }
                                    }

                                    //  Перебор файлов в каталоге.
                                    foreach (FileInfo fileInfo in directoryinfo.GetFiles("*", SearchOption.TopDirectoryOnly))
                                    {
                                        //  Получение имени.
                                        string fileName = fileInfo.Name.Trim().ToLower();

                                        //  Проверка расширения файла.
                                        if (fileName.EndsWith(".adxl"))
                                        {
                                            //  Обрезка имени.
                                            fileName = fileName[..^5];

                                            //  Разбивка имени.
                                            string[] parts = fileName.Split('-');

                                            //  Разбор частей.
                                            if (parts.Length != 9 ||
                                                parts[0] != "adxl" ||
                                                !int.TryParse(parts[2], out int year) ||
                                                !int.TryParse(parts[3], out int month) ||
                                                !int.TryParse(parts[4], out int day) ||
                                                !int.TryParse(parts[5], out int hour) ||
                                                !int.TryParse(parts[6], out int minute) ||
                                                !int.TryParse(parts[7], out int second) ||
                                                !int.TryParse(parts[8], out int millisecond))
                                            {
                                                //  Переход к следующему файлу.
                                                continue;
                                            }

                                            //  Проверка адреса.
                                            try
                                            {
                                                AdxlData data = new();
                                                data.SetIPAddress(parts[1]);
                                                if (adxlData.Address != data.Address)
                                                {
                                                    //  Переход к следующему файлу.
                                                    continue;
                                                }
                                            }
                                            catch
                                            {
                                                //  Переход к следующему файлу.
                                                continue;
                                            }

                                            //  Определение времени записи файла.
                                            DateTime writeTime = new(year, month, day, hour, minute, second, millisecond);

                                            //  Создание данных.
                                            AdxlFileData fileData = new()
                                            {
                                                Timestamp = writeTime.Ticks,
                                                HourDirectoryKey = hourDirectory.Key,
                                                AdxlKey = adxlData.Key,
                                            };

                                            //  Добавление в базу данных.
                                            try
                                            {
                                                //  Подключение к базе данных.
                                                using Oriole01StorageContext localContext = new();

                                                //  Добавление сущности.
                                                localContext.AdxlFiles.Add(fileData);

                                                //  Запись данных.
                                                await localContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                            }
                                            catch { }
                                        }
                                    }

                                    //  Завершение разбора.
                                    break;
                                case "nmea":
                                    //  Перебор файлов в каталоге.
                                    foreach (FileInfo fileInfo in directoryinfo.GetFiles("*", SearchOption.TopDirectoryOnly))
                                    {
                                        //  Получение имени.
                                        string fileName = fileInfo.Name.Trim().ToLower();

                                        //  Проверка расширения файла.
                                        if (fileName.EndsWith(".nmea"))
                                        {
                                            //  Обрезка имени.
                                            fileName = fileName[..^5];

                                            //  Разбивка имени.
                                            string[] parts = fileName.Split('-');

                                            //  Разбор частей.
                                            if (parts.Length != 6 ||
                                                parts[0] != "nmea" ||
                                                !int.TryParse(parts[1], out int year) ||
                                                !int.TryParse(parts[2], out int month) ||
                                                !int.TryParse(parts[3], out int day) ||
                                                !int.TryParse(parts[4], out int hour) ||
                                                !int.TryParse(parts[5], out int minute))
                                            {
                                                //  Переход к следующему файлу.
                                                continue;
                                            }

                                            //  Определение времени записи файла.
                                            DateTime writeTime = new(year, month, day, hour, minute, 0);

                                            //  Создание данных.
                                            NmeaFileData fileData = new()
                                            {
                                                Timestamp = writeTime.Ticks,
                                                HourDirectoryKey = hourDirectory.Key,
                                            };

                                            //  Добавление в базу данных.
                                            try
                                            {
                                                //  Подключение к базе данных.
                                                using Oriole01StorageContext localContext = new();

                                                //  Добавление сущности.
                                                localContext.NmeaFiles.Add(fileData);

                                                //  Запись данных.
                                                await localContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                                            }
                                            catch { }
                                        }
                                    }

                                    //  Завершение разбора.
                                    break;
                            };
                        }
                    }
                }
            }

            //  Ожидание перед следующим сканированием.
            await Task.Delay(_Period, cancellationToken).ConfigureAwait(false);
        }
    }
}
