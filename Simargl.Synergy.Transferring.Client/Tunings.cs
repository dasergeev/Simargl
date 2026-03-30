namespace Simargl.Synergy.Transferring.Client;

/// <summary>
/// Представляет настройки.
/// </summary>
public sealed class Tunings
{
    /// <summary>
    /// Возвращает или инициализирует адрес сервера.
    /// </summary>
    public string Server { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует порт для подключения к узлу.
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// Возвращает или инициализирует идентификатор клиента.
    /// </summary>
    public string Identifier { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует путь к корневому каталогу с файлами.
    /// </summary>
    public string Path { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует минимальную длительность хранения файла в секундах.
    /// </summary>
    public int MinDuration { get; init; }

    /// <summary>
    /// Возвращает или инициализирует время ожидания установки соединения в секундах.
    /// </summary>
    public int ConnectionTimeout { get; init; }

    /// <summary>
    /// Возвращает или инициализирует время ожидания передачи данных в секундах.
    /// </summary>
    public int DataTimeout { get; init; }
}
