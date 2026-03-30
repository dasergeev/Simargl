using Microsoft.Extensions.Logging;
using Simargl.Concurrent;
using System.Collections.Concurrent;
using System.IO;

namespace Simargl.Trials.Egypt.Egypt02.Converter.Workers;

/// <summary>
/// Представляет сканер.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
public sealed class Scanner(ILogger<Scanner> logger) :
    Worker<Scanner>(logger)
{
    /// <summary>
    /// Возвращает карту сырых данных.
    /// </summary>
    public static ConcurrentDictionary<DateTime, FileInfo> RawDataMap { get; } = [];

    /// <summary>
    /// Возвращает карту записей.
    /// </summary>
    public static ConcurrentDictionary<DateTime, FileInfo> RecordMap { get; } = [];

    /// <summary>
    /// Возвращает карту CSV-файлов.
    /// </summary>
    public static ConcurrentDictionary<DateTime, FileInfo> CsvMap { get; } = [];

    /// <summary>
    /// Выполняет разбор имени сырого кадра.
    /// </summary>
    /// <param name="name">
    /// Имя сырого кадра.
    /// </param>
    /// <param name="time">
    /// Время регистрации.
    /// </param>
    /// <param name="number">
    /// Номер кадра.
    /// </param>
    /// <returns>
    /// Значение, определяющее успешность разбора.
    /// </returns>
    public static bool RawDataNameParse(string name, out DateTime time, out int number)
    {
        //  Установка значений по умолчанию.
        time = default;
        number = default;

        //  Блок перехвата всех исключений.
        try
        {
            //  Проверка длины имени.
            if (name.Length == 36)
            {

                //  Нормализация имени.
                name = name.ToLower();

                //  Проверка префикса.
                if (!name.StartsWith("vp"))
                    return false;

                //  0123456789012345678901234567890123456789
                //  Vp000_0 22.04.2025 7_46_16.00000101

                //  Получение данных по времени.
                int year = int.Parse(name.Substring(14, 4));
                int month = int.Parse(name.Substring(11, 2));
                int day = int.Parse(name.Substring(8, 2));
                int hour = int.Parse(name.Substring(19, 2));
                int minute = int.Parse(name.Substring(22, 2));
                int second = int.Parse(name.Substring(25, 2));

                //  Установка времени.
                time = new(year, month, day, hour, minute, second);

                //  Получение номера кадра.
                number = int.Parse(name[28..]);
            }
            else if (name.Length == 35)
            {
                //  Нормализация имени.
                name = name.ToLower();

                //  Проверка префикса.
                if (!name.StartsWith("vp"))
                    return false;

                //  0123456789012345678901234567890123456789
                //  Vp000_0 22.04.2025 7_46_16.00000101

                //  Получение данных по времени.
                int year = int.Parse(name.Substring(14, 4));
                int month = int.Parse(name.Substring(11, 2));
                int day = int.Parse(name.Substring(8, 2));
                int hour = int.Parse(name.Substring(19, 1));
                int minute = int.Parse(name.Substring(21, 2));
                int second = int.Parse(name.Substring(24, 2));

                //  Установка времени.
                time = new(year, month, day, hour, minute, second);

                //  Получение номера кадра.
                number = int.Parse(name[28..]);
            }
            else
            {
                return false;
            }


            //  Имя разобрано.
            return true;
        }
        catch { }

        //  Не удалось произвести разбор.
        return false;
    }

    /// <summary>
    /// Выполняет разбор имени записи.
    /// </summary>
    /// <param name="name">
    /// Имя записи.
    /// </param>
    /// <param name="time">
    /// Время регистрации.
    /// </param>
    /// <param name="number">
    /// Номер кадра.
    /// </param>
    /// <param name="speed">
    /// Скорость движения.
    /// </param>
    /// <returns>
    /// Значение, определяющее успешность разбора.
    /// </returns>
    public static bool RecordNameParse(string name, out DateTime time, out int number, out double speed)
    {
        //  Установка значений по умолчанию.
        time = default;
        number = default;
        speed = default;

        //  Блок перехвата всех исключений.
        try
        {
            //  Проверка длины имени.
            if (name.Length != 33)
                return false;

            //  Нормализация имени.
            name = name.ToLower();

            //  Проверка префикса.
            if (!name.StartsWith("vp"))
                return false;

            //  Получение данных по времени.
            int year = int.Parse(name.Substring(8, 4));
            int month = int.Parse(name.Substring(13, 2));
            int day = int.Parse(name.Substring(16, 2));
            int hour = int.Parse(name.Substring(19, 2));
            int minute = int.Parse(name.Substring(22, 2));
            int second = int.Parse(name.Substring(25, 2));

            //  Установка времени.
            time = new(year, month, day, hour, minute, second);

            //  Получение номера кадра.
            number = int.Parse(name[28..]);

            //  Получение скорости.
            speed = double.Parse(name.Substring(2, 3)) + 0.1 * double.Parse(name.Substring(6, 1));

            //  Имя разобрано.
            return true;
        }
        catch { }

        //  Не удалось произвести разбор.
        return false;
    }


    /// <summary>
    /// Выполняет разбор имени файла CSV.
    /// </summary>
    /// <param name="fileInfo">
    /// Информация о файле.
    /// </param>
    /// <param name="time">
    /// Время регистрации.
    /// </param>
    /// <returns>
    /// Значение, определяющее успешность разбора.
    /// </returns>
    public static bool CsvNameParse(FileInfo fileInfo, out DateTime time)
    {
        //  Установка значений по умолчанию.
        time = default;

        //  Блок перехвата всех исключений.
        try
        {
            //  Получение имени.
            string name = fileInfo.Name;

            //  Проверка длины имени.
            if (name.Length != 9) return false;

            //  Нормализация имени.
            name = name.ToLower();

            //  Проверка постфикса.
            if (!name.EndsWith(".csv")) return false;

            //  Получение номера.
            int number = int.Parse(name[..5]);

            //  Получение имени каталога.
            string directory = fileInfo.Directory!.Name;

            //  Получение данных по времени.
            int year = int.Parse(directory[..4]);
            int month = int.Parse(directory.Substring(5, 2));
            int day = int.Parse(directory.Substring(8, 2));


            int second = number % 60;
            int minute = ((number - second) / 60) % 60;
            int hour = (number - second - minute * 60) / 3600;

            //  Установка времени.
            time = new(year, month, day, hour, minute, second);

            //  Имя разобрано.
            return true;
        }
        catch { }

        //  Не удалось произвести разбор.
        return false;
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
    protected override sealed async Task InvokeAsync(CancellationToken cancellationToken)
    {
        //  Основной цикл выполнения.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Вывод информации в консоль.
            Console.WriteLine("Начало сканирования.");

            //  Сканирование файлов csv.
            await ScanAsync(Tunings.CsvPath,
                async delegate (FileInfo file, CancellationToken cancellationToken)
                {
                    //  Ожидание завершённой задачи.
                    await Task.CompletedTask.ConfigureAwait(false);

                    //  Разбор файла.
                    if (CsvNameParse(file, out DateTime time))
                    {
                        //  Добавление файла в карту.
                        CsvMap.AddOrUpdate(time, file, (key, _) => file);
                    }
                }, cancellationToken).ConfigureAwait(false);

            //  Сканирование файлов записей.
            await ScanAsync(Tunings.RecordsPath,
                async delegate (FileInfo file, CancellationToken cancellationToken)
                {
                    //  Ожидание завершённой задачи.
                    await Task.CompletedTask.ConfigureAwait(false);

                    //  Разбор файла.
                    if (RecordNameParse(file.Name, out DateTime time, out int _, out double _))
                    {
                        //  Добавление файла в карту.
                        RecordMap.AddOrUpdate(time, file, (key, _) => file);
                    }
                }, cancellationToken).ConfigureAwait(false);

            //  Сканирование файлов сырых данных.
            await ScanAsync(Tunings.RawDataPath,
                async delegate (FileInfo file, CancellationToken cancellationToken)
                {
                    //  Ожидание завершённой задачи.
                    await Task.CompletedTask.ConfigureAwait(false);

                    //  Разбор файла.
                    if (RawDataNameParse(file.Name, out DateTime time, out int _))
                    {

                        //  Добавление файла в карту.
                        RawDataMap.AddOrUpdate(time, file, (key, _) => file);
                    }
                    else
                    {
                        //Console.WriteLine($"{file.Name}");
                    }

                }, cancellationToken).ConfigureAwait(false);

            //  Ожидание перед следующим проходом.
            await Task.Delay(60_000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно выполняет сканирование каталога.
    /// </summary>
    /// <param name="path">
    /// Путь к каталогу.
    /// </param>
    /// <param name="action">
    /// Действие, которое необходимо выполнить с файлом.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая сканирование каталога.
    /// </returns>
    private static async Task ScanAsync(string path, AsyncAction<FileInfo> action, CancellationToken cancellationToken)
    {
        //  Получение часовых каталогов.
        DirectoryInfo[] directories = new DirectoryInfo(path)
            .GetDirectories("*", SearchOption.TopDirectoryOnly);

        //  Асинхронная работа с каталогами.
        await Parallel.ForEachAsync(
            directories,
            cancellationToken,
            async delegate (DirectoryInfo directory, CancellationToken cancellationToken)
            {
                //Console.WriteLine($"{directory.FullName}");

                //  Проверка токена отмены.
                cancellationToken.ThrowIfCancellationRequested();

                //  Получение файлов.
                FileInfo[] files = directory
                    .GetFiles("*", SearchOption.TopDirectoryOnly);

                //  Асинхронная работа с файлами.
                await Parallel.ForEachAsync(
                    files,
                    cancellationToken,
                    async delegate (FileInfo file, CancellationToken cancellationToken)
                    {
                        //  Проверка токена отмены.
                        cancellationToken.ThrowIfCancellationRequested();

                        //  Блок перехвата всех исключений.
                        try
                        {
                            //  Выполнение действия с файлом.
                            await action(file, cancellationToken).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            //  Проверка токена отмены.
                            if (cancellationToken.IsCancellationRequested)
                            {
                                //  Повторный выброс исключения.
                                throw;
                            }

                            //  Вывод информации в консоль.
                            Console.WriteLine($"Произошла ошибка при сканировании: {ex}");
                        }
                    }).ConfigureAwait(false);

            }).ConfigureAwait(false);
    }
}
