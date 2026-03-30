namespace Simargl.Payload.Obsolete.Strain;

/// <summary>
/// Представляет устаревшую UDP-датаграмму
/// для ответа на запрос идентификации тензометрического модуля.
/// </summary>
public sealed class StrainIdentificationObsoleteResponse
{
    /// <summary>
    /// Возвращает стандартный заголовок датаграммы.
    /// </summary>
    public static ReadOnlyMemory<byte> StandardHeader { get; } = new(
        [
        0x41, 0x70, 0x72, 0x6e, //  Сигнатура
        0x01, 0x00, 0x00, 0x00, //  Формат
        0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, //  Размер данных
        0xcf, 0x43, 0x4b, 0x6e, 0xe4, 0x8e, 0x75, 0xa2, //  Тип устройства
        ]);

    

    /*


    
    0x6d, 0xa0, 0x9c, 0x00, //  Device Serial Num - серийный номер устройства


    */
}
