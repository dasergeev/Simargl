namespace Simargl.Web.Browsing.Core.Controls;

/// <summary>
/// Представляет контейнер веб-элементов.
/// </summary>
internal sealed class WebContainer :
    System.Windows.Forms.UserControl
{
    /// <summary>
    /// Инициализирует новый экземпляр.
    /// </summary>
    internal WebContainer()
    {
        //  Обновление размещения.
        UpdateLayout();
    }

    /// <summary>
    /// Добавляет оболочку.
    /// </summary>
    /// <returns>
    /// Новая оболочка.
    /// </returns>
    public WebShell AddShell()
    {
        //  Создание оболочки.
        WebShell shell = new();

        //  Добавление оболочки.
        Controls.Add(shell);

        //  Обновление размещения.
        UpdateLayout();

        //  Возврат новой оболочки.
        return shell;
    }

    /// <summary>
    /// Удаляет оболочку.
    /// </summary>
    /// <param name="shell">
    /// Удаляемая оболочка.
    /// </param>
    public void RemoveShell(WebShell shell)
    {
        //  Проверка вхождения.
        if (Controls.Contains(shell))
        {
            //  Удаление оболочки.
            Controls.Remove(shell);

            //  Обновление размещения.
            UpdateLayout();
        }
    }

    /// <summary>
    /// Вызывает событие изменения размеров элемента.
    /// </summary>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    protected override void OnResize(EventArgs e)
    {
        //  Вызов метода базового класса.
        base.OnResize(e);

        //  Обновление размещения.
        UpdateLayout();
    }

    /// <summary>
    /// Обновляет размещение элементов.
    /// </summary>
    private void UpdateLayout()
    {
        //  Перебор дочерних элементов.
        foreach (object item in Controls)
        {
            //  Проверка типа элемента.
            if (item is WebShell shell)
            {
                //  Установка границ элемента.
                shell.SetBounds(0, 0, ClientSize.Width, ClientSize.Height);
            }
        }
    }
}
