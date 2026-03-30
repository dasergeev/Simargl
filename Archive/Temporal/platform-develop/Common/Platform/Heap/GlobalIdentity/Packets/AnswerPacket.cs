namespace Apeiron.Services.GlobalIdentity.Packets;

/// <summary>
/// Представляет пакет, сообщающий ответ сервера.
/// </summary>
public class AnswerPacket
{
    /// <summary>
    /// Постоянная, определяющая сигнатуру пакета.
    /// </summary>
    private const ulong _Signature = 0x37371DD5685D4E5E;

    /// <summary>
    /// Постоянная, определяющая размер в байтах пакета версии 1.
    /// </summary>
    private const ushort _SizeV1 = 30;

    /// <summary>
    /// Постоянная, определяющая размер пакета в байтах.
    /// </summary>
    private const ushort _Size = _SizeV1;

    /// <summary>
    /// Возвращает или инициализирует версию пакета.
    /// </summary>
    public int Version { get; }

    /// <summary>
    /// Возвращает глобальный идентификатор.
    /// </summary>
    public long GlobalIdentifier { get; }

    /// <summary>
    /// Возвращает идентификатор пакета.
    /// </summary>
    public long PacketIdentifier { get; }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="globalIdentifier">
    /// Глобальный идентификатор.
    /// </param>
    /// <param name="packetIdentifier">
    /// Идентификатор пакета.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal AnswerPacket(
        [ParameterNoChecks] long globalIdentifier,
        [ParameterNoChecks] long packetIdentifier) :
        this(StaticSettings.AnswerPacketVersion, globalIdentifier, packetIdentifier)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="version">
    /// Версия пакета.
    /// </param>
    /// <param name="globalIdentifier">
    /// Глобальный идентификатор.
    /// </param>
    /// <param name="packetIdentifier">
    /// Идентификатор пакета.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AnswerPacket(
        [ParameterNoChecks] int version,
        [ParameterNoChecks] long globalIdentifier,
        [ParameterNoChecks] long packetIdentifier)
    {
        //  Установка версии пакета.
        Version = version;

        //  Установка глобального идентификатора.
        GlobalIdentifier = globalIdentifier;

        //  Установка идентификатора пакета.
        PacketIdentifier = packetIdentifier;
    }

    /// <summary>
    /// Возвращает датаграмму пакета.
    /// </summary>
    /// <returns>
    /// Датаграмма пакета.
    /// </returns>
    public byte[] GetDatagram()
    {
        //  Создание массива байт.
        byte[] datagram = new byte[_Size];

        //  Создание потока для записи в память.
        using MemoryStream stream = new(datagram);

        //  Создание средства записи в память.
        using BinaryWriter writer = new(stream);

        //  
        //  Первая версия:
        //  

        //  Запись сигнатуры.
        writer.Write(_Signature);       //  8 байт.

        //  Запись размера пакета.
        writer.Write(_Size);            //  2 байта.

        //  Запись версии пакета.
        writer.Write(Version);          //  4 байта.

        //  Запись глобального идентификатора.
        writer.Write(GlobalIdentifier); //  8 байт.

        //  Запись идентификатора пакета.
        writer.Write(PacketIdentifier); //  8 байт.

        //  Возврат датаграммы.
        return datagram;
    }

    /// <summary>
    /// Выполняет разбор датаграммы.
    /// </summary>
    /// <param name="datagram">
    /// Датаграмма.
    /// </param>
    /// <param name="packet">
    /// Пакет, содержащийся в датаграмме.
    /// </param>
    /// <returns>
    /// Результат разбора.
    /// </returns>
    public static bool TryParce(byte[] datagram, out AnswerPacket packet)
    {
        //  Установка недействительного значения пакета.
        packet = null!;

        //  Проверка датаграммы.
        if (datagram is null || datagram.Length < _SizeV1)
        {
            //  Датаграмма не содержит пакет.
            return false;
        }

        //  Создание потока для чтения из памяти.
        using MemoryStream stream = new(datagram);

        //  Создание средства чтения двоичных данных.
        using BinaryReader reader = new(stream);

        //  
        //  Первая версия:
        //  

        //  Чтение сигнатуры.
        if (reader.ReadUInt64() != _Signature)
        {
            //  Датаграмма не содержит пакет.
            return false;
        }

        //  Чтение размера пакета.
        ushort size = reader.ReadUInt16();

        //  Проверка размера пакета.
        if (datagram.Length != size)
        {
            //  Датаграмма не содержит пакет.
            return false;
        }

        //  Чтение версии пакета.
        int version = reader.ReadInt32();

        //  Чтение глобального идентификатора.
        long globalIdentifier = reader.ReadInt64();

        //  Чтение идентификатора пакета.
        long packetIdentifier = reader.ReadInt64();

        //  Создание пакета.
        packet = new(version, globalIdentifier, packetIdentifier);

        //  Датаграмма содержит пакет.
        return true;
    }
}
