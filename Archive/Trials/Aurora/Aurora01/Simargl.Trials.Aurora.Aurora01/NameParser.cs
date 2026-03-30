using System.IO;

namespace Simargl.Trials.Aurora.Aurora01;

/// <summary>
/// Предоставляет методы разбора имени.
/// </summary>
public static class NameParser
{
    /// <summary>
    /// Выполняет разбор имени файла nmea.
    /// </summary>
    /// <param name="fileName">
    /// Имя файла.
    /// </param>
    /// <returns>
    /// Время начала записи файла.
    /// </returns>
    public static DateTime NmeaParse(string fileName)
    {
        //  Нормализация имени.
        fileName = fileName.Trim().ToLower();

        //  Блок перехвата всех исключений.
        try
        {
            //  Проверка расширения файла.
            if (!fileName.EndsWith(".nmea")) throw new InvalidDataException("Недопустимое расширение файла.");

            //  Проверка префикса.
            if (!fileName.StartsWith("nmea-")) throw new InvalidDataException("Недопустимый префикс в имени файла.");

            // Проверка разделителей.
            if (fileName[4] != '-' ||
                fileName[9] != '-' ||
                fileName[12] != '-' ||
                fileName[15] != '-' ||
                fileName[18] != '-') throw new InvalidDataException("Недопустимый разделитель в имени файла.");

            //  Разбор времени.
            int year = int.Parse(fileName.Substring(5, 4));
            int month = int.Parse(fileName.Substring(10, 2));
            int day = int.Parse(fileName.Substring(13, 2));
            int hour = int.Parse(fileName.Substring(16, 2));
            int minute = int.Parse(fileName.Substring(19, 2));

            //  Возврат времени.
            return new(year, month, day, hour, minute, 0);
        }
        catch (Exception ex)
        {
            //  Повторный выброс исключения.
            throw new InvalidDataException(
                "Передано недопустимое имя файла.",
                ex);
        }
    }

    /// <summary>
    /// Выполняет разбор имени кадра.
    /// </summary>
    /// <param name="fileName">
    /// Имя файла.
    /// </param>
    /// <returns>
    /// Время начала записи файла.
    /// </returns>
    public static DateTime FrameParse(string fileName)
    {
        //  Нормализация имени.
        fileName = fileName.Trim().ToLower();

        //  Блок перехвата всех исключений.
        try
        {
            //  Проверка расширения файла.
            if (!fileName.EndsWith(".0001")) throw new InvalidDataException("Недопустимое расширение файла.");

            //  Проверка префикса.
            if (!fileName.StartsWith("vp000_0 ")) throw new InvalidDataException("Недопустимый префикс в имени файла.");

            // Проверка разделителей.
            if (fileName[12] != '-' ||
                fileName[15] != '-' ||
                fileName[18] != '-' ||
                fileName[21] != '-' ||
                fileName[24] != '-' ||
                fileName[27] != '-') throw new InvalidDataException("Недопустимый разделитель в имени файла.");

            //  Разбор времени.
            int year = int.Parse(fileName.Substring(8, 4));
            int month = int.Parse(fileName.Substring(13, 2));
            int day = int.Parse(fileName.Substring(16, 2));
            int hour = int.Parse(fileName.Substring(19, 2));
            int minute = int.Parse(fileName.Substring(22, 2));
            int second = int.Parse(fileName.Substring(25, 2));
            int millisecond = int.Parse(fileName.Substring(28, 3));

            //  Возврат времени.
            return new DateTime(year, month, day, hour, minute, second, millisecond).AddMinutes(-1);
        }
        catch (Exception ex)
        {
            //  Повторный выброс исключения.
            throw new InvalidDataException(
                "Передано недопустимое имя файла.",
                ex);
        }
    }


    /// <summary>
    /// Выполняет разбор имени файла данных Adxl.
    /// </summary>
    /// <param name="fileName">
    /// Имя файла.
    /// </param>
    /// <param name="address">
    /// Адрес датчика.
    /// </param>
    /// <returns>
    /// Время начала записи файла данных Adxl.
    /// </returns>
    public static DateTime AdxlParse(string fileName, string address)
    {
        //  Нормализация имени.
        fileName = fileName.Trim().ToLower();

        //  Блок перехвата всех исключений.
        try
        {
            //  Проверка расширения файла.
            if (!fileName.EndsWith(".adxl")) throw new InvalidDataException("Недопустимое расширение файла.");

            //  Проверка префикса.
            if (!fileName.StartsWith("adxl-" + address)) throw new InvalidDataException("Недопустимый префикс в имени файла.");

            // Проверка разделителей.
            if (fileName[17] != '-' ||
                fileName[22] != '-' ||
                fileName[25] != '-' ||
                fileName[28] != '-' ||
                fileName[31] != '-' ||
                fileName[34] != '-' ||
                fileName[37] != '-') throw new InvalidDataException("Недопустимый разделитель в имени файла.");

            //  Разбор времени.
            int year = int.Parse(fileName.Substring(18, 4));
            int month = int.Parse(fileName.Substring(23, 2));
            int day = int.Parse(fileName.Substring(26, 2));
            int hour = int.Parse(fileName.Substring(29, 2));
            int minute = int.Parse(fileName.Substring(32, 2));
            int second = int.Parse(fileName.Substring(35, 2));
            int millisecond = int.Parse(fileName.Substring(38, 3));

            //  Возврат времени.
            return new DateTime(year, month, day, hour, minute, second, millisecond).AddSeconds(-10);
        }
        catch (Exception ex)
        {
            //  Повторный выброс исключения.
            throw new InvalidDataException(
                "Передано недопустимое имя файла.",
                ex);
        }
    }
}
