using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Simargl.Border.Recorder.Configuring;

/// <summary>
/// Представляет конфигурацию приложения.
/// </summary>
public sealed class Configuration
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    private Configuration()
    {

    }

    /// <summary>
    /// Возвращает порт для подключения датчиков.
    /// </summary>
    public int ConnectingSensorsPort { get; private init; } = 0;

    /// <summary>
    /// Возвращает корневой путь к данным.
    /// </summary>
    public string DataPath { get; private init; } = string.Empty;

    /// <summary>
    /// Возвращает корневой путь к преобразованным данным.
    /// </summary>
    public string ConvertedPath { get; private init; } = string.Empty;

    /// <summary>
    /// Возвращает адрес сервера.
    /// </summary>
    public IPAddress TransferringServer { get; private init; } = null!;

    /// <summary>
    /// Возвращает порт для подключения к серверу.
    /// </summary>
    public int TransferringPort { get; private init; } = 0;

    /// <summary>
    /// Возвращает идентификатор клиента.
    /// </summary>
    public string TransferringIdentifier { get; private init; } = string.Empty;

    /// <summary>
    /// Возвращает минимальную длительность хранения файла.
    /// </summary>
    public TimeSpan TransferringMinDuration { get; private set; } = default;

    /// <summary>
    /// Возвращает время ожидания установки соединения.
    /// </summary>
    public TimeSpan TransferringConnectionTimeout { get; private set; } = default;

    /// <summary>
    /// Возвращает время ожидания передачи данных.
    /// </summary>
    public TimeSpan TransferringDataTimeout { get; private set; } = default;

    /// <summary>
    /// Возвращает корневой путь к передаваемым данным.
    /// </summary>
    public string TransferringPath { get; private init; } = string.Empty;

    /// <summary>
    /// Асинхронно создаёт новый экземпляр.
    /// </summary>
    /// <param name="logger">
    /// Средство ведения журнала.
    /// </param>
    /// <param name="baseConfiguration">
    /// Набор конфигурационных свойств приложения.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, создающая новый экземпляр.
    /// </returns>
    internal static Task<Configuration> CreateAsync(
        ILogger logger, IConfiguration baseConfiguration, CancellationToken cancellationToken)
    {
        //  Проверка токена отмены.
        cancellationToken.ThrowIfCancellationRequested();

        //  Блок перехвата всех исключений.
        try
        {
            //  Загрузка раздела кофигурации.
            IConfigurationSection section = baseConfiguration.GetSection("Configuration");

            //  Создание конфигурации.
            Configuration configuration = new()
            {
                ConnectingSensorsPort = section.GetValue<int>("ConnectingSensorsPort"),
                DataPath = section.GetValue<string>("DataPath") ?? throw new InvalidOperationException("Не удалось прочитать конфигурацию."),
                ConvertedPath = section.GetValue<string>("ConvertedPath") ?? throw new InvalidOperationException("Не удалось прочитать конфигурацию."),
                TransferringServer = IPAddress.Parse(section.GetValue<string>("TransferringServer") ?? throw new InvalidOperationException("Не удалось прочитать конфигурацию.")),
                TransferringPort = section.GetValue<int>("TransferringPort"),
                TransferringIdentifier = section.GetValue<string>("TransferringIdentifier") ?? throw new InvalidOperationException("Не удалось прочитать конфигурацию."),
                TransferringMinDuration = TimeSpan.FromSeconds(section.GetValue<int>("TransferringMinDuration")),
                TransferringConnectionTimeout = TimeSpan.FromSeconds(section.GetValue<int>("TransferringConnectionTimeout")),
                TransferringDataTimeout = TimeSpan.FromSeconds(section.GetValue<int>("TransferringDataTimeout")),
                TransferringPath = section.GetValue<string>("TransferringPath") ?? throw new InvalidOperationException("Не удалось прочитать конфигурацию."),
            };

            //  Возврат конфигурации.
            return Task.FromResult(configuration);
        }
        catch (Exception ex)
        {
            //  Запись ошибки в журнал.
            logger.LogCritical(ex, "Не удалось прочитать конфигурацию.");

            //  Повторный выброс исключения.
            throw;
        }
    }
}
