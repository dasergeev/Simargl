using System.Globalization;
using System.Text;
using System.Runtime.CompilerServices;
using System.IO;
using Simargl.Designing.Utilities;

namespace Simargl.Recording.Geolocation.Nmea;

/// <summary>
/// Представляет данные сообщения NMEA.
/// </summary>
public sealed class NmeaData
{
    /// <summary>
    /// Постоянная, определяющая минимальную длину сообщения.
    /// </summary>
    private const int _MinLength = 10;

    /// <summary>
    /// Постоянная, определяющая сигнатуру сообщения.
    /// </summary>
    private const char _Signature = '$';

    /// <summary>
    /// Постоянная, определяющаяя разделитель контрольной суммы.
    /// </summary>
    private const char _ChecksumDelimiter = '*';

    /// <summary>
    /// Постоянная, определяющаяя разделитель поля.
    /// </summary>
    private const char _FieldDelimiter = ',';

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="message">
    /// Строка сообщения NMEA.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="message"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверная сигнатура строки в формате NMEA.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверная контрольная сумма строки в формате NMEA.
    /// </exception>
    internal NmeaData(string message)
    {
        //  Проверка ссылки на строку.
        message = IsNotNull(message, nameof(message));

        //  Обрезка сообщения.
        message = message.Trim();

        //  Определение длины сообщения.
        int length = message.Length;

        //  Проверка длины сообщения.
        if (length < _MinLength)
        {
            //  Неверный формат строки в формате NMEA.
            throw ExceptionsCreator.NmeaInvalidFormat();
        }

        //  Проверка символа начала сообщения.
        if (message[0] != _Signature)
        {
            //  Неверная сигнатура строки в формате NMEA.
            throw ExceptionsCreator.NmeaInvalidSignature();
        }

        //  Определение индекса разделителя контрольной суммы.
        int delimiterIndex = message.LastIndexOf(_ChecksumDelimiter);

        //  Проверка индекса разделителя контрольной суммы.
        if (delimiterIndex == -1 || delimiterIndex != length - 3)
        {
            //  Неверный формат строки в формате NMEA.
            throw ExceptionsCreator.NmeaInvalidFormat();
        }

        //  Получение контрольной суммы.
        if (!int.TryParse(message[(delimiterIndex + 1)..],
            NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int checksum))
        {
            //  Неверная контрольная сумма строки в формате NMEA.
            throw ExceptionsCreator.NmeaInvalidChecksum();
        }

        //  Получение основной части сообщения.
        message = message[1..delimiterIndex];

        //  Проверка контрольной суммы.
        if (checksum != GetCheckSumCore(message))
        {
            //  Неверная контрольная сумма строки в формате NMEA.
            throw ExceptionsCreator.NmeaInvalidChecksum();
        }

        //  Проверка индекса разделителя адресного поля.
        if (message.IndexOf(_FieldDelimiter) != 5)
        {
            //  Неверный формат строки в формате NMEA.
            throw ExceptionsCreator.NmeaInvalidFormat();
        }

        //  Установка преамбулы.
        Preamble = new string([message[0], message[1]]);

        //  Установка мнемоники.
        Mnemonics = new string([message[2], message[3], message[4]]);

        //  Получение части сообщения, содержащей поля.
        message = message[6..];

        //  Получение полей.
        string?[] fields = message.Split(_FieldDelimiter);

        //  Получение количества полей.
        int fieldsLength = fields.Length;

        //  Нормализация полей.
        for (int i = 0; i < fieldsLength; i++)
        {
            //  Получение значения поля.
            string? field = fields[i];

            //  Проверка ссылки.
            if (field is not null)
            {
                //  Обрезка поля.
                field = field.Trim();

                //  Проверка пустой строки.
                if (field.Length == 0)
                {
                    //  Установка пустой ссылки.
                    field = null;
                }

                //  Установка значения поля.
                fields[i] = field;
            }
        }

        //  Создание коллекции полей сообщения.
        Fields = new(fields);
    }

    /// <summary>
    /// Возвращает преамбулу сообщения.
    /// </summary>
    public string Preamble { get; }

    /// <summary>
    /// Возвращает мнемонику сообщения.
    /// </summary>
    public string Mnemonics { get; }

    /// <summary>
    /// Возвращает коллекцию полей сообщения.
    /// </summary>
    public NmeaFieldCollection Fields { get; }

    /// <summary>
    /// Возвращает контрольную сумму строки.
    /// </summary>
    /// <param name="text">
    /// Строка, для которой необходимо расчитать контрольную сумму.
    /// </param>
    /// <returns>
    /// Контрольная сумма.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte GetCheckSumCore(string text)
    {
        //  Проверка ссылки на строку.
        text = IsNotNull(text, nameof(text));

        //  Массив байт сообщения.
        byte[] bytes;

        //  Блок перехвата некритических исключений.
        try
        {
            //  Получение массива байт сообщения.
            bytes = Encoding.ASCII.GetBytes(text);
        }
        catch (Exception ex)
        {
            //  Проверка критического исключения.
            if (ex.IsCritical())
            {
                //  Повторный выброс исключения.
                throw;
            }

            //  Неверный формат строки в формате NMEA.
            throw ExceptionsCreator.NmeaInvalidFormat(ex);
        }

        //  Определение длины массива байт.
        int length = bytes.Length;

        //  Проверка длины массива.
        if (length == 0)
        {
            //  Неверный формат строки в формате NMEA.
            throw ExceptionsCreator.NmeaInvalidFormat();
        }

        //  Контрольная сумма.
        byte checksum = bytes[0];

        //  Перебор всех байт сообщения.
        for (int i = 1; i != length; ++i)
        {
            //  Учёт очередного значения.
            checksum ^= bytes[i];
        }

        //  Возврат контрольной суммы.
        return checksum;
    }
}
