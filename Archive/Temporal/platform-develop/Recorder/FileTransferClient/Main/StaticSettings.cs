namespace Apeiron.Services.FileTransfer;

/// <summary>
/// Предоставляет статические настройки.
/// </summary>
public static class StaticSettings
{
    /// <summary>
    /// Постоянная, определяющая время запроса на соединение в миллисекундах.
    /// </summary>
    public const int RequestTimeout = 60000;

    /// <summary>
    /// Постоянная, определяющая время ожидания подтверждения соединения.
    /// </summary>
    public const int ResponseTimeout = 2 * RequestTimeout;

    /// <summary>
    /// Постоянная, определяющая время ожидания пакета данных.
    /// </summary>
    public const int ParcelTimeout = 180000;

    /// <summary>
    /// Постоянная, определяющая время ожидания уведомления о получении пакета данных.
    /// </summary>
    public const int NoticeTimeout = ParcelTimeout + ResponseTimeout;
}
