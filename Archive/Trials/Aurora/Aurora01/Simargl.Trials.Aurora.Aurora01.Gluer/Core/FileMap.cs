using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Simargl.Trials.Aurora.Aurora01.Gluer.Core;

/// <summary>
/// Представляет карту файлов.
/// </summary>
public sealed class FileMap
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    private FileMap(
        SortedDictionary<DateTime, FileInfo> nmea,
        SortedDictionary<DateTime, FileInfo> frame,
        SortedDictionary<DateTime, FileInfo>[] adxl)
    {
        //  Установка карт.
        Nmea = nmea;
        Frame = frame;
        Adxl = adxl;
    }

    /// <summary>
    /// Возвращает карту файлов NMEA.
    /// </summary>
    public SortedDictionary<DateTime, FileInfo> Nmea { get; }

    /// <summary>
    /// Возвращает карту кадров.
    /// </summary>
    public SortedDictionary<DateTime, FileInfo> Frame { get; }

    /// <summary>
    /// Возвращает карту файлов данных Adxl.
    /// </summary>
    public SortedDictionary<DateTime, FileInfo>[] Adxl { get; }

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
        builder.Append("BeginTime");

        //  Перебор данных.
        foreach (KeyValuePair<DateTime, FileInfo> info in Frame)
        {
            //  Переход на новую строку.
            builder.AppendLine();

            //  Запись информации о файле.
            builder.Append(info.Key.ToString(GluerTunnings.DateTimeFormat));
        }

        //  Сохранения данных в файл.
        await File.WriteAllTextAsync(Path.Combine(path, "frame.csv"), builder.ToString(), cancellationToken).ConfigureAwait(false);

        //  Очистка построителя текста.
        builder.Clear();

        //  Запись заголовка.
        builder.Append("Index;BeginTime");

        //  Перебор данных.
        for (int i = 0; i < Adxl.Length; i++)
        {
            //  Перебор данных.
            foreach (KeyValuePair<DateTime, FileInfo> info in Adxl[i])
            {
                //  Переход на новую строку.
                builder.AppendLine();

                //  Запись индекса.
                builder.Append(i);
                builder.Append(';');

                //  Запись информации о файле.
                builder.Append(info.Key.ToString(GluerTunnings.DateTimeFormat));
            }
        }

        //  Сохранения данных в файл.
        await File.WriteAllTextAsync(Path.Combine(path, "adxl.csv"), builder.ToString(), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно создаёт карту файлов.
    /// </summary>
    /// <param name="rootPath">
    /// Путь к корневому каталогу.
    /// </param>
    /// <param name="targetDate">
    /// Целевая дата.
    /// </param>
    /// <param name="adxlAddresses">
    /// Адреса датчиков.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая карту.
    /// </returns>
    public static async Task<FileMap> CreateAsync(string rootPath, DateOnly targetDate, string[] adxlAddresses, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

        //  Создание карт файлов.
        SortedDictionary<DateTime, FileInfo> nmea = [];
        SortedDictionary<DateTime, FileInfo> frame = [];
        SortedDictionary<DateTime, FileInfo>[] adxl = new SortedDictionary<DateTime, FileInfo>[adxlAddresses.Length];
        for (int i = 0; i < adxl.Length; i++)
        {
            adxl[i] = [];
        }

        //  Предыдущая и следующая дата.
        DateOnly previousDate = targetDate.AddDays(-1);
        DateOnly nextDate = targetDate.AddDays(1);

        //  Работа с предыдущим днём.
        await hourAsync(Path.Combine(rootPath, $"{previousDate.Year:0000}-{previousDate.Month:00}-{previousDate.Day:00}-23"));

        //  Определение базовой части пути.
        string pathBase = $"{targetDate.Year:0000}-{targetDate.Month:00}-{targetDate.Day:00}-";

        //  Перебор часовых каталогов.
        for (int hour = 0; hour < 24; hour++)
        {
            //  Работа с часовым каталогом.
            await hourAsync(Path.Combine(rootPath, $"{pathBase}{hour:00}"));
        }

        //  Работа со следующим днём.
        await hourAsync(Path.Combine(rootPath, $"{nextDate.Year:0000}-{nextDate.Month:00}-{nextDate.Day:00}-00"));

        //  Возврат карты.
        return new(nmea, frame, adxl);

        //  Выполняет работу с часовым каталогом.
        async Task hourAsync(string path)
        {
            //  Проверка токена отмены.
            await IsNotCancelledAsync(cancellationToken).ConfigureAwait(false);

            //  Проверка каталога.
            if (Directory.Exists(path))
            {
                //  Вывод в консоль.
                Console.WriteLine(path);

                //  Получение информации о каталоге с NMEA.
                DirectoryInfo directory = new(Path.Combine(path, "Nmea"));

                //  Проверка каталога.
                if (directory.Exists)
                {
                    //  Вывод в консоль.
                    Console.WriteLine($"  {directory.Name}");

                    //  Перебор файлов.
                    foreach (FileInfo fileInfo in directory.GetFiles("*", SearchOption.TopDirectoryOnly))
                    {
                        //  Получение времени файла.
                        DateTime time = NameParser.NmeaParse(fileInfo.Name);

                        //  Добавление файла в карту.
                        nmea.Add(time, fileInfo);
                    }
                }

                //  Получение информации о каталоге с кадрами.
                directory = new(Path.Combine(path, "RawFrames"));

                //  Проверка каталога.
                if (directory.Exists)
                {
                    //  Вывод в консоль.
                    Console.WriteLine($"  {directory.Name}");

                    //  Перебор файлов.
                    foreach (FileInfo fileInfo in directory.GetFiles("*", SearchOption.TopDirectoryOnly))
                    {
                        //  Получение времени файла.
                        DateTime time = NameParser.FrameParse(fileInfo.Name);

                        //  Добавление файла в карту.
                        frame.Add(time, fileInfo);
                    }
                }

                //  Перебор адресов датчиков.
                for (int i = 0; i < adxlAddresses.Length; i++)
                {
                    //  Получение адреса датчика.
                    string address = adxlAddresses[i];

                    //  Получение информации о каталоге с данными датчиков.
                    directory = new(Path.Combine(path, "Adxl-" + address));

                    //  Проверка каталога.
                    if (directory.Exists)
                    {
                        //  Вывод в консоль.
                        Console.WriteLine($"  {directory.Name}");

                        //  Перебор файлов.
                        foreach (FileInfo fileInfo in directory.GetFiles("*", SearchOption.TopDirectoryOnly))
                        {
                            //  Получение времени файла.
                            DateTime time = NameParser.AdxlParse(fileInfo.Name, address);

                            //  Добавление файла в карту.
                            adxl[i].Add(time, fileInfo);
                        }
                    }
                }
            }
        }
    }
}
