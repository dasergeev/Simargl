namespace Simargl.AdxlRecorder.Hardware.Receiving.Net;

/// <summary>
/// Представляет результат получения данных UDP.
/// </summary>
/// <param name="receiptTime">
/// Время получения.
/// </param>
/// <param name="result">
/// Полученные результаты UDP.
/// </param>
public sealed class UdpDataReceiveResult(DateTime receiptTime, UdpReceiveResult result) :
    DataReceiveResult(receiptTime)
{
    /// <summary>
    /// Возвращает полученные результаты UDP.
    /// </summary>
    public UdpReceiveResult Result { get; } = result;

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
    public override sealed async Task SaveAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Создание пакета данных.
        UdpDatagram datagram = new(ReceiptTime, Result.RemoteEndPoint, Result.Buffer);

        //  Сохранение пакета данных в поток.
        await datagram.SaveAsync(stream, cancellationToken).ConfigureAwait(false);
    }
}
