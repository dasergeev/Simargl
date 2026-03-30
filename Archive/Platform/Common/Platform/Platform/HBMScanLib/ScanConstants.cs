namespace Hbm.Devices.Scan;

/// <summary>
/// Хранилище констант используемых в библиотеке HBMScanLib
/// </summary>
public class ScanConstants
{
    /// <summary>
    /// Мультикаст групповой адрес конфигурирования
    /// </summary>
    public const string configureAddress = "239.255.77.77";
    /// <summary>
    /// IP порт для групповой рассылки конфигурирования
    /// </summary>
    public const string configurePort = "31417";
    /// <summary>
    /// Мультикаст групповой адрес объявления
    /// </summary>    
    public const string announceAddress = "239.255.77.76";
    /// <summary>
    /// 
    /// </summary>
    public const string announcePort = "31416";
    /// <summary>
    /// 
    /// </summary>
    public const string defaultExpirationInSeconds = "20";
}
