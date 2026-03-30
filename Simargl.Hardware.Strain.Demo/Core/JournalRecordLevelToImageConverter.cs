using Simargl.Hardware.Strain.Demo.Journaling;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Simargl.Hardware.Strain.Demo.Core;

/// <summary>
/// Представляет конвертер для значка уровня записи.
/// </summary>
internal sealed class JournalRecordLevelToImageConverter :
    IValueConverter
{
    /// <summary>
    /// Выполняет прямое преобразование.
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is JournalRecordLevel level)
        {
            string imageName = level switch
            {
                JournalRecordLevel.Information => "Information.png",
                JournalRecordLevel.Warning => "Warning.png",
                JournalRecordLevel.Error => "Error.png",
                JournalRecordLevel.Critical => "Critical.png",
                JournalRecordLevel.Debug => "Debug.png",
                _ => "Debug.png"
            };

            string uri = $"pack://application:,,,/Images/{imageName}";
            return new BitmapImage(new Uri(uri, UriKind.Absolute));
        }

        throw new NotImplementedException();
    }

    /// <summary>
    /// Выполняет обратное преобразование.
    /// </summary>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
