namespace Apeiron.Platform.Management.Controls;

/// <summary>
/// Представляет рабочий элемент управления.
/// </summary>
public abstract class WorkControl :
    UserControl
{
    /// <summary>
    /// Загружает рабочий элемент управления.
    /// </summary>
    /// <param name="node">
    /// Текущий узел.
    /// </param>
    internal void Load(ModelNode? node)
    {
        //  Отображение элемента.
        Visibility = Visibility.Visible;

        //  Установка текущего узла.
        SetNode(node);
    }

    /// <summary>
    /// Выгружает рабочий элемент управления.
    /// </summary>
    internal void Unload()
    {
        //  Скрытие элемента.
        Visibility = Visibility.Collapsed;

        //  Сброс текущего узла.
        SetNode(null);
    }

    /// <summary>
    /// Устанавливает текущий узел.
    /// </summary>
    /// <param name="node">
    /// Текущий узел.
    /// </param>
    internal virtual void SetNode(ModelNode? node)
    {
        _ = this;
        _ = node;
    }
}
