using Simargl.Algebra;
using Simargl.Analysis;
using Simargl.Frames;
using Simargl.Frames.OldStyle;
using Simargl.Frames.TestLab;
using Simargl.Recording.AccelEth3T;
using Simargl.Recording.Geolocation.Nmea;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Simargl.Projects.OneTimes.Tiv.Aurora01;

/// <summary>
/// Представляет сборщик кадров.
/// </summary>
public sealed class FrameCollector
{
    /// <summary>
    /// Асинхронно собирает кадр.
    /// </summary>
    /// <param name="time">
    /// Время начала кадра.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, собирающая кадр.
    /// </returns>
    public async Task<Frame> CollectAsync(DateTime time, CancellationToken cancellationToken)
    {
        //  Создание кадра.
        Frame frame = new();

        //  Список вспомогательных каналов.
        List<Channel> supports = [];

        //  Загрузка каналов геолокации.
        await LoadNmeaChannelsAsync(time, supports, cancellationToken).ConfigureAwait(false);

        //  Загрузка основных каналов.
        await LoadMainChannelsAsync(time, frame, supports, cancellationToken).ConfigureAwait(false);

        //  Возврат кадра.
        return frame;
    }

    /// <summary>
    /// Асинхронно загружает каналы геолокации.
    /// </summary>
    /// <param name="time">
    /// Время начала кадра.
    /// </param>
    /// <param name="supports">
    /// Список вспомогательных каналов.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая основные каналы.
    /// </returns>
    private async Task LoadNmeaChannelsAsync(DateTime time, List<Channel> supports, CancellationToken cancellationToken)
    {
        //  Для анализатора.
        _ = this;
        await Task.CompletedTask.ConfigureAwait(false);

        //  Получение длины каналов.
        int length = (int)Tunnings.TargetFrameFragmentDuration.TotalSeconds;

        //  Создание каналов.
        Channel valid = new("Valid_GPS", string.Empty, 1, 0, length);
        Channel speed = new("V_GPS", "kmph", 1, 0, length);
        Channel altitude = new("A_GPS", "m", 1, 0, length);
        Channel latitude = new("Lat_GPS", "°", 1, 0, length);
        Channel longitude = new("Lon_GPS", "°", 1, 0, length);
        Channel knots = new("Knots_GPS", "kn", 1, 0, length);
        Channel poleCourse = new("PCourse_GPS", "°", 1, 0, length);
        Channel magneticCourse = new("MCourse_GPS", "°", 1, 0, length);
        Channel satellites = new("Satell_GPS", string.Empty, 1, 0, length);
        Channel hdop = new("Hdop_GPS", string.Empty, 1, 0, length);
        Channel age = new("Age_GPS", "s", 1, 0, length);

        //  Создание списка каналов.
        List<Channel> channels = [
            valid, speed, altitude, latitude, longitude,
            knots, poleCourse, magneticCourse,
            satellites, hdop, age,
        ];

        //  Добавление каналов в основной список.
        supports.AddRange(channels);

        //  Перебор каналов.
        foreach (Channel channel in channels)
        {
            //  Перебор значений.
            for (int i = 0; i < length; i++)
            {
                //  Установка недействительного значения.
                channel[i] = double.NaN;
            }
        }

        //  Определение начального и конечного времени.
        DateTime beginTime = time;
        DateTime endTime = time + Tunnings.TargetFrameFragmentDuration + TimeSpan.FromSeconds(1);

        //  Поиск файлов.
        List<(string Path, DateTime Time)> files = FindFiles(
            "Nmea",
            beginTime - 3 * Tunnings.NmeaDuration,
            endTime + 3 * Tunnings.NmeaDuration);

        //  Перебор файлов.
        foreach ((string Path, DateTime Time) in files)
        {
            //  Загрузка данных.
            IEnumerable<NmeaMessage> messages = (await File.ReadAllLinesAsync(Path, cancellationToken).ConfigureAwait(false))
                .Select(x =>
                {
                    try { return NmeaMessage.Parse(x); }
                    catch { return null!; }
                })
                .Where(x => x is not null);

            //  Получение специальных сообщений.
            List<NmeaGgaMessage> ggaMessages = [.. messages.Where(x => x is NmeaGgaMessage).Select(x => (NmeaGgaMessage)x)];
            List<NmeaRmcMessage> rmcMessages = [.. messages.Where(x => x is NmeaRmcMessage).Select(x => (NmeaRmcMessage)x)];
            List<NmeaVtgMessage> vtgMessages = [.. messages.Where(x => x is NmeaVtgMessage).Select(x => (NmeaVtgMessage)x)];

            //  Перебор GGA сообщений.
            if (ggaMessages.Count > 0.75 * Tunnings.TargetFrameFragmentDuration.TotalSeconds)
            {
                //  Определение шага сообщений.
                TimeSpan step = Tunnings.NmeaDuration / ggaMessages.Count;

                //  Время сообщения.
                DateTime messageTime = Time;

                //  Перебор сообщений.
                foreach (NmeaGgaMessage ggaMessage in ggaMessages)
                {
                    //  Определение индекса.
                    int index = (int)Math.Round((messageTime - beginTime).TotalSeconds);

                    //  Проверка индекса.
                    if (0 <= index && index < length)
                    {
                        if (ggaMessage.Latitude.HasValue) latitude[index] = ggaMessage.Latitude.Value;
                        if (ggaMessage.Longitude.HasValue) longitude[index] = ggaMessage.Longitude.Value;
                        if (ggaMessage.Satellites.HasValue) satellites[index] = ggaMessage.Satellites.Value;
                        if (ggaMessage.Hdop.HasValue) hdop[index] = ggaMessage.Hdop.Value;
                        if (ggaMessage.Altitude.HasValue) altitude[index] = ggaMessage.Altitude.Value;
                        if (ggaMessage.Age.HasValue) age[index] = ggaMessage.Age.Value;
                    }

                    //  Смещение времени.
                    messageTime += step;
                }
            }

            //  Перебор RMC сообщений.
            if (rmcMessages.Count > 0.75 * Tunnings.TargetFrameFragmentDuration.TotalSeconds)
            {
                //  Определение шага сообщений.
                TimeSpan step = Tunnings.NmeaDuration / rmcMessages.Count;

                //  Время сообщения.
                DateTime messageTime = Time;

                //  Перебор сообщений.
                foreach (NmeaRmcMessage rmcMessage in rmcMessages)
                {
                    //  Определение индекса.
                    int index = (int)Math.Round((messageTime - beginTime).TotalSeconds);

                    //  Проверка индекса.
                    if (0 <= index && index < length)
                    {
                        if (rmcMessage.Latitude.HasValue) latitude[index] = rmcMessage.Latitude.Value;
                        if (rmcMessage.Longitude.HasValue) longitude[index] = rmcMessage.Longitude.Value;
                        if (rmcMessage.Knots.HasValue) knots[index] = rmcMessage.Knots.Value;
                        if (rmcMessage.Speed.HasValue) speed[index] = rmcMessage.Speed.Value;
                        if (rmcMessage.PoleCourse.HasValue) poleCourse[index] = rmcMessage.PoleCourse.Value;
                        if (rmcMessage.MagneticCourse.HasValue) magneticCourse[index] = rmcMessage.MagneticCourse.Value;
                    }

                    //  Смещение времени.
                    messageTime += step;
                }
            }

            //  Перебор VTG сообщений.
            if (vtgMessages.Count > 0.75 * Tunnings.TargetFrameFragmentDuration.TotalSeconds)
            {
                //  Определение шага сообщений.
                TimeSpan step = Tunnings.NmeaDuration / vtgMessages.Count;

                //  Время сообщения.
                DateTime messageTime = Time;

                //  Перебор сообщений.
                foreach (NmeaVtgMessage vtgMessage in vtgMessages)
                {
                    //  Определение индекса.
                    int index = (int)Math.Round((messageTime - beginTime).TotalSeconds);

                    //  Проверка индекса.
                    if (0 <= index && index < length)
                    {
                        if (vtgMessage.PoleCourse.HasValue) poleCourse[index] = vtgMessage.PoleCourse.Value;
                        if (vtgMessage.MagneticCourse.HasValue) magneticCourse[index] = vtgMessage.MagneticCourse.Value;
                        if (vtgMessage.Knots.HasValue) knots[index] = vtgMessage.Knots.Value;
                        if (vtgMessage.Speed.HasValue) speed[index] = vtgMessage.Speed.Value;
                    }

                    //  Смещение времени.
                    messageTime += step;
                }
            }
        }

        //  Перебор каналов.
        foreach (Channel channel in channels)
        {
            //  Подсчёт недействительных значений.
            int count = channel.Count(x => !double.IsFinite(x));

            //  Проверка недействительных значений.
            if (count == 0 || count > 0.5 * Tunnings.TargetFrameFragmentDuration.TotalSeconds)
            {
                //  Переход к следующему каналу.
                continue;
            }

            //  Перебор значений.
            for (int i = 0; i < length; i++)
            {
                //  Проверка значения.
                if (!double.IsFinite(channel[i]))
                {
                    //  Поиск предыдущего значения.
                    for (int j = i - 1; j >= 0; j--)
                    {
                        //  Проверка значения.
                        if (double.IsFinite(channel[j]))
                        {
                            //  Установка значения.
                            channel[i] = channel[j];

                            //  Завершение поиска.
                            break;
                        }
                    }
                }

                //  Проверка значения.
                if (!double.IsFinite(channel[i]))
                {
                    //  Поиск следующего значения.
                    for (int j = i + 1; j < length; j++)
                    {
                        //  Проверка значения.
                        if (double.IsFinite(channel[j]))
                        {
                            //  Установка значения.
                            channel[i] = channel[j];

                            //  Завершение поиска.
                            break;
                        }
                    }
                }
            }
        }

        //  Флаг действительных значений.
        bool isValid = !speed.Any(x => !double.IsFinite(x)) &&
            !altitude.Any(x => !double.IsFinite(x)) &&
            !latitude.Any(x => !double.IsFinite(x)) &&
            !longitude.Any(x => !double.IsFinite(x));

        //  Проверка флага действительных значений.
        if (isValid)
        {
            //  Перебор значений.
            for (int i = 0; i < length; i++)
            {
                //  Установка значения канала действительных данных.
                valid[i] = 1;
            }
        }

        //  Перебор каналов.
        foreach (Channel channel in channels)
        {
            //  Перебор значений.
            for (int i = 0; i < length; i++)
            {
                //  Проверка значения.
                if (!double.IsFinite(channel[i]))
                {
                    //  Сброс недействительного значения.
                    channel[i] = 0;
                }
            }
        }
    }

