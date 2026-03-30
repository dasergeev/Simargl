using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Simargl.Projects.OneTimes.Tiv.Aurora01;

/// <summary>
/// Предоставляет настройки.
/// </summary>
public static class Tunnings
{
    /// <summary>
    /// Инициализирует данные.
    /// </summary>
    static Tunnings()
    {
        //  Загрузка данных.
        Dictionary<string, string> map = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tunnings.txt"))
            .Select(x => x.Trim().Split(';')[0].Split('='))
            .Where(x => x.Length == 2)
            .Select(x => new
            {
                Name = x[0].Trim(),
                Value = x[1].Trim(),
            }).ToDictionary(x => x.Name, x => x.Value);

        //  Установка значений.
        BeginTime = DateTime.Parse(map["BeginTime"]);
        RawDataPath = map["RawDataPath"];
        LogPath = map["LogPath"];
        FramesPath = map["FramesPath"];
        TivPath = map["TivPath"];
    }

    /// <summary>
    /// Возвращает начальное время сбора данных.
    /// </summary>
    public static DateTime BeginTime { get; }

    /// <summary>
    /// Возвращает путь к исходным файлам.
    /// </summary>
    public static string RawDataPath { get; }

    /// <summary>
    /// Возвращает путь к журналу.
    /// </summary>
    public static string LogPath { get; }

    /// <summary>
    /// Возвращает путь к каталогу с кадрами.
    /// </summary>
    public static string FramesPath { get; }

    /// <summary>
    /// Возвращает путь к каталогу с файлами для ТИВа.
    /// </summary>
    public static string TivPath { get; }

    /// <summary>
    /// Поле для хранения длительности целевого кадра регистрации.
    /// </summary>
    public static readonly TimeSpan TargetFrameFragmentDuration = TimeSpan.FromSeconds(60);

    /// <summary>
    /// Поле для хранения длительности записи данных NMEA.
    /// </summary>
    public static readonly TimeSpan NmeaDuration = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Поле для хранения длительности записи фрагмента данных датчика ADXL.
    /// </summary>
    public static readonly TimeSpan AdxlFragmentDuration = TimeSpan.FromSeconds(10);

    /// <summary>
    /// Поле для хранения периода получения пакетов от датчика ADXL.
    /// </summary>
    public static readonly TimeSpan AdxlPeriod = TimeSpan.FromMilliseconds(25);

    /// <summary>
    /// Поле для хранения максимального разрыва между пакетами от датчика ADXL.
    /// </summary>
    public static readonly TimeSpan AdxlMaxGap = TimeSpan.FromMilliseconds(500);

    /// <summary>
    /// Постоянная, определяющая постфикс имени канала температуры процессора.
    /// </summary>
    public const string CpuTempPostfix = "CpuT";

    /// <summary>
    /// Постоянная, определяющая постфикс имени канала температуры датчика.
    /// </summary>
    public const string TempPostfix = "Temp";

    /// <summary>
    /// Постоянная, определяющая постфикс имени канала напряжения питания процессора.
    /// </summary>
    public const string CpuVoltagePostfix = "CpuV";

    /// <summary>
    /// Поле для хранения информации о первых датичках Adxl.
    /// </summary>
    public static readonly List<AdxlInfo> FirstAdxl =
    [
        new ("7500398F", "192.168.1.108", "BY11", "BX11", "BZ11", -1, -1, -1, -1, 0, 0),
        new ("76001121", "192.168.1.109", "RBX11", "RBZ11", "RBY11", 1, -1, 1, 0, 0, -1),
        new ("6B00FD6A", "192.168.1.107", "BY21", "BX21", "BZ21", -1, -1, -1, -1, 0, 0),
        new ("C9A80663", "192.168.1.110", "RBX21", "RBZ21", "RBY21", -1, 1, 1, 0, 0, -1),
        new ("7600E221", "192.168.1.103", "BY101", "BX101", "BZ101", -1, 1, 1, -1, 0, 0),
        new ("7500FF3A", "192.168.1.106", "RBX101", "RBZ101", "RBY101", 1, -1, 1, 0, 0, -1),
        new ("7500B1C9", "192.168.1.105", "BY201", "BX201", "BZ201", -1, 1, 1, -1, 0, 0),
        new ("75008AD0", "192.168.1.102", "RBX201", "RBZ201", "RBY201", -1, 1, 1, 0, 0, -1),
        new ("6B001F4F", "192.168.1.101", "BrusX1", "BrusZ1", "BrusY1", -1, 1, 1, 0, 0, -1),
        new ("7500A4D0", "192.168.1.104", "BrusX01", "BrusZ01", "BrusY01", -1, 1, 1, 0, 0, -1),


        //new ("7500398F", "192.168.1.108", "BY11", "BZ11", "BX11", 1, 1, 1, 0, 0, 0),
        //new ("76001121", "192.168.1.109", "RBX11", "RBY11", "RBZ11", -1, -1, 1, 0, 0, 0),
        //new ("6B00FD6A", "192.168.1.107", "BY21", "BZ21", "BX21", 1, 1, 1, 0, 0, 0),
        //new ("C9A80663", "192.168.1.110", "RBX21", "RBY21", "RBZ21", 1, -1, 1, 0, 0, 0),
        //new ("7600E221", "192.168.1.103", "BY101", "BZ101", "BX101", 1, -1, -1, 0, 0, 0),
        //new ("7500FF3A", "192.168.1.106", "RBX101", "RBY101", "RBZ101", -1, -1, -1, 0, 0, 0),
        //new ("7500B1C9", "192.168.1.105", "BY201", "BZ201", "BX201", 1, -1, -1, 0, 0, 0),
        //new ("75008AD0", "192.168.1.102", "RBX201", "RBY201", "RBZ201", 1, -1, -1, 0, 0, 0),
        //new ("6B001F4F", "192.168.1.101", "BrusX1", "BrusY1", "BrusZ1", 1, -1, -1, 0, 0, 0),
        //new ("7500A4D0", "192.168.1.104", "BrusX01", "BrusY01", "BrusZ01", 1, -1, -1, 0, 0, 0),
    ];

    /// <summary>
    /// Поле для хранения шаблонов для исключения каналов.
    /// </summary>
    public static readonly List<string> TivIgnoreChannelNames = [
        "Valid_GPS", "A_GPS", "Knots_GPS", "PCourse_GPS", "MCourse_GPS", "Satell_GPS", "Hdop_GPS", "Age_GPS",
        CpuTempPostfix, TempPostfix, CpuVoltagePostfix
        ];
}
