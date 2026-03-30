using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Simargl.Frames;
using Simargl.Frames.OldStyle;
using Simargl.Trials.Aurora.Aurora01.Gluer.Core;
using System.IO;

namespace Simargl.Trials.Aurora.Aurora01.Gluer;

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
        //  Ожидание инициализации консоли.
        await Task.Delay(1000, cancellationToken).ConfigureAwait(false);

        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Вывод сообщения в журнал.
        logger.LogInformation("Начало работы.");

        //  Блок перехвата всех исключений.
        try
        {
            //  Проверка сборки кадров.
            if (!GluerTunnings.IsConvertOnly)
            {

                //  Вывод в консоль.
                Console.WriteLine($"Склейка данных за {GluerTunnings.Date}.");
                Console.WriteLine($"Корневой каталог {GluerTunnings.RawDataPath}.");

                //  Создание карты файлов.
                FileMap fileMap = await FileMap.CreateAsync(
                    GluerTunnings.RawDataPath, GluerTunnings.Date, GluerTunnings.AdxlAddresses, cancellationToken)
                    .ConfigureAwait(false);

                //  Вывод в консоль.
                Console.WriteLine("Загрузка данных геолокации.");

                //  Загрузка данных геолокации.
                Geolocation geolocation = await Geolocation.CreateAsync(fileMap, cancellationToken).ConfigureAwait(false);

                //  Вывод в консоль.
                Console.WriteLine("Экспорт данных геолокации.");

                //  Экспорт данных.
                await geolocation.ExportAsync(GluerTunnings.ExportPath, cancellationToken).ConfigureAwait(false);

                //  Вывод в консоль.
                Console.WriteLine($"  Смещение времени по GPS: {geolocation.Displacement}");
                Console.WriteLine($"  Данные GPS: {geolocation.Latitude.Where(x => x > 0).Count() * 100.0 / geolocation.Latitude.Length:0.00}%");

                //  Вывод в консоль.
                Console.WriteLine("Экспорт информации о файлах.");

                //  Экспорт данных.
                await fileMap.ExportAsync(GluerTunnings.ExportPath, cancellationToken).ConfigureAwait(false);

                //  Вывод в консоль.
                Console.WriteLine("Получение непрерывных фрагментов:");

                //  Получение непрерывных фрагментов.
                FragmentMap fragmentMap = await FragmentMap.CreateAsync(fileMap, cancellationToken).ConfigureAwait(false);

                Console.WriteLine($"  frame = {fragmentMap.Frame.Length} ({fragmentMap.Frame.Sum(x => x.Files.Length)} из {fileMap.Frame.Count})");
                foreach (var item in fragmentMap.Frame)
                {
                    Console.WriteLine($"    {item.BeginTime} - {item.EndTime} : {item.Duration}");
                }
                for (int i = 0; i < fragmentMap.Adxl.Length; i++)
                {
                    Console.WriteLine($"  adxl[{i}] = {fragmentMap.Adxl[i].Length} ({fragmentMap.Adxl[i].Sum(x => x.Files.Length)} из {fileMap.Adxl[i].Count})");
                    foreach (var item in fragmentMap.Adxl[i])
                    {
                        Console.WriteLine($"    {item.BeginTime} - {item.EndTime} : {item.Duration}");
                    }
                }

                //  Вывод в консоль.
                Console.WriteLine("Сборка кадров:");

                //  Создание сборщика кадров.
                Collector collector = new(geolocation, fragmentMap);

                //  Флаг первого кадра.
                bool isFirst = true;

                //  Массив нулевых значений.
                double[] zero = new double[GluerTunnings.RawChannels.Length];

                //  Перебор индексов кадров.
                for (int minute = 0; minute < 1440; minute++)
                {
                    //  Проверка токена отмены.
                    await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

                    //  Создание построителя строки.
                    StringBuilder output = new();
                    output.Append($"  [{minute}]: ");

                    //  Кадр.
                    Frame? frame = null;

                    //  Блок перехвата всех исключений.
                    try
                    {
                        //  Загрузка кадра.
                        frame = await collector.CollectAsync(minute, cancellationToken).ConfigureAwait(false);

                        //  Проверка кадра.
                        if (frame is null)
                        {
                            output.Append("empty");
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

                        output.Append(ex.ToString());
                    }

                    //  Проверка кадра.
                    if (frame is not null)
                    {
                        //  Определение средней скорости.
                        double speed = frame.Channels["V_GPS"].Items.Average();

                        //  Проверка необходимости определить нули.
                        if (speed == 0 || isFirst)
                        {
                            //  Перебор тензометрических каналов.
                            for (int i = 0; i < GluerTunnings.RawChannels.Length; i++)
                            {
                                //  Получение информации о канале.
                                var info = GluerTunnings.RawChannels[i];

                                //  Получение канала.
                                Channel channel = frame.Channels[info.Name];

                                //  Определение нуля канала.
                                zero[i] = channel.Items.Average();
                            }
                        }

                        //  Сброс флага первого кадра.
                        isFirst = false;

                        //  Перебор тензометрических каналов.
                        for (int i = 0; i < GluerTunnings.RawChannels.Length; i++)
                        {
                            //  Получение информации о канале.
                            var info = GluerTunnings.RawChannels[i];

                            //  Получение канала.
                            Channel channel = frame.Channels[info.Name];

                            //  Смещение нуля.
                            channel.Move(-zero[i]);
                        }

                        //  Формирование пути к файлу.
                        string path = Path.Combine(
                            GluerTunnings.RecordsPath,
                            $"{GluerTunnings.Date:yyyy-MM-dd}",
                            $"Vp{speed:000}_0.{minute:0000}");

                        //  Проверка каталога.
                        new FileInfo(path).Directory!.Create();

                        //  Сохранение кадра.
                        frame.Save(path, StorageFormat.TestLab);

                        //break;
                    }

                    //  Вывод в консоль.
                    Console.WriteLine(output.ToString());
                }
            }

            //  Вывод в консоль.
            Console.WriteLine("Конвертация:");

            //  Выполнение конвертации.
            await Converter.ConvertAsync(cancellationToken).ConfigureAwait(false);
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

        //  Вывод сообщения в журнал.
        logger.LogInformation("Конец работы.");
    }
}
