namespace Simargl.Hardware.Recorder.Core;

/// <summary>
/// Представляет настройки сердца приложения.
/// </summary>
public sealed class HeartOptions
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; } = "DefaultName";

    /// <summary>
    /// 
    /// </summary>
    public int Interval { get; set; } = 1000;

    /// <summary>
    /// Возвращает или задаёт максимальную длину журнала.
    /// </summary>
    public int LogMaxLength { get; set; } = 100;

    /// <summary>
    /// Возвращает или задаёт путь к данным.
    /// </summary>
    public string DataPath { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или задаёт адрес сервера для приёма данных геолокации.
    /// </summary>
    public string GeolocationServer { get; set; } = string.Empty;

    /// <summary>
    /// Адрес сервера.
    /// </summary>
    public string TransferringServer { get; set; } = string.Empty;

    /// <summary>
    /// Номер порта для подключения к серверу.
    /// </summary>
    public int TransferringPort { get; set; } = 0;

    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public string TransferringIdentifier { get; set; } = string.Empty;

    /// <summary>
    /// Путь к файлам для передачи.
    /// </summary>
    public string TransferringPath { get; set; } = string.Empty;

    /// <summary>
    /// Минимальная длительность хранения файла в секундах.
    /// </summary>
    public int TransferringMinDuration { get; set; } = 300;

    /// <summary>
    /// Время ожидания установки соединения в секундах.
    /// </summary>
    public int TransferringConnectionTimeout { get; set; } = 0;

    /// <summary>
    /// Время ожидания передачи данных в секундах.
    /// </summary>
    public int TransferringDataTimeout { get; set; } = 60;
}
