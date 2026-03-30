using Apeiron.Services.GlobalIdentity.Tunings;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Apeiron.Services.GlobalIdentity.Converters;

/// <summary>
/// Конвертер. Анализирует дату и время в ячейки и по условию устанавливает цвет.
/// </summary>
internal class ReceiptTimeConverter : IValueConverter
{
    /// <summary>
    /// Подсвечивает строку если попадает под условие.
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Получаем и проверяем параметр из настроек.
        int period = 60;
        if (AppTuning.Instance.MainTuning.KeepAlivePeriod > 0)
        {
            period = AppTuning.Instance.MainTuning.KeepAlivePeriod;
        }

        // Подствечиваем строку в таблице если выполняется условие.
        return (DateTime)value <= ((DateTime.Now).AddMinutes(-period)) ?
            new SolidColorBrush(Colors.Salmon)
            : new SolidColorBrush(Colors.White);
    }

    /// <summary>
    /// Заглушка не используется в данный момент.
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}
