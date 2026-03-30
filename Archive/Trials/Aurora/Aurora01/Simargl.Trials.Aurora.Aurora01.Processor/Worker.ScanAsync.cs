using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Simargl.Trials.Aurora.Aurora01.Storage;
using Simargl.Trials.Aurora.Aurora01.Storage.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Simargl.Trials.Aurora.Aurora01.Processor;

partial class Worker
{
    /// <summary>
    /// Асинхронно выполняет сканирование.
    /// </summary>
    /// <param name="context">
    /// Контекст обработки.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая сканирование.
    /// </returns>
    private async Task ScanAsync(ProcessorContext context, CancellationToken cancellationToken)
    {
        //  Основной цикл сканирования.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Сканирование часовых каталогов.
            await ScanDirectoriesAsync().ConfigureAwait(false);

            //  Сканирование файлов.
            await ScanFilesAsync().ConfigureAwait(false);

            //  Ожидание перед повторным сканированием.
            await Task.Delay(Aurora01Tunings.ScanPeriod, cancellationToken).ConfigureAwait(false); ;
        }

        //  Сканирует часовые каталоги.
        async Task ScanDirectoriesAsync()
        {
            //  Перебор часовых каталогов.
            foreach (DirectoryInfo directory in new DirectoryInfo(Aurora01Tunings.RawDataPath)
                .GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                //  Проверка токена отмены.
                await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                //  Проверка существования.
                if (!directory.Exists)
                {
                    //  Переход к следующему каталогу.
                    continue;
                }

                //  Разбор имени.
                string[] parts = directory.Name.Split('-');

                //  Проверка имени.
                if (parts.Length != 4 ||
                    !int.TryParse(parts[0], out int year) ||
                    !int.TryParse(parts[1], out int month) ||
                    !int.TryParse(parts[2], out int day) ||
                    !int.TryParse(parts[3], out int hour))
                {
                    //  Переход к следующему каталогу.
                    continue;
                }

                //  Время каталога.
                DateTime time;

                //  Блок перехвата всех исключений.
                try
                {
                    //  Получение времени.
                    time = new(year, month, day, hour, 0, 0);
                }
                catch
                {
                    //  Переход к следующему каталогу.
                    continue;
                }

                //  Получение метки времени.
                int timestamp = HourDirectoryData.FromTime(time);

                try
                {
                    //  Создание данных.
                    HourDirectoryData directoryData = new()
                    {
                        Timestamp = timestamp,
                    };

                    //  Выполнение транзакции.
                    await context.TransactionAsync(
                        delegate (Aurora01StorageContext context)
                        {
                            //  Добавление данных.
                            context.HourDirectories.Add(directoryData);
                        }, cancellationToken).ConfigureAwait(false);

                    //  Вывод информации в журнал.
                    logger.LogInformation("Зарегистрирован новый часовой каталог \"{name}\".", directory.Name);
                }
                catch { }

                ////  Выполнение запроса.
                //HourDirectoryData? directoryData = await context.TransactionAsync(
                //    async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
                //    {
                //        //  Поиск в базе данных.
                //        return await context.HourDirectories
                //            .FindAsync([timestamp], cancellationToken)
                //            .ConfigureAwait(false);
                //    }, cancellationToken).ConfigureAwait(false);

                ////  Проверка данных.
                //if (directoryData is null)
                //{
                //    //  Создание данных.
                //    directoryData = new()
                //    {
                //        Timestamp = timestamp,
                //    };

                //    //  Выполнение транзакции.
                //    await context.TransactionAsync(
                //        delegate (Aurora01StorageContext context)
                //        {
                //            //  Добавление данных.
                //            context.HourDirectories.Add(directoryData);
                //        }, cancellationToken).ConfigureAwait(false);

                //    //  Вывод информации в журнал.
                //    logger.LogInformation("Зарегистрирован новый часовой каталог \"{name}\".", directory.Name);
                //}
            }

        }

        //  Сканирует файлы.
        async Task ScanFilesAsync()
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Выполнение запроса.
            HourDirectoryData[] directories = await context.TransactionAsync(
                async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
                {
                    //  Получение каталогов.
                    return await context.HourDirectories
                        .Where(x => x.State == HourDirectoryState.Registered)
                        .ToArrayAsync(cancellationToken)
                        .ConfigureAwait(false);
                }, cancellationToken).ConfigureAwait(false);

