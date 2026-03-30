using System.Text;

namespace Apeiron.Platform.Transmitters;

/// <summary>
/// Представляет пакет с заголовком для <see cref="BinaryTransmitter"/>
/// </summary>
public class TransmitterTextPackage
{
    /// <summary>
    /// Представляет префикс заголовка данных.
    /// </summary>
    public const string Prefix = "APEIRON2022:";

    /// <summary>
    /// Представляет кодировку пакета.
    /// </summary>
    private static readonly Encoding _CodePageInterface = Encoding.UTF8;

    /// <summary>
    /// Представляет сообщение передатчика.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Представляет сообщение передатчика.
    /// </summary>
    public DateTime Time { get; }

    /// <summary>
    /// Представляет идентификатор отправителя.
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    /// Инициализирует объект класса.
    /// </summary>
    /// <param name="message">
    /// Сообщение
    /// </param>
    /// <param name="time">
    /// Время установленное в заголовке.
    /// </param>
    /// <param name="identifier">
    /// Идентификато установленный в заголовке
    /// </param>
    public TransmitterTextPackage(string identifier, DateTime time, string message)
    {
        //  Проверка и установка сообщения
        Message = Check.IsNotNull(message, nameof(message));

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
        using StreamWriter writer = new(memory, _CodePageInterface);

        //  Запись префикса.
        writer.WriteLine(Prefix);

        //  Запись идентификатора.
        writer.WriteLine(Identifier);

        //  Запись времени.
        writer.WriteLine(Time);

        //  Запись размера сообщения.
        writer.WriteLine(Message.Length);

        //  Запись сообщения.
        writer.Write(Message);

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
    public static TransmitterTextPackage FromArray(byte[] array)
    {
        //  Создание потока памяти.
        using MemoryStream memory = new(array);

        //  Создание писателя.
        using StreamReader reader = new(memory, _CodePageInterface);

        //  Чтение строки.
        var line = reader.ReadLine();

        //  Проверка чтения префикса.
        var prefix = Check.IsNotNull(line, nameof(line));

        //  Проверка префикса.
        if (prefix.Equals(Prefix) == false)
        {
            //  Выброс исключения.
            throw new FormatException();
        }

        //  Чтение строки.
        line = reader.ReadLine();

        //  Проверка чтения идентификатора.
        var identifier = Check.IsNotNull(line, nameof(line));

        //  Чтение строки.
        line = reader.ReadLine();

        //  Проверка чтения времени.
        DateTime time = DateTime.Parse(Check.IsNotNull(line, nameof(line)));

        //  Чтение строки.
        line = reader.ReadLine();

        //  Проверка чтения длинны сообщения.
        int length = int.Parse(Check.IsNotNull(line, nameof(line)));

        //  Инициализация буффера.
        var textBlock = new char[length];

        //  Чтение тела сообщения.
        int count = reader.Read(textBlock);

        //  Проверка длинны полученного сообщения.
        if (count != length)
        {
            //  Выброс исключения.
            throw new EndOfStreamException();
        }

        //  Преобразование тела сообщения в строку.
        string message = Check.IsNotNull(textBlock.ToString(), nameof(textBlock));

        //  Создание экземпляра класса.
        TransmitterTextPackage package = new(identifier, time, message);

        //  Возвращение результата.
        return package;
    }
}
