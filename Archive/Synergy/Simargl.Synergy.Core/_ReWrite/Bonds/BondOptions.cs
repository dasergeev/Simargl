//namespace Simargl.Synergy.Core.Bonds;

///// <summary>
///// Представляет настройки соединения.
///// </summary>
//internal class BondOptions
//{
//    /// <summary>
//    /// Поле для хранения минимального времени простоя при отправке порций.
//    /// </summary>
//    public readonly TimeSpan MinSendingDelay = TimeSpan.FromMilliseconds(10);

//    /// <summary>
//    /// Поле для хранения максимального времени простоя при отправке порций.
//    /// </summary>
//    public readonly TimeSpan MaxSendingDelay = TimeSpan.FromMilliseconds(1000);

//    /// <summary>
//    /// Поле для хранения минимального периода активности соединения.
//    /// </summary>
//    public readonly TimeSpan MinActivityPeriod = TimeSpan.FromMilliseconds(1000);

//    /// <summary>
//    /// Поле для хранения максимального периода активности соединения.
//    /// </summary>
//    public readonly TimeSpan MaxActivityPeriod = TimeSpan.FromMilliseconds(60_000);

//    /// <summary>
//    /// Инициализирует новый экземпляр.
//    /// </summary>
//    /// <param name="sendingDelay">
//    /// Время простоя при отправке порций.
//    /// </param>
//    /// <param name="activityPeriod">
//    /// Период активности соединения.
//    /// </param>
//    public BondOptions(TimeSpan sendingDelay, TimeSpan activityPeriod)
//    {
//        //  Проверка вхходных параметров.
//        ArgumentOutOfRangeException.ThrowIfLessThan(sendingDelay, MinSendingDelay);
//        ArgumentOutOfRangeException.ThrowIfGreaterThan(sendingDelay, MaxSendingDelay);
//        ArgumentOutOfRangeException.ThrowIfLessThan(activityPeriod, MinActivityPeriod);
//        ArgumentOutOfRangeException.ThrowIfGreaterThan(activityPeriod, MaxActivityPeriod);

//        //  Установка значений полей.
//        SendingDelay = sendingDelay;
//        ActivityPeriod = activityPeriod;
//    }

//    /// <summary>
//    /// Возвращает время простоя при отправке порций.
//    /// </summary>
//    public TimeSpan SendingDelay { get; }

//    /// <summary>
//    /// Возвращает период активности соединения.
//    /// </summary>
//    public TimeSpan ActivityPeriod { get; }
//}
