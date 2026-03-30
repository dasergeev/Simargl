namespace Simargl.Projects.Egypt.EgyptXXX.Converter.Core;

/// <summary>
/// Представляет данные тензометрии.
/// </summary>
[CLSCompliant(false)]
public sealed class StrainData(long connectionKey, int blockIndex, DateTime receiptTime, uint serialNumber, float[][] data)
{
    /// <summary>
    /// Возвращает ключ соединения.
    /// </summary>
    public long ConnectionKey { get; } = connectionKey;

    /// <summary>
    /// Возвращает индекс блока.
    /// </summary>
    public int BlockIndex { get; } = blockIndex;

    /// <summary>
    /// Возвращает время получения.
    /// </summary>
    public DateTime ReceiptTime { get; } = receiptTime;

    /// <summary>
    /// Возвращает серийный номер.
    /// </summary>
    public uint SerialNumber { get; } = serialNumber;

    /// <summary>
    /// Возвращает данные.
    /// </summary>
    public float[][] Data { get; } = data;

    /// <summary>
    /// Возвращает или задаёт частоту дискретизации.
    /// </summary>
    public double Sampling { get; set; }
}
