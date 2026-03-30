using System;

namespace Simargl.Frames.Mera.Raw;

/// <summary>
/// Предоставляет методы для преобразования значений полей.
/// </summary>
internal static class RawMeraInfoFieldConverter
{
    /// <summary>
    /// Выполняет попытку преобразовать значения полей в дату и время.
    /// </summary>
    /// <param name="date">
    /// Значение поля, содержащего дату.
    /// </param>
    /// <param name="time">
    /// Значение поля, содержащего время.
    /// </param>
    /// <param name="result">
    /// Результат преобразования.
    /// </param>
    /// <returns>
    /// Значение, определяющее успешность преобразования.
    /// </returns>
    public static bool TryParse(string? date, string? time, out DateTime result)
    {
        //  Установка значения по умолчанию.
        result = default;

        //  Проверка даты и времени.
        if (DateTime.TryParse(date, out DateTime dateValue) &&
            DateTime.TryParse(time, out DateTime timeValue))
        {
            //  Установка времени.
            result = new(
                dateValue.Year, dateValue.Month, dateValue.Day,
                timeValue.Hour, timeValue.Minute, timeValue.Second,
                timeValue.Microsecond, timeValue.Microsecond);

            //  Значение получено.
            return true;
        }

        //  Не удалось получить значение.
        return false;
    }

    /// <summary>
    /// Выполняет попытку преобразовать значение поля в значение типа <see cref="Double"/>.
    /// </summary>
    /// <param name="text">
    /// Исходный текст.
    /// </param>
    /// <param name="value">
    /// Преобразованное значение.
    /// </param>
    /// <returns>
    /// Значение, определяющее успешность преобразования.
    /// </returns>
    public static bool TryParse(string? text, out double value)
    {
        //  Установка значения по умолчанию.
        value = default;

        //  Проверка ссылки на текст.
        if (text is null)
        {
            //  Нет текста для преобразования.
            return false;
        }

        //  Попытка преобразования исходного текста.
        if (double.TryParse(text, out value))
        {
            //  Значение получено.
            return true;
        }

        //  Попытка преобразования текста с запятой.
        if (double.TryParse(text.Replace('.', ','), out value))
        {
            //  Значение получено.
            return true;
        }

        //  Попытка преобразования текста с точкой.
        if (double.TryParse(text.Replace(',', '.'), out value))
        {
            //  Значение получено.
            return true;
        }

        //  Не удалось получить значение.
        return false;
    }
}
