using Simargl.Recording.Geolocation;
using Simargl.Recording.Geolocation.Nmea;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Simargl.Trials.Aurora.Aurora01.Gluer.Core;

/// <summary>
/// Представляет данные геолокации.
/// </summary>
public sealed class Geolocation
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    private Geolocation(
        List<NmeaMessageInfo<NmeaGgaMessage>> gga,
        List<NmeaMessageInfo<NmeaVtgMessage>> vtg,
        List<NmeaMessageInfo<NmeaRmcMessage>> rmc)
    {
        //  Установка списков.
        Gga = gga;
        Vtg = vtg;
        Rmc = rmc;

        //  Определение среднего смещения времени.
        Displacement = GetDisplacement(TimeSpan.Zero, TimeSpan.MaxValue);

        //  Карта данных.
        SortedDictionary<int, (double Speed, double Latitude, double Longitude)> map = [];

        //  Перебор данных.
        foreach (NmeaMessageInfo<NmeaRmcMessage> info in Rmc)
        {
            //  Проверка данных.
            if (info.Message.Valid.HasValue &&
                info.Message.Valid.Value &&
                info.Message.Speed.HasValue &&
                info.Message.Latitude.HasValue &&
                info.Message.Longitude.HasValue &&
                info.Message.Date.HasValue &&
                info.Message.Time.HasValue)
            {
                //  Определение индекса.
                int index = (int)(((info.Message.Date.Value.ToDateTime(info.Message.Time.Value).AddYears(2000) + Displacement) - GluerTunnings.Date.ToDateTime(default)).TotalSeconds);

                //  Добавление в карту.
                map.TryAdd(index, (info.Message.Speed.Value, info.Message.Latitude.Value, info.Message.Longitude.Value));
            }
        }

        //  Получение индексов.
        int[] indexes = [.. map.Keys];

        //  Создание массивов данных.
        Speed = new double[86400];
        Latitude = new double[Speed.Length];
        Longitude = new double[Speed.Length];

        //  Перебор индексов.
        for (int i = 1; i < indexes.Length; i++)
        {
            //  Получение индексов.
            int firstIndex = indexes[i - 1];
            int secondIndex = indexes[i];

            //  Проверка разрыва.
            if (secondIndex - firstIndex < GluerTunnings.GpsGap.TotalSeconds)
            {
                //  Получение данных.
                (double firstSpeed, double firstLatitude, double firstLongitude) = map[firstIndex];
                (double secondSpeed, double secondLatitude, double secondLongitude) = map[secondIndex];

                //  Определение коэффициентов.
                double speedScale = (secondSpeed - firstSpeed) / (secondIndex - firstIndex);
                double latitudeScale = (secondLatitude - firstLatitude) / (secondIndex - firstIndex);
                double longitudeScale = (secondLongitude - firstLongitude) / (secondIndex - firstIndex);
                double speedOffset = firstSpeed - speedScale * firstIndex;
                double latitudeOffset = firstLatitude - latitudeScale * firstIndex;
                double longitudeOffset = firstLongitude - longitudeScale * firstIndex;

                //  Перебор индексов.
                for (int index = firstIndex; index < secondIndex; index++)
                {
                    //  Проверка индекса.
                    if (0 <= index && index < Speed.Length)
                    {
                        //  Установка значений.
                        Speed[index] = speedScale * index + speedOffset;
                        Latitude[index] = latitudeScale * index + latitudeOffset;
                        Longitude[index] = longitudeScale * index + longitudeOffset;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Возвращает список информации GGA.
    /// </summary>
    public List<NmeaMessageInfo<NmeaGgaMessage>> Gga { get; }

    /// <summary>
    /// Возвращает список информации VTG.
    /// </summary>
    public List<NmeaMessageInfo<NmeaVtgMessage>> Vtg { get; }

    /// <summary>
    /// Возвращает список информации RMC.
    /// </summary>
    public List<NmeaMessageInfo<NmeaRmcMessage>> Rmc { get; }

    /// <summary>
    /// Возвращает массив скоростей.
    /// </summary>
    public double[] Speed { get; }

    /// <summary>
    /// Возвращает массив значений широты.
    /// </summary>
    public double[] Latitude { get; }

    /// <summary>
    /// Возвращает массив значений долготы.
    /// </summary>
    public double[] Longitude { get; }

    /// <summary>
    /// Возвращает среднее смещение времени.
    /// </summary>
    public TimeSpan Displacement { get; }

    /// <summary>
    /// Возвращает среднее смещение времени.
    /// </summary>
    /// <param name="average">
    /// Базовое смещение.
    /// </param>
    /// <param name="level">
    /// Уровень отсева.
    /// </param>
    /// <returns>
    /// Среднее смещение.
    /// </returns>
    private TimeSpan GetDisplacement(TimeSpan average, TimeSpan level)
    {
        //  Разбор данных.
        return TimeSpan.FromSeconds(Rmc
            .Where(x => x.Count == 60 && x.Message.Date.HasValue && x.Message.Time.HasValue)
            .Select(x => new
            {
                ReceiptTime = x.BeginTime.AddSeconds(x.Index),
                GpsTime = x.Message.Date!.Value.ToDateTime(x.Message.Time!.Value).AddYears(2000),
            })
            .Select(x => (x.ReceiptTime - x.GpsTime - average).TotalSeconds)
            .Where(x => Math.Abs(x) < level.TotalSeconds)
            .Average());
    }

    /// <summary>
    /// Асинхронно создаёт данные геолокации.
    /// </summary>
    /// <param name="fileMap">
    /// Карта файлов.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая данные геолокации.
    /// </returns>
    public static async Task<Geolocation> CreateAsync(FileMap fileMap, CancellationToken cancellationToken)
    {
        //  Создание списков данных.
        List<NmeaMessageInfo<NmeaGgaMessage>> ggaAll = [];
        List<NmeaMessageInfo<NmeaVtgMessage>> vtgAll = [];
        List<NmeaMessageInfo<NmeaRmcMessage>> rmcAll = [];

        //  Перебор файлов.
        foreach (KeyValuePair<DateTime, FileInfo> pair in fileMap.Nmea)
        {
            //  Получение времени начала записи файла.
            DateTime beginTime = pair.Key;

            //  Получение информации о файле.
            FileInfo fileInfo = pair.Value;

            //  Получение времён файла.
            DateTime creationTime = fileInfo.CreationTime;
            DateTime lastAccessTime = fileInfo.LastAccessTime;
            DateTime lastWriteTime = fileInfo.LastWriteTime;

            //  Загрузка данных.
            string[] lines = (await File.ReadAllLinesAsync(fileInfo.FullName, cancellationToken).ConfigureAwait(false))
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

            //  Создание списка сообщений.
            List<NmeaGgaMessage> gga = [];
            List<NmeaVtgMessage> vtg = [];
            List<NmeaRmcMessage> rmc = [];

            //  Перебор строк.
            foreach (string line in lines)
            {
                //  Блок перехвата всех исключений.
                try
                {
                    //  Разбор сообщения.
                    NmeaMessage message = NmeaMessage.Parse(line);

                    //  Разбор сообщения.
                    switch (message.Mnemonics)
                    {
                        case NmeaMnemonics.Gga:
                            //  Добавление сообщения.
                            gga.Add((NmeaGgaMessage)message);

                            //  Завершение разбора.
                            break;
                        case NmeaMnemonics.Vtg:
                            //  Добавление сообщения.
                            vtg.Add((NmeaVtgMessage)message);

                            //  Завершение разбора.
                            break;
                        case NmeaMnemonics.Rmc:
                            //  Добавление сообщения.
                            rmc.Add((NmeaRmcMessage)message);

                            //  Завершение разбора.
                            break;
                        case NmeaMnemonics.Unknown:
                        default:
                            //  Завершение разбора.
                            break;
                    }
                }
                catch { }
            }

            //  Заполнение списка.
            for (int i = 0; i < gga.Count; i++)
            {
                //  Добавление информации в список.
                ggaAll.Add(new(gga[i], beginTime, creationTime, lastAccessTime, lastWriteTime, i, gga.Count));
            }

            //  Заполнение списка.
            for (int i = 0; i < vtg.Count; i++)
            {
                //  Добавление информации в список.
                vtgAll.Add(new(vtg[i], beginTime, creationTime, lastAccessTime, lastWriteTime, i, vtg.Count));
            }

            //  Заполнение списка.
            for (int i = 0; i < rmc.Count; i++)
            {
                //  Добавление информации в список.
                rmcAll.Add(new(rmc[i], beginTime, creationTime, lastAccessTime, lastWriteTime, i, rmc.Count));
            }
        }

        //  Возврат данных.
        return new(ggaAll, vtgAll, rmcAll);
    }

    /// <summary>
    /// Асинхронно выполняет экспорт данных.
    /// </summary>
    /// <param name="path">
    /// Путь к каталогу для записи данных.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая экспорт данных.
    /// </returns>
    public async Task ExportAsync(string path, CancellationToken cancellationToken)
    {
        //  Создание построителя текста.
        StringBuilder builder = new();

        //  Очистка построителя текста.
        builder.Clear();

        //  Запись заголовка.
        builder.Append(
            "Time;Latitude;Longitude;Solution;Satellites;Hdop;Altitude;Geoidal;Age;Station;" +
            "BeginTime;CreationTime;LastAccessTime;LastWriteTime;" +
            "Index;Count");

        //  Перебор данных.
        foreach (NmeaMessageInfo<NmeaGgaMessage> info in Gga)
        {
            //  Переход на новую строку.
            builder.AppendLine();

            //  Запись основной информации.
            writeTime(info.Message.Time);
            writeDouble(info.Message.Latitude);
            writeDouble(info.Message.Longitude);
            writeSolution(info.Message.Solution);
            writeInt(info.Message.Satellites);
            writeDouble(info.Message.Hdop);
            writeDouble(info.Message.Altitude);
            writeDouble(info.Message.Geoidal);
            writeDouble(info.Message.Age);
            writeInt(info.Message.Station);

            //  Запись информации о файле.
            writeDateTime(info.BeginTime);
            writeDateTime(info.CreationTime);
            writeDateTime(info.LastAccessTime);
            writeDateTime(info.LastWriteTime);

            //  Запись информации о положении сообщения.
            builder.Append(info.Index);
            builder.Append(';');
            builder.Append(info.Count);
        }

        //  Сохранения данных в файл.
        await File.WriteAllTextAsync(Path.Combine(path, "gga.csv"), builder.ToString(), cancellationToken).ConfigureAwait(false);

        //  Очистка построителя текста.
        builder.Clear();

        //  Запись заголовка.
        builder.Append(
            "PoleCourse;MagneticCourse;Knots;Speed;Mode;" +
            "BeginTime;CreationTime;LastAccessTime;LastWriteTime;" +
            "Index;Count");

        //  Перебор данных.
        foreach (NmeaMessageInfo<NmeaVtgMessage> info in Vtg)
        {
            //  Переход на новую строку.
            builder.AppendLine();

            //  Запись основной информации.
            writeDouble(info.Message.PoleCourse);
            writeDouble(info.Message.MagneticCourse);
            writeDouble(info.Message.Knots);
            writeDouble(info.Message.Speed);
            writeGpsMode(info.Message.Mode);

            //  Запись информации о файле.
            writeDateTime(info.BeginTime);
            writeDateTime(info.CreationTime);
            writeDateTime(info.LastAccessTime);
            writeDateTime(info.LastWriteTime);

            //  Запись информации о положении сообщения.
            builder.Append(info.Index);
            builder.Append(';');
            builder.Append(info.Count);
        }

        //  Сохранения данных в файл.
        await File.WriteAllTextAsync(Path.Combine(path, "vtg.csv"), builder.ToString(), cancellationToken).ConfigureAwait(false);

        //  Очистка построителя текста.
        builder.Clear();

        //  Запись заголовка.
        builder.Append(
            "Time;Valid;Latitude;Longitude;Knots;Speed;" +
            "PoleCourse;Date;MagneticVariation;MagneticCourse;Mode;" +
            "BeginTime;CreationTime;LastAccessTime;LastWriteTime;" +
            "Index;Count");

        //  Перебор данных.
        foreach (NmeaMessageInfo<NmeaRmcMessage> info in Rmc)
        {
            //  Переход на новую строку.
            builder.AppendLine();

            //  Запись основной информации.
            writeTime(info.Message.Time);
            writeBoolean(info.Message.Valid);
            writeDouble(info.Message.Latitude);
            writeDouble(info.Message.Longitude);
            writeDouble(info.Message.Knots);
            writeDouble(info.Message.Speed);
            writeDouble(info.Message.PoleCourse);
            writeDate(info.Message.Date);
            writeDouble(info.Message.MagneticVariation);
            writeDouble(info.Message.MagneticCourse);
            writeGpsMode(info.Message.Mode);

            //  Запись информации о файле.
            writeDateTime(info.BeginTime);
            writeDateTime(info.CreationTime);
            writeDateTime(info.LastAccessTime);
            writeDateTime(info.LastWriteTime);

            //  Запись информации о положении сообщения.
            builder.Append(info.Index);
            builder.Append(';');
            builder.Append(info.Count);
        }

        //  Сохранения данных в файл.
        await File.WriteAllTextAsync(Path.Combine(path, "rmc.csv"), builder.ToString(), cancellationToken).ConfigureAwait(false);


        //  Очистка построителя текста.
        builder.Clear();

        //  Запись заголовка.
        builder.Append("Time;Speed;Latitude;Longitude");

        //  Перебор данных.
        for (int i = 0; i < Latitude.Length; i++)
        {
            //  Переход на новую строку.
            builder.AppendLine();

            //  Проверка данных.
            if (Latitude[i] > 0)
            {
                builder.Append($"{i};{Speed[i]};{Latitude[i]};{Longitude[i]}");
            }
            else
            {
                builder.Append($"{i};;;");
            }
        }

        //  Сохранения данных в файл.
        await File.WriteAllTextAsync(Path.Combine(path, "gps.csv"), builder.ToString(), cancellationToken).ConfigureAwait(false);

        void writeDateTime(DateTime time)
        {
            builder.Append(time.ToString(GluerTunnings.DateTimeFormat));
            builder.Append(';');
        }

        void writeBoolean(bool? value)
        {
            if (value.HasValue)
            {
                builder.Append(value.Value);
            }
            builder.Append(';');
        }

        void writeTime(TimeOnly? time)
        {
            if (time.HasValue)
            {
                builder.Append(time.Value.ToString(GluerTunnings.TimeFormat));
            }
            builder.Append(';');
        }

        void writeDate(DateOnly? date)
        {
            if (date.HasValue)
            {
                builder.Append(date.Value.ToString(GluerTunnings.DateFormat));
            }
            builder.Append(';');
        }

        void writeDouble(double? value)
        {
            if (value.HasValue)
            {
                builder.Append(value.Value);
            }
            builder.Append(';');
        }

        void writeSolution(GpsSolution? solution)
        {
            if (solution.HasValue)
            {
                builder.Append(solution.Value);
            }
            builder.Append(';');
        }

        void writeGpsMode(GpsMode? gpsMode)
        {
            if (gpsMode.HasValue)
            {
                builder.Append(gpsMode.Value);
            }
            builder.Append(';');
        }

        void writeInt(int? value)
        {
            if (value.HasValue)
            {
                builder.Append(value.Value);
            }
            builder.Append(';');
        }
    }
}
