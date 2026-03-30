namespace Simargl.Trials.Aurora.Aurora01.Gluer;

/// <summary>
/// Предоставляет настройки.
/// </summary>
public static class GluerTunnings
{
    /// <summary>
    /// Возвращает значение, определяющее, требуется ли только перобразовать файлы.
    /// </summary>
    public static bool IsConvertOnly { get; } = false;

    /// <summary>
    /// Постоянная, определяющая путь к каталогу сырых данных.
    /// </summary>
    public const string RawDataPath = "D:\\_Work\\RawData";// Aurora01Tunings.RawDataPath;

    /// <summary>
    /// Постоянная, определяющая путь к каталогу с кадрами.
    /// </summary>
    public const string RecordsPath = "D:\\_Work\\Records";

    /// <summary>
    /// Постоянная, определяющая путь к CSV-файлам.
    /// </summary>
    public const string CsvPath = "D:\\_Work\\Csv";

    /// <summary>
    /// Возвращает дату данных для склейки.
    /// </summary>
    public static DateOnly Date { get; } = DateOnly.Parse("31.01.2025");

    /// <summary>
    /// Возвращает массив адресов.
    /// </summary>
    public static string[] AdxlAddresses { get; } = [
        "192.168.8.18",
        "192.168.8.19",
        "192.168.8.20",
        "192.168.8.21",
        "192.168.8.22",
        "192.168.8.23",
        "192.168.8.24",
        "192.168.8.25",
        "192.168.8.26",
        "192.168.8.27",
        ];

    /// <summary>
    /// Возвращает информацию о каналах.
    /// </summary>
    public static (string Name, string Unit, double Scale, int Index)[] RawChannels { get; } =
    [
        ("B1", "kN", 116, 1),
        ("PP1", "kN", 1120, 2),
        ("PZ1", "kNm", 4.4, 3),
        ("PX1", "kNm", 4.4, 4),
        ("CV1", "kN", 176, 5),
        ("CG1", "kN", 80, 6),
        ("S1", "kNm", 24.4, 7),
        ("T1", "kN", 116, 8),
        ("B01", "kN", 116, 9),
        ("T01", "kN", 116, 10),
        ("S01", "kNm", 24.4, 11),
        ("CG01", "kN", 80, 12),
        ("PP01", "kN", 1120, 13),
        ("CV01", "kN", 176, 14),
        ("PX01", "kNm", 4.4, 15),
        ("PZ01", "kNm", 4.4, 16),
    ];