    /// <summary>
    /// Асинхронно загружает основные каналы.
    /// </summary>
    /// <param name="time">
    /// Время начала кадра.
    /// </param>
    /// <param name="frame">
    /// Кадр.
    /// </param>
    /// <param name="supports">
    /// Список вспомогательных каналов.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая основные каналы.
    /// </returns>
    private async Task LoadMainChannelsAsync(DateTime time, Frame frame, List<Channel> supports, CancellationToken cancellationToken)
    {
        //  Для анализатора.
        _ = this;

        //  Определение начального и конечного времени.
        DateTime beginTime = time;
        DateTime endTime = time + Tunnings.TargetFrameFragmentDuration + TimeSpan.FromSeconds(1);

        //  Перебор датчиков.
        foreach (AdxlInfo adxlInfo in Tunnings.FirstAdxl)
        {
            //  Поиск файлов.
            List<(string Path, DateTime Time)> files = FindFiles(
                "Adxl-" + adxlInfo.Address,
                beginTime - 3 * Tunnings.AdxlFragmentDuration,
                endTime + 3 * Tunnings.AdxlFragmentDuration);

            //  Фрагменты потока данных.
            List<AccelEth3TStreamFragment> fragments = [];

            //  Загрузка данных.
            foreach ((string Path, DateTime Time) in files)
            {
                //  Загрузка фрагмента.
                fragments.Add(await AccelEth3TStreamFragment.LoadAsync(
                    Path, Tunnings.AdxlPeriod, Time, cancellationToken)
                    .ConfigureAwait(false));
            }

            //  Объединение данных.
            AccelEth3TStreamFragment fragment = AccelEth3TStreamFragment.Join(fragments, Tunnings.AdxlMaxGap);

            //  Определение длительности загружаемых данных.
            TimeSpan duration = Tunnings.TargetFrameFragmentDuration + TimeSpan.FromSeconds(1);

            //  Каналы.
            List<Channel> channels = [];

            //  Перебор направлений.
            foreach (var item in new[]
            {
                new
                {
                    GetData = new Func<DateTime, TimeSpan, double[]>(fragment.GetXData),
                    Name = adxlInfo.XName,
                    Scale = adxlInfo.XScale,
                    Offset = adxlInfo.XOffset,
                },
                new
                {
                    GetData = new Func<DateTime, TimeSpan, double[]>(fragment.GetYData),
                    Name = adxlInfo.YName,
                    Scale = adxlInfo.YScale,
                    Offset = adxlInfo.YOffset,
                },
                new
                {
                    GetData = new Func<DateTime, TimeSpan, double[]>(fragment.GetZData),
                    Name = adxlInfo.ZName,
                    Scale = adxlInfo.ZScale,
                    Offset = adxlInfo.ZOffset,
                },
            })
            {
                //  Получение данных.
                double[] data = item.GetData(time, duration);

                //  Определение частоты дискретизации.
                double sampling = data.Length / duration.TotalSeconds;

                //  Определение длины канала.
                int length = (int)Math.Floor(Tunnings.TargetFrameFragmentDuration.TotalSeconds * sampling);

                //  Изменение размера данных.
                Array.Resize(ref data, length);

                //  Перебор данных.
                for (int i = 0; i < length; i++)
                {
                    //  Корректировка значения.
                    data[i] = item.Scale * data[i] + item.Offset;
                }

                //  Создание канала.
                Channel channel = new(
                    new TestLabChannelHeader(item.Name, "g", 0.25 * sampling),
                    new Signal(sampling, data));

                //  Добавление канала.
                channels.Add(channel);
            }

            //  Добавление каналов в кадр.
            frame.Channels.Add(channels.Where(x => x.Name.Contains('x', StringComparison.CurrentCultureIgnoreCase)).First());
            frame.Channels.Add(channels.Where(x => x.Name.Contains('y', StringComparison.CurrentCultureIgnoreCase)).First());
            frame.Channels.Add(channels.Where(x => x.Name.Contains('z', StringComparison.CurrentCultureIgnoreCase)).First());

            //  Перебор вспомогательных каналов.
            foreach (var item in new[]
            {
                new
                {
                    Index = 0,
                    Name = adxlInfo.Number + Tunnings.CpuTempPostfix,
                    Unit = "°C",
                },
                new
                {
                    Index = 1,
                    Name = adxlInfo.Number + Tunnings.TempPostfix,
                    Unit = "°C",
                },
                new
                {
                    Index = 2,
                    Name = adxlInfo.Number + Tunnings.CpuVoltagePostfix,
                    Unit = "mV",
                },
            })
            {
                //  Получение данных.
                double[] data = fragment.GetAsyncData(time, duration, item.Index);

                //  Определение частоты дискретизации.
                double sampling = data.Length / duration.TotalSeconds;

                //  Определение длины канала.
                int length = (int)Math.Floor(Tunnings.TargetFrameFragmentDuration.TotalSeconds * sampling);

                //  Изменение размера данных.
                Array.Resize(ref data, length);

                //  Создание канала.
                Channel channel = new(
                    new TestLabChannelHeader(item.Name, "g", 0.25 * sampling),
                    new Signal(sampling, data));

                //  Добавление вспомогательного канала.
                supports.Add(channel);
            }
        }

        //  Перебор вспомогательных каналов.
        foreach (Channel channel in supports)
        {
            //  Добавление канала в кадр.
            frame.Channels.Add(channel);
        }
    }

