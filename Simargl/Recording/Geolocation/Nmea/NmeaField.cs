using System.Globalization;
using System.Runtime.CompilerServices;
using System.IO;
using Simargl.Designing.Utilities;

namespace Simargl.Recording.Geolocation.Nmea;

/// <summary>
/// Представляет поле сообщения NMEA.
/// </summary>
public sealed class NmeaField
{
    /// <summary>
    /// Возвращает пустое поле.
    /// </summary>
    internal static NmeaField Empty { get; } = new NmeaField(null);

    /// <summary>
    /// Поле для хранения допустимых символов в текстовом представлении целых чисел.
    /// </summary>
    private readonly static char[] _IntegerCharacters =
        ['-', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

    /// <summary>
    /// Поле для хранения допустимых символов в текстовом представлении чисел с плавающей точкой.
    /// </summary>
    private readonly static char[] _FloatingCharacters =
        ['-', '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    /// <param name="text">
    /// Текстовое представление значения поля.
    /// </param>
    internal NmeaField(string? text)
    {
        //  Установка текстового представления значения поля.
        Text = text;
    }

    /// <summary>
    /// Возвращает текстовое представление значения поля.
    /// </summary>
    public string? Text { get; }

    /// <summary>
    /// Выполняет преобразование поля к состоянию.
    /// </summary>
    /// <returns>
    /// Состояние, которое содержится в поле.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Спецификация: "A".
    /// </para>
    /// <para>
    /// <list type="table">
    /// <listheader>
    /// <term>Значение</term>
    /// <description>Описание</description>
    /// </listheader>
    /// <item>
    /// <term>A</term>
    /// <description>
    /// Да, данные действительны, флаг предупреждения снят.
    /// </description>
    /// <term>V</term>
    /// <description>
    /// Нет, данные недействительны, установлен флаг предупреждения.
    /// </description>
    /// </item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public bool? ToStatus()
    {
        //  Получение символа.
        char? value = ToCharacter();

        //  Проверка значения.
        if (value.HasValue)
        {
            //  Возврат состояния на основании значения символа.
            return value switch
            {
                'A' => true,
                'V' => false,
                _ => throw ExceptionsCreator.NmeaInvalidFormat(),
            };
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к целому числу.
    /// </summary>
    /// <param name="lenght">
    /// Требуемая длина текстового представления.
    /// </param>
    /// <returns>
    /// Целое число, которое содержится в поле.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="lenght"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="lenght"/> передано нулевое значение.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public int? ToInteger(int lenght)
    {
        //  Проверка контрольной длины.
        lenght = IsPositive(lenght, nameof(lenght));

        //  Проверка ссылки на текстовое значение.
        if (Text is string text)
        {
            //  Проверка длины текста и символа-разделителя.
            if (SignLengthCore(text) != lenght)
            {
                //  Неверный формат строки в формате NMEA.
                throw ExceptionsCreator.NmeaInvalidFormat();
            }

            //  Возврат значения.
            return ParceIntegerCore(text);
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к целому числу.
    /// </summary>
    /// <param name="lenght">
    /// Требуемая длина текстового представления.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <returns>
    /// Целое число, которое содержится в поле.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="lenght"/> передано отрицательное значение.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// В параметре <paramref name="lenght"/> передано нулевое значение.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public int? ToInteger(int lenght, int minValue, int maxValue)
    {
        //  Проверка контрольной длины.
        lenght = IsPositive(lenght, nameof(lenght));

        //  Проверка ссылки на текстовое значение.
        if (Text is string text)
        {
            //  Проверка длины текста и символа-разделителя.
            if (SignLengthCore(text) != lenght)
            {
                //  Неверный формат строки в формате NMEA.
                throw ExceptionsCreator.NmeaInvalidFormat();
            }

            //  Возврат значения.
            return ParceIntegerCore(text, minValue, maxValue);
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к числу с плавающей точкой.
    /// </summary>
    /// <returns>
    /// Число с плавающей точкой, которое содержится в поле.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public double? ToFloating()
    {
        //  Проверка ссылки на текстовое значение.
        if (Text is string text)
        {
            //  Возврат значения.
            return ParceFloatingCore(text);
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к числу с плавающей точкой.
    /// </summary>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <returns>
    /// Целое число, которое содержится в поле.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public double? ToFloating(double minValue, double maxValue)
    {
        //  Проверка ссылки на текстовое значение.
        if (Text is string text)
        {
            //  Возврат значения.
            return ParceFloatingCore(text, minValue, maxValue);
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к символу.
    /// </summary>
    /// <returns>
    /// Символ, который содержится в поле.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public char? ToCharacter()
    {
        //  Проверка ссылки на текстовое значение.
        if (Text is string text)
        {
            //  Проверка длины текста.
            if (text.Length != 1)
            {
                //  Неверный формат строки в формате NMEA.
                throw ExceptionsCreator.NmeaInvalidFormat();
            }

            //  Возврат значения.
            return text[0];
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля ко времени.
    /// </summary>
    /// <returns>
    /// Время, которое содержится в поле.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public TimeOnly? ToTime()
    {
        //  Проверка ссылки на текстовое значение.
        if (Text is string text)
        {
            //  Определение длины текстового представления.
            int length = text.Length;

            //  Проверка длины текста и символа-разделителя.
            if (length < 6 || length == 7 || (length != 6 && text[6] != '.'))
            {
                //  Неверный формат строки в формате NMEA.
                throw ExceptionsCreator.NmeaInvalidFormat();
            }

            //  Определение часа, минуты, секунды, милисекунды.
            int hour = ParceIntegerCore(text[..2], 0, 23);
            int minute = ParceIntegerCore(text[2..4], 0, 59);
            double second = ParceFloatingCore(text[4..], 0, 60);

            //  Определение интервала времени.
            TimeSpan span = new TimeSpan(hour, minute, 0) + TimeSpan.FromSeconds(second);

            //  Возврат времени.
            return new(span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к дате.
    /// </summary>
    /// <returns>
    /// Дата, которое содержится в поле.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Ожидаемый формат поля ддммгг.
    /// </para>
    /// <para>
    /// Возвращаемый диапазон по годам: от 2000 до 2099.
    /// </para>
    /// </remarks>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public DateOnly? ToDate()
    {
        //  Проверка ссылки на текстовое значение.
        if (Text is string text)
        {
            //  Определение длины текстового представления.
            int length = text.Length;

            //  Проверка длины текста и символа-разделителя.
            if (length != 6)
            {
                //  Неверный формат строки в формате NMEA.
                throw ExceptionsCreator.NmeaInvalidFormat();
            }

            //  Определение дня, месяца и года.
            int day = ParceIntegerCore(text[..2], 0, 31);
            int month = ParceIntegerCore(text[2..4], 0, 12);
            int year = ParceIntegerCore(text[4..6], 0, 99);

            //  Блок перехвата исключений.
            try
            {
                //  Возврат времени.
                return new(year, month, day);
            }
            catch (Exception ex)
            {
                //  Неверный формат строки в формате NMEA.
                throw ExceptionsCreator.NmeaInvalidFormat(ex);
            }
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к широте.
    /// </summary>
    /// <returns>
    /// Широта в градусах, которая содержится в поле.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public double? ToLatitude()
    {
        //  Проверка ссылки на текстовое значение.
        if (Text is string text)
        {
            //  Определение длины текстового представления.
            int length = text.Length;

            //  Проверка длины текста и символа-разделителя.
            if (length < 4 || length == 5 || (length != 4 && text[4] != '.'))
            {
                //  Неверный формат строки в формате NMEA.
                throw ExceptionsCreator.NmeaInvalidFormat();
            }

            //  Определение градусов.
            int degrees = ParceIntegerCore(text[..2], 0, 90);

            //  Определение минут.
            double minutes = ParceFloatingCore(text[2..], 0, 100);

            //  Возврат широты.
            return degrees + minutes / 60.0;
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к долготе.
    /// </summary>
    /// <returns>
    /// Долгота в градусах, которая содержится в поле.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public double? ToLongitude()
    {
        //  Проверка ссылки на текстовое значение.
        if (Text is string text)
        {
            //  Определение длины текстового представления.
            int length = text.Length;

            //  Проверка длины текста и символа-разделителя.
            if (length < 5 || length == 6 || (length != 5 && text[5] != '.'))
            {
                //  Неверный формат строки в формате NMEA.
                throw ExceptionsCreator.NmeaInvalidFormat();
            }

            //  Определение градусов.
            int degrees = ParceIntegerCore(text[..3], 0, 180);

            //  Определение минут.
            double minutes = ParceFloatingCore(text[3..], 0, 100);

            //  Возврат долготы.
            return degrees + minutes / 60.0;
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к значению, определяющему направление широты.
    /// </summary>
    /// <returns>
    /// Значение, определяющее направление широты, которое содержится в поле.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public LatitudeDirection? ToLatitudeDirection()
    {
        //  Чтение символа.
        char? symbol = ToCharacter();

        //  Проверка символа.
        if (symbol.HasValue)
        {
            //  Определение значения.
            return symbol switch
            {
                'N' => LatitudeDirection.North,
                'S' => LatitudeDirection.South,
                _ => throw ExceptionsCreator.NmeaInvalidFormat(),
            };
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к значению, определяющему направление долготы.
    /// </summary>
    /// <returns>
    /// Значение, определяющее направление долготы, которое содержится в поле.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public LongitudeDirection? ToLongitudeDirection()
    {
        //  Чтение символа.
        char? symbol = ToCharacter();

        //  Проверка символа.
        if (symbol.HasValue)
        {
            //  Определение значения.
            return symbol switch
            {
                'E' => LongitudeDirection.Eastern,
                'W' => LongitudeDirection.Western,
                _ => throw ExceptionsCreator.NmeaInvalidFormat(),
            };
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к значению, определяющему направление отклонения курса на магнитный полюс.
    /// </summary>
    /// <returns>
    /// Значение, определяющее направление отклонения курса на магнитный полюс, которое содержится в поле.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public MagneticVariationDirection? ToMagneticVariationDirection()
    {
        //  Чтение символа.
        char? symbol = ToCharacter();

        //  Проверка символа.
        if (symbol.HasValue)
        {
            //  Определение значения.
            return symbol switch
            {
                'E' => MagneticVariationDirection.Easterly,
                'W' => MagneticVariationDirection.Westerly,
                _ => throw ExceptionsCreator.NmeaInvalidFormat(),
            };
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к значению, определяющему способ вычисления координат.
    /// </summary>
    /// <returns>
    /// Значение, определяющее способ вычисления координат, которое содержится в поле.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public GpsSolution? ToGpsSolution()
    {
        //  Проверка ссылки на текстовое значение.
        if (Text is string text)
        {
            //  Проверка длины текста.
            if (text.Length != 1)
            {
                //  Неверный формат строки в формате NMEA.
                throw ExceptionsCreator.NmeaInvalidFormat();
            }

            //  Определение значения.
            return text[0] switch
            {
                '0' => GpsSolution.NotAvailable,
                '1' => GpsSolution.Autonomous,
                '2' => GpsSolution.Differential,
                '3' => GpsSolution.Pps,
                '4' => GpsSolution.FixedRtk,
                '5' => GpsSolution.FloatRtk,
                '6' => GpsSolution.Extrapolation,
                '7' => GpsSolution.FixedCoordinates,
                '8' => GpsSolution.Simulation,
                '9' => GpsSolution.Unknown,
                _ => throw ExceptionsCreator.NmeaInvalidFormat(),
            };
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет преобразование поля к значению, определяющему режим системы позиционирования.
    /// </summary>
    /// <returns>
    /// Значение, определяющее режим системы позиционирования.
    /// </returns>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    public GpsMode? ToGpsMode()
    {
        //  Чтение значения, определяющего режим системы позиционирования.
        char? mode = ToCharacter();

        //  Проверка значения.
        if (mode.HasValue)
        {
            //  Установка значения, определяющего режим системы позиционирования.
            return mode.Value switch
            {
                'A' => GpsMode.Autonomous,
                'D' => GpsMode.Differential,
                'E' => GpsMode.Estimated,
                'M' => GpsMode.Manual,
                'S' => GpsMode.Simulator,
                'N' => GpsMode.NotValid,
                _ => GpsMode.Unknown,
            };
        }
        else
        {
            //  Поле не задано.
            return null;
        }
    }

    /// <summary>
    /// Выполняет разбор сроки, содержащей целое число.
    /// </summary>
    /// <param name="text">
    /// Строка, содержащая целое число.
    /// </param>
    /// <returns>
    /// Результат разбора.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ParceIntegerCore(string text)
    {
        //  Проверка символов строки.
        IntegerCheckCore(text);

        //  Разбор значения.
        if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
        {
            //  Неверный формат строки в формате NMEA.
            throw ExceptionsCreator.NmeaInvalidFormat();
        }

        //  Возврат значения.
        return value;
    }

    /// <summary>
    /// Выполняет разбор сроки, содержащей целое число.
    /// </summary>
    /// <param name="text">
    /// Строка, содержащая целое число.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <returns>
    /// Результат разбора.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ParceIntegerCore(string text, int minValue, int maxValue)
    {
        //  Разбор строки.
        int value = ParceIntegerCore(text);

        //  Проверка значения.
        if (value < minValue || value > maxValue)
        {
            //  Неверный формат строки в формате NMEA.
            throw ExceptionsCreator.NmeaInvalidFormat();
        }

        //  Возврат значения.
        return value;
    }

    /// <summary>
    /// Выполняет разбор сроки, содержащей число с плавающей точкой.
    /// </summary>
    /// <param name="text">
    /// Строка, содержащая число с плавающей точкой.
    /// </param>
    /// <returns>
    /// Результат разбора.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double ParceFloatingCore(string text)
    {
        //  Проверка символов строки.
        FloatingCheckCore(text);

        //  Разбор значения.
        if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
        {
            //  Неверный формат строки в формате NMEA.
            throw ExceptionsCreator.NmeaInvalidFormat();
        }

        //  Возврат значения.
        return value;
    }

    /// <summary>
    /// Выполняет разбор сроки, содержащей число с плавающей точкой.
    /// </summary>
    /// <param name="text">
    /// Строка, содержащая число с плавающей точкой.
    /// </param>
    /// <param name="minValue">
    /// Минимально допустимое значение.
    /// </param>
    /// <param name="maxValue">
    /// Максимально допустимое значение.
    /// </param>
    /// <returns>
    /// Результат разбора.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double ParceFloatingCore(string text, double minValue, double maxValue)
    {
        //  Разбор строки.
        double value = ParceFloatingCore(text);

        //  Проверка значения.
        if (value < minValue || value > maxValue)
        {
            //  Неверный формат строки в формате NMEA.
            throw ExceptionsCreator.NmeaInvalidFormat();
        }

        //  Возврат значения.
        return value;
    }

    /// <summary>
    /// Выполняет проверку символов текстового представления целого числа.
    /// </summary>
    /// <param name="text">
    /// Текстовое представление.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void IntegerCheckCore(string text)
    {
        //  Проверка символов текстового представления.
        CharactersCheckCore(text, _IntegerCharacters);
    }

    /// <summary>
    /// Выполняет проверку символов текстового представления числа с плавающей точкой.
    /// </summary>
    /// <param name="text">
    /// Текстовое представление.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void FloatingCheckCore(string text)
    {
        //  Проверка символов текстового представления.
        CharactersCheckCore(text, _FloatingCharacters);
    }

    /// <summary>
    /// Выполняет проверку символов текстового представления.
    /// </summary>
    /// <param name="text">
    /// Текстовое представление.
    /// </param>
    /// <param name="valid">
    /// Отсортированный по возрастанию массив допустимых символов.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="valid"/> передана пустая ссылка.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// Неверный формат строки в формате NMEA.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void CharactersCheckCore(string text, char[] valid)
    {
        //  Проверка ссылки на текстовое представление.
        text = IsNotNull(text, nameof(text));

        //  Проверка ссылки на массив допустимых символов.
        valid = IsNotNull(valid, nameof(valid));

        //  Определение длины текстового представления.
        int length = text.Length;

        //  Перебор всех символов в текстовом представлении.
        for (int i = 0; i < length; i++)
        {
            //  Проверка символа.
            if (Array.BinarySearch(valid, text[i]) < 0)
            {
                //  Неверный формат строки в формате NMEA.
                throw ExceptionsCreator.NmeaInvalidFormat();
            }
        }
    }

    /// <summary>
    /// Вовзвращает длину строки без учёта начального знака минус.
    /// </summary>
    /// <param name="text">
    /// Текстовая срока для определения длины.
    /// </param>
    /// <returns>
    /// Длина строки.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// В параметре <paramref name="text"/> передана пустая ссылка.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int SignLengthCore(string text)
    {
        //  Проверка ссылки на строку.
        text = IsNotNull(text, nameof(text));

        //  Определение сдлины строки.
        int length = text.Length;

        //  Проверка длины строки и начального знака.
        if (length != 0 && text[0] == '-')
        {
            //  Корректировка длины строки.
            --length;
        }

        //  Возврат длины строки.
        return length;
    }
}
