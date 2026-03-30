namespace Simargl.Journaling;

/// <summary>
/// Представляет запись в журнале.
/// </summary>
/// <param name="text">
/// Текст записи.
/// </param>
/// <param name="level">
/// Значение, определяющее уровень записи в журнале.
/// </param>
/// <param name="time">
/// Время записи.
/// </param>
/// <exception cref="ArgumentNullException">
/// В параметре <paramref name="text"/> передана пустая ссылка.
/// </exception>
/// <exception cref="ArgumentOutOfRangeException">
/// В параметре <paramref name="level"/> передано значение,
/// которое не содержится в перечислении <see cref="JournalRecordLevel"/>.
/// </exception>
public sealed class JournalRecord(string text, JournalRecordLevel level, DateTime time)
{
    /// <summary>
    /// Возвращает текст записи.
    /// </summary>
    public string Text { get; } = IsNotNull(text);

    /// <summary>
    /// Возвращает значение, определяющее уровень записи в журнале.
    /// </summary>
    public JournalRecordLevel Level { get; } = IsDefined(level);

    /// <summary>
    /// Возвращает время записи.
    /// </summary>
    public DateTime Time { get; } = time;
}
