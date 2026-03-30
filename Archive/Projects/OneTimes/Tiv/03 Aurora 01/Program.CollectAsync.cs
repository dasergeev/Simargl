using Simargl.Frames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simargl.Projects.OneTimes.Tiv.Aurora01;

partial class Program
{
    /// <summary>
    /// Асинхронно выполняет сборку кадров.
    /// </summary>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая сборку кадров.
    /// </returns>
    public static async Task CollectAsync(CancellationToken cancellationToken)
    {
        //  Создание сборщика кадров.
        FrameCollector frameCollector = new();

        //  Получение начального и конечного времени сбора кадров.
        DateTime begin = Tunnings.BeginTime;
        DateTime end = begin.AddDays(1);

        //  Получение корневых каталогов.
        string framesPath = Path.Combine(Tunnings.FramesPath, $"{begin:yyyy-MM-dd}");
        string tivPath = Path.Combine(Tunnings.TivPath, $"{begin:yyyy-MM-dd}");

        //  Проверка корневых каталогов.
        Directory.CreateDirectory(framesPath);
        Directory.CreateDirectory(tivPath);

        //  Номера файлов.
        int frameNumber = 0;
        int tivNumber = 0;

        //  Журнал.
        StringBuilder journal = new();

        //  Флаг вывода информации.
        bool isInfo = false;

        //  Перебор времён.
        for (DateTime time = begin; time < end; time += Tunnings.TargetFrameFragmentDuration)
        {
            //  Создание журнала.
            StringBuilder log = new();

            //  Добавление основной записи.
            log.AppendLine($"[{DateTime.Now}] Сборка кадра со временем начала {time}:");

            //  Корректировка номера кадра.
            ++frameNumber;

            //  Блок перехвата всех исключений.
            try
            {
                //  Сборка кадра.
                Frame frame = await frameCollector.CollectAsync(time, cancellationToken).ConfigureAwait(false);

                //  Получение флага действительных данных геолокации.
                bool isGeolocation = !frame.Channels["Valid_GPS"].Any(x => x == 0);
                log.AppendLine($"  Данные геолокации: {(isGeolocation ? "есть" : "нет")}.");

                //  Скорость движения.
                double speed = 0;

                //  Проверка действительных данных геолокации.
                if (isGeolocation)
                {
                    //  Получение скорости.
                    speed = frame.Channels["V_GPS"].Average();

                    //  Создание средства постороения текста.
                    StringBuilder output = new();

                    //  Создание списка каналов.
                    List<Channel> tivChannels = [];

                    //  Длина каналов для ТИВ.
                    int tivLength = 0;

                    //  Перебор каналов.
                    foreach (Channel channel in frame.Channels)
                    {
                        //  Флаг игнорирования.
                        bool isIgnore = false;

                        //  Перебор фильтров.
                        foreach (string ignore in Tunnings.TivIgnoreChannelNames)
                        {
                            //  Проверка фильтра.
                            if (channel.Name.Contains(ignore))
                            {
                                //  Установка флага.
                                isIgnore = true;

                                //  Завершение разбора фильтров.
                                break;
                            }
                        }

                        //  Проверка флага игнорирования.
                        if (!isIgnore)
                        {
                            //  Отбор канала.
                            tivChannels.Add(channel);

                            //  Корректировка длины.
                            tivLength = Math.Max(tivLength, channel.Length);
                        }
                    }

                    //  Проверка необходимости сохранения общей информации.
                    if (!isInfo)
                    {
                        //  Установка флага.
                        isInfo = true;

                        //  Добавление информации о времени.
                        output.AppendLine("t;s");

                        //  Перебор каналов.
                        foreach (Channel channel in tivChannels)
                        {
                            output.AppendLine($"{channel.Name};{channel.Unit}");
                        }

                        //  Сохранение информации.
                        File.WriteAllText(Path.Combine(tivPath, "_info.csv"), output.ToString());

                        //  Очистка средства посторения текста.
                        output.Clear();
                    }

                    //  Определение частоты дискретизации для ТИВ.
                    double tivSampling = tivLength / Tunnings.TargetFrameFragmentDuration.TotalSeconds;

                    //  Перебор значений.
                    for (int i = 0; i < tivLength; i++)
                    {
                        //  Определение времени.
                        double t = i / tivSampling;

                        //  Добавление времени в вывод.
                        output.Append(t);

                        //  Перебор каналов.
                        foreach (Channel channel in tivChannels)
                        {
                            //  Добавление разделителя.
                            output.Append(';');

                            //  Проверка частоты дискретизации.
                            if (channel.Sampling == tivSampling)
                            {
                                //  Добавление фактического значения.
                                output.Append(channel[i]);
                            }
                            else
                            {
                                //  Добавление данных геолокации.
                                output.Append(channel[(int)t]);
                            }
                        }

                        //  Переход на новую строку.
                        output.AppendLine();
                    }

                    //  Корректировка номера файла для ТИВ.
                    ++tivNumber;

                    //  Получение имени файла.
                    string fileName = $"{tivNumber:0000}.csv";

                    //  Сохранение файла.
                    File.WriteAllText(Path.Combine(tivPath, fileName), output.ToString());

                    //  Добавление информации в журнал.
                    journal.AppendLine($"{fileName};{time};{speed}");
                }

                //  Формирование имени кадра.
                string frameName = $"Vp{speed:000.0}".Replace('.', '_').Replace(',', '_') + $" {time:HH:mm}".Replace(':', '_')
                    + (!isGeolocation ? " [NO GPS]" : string.Empty)
                    + $".{frameNumber:0000}";

                //  Сохранение кадра.
                frame.Save(Path.Combine(framesPath, frameName), StorageFormat.TestLab);
            }
            catch (Exception ex)
            {
                //  Добавление информации в журнал.
                log.AppendLine("ПРОИЗОШЛА ОШИБКА:");
                log.AppendLine(ex.ToString());
            }

            //  Вывод журнала в консоль.
            Console.WriteLine(log);
            File.AppendAllText(Tunnings.LogPath, log.ToString());
        }

        //  Запись журнала.
        File.WriteAllText(Path.Combine(tivPath, "_journal.csv"), journal.ToString());

    }
}
