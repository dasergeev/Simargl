using Apeiron.Platform.Management.Models;

namespace Apeiron.Platform.Management.Controls;

/// <summary>
/// Представляет главный элемент управления.
/// </summary>
public sealed partial class MainControl :
    UserControl
{
    /// <summary>
    /// Инициализирует новый экземпляр класса.
    /// </summary>
    public MainControl()
    {
        //  Инициализация основных компонентов.
        InitializeComponent();

        //  Установка модели.
        Model = _Explorer.Model;

        //  Установка обработчика события изменения выбранного узла.
        _Explorer.SelectedNodeChanged += Explorer_SelectedNodeChanged;
    }

    /// <summary>
    /// Обрабатывает событие изменения выбранного узла.
    /// </summary>
    /// <param name="sender">
    /// Объект, создавший событие.
    /// </param>
    /// <param name="e">
    /// Аргументы, связанные с событием.
    /// </param>
    private void Explorer_SelectedNodeChanged(object? sender, EventArgs e)
    {
        //  Установка узла в рабочее пространство.
        _WorkSpace.SetNode(_Explorer.SelectedNode);
    }

    /// <summary>
    /// Возвращает модель.
    /// </summary>
    public Model Model { get; }
}
