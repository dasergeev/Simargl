using System.IO;
using System.Net;

namespace Simargl.Border.Hardware;

/// <summary>
/// Представляет пакет данных от модуля Т.
/// </summary>
public sealed class DevicePackage
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    /// <param name="endPoint">
    /// Точка подключения датчика.
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
    /// Время получения блока.
    /// </param>
    /// <param name="buffer">
    /// Данные пакета.
    /// </param>
    public DevicePackage(IPEndPoint endPoint, DateTime startTime, long connectionKey, int blockIndex, DateTime receiptTime, byte[] buffer)
    {
        //  Установка основных свойств.
        EndPoint = endPoint;
        StartTime = startTime;
        ConnectionKey = connectionKey;
        BlockIndex = blockIndex;
        ReceiptTime = receiptTime;

        //  Создание потока для чтения.
        using MemoryStream stream = new(buffer);

        //  Создание средтва чтения двоичных данных.
        using BinaryReader reader = new(stream);

        //  Чтение длины пакета.
        ushort packageSize = reader.ReadUInt16();

        //  Проверка длины пакета.
        if (packageSize != BasisConstants.SensorPackageSize)
        {
            //  Завершение работы.
            throw new InvalidOperationException(getInfo($"Получен некорректный пакет данных: packageSize = {packageSize}."));
        }

        //  Чтение идентификатора пакета.
        byte id = reader.ReadByte();

        //  Проверка идентификатора пакета.
        if (id != 0x03)
        {
            //  Завершение работы.
            throw new InvalidOperationException(getInfo($"Получен некорректный пакет данных: id = {id}."));
        }

        //  Чтение синхромаркера.
        Synchromarker = new(reader.ReadUInt32());

        //  Создание данных каналов.
        Data0 = new int[50];
        Data1 = new int[50];
        Data2 = new int[50];

        //  Чтение значений.
        for (int i = 0; i != 50; ++i)
        {
            Data0[i] = reader.ReadInt32();
            Data1[i] = reader.ReadInt32();
            Data2[i] = reader.ReadInt32();
        }

        //  Чтение контрольной суммы.
        uint checksum = reader.ReadUInt32();

        //  Проверка контрольной суммы.
        if (checksum != 0xffffffff)
        {
            //  Завершение работы.
            throw new InvalidOperationException(getInfo($"Получен некорректный пакет данных: checksum = {checksum}."));
        }

        string getInfo(string message)
        {
            StringBuilder builder = new();
            builder.AppendLine(message);
            builder.AppendLine($"endPoint = {endPoint}");
            builder.AppendLine($"startTime = {startTime}");
            builder.AppendLine($"connectionKey = {connectionKey}");
            builder.AppendLine($"blockIndex = {blockIndex}");
            builder.AppendLine($"receiptTime = {receiptTime}");
            return builder.ToString();
        }
    }

    /// <summary>
    /// Возвращает точку подключения датчика.
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
    /// Возвращает время получения блока.
    /// </summary>
    public DateTime ReceiptTime { get; }

    /// <summary>
    /// Возвращает синхромаркер.
    /// </summary>
    public Synchromarker Synchromarker { get; }

    /// <summary>
    /// Возвращает данные канала с индексом 0.
    /// </summary>
    public int[] Data0 { get; }

    /// <summary>
    /// Возвращает данные канала с индексом 1.
    /// </summary>
    public int[] Data1 { get; }

    /// <summary>
    /// Возвращает данные канала с индексом 2.
    /// </summary>
    public int[] Data2 { get; }
}
