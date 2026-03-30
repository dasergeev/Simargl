namespace Simargl.Recording.Geolocation;

/// <summary>
/// Представляет значение, определяющее способ вычисления координат.
/// </summary>
public enum GpsSolution
{
    /// <summary>
    /// Недоступное решение.
    /// </summary>
    NotAvailable = 0,

    /// <summary>
    /// Автономное решение.
    /// </summary>
    Autonomous = 1,

    /// <summary>
    /// Дифференциальное решение.
    /// </summary>
    Differential = 2,

    /// <summary>
    /// Решение по секундной метке.
    /// </summary>
    Pps = 3,

    /// <summary>
    /// Решение на основе фиксированной кинематики реального времени.
    /// </summary>
    FixedRtk = 4,

    /// <summary>
    /// Решение на основе нефиксированной кинематики реального времени.
    /// </summary>
    FloatRtk = 5,

    /// <summary>
    /// Решение на основе экстраполяции.
    /// </summary>
    Extrapolation = 6,

    /// <summary>
    /// Решение, возвращающее фиксированные координаты.
    /// </summary>
    FixedCoordinates = 7,

    /// <summary>
    /// Решение в режиме симуляции.
    /// </summary>
    Simulation = 8,

    /// <summary>
    /// Неизвестное решение.
    /// </summary>
    Unknown = 9,
}
