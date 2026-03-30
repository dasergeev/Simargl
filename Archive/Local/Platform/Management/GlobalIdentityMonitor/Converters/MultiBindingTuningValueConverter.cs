using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Apeiron.Services.GlobalIdentity.Converters;

/// <summary>
/// Конвертер. Реализует преобразование параметров значения получаемых из MultiBinding окна Tuning.
/// </summary>
internal class MultiBindingTuningValueConverter : IMultiValueConverter
{
    /// <summary>
    /// Преобразует данные полученные из MultiBinding окна Tuning в значения.
    /// </summary>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        List<object> resultList = new() { values[0] , values[1] };

        return resultList;
    }

    /// <summary>
    /// Заглушка не используется в данный момент.
    /// </summary>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

