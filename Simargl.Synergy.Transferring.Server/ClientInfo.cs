using System.Collections.Generic;

namespace Simargl.Synergy.Transferring.Server;

/// <summary>
/// Представляет информацию о клиенте.
/// </summary>
public sealed class ClientInfo
{
    /// <summary>
    /// Возвращает или инициализирует идентификатор клиента.
    /// </summary>
    public string Identifier { get; init; } = null!;

    /// <summary>
    /// Возвращает или инициализирует пути для сохранения данных.
    /// </summary>
    public List<string> Paths { get; init; } = null!;
}
