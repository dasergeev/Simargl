using Apeiron.Services.GlobalIdentity.Packets;

namespace Apeiron.Platform.Databases.GlobalIdentityDatabase.Entities;

/// <summary>
/// Представляет идентификационное сообщение.
/// </summary>
public class IdentityMessage :
    Entity
{
    /// <summary>
    /// Возвращает или задаёт IP-адрес, с которого было отправлено сообщение.
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт порт, с которого было отправлено сообщение.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Возвращает или задаёт  версию пакета.
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// Возвращает или задаёт глобальный идентификатор.
    /// </summary>
    public GlobalIdentifier GlobalIdentifier { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт идентификатор пакета.
    /// </summary>
    public long PacketIdentifier { get; set; }

    /// <summary>
    /// Возвращает или задаёт источник пакета.
    /// </summary>
    public StatusPacketSource Source { get; set; }

    /// <summary>
    /// Возвращает или задаёт время создания идентификационных данных.
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Возвращает или задаёт время получения сообщения на сервер.
    /// </summary>
    public DateTime ReceiptTime { get; set; }
}
