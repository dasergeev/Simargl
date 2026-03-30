using Simargl.Hardware.Strain.Demo.Main.Attributes;
using System.Windows;
using System.Windows.Controls;

namespace Simargl.Hardware.Strain.Demo.Core;

/// <summary>
/// 
/// </summary>
public sealed class SensorAttributeFormatTemplateSelector :
    DataTemplateSelector
{
    /// <summary></summary>
    public DataTemplate StaticTemplate { get; set; } = null!;

    /// <summary></summary>
    public DataTemplate ReadableTemplate { get; set; } = null!;

    /// <summary></summary>
    public DataTemplate ResettableTemplate { get; set; } = null!;

    /// <summary></summary>
    public DataTemplate WritableTemplate { get; set; } = null!;

    /// <summary></summary>
    public DataTemplate SelectableTemplate { get; set; } = null!;

    /// <inheritdoc/>
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is SensorAttribute attribute)
        {
            return attribute.Format switch
            {
                SensorAttributeFormat.Static => StaticTemplate,
                SensorAttributeFormat.Readable => ReadableTemplate,
                SensorAttributeFormat.Resettable => ResettableTemplate,
                SensorAttributeFormat.Writable => WritableTemplate,
                SensorAttributeFormat.Selectable => SelectableTemplate,
                _ => null!,
            };
        }
        return null!;
    }
}
