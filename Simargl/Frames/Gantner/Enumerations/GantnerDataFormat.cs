namespace Simargl.Frames.Gantner;

/// <summary>
/// Представляет значение, определяющее формат данных в потоке <see cref="StorageFormat.Gantner"/>.
/// </summary>
public enum GantnerDataFormat
{
    /// <summary>
    /// Отсутствие значения.
    /// </summary>
    None = 0,

    /// <summary>
    /// Логическое значение.
    /// </summary>
    Boolean = 1,

    /// <summary>
    /// Знаковое целое число размером 8 бит.
    /// </summary>
    Int8 = 2,

    /// <summary>
    /// Беззнаковое целое число размером 8 бит.
    /// </summary>
    UInt8 = 3,

    /// <summary>
    /// Знаковое целое число размером 16 бит.
    /// </summary>
    Int16 = 4,

    /// <summary>
    /// Беззнаковое целое число размером 16 бит.
    /// </summary>
    UInt16 = 5,

    /// <summary>
    /// Знаковое целое число размером 32 бита.
    /// </summary>
    Int32 = 6,

    /// <summary>
    /// Беззнаковое целое число размером 32 бита.
    /// </summary>
    UInt32 = 7,

    /// <summary>
    /// Число с плавающей точкой размером 32 бита.
    /// </summary>
    Float32 = 8,

    /// <summary>
    /// Набор битовых флагов размером 8 бит.
    /// </summary>
    BitSet8 = 9,

    /// <summary>
    /// Набор битовых флагов размером 16 бит.
    /// </summary>
    BitSet16 = 10,

    /// <summary>
    /// Набор битовых флагов размером 32 бита.
    /// </summary>
    BitSet32 = 11,

    /// <summary>
    /// Число с плавающей точкой размером 64 бита.
    /// </summary>
    Float64 = 12,

    /// <summary>
    /// Знаковое целое число размером 64 бита.
    /// </summary>
    Int64 = 13,

    /// <summary>
    /// Беззнаковое целое число размером 64 бита.
    /// </summary>
    UInt64 = 14,

    /// <summary>
    /// Набор битовых флагов размером 64 бита.
    /// </summary>
    BitSet64 = 15,
}
