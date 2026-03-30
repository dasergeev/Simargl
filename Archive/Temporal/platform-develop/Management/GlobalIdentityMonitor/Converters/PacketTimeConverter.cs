using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Apeiron.Services.GlobalIdentity.Converters;

/// <summary>
/// Конвертер. Анализирует дату и время в ячейки и по условию устанавливает цвет.
/// </summary>
internal class PacketTimeConverter : IMultiValueConverter
{
    /// <summary>
    /// Подсвечивает ячейку если попадает под условие.
    /// </summary>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        SolidColorBrush solidColorBrush = new(Colors.Transparent);

        if (values.Any(x => x == DependencyProperty.UnsetValue))
            return solidColorBrush;

        var receiptTime = (DateTime)values[0];
        var packetTime = (DateTime)values[1];

        // Подсвечиваем строку в таблице если выполняется условие.

        if (packetTime > DateTime.Parse("0001-01-01 00:00:00.0000000"))
        {
            int result1 = DateTime.Compare(receiptTime, packetTime.AddHours(24));
            int result2 = DateTime.Compare(receiptTime, packetTime.AddSeconds(-1));

            if (result1 > 0)
            {
                solidColorBrush = new SolidColorBrush(Colors.MediumPurple);
            }
            if (result2 < 0)
            {
                solidColorBrush = new SolidColorBrush(Colors.MediumPurple);
            }

            return solidColorBrush;
        }
        else
        {
            return solidColorBrush;
        }

    }


    ///// <summary>
    ///// Заглушка не используется в данный момент.
    ///// </summary>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
