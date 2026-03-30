using Simargl.Hardware.Strain.Demo.Main.Attributes;
using System.Windows;

namespace Simargl.Hardware.Strain.Demo.UI.Primitives;

/// <summary>
/// Представляет элемент управления, отображающий атрибут.
/// </summary>
partial class SensorAttributeView
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    public SensorAttributeView()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();
    }

    /// <summary>
    /// Обрабатывает событие сброса.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        //  Поиск атрибута.
        if (e.Source is FrameworkElement element && element.DataContext is SensorAttribute attribute)
        {
            //  Выполнение сброса.
            attribute.Reset();
        }
    }
}