    /// <summary>
    /// Возвращает допустимый разрыв GPS-данных.
    /// </summary>
    public static TimeSpan GpsGap { get; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Возвращает шаг записи кадров.
    /// </summary>
    public static TimeSpan FrameStep { get; } = TimeSpan.FromSeconds(60);

    /// <summary>
    /// Постоянная, определяющая минимальное количество кадров во фрагменте.
    /// </summary>
    public const int FrameMinCount = 5;

    /// <summary>
    /// Возвращает максимально допустимое смещение кадров.
    /// </summary>
    public static TimeSpan FrameMaxDisplacement { get; } = TimeSpan.FromSeconds(1);

    /// <summary>
    /// Постоянная, определяющая максимальное допустимое сжатие кадров.
    /// </summary>
    public const double FrameMaxCompression = 0.99;

    /// <summary>
    /// Возвращает шаг записи Adxl.
    /// </summary>
    public static TimeSpan AdxlStep { get; } = TimeSpan.FromSeconds(10);

    /// <summary>
    /// Постоянная, определяющая минимальное количество Adxl во фрагменте.
    /// </summary>
    public const int AdxlMinCount = 5;

    /// <summary>
    /// Возвращает максимально допустимое смещение Adxl.
    /// </summary>
    public static TimeSpan AdxlMaxDisplacement { get; } = TimeSpan.FromSeconds(1);

    /// <summary>
    /// Постоянная, определяющая максимальное допустимое сжатие Adxl.
    /// </summary>
    public const double AdxlMaxCompression = 0.99;

    /// <summary>
    /// Возвращает формат даты.
    /// </summary>
    public const string DateFormat = "dd.MM.yyyy";

    /// <summary>
    /// Возвращает формат времени.
    /// </summary>
    public const string TimeFormat = "HH:mm:ss.fff";

    /// <summary>
    /// Возвращает формат даты и времени.
    /// </summary>
    public const string DateTimeFormat = DateFormat + " " + TimeFormat;

    /// <summary>
    /// Постоянная, определяющая путь к файлам экспорта данных.
    /// </summary>
    public const string ExportPath = "D:\\Environs\\Data\\Simargl.Trials.Aurora.Aurora01.Gluer";

    /// <summary>
    /// Возвращает данные датчика Adxl.
    /// </summary>
    public static (string Name, string Address0, int AdxlIndex0, double Scale0, string Address1, int AdxlIndex1, double Scale1, int Index)[] AdxlData { get; } =
    [
        ("BrusX01", "192.168.1.104", 0, -1, "192.168.8.20", 0, -1, 27),
        ("BrusX1", "192.168.1.101", 0, -1, "192.168.8.23", 0, -1, 24),
        ("BrusY01", "192.168.1.104", 2, 1, "192.168.8.20", 2, -1, 28),
        ("BrusY1", "192.168.1.101", 2, 1, "192.168.8.23", 2, -1, 25),
        ("BrusZ01", "192.168.1.104", 1, 1, "192.168.8.20", 1, 1, 29),
        ("BrusZ1", "192.168.1.101", 1, 1, "192.168.8.23", 1, 1, 26),
        ("BX101", "192.168.1.103", 1, 1, "192.168.8.22", 1, 1, 12),
        ("BX11", "192.168.1.108", 1, 1, "192.168.8.21", 1, 1, 0),
        ("BX201", "192.168.1.105", 1, 1, "192.168.8.25", 1, 1, 18),
        ("BX21", "192.168.1.107", 1, 1, "192.168.8.26", 1, 1, 6),
        ("BY101", "192.168.1.103", 0, -1, "192.168.8.22", 0, -1, 13),
        ("BY11", "192.168.1.108", 0, -1, "192.168.8.21", 0, -1, 1),
        ("BY201", "192.168.1.105", 0, -1, "192.168.8.25", 0, -1, 19),
        ("BY21", "192.168.1.107", 0, -1, "192.168.8.26", 0, -1, 7),
        ("BZ101", "192.168.1.103", 2, 1, "192.168.8.22", 2, 1, 14),
        ("BZ11", "192.168.1.108", 2, 1, "192.168.8.21", 2, 1, 2),
        ("BZ201", "192.168.1.105", 2, 1, "192.168.8.25", 2, 1, 20),
        ("BZ21", "192.168.1.107", 2, 1, "192.168.8.26", 2, 1, 8),
        ("RBX101", "192.168.1.106", 0, 1, "192.168.8.19", 0, 1, 15),
        ("RBX11", "192.168.1.109", 0, 1, "192.168.8.24", 0, 1, 3),
        ("RBX201", "192.168.1.102", 0, -1, "192.168.8.27", 0, -1, 21),
        ("RBX21", "192.168.1.110", 0, -1, "192.168.8.18", 0, -1, 9),
        ("RBY101", "192.168.1.106", 2, 1, "192.168.8.19", 2, -1, 16),
        ("RBY11", "192.168.1.109", 2, 1, "192.168.8.24", 2, -1, 4),
        ("RBY201", "192.168.1.102", 2, 1, "192.168.8.27", 2, -1, 22),
        ("RBY21", "192.168.1.110", 2, 1, "192.168.8.18", 2, -1, 10),
        ("RBZ101", "192.168.1.106", 1, 1, "192.168.8.19", 1, 1, 17),
        ("RBZ11", "192.168.1.109", 1, 1, "192.168.8.24", 1, 1, 5),
        ("RBZ201", "192.168.1.102", 1, 1, "192.168.8.27", 1, 1, 23),
        ("RBZ21", "192.168.1.110", 1, 1, "192.168.8.18", 1, 1, 11),
    ];
}
