namespace Simargl.Frames.Mera;

/// <summary>
/// Представляет значение, определяющее формат данных канала.
/// </summary>
public enum MeraDataFormat
{
    /// <summary>
    /// Знаковое целое число размером 1 байт.
    /// </summary>
    Int8,

    /// <summary>
    /// Беззнаковое целое число размером 1 байт.
    /// </summary>
    UInt8,

    /// <summary>
    /// Знаковое целое число размером 2 байта.
    /// </summary>
    Int16,

    /// <summary>
    /// Беззнаковое целое число размером 2 байта.
    /// </summary>
    UInt16,

    /// <summary>
    /// Знаковое целое число размером 4 байта.
    /// </summary>
    Int32,

    /// <summary>
    /// Знаковое целое число размером 8 байт.
    /// </summary>
    Int64,

    /// <summary>
    /// Число с плавающей точкой размером 32 бита.
    /// </summary>
    Float32,

    /// <summary>
    /// Число с плавающей точкой размером 64 бита.
    /// </summary>
    Float64,
}
