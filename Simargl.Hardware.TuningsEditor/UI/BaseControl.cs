using System.Windows.Controls;

namespace Simargl.Hardware.TuningsEditor.UI;

/// <summary>
/// Представляет элемент управления.
/// </summary>
public abstract class BaseControl :
    UserControl
{
    /// <summary>
    /// Свойство зависимости для свойства <see cref="Kernel"/>.
    /// </summary>
    public static readonly DependencyProperty KernelProperty =
        MainWindow.KernelProperty.AddOwner(
            ownerType: typeof(Control),
            typeMetadata: new FrameworkPropertyMetadata(
                defaultValue: null,
                flags: FrameworkPropertyMetadataOptions.Inherits));

    /// <summary>
    /// Возвращает ядро приложения.
    /// </summary>
    public Kernel Kernel => (Kernel)GetValue(KernelProperty);
}
