namespace Simargl.Recording.Geolocation;

/// <summary>
/// Возвращает значение, определяющее режим системы позиционирования.
/// </summary>
public enum GpsMode
{
    /// <summary>
    /// Автономный режим.
    /// </summary>
    Autonomous,

    /// <summary>
    /// Дифференциально-кодовый режим.
    /// </summary>
    Differential,

    /// <summary>
    /// Экстраполяция координат.
    /// </summary>
    Estimated,

    /// <summary>
    /// Ручной режим ввода.
    /// </summary>
    Manual,

    /// <summary>
    /// Режим симулятора.
    /// </summary>
    Simulator,

    /// <summary>
    /// Данные недостоверны.
    /// </summary>
    NotValid,

    /// <summary>
    /// Неизвестный режим.
    /// </summary>
    Unknown,
}
