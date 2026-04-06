namespace Simargl.Hardware.Strain.Demo.Journaling;

/// <summary>
/// Пердставляет запись в журнале.
/// </summary>
public sealed class JournalRecord
{
    /// <summary>
    /// Поле для хранения следующего ключа.
    /// </summary>
    private static int _NextKey = 0;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="level">
    /// Уровень записи.
    /// </param>
    /// <param name="text">
    /// Текст записи.
    /// </param>
    public JournalRecord(JournalRecordLevel level, string text)
    {
        //  Установка значений свойств.
        Level = IsDefined(level);
        Time = DateTime.Now;
        Text = IsNotNull(text);

        //  Получение ключа записи.
        Key = Interlocked.Increment(ref _NextKey) - 1;
    }

    /// <summary>
    /// Возвращает ключ записи.
    /// </summary>
    public int Key { get; }

    /// <summary>
    /// Возвращает уровень записи.
    /// </summary>
    public JournalRecordLevel Level { get; }

    /// <summary>
    /// Возвращает время записи.
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    /// Возвращает текст записи.
    /// </summary>
    public string Text { get; }
}