            //  Параллельная работа с каталогами.
            await Parallel.ForEachAsync(
                directories,
                new ParallelOptions()
                {
                    CancellationToken = cancellationToken,
                    MaxDegreeOfParallelism = Aurora01Tunings.ScanMaxDegreeOfParallelism,
                },
                async delegate (HourDirectoryData directoryData, CancellationToken cancellationToken)
                {
                    //  Блок перехвата всех исключений.
                    try
                    {

                        //  Проверка токена отмены.
                        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                        //  Получение времени каталога.
                        DateTime time = HourDirectoryData.ToTime(directoryData.Timestamp);

                        //  Получение пути к каталогу.
                        string path = Path.Combine(
                            Aurora01Tunings.RawDataPath,
                            $"{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}");

                        //  Перебор подкаталогов.
                        foreach (DirectoryInfo directoryInfo in new DirectoryInfo(path).GetDirectories("*", SearchOption.TopDirectoryOnly))
                        {
                            //  Получение имени каталога.
                            string directoryName = directoryInfo.Name.Trim().ToLower();

                            //  Проверка каталога данных датчика ускорения.
                            if (directoryName.StartsWith("adxl-"))
                            {
                                //  Получение IP-адреса датчика.
                                string ipAddress = directoryName[5..];

                                //  Сканирование файлов.
                                await ScanAdxlAsync(ipAddress, directoryInfo);
                            }

                            //  Проверка каталога данных геолокации.
                            else if (directoryName == "nmea")
                            {
                                //  Сканирование файлов.
                                await ScanNmeaAsync(directoryInfo);
                            }

                            //  Проверка каталога кадров.
                            else if (directoryName == "rawframes")
                            {
                                //  Сканирование файлов.
                                await ScanRawFramesAsync(directoryInfo);
                            }

                            //  Неизвестный каталог.
                            else
                            {
                                //  Вывод предупреждения.
                                logger.LogWarning(
                                    "Найден неизвестный каталог \"{path}\".",
                                    directoryInfo.FullName);
                            }
                        }

                        //  Сканирует файлы Adxl.
                        async Task ScanAdxlAsync(string ipAddress, DirectoryInfo directoryInfo)
                        {
                            //  Проверка токена отмены.
                            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                            //  Получение адреса датичка.
                            if (!AdxlData.TryFromIPAddress(ipAddress, out int address))
                            {
                                //  Вывод предупреждения.
                                logger.LogWarning(
                                    "Имя каталога \"{path}\" не содержит IP-адреса.",
                                    directoryInfo.FullName);

                                //  Завершение работы с каталогом.
                                return;
                            }

                            //  Создание данных.
                            AdxlData adxlData = new()
                            {
                                Address = address,
                            };


                            try
                            {
                                //  Выполнение запроса.
                                /*AdxlData adxlData =*/
                                await context.TransactionAsync(
                                    async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
                                    {
                                        ////  Поиск датчика в базе данных.
                                        //AdxlData? adxlData = await context.Adxls
                                        //    .FindAsync([address], cancellationToken).ConfigureAwait(false);

                                        ////  Проверка результата.
                                        //if (adxlData is null)
                                        //{
                                        //    //  Создание данных.
                                        //    adxlData = new()
                                        //    {
                                        //        Address = address,
                                        //    };

                                        //  Добавление данных.
                                        await context.Adxls.AddAsync(adxlData, cancellationToken).ConfigureAwait(false);
                                        //}

                                        ////  Возврат данных.
                                        //return adxlData;
                                    }, cancellationToken).ConfigureAwait(false);
                            }
                            catch { }

                            //  Перебор файлов в каталоге.
                            foreach (FileInfo fileInfo in directoryInfo.GetFiles("*", SearchOption.TopDirectoryOnly))
                            {
                                //  Получение имени файла.
                                string fileName = fileInfo.Name.ToLower();

                                //  Разбивка имени.
                                string[] parts = fileName.Split('-');

                                //  Проверка количества частей.
                                if (parts.Length != 9)
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" имеет недопустимый формат.",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Проверка расширения файла.
                                if (!parts[8].EndsWith(".adxl"))
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит расширения \".adxl\".",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Корректировка данных.
                                parts[8] = parts[8][..^5];

                                //  Проверка сигнатуры.
                                if (parts[0] != "adxl")
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит сигнатуры \"adxl\".",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Проверка IP-адреса.
                                if (!AdxlData.TryFromIPAddress(parts[1], out int fileAddress))
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит IP-адреса.",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Проверка принадлежности.
                                if (fileAddress != address)
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит требуемого IP-адреса: {address}.",
                                        fileInfo.FullName,
                                        ipAddress);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Разбор времени.
                                if (!int.TryParse(parts[2], out int year) ||
                                    !int.TryParse(parts[3], out int month) ||
                                    !int.TryParse(parts[4], out int day) ||
                                    !int.TryParse(parts[5], out int hour) ||
                                    !int.TryParse(parts[6], out int minute) ||
                                    !int.TryParse(parts[7], out int second) ||
                                    !int.TryParse(parts[8], out int millisecond))
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит времени.",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Проверка времени часового каталога.
                                if (directoryData.Timestamp != HourDirectoryData.FromTime(new DateTime(year, month, day, hour, 0, 0)))
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит требуемого времени каталога: {time}.",
                                        fileInfo.FullName,
                                        HourDirectoryData.ToTime(directoryData.Timestamp));

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Получение метки времени.
                                int timestamp = AdxlFileData.FromTime(new(0, 0, minute, second, millisecond));

                                try
                                {
                                    //  Выполнение транзакции.
                                    await context.TransactionAsync(
                                        async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
                                        {
                                            ////  Поиск файла в базе данных.
                                            //AdxlFileData? adxlFileData = await context.AdxlFiles
                                            //    .FindAsync([adxlData.Address, directoryData.Timestamp, timestamp], cancellationToken).ConfigureAwait(false);

                                            ////  Проверка результата.
                                            //if (adxlFileData is null)
                                            //{
                                            //  Создание данных.
                                            AdxlFileData adxlFileData = new()
                                            {
                                                AdxlAddress = adxlData.Address,
                                                HourDirectoryTimestamp = directoryData.Timestamp,
                                                Timestamp = timestamp,
                                            };

                                            //  Добавление данных.
                                            await context.AdxlFiles.AddAsync(adxlFileData, cancellationToken).ConfigureAwait(false);
                                            //}
                                        }, cancellationToken).ConfigureAwait(false);
                                }
                                catch { }

                            }
                        }

                        //  Сканирует файлы Nmea.
                        async Task ScanNmeaAsync(DirectoryInfo directoryInfo)
                        {
                            //  Проверка токена отмены.
                            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                            //  Перебор файлов в каталоге.
                            foreach (FileInfo fileInfo in directoryInfo.GetFiles("*", SearchOption.TopDirectoryOnly))
                            {
                                //  Получение имени файла.
                                string fileName = fileInfo.Name.ToLower();

                                //  Разбивка имени.
                                string[] parts = fileName.Split('-');

                                //  Проверка количества частей.
                                if (parts.Length != 6)
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" имеет недопустимый формат.",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Проверка расширения файла.
                                if (!parts[5].EndsWith(".nmea"))
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит расширения \".nmea\".",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Корректировка данных.
                                parts[5] = parts[5][..^5];

                                //  Проверка сигнатуры.
                                if (parts[0] != "nmea")
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит сигнатуры \"nmea\".",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Разбор времени.
                                if (!int.TryParse(parts[1], out int year) ||
                                    !int.TryParse(parts[2], out int month) ||
                                    !int.TryParse(parts[3], out int day) ||
                                    !int.TryParse(parts[4], out int hour) ||
                                    !byte.TryParse(parts[5], out byte minute))
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит времени.",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Проверка времени часового каталога.
                                if (directoryData.Timestamp != HourDirectoryData.FromTime(new DateTime(year, month, day, hour, 0, 0)))
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит требуемого времени каталога: {time}.",
                                        fileInfo.FullName,
                                        HourDirectoryData.ToTime(directoryData.Timestamp));

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                try
                                {
                                    //  Выполнение транзакции.
                                    await context.TransactionAsync(
                                        async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
                                        {
                                            ////  Поиск файла в базе данных.
                                            //NmeaFileData? nmeaFileData = await context.NmeaFiles
                                            //    .FindAsync([directoryData.Timestamp, minute], cancellationToken).ConfigureAwait(false);

                                            ////  Проверка результата.
                                            //if (nmeaFileData is null)
                                            //{
                                            //  Создание данных.
                                            NmeaFileData nmeaFileData = new()
                                            {
                                                HourDirectoryTimestamp = directoryData.Timestamp,
                                                Minute = minute,
                                            };

                                            //  Добавление данных.
                                            await context.NmeaFiles.AddAsync(nmeaFileData, cancellationToken).ConfigureAwait(false);
                                            //}
                                        }, cancellationToken).ConfigureAwait(false);
                                }
                                catch { }

                            }
                        }

