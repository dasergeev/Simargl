using Simargl.Designing;
using System;
using System.Text;

namespace Simargl.Text;

/// <summary>
/// Представляет кодировку Windows-1251.
/// </summary>
public class CyrillicEncoding :
    Encoding
{
    /// <summary>
    /// Представляет код символа по умолчанию.
    /// </summary>
    public const byte DefaultCode = 0xb7;

    /// <summary>
    /// Возвращает значение, указывающее, используются ли в текущей кодировке однобайтовые кодовые точки.
    /// </summary>
    public override bool IsSingleByte => true;

    /// <summary>
    /// Возвращает значение, указывающее, может ли текущая кодировка
    /// использоваться клиентами электронной почты и новостей для сохранения содержимого.
    /// </summary>
    public override bool IsMailNewsSave => true;

    /// <summary>
    /// Возвращает значение, указывающее, может ли текущая кодировка
    /// использоваться клиентами электронной почты и новостей для отображения содержимого.
    /// </summary>
    public override bool IsMailNewsDisplay => true;

    /// <summary>
    /// Возвращает значение, указывающее, может ли текущая кодировка
    /// использоваться клиентами браузера для сохранения содержимого.
    /// </summary>
    public override bool IsBrowserSave => true;

    /// <summary>
    /// Возвращает значение, указывающее, может ли текущая кодировка
    /// использоваться клиентами браузера для отображения содержимого.
    /// </summary>
    public override bool IsBrowserDisplay => true;

    /// <summary>
    /// Возвращает кодовую страницу операционной системы Windows, наиболее точно соответствующую текущей кодировке.
    /// </summary>
    public override int WindowsCodePage => 1251;

    /// <summary>
    /// Возвращает для текущей кодировки имя, зарегистрированное в IANA (Internet Assigned Numbers Authority).
    /// </summary>
    public override string WebName => "windows - 1251";

    /// <summary>
    /// Возвращает имя текущей кодировки, которое может использоваться с тегами заголовка сообщения почтового агента.
    /// </summary>
    public override string HeaderName => "windows - 1251";

    /// <summary>
    /// Возвращает описание текущей кодировки, которое может быть прочитано пользователем.
    /// </summary>
    public override string EncodingName => "Кириллица (Windows)";

    /// <summary>
    /// Возвращает имя текущей кодировки, которое может использоваться с тегами текста сообщения почтового агента.
    /// </summary>
    public override string BodyName => "koi8-r";

    /// <summary>
    /// Возвращает идентификатор кодовой страницы текущего объекта.
    /// </summary>
    public override int CodePage => 1251;

    /// <summary>
    /// Вычисляет количество байтов, полученных при кодировании набора символов.
    /// </summary>
    /// <param name="chars">
    /// Массив символов, содержащий набор кодируемых символов.
    /// </param>
    /// <param name="index">
    /// Индекс первого кодируемого символа.
    /// </param>
    /// <param name="count">
    /// Число кодируемых символов.
    /// </param>
    /// <returns>
    /// Число байтов, полученных при кодировании заданных символов.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="chars"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее или равное длине массива <paramref name="chars"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано значение большее допустимого.
    /// </exception>
    public override int GetByteCount(char[] chars, int index, int count)
    {
        //  Проверка диапазона в массиве.
        IsRange(chars, index, count, nameof(chars), nameof(index), nameof(count));

        //  Возврат числа байтов, полученных при кодировании заданных символов.
        return count;
    }

    /// <summary>
    /// Кодирует набор символов из указанного массива символов в указанный массив байтов.
    /// </summary>
    /// <param name="chars">
    /// Массив символов, содержащий набор кодируемых символов.
    /// </param>
    /// <param name="charIndex">
    /// Индекс первого кодируемого символа.
    /// </param>
    /// <param name="charCount">
    /// Число кодируемых символов.
    /// </param>
    /// <param name="bytes">
    /// Массив байтов, в который будет помещена результирующая последовательность байтов.
    /// </param>
    /// <param name="byteIndex">
    /// Индекс, с которого начинается запись результирующей последовательности байтов.
    /// </param>
    /// <returns>
    /// Фактическое число байтов, записанных в в массив <paramref name="bytes"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="chars"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="charIndex"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="charCount"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="charIndex"/> передано значение,
    /// которое превышает длину массива <paramref name="chars"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Сумма значений параметров <paramref name="charIndex"/> и <paramref name="charCount"/>
    /// превышает длину массива <paramref name="chars"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="bytes"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="byteIndex"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="byteIndex"/> передано значение,
    /// которое превышает длину массива <paramref name="bytes"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Сумма значений параметров <paramref name="byteIndex"/> и <paramref name="charCount"/>
    /// превышает длину массива <paramref name="bytes"/>.
    /// </exception>
    public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
    {
        //  Проверка диапазона символов.
        IsRange(chars, charIndex, charCount, nameof(chars), nameof(charIndex), nameof(charCount));

        //  Проверка диапазона массива байт.
        IsRange(bytes, byteIndex, charCount, nameof(bytes), nameof(byteIndex), nameof(charCount));

        //  Перебор всех символов.
        for (long i = 0; i != charCount; ++i)
        {
            //  Получение кода символа.
            bytes[byteIndex + i] = GetCode(chars[charIndex + i]);
        }

        //  Возврат фактического числа байтов, записанных в массив байтов.
        return charCount;
    }

    /// <summary>
    /// Определяет количество символов, полученных при декодировании последовательности байтов из заданного массива байтов.
    /// </summary>
    /// <param name="bytes">
    /// Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <param name="index">
    /// Индекс первого декодируемого байта.
    /// </param>
    /// <param name="count">
    /// Число байтов для декодирования.
    /// </param>
    /// <returns>
    /// Число символов, полученных при декодировании заданной последовательности байтов.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="bytes"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="index"/> передано значение большее допустимого.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="count"/> передано значение большее допустимого.
    /// </exception>
    public override int GetCharCount(byte[] bytes, int index, int count)
    {
        //  Проверка ссылки на массив.
        IsRange(bytes, index, count, nameof(bytes), nameof(index), nameof(count));

        //  Возврат числа символов, полученных при декодировании заданной последовательности байтов.
        return count;
    }

    /// <summary>
    /// Декодирует последовательность байтов из указанного массива байтов в указанный массив символов.
    /// </summary>
    /// <param name="bytes">
    /// Массив байтов, содержащий последовательность байтов, которую требуется декодировать.
    /// </param>
    /// <param name="byteIndex">
    /// Индекс первого декодируемого байта.
    /// </param>
    /// <param name="byteCount">
    /// Число байтов для декодирования.
    /// </param>
    /// <param name="chars">
    /// Массив символов, в который будет помещен результирующий набор символов.
    /// </param>
    /// <param name="charIndex">
    /// Индекс, с которого начинается запись результирующего набора символов.
    /// </param>
    /// <returns>
    /// Фактическое число символов, записанных в <paramref name="chars"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="bytes"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="byteIndex"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="byteCount"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="byteIndex"/> передано значение,
    /// которое превышает длину массива <paramref name="bytes"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Сумма значений параметров <paramref name="byteIndex"/> и <paramref name="byteCount"/>
    /// превышает длину массива <paramref name="bytes"/>.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="chars"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="charIndex"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="charIndex"/> передано значение,
    /// которое превышает длину массива <paramref name="chars"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Сумма значений параметров <paramref name="charIndex"/> и <paramref name="byteCount"/>
    /// превышает длину массива <paramref name="chars"/>.
    /// </exception>
    public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
    {
        //  Проверка диапазона массива байт.
        IsRange(bytes, byteIndex, byteCount, nameof(bytes), nameof(byteIndex), nameof(byteCount));

        //  Проверка диапазона массива символов.
        IsRange(chars, charIndex, byteCount, nameof(chars), nameof(charIndex), nameof(byteCount));

        //  Перебор всех байтов.
        for (long i = 0; i != byteCount; ++i)
        {
            //  Получение символа.
            chars[charIndex + i] = GetChar(bytes[byteIndex + i]);
        }

        //  Возврат фактического числа прочитанных символов.
        return byteCount;
    }

    /// <summary>
    /// Вычисляет максимальное количество байтов, полученных при кодировании заданного количества символов.
    /// </summary>
    /// <param name="charCount">
    /// Число кодируемых символов.
    /// </param>
    /// <returns>
    /// Максимальное количество байтов, полученных при кодировании заданного количества символов.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="charCount"/> передано отрицательное значение.
    /// </exception>
    public override int GetMaxByteCount(int charCount)
    {
        //  Проверка числа кодируемых символов.
        IsNotNegative(charCount, nameof(charCount));

        //  Возврат максимального количества байтов, полученных при кодировании заданного количества символов.
        return charCount;
    }

    /// <summary>
    /// Вычисляет максимальное количество символов, полученных при декодировании заданного количества байтов.
    /// </summary>
    /// <param name="byteCount">
    /// Число байтов для декодирования.
    /// </param>
    /// <returns>
    /// Максимальное количество символов, полученных при декодировании заданного количества байтов.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="byteCount"/> передано отрицательное значение.
    /// </exception>
    public override int GetMaxCharCount(int byteCount)
    {
        //  Проверка числа байтов для декодирования.
        IsNotNegative(byteCount, nameof(byteCount));

        //  Возврат максимального количества символов, полученных при декодировании заданного количества байтов.
        return byteCount;
    }

    /// <summary>
    /// Создаёт неполную копию текущего объекта.
    /// </summary>
    /// <returns>
    /// Копия текущего объекта.
    /// </returns>
    public override object Clone() => new CyrillicEncoding();

    /// <summary>
    /// Определяет, равен ли указанный объект текущему экземпляру.
    /// </summary>
    /// <param name="value">
    /// Объект для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    /// Результат сравнения.
    /// </returns>
    public override bool Equals(object? value) => value is CyrillicEncoding;

    /// <summary>
    /// Возвращает хэш-код объекта.
    /// </summary>
    /// <returns>
    /// Хэш-код объекта.
    /// </returns>
    public override int GetHashCode() => 1;

    /// <summary>
    /// Возвращает символ.
    /// </summary>
    /// <param name="code">
    /// Код символа.
    /// </param>
    /// <returns>
    /// Символ.
    /// </returns>
    public static char GetChar(byte code)
    {
        return code switch
        {
            0x00 => '\u0000',
            0x01 => '\u0001',
            0x02 => '\u0002',
            0x03 => '\u0003',
            0x04 => '\u0004',
            0x05 => '\u0005',
            0x06 => '\u0006',
            0x07 => '\u0007',
            0x08 => '\u0008',
            0x09 => '\u0009',
            0x0a => '\u000a',
            0x0b => '\u000b',
            0x0c => '\u000c',
            0x0d => '\u000d',
            0x0e => '\u000e',
            0x0f => '\u000f',
            0x10 => '\u0010',
            0x11 => '\u0011',
            0x12 => '\u0012',
            0x13 => '\u0013',
            0x14 => '\u0014',
            0x15 => '\u0015',
            0x16 => '\u0016',
            0x17 => '\u0017',
            0x18 => '\u0018',
            0x19 => '\u0019',
            0x1a => '\u001a',
            0x1b => '\u001b',
            0x1c => '\u001c',
            0x1d => '\u001d',
            0x1e => '\u001e',
            0x1f => '\u001f',
            0x20 => '\u0020',
            0x21 => '\u0021',
            0x22 => '\u0022',
            0x23 => '\u0023',
            0x24 => '\u0024',
            0x25 => '\u0025',
            0x26 => '\u0026',
            0x27 => '\u0027',
            0x28 => '\u0028',
            0x29 => '\u0029',
            0x2a => '\u002a',
            0x2b => '\u002b',
            0x2c => '\u002c',
            0x2d => '\u002d',
            0x2e => '\u002e',
            0x2f => '\u002f',
            0x30 => '\u0030',
            0x31 => '\u0031',
            0x32 => '\u0032',
            0x33 => '\u0033',
            0x34 => '\u0034',
            0x35 => '\u0035',
            0x36 => '\u0036',
            0x37 => '\u0037',
            0x38 => '\u0038',
            0x39 => '\u0039',
            0x3a => '\u003a',
            0x3b => '\u003b',
            0x3c => '\u003c',
            0x3d => '\u003d',
            0x3e => '\u003e',
            0x3f => '\u003f',
            0x40 => '\u0040',
            0x41 => '\u0041',
            0x42 => '\u0042',
            0x43 => '\u0043',
            0x44 => '\u0044',
            0x45 => '\u0045',
            0x46 => '\u0046',
            0x47 => '\u0047',
            0x48 => '\u0048',
            0x49 => '\u0049',
            0x4a => '\u004a',
            0x4b => '\u004b',
            0x4c => '\u004c',
            0x4d => '\u004d',
            0x4e => '\u004e',
            0x4f => '\u004f',
            0x50 => '\u0050',
            0x51 => '\u0051',
            0x52 => '\u0052',
            0x53 => '\u0053',
            0x54 => '\u0054',
            0x55 => '\u0055',
            0x56 => '\u0056',
            0x57 => '\u0057',
            0x58 => '\u0058',
            0x59 => '\u0059',
            0x5a => '\u005a',
            0x5b => '\u005b',
            0x5c => '\u005c',
            0x5d => '\u005d',
            0x5e => '\u005e',
            0x5f => '\u005f',
            0x60 => '\u0060',
            0x61 => '\u0061',
            0x62 => '\u0062',
            0x63 => '\u0063',
            0x64 => '\u0064',
            0x65 => '\u0065',
            0x66 => '\u0066',
            0x67 => '\u0067',
            0x68 => '\u0068',
            0x69 => '\u0069',
            0x6a => '\u006a',
            0x6b => '\u006b',
            0x6c => '\u006c',
            0x6d => '\u006d',
            0x6e => '\u006e',
            0x6f => '\u006f',
            0x70 => '\u0070',
            0x71 => '\u0071',
            0x72 => '\u0072',
            0x73 => '\u0073',
            0x74 => '\u0074',
            0x75 => '\u0075',
            0x76 => '\u0076',
            0x77 => '\u0077',
            0x78 => '\u0078',
            0x79 => '\u0079',
            0x7a => '\u007a',
            0x7b => '\u007b',
            0x7c => '\u007c',
            0x7d => '\u007d',
            0x7e => '\u007e',
            0x7f => '\u007f',
            0x80 => '\u0402',
            0x81 => '\u0403',
            0x82 => '\u201a',
            0x83 => '\u0453',
            0x84 => '\u201e',
            0x85 => '\u2026',
            0x86 => '\u2020',
            0x87 => '\u2021',
            0x88 => '\u20ac',
            0x89 => '\u2030',
            0x8a => '\u0409',
            0x8b => '\u2039',
            0x8c => '\u040a',
            0x8d => '\u040c',
            0x8e => '\u040b',
            0x8f => '\u040f',
            0x90 => '\u0452',
            0x91 => '\u2018',
            0x92 => '\u2019',
            0x93 => '\u201c',
            0x94 => '\u201d',
            0x95 => '\u2022',
            0x96 => '\u2013',
            0x97 => '\u2014',
            0x98 => '\u0098',
            0x99 => '\u2122',
            0x9a => '\u0459',
            0x9b => '\u203a',
            0x9c => '\u045a',
            0x9d => '\u045c',
            0x9e => '\u045b',
            0x9f => '\u045f',
            0xa0 => '\u00a0',
            0xa1 => '\u040e',
            0xa2 => '\u045e',
            0xa3 => '\u0408',
            0xa4 => '\u00a4',
            0xa5 => '\u0490',
            0xa6 => '\u00a6',
            0xa7 => '\u00a7',
            0xa8 => '\u0401',
            0xa9 => '\u00a9',
            0xaa => '\u0404',
            0xab => '\u00ab',
            0xac => '\u00ac',
            0xad => '\u00ad',
            0xae => '\u00ae',
            0xaf => '\u0407',
            0xb0 => '\u00b0',
            0xb1 => '\u00b1',
            0xb2 => '\u0406',
            0xb3 => '\u0456',
            0xb4 => '\u0491',
            0xb5 => '\u00b5',
            0xb6 => '\u00b6',
            0xb7 => '\u00b7',
            0xb8 => '\u0451',
            0xb9 => '\u2116',
            0xba => '\u0454',
            0xbb => '\u00bb',
            0xbc => '\u0458',
            0xbd => '\u0405',
            0xbe => '\u0455',
            0xbf => '\u0457',
            0xc0 => '\u0410',
            0xc1 => '\u0411',
            0xc2 => '\u0412',
            0xc3 => '\u0413',
            0xc4 => '\u0414',
            0xc5 => '\u0415',
            0xc6 => '\u0416',
            0xc7 => '\u0417',
            0xc8 => '\u0418',
            0xc9 => '\u0419',
            0xca => '\u041a',
            0xcb => '\u041b',
            0xcc => '\u041c',
            0xcd => '\u041d',
            0xce => '\u041e',
            0xcf => '\u041f',
            0xd0 => '\u0420',
            0xd1 => '\u0421',
            0xd2 => '\u0422',
            0xd3 => '\u0423',
            0xd4 => '\u0424',
            0xd5 => '\u0425',
            0xd6 => '\u0426',
            0xd7 => '\u0427',
            0xd8 => '\u0428',
            0xd9 => '\u0429',
            0xda => '\u042a',
            0xdb => '\u042b',
            0xdc => '\u042c',
            0xdd => '\u042d',
            0xde => '\u042e',
            0xdf => '\u042f',
            0xe0 => '\u0430',
            0xe1 => '\u0431',
            0xe2 => '\u0432',
            0xe3 => '\u0433',
            0xe4 => '\u0434',
            0xe5 => '\u0435',
            0xe6 => '\u0436',
            0xe7 => '\u0437',
            0xe8 => '\u0438',
            0xe9 => '\u0439',
            0xea => '\u043a',
            0xeb => '\u043b',
            0xec => '\u043c',
            0xed => '\u043d',
            0xee => '\u043e',
            0xef => '\u043f',
            0xf0 => '\u0440',
            0xf1 => '\u0441',
            0xf2 => '\u0442',
            0xf3 => '\u0443',
            0xf4 => '\u0444',
            0xf5 => '\u0445',
            0xf6 => '\u0446',
            0xf7 => '\u0447',
            0xf8 => '\u0448',
            0xf9 => '\u0449',
            0xfa => '\u044a',
            0xfb => '\u044b',
            0xfc => '\u044c',
            0xfd => '\u044d',
            0xfe => '\u044e',
            0xff => '\u044f',
        };
    }

    /// <summary>
    /// Возвращает код символа.
    /// </summary>
    /// <param name="c">
    /// Символ.
    /// </param>
    /// <returns>
    /// Код символа.
    /// </returns>
    public static byte GetCode(char c)
    {
        return c switch
        {
            '\u0000' => 0x00,
            '\u0001' => 0x01,
            '\u0002' => 0x02,
            '\u0003' => 0x03,
            '\u0004' => 0x04,
            '\u0005' => 0x05,
            '\u0006' => 0x06,
            '\u0007' => 0x07,
            '\u0008' => 0x08,
            '\u0009' => 0x09,
            '\u000a' => 0x0a,
            '\u000b' => 0x0b,
            '\u000c' => 0x0c,
            '\u000d' => 0x0d,
            '\u000e' => 0x0e,
            '\u000f' => 0x0f,
            '\u0010' => 0x10,
            '\u0011' => 0x11,
            '\u0012' => 0x12,
            '\u0013' => 0x13,
            '\u0014' => 0x14,
            '\u0015' => 0x15,
            '\u0016' => 0x16,
            '\u0017' => 0x17,
            '\u0018' => 0x18,
            '\u0019' => 0x19,
            '\u001a' => 0x1a,
            '\u001b' => 0x1b,
            '\u001c' => 0x1c,
            '\u001d' => 0x1d,
            '\u001e' => 0x1e,
            '\u001f' => 0x1f,
            '\u0020' => 0x20,
            '\u0021' => 0x21,
            '\u0022' => 0x22,
            '\u0023' => 0x23,
            '\u0024' => 0x24,
            '\u0025' => 0x25,
            '\u0026' => 0x26,
            '\u0027' => 0x27,
            '\u0028' => 0x28,
            '\u0029' => 0x29,
            '\u002a' => 0x2a,
            '\u002b' => 0x2b,
            '\u002c' => 0x2c,
            '\u002d' => 0x2d,
            '\u002e' => 0x2e,
            '\u002f' => 0x2f,
            '\u0030' => 0x30,
            '\u0031' => 0x31,
            '\u0032' => 0x32,
            '\u0033' => 0x33,
            '\u0034' => 0x34,
            '\u0035' => 0x35,
            '\u0036' => 0x36,
            '\u0037' => 0x37,
            '\u0038' => 0x38,
            '\u0039' => 0x39,
            '\u003a' => 0x3a,
            '\u003b' => 0x3b,
            '\u003c' => 0x3c,
            '\u003d' => 0x3d,
            '\u003e' => 0x3e,
            '\u003f' => 0x3f,
            '\u0040' => 0x40,
            '\u0041' => 0x41,
            '\u0042' => 0x42,
            '\u0043' => 0x43,
            '\u0044' => 0x44,
            '\u0045' => 0x45,
            '\u0046' => 0x46,
            '\u0047' => 0x47,
            '\u0048' => 0x48,
            '\u0049' => 0x49,
            '\u004a' => 0x4a,
            '\u004b' => 0x4b,
            '\u004c' => 0x4c,
            '\u004d' => 0x4d,
            '\u004e' => 0x4e,
            '\u004f' => 0x4f,
            '\u0050' => 0x50,
            '\u0051' => 0x51,
            '\u0052' => 0x52,
            '\u0053' => 0x53,
            '\u0054' => 0x54,
            '\u0055' => 0x55,
            '\u0056' => 0x56,
            '\u0057' => 0x57,
            '\u0058' => 0x58,
            '\u0059' => 0x59,
            '\u005a' => 0x5a,
            '\u005b' => 0x5b,
            '\u005c' => 0x5c,
            '\u005d' => 0x5d,
            '\u005e' => 0x5e,
            '\u005f' => 0x5f,
            '\u0060' => 0x60,
            '\u0061' => 0x61,
            '\u0062' => 0x62,
            '\u0063' => 0x63,
            '\u0064' => 0x64,
            '\u0065' => 0x65,
            '\u0066' => 0x66,
            '\u0067' => 0x67,
            '\u0068' => 0x68,
            '\u0069' => 0x69,
            '\u006a' => 0x6a,
            '\u006b' => 0x6b,
            '\u006c' => 0x6c,
            '\u006d' => 0x6d,
            '\u006e' => 0x6e,
            '\u006f' => 0x6f,
            '\u0070' => 0x70,
            '\u0071' => 0x71,
            '\u0072' => 0x72,
            '\u0073' => 0x73,
            '\u0074' => 0x74,
            '\u0075' => 0x75,
            '\u0076' => 0x76,
            '\u0077' => 0x77,
            '\u0078' => 0x78,
            '\u0079' => 0x79,
            '\u007a' => 0x7a,
            '\u007b' => 0x7b,
            '\u007c' => 0x7c,
            '\u007d' => 0x7d,
            '\u007e' => 0x7e,
            '\u007f' => 0x7f,
            '\u0402' => 0x80,
            '\u0403' => 0x81,
            '\u201a' => 0x82,
            '\u0453' => 0x83,
            '\u201e' => 0x84,
            '\u2026' => 0x85,
            '\u2020' => 0x86,
            '\u2021' => 0x87,
            '\u20ac' => 0x88,
            '\u2030' => 0x89,
            '\u0409' => 0x8a,
            '\u2039' => 0x8b,
            '\u040a' => 0x8c,
            '\u040c' => 0x8d,
            '\u040b' => 0x8e,
            '\u040f' => 0x8f,
            '\u0452' => 0x90,
            '\u2018' => 0x91,
            '\u2019' => 0x92,
            '\u201c' => 0x93,
            '\u201d' => 0x94,
            '\u2022' => 0x95,
            '\u2013' => 0x96,
            '\u2014' => 0x97,
            '\u0098' => 0x98,
            '\u2122' => 0x99,
            '\u0459' => 0x9a,
            '\u203a' => 0x9b,
            '\u045a' => 0x9c,
            '\u045c' => 0x9d,
            '\u045b' => 0x9e,
            '\u045f' => 0x9f,
            '\u00a0' => 0xa0,
            '\u040e' => 0xa1,
            '\u045e' => 0xa2,
            '\u0408' => 0xa3,
            '\u00a4' => 0xa4,
            '\u0490' => 0xa5,
            '\u00a6' => 0xa6,
            '\u00a7' => 0xa7,
            '\u0401' => 0xa8,
            '\u00a9' => 0xa9,
            '\u0404' => 0xaa,
            '\u00ab' => 0xab,
            '\u00ac' => 0xac,
            '\u00ad' => 0xad,
            '\u00ae' => 0xae,
            '\u0407' => 0xaf,
            '\u00b0' => 0xb0,
            '\u00b1' => 0xb1,
            '\u0406' => 0xb2,
            '\u0456' => 0xb3,
            '\u0491' => 0xb4,
            '\u00b5' => 0xb5,
            '\u00b6' => 0xb6,
            '\u00b7' => 0xb7,
            '\u0451' => 0xb8,
            '\u2116' => 0xb9,
            '\u0454' => 0xba,
            '\u00bb' => 0xbb,
            '\u0458' => 0xbc,
            '\u0405' => 0xbd,
            '\u0455' => 0xbe,
            '\u0457' => 0xbf,
            '\u0410' => 0xc0,
            '\u0411' => 0xc1,
            '\u0412' => 0xc2,
            '\u0413' => 0xc3,
            '\u0414' => 0xc4,
            '\u0415' => 0xc5,
            '\u0416' => 0xc6,
            '\u0417' => 0xc7,
            '\u0418' => 0xc8,
            '\u0419' => 0xc9,
            '\u041a' => 0xca,
            '\u041b' => 0xcb,
            '\u041c' => 0xcc,
            '\u041d' => 0xcd,
            '\u041e' => 0xce,
            '\u041f' => 0xcf,
            '\u0420' => 0xd0,
            '\u0421' => 0xd1,
            '\u0422' => 0xd2,
            '\u0423' => 0xd3,
            '\u0424' => 0xd4,
            '\u0425' => 0xd5,
            '\u0426' => 0xd6,
            '\u0427' => 0xd7,
            '\u0428' => 0xd8,
            '\u0429' => 0xd9,
            '\u042a' => 0xda,
            '\u042b' => 0xdb,
            '\u042c' => 0xdc,
            '\u042d' => 0xdd,
            '\u042e' => 0xde,
            '\u042f' => 0xdf,
            '\u0430' => 0xe0,
            '\u0431' => 0xe1,
            '\u0432' => 0xe2,
            '\u0433' => 0xe3,
            '\u0434' => 0xe4,
            '\u0435' => 0xe5,
            '\u0436' => 0xe6,
            '\u0437' => 0xe7,
            '\u0438' => 0xe8,
            '\u0439' => 0xe9,
            '\u043a' => 0xea,
            '\u043b' => 0xeb,
            '\u043c' => 0xec,
            '\u043d' => 0xed,
            '\u043e' => 0xee,
            '\u043f' => 0xef,
            '\u0440' => 0xf0,
            '\u0441' => 0xf1,
            '\u0442' => 0xf2,
            '\u0443' => 0xf3,
            '\u0444' => 0xf4,
            '\u0445' => 0xf5,
            '\u0446' => 0xf6,
            '\u0447' => 0xf7,
            '\u0448' => 0xf8,
            '\u0449' => 0xf9,
            '\u044a' => 0xfa,
            '\u044b' => 0xfb,
            '\u044c' => 0xfc,
            '\u044d' => 0xfd,
            '\u044e' => 0xfe,
            '\u044f' => 0xff,
            _ => DefaultCode,
        };
    }

    /// <summary>
    /// Возвращает значение, определяющее содержится ли символ в таблице кодировки.
    /// </summary>
    /// <param name="c">
    /// Символ.
    /// </param>
    /// <returns>
    /// Значение <c>true</c>, если символ содержится в таблице кодировки;
    /// <c>false</c> - в противном случае.
    /// </returns>
    public static bool Contains(char c)
    {
        return c switch
        {
            '\u0000' or '\u0001' or '\u0002' or '\u0003' or '\u0004' or '\u0005' or '\u0006' or '\u0007' or
            '\u0008' or '\u0009' or '\u000a' or '\u000b' or '\u000c' or '\u000d' or '\u000e' or '\u000f' or
            '\u0010' or '\u0011' or '\u0012' or '\u0013' or '\u0014' or '\u0015' or '\u0016' or '\u0017' or
            '\u0018' or '\u0019' or '\u001a' or '\u001b' or '\u001c' or '\u001d' or '\u001e' or '\u001f' or
            '\u0020' or '\u0021' or '\u0022' or '\u0023' or '\u0024' or '\u0025' or '\u0026' or '\u0027' or
            '\u0028' or '\u0029' or '\u002a' or '\u002b' or '\u002c' or '\u002d' or '\u002e' or '\u002f' or
            '\u0030' or '\u0031' or '\u0032' or '\u0033' or '\u0034' or '\u0035' or '\u0036' or '\u0037' or
            '\u0038' or '\u0039' or '\u003a' or '\u003b' or '\u003c' or '\u003d' or '\u003e' or '\u003f' or
            '\u0040' or '\u0041' or '\u0042' or '\u0043' or '\u0044' or '\u0045' or '\u0046' or '\u0047' or
            '\u0048' or '\u0049' or '\u004a' or '\u004b' or '\u004c' or '\u004d' or '\u004e' or '\u004f' or
            '\u0050' or '\u0051' or '\u0052' or '\u0053' or '\u0054' or '\u0055' or '\u0056' or '\u0057' or
            '\u0058' or '\u0059' or '\u005a' or '\u005b' or '\u005c' or '\u005d' or '\u005e' or '\u005f' or
            '\u0060' or '\u0061' or '\u0062' or '\u0063' or '\u0064' or '\u0065' or '\u0066' or '\u0067' or
            '\u0068' or '\u0069' or '\u006a' or '\u006b' or '\u006c' or '\u006d' or '\u006e' or '\u006f' or
            '\u0070' or '\u0071' or '\u0072' or '\u0073' or '\u0074' or '\u0075' or '\u0076' or '\u0077' or
            '\u0078' or '\u0079' or '\u007a' or '\u007b' or '\u007c' or '\u007d' or '\u007e' or '\u007f' or
            '\u0402' or '\u0403' or '\u201a' or '\u0453' or '\u201e' or '\u2026' or '\u2020' or '\u2021' or
            '\u20ac' or '\u2030' or '\u0409' or '\u2039' or '\u040a' or '\u040c' or '\u040b' or '\u040f' or
            '\u0452' or '\u2018' or '\u2019' or '\u201c' or '\u201d' or '\u2022' or '\u2013' or '\u2014' or
            '\u0098' or '\u2122' or '\u0459' or '\u203a' or '\u045a' or '\u045c' or '\u045b' or '\u045f' or
            '\u00a0' or '\u040e' or '\u045e' or '\u0408' or '\u00a4' or '\u0490' or '\u00a6' or '\u00a7' or
            '\u0401' or '\u00a9' or '\u0404' or '\u00ab' or '\u00ac' or '\u00ad' or '\u00ae' or '\u0407' or
            '\u00b0' or '\u00b1' or '\u0406' or '\u0456' or '\u0491' or '\u00b5' or '\u00b6' or '\u00b7' or
            '\u0451' or '\u2116' or '\u0454' or '\u00bb' or '\u0458' or '\u0405' or '\u0455' or '\u0457' or
            '\u0410' or '\u0411' or '\u0412' or '\u0413' or '\u0414' or '\u0415' or '\u0416' or '\u0417' or
            '\u0418' or '\u0419' or '\u041a' or '\u041b' or '\u041c' or '\u041d' or '\u041e' or '\u041f' or
            '\u0420' or '\u0421' or '\u0422' or '\u0423' or '\u0424' or '\u0425' or '\u0426' or '\u0427' or
            '\u0428' or '\u0429' or '\u042a' or '\u042b' or '\u042c' or '\u042d' or '\u042e' or '\u042f' or
            '\u0430' or '\u0431' or '\u0432' or '\u0433' or '\u0434' or '\u0435' or '\u0436' or '\u0437' or
            '\u0438' or '\u0439' or '\u043a' or '\u043b' or '\u043c' or '\u043d' or '\u043e' or '\u043f' or
            '\u0440' or '\u0441' or '\u0442' or '\u0443' or '\u0444' or '\u0445' or '\u0446' or '\u0447' or
            '\u0448' or '\u0449' or '\u044a' or '\u044b' or '\u044c' or '\u044d' or '\u044e' or '\u044f' => true,
            _ => false,
        };
    }
}
