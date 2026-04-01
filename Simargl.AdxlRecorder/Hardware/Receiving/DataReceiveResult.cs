namespace Simargl.AdxlRecorder.Hardware.Receiving;

/// <summary>
/// Представляет результат получения данных.
/// </summary>
/// <param name="receiptTime">
/// Время получения.
/// </param>
public abstract class DataReceiveResult(DateTime receiptTime)
{
    /// <summary>
    /// Возвращает время получения.
    /// </summary>
    public DateTime ReceiptTime { get; } = receiptTime;

    /// <summary>
    /// Асинхронно сохраняет данные в поток.
    /// </summary>
    /// <param name="stream">
    /// Поток, в который необходимо сохранить данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, сохранающая данные в поток.
    /// </returns>
    public abstract Task SaveAsync(Stream stream, CancellationToken cancellationToken);
}
