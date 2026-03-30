namespace Apeiron.Platform.Databases.GlobalIdentityDatabase.Entities;

/// <summary>
/// Представляет глобальный идентификатор.
/// </summary>
public class GlobalIdentifier :
    Entity
{
    /// <summary>
    /// Возвращает значение идентификатора.
    /// </summary>
    public long Value { get; set; }

    /// <summary>
    /// Возвращает имя идентифицируемого объекта.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт время получения последних идентификационных данных.
    /// </summary>
    public DateTime LastTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт время создания пакета.
    /// </summary>
    public DateTime LastPacketTime { get; set; }

    /// <summary>
    /// Возвращает или задаёт последний IP-адрес, с которого было отправлено сообщение.
    /// </summary>
    public string LastAddress { get; set; } = null!;

    /// <summary>
    /// Возвращает или задаёт последний порт, с которого было отправлено сообщение.
    /// </summary>
    public int LastPort { get; set; }

    /// <summary>
    /// Возвращает или задаёт версию последнего пакета.
    /// </summary>
    public int LastVersion { get; set; }

    /// <summary>
    /// Возвращает коллекцию идентификационных сообщений.
    /// </summary>
    public ICollection<IdentityMessage> IdentityMessages { get; } = new HashSet<IdentityMessage>();
}
