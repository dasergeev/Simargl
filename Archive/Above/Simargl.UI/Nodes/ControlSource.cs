namespace Simargl.UI.Nodes;

/// <summary>
/// Представляет источник элемента управления.
/// </summary>
public abstract class ControlSource :
    Anything
{
    /// <summary>
    /// Возвращает тип элемента управления.
    /// </summary>
    /// <returns>
    /// Тип элемента управления.
    /// </returns>
    internal abstract Type GetControlType();

    /// <summary>
    /// Создаёт элемент управления.
    /// </summary>
    /// <returns>
    /// Новый элемент управления.
    /// </returns>
    internal virtual object CreateInstance()
    {
        //  Создание объекта.
        return Activator.CreateInstance(GetControlType()) ??
            throw new InvalidOperationException("Не удалось создать элемент управления.");
    }
}

/// <summary>
/// Представляет источник элемента управления.
/// </summary>
/// <typeparam name="TControl">
/// Тип элемента управления.
/// </typeparam>
public class ControlSource<TControl> :
    ControlSource
    where TControl : System.Windows.Controls.Control, new()
{
    /// <summary>
    /// Возвращает тип элемента управления.
    /// </summary>
    /// <returns>
    /// Тип элемента управления.
    /// </returns>
    internal sealed override Type GetControlType()
    {
        //  Возврат типа элемента управления.
        return typeof(TControl);
    }
}
