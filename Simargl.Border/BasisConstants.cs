using Simargl.Border.Geometry;
using Simargl.Border.Storage.Entities;
using System.Net;

namespace Simargl.Border;

/// <summary>
/// Предоставляет базовые постоянные.
/// </summary>
public static class BasisConstants
{
    /// <summary>
    /// 
    /// </summary>
    public static readonly double DefectRangeStep = 10;

    /// <summary>
    /// 
    /// </summary>
    public static readonly int DefectBeginLevel = 3;

    /// <summary>
    /// Поиск всех дефектов.
    /// </summary>
    public static Dictionary<int, List<(string Number, string Rail, string Range)>>? AllDefects(IEnumerable<AxisData> axes)
    {
        try
        {
            return axes.SelectMany(static x => new[]
                {
                new
                {
                    x.Index,
                    Rail = Rail.Left,
                    Range = DefectRange(x, Rail.Left),
                },
                new
                {
                    x.Index,
                    Rail = Rail.Right,
                    Range = DefectRange(x, Rail.Right),
                }
            })
                .Where(x => x.Range.HasValue)
                .Select(x => new
                {
                    x.Index,
                    x.Rail,
                    Range = x.Range!.Value,
                })
                .Select(x => new
                {
                    Number = $"{x.Index + 1}",
                    Rail = x.Rail == Rail.Left ? "Левая сторона" : "Правая сторона",
                    Range = $"{x.Range:0.00}%",
                    Level = (int)Math.Floor(Math.Abs(x.Range) / DefectRangeStep),
                })
                .Where(x => x.Level >= DefectBeginLevel)
                .OrderBy(x => x.Range)
                .Select(x => new
                {
                    x.Level,
                    Values = (x.Number, x.Rail, x.Range)
                })
                .GroupBy(x => x.Level)
                .ToDictionary(x => x.Key, x => x.Select(x => x.Values).ToList());
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Есть/нет дефекта.
    /// </summary>
    public static string IsDefect(AxisData axis, Rail rail)
    {
        double? range = DefectRange(axis, rail);
        if (range.HasValue)
        {
            return $"{range.Value:0.00}%";
        }
        else
        {
            return "—";
        }
    }

    private static double? DefectRange(AxisData axis, Rail rail)
    {
        List<double> data = [.. axis.Interactions.Where(x => _Sections.Contains(x.Section))
            .Select(x => new double[] { x.LeftMax, x.RightMax })
            .Select(x => x[(int)rail])];

        if (data.Count > 0)
        {
            var a = data.Average();
            var m = data.Max();
            if (a > 0)
            {
                double range = 100.0 * (m - a) / a;
                if (double.IsFinite(range))
                {
                    return range;
                }
            }
        }
        return null;
    }
    private static readonly List<int> _Sections = [0, 1, 2, 3, 4, 8, 10, 12, 15, /*17,*/ 19, 20, 21,];


    /// <summary>
    /// Представляет фильтр проездов.
    /// </summary>
    public static readonly Func<PassageData, bool> PassageFilter = new(x => x.AxesCount % 2 == 0 && x.AxesCommits >= 5);

    /// <summary>
    /// Поле для хранения данных для подключения к хранилищу.
    /// </summary>
    public static readonly (IPEndPoint EndPoint, string Database, string Username, string Password) Storage =
        new(new(IPAddress.Loopback, 5432), "Border", "postgres", "123QWEasd");

    /// <summary>
    /// Возвращает 
    /// </summary>
    public static readonly IPEndPoint StorageEndPoint = new(IPAddress.Loopback, 5432);

    /// <summary>
    /// Поле для хранения корневого пути к данным.
    /// </summary>
    public const string RootDataPath = "D:\\Environs\\Data\\Simargl.Border.Recorder";

    /// <summary>
    /// Поле для хранения пути к очереди кадров.
    /// </summary>
    public const string FrameQueuePath = RootDataPath + "\\FrameQueue";

    /// <summary>
    /// Поле для хранения пути к сырым кадрам.
    /// </summary>
    public const string RawFramesPath = RootDataPath + "\\RawFrames";

    /// <summary>
    /// Поле для хранения пути к обработанным кадрам.
    /// </summary>
    public const string ProcessedFramesPath = RootDataPath + "\\ProcessedFrames";

    /// <summary>
    /// Поле для хранения пути к сжатым кадрам.
    /// </summary>
    public const string ZipFramesPath = RootDataPath + "\\ZipFrames";

    /// <summary>
    /// Возвращает время ожидания восстановления службы.
    /// </summary>
    public static TimeSpan ServiceRecoveryTimeout { get; } = TimeSpan.FromMilliseconds(100);

    /// <summary>
    /// Постоянная, определяющая размер пакета данных от датчика.
    /// </summary>
    public const int SensorPackageSize = 611;

    /// <summary>
    /// Постоянная, определяющая длину фрагмента сигнала.
    /// </summary>
    public const int SignalFragmentLength = 50;

    /// <summary>
    /// Возвращает время ожидания получения данных от датчика.
    /// </summary>
    public static TimeSpan SensorTimeout { get; } = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Возвращает период работы быстрой службы.
    /// </summary>
    public static TimeSpan FastServicePeriod { get; } = TimeSpan.FromMilliseconds(100);

    /// <summary>
    /// Возвращает период работы средней службы.
    /// </summary>
    public static TimeSpan MediumServicePeriod { get; } = TimeSpan.FromMilliseconds(1000);

    /// <summary>
    /// Возвращает период синхронизации.
    /// </summary>
    public static TimeSpan SynchronizationPeriod { get; } = TimeSpan.FromSeconds(5);

    /// <summary>
    /// Постоянная, определяющая минимальное количество точек для синхронизации.
    /// </summary>
    public const int SynchronizationMinSize = 256;

    /// <summary>
    /// Постоянная, определяющая минимальную координату точек на графиках.
    /// </summary>
    public const double PointsXMin = -10;

    /// <summary>
    /// Постоянная, определяющая размер коллекции пакетов устройства.
    /// </summary>
    public const int DevicePackageCollectionSize = 2400;

    /// <summary>
    /// Постоянная, определяющая размер буфера источников каналов.
    /// </summary>
    /// <remarks>
    /// Должно быть больше <see cref="ChannelSourceSaveSize"/>.
    /// </remarks>
    public const int ChannelSourceBufferSize = ChannelSourceSaveSize + 2400;

    /// <summary>
    /// Постоянная, определяющая размер данных для сохранения.
    /// </summary>
    public const int ChannelSourceSaveSize = 2400;

    /// <summary>
    /// Постоянная, определяющая длину очереди кадров.
    /// </summary>
    public const int FrameQueueLength = 2;

    /// <summary>
    /// Постоянная, определяющая количество, по которому определяется значение нуля.
    /// </summary>
    public const int ChannelSourceZeroCount = 5 * 40;

    /// <summary>
    /// Постоянная, определяющее минимальное количество непрерывных синхромаркеров для синхронизации.
    /// </summary>
    public const int SynchromarkerContinuousMin = 40;

    /// <summary>
    /// Постоянная, определяющая минимальное количество устройств.
    /// </summary>
    public const int DeviceCountMin = 16;

    /// <summary>
    /// Постоянная, определяющая смещение синхромаркера при синхронизации.
    /// </summary>
    public const int SynchronizationOffset = -5 * 40;

    /// <summary>
    /// Постоянная, определяющая количество групп каналов.
    /// </summary>
    public const int SectionGroupCount = 21;

    /// <summary>
    /// Постоянная, определяющая порог СКО сигнала.
    /// </summary>
    public const double SignalDeviationThreshold = 200;//100

    /// <summary>
    /// Постоянная, определяющее минимальное количество порогов.
    /// </summary>
    public const int ThresholdCountMinCount = 4*8;//16

    /// <summary>
    /// Поле для хранения времени завершения обработки.
    /// </summary>
    public static readonly TimeSpan ProcessingCompletionDuration = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Постоянная, определяющая количество сечений.
    /// </summary>
    public const int SectionCount = 21;

    /// <summary>
    /// Поле для хранения параметров предобработки.
    /// </summary>
    public static readonly (
        int BlockSize, int Sampling, TimeSpan Zero,
        int SectionCount, int ChannelLenght, double CounterThreshold,
        int MinCommitsCount, int MinAxesCount,
        double DeltaTime, double MaxSpeed, int TimeRange,
        int SpeedIndexStep) Preprocessor =
        new()
        {
            BlockSize = 50,
            Sampling = 2000,
            Zero = TimeSpan.FromSeconds(1),
            SectionCount = SectionCount,
            ChannelLenght = SignalFragmentLength * ChannelSourceSaveSize,
            CounterThreshold = 20,
            MinCommitsCount = 10,
            MinAxesCount = 2,
            DeltaTime = 1.0 / 2000.0,
            MaxSpeed = 150,
            TimeRange = 1000,
            SpeedIndexStep = 2000,
        };

    /// <summary>
    /// Возвращает карту целевых имён.
    /// </summary>
    public static readonly Dictionary<string, int> TargetNames = new Func<Dictionary<string, int>>(delegate
    {
        //  Получение карты.
        Dictionary<string, int> map = Enumerable
            .Range(1, SectionCount)
            .Select(x => new
            {
                Section = x,
                Map = new (string Name, int Index)[]
                {
                    ($"Counter{x:00}", 1),

                    ($"PCL{x:00}", 2),
                    ($"SLe{x:00}_0", 3),
                    ($"SLe{x:00}_1", 4),
                    ($"SLe{x:00}_2", 5),
                    ($"SLi{x:00}_0", 6),
                    ($"SLi{x:00}_1", 7),
                    ($"SLi{x:00}_2", 8),

                    ($"PCR{x:00}", 9),
                    ($"SRe{x:00}_0", 10),
                    ($"SRe{x:00}_1", 11),
                    ($"SRe{x:00}_2", 12),
                    ($"SRi{x:00}_0", 13),
                    ($"SRi{x:00}_1", 14),
                    ($"SRi{x:00}_2", 15),
                },
            })
            .Select(x => x.Map.Select(y => new
            {
                y.Name,
                Index = y.Index + 100 * x.Section,
            }))
            .SelectMany(x => x)
            .ToDictionary(x => x.Name, x => x.Index);

        //  Добавление общих каналов.
        map.Add("PreSpeed", 1);
        map.Add("Speed", 2);

        //  Возврат карты.
        return map;
    }).Invoke();
}
