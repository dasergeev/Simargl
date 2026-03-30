namespace Simargl.Payload.Obsolete.Strain;

/// <summary>
/// Представляет устаревшую UDP-датаграмму
/// для запроса идентификации тензометрического модуля.
/// </summary>
public sealed class StrainIdentificationObsoleteRequest
{
    /// <summary>
    /// Возвращает датаграмму.
    /// </summary>
    public static ReadOnlyMemory<byte> Datagram { get; } = new(
        [
        0x41, 0x70, 0x72, 0x6e, //  Сигнатура
        0x01, 0x00, 0x00, 0x00, //  Формат
        0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00  //  Размер данных
        ]);
}
