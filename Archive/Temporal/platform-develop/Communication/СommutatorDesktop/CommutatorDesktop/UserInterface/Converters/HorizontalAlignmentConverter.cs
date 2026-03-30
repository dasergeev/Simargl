using System.Globalization;
using System.Windows.Data;

namespace Apeiron.Platform.Communication.СommutatorDesktop.UserInterface.Converters;

/// <summary>
/// Представляет конвертер значения <see cref="bool"/> в <see cref="HorizontalAlignment"/>.
/// </summary>
public sealed class HorizontalAlignmentConverter :
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
            return flag ? HorizontalAlignment.Right : HorizontalAlignment.Left;
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