    /// <summary>
    /// Выполняет поиск файлов.
    /// </summary>
    /// <param name="label">
    /// Метка данных.
    /// </param>
    /// <param name="beginTime">
    /// Начальное время.
    /// </param>
    /// <param name="endTime">
    /// Конечное время.
    /// </param>
    /// <returns>
    /// Список данных о файлах.
    /// </returns>
    private static List<(string Path, DateTime Time)> FindFiles(string label, DateTime beginTime, DateTime endTime)
    {
        //  Формирование путей к каталогам с данными.
        HashSet<string> directories =
        [
            Path.Combine(Tunnings.RawDataPath, getName(beginTime.AddDays(-1)), label),
            Path.Combine(Tunnings.RawDataPath, getName(beginTime), label),
            Path.Combine(Tunnings.RawDataPath, getName(endTime), label),
            Path.Combine(Tunnings.RawDataPath, getName(endTime.AddDays(1)), label),
        ];

        //  Набор путей.
        HashSet<string> paths = [];

        //  Перебор путей.
        foreach (string directory in directories)
        {
            //  Проверка каталога.
            if (Directory.Exists(directory))
            {
                //  Получение файлов.
                foreach(string path in Directory.GetFiles(directory, "*", SearchOption.TopDirectoryOnly))
                {
                    //  Добавление пути.
                    paths.Add(path);
                }
            }
        }

        //  Возврат списка данных о файлах.
        return [.. paths
            .Select(x => (x, GetTime(x)))
            .Where(x => beginTime <= x.Item2 && x.Item2 < endTime)
            .OrderBy(x => x.Item2)];

        //  Возвращает имя каталога.
        static string getName(DateTime time) => $"{time.Year:0000}-{time.Month:00}-{time.Day:00}-{time.Hour:00}";
    }

