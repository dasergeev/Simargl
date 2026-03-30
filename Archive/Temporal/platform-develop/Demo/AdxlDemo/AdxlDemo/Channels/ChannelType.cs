namespace Apeiron.Platform.Demo.AdxlDemo.Channels;

/// <summary>
/// Представляет значение, определяющее тип канала.
/// </summary>
public enum ChannelType
{
    /// <summary>
    /// Синхронный канал.
    /// </summary>
    Sync = 0,

    /// <summary>
    /// Асинхронный канал.
    /// </summary>
    Async = 1,

    /// <summary>
    /// Информационный канал.
    /// </summary>
    Info = 2,
}
