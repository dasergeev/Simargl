namespace Simargl.Border.Modules;

/// <summary>
/// Представляет сырой пакет данных.
/// </summary>
public sealed class RawPackage
{
    /// <summary>
    /// Возвращает длину пакета.
    /// </summary>
    public int Lenght { get; }

    //public 

    /*


    
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
        Synchromarker = reader.ReadUInt32();

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

        ////  Проверка контрольной суммы.
        //if (checksum != 0xffffffff)
        //{
        //    //  Завершение работы.
        //    throw new InvalidOperationException(getInfo($"Получен некорректный пакет данных: checksum = {checksum}."));
        //}

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
    */
}
