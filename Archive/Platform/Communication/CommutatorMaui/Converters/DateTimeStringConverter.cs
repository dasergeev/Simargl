using Microsoft.Maui.Graphics;
using System.Globalization;

namespace CommutatorMaui;

/// <summary>
/// Представляет конвертер значения <see cref="DateTime"/> в <see cref="string"/>.
/// </summary>
public sealed class DateTimeStringConverter :
    IValueConverter
{
    /// <summary>
    /// Выполняет преобразование значения.
    /// </summary>
    /// <param name="val">
    /// Значение, произведенное исходной привязкой.
    /// </param>
    /// <param name="targetType">
    /// Тип целевого свойства привязки.
    /// </param>
    /// <param name="parameter">
    /// Используемый параметр преобразователя.
    /// </param>
    /// <param name="culture">
    /// Язык и региональные параметры, используемые в преобразователе.
    /// </param>
    /// <returns>
    /// Преобразованное значение.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Метод не реализован.
    /// </exception>
    public object Convert(object val, Type targetType, object parameter, CultureInfo culture)
    {
        //  Проверка значения.
        DateTime value = (DateTime)val;
        if (value.Date == DateTime.Now.Date)
        {
            return $"{value.Hour}:{value.Minute}"; ;
        }
        else
        {
            if (value.Year == DateTime.Now.Year)
            {
                return $"{value.Day}.{value.Month}.{value.Year} {value.Hour}:{value.Minute}";
            }
            else
            {
                return $"{value.Year} г.";
            }
        }


        //  Метод не реализован.
        throw new NotImplementedException();
    }

    /// <summary>
    /// Преобразует значение.
    /// </summary>
    /// <param name="value">
    /// Значение, произведенное целью привязки.
    /// </param>
    /// <param name="targetType">
    /// Целевой тип преобразования.
    /// </param>
    /// <param name="parameter">
    /// Используемый параметр преобразователя.
    /// </param>
    /// <param name="culture">
    /// Язык и региональные параметры, используемые в преобразователе.
    /// </param>
    /// <returns>
    /// Преобразованное значение.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Метод не реализован.
    /// </exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //  Метод не реализован.
        throw new NotImplementedException();
    }
}