                        //  Сканирует файлы RawFrames.
                        async Task ScanRawFramesAsync(DirectoryInfo directoryInfo)
                        {
                            //  Проверка токена отмены.
                            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                            //  Перебор файлов в каталоге.
                            foreach (FileInfo fileInfo in directoryInfo.GetFiles("*", SearchOption.TopDirectoryOnly))
                            {
                                //  Получение имени файла.
                                string fileName = fileInfo.Name.ToLower();

                                //  Проверка сигнатуры.
                                if (!fileName.StartsWith("vp000_0 "))
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит сигнатуры \"vp000_0 \".",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Корректировка имени файла.
                                fileName = fileName[8..];

                                //  Разбивка имени.
                                string[] parts = fileName.Split('-');

                                //  Проверка количества частей.
                                if (parts.Length != 7)
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" имеет недопустимый формат.",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Проверка расширения файла.
                                if (!parts[6].EndsWith(".0001"))
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит расширения \".0001\".",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Корректировка данных.
                                parts[6] = parts[6][..^5];

                                //  Разбор времени.
                                if (!int.TryParse(parts[0], out int year) ||
                                    !int.TryParse(parts[1], out int month) ||
                                    !int.TryParse(parts[2], out int day) ||
                                    !int.TryParse(parts[3], out int hour) ||
                                    !int.TryParse(parts[4], out int minute) ||
                                    !int.TryParse(parts[5], out int second) ||
                                    !int.TryParse(parts[6], out int millisecond))
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит времени.",
                                        fileInfo.FullName);

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Проверка времени часового каталога.
                                if (directoryData.Timestamp != HourDirectoryData.FromTime(new DateTime(year, month, day, hour, 0, 0)))
                                {
                                    //  Вывод предупреждения.
                                    logger.LogWarning(
                                        "Имя файла \"{path}\" не содержит требуемого времени каталога: {time}.",
                                        fileInfo.FullName,
                                        HourDirectoryData.ToTime(directoryData.Timestamp));

                                    //  Переход к следующему файлу.
                                    continue;
                                }

                                //  Получение метки времени.
                                int timestamp = RawFrameFileData.FromTime(new(0, 0, minute, second, millisecond));

                                try
                                {

                                    //  Выполнение транзакции.
                                    await context.TransactionAsync(
                                        async delegate (Aurora01StorageContext context, CancellationToken cancellationToken)
                                        {
                                            ////  Поиск файла в базе данных.
                                            //RawFrameFileData? rawFrameFileData = await context.RawFrameFiles
                                            //    .FindAsync([directoryData.Timestamp, timestamp], cancellationToken).ConfigureAwait(false);

                                            ////  Проверка результата.
                                            //if (rawFrameFileData is null)
                                            //{
                                            //  Создание данных.
                                            RawFrameFileData rawFrameFileData = new()
                                            {
                                                HourDirectoryTimestamp = directoryData.Timestamp,
                                                Timestamp = timestamp,
                                            };

                                            //  Добавление данных.
                                            await context.RawFrameFiles.AddAsync(rawFrameFileData, cancellationToken).ConfigureAwait(false);
                                            //}
                                        }, cancellationToken).ConfigureAwait(false);
                                }
                                catch { }

                            }
                        }
                    }
                    catch { }
                }).ConfigureAwait(false);
        }
    }
}
