using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Simargl.Synergy.Transferring.Server;

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
    /// Возвращает список клиентов.
    /// </summary>
    public List<ClientInfo> Clients { get; init; } = [];
}
