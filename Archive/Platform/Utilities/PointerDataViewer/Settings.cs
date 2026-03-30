namespace PointerDataViewer;

/// <summary>
/// Предоставляет настройки.
/// </summary>
public static class Settings
{

    /// <summary>
    /// Постоянная, определяющая размер пакета данных от датчика.
    /// </summary>
    public const int AdxlPackageSize = 636;

    /// <summary>
    /// Постоянная, определяющая время ожидания данных от датчика Adxl в милисекундах.
    /// </summary>
    public const int AdxlWaitingTimeout = 1000;

    /// <summary>
    /// Постоянная, определяющая время ожидания данных от датчика MTP в милисекундах.
    /// </summary>
    public const int MtpWaitingTimeout = 1000;


}
