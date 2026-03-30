using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Simargl.Frames;
using Simargl.Projects.Egypt.EgyptXXX.Converter.Core;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.InteropServices;

namespace Simargl.Projects.Egypt.EgyptXXX.Converter;

/// <summary>
/// Представляет основной фоновый процесс.
/// </summary>
/// <param name="logger">
/// Средство ведения журнала.
/// </param>
/// <param name="options">
/// Настройки.
/// </param>
public class Worker(ILogger<Worker> logger, IOptions<MeasurementOptions> options) :
    BackgroundService
{
    /// <summary>
    /// Поле для хранения пути к корневому каталогу.
    /// </summary>
    private static readonly string _RootPath = new Func<string>(delegate
    {
        //  Проверка платформы.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return "\\\\192.168.3.21\\share\\";
        }
        else
        {
            return "/mnt/share/";
        }
    })();

    /// <summary>
    /// Поле для хранения пути к корневому каталогу сырых данных.
    /// </summary>
    private static readonly string _RawPath = Path.Combine(_RootPath, "RawData", "Egypt", "Egypt XXX");

    /// <summary>
    /// Поле для хранения пути к корневому каталогу записей.
    /// </summary>
    private static readonly string _RecordPath = Path.Combine(_RootPath, "Records", "Egypt", "Egypt XXX");

    /// <summary>
    /// Поле для хранения пути к карте.
    /// </summary>
    private static readonly string _MapPath = Path.Combine(_RecordPath, "Map");

    /// <summary>
    /// Поле для хранения пути к кадрам.
    /// </summary>
    private static readonly string _FramePath = Path.Combine(_RecordPath, "Frames");

    /// <summary>
    /// Поле для хранения пути к кадрам.
    /// </summary>
    private static readonly string _CsvPath = Path.Combine(_RecordPath, "Csv");

    /// <summary>
    /// Поле для хранения пути к кадрам.
    /// </summary>
    private static readonly string _SendPath = "D:\\Nextcloud\\Transit\\Tiv\\Egypt\\Egypt 03";
    //private static readonly string _SendPath = Path.Combine(_RecordPath, "Send");

    //

    /// <summary>
    /// Поле для хранения карты файлов.
    /// </summary>
    private readonly ConcurrentDictionary<DateTime, List<FileDetails>> _FileMap = [];

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
        //  Основной цикл поддержки работы.
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
                if (cancellationToken.IsCancellationRequested)
                {
                    //  Завершение работы.
                    return;
                }

                //  Вывод информации в журнал.
                logger.LogError("Произошло исключение: {ex}", ex);
            }

            //  Ожидание перед следующей попыткой.
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
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
        //  Ожидание инициализации консоли.
        await Task.Delay(1_000, cancellationToken).ConfigureAwait(false);

        //  Основной цикл поддержки работы.
        while (!cancellationToken.IsCancellationRequested)
        {
            //  Вывод информации в журнал.
            logger.LogInformation("Начало работы.");

            //  Получение корневого каталога.
            DirectoryInfo directory = new(_RawPath);

            //  Создание карты файлов.
            await CreateMapAsync(directory, cancellationToken).ConfigureAwait(false);

            //  Параллельная работа с подкаталогами.
            await Parallel.ForEachAsync(
                directory.GetDirectories(),
                cancellationToken,
                InvokeAsync).ConfigureAwait(false);

            //  Вывод информации в журнал.
            logger.LogInformation("Завершение работы.");

            //  Ожидание перед следующим проходом.
            await Task.Delay(300_000, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Асинхронно создаёт карту файлов.
    /// </summary>
    /// <param name="directory">
    /// Корневой каталог.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая карту файлов.
    /// </returns>
    private async Task CreateMapAsync(DirectoryInfo directory, CancellationToken cancellationToken)
    {
        //  Очистка карты.
        _FileMap.Clear();

        //  Поиск в каталоге.
        await directorySearch(directory, cancellationToken).ConfigureAwait(false);

        //  Выполняет поиск по каталогу.
        async ValueTask directorySearch(DirectoryInfo directory, CancellationToken cancellationToken)
        {
            //  Поиск в подкаталогах.
            await Parallel.ForEachAsync(
                directory.GetDirectories("*", SearchOption.TopDirectoryOnly),
                cancellationToken,
                directorySearch).ConfigureAwait(false);

            //  Анализ файлов.
            await Parallel.ForEachAsync(
                directory.GetFiles("*", SearchOption.TopDirectoryOnly),
                cancellationToken,
                fileAnalysis).ConfigureAwait(false);
        }

        //  Выполняет анализ файла.
        async ValueTask fileAnalysis(FileInfo file, CancellationToken cancellationToken)
        {
            //  Проверка токена отмены.
            cancellationToken.ThrowIfCancellationRequested();

            //  Ожидание завершённой задачи.
            await ValueTask.CompletedTask.ConfigureAwait(false);

            //  Блок перехвата всех исключений.
            try
            {
                //  Получение имени файла.
                string name = file.Name;

                //  Определение формата файла.
                FileFormat format = file.Extension.ToLower() switch
                {
                    ".nmea" => FileFormat.Nmea,
                    ".adxl" => FileFormat.Adxl,
                    ".strain" => FileFormat.Strain,
                    ".rs485" => FileFormat.RS485,
                    _ => throw new InvalidOperationException($"Недопустимое расширение файла {file.Extension}."),
                };

                //  Нормализация имени в соответствии с форматом.
                switch (format)
                {
                    case FileFormat.Nmea:
                    case FileFormat.Adxl:
                        //  Нормализация имени.
                        name = name[5..^5];

                        //  Завершение нормализации.
                        break;
                    case FileFormat.Strain:
                        //  Нормализация имени.
                        name = name[7..^7];

                        //  Завершение нормализации.
                        break;
                    case FileFormat.RS485:
                        //  Нормализация имени.
                        name = name[6..^6];

                        //  Завершение нормализации.
                        break;
                    default:
                        //  Завершение нормализации.
                        break;
                }

                //  Разбивка данных.
                string[] parts = name.Split('-');

                //  Проверка количества частей.
                if ((format == FileFormat.Nmea && parts.Length != 5) ||
                    (format == FileFormat.Adxl && parts.Length != 9) ||
                    (format == FileFormat.Strain && parts.Length != 9))
                {
                    //  Выброс исключения.
                    throw new InvalidOperationException($"Недопустимое имя файла {file.Name}.");
                }

                //  Определение источника.
                string source = format switch
                {
                    FileFormat.Adxl or FileFormat.Strain => string.Join('.', parts.AsSpan(0, 4).ToArray()),
                    _ => string.Empty,
                };

                //  Определение времени.
                DateTime time = new(
                    int.Parse(parts[^5]),
                    int.Parse(parts[^4]),
                    int.Parse(parts[^3]),
                    int.Parse(parts[^2]),
                    int.Parse(parts[^1]),
                    0);

                //  Создание информации о файле.
                FileDetails details = await FileDetails.CreateAsync(format, file, source, time, cancellationToken).ConfigureAwait(false);

                //  Получение списка из карты.
                List<FileDetails> files = _FileMap.GetOrAdd(time, _ => []);

                //  Добавление информации о файле в список.
                files.Add(details);
            }
            catch (Exception ex)
            {
                //  Вывод информации в журнал.
                logger.LogError(ex, "Произошла ошибка при работе с файлом {file}.", file);

                //  Завершение работы с файлом.
                return;
            }
        }
    }

    /// <summary>
    /// Асинхронно выполняет работу с каталогом.
    /// </summary>
    /// <param name="directory">
    /// Каталог, с которым необходимо выполнить работу.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с каталогом.
    /// </returns>
    private async ValueTask InvokeAsync(DirectoryInfo directory, CancellationToken cancellationToken)
    {
        //  Время каталога.
        DateTime directoryTime;

        //  Блок перехвата всех исключений.
        try
        {
            //  Получение времени каталога.
            directoryTime = new(
                int.Parse(directory.Name.AsSpan(0, 4)),
                int.Parse(directory.Name.AsSpan(5, 2)),
                int.Parse(directory.Name.AsSpan(8, 2)),
                int.Parse(directory.Name.AsSpan(11, 2)),
                0, 0);
        }
        catch (Exception ex)
        {
            //  Вывод информации в журнал.
            logger.LogError(ex, "Произошла ошибка при работе с каталогом {directory}.", directory);

            //  Заверешение работы с каталогом.
            return;
        }

#if DEBUG
        //  Проверка текстового каталога.
        if (directory.Name != "2025-12-01-12")
        {
            //  Завершение работы.
            return;
        }
#endif

        //  Параллельная работа с минутными интервалами.
        await Parallel.ForEachAsync(
            Enumerable.Range(0, 60).Select(x => directoryTime.AddMinutes(x)),
            cancellationToken,
            InvokeAsync);
    }

    /// <summary>
    /// Асинхронно выполняет работу с минутным интервалом.
    /// </summary>
    /// <param name="time">
    /// Время начала интервала.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, выполняющая работу с минутным интервалом.
    /// </returns>
    private async ValueTask InvokeAsync(DateTime time, CancellationToken cancellationToken)
    {
        //  Получение списка информации о файлах.
        if (!_FileMap.TryGetValue(time, out List<FileDetails>? files) ||
            files is null)
        {
            //  Завершение работы с интервалом.
            return;
        }

        //  Получение карты файлов.
        IEnumerable<string> map = files.Select(x => x.FileInfo.Name);

        //  Получение пути к карте.
        string mapDirectory = Path.Combine(_MapPath, $"{time:yyyy-MM-dd}");
        string mapFile = Path.Combine(mapDirectory, $"{time:yyyy-MM-dd-HH-mm}.map");

        //  Проверка карты.
        if (File.Exists(mapFile))
        {
            //  Загрузка старой карты.
            IEnumerable<string> oldMap = (await File.ReadAllLinesAsync(mapFile, cancellationToken).ConfigureAwait(false))
                .Where(x => !string.IsNullOrWhiteSpace(x));

            //  Проверка новых файлов.
            if (!map.Except(oldMap).Any())
            {
#if !DEBUG
                //  Интервал уже обработан.
                return;
//#else
//                if (!map.Except(oldMap).Any(x => x.EndsWith("485")))
//                {
//                    return;
//                }
#endif
            }
        }

        //  Создание построителя кадра.
        FrameBuilder builder = new(time, files, options.Value);

        //  Выполнение построения.
        await builder.BuildAsync(cancellationToken).ConfigureAwait(false);

        //  Получение кадра.
        Frame frame = builder.Frame;

        //  Получение каталога.
        string frameDirectory = Path.Combine(_FramePath, $"{time:yyyy-MM-dd}");
        CreateDirectory(frameDirectory);

        //  Перебор файлов в каталоге.
        foreach (FileInfo file in new DirectoryInfo(frameDirectory).GetFiles("*", SearchOption.TopDirectoryOnly))
        {
            //  Проверка расширения файла.
            if (file.Extension.Length == 5 &&
                int.TryParse(file.Extension.AsSpan(1), out int number)
                && number == builder.Number)
            {
                //  Удаление файла.
                file.Delete();
            }
        }

        //  Получение пути к кадру.
        string framePath = Path.Combine(frameDirectory, $"Vp{builder.AverageSpeed:000}_0.{builder.Number:0000}");

        //  Сохранение кадра.
        frame.Save(framePath, StorageFormat.TestLab);

        //  Получение каталогов.
        string csvDirectory = Path.Combine(_CsvPath, $"{time:yyyy-MM-dd}");
        CreateDirectory(csvDirectory);
        string sendDirectory = Path.Combine(_SendPath, $"{time:yyyy-MM-dd}");
        CreateDirectory(sendDirectory);

        //  Получение пути к файлам.
        string csvPath = Path.Combine(csvDirectory, $"{builder.Number:0000}.csv");
        string sendPath = Path.Combine(sendDirectory, $"{builder.Number:0000}.csv");

        //  Получение текстовых данных.
        string text = await builder.ToCsvAsync(cancellationToken).ConfigureAwait(false);

        //  Сохранение данных.
        await File.WriteAllTextAsync(csvPath, text, cancellationToken).ConfigureAwait(false);
        await File.WriteAllTextAsync(sendPath, text, cancellationToken).ConfigureAwait(false);

        //  Вывод информации в журнал.
        logger.LogInformation("Работа с минутным интервалом {time}", time);

        //  Создание каталога.
        CreateDirectory(mapDirectory);

        //  Сохранение карты файлов.
        await File.WriteAllLinesAsync(mapFile, map, CancellationToken.None).ConfigureAwait(false);
    }

    /// <summary>
    /// Создаёт каталог.
    /// </summary>
    /// <param name="path">
    /// Путь к каталогу.
    /// </param>
    private static void CreateDirectory(string path)
    {
        //  Проверка каталога.
        if (Directory.Exists(path))
        {
            //  Каталог существует.
            return;
        }

        //  Получение каталога.
        DirectoryInfo directory = new(path);

        //  Проверка корневого каталога.
        if (directory.Parent is DirectoryInfo parent)
        {
            //  Создание корневого каталога.
            CreateDirectory(parent.FullName);
        }
        
        //  Создание текущего каталога.
        directory.Create();
    }
}
