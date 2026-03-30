namespace Apeiron.Frames.Catman;

/// <summary>
/// Представляет значение, определяющее код характеристик фильтра.
/// </summary>
public enum CatmanCodeOfFilterCharacteristic
{
    /// <summary>
    /// Butterworth.
    /// </summary>
    Butterworth = 141,

    /// <summary>
    /// Bessel.
    /// </summary>
    Bessel = 142,

    /// <summary>
    /// Butterworth 4-ого порядка (ML801 only).
    /// </summary>
    Butterworth4thOrder = 144,

    /// <summary>
    /// Bessel 4-ого порядка (ML801 only).
    /// </summary>
    Bessel4thOrder = 145,

    /// <summary>
    /// Без фильтра (Spider8).
    /// </summary>
    NoFilter = 140,

    /// <summary>
    /// Апериодический фильтр (Spider8).
    /// </summary>
    AperiodicFilter = 143,

    /// <summary>
    /// Фиксированный фильтр 1Гц -3 дБ (Spider8).
    /// </summary>
    FixedFilter1Hz = 149,

    /// <summary>
    /// Переменный фильтр (Spider8).
    /// </summary>
    VariableFilter = 148
}
