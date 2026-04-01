using Simargl.AdxlRecorder.IO;
using Simargl.AdxlRecorder.Payload.Common;

namespace Simargl.AdxlRecorder.Payload.Recording;

/// <summary>
/// Представляет UDP-датаграмму.
/// </summary>
public sealed class UdpDatagram :
    DataPackage
{
    /// <summary>
    /// Постоянная, определяющая базовый размер данных.
    /// </summary>
    private const long _BaseSize
        = sizeof(int)       //  FormatVersion
        + sizeof(long)      //  ReceiptTime.Ticks
        + sizeof(byte)      //  address.Length
        + 0                 //  address
        + sizeof(ushort)    //  EndPoint.Port
        + sizeof(ushort)    //  Datagram.Length
        + 0;                //  Datagram

    /// <summary>
    /// Постоянная, определяющая текущую версию формата.
    /// </summary>
    private const int _ActualFormatVersion = 1;

    /// <summary>
    /// Поле для хранения данных датаграммы.
    /// </summary>
    private readonly byte[] _Datagram;

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="formatVersion">
    /// Версия формата.
    /// </param>
    /// <param name="receiptTime">
    /// Время получения.
    /// </param>
    /// <param name="endPoint">
    /// Конечная точка.
    /// </param>
    /// <param name="datagram">
    /// Данные UDP-пакета.
    /// </param>
    private UdpDatagram([NoVerify] int formatVersion, [NoVerify] DateTime receiptTime,
        [NoVerify] IPEndPoint endPoint, [NoVerify] byte[] datagram) :
        base(DataPackageFormat.UdpDatagram)
    {
        //  Установка значений полей.
        FormatVersion = formatVersion;
        ReceiptTime = receiptTime;
        EndPoint = endPoint;
        _Datagram = datagram;
    }

    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="receiptTime">
    /// Время получения.
    /// </param>
    /// <param name="endPoint">
    /// Конечная точка.
    /// </param>
    /// <param name="datagram">
    /// Данные UDP-пакета.
    /// </param>
    public UdpDatagram(DateTime receiptTime, IPEndPoint endPoint, byte[] datagram) :
        this(_ActualFormatVersion, receiptTime,
            IsNotNull(endPoint), IsNotNull(datagram))
    {

    }

    /// <summary>
    /// Возвращает версию формата.
    /// </summary>
    public int FormatVersion { get; }

    /// <summary>
    /// Возвращает время получения.
    /// </summary>
    public DateTime ReceiptTime { get; }

    /// <summary>
    /// Возвращает конечную точку.
    /// </summary>
    public IPEndPoint EndPoint { get; }

    /// <summary>
    /// Возвращает данные UDP-пакета.
    /// </summary>
    public byte[] Datagram => _Datagram;

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
        long size = _BaseSize + address.Length + _Datagram.Length;

        //  Создание распределителя данных потока.
        Spreader spreader = new(stream);

        //  Запись сигнатуры.
        await spreader.WriteInt32Async(Preamble.ActualSignature, cancellationToken).ConfigureAwait(false);

        //  Запись формата.
        await spreader.WriteInt32Async((int)PreambleFormat.RecordingUdpDatagram, cancellationToken).ConfigureAwait(false);

        //  Запись размера.
        await spreader.WriteInt64Async(size, cancellationToken).ConfigureAwait(false);

        //  Запись версии формата.
        await spreader.WriteInt32Async(_ActualFormatVersion, cancellationToken).ConfigureAwait(false);

        //  Запись времени.
        await spreader.WriteInt64Async(ReceiptTime.Ticks, cancellationToken).ConfigureAwait(false);

        //  Запись размера адреса.
        await spreader.WriteUInt8Async((byte)address.Length, cancellationToken).ConfigureAwait(false);

        //  Запись адреса.
        await spreader.WriteBytesAsync(address, cancellationToken).ConfigureAwait(false);

        //  Запись номера порта.
        await spreader.WriteUInt16Async((ushort)EndPoint.Port, cancellationToken).ConfigureAwait(false);

        //  Запись размера датаграммы.
        await spreader.WriteUInt16Async((ushort)_Datagram.Length, cancellationToken).ConfigureAwait(false);

        //  Запись датаграммы.
        await spreader.WriteBytesAsync(_Datagram, cancellationToken).ConfigureAwait(false);
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
    public static new async Task<UdpDatagram> LoadAsync(Stream stream, CancellationToken cancellationToken)
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
        if (format != PreambleFormat.RecordingUdpDatagram)
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
    internal static async Task<UdpDatagram> LoadAsync(Spreader spreader, CancellationToken cancellationToken)
    {
        //  Чтение размера данных.
        long size = await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false);

        //  Чтение версии формата.
        int formatVersion = await spreader.ReadInt32Async(cancellationToken).ConfigureAwait(false);

        //  Проверка версии формата.
        if (formatVersion != _ActualFormatVersion)
        {
            //  Неподдерживаемая версия формата.
            throw new InvalidDataException("Из потока данных прочитана неподдерживаемая версия формата.");
        }

        //  Чтение времени.
        DateTime receiptTime = new(await spreader.ReadInt64Async(cancellationToken).ConfigureAwait(false));

        //  Чтение размера адреса.
        int addressLength = await spreader.ReadUInt8Async(cancellationToken).ConfigureAwait(false);

        //  Чтение адреса.
        byte[] address = await spreader.ReadBytesAsync(addressLength, cancellationToken).ConfigureAwait(false);

        //  Чтение порта.
        int port = await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false);

        //  Чтение размера датаграммы.
        int datagramLength = await spreader.ReadUInt16Async(cancellationToken).ConfigureAwait(false);

        //  Чтение датаграммы.
        byte[] datagram = await spreader.ReadBytesAsync(datagramLength, cancellationToken).ConfigureAwait(false);

        //  Проверка размера данных.
        if (size != _BaseSize + address.Length + datagram.Length)
        {
            //  Недопустимый размер данных.
            throw new InvalidDataException("Из потока данных прочитан недопустимый размер.");
        }

        //  Получение адреса.
        IPAddress ipAddress = new(address);

        //  Получение конечной точки.
        IPEndPoint endPoint = new(ipAddress, port);

        //  Возврат прочитанных данных.
        return new(formatVersion, receiptTime, endPoint, datagram);
    }
}
