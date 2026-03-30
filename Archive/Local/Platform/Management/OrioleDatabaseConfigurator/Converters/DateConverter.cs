using System.Globalization;
using System.Windows.Data;

namespace Apeiron.Oriole.DatabaseConfigurator.Converters;

/// <summary>
/// Конвертер даты и времени.
/// Преобразовывает дату в строку формата dd.MM.yyyy для использования в выгрузках.
/// </summary>
public class DateConverter : IValueConverter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime && value is not null)
        {
            return dateTime.ToString("dd.MM.yyyy HH:mm:ss");
        }
        else
        {
            return value!;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string dateTimeString)
        {
            return DateTime.Parse(dateTimeString);
        }
        return value;
    }
}
