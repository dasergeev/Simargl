using Simargl.Text;
using System.Globalization;
using Simargl.Designing;
using System.IO;
using System.Runtime.CompilerServices;
using Simargl.Designing.Utilities;
using Simargl.IO;

namespace Simargl.Frames.TestLab;

/// <summary>
/// Представляет заголовок кадра в формате <see cref="StorageFormat.TestLab"/>.
/// </summary>
public class TestLabFrameHeader :
    FrameHeader
{
    /// <summary>
    /// Постоянная, определяющая максимальную длину названия испытания.
    /// </summary>
    public const int TitleMaxLength = 60;

    /// <summary>
    /// Постоянная, определяющая максимальную длину названия типа испытаний.
    /// </summary>
    public const int CharacterMaxLength = 120;

    /// <summary>
    /// Постоянная, определяющая максимальную длину названия места проведения испытаний.
    /// </summary>
    public const int RegionMaxLength = 120;

    /// <summary>
    /// Постоянная, определяющая длину данных зарезервированных полей в байтах.
    /// </summary>
    public const int ReservedLength = 17;

    /// <summary>
    /// Поле для хранения название испытаний.
    /// </summary>
    string _Title;

    /// <summary>
    /// Поле для хранения названия типа испытаний.
    /// </summary>
    string _Character;

    /// <summary>
    /// Поле для хранения названия места проведения испытаний.
    /// </summary>
    string _Region;

    /// <summary>
    /// Поле для хранения времени записи.
    /// </summary>
    DateTime _Time;

    /// <summary>
    /// Поле для хранения данных зарезервированных полей.
    /// </summary>
    readonly Memory<byte> _Reserved;

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    internal TestLabFrameHeader() :
        this(string.Empty, string.Empty, string.Empty, DateTime.Now, new byte[ReservedLength])
    {

    }

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="title">
    /// Название испытаний.
    /// </param>
    /// <param name="character">
    /// Название типа испытаний.
    /// </param>
    /// <param name="region">
    /// Название места проведения испытаний.
    /// </param>
    /// <param name="time">
    /// Время записи.
    /// </param>
    /// <param name="reserved">
    /// Данные зарезервированных полей.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="title"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="title"/> передана строка, длина которой больше значения <see cref="TitleMaxLength"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="title"/> передана строка, которая содержит недопустимый символ.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="character"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="character"/> передана строка, длина которой больше значения <see cref="CharacterMaxLength"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="character"/> передана строка, которая содержит недопустимый символ.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="region"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="region"/> передана строка, длина которой больше значения <see cref="RegionMaxLength"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="region"/> передана строка, которая содержит недопустимый символ.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="reserved"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="reserved"/> передан массив, длина которого не равна значению <see cref="ReservedLength"/>.
    /// </exception>
    TestLabFrameHeader(string title, string character, string region, DateTime time, Memory<byte> reserved) :
        base(StorageFormat.TestLab)
    {
        //  Установка названия испытаний.
        _Title = IsString(title, TitleMaxLength, nameof(title));

        //  Установка названия типа испытаний.
        _Character = IsString(character, CharacterMaxLength, nameof(character));

        //  Установка названия места проведения испытаний.
        _Region = IsString(region, RegionMaxLength, nameof(region));

        //  Установка времени записи.
        _Time = time;

        //  Проверка длины массива, содержащего данные зарезервированных полей.
        if (reserved.Length != ReservedLength)
        {
            //  Неверная длина массива.
            throw ExceptionsCreator.ArgumentOutOfRange(nameof(reserved));
        }

        //  Создание массива для данных зарезервированных полей.
        _Reserved = new byte[ReservedLength];

        //  Копирование данных зарезервированных полей.
        reserved.CopyTo(_Reserved);
    }

    /// <summary>
    /// Возвращает или задаёт название испытаний.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передана строка, длина которой больше значения <see cref="TitleMaxLength"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передана строка, которая содержит недопустимый символ.
    /// </exception>
    public string Title
    {
        get => _Title;
        set => _Title = IsString(value, TitleMaxLength, nameof(Title));
    }

    /// <summary>
    /// Возвращает или задаёт название типа испытаний.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передана строка, длина которой больше значения <see cref="CharacterMaxLength"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передана строка, которая содержит недопустимый символ.
    /// </exception>
    public string Character
    {
        get => _Character;
        set => _Character = IsString(value, CharacterMaxLength, nameof(Character));
    }

    /// <summary>
    /// Возвращает или задаёт название места проведения испытаний.
    /// </summary>
    /// <exception cref="ArgumentNullException">
    /// Передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передана строка, длина которой больше значения <see cref="RegionMaxLength"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Передана строка, которая содержит недопустимый символ.
    /// </exception>
    public string Region
    {
        get => _Region;
        set => _Region = IsString(value, RegionMaxLength, nameof(Region));
    }

    /// <summary>
    /// Возвращает или задаёт время записи.
    /// </summary>
    public DateTime Time
    {
        get => _Time;
        set => _Time = value;
    }

    /// <summary>
    /// Возвращает данные зарезервированных полей.
    /// </summary>
    public Memory<byte> Reserved => _Reserved;

    /// <summary>
    /// Создаёт копию объекта.
    /// </summary>
    /// <returns>
    /// Копия объекта.
    /// </returns>
    public override FrameHeader Clone()
    {
        //  Возврат копии объекта.
        return new TestLabFrameHeader(_Title, _Character, _Region, _Time, _Reserved);
    }

    /// <summary>
    /// Загружает заголовок кадра из потока.
    /// </summary>
    /// <param name="spreader">
    /// Распределитель данных потока, из которого необходимо загрузить заголовок кадра.
    /// </param>
    /// <returns>
    /// Заголовок кадра и количество каналов в кадре.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="spreader"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат кадра.
    /// </exception>
    internal static (TestLabFrameHeader header, int channelCount) Load(Spreader spreader)
    {
        //  Проверка распределителя данных потока.
        spreader = IsNotNull(spreader, nameof(spreader));

        //  Блок перехвата несистемных исключений.
        try
        {
            ////  Проверка сигнатуры.
            //if (reader.ReadUInt64() != _Signature)
            //{
            //    //  Неверная сигнатура кадра.
            //    throw Exceptions.FrameInvalidSignature();
            //}

            //  Чтение названия испытаний.
            string title = ReadStringCore(spreader, TitleMaxLength);

            //  Чтение названия типа испытаний.
            string character = ReadStringCore(spreader, CharacterMaxLength);

            //  Чтение названия места проведения испытаний.
            string region = ReadStringCore(spreader, RegionMaxLength);

            //  Чтение времени записи.
            DateTime time = ReadTimeCore(spreader);

            //  Чтение количества каналов.
            int channelCount = spreader.ReadUInt16();

            //  Чтение данных зарезервированных полей.
            byte[] reserved = spreader.ReadBytes(ReservedLength);

            //  Создание заголовка кадра.
            TestLabFrameHeader header = new(title, character, region, time, reserved);

            //  Возврат прочитанных данных.
            return (header, channelCount);
        }
        catch (Exception ex)
        {
            //  Проверка исключений для повторного выброса.
            if (ex.IsSystem() || ex is InvalidDataException)
            {
                //  Повторный выброс исключения.
                throw;
            }

            //  Неверный формат кадра.
            throw ExceptionsCreator.FrameInvalidSignature(ex);
        }
    }

    /// <summary>
    /// Выполняет проверку строки.
    /// </summary>
    /// <param name="value">
    /// Строка, которую необходимо проверить.
    /// </param>
    /// <param name="maxLength">
    /// Максимальная длина строки.
    /// </param>
    /// <param name="paramName">
    /// Имя проверяемого параметра.
    /// </param>
    /// <returns>
    /// Проверенная строка.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="value"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="maxLength"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передана строка, длина которой больше максимально допустимой.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="value"/> передана строка, которая содержит недопустимый символ.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string IsString(string value, int maxLength, string? paramName)
    {
        //  Проверка длины строки.
        value = IsLength(value, maxLength, paramName);

        //  Определение длины строки.
        int length = value.Length;

        //  Проверка символов строки.
        for (int i = 0; i < length; i++)
        {
            //  Получение символа.
            char c = value[i];

            //  Проверка вхождения символа в кодировку.
            if (!CyrillicEncoding.Contains(c))
            {
                //  Передана строка, которая содержит недопустимый символ.
                throw ExceptionsCreator.ArgumentStringContainInvalidChar(paramName);
            }

            //  Определение категории символа.
            var category = char.GetUnicodeCategory(c);

            //  Проверка категории символа.
            if (category == UnicodeCategory.Control || category == UnicodeCategory.Format)
            {
                //  Передана строка, которая содержит недопустимый символ.
                throw ExceptionsCreator.ArgumentStringContainInvalidChar(paramName);
            }
        }

        //  Возврат проверенной строки.
        return value;
    }

    /// <summary>
    /// Выполняет чтение строки из потока.
    /// </summary>
    /// <param name="spreader">
    /// Средство чтения двоичных данных.
    /// </param>
    /// <param name="maxLength">
    /// Максимальная длина строки.
    /// </param>
    /// <returns>
    /// Прочитанная строка.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static string ReadStringCore(Spreader spreader, int maxLength)
    {
        //  Чтение массива байт.
        byte[] bytes = spreader.ReadBytes(maxLength + 1);

        //  Создание массива символов.
        char[] chars = new char[maxLength + 1];

        //  Длина прочитанной строки.
        int length = 0;

        //  Разбор данных.
        while (length <= maxLength && bytes[length] != 0)
        {
            //  Получение очередного символа.
            chars[length] = CyrillicEncoding.GetChar(bytes[length]);
                
            //  Установка длины строки.
            ++length;
        }

        //  Проверка длины строки.
        if (length > maxLength)
        {
            //  Корректировка длины строки.
            length = maxLength;
        }

        //  Возврат прочитанный строки.
        return new string(chars, 0, length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static DateTime ReadTimeCore(Spreader spreader)
    {
        const byte nullCode = 0x00;
        const byte digitZeroCode = 0x30;
        const byte fullStopCode = 0x2e;
        const byte colonCode = 0x3a;

        //  Чтение массива данных.
        byte[] bytes = spreader.ReadBytes(20);

        if (bytes[2] != fullStopCode || bytes[5] != fullStopCode || bytes[10] != nullCode ||
            bytes[13] != colonCode || bytes[16] != colonCode || bytes[19] != nullCode)
        {
            throw new Exception();
        }

        int getDigit(int index)
        {
            int digit = bytes[index] - digitZeroCode;
            if (digit > 9)
            {
                throw new Exception();
            }
            return digit;
        }

        int day = getDigit(0) * 10 + getDigit(1);
        int month = getDigit(3) * 10 + getDigit(4);
        int year = getDigit(6) * 1000 + getDigit(7) * 100 + getDigit(8) * 10 + getDigit(9);

        int hour = getDigit(11) * 10 + getDigit(12);
        int minute = getDigit(14) * 10 + getDigit(15); 
        int second = getDigit(17) * 10 + getDigit(18);

        return new DateTime(year, month, day, hour, minute, second);// time;
    }
}
