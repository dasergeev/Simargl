using System.Globalization;
namespace CommutatorMaui;
using Microsoft.Maui.Graphics;


/// <summary>
/// Представляет конвертер значения <see cref="bool"/> в <see cref="TextAlignment"/>.
/// </summary>
public sealed class TextAlignmentConverter :
    IValueConverter
{
    /// <summary>
    /// Выполняет преобразование значения.
    /// </summary>
    /// <param name="value">
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
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //  Проверка значения.
        if (value is bool flag)
        {
            //  Возврат преобразованного значения.
            return flag ? TextAlignment.End : TextAlignment.Start;
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
