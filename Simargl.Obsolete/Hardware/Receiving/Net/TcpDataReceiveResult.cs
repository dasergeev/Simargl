using Simargl.Payload.Recording;
using System.IO;
using System.Net;

namespace Simargl.Hardware.Receiving.Net;

/// <summary>
/// Представляет результат получения данных UDP.
/// </summary>
/// <param name="endPoint">
/// Удалённая конечная точка.
/// </param>
/// <param name="startTime">
/// Время начала записи.
/// </param>
/// <param name="connectionKey">
/// Ключ соединения.
/// </param>
/// <param name="blockIndex">
/// Индекс блока.
/// </param>
/// <param name="receiptTime">
/// Время получения.
/// </param>
/// <param name="data">
/// Данные блока.
/// </param>
public sealed class TcpDataReceiveResult(
    IPEndPoint endPoint, DateTime startTime, long connectionKey,
    int blockIndex, DateTime receiptTime, byte[] data) :
    DataReceiveResult(receiptTime)
{
    /// <summary>
    /// Возвращает удалённую конечную точку.
    /// </summary>
    public IPEndPoint EndPoint { get; } = IsNotNull(endPoint);

    /// <summary>
    /// Возвращает время начала записи.
    /// </summary>
    public DateTime StartTime { get; } = startTime;

    /// <summary>
    /// Возвращает ключ соединения.
    /// </summary>
    public long ConnectionKey { get; } = connectionKey;

    /// <summary>
    /// Возвращает индекс блока.
    /// </summary>
    public int BlockIndex { get; } = blockIndex;

    /// <summary>
    /// Возвращает данные блока.
    /// </summary>
    public byte[] Data { get; } = IsNotNull(data);

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
        TcpDataBlock dataBlock = new(EndPoint, StartTime, ConnectionKey, BlockIndex, ReceiptTime, Data);

        //  Сохранение пакета данных в поток.
        await dataBlock.SaveAsync(stream, cancellationToken).ConfigureAwait(false);
    }
}
