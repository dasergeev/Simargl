using Apeiron.Platform.Demo.AdxlDemo.Nodes;
using System.Globalization;
using System.Windows.Data;

namespace Apeiron.Platform.Demo.AdxlDemo.UserInterface.Converters;

/// <summary>
/// Представляет конвертер значения <see cref="NodeFormat"/> в изображение.
/// </summary>
public sealed class NodeFormatToImageConverter :
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
        if (value is NodeFormat format)
        {
            //  Определение ключа изображения.
            string key = format switch
            {
                NodeFormat.Network => "OrgChart",
                NodeFormat.NetworkCollection => "Home",
                NodeFormat.AdxlDevice => "FullScreen",
                NodeFormat.AdxlDeviceCollection => "RadialChart",
                NodeFormat.ChannelOrganizer => "Channel",
                NodeFormat.ChannelOrganizerCollection => "Channels",
                _ => "Image",
            };
            
            //  Возврат изображения.
            return Application.Current.FindResource(key);
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