    /// <summary>
    /// Возвращает время файла.
    /// </summary>
    /// <param name="path">
    /// Путь к файлу.
    /// </param>
    /// <returns>
    /// Время файла.
    /// </returns>
    private static DateTime GetTime(string path)
    {
        //  Получение информации о файле.
        FileInfo fileInfo = new(path);

        //  Получение расширения.
        string extension = fileInfo.Extension.ToLower();

        //  Получение частей имени.
        string[] parts = fileInfo.Name[..^extension.Length].Split('-');

        //  Данные.
        int year;
        int month;
        int day;
        int hour;
        int minute;
        int second = 0; 
        int millisecond = 0;

        //  Разбор расширения.
        switch (extension)
        {
            case ".adxl":
                //  0    1             2    3  4  5  6  7  8
                //  Adxl-192.168.1.107-2025-01-12-00-02-16-947.adxl

                //  Разбор данных.
                year = int.Parse(parts[2]);
                month = int.Parse(parts[3]);
                day = int.Parse(parts[4]);
                hour = int.Parse(parts[5]);
                minute = int.Parse(parts[6]);
                second = int.Parse(parts[7]);
                millisecond = int.Parse(parts[8]);

                //  Завершение разбора.
                break;
            case ".nmea":
                //  0    1    2  3  4  5
                //  Nmea-2025-01-12-00-13.nmea

                //  Разбор данных.
                year = int.Parse(parts[1]);
                month = int.Parse(parts[2]);
                day = int.Parse(parts[3]);
                hour = int.Parse(parts[4]);
                minute = int.Parse(parts[5]);

                //  Завершение разбора.
                break;
            default:
                throw new InvalidDataException("Неизвестное расширение файла.");
        }

        //  Возврат времени.
        return new(year, month, day, hour, minute, second, millisecond);
    }
}
