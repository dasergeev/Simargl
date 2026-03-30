using Simargl.Recording.Geolocation.Nmea;

namespace Simargl.Trials.Aurora.Aurora01.Gluer.Core;

/// <summary>
/// Представляет информацию о сообщении.
/// </summary>
/// <typeparam name="TMessage">
/// Тип сообщения.
/// </typeparam>
public sealed class NmeaMessageInfo<TMessage>(
    TMessage message, DateTime beginTime,
    DateTime creationTime, DateTime lastAccessTime, DateTime lastWriteTime,
    int index, int count)
    where TMessage : NmeaMessage
{
    /// <summary>
    /// Возвращает сообщение.
    /// </summary>
    public TMessage Message { get; } = message;

    /// <summary>
    /// Возвращает время начала записи файла.
    /// </summary>
    public DateTime BeginTime { get; } = beginTime;

    /// <summary>
    /// Возвращает время создания файла.
    /// </summary>
    public DateTime CreationTime { get; } = creationTime;

    /// <summary>
    /// Возвращает время последнего доступа к файлу.
    /// </summary>
    public DateTime LastAccessTime { get; } = lastAccessTime;

    /// <summary>
    /// Возвращает время последней записи в файл.
    /// </summary>
    public DateTime LastWriteTime { get; } = lastWriteTime;

    /// <summary>
    /// Возвращает индекс сообщения в файле.
    /// </summary>
    public int Index { get; } = index;

    /// <summary>
    /// Возвращает количество сообщений в файле.
    /// </summary>
    public int Count { get; } = count;
}
