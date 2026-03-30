namespace Apeiron.Services.GlobalIdentity.Tunings;

/// <summary>
/// Класс для передачи аргументов события.
/// </summary>
public class TuningEventArgs : EventArgs
{
    /// <summary>
    /// Возвращает настройки.
    /// </summary>
    public AppTuning AppTuning { get; private set; }

    /// <summary>
    /// Инициализирует класс.
    /// </summary>
    /// <param name="appTuning">Настройки.</param>
    public TuningEventArgs(AppTuning appTuning)
    {
        AppTuning = appTuning;
    }
}

