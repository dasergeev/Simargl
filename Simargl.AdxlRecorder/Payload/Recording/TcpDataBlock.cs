using Simargl.AdxlRecorder.IO;
using Simargl.AdxlRecorder.Payload.Common;

namespace Simargl.AdxlRecorder.Payload.Recording;

/// <summary>
/// Представляет блок данных TCP.
/// </summary>
public sealed class TcpDataBlock :
    DataPackage
{
    /// <summary>
    /// Постоянная, определяющая базовый размер данных.
    /// </summary>
    private const long _BaseSize
        = sizeof(int)       //  FormatVersion
        + sizeof(byte)      //  address.Length
        + 0                 //  address
        + sizeof(ushort)    //  EndPoint.Port
        + sizeof(long)      //  StartTime.Ticks
        + sizeof(long)      //  ConnectionKey
        + sizeof(int)       //  BlockIndex
        + sizeof(long)      //  ReceiptTime.Ticks
        + sizeof(int)       //  Data.Length
        + 0;                //  Data

    /// <summary>
    /// Постоянная, определяющая текущую версию формата.
    /// </summary>
    private const int _ActualFormatVersion = 2;

    /// <summary>
    /// Поле для хранения данных блока.
    /// </summary>
    private readonly byte[] _Data;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="formatVersion">
    /// Версия формата.
    /// </param>
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
    private TcpDataBlock([NoVerify] int formatVersion,
        [NoVerify] IPEndPoint endPoint, [NoVerify] DateTime startTime, [NoVerify] long connectionKey,
        [NoVerify] int blockIndex, [NoVerify] DateTime receiptTime, [NoVerify] byte[] data) :
        base(DataPackageFormat.TcpDataBlock)
    {
        //  Установка значений полей.
        FormatVersion = formatVersion;
        EndPoint = endPoint;
        StartTime = startTime;
        ConnectionKey = connectionKey;
        BlockIndex = blockIndex;
        ReceiptTime = receiptTime;
        _Data = data;
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
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
    public TcpDataBlock(IPEndPoint endPoint, DateTime startTime, long connectionKey,
        int blockIndex, DateTime receiptTime, byte[] data) :
        this(_ActualFormatVersion,
            IsNotNull(endPoint), startTime, connectionKey,
            blockIndex, receiptTime, IsNotNull(data))
    {

    }

    /// <summary>
    /// Возвращает версию формата.
    /// </summary>
    public int FormatVersion { get; }

    /// <summary>
    /// Возвращает удалённую конечную точку.
    /// </summary>
    public IPEndPoint EndPoint { get; }

    /// <summary>
    /// Возвращает время начала записи.
    /// </summary>
    public DateTime StartTime { get; }

    /// <summary>
    /// Возвращает ключ соединения.
    /// </summary>
    public long ConnectionKey { get; }

    /// <summary>
    /// Возвращает индекс блока.
    /// </summary>
    public int BlockIndex { get; }

    /// <summary>
    /// Возвращает время получения.
    /// </summary>
    public DateTime ReceiptTime { get; }

    /// <summary>
    /// Возвращает данные блока.
    /// </summary>
    public byte[] Data => _Data;

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
        //  Получение данных адреса.
        byte[] address = EndPoint.Address.GetAddressBytes();

        //  Определение размера.
        long size = _BaseSize + address.Length + _Data.Length;

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        //  Запись сигнатуры.
        await spreader.WriteInt32Async(Preamble.ActualSignature, cancellationToken).ConfigureAwait(false);

        //  Запись формата.
        await spreader.WriteInt32Async((int)PreambleFormat.RecordingTcpDataBlock, cancellationToken).ConfigureAwait(false);

        //  Запись размера.
        await spreader.WriteInt64Async(size, cancellationToken).ConfigureAwait(false);

        //  Запись версии формата.
        await spreader.WriteInt32Async(_ActualFormatVersion, cancellationToken).ConfigureAwait(false);

        //  Запись размера адреса.
        await spreader.WriteUInt8Async((byte)address.Length, cancellationToken).ConfigureAwait(false);

        //  Запись адреса.
        await spreader.WriteBytesAsync(address, cancellationToken).ConfigureAwait(false);

        //  Запись номера порта.
        await spreader.WriteUInt16Async((ushort)EndPoint.Port, cancellationToken).ConfigureAwait(false);

        //  Запись времени начала записи.
        await spreader.WriteInt64Async(StartTime.Ticks, cancellationToken).ConfigureAwait(false);

        //  Запись ключа соединения.
        await spreader.WriteInt64Async(ConnectionKey, cancellationToken).ConfigureAwait(false);

        //  Запись индекса блока.
        await spreader.WriteInt32Async(BlockIndex, cancellationToken).ConfigureAwait(false);

        //  Запись времени получения.
        await spreader.WriteInt64Async(ReceiptTime.Ticks, cancellationToken).ConfigureAwait(false);

        //  Запись размера данных.
        await spreader.WriteInt32Async(_Data.Length, cancellationToken).ConfigureAwait(false);

        //  Запись данных.
        await spreader.WriteBytesAsync(_Data, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно загружает данные из потока.
    /// </summary>
    /// <param name="stream">
    /// Поток, из которого необходимо загрузить данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая данные из потока.
    /// </returns>
    public static new async Task<TcpDataBlock> LoadAsync(Stream stream, CancellationToken cancellationToken)
    {
        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        //  Чтение сигнатуры.
        int signature = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Проверка сигнатуры.
        if (signature != Preamble.ActualSignature)
        {
            //  Недопустимая сигнатура.
            throw new InvalidDataException("Из потока данных прочитана недопустимая сигнатура.");
        }

        //  Чтение формата.
        PreambleFormat format = (PreambleFormat)await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Проверка формата.
        if (format != PreambleFormat.RecordingTcpDataBlock)
        {
            //  Недопустимый формат.
            throw new InvalidDataException("Из потока данных прочитан недопустимый формат.");
        }

        //  Загрузка данных.
        return await LoadAsync(spreader, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Асинхронно загружает данные из потока.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока, из которого необходимо загрузить данные.
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача, загружающая данные из потока.
    /// </returns>
    internal static async Task<TcpDataBlock> LoadAsync(Spreader spreader, CancellationToken cancellationToken)
    {
        //  Чтение размера данных.
        long size = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);

        //  Чтение версии формата.
        int formatVersion = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Проверка версии формата.
        if (formatVersion < 1 ||
            formatVersion > _ActualFormatVersion)
        {
            //  Неподдерживаемая версия формата.
            throw new InvalidDataException("Из потока данных прочитана неподдерживаемая версия формата.");
        }

        //  Чтение размера адреса.
        int addressLength = await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false);

        //  Чтение адреса.
        byte[] address = await spreader.ReadBytesAsync(addressLength, cancellationToken).ConfigureAwait(false);

        //  Чтение порта.
        int port = await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false);

        //  Чтение начала записи.
        DateTime startTime = new(await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false));

        //  Чтение ключа соединения.
        long connectionKey = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);

        //  Чтение индекса блока.
        int blockIndex = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Чтение времени получения.
        DateTime receiptTime = new(await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false));

        //  Чтение размера данных.
        int dataLength = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Чтение данных.
        byte[] data = await spreader.ReadBytesAsync(dataLength, cancellationToken).ConfigureAwait(false);

        //  Проверка размера данных.
        if ((formatVersion == 1 && size + 2 != _BaseSize + address.Length + data.Length) ||
            (formatVersion == 2 && size != _BaseSize + address.Length + data.Length))
        {
            //  Недопустимый размер данных.
            throw new InvalidDataException("Из потока данных прочитан недопустимый размер.");
        }

        //  Получение адреса.
        IPAddress ipAddress = new(address);

        //  Получение конечной точки.
        IPEndPoint endPoint = new(ipAddress, port);

        //  Возврат прочитанных данных.
        return new(formatVersion, endPoint, startTime, connectionKey,
            blockIndex, receiptTime, data);
    }
}
