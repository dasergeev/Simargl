namespace Simargl.Synergy.Hub;

/// <summary>
/// Представляет настройки.
/// </summary>
public sealed class Tunings
{
    /// <summary>
    /// Возвращает или инициализирует порт для подключения к узлу.
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// Возвращает или задаёт путь к сертификатам.
    /// </summary>
    public string EncryptPath { get; init; } = null!;
    
    /// <summary>
    /// Возвращает или задаёт пароль от PFX-файла.
    /// </summary>
    public string PfxPassword { get; init; } = null!;

    ///// <summary>
    ///// Возвращает список клиентов.
    ///// </summary>
    //public List<ClientInfo> Clients { get; init; } = [];
}
