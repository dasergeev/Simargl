namespace Apeiron.Services.GlobalIdentity.Packets;

/// <summary>
/// Представляет пакет, сообщающий состояние.
/// </summary>
public class StatusPacket
{
    /// <summary>
    /// Постоянная, определяющая сигнатуру пакета.
    /// </summary>
    private const ulong _Signature = 0xB702BF4D1CAB4668;

    /// <summary>
    /// Постоянная, определяющая размер в байтах пакета версии 1.
    /// </summary>
    private const ushort _SizeV1 = 22;

    /// <summary>
    /// Постоянная, определяющая размер в байтах пакета версии 2.
    /// </summary>
    private const ushort _SizeV2 = _SizeV1 + 17;

    /// <summary>
    /// Постоянная, определяющая размер в байтах пакета версии 3.
    /// </summary>
    /// <remarks>
    /// Так как флаг <see cref="IsValid"/> занимает 1 бит, 7 бит свободно для других флагов.
    /// </remarks>
    private const ushort _SizeV3 = _SizeV2 + 21;

    /// <summary>
    /// Постоянная, определяющая размер пакета в байтах.
    /// </summary>
    private const ushort _Size = _SizeV3;

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
    /// <remarks>
    /// Свойство добавлено в версии 2.
    /// </remarks>
    public long PacketIdentifier { get; }

    /// <summary>
    /// Возвращает или задаёт источник пакета.
    /// </summary>
    /// <remarks>
    /// Свойство добавлено в версии 2.
    /// </remarks>
    public StatusPacketSource Source { get; set; }

    /// <summary>
    /// Возвращает время создания идентификационных данных.
    /// </summary>
    /// <remarks>
    /// Свойство добавлено в версии 2.
    /// </remarks>
    public DateTime Time { get; }

    /// <summary>
    /// Возвращает широту.
    /// </summary>
    /// <remarks>
    /// Свойство добавлено в версии 3.
    /// </remarks>
    public double Latitude { get; init; }

    /// <summary>
    /// Возвращает долготу.
    /// </summary>
    /// <remarks>
    /// Свойство добавлено в версии 3.
    /// </remarks>
    public double Longitude { get; init; }

    /// <summary>
    /// Возвращает скорость.
    /// </summary>
    /// <remarks>
    /// Свойство добавлено в версии 3.
    /// </remarks>
    public int Speed { get; init; }

    /// <summary>
    /// Возвращает флаг достоверности геолокационных данных.
    /// </summary>
    /// <remarks>
    /// Свойство добавлено в версии 3.
    /// </remarks>
    public bool IsValid { get; init; }

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
    /// <param name="time">
    /// Время создания идентификационных данных.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StatusPacket(
        [ParameterNoChecks] int version,
        [ParameterNoChecks] long globalIdentifier,
        [ParameterNoChecks] long packetIdentifier,
        [ParameterNoChecks] DateTime time)
    {
        //  Установка версии пакета.
        Version = version;

        //  Установка глобального идентификатора.
        GlobalIdentifier = globalIdentifier;

        //  Установка идентификатора пакета.
        PacketIdentifier = packetIdentifier;

        //  Установка источника пакета.
        Source = StatusPacketSource.RealTime;

        //  Установка времени создания идентификационных данных.
        Time = time;
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
    /// <param name="time">
    /// Время создания идентификационных данных.
    /// </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StatusPacket(
        [ParameterNoChecks] int version,
        [ParameterNoChecks] long globalIdentifier,
        [ParameterNoChecks] DateTime time) :
        this(version, globalIdentifier, time.Ticks, time)
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="globalIdentifier">
    /// Глобальный идентификатор.
    /// </param>
    public StatusPacket([ParameterNoChecks] long globalIdentifier) :
        this(StaticSettings.StatusPacketVersion, globalIdentifier, DateTime.Now)
    {

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

        //  
        //  Вторая версия:
        //  

        //  Запись идентификатора пакета.
        writer.Write(PacketIdentifier); //  8 байт.

        //  Запись источника пакета.
        writer.Write((byte)Source);     //  1 байт.

        //  Запись времени создания идентификационных данных.
        writer.Write(Time.Ticks);       //  8 байт.

        //  
        //  Третья версия:
        //  

        //  Запись широты.
        writer.Write(Latitude);         //  8 байт

        //  Запись долготы.
        writer.Write(Longitude);        //  8 байт
        
        //  Запись скорости.
        writer.Write(Speed);            //  4 байт

        //  Запись флага.
        writer.Write(IsValid);          //  1 байт (1 бит)


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
    public static bool TryParce(byte[] datagram, out StatusPacket packet)
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

        //  
        //  Вторая версия:
        //  

        //  Идентификатор пакета.
        long packetIdentifier = 0;

        //  Время создания идентификационных данных.
        DateTime time = default;

        //  Источник пакета.
        StatusPacketSource source = StatusPacketSource.RealTime;

        if (version >= 2)
        {
            //  Чтение идентификатора пакета.
            packetIdentifier = reader.ReadInt64();

            //  Чтение источника пакета.
            source = (StatusPacketSource)reader.ReadByte();

            //  Чтение времени создания идентификационных данных.
            time = new(reader.ReadInt64());
        }

        //  
        //  Третья версия:
        //  

        //  Широта.
        double latitude = 0;

        //  Долгота.
        double longitude = 0;
        
        //  Скорость.
        int speed = 0;

        //  Флаг достоверности данных.
        bool isValid = false;

        if (version >= 3)
        {
            //  Чтение широты.
            latitude = reader.ReadDouble();

            //  Чтение долготы.
            longitude = reader.ReadDouble();

            //  Чтение скорости.
            speed = reader.ReadInt32();

            //  Чтение флага достоверности данных.
            isValid = reader.ReadBoolean();
        }

        //  Создание пакета.
        packet = new(version, globalIdentifier, packetIdentifier, time)
        {
            Source = source,
            Latitude = latitude,
            Longitude = longitude,
            Speed = speed,
            IsValid = isValid,
        };

        //  Датаграмма содержит пакет.
        return true;
    }

    /// <summary>
    /// Создаёт ответный пакет.
    /// </summary>
    /// <returns>
    /// Пакет, сообщающий ответ сервера.
    /// </returns>
    public AnswerPacket CreateAnswer()
    {
        //  Создание ответного пакета.
        return new(GlobalIdentifier, PacketIdentifier);
    }
}
