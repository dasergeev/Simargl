using Apeiron.Recording.Adxl357;

namespace Apeiron.Platform.Demo.AdxlDemo.Adxl;

/// <summary>
/// Представляет расширенный пакет датчика ADXL357.
/// </summary>
public sealed class AdxlExtendedPackage
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="receiptTime">
    /// Время получения пакета.
    /// </param>
    /// <param name="deviceProperties">
    /// Текущие свойства устройства.
    /// </param>
    /// <param name="dataPackage">
    /// Пакет данных, полученных от датчика.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="dataPackage"/> передана пустая ссылка.
    /// </exception>
    public AdxlExtendedPackage(DateTime receiptTime, AdxlDeviceProperties deviceProperties, Adxl357DataPackage dataPackage)
    {
        //  Установка значений свойств.
        ReceiptTime = receiptTime;
        DeviceProperties = deviceProperties;
        DataPackage = IsNotNull(dataPackage, nameof(dataPackage));
    }

    /// <summary>
    /// Возвращает время получения пакета.
    /// </summary>
    public DateTime ReceiptTime { get; }

    /// <summary>
    /// Возвращает текущие свойства устройства.
    /// </summary>
    public AdxlDeviceProperties DeviceProperties { get; }

    /// <summary>
    /// Возвращает пакет данных, полученных от датчика.
    /// </summary>
    public Adxl357DataPackage DataPackage { get; }

    /// <summary>
    /// Возвращает или задаёт длительность записи данных.
    /// </summary>
    public TimeSpan? Duration { get; set; }

    /// <summary>
    /// Возвращает или задаёт время начала записи данных.
    /// </summary>
    public DateTime? BeginTime { get; set; }

    /// <summary>
    /// Возвращает время окончания записи данных.
    /// </summary>
    public DateTime? EndTime => BeginTime + Duration;
}
