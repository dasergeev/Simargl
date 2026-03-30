using Simargl.Engine;
using Simargl.Journaling;
using System.ComponentModel;

namespace Simargl.UI;

/// <summary>
/// Представляет элемент управления.
/// </summary>
public class Control :
    System.Windows.Controls.UserControl
{
    /// <summary>
    /// Возвращает вход в приложение.
    /// </summary>
    protected Entry Entry
    {
        get
        {
            //  Для анализатора.
            _ = this;

            //  Возврат входа в приложение.
            return Entry.Unique;
        }
    }

    /// <summary>
    /// Возвращает журнал.
    /// </summary>
    protected Journal Journal => Entry.Journal;

    /// <summary>
    /// Возвращает значение, определяющее, находится ли элемент приложения в режиме разработки.
    /// </summary>
    protected bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(this);
}
