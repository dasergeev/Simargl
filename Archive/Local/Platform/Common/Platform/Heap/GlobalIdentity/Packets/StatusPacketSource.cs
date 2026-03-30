namespace Apeiron.Services.GlobalIdentity.Packets;

/// <summary>
/// Представляет источник пакета, сообщающего состояние.
/// </summary>
public enum StatusPacketSource :
    byte
{
    /// <summary>
    /// Сформирован в режиме реального времени.
    /// </summary>
    RealTime,

    /// <summary>
    /// Получен из истории.
    /// </summary>
    History,
}
