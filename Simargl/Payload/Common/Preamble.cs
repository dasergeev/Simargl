namespace Simargl.Payload.Common;

/// <summary>
/// Представляет преамбулу двоичных данных.
/// </summary>
public readonly struct Preamble
{
    /// <summary>
    /// Постоянная, определяющая устаревшую сигнатуру.
    /// </summary>
    public const int ObsoleteSignature = 0x6E727041;

    /// <summary>
    /// Постоянная, определяющая актуальную сигнатуру.
    /// </summary>
    public const int ActualSignature = 0x0DE83994;

    /// <summary>
    /// Постоянная, определяющая значение первого втроенного формата двоичных данных.
    /// </summary>
    public const PreambleFormat FirstEmbeddedFormat = (PreambleFormat)0x10000000;

    /// <summary>
    /// Постоянная, определяющая значение первого управляемого формата двоичных данных.
    /// </summary>
    public const PreambleFormat FirstManagedFormat = (PreambleFormat)0x40000000;

    /// <summary>
    /// Возвращает или инициализирует сигнатуру.
    /// </summary>
    public readonly int Signature { get; init; }

    /// <summary>
    /// Возвращает или инициализирует значение, определяющее формат двоичных данных.
    /// </summary>
    public readonly PreambleFormat Format { get; init; }

    /// <summary>
    /// Возвращает или инициализирует размер последующих данных.
    /// </summary>
    public readonly long Size { get; init; }
}
