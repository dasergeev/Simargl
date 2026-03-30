namespace Apeiron.Platform.Server.Services.Orchestrator.OrchestratorHostAgent;

/// <summary>
/// Содержит опции для OrchestratorHostAgent
/// </summary>
public class WorkerOptions
{
    /// <summary>
    /// Возвращает или устанавливает IP адрес сервера.
    /// </summary>
    public string ServerIpAdress { get; set; } = string.Empty;
    /// <summary>
    /// Возвращает или устанавливает Port сервера.
    /// </summary>
    public int ServerPortAdress { get; set; }
    /// <summary>
    /// Возвращает или устанавливает время между попытками повторного установление соединения с сервером в мс.
    /// </summary>
    public int TcpReconnectDelay { get; set; }
    /// <summary>
    /// Возвращает или устанавливает время между попытками повторной отправки идентификационного пакета.
    /// </summary>
    public int IdentificationPackageSendDelay { get; set; }
    /// <summary>
    /// Возвращает или устанавливает путь к директории, которая содержит файлы служб для распространения и обновления.
    /// </summary>
    public string SourceDeploymentPath { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает или устанавливает путь к диерктории, которая содержит файлы служб на локальном хосте.
    /// </summary>
    public string LocalPath { get; set; } = string.Empty;

    /// <summary>
    /// Содержит список служб для управления на локальном хосте.
    /// </summary>
    public List<string> ListOfService { get; set;} = new List<string>();
}

