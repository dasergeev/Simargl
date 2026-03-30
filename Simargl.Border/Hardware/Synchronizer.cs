namespace Simargl.Border.Hardware;

/// <summary>
/// Представляет синхронизатор модулей.
/// </summary>
public sealed class Synchronizer
{
    /// <summary>
    /// Возвращает последнее время.
    /// </summary>
    private DateTime _LastTime;

    /// <summary>
    /// Возвращает значение, определяющее включен ли синхронизатор модулей.
    /// </summary>
    public bool IsEnable { get; private set; } = false;

    /// <summary>
    /// Возвращает последний синхромаркер.
    /// </summary>
    public Synchromarker LastSynchromarker { get; private set; }

    /// <summary>
    /// Регистрирует новую точку.
    /// </summary>
    /// <param name="time">
    /// Время получения точки.
    /// </param>
    /// <param name="synchromarker">
    /// Синхромаркер.
    /// </param>
    public void Register(DateTime time, Synchromarker synchromarker)
    {
        //  Проверка последнего синхромаркера.
        if (LastSynchromarker - synchromarker < 0)
        {
            //  Установка последнего синхромаркера.
            LastSynchromarker = synchromarker;

            //  Установка последнего времени.
            _LastTime = time;

            //  Включение синхронизатора.
            IsEnable = true;
        }
    }

    /// <summary>
    /// Выполняет преобразование.
    /// </summary>
    /// <param name="synchromarker">
    /// Синхромаркер.
    /// </param>
    /// <returns>
    /// Время.
    /// </returns>
    public DateTime Convert(Synchromarker synchromarker)
    {
        //  Возврат времени.
        return _LastTime.AddSeconds((synchromarker - LastSynchromarker) / 40.0);
    }
}
