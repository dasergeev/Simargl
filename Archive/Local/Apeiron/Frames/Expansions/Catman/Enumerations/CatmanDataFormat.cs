namespace Apeiron.Frames.Catman;

/// <summary>
/// Значение, определяющее формат данных канала.
/// </summary>
public enum CatmanDataFormat
{
    /// <summary>
    /// Числовой формат с плавающей точкой двойной точности.
    /// </summary>
    DoubleNumeric = 0,

    /// <summary>
    /// Формат текстовых данных.
    /// </summary>
    String = 1,

    /// <summary>
    /// Формат бинарного объекта.
    /// </summary>
    BinaryObject = 2,

    /// <summary>
    /// Числовой формат с плавающей точкой одинарной точности.
    /// </summary>
    SingleNumeric = 4
}
