namespace Apeiron.Oriole;

/// <summary>
/// Предоставляет методы для построения путей.
/// </summary>
internal static class OriolePathBuilder
{
    /// <summary>
    /// Постоянная, определяющая имя каталога, содержащего данные геолокации.
    /// </summary>
    public const string GeolocationDirectoryName = "Gps";

    /// <summary>
    /// Постоянная, определяющая длину имени каталога, содержащего данные геолокации.
    /// </summary>
    public const int GeolocationDirectoryNameLength = 3;

    /// <summary>
    /// Постоянная, определяющая префикс имёни каталога, содержащего пакеты исходных данных.
    /// </summary>
    public const string SourceDirectoryNamePrefix = "Adxl";

    /// <summary>
    /// Постоянная, определяющая длину префикса имёни каталога, содержащего пакеты исходных данных.
    /// </summary>
    public const int SourceDirectoryNamePrefixLength = 4;

    /// <summary>
    /// Постоянная, определяющая недействительный номер источника исходных данных.
    /// </summary>
    public const int InvalidNumberSource = -1;

    /// <summary>
    /// Постоянная, определяющая минимальный номер источника исходных данных.
    /// </summary>
    public const int MinNumberSource = 1;

    /// <summary>
    /// Постоянная, определяющая максимальный номер источника исходных данных.
    /// </summary>
    public const int MaxNumberSource = 254;

    /// <summary>
    /// Постоянная, определяющая формат текстового представления номера источника исходных данных.
    /// </summary>
    public const string NumberSourceFormat = "000";

    /// <summary>
    /// Постоянная, определяющая длину текстового представления номера источника исходных данных.
    /// </summary>
    public const int NumberSourceTextLength = 3;

    /// <summary>
    /// Постоянная, определяющая длину имени каталога, содержащего пакеты исходных данных.
    /// </summary>
    public const int SourceDirectoryNameLength = SourceDirectoryNamePrefixLength + NumberSourceTextLength;

    /// <summary>
    /// Выполняет разбор номера источника.
    /// </summary>
    /// <param name="text">
    /// Исхоный текст для разбора.
    /// </param>
    /// <returns>
    /// Номер источника исходных данных.
    /// </returns>
    public static int ParseSourceNumber(string text)
    {
        //  Разбор текстовой строки.
        if (int.TryParse(text, out int number) && MinNumberSource <= number && number <= MaxNumberSource)
        {
            //  Возврат номера источника данных.
            return number;
        }

        //  Возврат недействительный номера источника исходных данных.
        return InvalidNumberSource;
    }

    /// <summary>
    /// Возвращает формат данных, соответствующий указанному номеру источника данных.
    /// </summary>
    /// <param name="sourceNumber">
    /// Номер источника данных.
    /// </param>
    /// <returns>
    /// Формат данных, соответствующий указанному номеру источника данных.
    /// </returns>
    public static DataFormat GetDataFormat(int sourceNumber)
    {
        //  Проверка номера источника данных.
        if (MinNumberSource <= sourceNumber && sourceNumber <= MaxNumberSource)
        {
            //  Возврат формата данных.
            return (DataFormat)sourceNumber;
        }
        else
        {
            //  Возврат недействительного формата данных.
            return DataFormat.Invalid;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public static bool TryParseDirectoryFormat(string name, out DataFormat format)
    {
        format = GetDirectoryNameFormat(name);
        return format != DataFormat.Invalid;
    }


    /// <summary>
    /// Выполняет разбор имени каталога.
    /// </summary>
    /// <param name="name">
    /// Имя каталога.
    /// </param>
    /// <param name="time">
    /// Время каталога.
    /// </param>
    /// <returns>
    /// Результат разбора.
    /// </returns>
    public static bool TryParseDirectoryTime(string name, out DateTime time)
    {
        //  Установка времени по умолчанию.
        time = default;

        //  Проверка имени.
        if (name is null)
        {
            //  Неверное имя каталога.
            return false;
        }

        //  Разбивка имени каталога по разделителю.
        string[] parts = name.Split('-');

        //  Проверка количества частей при разбивке.
        if (parts.Length != 4)
        {
            //  Неверное имя каталога.
            return false;
        }

        //  Получение параметров времени.
        if (int.TryParse(parts[0], out int year) &&
            int.TryParse(parts[1], out int month) &&
            int.TryParse(parts[2], out int day) &&
            int.TryParse(parts[3], out int hour))
        {
            try
            {
                //  Определение времени.
                time = new DateTime(year, month, day, hour, 0, 0);

                //  Время определено.
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                //  Неверное имя каталога.
                return false;
            }
        }
        else
        {
            //  Неверное имя каталога.
            return false;
        }
    }

    /// <summary>
    /// Возвращает дату файла по его имени.
    /// </summary>
    /// <param name="name">
    /// Имя файла.
    /// </param>
    /// <param name="time">
    /// Дата файла.
    /// </param>
    /// <returns>
    /// Результат разбора.
    /// </returns>
    public static bool TryParseFileName(string name, out DateTime time)
    {
        //  Установка времени по умолчанию.
        time = default;

        //  Проверка имени.
        if (name is null)
        {
            //  Неверное имя файла.
            return false;
        }

        //  Разбивка имени файла по разделителю.
        string[] parts = name.Split('.');

        //  Проверка количества частей при разбивке.
        if (parts.Length != 2)
        {
            //  Неверное имя файла.
            return false;
        }

        //  Установка основной части имени.
        name = parts[0];

        //  Разбивка основной части имени по разделителю.
        parts = name.Split('-');

        //  Проверка количества частей при разбивке.
        if (parts.Length != 7)
        {
            //  Неверное имя файла.
            return false;
        }

        //  Получение параметров времени.
        if (int.TryParse(parts[0], out int year) &&
            int.TryParse(parts[1], out int month) &&
            int.TryParse(parts[2], out int day) &&
            int.TryParse(parts[3], out int hour) &&
            int.TryParse(parts[4], out int minute) &&
            int.TryParse(parts[5], out int second) &&
            int.TryParse(parts[6], out int millisecond))
        {
            try
            {
                //  Определение времени.
                time = new DateTime(year, month, day, hour, minute, second, millisecond);

                //  Время определено.
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                //  Неверное имя файла.
                return false;
            }
        }
        else
        {
            //  Неверное имя файла.
            return false;
        }
    }

    /// <summary>
    /// Возвращает формат данных по имени каталога.
    /// </summary>
    /// <param name="directoryName">
    /// Имя каталога.
    /// </param>
    /// <returns>
    /// Формат данных.
    /// </returns>
    public static DataFormat GetDirectoryNameFormat(string directoryName)
    {
        //  Определение формата.
        return directoryName.Length switch
        {
            //  Данные геолокации.
            GeolocationDirectoryNameLength =>
                directoryName == GeolocationDirectoryName ?
                    DataFormat.Geolocation : DataFormat.Invalid,

            //  Исходные данные.
            SourceDirectoryNameLength =>
                (directoryName[..SourceDirectoryNamePrefixLength] == SourceDirectoryNamePrefix &&
                    int.TryParse(directoryName[SourceDirectoryNamePrefixLength..], out int sourceNumber)) ?
                    GetDataFormat(sourceNumber) : DataFormat.Invalid,

            //  Неверный формат данных.
            _ => DataFormat.Invalid,
        };
    }
}
