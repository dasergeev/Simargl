namespace Apeiron.Platform.Demo.AdxlDemo;

/// <summary>
/// Представляет аргументы для события <see cref="Synchronizer.Tick"/>.
/// </summary>
public sealed class SynchronizerEventArgs :
    EventArgs
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="time">
    /// Время отметки синхронизатора.
    /// </param>
    public SynchronizerEventArgs(DateTime time)
    {
        //  Установка времени отметки синхронизатора.
        Time = time;
    }

    /// <summary>
    /// Возвращает время отметки синхронизатора.
    /// </summary>
    public DateTime Time { get; }
}
