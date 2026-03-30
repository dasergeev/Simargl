using System.Text;

namespace Apeiron.Platform.Transmitters;

/// <summary>
/// Представляет пакет с заголовком для <see cref="BinaryTransmitter"/>
/// </summary>
public class TransmitterBinPackage
{
    /// <summary>
    /// Представляет префикс заголовка данных.
    /// </summary>
    public const long Prefix = 0x3A5429CF953B8D3C;

    /// <summary>
    /// Представляет сообщение передатчика.
    /// </summary>
    public byte[] Data { get; }

    /// <summary>
    /// Представляет сообщение передатчика.
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    /// Представляет идентификатор отправителя.
    /// </summary>
    public long Identifier { get; }

    /// <summary>
    /// Инициализирует объект класса.
    /// </summary>
    /// <param name="data">
    /// Сообщение
    /// </param>
    /// <param name="time">
    /// Время установленное в заголовке.
    /// </param>
    /// <param name="identifier">
    /// Идентификато установленный в заголовке
    /// </param>
    public TransmitterBinPackage(long identifier, DateTime time, byte[] data)
    {
        //  Проверка и установка сообщения
        Data = Check.IsNotNull(data, nameof(data));

        //  Установка времени.
        Time = time;

        //  Установка идентификатора.
        Identifier = identifier;

    }

    /// <summary>
    /// Представляет функцию преобразования данных в строку.
    /// </summary>
    /// <returns></returns>
    public byte[] ToArray()
    {
        //  Создание потока памяти.
        using MemoryStream memory = new();

        //  Создание писателя.
        using BinaryWriter writer = new(memory);

        //  Запись префикса.
        writer.Write(Prefix);

        //  Запись идентификатора.
        writer.Write(Identifier);

        //  Запись времени.
        writer.Write(Time.ToBinary());

        //  Запись размера сообщения.
        writer.Write(Data.Length);

        //  Запись данных.
        writer.Write(Data);

        //  Возврат данных.
        return memory.ToArray();
    }

    /// <summary>
    /// Представляет функцию получения пакета из массива заданной кодировки.
    /// </summary>
    /// <param name="array">Массив.</param>
    /// <returns>Экземпляр класса.</returns>
    /// <exception cref="ArgumentNullException">Ошибка получения строки файла.</exception>
    /// <exception cref="FormatException">Ошибка формата массива.</exception>
    /// <exception cref="EndOfStreamException">Обнаружен не предвиденный конец блока сообщения.</exception>
    public static TransmitterBinPackage FromArray(byte[] array)
    {
        //  Создание потока памяти.
        using MemoryStream memory = new(array);

        //  Создание писателя.
        using BinaryReader reader = new(memory, Encoding.UTF8, true);

        //  Чтениe префикса.
        var prefix = reader.ReadInt64();

        //  Проверка префикса.
        if (prefix.Equals(Prefix) == false)
        {
            //  Выброс исключения.
            throw new FormatException();
        }

        //  Чтениe идентификатора.
        var identifier = reader.ReadInt64();

        //  Чтениe времени.
        DateTime time = new(reader.ReadInt64());

        //  Проверка чтения длинны сообщения.
        int length = reader.ReadInt32();

        //  Преобразование тела сообщения в строку.
        byte[] data = reader.ReadBytes(length);

        //  Создание экземпляра класса.
        TransmitterBinPackage package = new(identifier, time, data);

        //  Возвращение результата.
        return package;
    }
}
