using Apeiron.IO;
using System.Net;

namespace Apeiron.Services.GlobalIdentity.Tunings;

/// <summary>
/// Представляет настройки службы, работающей на клиенте.
/// </summary>
public class ClientTuning :
    Tuning
{
    /// <summary>
    /// Поле для хранения пути к каталогу для сохранения временной истории.
    /// </summary>
    private string _HistoryPath = null!;

    /// <summary>
    /// Возвращает или инициализирует глобальный идентификатор.
    /// </summary>
    public long GlobalIdentifier { get; init; } = 0;

    /// <summary>
    /// Возвращает или инициализирует имя домена для подключения.
    /// </summary>
    public string Domain { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует номер порта для подключения.
    /// </summary>
    public int Port { get; init; } = 0;

    /// <summary>
    /// Возвращает или инициализирует номер порта приема данных сервиса геолокации.
    /// </summary>
    public int GeolocationServicePort { get; init; } = 0;

    /// <summary>
    /// Возвращает или инициализирует время достоверности пакета сервиса геолокации.
    /// </summary>
    public int GeolocationPackageLifeTimeSecond { get; init; } = 0;

    /// <summary>
    /// Возвращает или инициализирует период формирования идентификационных данных в миллисекундах.
    /// </summary>
    public int Period { get; init; } = 0;

    /// <summary>
    /// Возвращает или инициализирует количество пакетов отправляемых в режиме реального времени
    /// без повторного запроса адресов сервера.
    /// </summary>
    public int RealTimePacketCount { get; init; } = 0;

    /// <summary>
    /// Возвращает или инициализирует путь к каталогу для сохранения временной истории.
    /// </summary>
    public string HistoryPath
    {
        get => _HistoryPath;
        init => _HistoryPath = value;
    }

    /// <summary>
    /// Возвращает или инициализирует таймаут отправки истории пакетов в миллисекундах.
    /// </summary>
    public int HistoryTimeout { get; init; } = 0;


    /// <summary>
    /// Возвращает или инициализирует таймаут в случае возникновения исключения.
    /// </summary>
    public int ExceptionsTimeout { get; init; } = 0;

    /// <summary>
    /// Возвращает путь к файлу, содержащему пакет с указанным идентификатором.
    /// </summary>
    /// <param name="identifier">
    /// Идентификатор пакета.
    /// </param>
    /// <returns>
    /// Путь к файлу.
    /// </returns>
    public string GetHistoryPacketPath(long identifier)
    {
        //  Построение пути.
        return PathBuilder.Combine(HistoryPath, $"{identifier:000-000-000-000-000-000-000}.packet");
    }

    /// <summary>
    /// Выполняет проверку настроек.
    /// </summary>
    public override void Validation()
    {
        //  Проверка глобального идентификатора.
        Check.IsPositive(GlobalIdentifier, nameof(GlobalIdentifier));

        //  Проверка имени домена для подключения.
        Check.IsNotNull(Domain, nameof(Domain));

        //  Проверка номера порта для подключения.
        Check.IsInRange(Port, IPEndPoint.MinPort, IPEndPoint.MaxPort, nameof(Port));

        //  Проверка периода формирования идентификационных данных в миллисекундах.
        Check.IsPositive(Period, nameof(Period));

        //  Проверка количества пакетов отправляемых в режиме реального времени
        //  без повторного запроса адресов сервера.
        Check.IsPositive(RealTimePacketCount, nameof(RealTimePacketCount));

        //  Проверка ссылки на путь к каталогу для сохранения временной истории.
        _HistoryPath = PathBuilder.Normalize(Check.IsNotNull(HistoryPath, nameof(HistoryPath)));

        //  Проверка существования каталога.
        if (!Directory.Exists(HistoryPath))
        {
            //  Создание каталога.
            Directory.CreateDirectory(HistoryPath);
        }

        //  Проверка таймаута отправки истории пакетов в миллисекундах.
        Check.IsPositive(HistoryTimeout, nameof(HistoryTimeout));

        //  Проверка таймаута отправки исключения задачи в миллисекундах.
        Check.IsPositive(ExceptionsTimeout, nameof(ExceptionsTimeout));
    }
}
