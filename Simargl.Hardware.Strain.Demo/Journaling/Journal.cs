using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Simargl.Hardware.Strain.Demo.Journaling;

/// <summary>
/// Пердставляет журнал.
/// </summary>
public sealed class Journal :
    Anything
{
    /// <summary>
    /// Поле для хранения очереди сообщений журнала.
    /// </summary>
    private readonly ConcurrentQueue<JournalRecord> _Queue;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="queue">
    /// Очередь сообщений журнала.
    /// </param>
    public Journal(ConcurrentQueue<JournalRecord> queue)
    {
        //  Обращение к объекту.
        Lay();

        //  Установка очереди сообщений журнала.
        _Queue = queue;
    }

    /// <summary>
    /// Добавляет запись в журнал.
    /// </summary>
    /// <param name="record">
    /// Запись в журнале.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="record"/> передана пустая ссылка.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(JournalRecord record)
    {
        //  Проверка записи.
        IsNotNull(record);

        //  Добавление записи в очередь.
        _Queue.Enqueue(record);
    }

    /// <summary>
    /// Добавляет запись в журнал.
    /// </summary>
    /// <param name="text">
    /// Текст записи в журнал.
    /// </param>
    public void Add(string text)
    {
        //  Создание записи.
        JournalRecord record = new(JournalRecordLevel.Information, text);

        //  Добавление записи в журнал.
        Add(record);
    }

    /// <summary>
    /// Добавляет запись об исключении.
    /// </summary>
    /// <param name="text">
    /// Текст сообщения.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddError(string text)
    {
        //  Создание записи.
        JournalRecord record = new(JournalRecordLevel.Error, text);

        //  Добавление записи в журнал.
        Add(record);
    }

    /// <summary>
    /// Добавляет запись об исключении.
    /// </summary>
    /// <param name="exception">
    /// Исключение.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddError(Exception exception)
    {
        //  Создание записи.
        JournalRecord record = new(JournalRecordLevel.Error, exception.ToString());

        //  Добавление записи в журнал.
        Add(record);
    }
}
