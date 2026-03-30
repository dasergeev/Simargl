namespace Simargl.Trials.Aurora.Aurora01;

/// <summary>
/// Предоставляет общие настройки.
/// </summary>
public static class Aurora01Tunings
{
    /// <summary>
    /// Поле для хранения массива частот.
    /// </summary>
    public static readonly double[] Frequencies = [100, 80, 60, 40, 20];

    /// <summary>
    /// Постоянная, определяющая адрес для подключения к базе данных.
    /// </summary>
    public const string StorageHost = "192.168.15.202";// "10.7.0.8";
//#if DEBUG
//            "192.168.15.202"
//#else
//            "127.0.0.1"
//#endif
//;

    /// <summary>
    /// Постоянная, определяющая путь к каталогу переданных данных.
    /// </summary>
    public const string TransferringRawDataPath = "D:\\RawData\\Aurora\\Aurora 01";

    /// <summary>
    /// Постоянная, определяющая путь к сетевому каталогу сырых данных.
    /// </summary>
    public const string NetworkRawDataPath = "S:\\RawData\\Aurora\\Aurora 01";// "\\\\192.168.15.202\\RawData\\Aurora\\Aurora 01";

    /// <summary>
    /// Постоянная, определяющая путь к каталогу сырых данных.
    /// </summary>
    public const string RawDataPath = "\\\\192.168.3.21\\share\\RawData\\Aurora\\Aurora 01";// "\\\\10.7.2.7\\share\\RawData\\Aurora\\Aurora 01";
//#if DEBUG
//            NetworkRawDataPath
//#else
//            "F:\\RawData\\Aurora\\Aurora 01"
//#endif
//;

    /// <summary>
    /// Поле для хранения периода сканирования.
    /// </summary>
    public static readonly TimeSpan ScanPeriod = TimeSpan.FromMinutes(60);

    /// <summary>
    /// Постоянная, определяющая степень параллелизма при сканировании.
    /// </summary>
    public const int ScanMaxDegreeOfParallelism = 10;

    /// <summary>
    /// Поле для хранения периода обработки файлов Nmea.
    /// </summary>
    public static readonly TimeSpan NmeaPeriod = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Постоянная, определяющая степень параллелизма при обработке файлов Nmea.
    /// </summary>
    public const int NmeaMaxDegreeOfParallelism = 10;

    /// <summary>
    /// Поле для хранения периода обработки файлов Adxl.
    /// </summary>
    public static readonly TimeSpan AdxlPeriod = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Постоянная, определяющая степень параллелизма при обработке файлов Adxl.
    /// </summary>
    public const int AdxlMaxDegreeOfParallelism = 10;

    /// <summary>
    /// Поле для хранения периода обработки файлов RawFrame.
    /// </summary>
    public static readonly TimeSpan RawFramePeriod = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Постоянная, определяющая степень параллелизма при обработке файлов RawFrame.
    /// </summary>
    public const int RawFrameMaxDegreeOfParallelism = 10;
}
