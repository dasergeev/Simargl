using System.IO;

namespace Simargl.Recording.Synch;

/// <summary>
/// Представляет информацию об источнике.
/// </summary>
/// <param name="beginTime">
/// Время начала записи.
/// </param>
/// <param name="duration">
/// Длительность записи.
/// </param>
/// <param name="fileInfo">
/// Информация о файле.
/// </param>
public sealed class RecordInfo(DateTime beginTime, TimeSpan duration, FileInfo fileInfo)
{
    /// <summary>
    /// Возвращает время начала записи.
    /// </summary>
    public DateTime BeginTime { get; } = beginTime;

    /// <summary>
    /// Возвращает длительность записи.
    /// </summary>
    public TimeSpan Duration { get; } = duration;

    /// <summary>
    /// Возвращает информацию о файле, в котором хранятся данные.
    /// </summary>
    public FileInfo FileInfo { get; } = IsNotNull(fileInfo);
}
